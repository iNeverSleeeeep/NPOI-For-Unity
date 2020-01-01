using NPOI.DDF;
using NPOI.HPSF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation of a workbook.  This is the first object most users
	/// will construct whether they are reading or writing a workbook.  It is also the
	/// top level object for creating new sheets/etc.
	/// </summary>
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author  Shawn Laubach (slaubach at apache dot org)
	[Serializable]
	public class HSSFWorkbook : POIDocument, IWorkbook
	{
		/// <summary>
		/// Totals the sizes of all sheet records and eventually serializes them
		/// </summary>
		private class SheetRecordCollector : RecordVisitor, IDisposable
		{
			private ArrayList _list;

			private int _totalSize;

			public int TotalSize => _totalSize;

			public SheetRecordCollector()
			{
				_totalSize = 0;
				_list = new ArrayList(128);
			}

			public void VisitRecord(NPOI.HSSF.Record.Record r)
			{
				_list.Add(r);
				_totalSize += r.RecordSize;
			}

			public int Serialize(int offset, byte[] data)
			{
				int num = 0;
				int count = _list.Count;
				for (int i = 0; i < count; i++)
				{
					NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)_list[i];
					num += record.Serialize(offset + num, data);
				}
				return num;
			}

			public void Dispose()
			{
			}
		}

		/// The maximum number of cell styles in a .xls workbook.
		/// The 'official' limit is 4,000, but POI allows a slightly larger number.
		/// This extra delta takes into account built-in styles that are automatically
		/// created for new workbooks
		///
		/// See http://office.microsoft.com/en-us/excel-help/excel-specifications-and-limits-HP005199291.aspx
		private const int MAX_STYLES = 4030;

		/// used for compile-time performance/memory optimization.  This determines the
		/// initial capacity for the sheet collection.  Its currently Set to 3.
		/// Changing it in this release will decrease performance
		/// since you're never allowed to have more or less than three sheets!
		public const int INITIAL_CAPACITY = 3;

		/// @deprecated POI will now properly handle Unicode strings without
		/// forceing an encoding
		public const byte ENCODING_COMPRESSED_UNICODE = 0;

		/// @deprecated POI will now properly handle Unicode strings without
		/// forceing an encoding
		public const byte ENCODING_UTF_16 = 1;

		/// this Is the reference to the low level Workbook object
		private InternalWorkbook workbook;

		/// this holds the HSSFSheet objects attached to this workbook
		protected List<HSSFSheet> _sheets;

		/// this holds the HSSFName objects attached to this workbook
		private List<HSSFName> names;

		/// holds whether or not to preserve other nodes in the POIFS.  Used
		/// for macros and embedded objects.
		private bool preserveNodes;

		/// Used to keep track of the data formatter so that all
		/// CreateDataFormatter calls return the same one for a given
		/// book.  This Ensures that updates from one places Is visible
		/// someplace else.
		private HSSFDataFormat formatter;

		/// this holds the HSSFFont objects attached to this workbook.
		/// We only create these from the low level records as required.
		private Hashtable fonts;

		/// Normally, the Workbook will be in a POIFS Stream
		/// called "Workbook". However, some weird XLS generators use "WORKBOOK"
		private static readonly string[] WORKBOOK_DIR_ENTRY_NAMES = new string[2]
		{
			"Workbook",
			"WORKBOOK"
		};

		[NonSerialized]
		private MissingCellPolicy missingCellPolicy = MissingCellPolicy.RETURN_NULL_AND_BLANK;

		/// The locator of user-defined functions.
		/// By default includes functions from the Excel Analysis Toolpack
		[NonSerialized]
		private UDFFinder _udfFinder = UDFFinder.DEFAULT;

		/// <summary>
		/// Retrieves the current policy on what to do when
		/// getting missing or blank cells from a row.
		/// The default is to return blank and null cells.
		/// </summary>
		/// <value>The missing cell policy.</value>
		public MissingCellPolicy MissingCellPolicy
		{
			get
			{
				return missingCellPolicy;
			}
			set
			{
				missingCellPolicy = value;
			}
		}

		/// <summary>
		/// Gets the tab whose data is actually seen when the sheet is opened.
		/// This may be different from the "selected sheet" since excel seems to
		/// allow you to show the data of one sheet when another Is seen "selected"
		/// in the tabs (at the bottom).
		/// </summary>
		public int ActiveSheetIndex => workbook.WindowOne.ActiveSheetIndex;

		/// <summary>
		/// Gets or sets the first tab that is displayed in the list of tabs
		/// in excel.
		/// </summary>
		public int FirstVisibleTab
		{
			get
			{
				return workbook.WindowOne.FirstVisibleTab;
			}
			set
			{
				workbook.WindowOne.FirstVisibleTab = value;
			}
		}

		[Obsolete("Misleading name - use GetFirstVisibleTab() ")]
		public short DisplayedTab
		{
			get
			{
				return (short)FirstVisibleTab;
			}
		}

		/// <summary>
		/// Get the number of spreadsheets in the workbook (this will be three after serialization)
		/// </summary>
		/// <value>The number of sheets.</value>
		public int NumberOfSheets => _sheets.Count;

		/// <summary>
		/// determine whether the Excel GUI will backup the workbook when saving.
		/// </summary>
		/// <value>the current Setting for backups.</value>
		public bool BackupFlag
		{
			get
			{
				BackupRecord backupRecord = workbook.BackupRecord;
				if (backupRecord.Backup != 0)
				{
					return true;
				}
				return false;
			}
			set
			{
				BackupRecord backupRecord = workbook.BackupRecord;
				backupRecord.Backup = (short)(value ? 1 : 0);
			}
		}

		/// <summary>
		/// Get the number of fonts in the font table
		/// </summary>
		/// <value>The number of fonts.</value>
		public short NumberOfFonts => (short)workbook.NumberOfFontRecords;

		public bool IsHidden
		{
			get
			{
				return workbook.WindowOne.Hidden;
			}
			set
			{
				workbook.WindowOne.Hidden = value;
			}
		}

		/// <summary>
		/// Get the number of styles the workbook Contains
		/// </summary>
		/// <value>count of cell styles</value>
		public short NumCellStyles => (short)workbook.NumExFormats;

		/// <summary>
		/// Gets the workbook.
		/// </summary>
		/// <value>The workbook.</value>
		public InternalWorkbook Workbook => workbook;

		/// <summary>
		/// Gets the total number of named ranges in the workboko
		/// </summary>
		/// <value>The number of named ranges</value>
		public int NumberOfNames => names.Count;

		/// <summary>
		/// Is the workbook protected with a password (not encrypted)?
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is write protected; otherwise, <c>false</c>.
		/// </value>
		public bool IsWriteProtected => workbook.IsWriteProtected;

		/// <summary>
		/// Gets the new UID.
		/// </summary>
		/// <value>The new UID.</value>
		public byte[] NewUID => new byte[16];

		/// <summary>
		/// Whether the application shall perform a full recalculation when the workbook is opened.
		///
		/// Typically you want to force formula recalculation when you modify cell formulas or values
		/// of a workbook previously created by Excel. When set to true, this flag will tell Excel
		/// that it needs to recalculate all formulas in the workbook the next time the file is opened.
		///
		/// Note, that recalculation updates cached formula results and, thus, modifies the workbook.
		/// Depending on the version, Excel may prompt you with "Do you want to save the changes in <em>filename</em>?"
		/// on close.
		///
		/// Value is true if the application will perform a full recalculation of
		/// workbook values when the workbook is opened.
		///
		/// since 3.8
		/// </summary>
		public bool ForceFormulaRecalculation
		{
			get
			{
				InternalWorkbook internalWorkbook = Workbook;
				RecalcIdRecord recalcIdRecord = (RecalcIdRecord)internalWorkbook.FindFirstRecordBySid(449);
				if (recalcIdRecord != null)
				{
					return recalcIdRecord.EngineId != 0;
				}
				return false;
			}
			set
			{
				InternalWorkbook internalWorkbook = Workbook;
				RecalcIdRecord recalcId = internalWorkbook.RecalcId;
				recalcId.EngineId = 0;
			}
		}

		public DirectoryNode RootDirectory => directory;

		public ISheet this[int index]
		{
			get
			{
				return GetSheetAt(index);
			}
			set
			{
				if (_sheets[index] != null)
				{
					_sheets[index] = (HSSFSheet)value;
				}
				else
				{
					_sheets.Insert(index, (HSSFSheet)value);
				}
			}
		}

		public int Count => NumberOfSheets;

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ICreationHelper GetCreationHelper()
		{
			return new HSSFCreationHelper(this);
		}

		public static HSSFWorkbook Create(InternalWorkbook book)
		{
			return new HSSFWorkbook(book);
		}

		/// <summary>
		/// Creates new HSSFWorkbook from scratch (start here!)
		/// </summary>
		public HSSFWorkbook()
			: this(InternalWorkbook.CreateWorkbook())
		{
		}

		public HSSFWorkbook(InternalWorkbook book)
			: base((DirectoryNode)null)
		{
			workbook = book;
			_sheets = new List<HSSFSheet>(3);
			names = new List<HSSFName>(3);
		}

		public HSSFWorkbook(POIFSFileSystem fs)
			: this(fs, preserveNodes: true)
		{
		}

		/// <summary>
		/// given a POI POIFSFileSystem object, Read in its Workbook and populate the high and
		/// low level models.  If you're Reading in a workbook...start here.
		/// </summary>
		/// <param name="fs">the POI filesystem that Contains the Workbook stream.</param>
		/// <param name="preserveNodes">whether to preseve other nodes, such as
		/// macros.  This takes more memory, so only say yes if you
		/// need to. If Set, will store all of the POIFSFileSystem
		/// in memory</param>
		public HSSFWorkbook(POIFSFileSystem fs, bool preserveNodes)
			: this(fs.Root, fs, preserveNodes)
		{
		}

		private static string GetWorkbookDirEntryName(DirectoryNode directory)
		{
			string[] wORKBOOK_DIR_ENTRY_NAMES = WORKBOOK_DIR_ENTRY_NAMES;
			foreach (string text in wORKBOOK_DIR_ENTRY_NAMES)
			{
				try
				{
					directory.GetEntry(text);
					return text;
				}
				catch (FileNotFoundException)
				{
				}
			}
			try
			{
				directory.GetEntry("Book");
				throw new OldExcelFormatException("The supplied spreadsheet seems to be Excel 5.0/7.0 (BIFF5) format. POI only supports BIFF8 format (from Excel versions 97/2000/XP/2003)");
			}
			catch (FileNotFoundException)
			{
			}
			throw new ArgumentException("The supplied POIFSFileSystem does not contain a BIFF8 'Workbook' entry. Is it really an excel file?");
		}

		/// <summary>
		/// given a POI POIFSFileSystem object, and a specific directory
		/// within it, Read in its Workbook and populate the high and
		/// low level models.  If you're Reading in a workbook...start here.
		/// </summary>
		/// <param name="directory">the POI filesystem directory to Process from</param>
		/// <param name="fs">the POI filesystem that Contains the Workbook stream.</param>
		/// <param name="preserveNodes">whether to preseve other nodes, such as
		/// macros.  This takes more memory, so only say yes if you
		/// need to. If Set, will store all of the POIFSFileSystem
		/// in memory</param>
		public HSSFWorkbook(DirectoryNode directory, POIFSFileSystem fs, bool preserveNodes)
			: this(directory, preserveNodes)
		{
		}

		/// given a POI POIFSFileSystem object, and a specific directory
		///  within it, read in its Workbook and populate the high and
		///  low level models.  If you're reading in a workbook...start here.
		///
		/// @param directory the POI filesystem directory to process from
		/// @param preserveNodes whether to preseve other nodes, such as
		///        macros.  This takes more memory, so only say yes if you
		///        need to. If set, will store all of the POIFSFileSystem
		///        in memory
		/// @see org.apache.poi.poifs.filesystem.POIFSFileSystem
		/// @exception IOException if the stream cannot be read
		public HSSFWorkbook(DirectoryNode directory, bool preserveNodes)
			: base(directory)
		{
			string workbookDirEntryName = GetWorkbookDirEntryName(directory);
			this.preserveNodes = preserveNodes;
			if (!preserveNodes)
			{
				base.directory = null;
			}
			_sheets = new List<HSSFSheet>(3);
			names = new List<HSSFName>(3);
			Stream @in = directory.CreatePOIFSDocumentReader(workbookDirEntryName);
			List<NPOI.HSSF.Record.Record> list = RecordFactory.CreateRecords(@in);
			workbook = InternalWorkbook.CreateWorkbook(list);
			SetPropertiesFromWorkbook(workbook);
			int numRecords = workbook.NumRecords;
			ConvertLabelRecords(list, numRecords);
			RecordStream recordStream = new RecordStream(list, numRecords);
			while (recordStream.HasNext())
			{
				InternalSheet sheet = InternalSheet.CreateSheet(recordStream);
				_sheets.Add(new HSSFSheet(this, sheet));
			}
			for (int i = 0; i < workbook.NumNames; i++)
			{
				NameRecord nameRecord = workbook.GetNameRecord(i);
				HSSFName item = new HSSFName(this, workbook.GetNameRecord(i), workbook.GetNameCommentRecord(nameRecord));
				names.Add(item);
			}
		}

		public HSSFWorkbook(Stream s)
			: this(s, preserveNodes: true)
		{
		}

		/// Companion to HSSFWorkbook(POIFSFileSystem), this constructs the POI filesystem around your
		/// inputstream.
		///
		/// @param s  the POI filesystem that Contains the Workbook stream.
		/// @param preserveNodes whether to preseve other nodes, such as
		///        macros.  This takes more memory, so only say yes if you
		///        need to.
		/// @see org.apache.poi.poifs.filesystem.POIFSFileSystem
		/// @see #HSSFWorkbook(POIFSFileSystem)
		/// @exception IOException if the stream cannot be Read
		public HSSFWorkbook(Stream s, bool preserveNodes)
			: this(new POIFSFileSystem(s), preserveNodes)
		{
		}

		/// used internally to Set the workbook properties.
		private void SetPropertiesFromWorkbook(InternalWorkbook book)
		{
			workbook = book;
		}

		/// <summary>
		/// This is basically a kludge to deal with the now obsolete Label records.  If
		/// you have to read in a sheet that contains Label records, be aware that the rest
		/// of the API doesn't deal with them, the low level structure only provides Read-only
		/// semi-immutable structures (the Sets are there for interface conformance with NO
		/// impelmentation).  In short, you need to call this function passing it a reference
		/// to the Workbook object.  All labels will be converted to LabelSST records and their
		/// contained strings will be written to the Shared String tabel (SSTRecord) within
		/// the Workbook.
		/// </summary>
		/// <param name="records">The records.</param>
		/// <param name="offset">The offset.</param>
		private void ConvertLabelRecords(IList records, int offset)
		{
			for (int i = offset; i < records.Count; i++)
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)records[i];
				if (record.Sid == 516)
				{
					LabelRecord labelRecord = (LabelRecord)record;
					records.RemoveAt(i);
					LabelSSTRecord labelSSTRecord = new LabelSSTRecord();
					int sSTIndex = workbook.AddSSTString(new NPOI.HSSF.Record.UnicodeString(labelRecord.Value));
					labelSSTRecord.Row = labelRecord.Row;
					labelSSTRecord.Column = labelRecord.Column;
					labelSSTRecord.XFIndex = labelRecord.XFIndex;
					labelSSTRecord.SSTIndex = sSTIndex;
					records.Insert(i, labelSSTRecord);
				}
			}
		}

		/// <summary>
		/// Sets the order of appearance for a given sheet.
		/// </summary>
		/// <param name="sheetname">the name of the sheet to reorder</param>
		/// <param name="pos">the position that we want to Insert the sheet into (0 based)</param>
		public void SetSheetOrder(string sheetname, int pos)
		{
			int sheetIndex = GetSheetIndex(sheetname);
			HSSFSheet item = (HSSFSheet)GetSheet(sheetname);
			_sheets.RemoveAt(sheetIndex);
			_sheets.Insert(pos, item);
			workbook.SetSheetOrder(sheetname, pos);
			FormulaShifter shifter = FormulaShifter.CreateForSheetShift(sheetIndex, pos);
			foreach (HSSFSheet sheet in _sheets)
			{
				sheet.Sheet.UpdateFormulasAfterCellShift(shifter, -1);
			}
			workbook.UpdateNamesAfterCellShift(shifter);
		}

		/// <summary>
		/// Validates the index of the sheet.
		/// </summary>
		/// <param name="index">The index.</param>
		private void ValidateSheetIndex(int index)
		{
			int num = _sheets.Count - 1;
			if (index < 0 || index > num)
			{
				throw new ArgumentException("Sheet index (" + index + ") is out of range (0.." + num + ")");
			}
		}

		/// Test only. Do not use 
		public void InsertChartRecord()
		{
			int index = workbook.FindFirstRecordLocBySid(252);
			byte[] data = new byte[90]
			{
				15,
				0,
				0,
				240,
				82,
				0,
				0,
				0,
				0,
				0,
				6,
				240,
				24,
				0,
				0,
				0,
				1,
				8,
				0,
				0,
				2,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				3,
				0,
				0,
				0,
				51,
				0,
				11,
				240,
				18,
				0,
				0,
				0,
				191,
				0,
				8,
				0,
				8,
				0,
				129,
				1,
				9,
				0,
				0,
				8,
				192,
				1,
				64,
				0,
				0,
				8,
				64,
				0,
				30,
				241,
				16,
				0,
				0,
				0,
				13,
				0,
				0,
				8,
				12,
				0,
				0,
				8,
				23,
				0,
				0,
				8,
				247,
				0,
				0,
				16
			};
			UnknownRecord value = new UnknownRecord(235, data);
			workbook.Records.Insert(index, value);
		}

		/// <summary>
		/// Selects a single sheet. This may be different to
		/// the 'active' sheet (which Is the sheet with focus).
		/// </summary>
		/// <param name="index">The index.</param>
		public void SetSelectedTab(int index)
		{
			ValidateSheetIndex(index);
			int count = _sheets.Count;
			for (int i = 0; i < count; i++)
			{
				GetSheetAt(i).IsSelected = (i == index);
			}
			workbook.WindowOne.NumSelectedTabs = 1;
		}

		/// <summary>
		/// Sets the selected tabs.
		/// </summary>
		/// <param name="indexes">The indexes.</param>
		public void SetSelectedTabs(int[] indexes)
		{
			for (int i = 0; i < indexes.Length; i++)
			{
				ValidateSheetIndex(indexes[i]);
			}
			int count = _sheets.Count;
			for (int j = 0; j < count; j++)
			{
				bool isSelected = false;
				for (int k = 0; k < indexes.Length; k++)
				{
					if (indexes[k] == j)
					{
						isSelected = true;
						break;
					}
				}
				GetSheetAt(j).IsSelected = isSelected;
			}
			workbook.WindowOne.NumSelectedTabs = (short)indexes.Length;
		}

		/// <summary>
		/// Sets the tab whose data is actually seen when the sheet is opened.
		/// This may be different from the "selected sheet" since excel seems to
		/// allow you to show the data of one sheet when another Is seen "selected"
		/// in the tabs (at the bottom).
		/// <param name="index">The sheet number(0 based).</param>
		/// </summary>
		public void SetActiveSheet(int index)
		{
			ValidateSheetIndex(index);
			int count = _sheets.Count;
			for (int i = 0; i < count; i++)
			{
				GetSheetAt(i).SetActive(i == index);
			}
			workbook.WindowOne.ActiveSheetIndex = index;
		}

		/// <summary>
		/// Set the sheet name.
		/// </summary>
		/// <param name="sheetIx">The sheet number(0 based).</param>
		/// <param name="name">The name.</param>
		public void SetSheetName(int sheetIx, string name)
		{
			if (name == null)
			{
				throw new ArgumentException("sheetName must not be null");
			}
			if (workbook.ContainsSheetName(name, sheetIx))
			{
				throw new ArgumentException("The workbook already contains a sheet with this name");
			}
			ValidateSheetIndex(sheetIx);
			workbook.SetSheetName(sheetIx, name);
		}

		/// <summary>
		/// Get the sheet name
		/// </summary>
		/// <param name="sheetIx">The sheet index.</param>
		/// <returns>Sheet name</returns>
		public string GetSheetName(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			return workbook.GetSheetName(sheetIx);
		}

		/// <summary>
		/// Check whether a sheet is hidden
		/// </summary>
		/// <param name="sheetIx">The sheet index.</param>
		/// <returns>
		/// 	<c>true</c> if sheet is hidden; otherwise, <c>false</c>.
		/// </returns>
		public bool IsSheetHidden(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			return workbook.IsSheetHidden(sheetIx);
		}

		/// <summary>
		/// Check whether a sheet is very hidden.
		/// This is different from the normal
		/// hidden status
		/// </summary>
		/// <param name="sheetIx">The sheet index.</param>
		/// <returns>
		/// 	<c>true</c> if sheet is very hidden; otherwise, <c>false</c>.
		/// </returns>
		public bool IsSheetVeryHidden(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			return workbook.IsSheetVeryHidden(sheetIx);
		}

		/// <summary>
		/// Hide or Unhide a sheet
		/// </summary>
		/// <param name="sheetIx">The sheet index</param>
		/// <param name="hidden">True to mark the sheet as hidden, false otherwise</param>
		public void SetSheetHidden(int sheetIx, SheetState hidden)
		{
			ValidateSheetIndex(sheetIx);
			WorkbookUtil.ValidateSheetState(hidden);
			workbook.SetSheetHidden(sheetIx, (int)hidden);
		}

		/// <summary>
		/// Hide or unhide a sheet.
		/// </summary>
		/// <param name="sheetIx">The sheet number</param>
		/// <param name="hidden">0 for not hidden, 1 for hidden, 2 for very hidden</param>
		public void SetSheetHidden(int sheetIx, int hidden)
		{
			ValidateSheetIndex(sheetIx);
			workbook.SetSheetHidden(sheetIx, hidden);
		}

		public void SetSheetHidden(int sheetIx, bool hidden)
		{
			ValidateSheetIndex(sheetIx);
			workbook.SetSheetHidden(sheetIx, hidden);
		}

		/// <summary>
		/// Returns the index of the sheet by his name
		/// </summary>
		/// <param name="name">the sheet name</param>
		/// <returns>index of the sheet (0 based)</returns>
		public int GetSheetIndex(string name)
		{
			return workbook.GetSheetIndex(name);
		}

		/// <summary>
		/// Returns the index of the given sheet
		/// </summary>
		/// <param name="sheet">the sheet to look up</param>
		/// <returns>index of the sheet (0 based).-1
		///  if not found </returns>
		public int GetSheetIndex(ISheet sheet)
		{
			for (int i = 0; i < _sheets.Count; i++)
			{
				if (_sheets[i] == sheet)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns the external sheet index of the sheet
		/// with the given internal index, creating one
		/// if needed.
		/// Used by some of the more obscure formula and
		/// named range things.
		/// </summary>
		/// <param name="internalSheetIndex">Index of the internal sheet.</param>
		/// <returns></returns>
		public int GetExternalSheetIndex(int internalSheetIndex)
		{
			return workbook.CheckExternSheet(internalSheetIndex);
		}

		/// <summary>
		/// Create an HSSFSheet for this HSSFWorkbook, Adds it to the sheets and returns
		/// the high level representation.  Use this to Create new sheets.
		/// </summary>
		/// <returns>HSSFSheet representing the new sheet.</returns>
		public ISheet CreateSheet()
		{
			HSSFSheet hSSFSheet = new HSSFSheet(this);
			_sheets.Add(hSSFSheet);
			workbook.SetSheetName(_sheets.Count - 1, "Sheet" + (_sheets.Count - 1));
			bool isActive = hSSFSheet.IsSelected = (_sheets.Count == 1);
			hSSFSheet.IsActive = isActive;
			return hSSFSheet;
		}

		/// <summary>
		/// Create an HSSFSheet from an existing sheet in the HSSFWorkbook.
		/// </summary>
		/// <param name="sheetIndex">the sheet index</param>
		/// <returns>HSSFSheet representing the Cloned sheet.</returns>
		public ISheet CloneSheet(int sheetIndex)
		{
			ValidateSheetIndex(sheetIndex);
			HSSFSheet hSSFSheet = _sheets[sheetIndex];
			string sheetName = workbook.GetSheetName(sheetIndex);
			ISheet sheet = hSSFSheet.CloneSheet(this);
			sheet.IsSelected = false;
			sheet.IsActive = false;
			string uniqueSheetName = GetUniqueSheetName(sheetName);
			int count = _sheets.Count;
			_sheets.Add((HSSFSheet)sheet);
			workbook.SetSheetName(count, uniqueSheetName);
			int num = FindExistingBuiltinNameRecordIdx(sheetIndex, 13);
			if (num != -1)
			{
				NameRecord name = workbook.CloneFilter(num, count);
				HSSFName item = new HSSFName(this, name);
				names.Add(item);
			}
			return sheet;
		}

		/// <summary>
		/// Gets the name of the unique sheet.
		/// </summary>
		/// <param name="srcName">Name of the SRC.</param>
		/// <returns></returns>
		private string GetUniqueSheetName(string srcName)
		{
			int num = 2;
			string text = srcName;
			int num2 = srcName.LastIndexOf('(');
			if (num2 > 0 && srcName.EndsWith(")", StringComparison.Ordinal))
			{
				string text2 = srcName.Substring(num2 + 1, srcName.Length - num2 - 2);
				try
				{
					num = int.Parse(text2.Trim(), CultureInfo.InvariantCulture);
					num++;
					text = srcName.Substring(0, num2).Trim();
				}
				catch (FormatException)
				{
				}
			}
			string text4;
			do
			{
				string text3 = num++.ToString(CultureInfo.CurrentCulture);
				text4 = ((text.Length + text3.Length + 2 >= 31) ? (text.Substring(0, 31 - text3.Length - 2) + "(" + text3 + ")") : (text + " (" + text3 + ")"));
			}
			while (workbook.GetSheetIndex(text4) != -1);
			return text4;
		}

		/// <summary>
		/// Create an HSSFSheet for this HSSFWorkbook, Adds it to the sheets and
		/// returns the high level representation. Use this to Create new sheets.
		/// </summary>
		/// <param name="sheetname">sheetname to set for the sheet.</param>
		/// <returns>HSSFSheet representing the new sheet.</returns>
		public ISheet CreateSheet(string sheetname)
		{
			if (sheetname == null)
			{
				throw new ArgumentException("sheetName must not be null");
			}
			if (workbook.ContainsSheetName(sheetname, _sheets.Count))
			{
				throw new ArgumentException("The workbook already contains a sheet of this name");
			}
			HSSFSheet hSSFSheet = new HSSFSheet(this);
			workbook.SetSheetName(_sheets.Count, sheetname);
			_sheets.Add(hSSFSheet);
			bool isActive = hSSFSheet.IsSelected = (_sheets.Count == 1);
			hSSFSheet.IsActive = isActive;
			return hSSFSheet;
		}

		/// <summary>
		/// Gets the sheets.
		/// </summary>
		/// <returns></returns>
		private List<HSSFSheet> GetSheets()
		{
			return _sheets;
		}

		/// <summary>
		///  Get the HSSFSheet object at the given index.
		/// </summary>
		///  <param name="index">index of the sheet number (0-based)</param>
		///  <returns>HSSFSheet at the provided index</returns>
		public ISheet GetSheetAt(int index)
		{
			return _sheets[index];
		}

		/// <summary>
		/// Get sheet with the given name (case insensitive match)
		/// </summary>
		/// <param name="name">name of the sheet</param>
		/// <returns>HSSFSheet with the name provided or null if it does not exist</returns>
		public ISheet GetSheet(string name)
		{
			HSSFSheet result = null;
			for (int i = 0; i < _sheets.Count; i++)
			{
				string sheetName = workbook.GetSheetName(i);
				if (sheetName.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					result = _sheets[i];
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// Removes sheet at the given index.
		/// </summary>
		/// <param name="index">index of the sheet  (0-based)</param>
		///             <remarks>
		/// Care must be taken if the Removed sheet Is the currently active or only selected sheet in
		/// the workbook. There are a few situations when Excel must have a selection and/or active
		/// sheet. (For example when printing - see Bug 40414).
		/// This method makes sure that if the Removed sheet was active, another sheet will become
		/// active in its place.  Furthermore, if the Removed sheet was the only selected sheet, another
		/// sheet will become selected.  The newly active/selected sheet will have the same index, or
		/// one less if the Removed sheet was the last in the workbook.
		/// </remarks>
		public void RemoveSheetAt(int index)
		{
			ValidateSheetIndex(index);
			bool isActive = GetSheetAt(index).IsActive;
			bool isSelected = GetSheetAt(index).IsSelected;
			_sheets.RemoveAt(index);
			workbook.RemoveSheet(index);
			int count = _sheets.Count;
			if (count >= 1)
			{
				int num = index;
				if (num >= count)
				{
					num = count - 1;
				}
				if (isActive)
				{
					SetActiveSheet(num);
				}
				if (isSelected)
				{
					bool flag = false;
					for (int i = 0; i < count; i++)
					{
						if (GetSheetAt(i).IsSelected)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						SetSelectedTab(num);
					}
				}
			}
		}

		/// <summary>
		/// Sets the repeating rows and columns for a sheet (as found in
		/// File-&gt;PageSetup-&gt;Sheet).  This Is function Is included in the workbook
		/// because it Creates/modifies name records which are stored at the
		/// workbook level.
		/// </summary>
		/// <param name="sheetIndex">0 based index to sheet.</param>
		/// <param name="startColumn">0 based start of repeating columns.</param>
		/// <param name="endColumn">0 based end of repeating columns.</param>
		/// <param name="startRow">0 based start of repeating rows.</param>
		/// <param name="endRow">0 based end of repeating rows.</param>
		/// <example>
		/// To set just repeating columns:
		/// workbook.SetRepeatingRowsAndColumns(0,0,1,-1-1);
		/// To set just repeating rows:
		/// workbook.SetRepeatingRowsAndColumns(0,-1,-1,0,4);
		/// To remove all repeating rows and columns for a sheet.
		/// workbook.SetRepeatingRowsAndColumns(0,-1,-1,-1,-1);
		/// </example>
		[Obsolete("use HSSFSheet#setRepeatingRows(CellRangeAddress) or HSSFSheet#setRepeatingColumns(CellRangeAddress)")]
		public void SetRepeatingRowsAndColumns(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
		{
			HSSFSheet hSSFSheet = (HSSFSheet)GetSheetAt(sheetIndex);
			CellRangeAddress repeatingRows = null;
			CellRangeAddress repeatingColumns = null;
			if (startRow != -1)
			{
				repeatingRows = new CellRangeAddress(startRow, endRow, -1, -1);
			}
			if (startColumn != -1)
			{
				repeatingColumns = new CellRangeAddress(-1, -1, startColumn, endColumn);
			}
			hSSFSheet.RepeatingRows = repeatingRows;
			hSSFSheet.RepeatingColumns = repeatingColumns;
		}

		internal int FindExistingBuiltinNameRecordIdx(int sheetIndex, byte builtinCode)
		{
			for (int i = 0; i < names.Count; i++)
			{
				NameRecord nameRecord = workbook.GetNameRecord(i);
				if (nameRecord == null)
				{
					throw new InvalidOperationException("Unable to find all defined names to iterate over");
				}
				if (nameRecord.IsBuiltInName && nameRecord.BuiltInName == builtinCode && nameRecord.SheetNumber - 1 == sheetIndex)
				{
					return i;
				}
			}
			return -1;
		}

		internal HSSFName CreateBuiltInName(byte builtinCode, int sheetIndex)
		{
			NameRecord name = workbook.CreateBuiltInName(builtinCode, sheetIndex + 1);
			HSSFName hSSFName = new HSSFName(this, name, null);
			names.Add(hSSFName);
			return hSSFName;
		}

		internal HSSFName GetBuiltInName(byte builtinCode, int sheetIndex)
		{
			int num = FindExistingBuiltinNameRecordIdx(sheetIndex, builtinCode);
			if (num < 0)
			{
				return null;
			}
			return names[num];
		}

		private bool IsRowColHeaderRecord(NameRecord r)
		{
			if (r.OptionFlag == 32)
			{
				return string.Concat('\a').Equals(r.NameText);
			}
			return false;
		}

		/// <summary>
		/// Create a new Font and Add it to the workbook's font table
		/// </summary>
		/// <returns>new font object</returns>
		public IFont CreateFont()
		{
			workbook.CreateNewFont();
			short num = (short)(NumberOfFonts - 1);
			if (num > 3)
			{
				num = (short)(num + 1);
			}
			if (num == 32767)
			{
				throw new ArgumentException("Maximum number of fonts was exceeded");
			}
			return GetFontAt(num);
		}

		/// <summary>
		/// Finds a font that matches the one with the supplied attributes
		/// </summary>
		/// <param name="boldWeight">The bold weight.</param>
		/// <param name="color">The color.</param>
		/// <param name="fontHeight">Height of the font.</param>
		/// <param name="name">The name.</param>
		/// <param name="italic">if set to <c>true</c> [italic].</param>
		/// <param name="strikeout">if set to <c>true</c> [strikeout].</param>
		/// <param name="typeOffset">The type offset.</param>
		/// <param name="Underline">The underline.</param>
		/// <returns></returns>
		public IFont FindFont(short boldWeight, short color, short fontHeight, string name, bool italic, bool strikeout, FontSuperScript typeOffset, FontUnderlineType underline)
		{
			for (short num = 0; num <= NumberOfFonts; num = (short)(num + 1))
			{
				if (num != 4)
				{
					IFont fontAt = GetFontAt(num);
					if (fontAt.Boldweight == boldWeight && fontAt.Color == color && fontAt.FontHeight == (double)fontHeight && fontAt.FontName.Equals(name) && fontAt.IsItalic == italic && fontAt.IsStrikeout == strikeout && fontAt.TypeOffset == typeOffset && fontAt.Underline == underline)
					{
						return fontAt;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Get the font at the given index number
		/// </summary>
		/// <param name="idx">The index number</param>
		/// <returns>HSSFFont at the index</returns>
		public IFont GetFontAt(short idx)
		{
			if (fonts == null)
			{
				fonts = new Hashtable();
			}
			if (fonts.ContainsKey(idx))
			{
				return (HSSFFont)fonts[idx];
			}
			FontRecord fontRecordAt = workbook.GetFontRecordAt(idx);
			HSSFFont hSSFFont = new HSSFFont(idx, fontRecordAt);
			fonts[idx] = hSSFFont;
			return hSSFFont;
		}

		/// <summary>
		/// Reset the fonts cache, causing all new calls
		/// to getFontAt() to create new objects.
		/// Should only be called after deleting fonts,
		/// and that's not something you should normally do
		/// </summary>
		public void ResetFontCache()
		{
			fonts = new Hashtable();
		}

		/// <summary>
		/// Create a new Cell style and Add it to the workbook's style table
		/// </summary>
		/// <returns>the new Cell Style object</returns>
		public ICellStyle CreateCellStyle()
		{
			if (workbook.NumExFormats == 4030)
			{
				throw new InvalidOperationException("The maximum number of cell styles was exceeded. You can define up to 4000 styles in a .xls workbook");
			}
			ExtendedFormatRecord rec = workbook.CreateCellXF();
			short index = (short)(NumCellStyles - 1);
			return new HSSFCellStyle(index, rec, this);
		}

		/// <summary>
		/// Get the cell style object at the given index
		/// </summary>
		/// <param name="idx">index within the Set of styles</param>
		/// <returns>HSSFCellStyle object at the index</returns>
		public ICellStyle GetCellStyleAt(short idx)
		{
			ExtendedFormatRecord exFormatAt = workbook.GetExFormatAt(idx);
			return new HSSFCellStyle(idx, exFormatAt, this);
		}

		/// <summary>
		/// Write out this workbook to an Outputstream.  Constructs
		/// a new POI POIFSFileSystem, passes in the workbook binary representation  and
		/// Writes it out.
		/// </summary>
		/// <param name="stream">the java OutputStream you wish to Write the XLS to</param>
		public override void Write(Stream stream)
		{
			byte[] bytes = GetBytes();
			POIFSFileSystem pOIFSFileSystem = new POIFSFileSystem();
			if (base.DocumentSummaryInformation == null)
			{
				base.DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
			}
			var customProperties = base.DocumentSummaryInformation.CustomProperties;
			if (customProperties == null)
			{
				customProperties = new HPSF.CustomProperties();
			}
			customProperties.Put("Generator", "NPOI");
			customProperties.Put("Generator Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
			base.DocumentSummaryInformation.CustomProperties = customProperties;
			if (base.SummaryInformation == null)
			{
				base.SummaryInformation = PropertySetFactory.CreateSummaryInformation();
			}
			base.SummaryInformation.ApplicationName = "NPOI";
			List<string> list = new List<string>(1);
			using (MemoryStream stream2 = new MemoryStream(bytes))
			{
				pOIFSFileSystem.CreateDocument(stream2, "Workbook");
				WriteProperties(pOIFSFileSystem, list);
				if (preserveNodes)
				{
					list.Add("Workbook");
					list.Add("WORKBOOK");
					POIUtils.CopyNodes(directory, pOIFSFileSystem.Root, list);
					pOIFSFileSystem.Root.StorageClsid = directory.StorageClsid;
				}
				pOIFSFileSystem.WriteFileSystem(stream);
			}
			bytes = null;
		}

		/// <summary>
		/// Get the bytes of just the HSSF portions of the XLS file.
		/// Use this to construct a POI POIFSFileSystem yourself.
		/// </summary>
		/// <returns>byte[] array containing the binary representation of this workbook and all contained
		/// sheets, rows, cells, etc.</returns>
		public byte[] GetBytes()
		{
			List<HSSFSheet> sheets = GetSheets();
			int count = sheets.Count;
			workbook.PreSerialize();
			for (int i = 0; i < count; i++)
			{
				sheets[i].Sheet.Preserialize();
				sheets[i].PreSerialize();
			}
			int num = workbook.Size;
			SheetRecordCollector[] array = new SheetRecordCollector[count];
			for (int j = 0; j < count; j++)
			{
				workbook.SetSheetBof(j, num);
				using (SheetRecordCollector sheetRecordCollector = new SheetRecordCollector())
				{
					sheets[j].Sheet.VisitContainedRecords(sheetRecordCollector, num);
					num += sheetRecordCollector.TotalSize;
					array[j] = sheetRecordCollector;
				}
			}
			byte[] array2 = new byte[num];
			int num2 = workbook.Serialize(0, array2);
			for (int k = 0; k < count; k++)
			{
				SheetRecordCollector sheetRecordCollector2 = array[k];
				int num3 = sheetRecordCollector2.Serialize(num2, array2);
				if (num3 != sheetRecordCollector2.TotalSize)
				{
					throw new InvalidOperationException("Actual serialized sheet size (" + num3 + ") differs from pre-calculated size (" + sheetRecordCollector2.TotalSize + ") for sheet (" + k + ")");
				}
				num2 += num3;
				sheetRecordCollector2.Dispose();
			}
			return array2;
		}

		[Obsolete("Do not call this method from your applications. Use the methods available in the HSSFRow to Add string HSSFCells")]
		public int AddSSTString(string str)
		{
			return workbook.AddSSTString(new NPOI.HSSF.Record.UnicodeString(str));
		}

		[Obsolete("Do not call this method from your applications. Use the methods available in the HSSFRow to Get string HSSFCells")]
		public string GetSSTString(int index)
		{
			return workbook.GetSSTString(index).String;
		}

		/// Register a new toolpack in this workbook.
		///
		/// @param toopack the toolpack to register
		public void AddToolPack(UDFFinder toopack)
		{
			AggregatingUDFFinder aggregatingUDFFinder = (AggregatingUDFFinder)_udfFinder;
			aggregatingUDFFinder.Add(toopack);
		}

		internal UDFFinder GetUDFFinder()
		{
			return _udfFinder;
		}

		public IName GetName(string name)
		{
			int nameIndex = GetNameIndex(name);
			if (nameIndex < 0)
			{
				return null;
			}
			return names[nameIndex];
		}

		/// <summary>
		/// Gets the Named range
		/// </summary>
		/// <param name="index">position of the named range</param>
		/// <returns>named range high level</returns>
		public IName GetNameAt(int index)
		{
			return names[index];
		}

		/// <summary>
		/// Gets the named range name
		/// </summary>
		/// <param name="index">the named range index (0 based)</param>
		/// <returns>named range name</returns>
		public string GetNameName(int index)
		{
			return GetNameAt(index).NameName;
		}

		public NameRecord GetNameRecord(int nameIndex)
		{
			return Workbook.GetNameRecord(nameIndex);
		}

		/// <summary>
		/// TODO - make this less cryptic / move elsewhere
		/// </summary>
		/// <param name="reFindex">Index to REF entry in EXTERNSHEET record in the Link Table</param>
		/// <param name="definedNameIndex">zero-based to DEFINEDNAME or EXTERNALNAME record</param>
		/// <returns>the string representation of the defined or external name</returns>
		public string ResolveNameXText(int reFindex, int definedNameIndex)
		{
			return workbook.ResolveNameXText(reFindex, definedNameIndex);
		}

		/// <summary>
		/// Sets the printarea for the sheet provided
		/// i.e. Reference = $A$1:$B$2
		/// </summary>
		/// <param name="sheetIndex">Zero-based sheet index (0 Represents the first sheet to keep consistent with java)</param>
		/// <param name="reference">Valid name Reference for the Print Area</param>
		public void SetPrintArea(int sheetIndex, string reference)
		{
			NameRecord nameRecord = workbook.GetSpecificBuiltinRecord(6, sheetIndex + 1);
			if (nameRecord == null)
			{
				nameRecord = workbook.CreateBuiltInName(6, sheetIndex + 1);
			}
			string[] array = reference.Split(',');
			StringBuilder stringBuilder = new StringBuilder(32);
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				SheetNameFormatter.AppendFormat(stringBuilder, GetSheetName(sheetIndex));
				stringBuilder.Append("!");
				stringBuilder.Append(array[i]);
			}
			nameRecord.NameDefinition = HSSFFormulaParser.Parse(stringBuilder.ToString(), this, FormulaType.NamedRange, sheetIndex);
		}

		/// <summary>
		/// Sets the print area.
		/// </summary>
		/// <param name="sheetIndex">Zero-based sheet index (0 = First Sheet)</param>
		/// <param name="startColumn">Column to begin printarea</param>
		/// <param name="endColumn">Column to end the printarea</param>
		/// <param name="startRow">Row to begin the printarea</param>
		/// <param name="endRow">Row to end the printarea</param>
		public void SetPrintArea(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
		{
			CellReference cellReference = new CellReference(startRow, startColumn, pAbsRow: true, pAbsCol: true);
			string str = cellReference.FormatAsString();
			cellReference = new CellReference(endRow, endColumn, pAbsRow: true, pAbsCol: true);
			str = str + ":" + cellReference.FormatAsString();
			SetPrintArea(sheetIndex, str);
		}

		/// <summary>
		/// Retrieves the reference for the printarea of the specified sheet, the sheet name Is Appended to the reference even if it was not specified.
		/// </summary>
		/// <param name="sheetIndex">Zero-based sheet index (0 Represents the first sheet to keep consistent with java)</param>
		/// <returns>String Null if no print area has been defined</returns>
		public string GetPrintArea(int sheetIndex)
		{
			NameRecord specificBuiltinRecord = workbook.GetSpecificBuiltinRecord(6, sheetIndex + 1);
			if (specificBuiltinRecord == null)
			{
				return null;
			}
			return HSSFFormulaParser.ToFormulaString(this, specificBuiltinRecord.NameDefinition);
		}

		/// <summary>
		/// Delete the printarea for the sheet specified
		/// </summary>
		/// <param name="sheetIndex">Zero-based sheet index (0 = First Sheet)</param>
		public void RemovePrintArea(int sheetIndex)
		{
			Workbook.RemoveBuiltinRecord(6, sheetIndex + 1);
		}

		/// <summary>
		/// Creates a new named range and Add it to the model
		/// </summary>
		/// <returns>named range high level</returns>
		public IName CreateName()
		{
			NameRecord name = workbook.CreateName();
			HSSFName hSSFName = new HSSFName(this, name);
			names.Add(hSSFName);
			return hSSFName;
		}

		/// <summary>
		/// Gets the named range index by his name
		/// Note:
		/// Excel named ranges are case-insensitive and
		/// this method performs a case-insensitive search.
		/// </summary>
		/// <param name="name">named range name</param>
		/// <returns>named range index</returns>
		public int GetNameIndex(string name)
		{
			int result = -1;
			for (int i = 0; i < names.Count; i++)
			{
				string nameName = GetNameName(i);
				if (nameName.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// As GetNameIndex(String) is not necessarily unique 
		/// (name + sheet index is unique), this method is more accurate.
		/// </summary>
		/// <param name="name">the name whose index in the list of names of this workbook should be looked up.</param>
		/// <returns>an index value &gt;= 0 if the name was found; -1, if the name was not found</returns>
		public int GetNameIndex(HSSFName name)
		{
			for (int i = 0; i < names.Count; i++)
			{
				if (name == names[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Remove the named range by his index
		/// </summary>
		/// <param name="index">The named range index (0 based)</param>
		public void RemoveName(int index)
		{
			names.RemoveAt(index);
			workbook.RemoveName(index);
		}

		/// <summary>
		/// Creates the instance of HSSFDataFormat for this workbook.
		/// </summary>
		/// <returns>the HSSFDataFormat object</returns>
		public IDataFormat CreateDataFormat()
		{
			if (formatter == null)
			{
				formatter = new HSSFDataFormat(workbook);
			}
			return formatter;
		}

		/// <summary>
		/// Remove the named range by his name
		/// </summary>
		/// <param name="name">named range name</param>
		public void RemoveName(string name)
		{
			int nameIndex = GetNameIndex(name);
			RemoveName(nameIndex);
		}

		/// <summary>
		///  As #removeName(String) is not necessarily unique (name + sheet index is unique), 
		///  this method is more accurate.
		/// </summary>
		/// <param name="name">the name to remove.</param>
		public void RemoveName(HSSFName name)
		{
			int nameIndex = GetNameIndex(name);
			RemoveName(nameIndex);
		}

		public HSSFPalette GetCustomPalette()
		{
			return new HSSFPalette(workbook.CustomPalette);
		}

		/// <summary>
		/// Spits out a list of all the drawing records in the workbook.
		/// </summary>
		/// <param name="fat">if set to <c>true</c> [fat].</param>
		public void DumpDrawingGroupRecords(bool fat)
		{
			DrawingGroupRecord drawingGroupRecord = (DrawingGroupRecord)workbook.FindFirstRecordBySid(235);
			drawingGroupRecord.Decode();
			IList escherRecords = drawingGroupRecord.EscherRecords;
			IEnumerator enumerator = escherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (fat)
				{
					Console.WriteLine(escherRecord.ToString());
				}
				else
				{
					escherRecord.Display(0);
				}
			}
		}

		internal void InitDrawings()
		{
			DrawingManager2 drawingManager = workbook.FindDrawingGroup();
			if (drawingManager != null)
			{
				for (int i = 0; i < NumberOfSheets; i++)
				{
					IDrawing drawingPatriarch = GetSheetAt(i).DrawingPatriarch;
				}
			}
			else
			{
				workbook.CreateDrawingGroup();
			}
		}

		/// <summary>
		/// Adds a picture to the workbook.
		/// </summary>
		/// <param name="pictureData">The bytes of the picture</param>
		/// <param name="format">The format of the picture.  One of 
		/// PictureType.</param>
		/// <returns>the index to this picture (1 based).</returns>
		public int AddPicture(byte[] pictureData, PictureType format)
		{
			InitDrawings();
			byte[] uID;
			using (MD5 mD = MD5.Create())
			{
				uID = mD.ComputeHash(pictureData);
			}
			EscherBitmapBlip escherBitmapBlip = new EscherBitmapBlip();
			escherBitmapBlip.RecordId = (short)(-4072 + format);
			switch (format)
			{
			case PictureType.EMF:
				escherBitmapBlip.Options = 15680;
				break;
			case PictureType.WMF:
				escherBitmapBlip.Options = 8544;
				break;
			case PictureType.PICT:
				escherBitmapBlip.Options = 21536;
				break;
			case PictureType.PNG:
				escherBitmapBlip.Options = 28160;
				break;
			case PictureType.JPEG:
				escherBitmapBlip.Options = 18080;
				break;
			case PictureType.DIB:
				escherBitmapBlip.Options = 31360;
				break;
			}
			escherBitmapBlip.UID = uID;
			escherBitmapBlip.Marker = byte.MaxValue;
			escherBitmapBlip.PictureData = pictureData;
			EscherBSERecord escherBSERecord = new EscherBSERecord();
			escherBSERecord.RecordId = -4089;
			escherBSERecord.Options = (short)(2 | ((int)format << 4));
			escherBSERecord.BlipTypeMacOS = (byte)format;
			escherBSERecord.BlipTypeWin32 = (byte)format;
			escherBSERecord.UID = uID;
			escherBSERecord.Tag = 255;
			escherBSERecord.Size = pictureData.Length + 25;
			escherBSERecord.Ref = 0;
			escherBSERecord.Offset = 0;
			escherBSERecord.BlipRecord = escherBitmapBlip;
			return workbook.AddBSERecord(escherBSERecord);
		}

		/// <summary>
		/// Gets all pictures from the Workbook.
		/// </summary>
		/// <returns>the list of pictures (a list of HSSFPictureData objects.)</returns>
		public IList GetAllPictures()
		{
			List<HSSFPictureData> list = new List<HSSFPictureData>();
			IEnumerator enumerator = workbook.Records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (current is AbstractEscherHolderRecord)
				{
					((AbstractEscherHolderRecord)current).Decode();
					IList escherRecords = ((AbstractEscherHolderRecord)current).EscherRecords;
					SearchForPictures(escherRecords, list);
				}
			}
			return list;
		}

		/// <summary>
		/// Performs a recursive search for pictures in the given list of escher records.
		/// </summary>
		/// <param name="escherRecords">the escher records.</param>
		/// <param name="pictures">the list to populate with the pictures.</param>
		private void SearchForPictures(IList escherRecords, List<HSSFPictureData> pictures)
		{
			IEnumerator enumerator = escherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (current is EscherRecord)
				{
					EscherRecord escherRecord = (EscherRecord)current;
					if (escherRecord is EscherBSERecord)
					{
						EscherBlipRecord blipRecord = ((EscherBSERecord)escherRecord).BlipRecord;
						if (blipRecord != null)
						{
							pictures.Add(new HSSFPictureData(blipRecord));
						}
					}
					SearchForPictures(escherRecord.ChildRecords, pictures);
				}
			}
		}

		/// <summary>
		/// protect a workbook with a password (not encypted, just Sets Writeprotect
		/// flags and the password.
		/// </summary>
		/// <param name="password">password to set</param>
		/// <param name="username">The username.</param>
		public void WriteProtectWorkbook(string password, string username)
		{
			workbook.WriteProtectWorkbook(password, username);
		}

		/// <summary>
		/// Removes the Write protect flag
		/// </summary>
		public void UnwriteProtectWorkbook()
		{
			workbook.UnwriteProtectWorkbook();
		}

		/// <summary>
		/// Gets all embedded OLE2 objects from the Workbook.
		/// </summary>
		/// <returns>the list of embedded objects (a list of HSSFObjectData objects.)</returns>
		public IList<HSSFObjectData> GetAllEmbeddedObjects()
		{
			List<HSSFObjectData> list = new List<HSSFObjectData>();
			for (int i = 0; i < NumberOfSheets; i++)
			{
				GetAllEmbeddedObjects((HSSFSheet)GetSheetAt(i), list);
			}
			return list;
		}

		/// <summary>
		/// Gets all embedded OLE2 objects from the Workbook.
		/// </summary>
		/// <param name="records">the list of records to search.</param>
		/// <param name="objects">the list of embedded objects to populate.</param>
		private void GetAllEmbeddedObjects(HSSFSheet sheet, List<HSSFObjectData> objects)
		{
			HSSFPatriarch hSSFPatriarch = sheet.DrawingPatriarch as HSSFPatriarch;
			if (hSSFPatriarch != null)
			{
				foreach (HSSFShape child in hSSFPatriarch.Children)
				{
					if (child is HSSFObjectData)
					{
						objects.Add((HSSFObjectData)child);
					}
				}
			}
		}

		/// <summary>
		/// Support foreach ISheet, e.g.
		/// HSSFWorkbook workbook = new HSSFWorkbook();
		/// foreach(ISheet sheet in workbook) ...
		/// </summary>
		/// <returns>Enumeration of all the sheets of this workbook</returns>
		public IEnumerator GetEnumerator()
		{
			return _sheets.GetEnumerator();
		}

		/// Changes an external referenced file to another file.
		/// A formular in Excel which refers a cell in another file is saved in two parts: 
		/// The referenced file is stored in an reference table. the row/cell information is saved separate.
		/// This method invokation will only change the reference in the lookup-table itself.
		/// @param oldUrl The old URL to search for and which is to be replaced
		/// @param newUrl The URL replacement
		/// @return true if the oldUrl was found and replaced with newUrl. Otherwise false
		public bool ChangeExternalReference(string oldUrl, string newUrl)
		{
			return workbook.ChangeExternalReference(oldUrl, newUrl);
		}

		public int IndexOf(ISheet item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, ISheet item)
		{
			_sheets.Insert(index, (HSSFSheet)item);
		}

		public void RemoveAt(int index)
		{
			_sheets.RemoveAt(index);
		}

		public void Add(ISheet item)
		{
			_sheets.Add((HSSFSheet)item);
		}

		public void Clear()
		{
			_sheets.Clear();
		}

		public bool Contains(ISheet item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(ISheet[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(ISheet item)
		{
			return _sheets.Remove((HSSFSheet)item);
		}
	}
}
