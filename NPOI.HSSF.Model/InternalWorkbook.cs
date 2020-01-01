using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace NPOI.HSSF.Model
{
	/// Low level model implementation of a Workbook.  Provides creational methods
	/// for Settings and objects contained in the workbook object.
	///
	/// This file Contains the low level binary records starting at the workbook's BOF and
	/// ending with the workbook's EOF.  Use HSSFWorkbook for a high level representation.
	///
	/// The structures of the highlevel API use references to this to perform most of their
	/// operations.  Its probably Unwise to use these low level structures directly Unless you
	/// really know what you're doing.  I recommend you Read the Microsoft Excel 97 Developer's
	/// Kit (Microsoft Press) and the documentation at http://sc.openoffice.org/excelfileformat.pdf
	/// before even attempting to use this.
	///
	///
	/// @author  Luc Girardin (luc dot girardin at macrofocus dot com)
	/// @author  Sergei Kozello (sergeikozello at mail.ru)
	/// @author  Shawn Laubach (slaubach at apache dot org) (Data Formats)
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Brian Sanders (bsanders at risklabs dot com) - custom palette
	/// @author  Dan Sherman (dsherman at Isisph.com)
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @see org.apache.poi.hssf.usermodel.HSSFWorkbook
	/// @version 1.0-pre
	[Serializable]
	public class InternalWorkbook
	{
		/// Excel silently truncates long sheet names to 31 chars.
		/// This constant is used to ensure uniqueness in the first 31 chars
		private const int MAX_SENSITIVE_SHEET_NAME_LEN = 31;

		/// constant used to Set the "codepage" wherever "codepage" is Set in records
		/// (which is duplciated in more than one record)
		private const short CODEPAGE = 1200;

		/// this Contains the Worksheet record objects
		[NonSerialized]
		protected WorkbookRecordList records = new WorkbookRecordList();

		/// this Contains a reference to the SSTRecord so that new stings can be Added
		/// to it.
		[NonSerialized]
		protected SSTRecord sst;

		[NonSerialized]
		private LinkTable linkTable;

		/// holds the "boundsheet" records (aka bundlesheet) so that they can have their
		/// reference to their "BOF" marker
		protected List<BoundSheetRecord> boundsheets;

		protected List<FormatRecord> formats;

		protected List<HyperlinkRecord> hyperlinks;

		protected int numxfs;

		protected int numfonts;

		private int maxformatid = -1;

		private bool uses1904datewindowing;

		[NonSerialized]
		private DrawingManager2 drawingManager;

		private IList escherBSERecords;

		[NonSerialized]
		private WindowOneRecord windowOne;

		[NonSerialized]
		private FileSharingRecord fileShare;

		[NonSerialized]
		private WriteAccessRecord writeAccess;

		[NonSerialized]
		private WriteProtectRecord writeProtect;

		private Dictionary<string, NameCommentRecord> commentRecords;

		public int NumRecords => records.Count;

		/// Gets the number of font records
		///
		/// @return   number of font records in the "font table"
		public int NumberOfFontRecords => numfonts;

		/// Returns the position of the backup record.
		public BackupRecord BackupRecord => (BackupRecord)records[records.Backuppos];

		/// returns the number of boundsheet objects contained in this workbook.
		///
		/// @return number of BoundSheet records
		public int NumSheets => boundsheets.Count;

		/// Get the number of ExtendedFormat records contained in this workbook.
		///
		/// @return int count of ExtendedFormat records
		public int NumExFormats => numxfs;

		public int Size
		{
			get
			{
				int num = 0;
				SSTRecord sSTRecord = null;
				for (int i = 0; i < records.Count; i++)
				{
					NPOI.HSSF.Record.Record record = records[i];
					if (record.Sid != 449 || ((RecalcIdRecord)record).IsNeeded)
					{
						if (record is SSTRecord)
						{
							sSTRecord = (SSTRecord)record;
						}
						num = ((record.Sid != 255 || sSTRecord == null) ? (num + record.RecordSize) : (num + sSTRecord.CalcExtSSTRecordSize()));
					}
				}
				return num;
			}
		}

		/// lazy initialization
		/// Note - creating the link table causes creation of 1 EXTERNALBOOK and 1 EXTERNALSHEET record
		private LinkTable OrCreateLinkTable
		{
			get
			{
				if (linkTable == null)
				{
					linkTable = new LinkTable((short)NumSheets, records);
				}
				return linkTable;
			}
		}

		/// Gets the total number of names
		/// @return number of names
		public int NumNames
		{
			get
			{
				if (linkTable == null)
				{
					return 0;
				}
				return linkTable.NumNames;
			}
		}

		/// Returns the list of FormatRecords in the workbook.
		/// @return ArrayList of FormatRecords in the notebook
		public List<FormatRecord> Formats => formats;

		public IList Hyperlinks => hyperlinks;

		public IList Records => records.Records;

		/// Whether date windowing is based on 1/2/1904 or 1/1/1900.
		/// Some versions of Excel (Mac) can save workbooks using 1904 date windowing.
		///
		/// @return true if using 1904 date windowing
		public bool IsUsing1904DateWindowing => uses1904datewindowing;

		/// Returns the custom palette in use for this workbook; if a custom palette record
		/// does not exist, then it is Created.
		public PaletteRecord CustomPalette
		{
			get
			{
				int palettepos = records.Palettepos;
				PaletteRecord paletteRecord;
				if (palettepos != -1)
				{
					NPOI.HSSF.Record.Record record = records[palettepos];
					if (!(record is PaletteRecord))
					{
						throw new Exception("InternalError: Expected PaletteRecord but got a '" + record + "'");
					}
					paletteRecord = (PaletteRecord)record;
				}
				else
				{
					paletteRecord = CreatePalette();
					records.Add(1, paletteRecord);
					records.Palettepos = 1;
				}
				return paletteRecord;
			}
		}

		public WindowOneRecord WindowOne => windowOne;

		public DrawingManager2 DrawingManager => drawingManager;

		public WriteProtectRecord WriteProtect
		{
			get
			{
				if (writeProtect == null)
				{
					writeProtect = new WriteProtectRecord();
					int num = 0;
					for (num = 0; num < records.Count && !(records[num] is BOFRecord); num++)
					{
					}
					records.Add(num + 1, writeProtect);
				}
				return writeProtect;
			}
		}

		public WriteAccessRecord WriteAccess
		{
			get
			{
				if (writeAccess == null)
				{
					writeAccess = (WriteAccessRecord)CreateWriteAccess();
					int num = 0;
					for (num = 0; num < records.Count && !(records[num] is InterfaceEndRecord); num++)
					{
					}
					records.Add(num + 1, writeAccess);
				}
				return writeAccess;
			}
		}

		public FileSharingRecord FileSharing
		{
			get
			{
				if (fileShare == null)
				{
					fileShare = new FileSharingRecord();
					int num = 0;
					for (num = 0; num < records.Count && !(records[num] is WriteAccessRecord); num++)
					{
					}
					records.Add(num + 1, fileShare);
				}
				return fileShare;
			}
		}

		/// is the workbook protected with a password (not encrypted)?
		public bool IsWriteProtected
		{
			get
			{
				if (fileShare == null)
				{
					return false;
				}
				FileSharingRecord fileSharing = FileSharing;
				return fileSharing.ReadOnly == 1;
			}
		}

		/// Get or create RecalcIdRecord
		///
		/// @see org.apache.poi.hssf.usermodel.HSSFWorkbook#setForceFormulaRecalculation(boolean)
		public RecalcIdRecord RecalcId
		{
			get
			{
				RecalcIdRecord recalcIdRecord = (RecalcIdRecord)FindFirstRecordBySid(449);
				if (recalcIdRecord == null)
				{
					recalcIdRecord = new RecalcIdRecord();
					int num = FindFirstRecordLocBySid(140);
					records.Add(num + 1, recalcIdRecord);
				}
				return recalcIdRecord;
			}
		}

		/// Creates new Workbook with no intitialization --useless right now
		/// @see #CreateWorkbook(List)
		public InternalWorkbook()
		{
			records = new WorkbookRecordList();
			boundsheets = new List<BoundSheetRecord>();
			formats = new List<FormatRecord>();
			hyperlinks = new List<HyperlinkRecord>();
			numxfs = 0;
			numfonts = 0;
			maxformatid = -1;
			uses1904datewindowing = false;
			escherBSERecords = new List<EscherBSERecord>();
			commentRecords = new Dictionary<string, NameCommentRecord>();
		}

		/// Read support  for low level
		/// API.  Pass in an array of Record objects, A Workbook
		/// object is constructed and passed back with all of its initialization Set
		/// to the passed in records and references to those records held. Unlike Sheet
		/// workbook does not use an offset (its assumed to be 0) since its first in a file.
		/// If you need an offset then construct a new array with a 0 offset or Write your
		/// own ;-p.
		///
		/// @param recs an array of Record objects
		/// @return Workbook object
		public static InternalWorkbook CreateWorkbook(List<NPOI.HSSF.Record.Record> recs)
		{
			InternalWorkbook internalWorkbook = new InternalWorkbook();
			List<NPOI.HSSF.Record.Record> list = new List<NPOI.HSSF.Record.Record>(recs.Count / 3);
			internalWorkbook.records.Records = list;
			int i;
			for (i = 0; i < recs.Count; i++)
			{
				NPOI.HSSF.Record.Record record = recs[i];
				if (record.Sid == 10)
				{
					list.Add(record);
					break;
				}
				switch (record.Sid)
				{
				case 133:
					internalWorkbook.boundsheets.Add((BoundSheetRecord)record);
					internalWorkbook.records.Bspos = i;
					goto default;
				case 252:
					internalWorkbook.sst = (SSTRecord)record;
					goto default;
				case 49:
					internalWorkbook.records.Fontpos = i;
					internalWorkbook.numfonts++;
					goto default;
				case 224:
					internalWorkbook.records.Xfpos = i;
					internalWorkbook.numxfs++;
					goto default;
				case 317:
					internalWorkbook.records.Tabpos = i;
					goto default;
				case 18:
					internalWorkbook.records.Protpos = i;
					goto default;
				case 64:
					internalWorkbook.records.Backuppos = i;
					goto default;
				case 23:
					throw new Exception("Extern sheet is part of LinkTable");
				case 24:
				case 430:
					internalWorkbook.linkTable = new LinkTable(recs, i, internalWorkbook.records, internalWorkbook.commentRecords);
					i += internalWorkbook.linkTable.RecordCount - 1;
					break;
				case 1054:
					internalWorkbook.formats.Add((FormatRecord)record);
					internalWorkbook.maxformatid = ((internalWorkbook.maxformatid >= ((FormatRecord)record).IndexCode) ? internalWorkbook.maxformatid : ((FormatRecord)record).IndexCode);
					goto default;
				case 34:
					internalWorkbook.uses1904datewindowing = (((DateWindow1904Record)record).Windowing == 1);
					goto default;
				case 146:
					internalWorkbook.records.Palettepos = i;
					goto default;
				case 61:
					internalWorkbook.windowOne = (WindowOneRecord)record;
					goto default;
				case 92:
					internalWorkbook.writeAccess = (WriteAccessRecord)record;
					goto default;
				case 134:
					internalWorkbook.writeProtect = (WriteProtectRecord)record;
					goto default;
				case 91:
					internalWorkbook.fileShare = (FileSharingRecord)record;
					goto default;
				case 2196:
				{
					NameCommentRecord nameCommentRecord = (NameCommentRecord)record;
					internalWorkbook.commentRecords[nameCommentRecord.NameText] = nameCommentRecord;
					goto default;
				}
				default:
					list.Add(record);
					break;
				}
			}
			for (; i < recs.Count; i++)
			{
				NPOI.HSSF.Record.Record record2 = recs[i];
				short sid = record2.Sid;
				if (sid == 440)
				{
					internalWorkbook.hyperlinks.Add((HyperlinkRecord)record2);
				}
			}
			if (internalWorkbook.windowOne == null)
			{
				internalWorkbook.windowOne = (WindowOneRecord)CreateWindowOne();
			}
			return internalWorkbook;
		}

		/// gets the name comment record
		/// @param nameRecord name record who's comment is required.
		/// @return name comment record or <code>null</code> if there isn't one for the given name.
		public NameCommentRecord GetNameCommentRecord(NameRecord nameRecord)
		{
			if (commentRecords.ContainsKey(nameRecord.NameText))
			{
				return commentRecords[nameRecord.NameText];
			}
			return null;
		}

		/// Creates an empty workbook object with three blank sheets and all the empty
		/// fields.  Use this to Create a workbook from scratch.
		public static InternalWorkbook CreateWorkbook()
		{
			InternalWorkbook internalWorkbook = new InternalWorkbook();
			List<NPOI.HSSF.Record.Record> list = new List<NPOI.HSSF.Record.Record>(30);
			internalWorkbook.records.Records = list;
			List<FormatRecord> list2 = new List<FormatRecord>(8);
			list.Add(CreateBOF());
			list.Add(new InterfaceHdrRecord(1200));
			list.Add(CreateMMS());
			list.Add(InterfaceEndRecord.Instance);
			list.Add(CreateWriteAccess());
			list.Add(CreateCodepage());
			list.Add(CreateDSF());
			list.Add(CreateTabId());
			internalWorkbook.records.Tabpos = list.Count - 1;
			list.Add(CreateFnGroupCount());
			list.Add(CreateWindowProtect());
			list.Add(CreateProtect());
			internalWorkbook.records.Protpos = list.Count - 1;
			list.Add(CreatePassword());
			list.Add(CreateProtectionRev4());
			list.Add(CreatePasswordRev4());
			internalWorkbook.windowOne = (WindowOneRecord)CreateWindowOne();
			list.Add(internalWorkbook.windowOne);
			list.Add(CreateBackup());
			internalWorkbook.records.Backuppos = list.Count - 1;
			list.Add(CreateHideObj());
			list.Add(CreateDateWindow1904());
			list.Add(CreatePrecision());
			list.Add(CreateRefreshAll());
			list.Add(CreateBookBool());
			list.Add(CreateFont());
			list.Add(CreateFont());
			list.Add(CreateFont());
			list.Add(CreateFont());
			internalWorkbook.records.Fontpos = list.Count - 1;
			internalWorkbook.numfonts = 4;
			for (int i = 0; i <= 7; i++)
			{
				NPOI.HSSF.Record.Record record = CreateFormat(i);
				internalWorkbook.maxformatid = ((internalWorkbook.maxformatid >= ((FormatRecord)record).IndexCode) ? internalWorkbook.maxformatid : ((FormatRecord)record).IndexCode);
				list2.Add((FormatRecord)record);
				list.Add(record);
			}
			internalWorkbook.formats = list2;
			for (int j = 0; j < 21; j++)
			{
				list.Add(CreateExtendedFormat(j));
				internalWorkbook.numxfs++;
			}
			internalWorkbook.records.Xfpos = list.Count - 1;
			for (int k = 0; k < 6; k++)
			{
				list.Add(CreateStyle(k));
			}
			list.Add(CreateUseSelFS());
			int num = 1;
			for (int l = 0; l < num; l++)
			{
				BoundSheetRecord item = (BoundSheetRecord)CreateBoundSheet(l);
				list.Add(item);
				internalWorkbook.boundsheets.Add(item);
				internalWorkbook.records.Bspos = list.Count - 1;
			}
			list.Add(CreateCountry());
			for (int m = 0; m < num; m++)
			{
				internalWorkbook.OrCreateLinkTable.CheckExternSheet(m);
			}
			internalWorkbook.sst = new SSTRecord();
			list.Add(internalWorkbook.sst);
			list.Add(CreateExtendedSST());
			list.Add(EOFRecord.instance);
			return internalWorkbook;
		}

		/// Retrieves the Builtin NameRecord that matches the name and index
		/// There shouldn't be too many names to make the sequential search too slow
		/// @param name byte representation of the builtin name to match
		/// @param sheetIndex Index to match
		/// @return null if no builtin NameRecord matches
		public NameRecord GetSpecificBuiltinRecord(byte name, int sheetIndex)
		{
			return OrCreateLinkTable.GetSpecificBuiltinRecord(name, sheetIndex);
		}

		public ExternalSheet GetExternalSheet(int externSheetIndex)
		{
			string[] externalBookAndSheetName = linkTable.GetExternalBookAndSheetName(externSheetIndex);
			if (externalBookAndSheetName == null)
			{
				return null;
			}
			return new ExternalSheet(externalBookAndSheetName[0], externalBookAndSheetName[1]);
		}

		public ExternalName GetExternalName(int externSheetIndex, int externNameIndex)
		{
			string text = linkTable.ResolveNameXText(externSheetIndex, externNameIndex);
			if (text == null)
			{
				return null;
			}
			int ix = linkTable.ResolveNameXIx(externSheetIndex, externNameIndex);
			return new ExternalName(text, externNameIndex, ix);
		}

		/// Removes the specified Builtin NameRecord that matches the name and index
		/// @param name byte representation of the builtin to match
		/// @param sheetIndex zero-based sheet reference
		public void RemoveBuiltinRecord(byte name, int sheetIndex)
		{
			linkTable.RemoveBuiltinRecord(name, sheetIndex);
		}

		/// Gets the font record at the given index in the font table.  Remember
		/// "There is No Four" (someone at M$ must have gone to Rocky Horror one too
		/// many times)
		///
		/// @param idx the index to look at (0 or greater but NOT 4)
		/// @return FontRecord located at the given index
		public FontRecord GetFontRecordAt(int idx)
		{
			int num = idx;
			if (num > 4)
			{
				num--;
			}
			if (num > numfonts - 1)
			{
				throw new IndexOutOfRangeException("There are only " + numfonts + " font records, you asked for " + idx);
			}
			return (FontRecord)records[records.Fontpos - (numfonts - 1) + num];
		}

		/// Creates a new font record and Adds it to the "font table".  This causes the
		/// boundsheets to move down one, extended formats to move down (so this function moves
		/// those pointers as well)
		///
		/// @return FontRecord that was just Created
		public FontRecord CreateNewFont()
		{
			FontRecord fontRecord = (FontRecord)CreateFont();
			records.Add(records.Fontpos + 1, fontRecord);
			records.Fontpos += 1;
			numfonts++;
			return fontRecord;
		}

		/// Check if the cloned sheet has drawings. If yes, then allocate a new drawing group ID and
		/// re-generate shape IDs
		///
		/// @param sheet the cloned sheet
		public void CloneDrawings(InternalSheet sheet)
		{
			FindDrawingGroup();
			if (drawingManager != null)
			{
				int num = sheet.AggregateDrawingRecords(drawingManager, CreateIfMissing: false);
				if (num != -1)
				{
					EscherAggregate escherAggregate = (EscherAggregate)sheet.FindFirstRecordBySid(9876);
					EscherContainerRecord escherContainer = escherAggregate.GetEscherContainer();
					if (escherContainer != null)
					{
						EscherDggRecord dgg = drawingManager.GetDgg();
						int num2 = drawingManager.FindNewDrawingGroupId();
						dgg.AddCluster(num2, 0);
						dgg.DrawingsSaved++;
						EscherDgRecord escherDgRecord = null;
						IEnumerator enumerator = escherContainer.ChildRecords.GetEnumerator();
						while (enumerator.MoveNext())
						{
							object current = enumerator.Current;
							if (current is EscherDgRecord)
							{
								escherDgRecord = (EscherDgRecord)current;
								escherDgRecord.Options = (short)(num2 << 4);
							}
							else if (current is EscherContainerRecord)
							{
								new ArrayList();
								EscherContainerRecord escherContainerRecord = (EscherContainerRecord)current;
								IEnumerator enumerator2 = escherContainerRecord.ChildRecords.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									EscherContainerRecord escherContainerRecord2 = (EscherContainerRecord)enumerator2.Current;
									foreach (EscherRecord childRecord in escherContainerRecord2.ChildRecords)
									{
										switch (childRecord.RecordId)
										{
										case -4086:
										{
											EscherSpRecord escherSpRecord = (EscherSpRecord)childRecord;
											int shapeId = drawingManager.AllocateShapeId((short)num2, escherDgRecord);
											escherDgRecord.NumShapes--;
											escherSpRecord.ShapeId = shapeId;
											break;
										}
										case -4085:
										{
											EscherOptRecord escherOptRecord = (EscherOptRecord)childRecord;
											EscherSimpleProperty escherSimpleProperty = (EscherSimpleProperty)escherOptRecord.Lookup(260);
											if (escherSimpleProperty != null)
											{
												int propertyValue = escherSimpleProperty.PropertyValue;
												EscherBSERecord bSERecord = GetBSERecord(propertyValue);
												bSERecord.Ref++;
											}
											break;
										}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		/// Sets the BOF for a given sheet
		///
		/// @param sheetnum the number of the sheet to Set the positing of the bof for
		/// @param pos the actual bof position
		public void SetSheetBof(int sheetIndex, int pos)
		{
			CheckSheets(sheetIndex);
			GetBoundSheetRec(sheetIndex).PositionOfBof = pos;
		}

		/// Sets the name for a given sheet.  If the boundsheet record doesn't exist and
		/// its only one more than we have, go ahead and Create it.  If its &gt; 1 more than
		/// we have, except
		///
		/// @param sheetnum the sheet number (0 based)
		/// @param sheetname the name for the sheet
		public void SetSheetName(int sheetnum, string sheetname)
		{
			CheckSheets(sheetnum);
			if (sheetname.Length > 31)
			{
				sheetname = sheetname.Substring(0, 31);
			}
			BoundSheetRecord boundSheetRecord = boundsheets[sheetnum];
			boundSheetRecord.Sheetname = sheetname;
		}

		private BoundSheetRecord GetBoundSheetRec(int sheetIndex)
		{
			return boundsheets[sheetIndex];
		}

		/// Determines whether a workbook Contains the provided sheet name.
		///
		/// @param name the name to test (case insensitive match)
		/// @param excludeSheetIdx the sheet to exclude from the Check or -1 to include all sheets in the Check.
		/// @return true if the sheet Contains the name, false otherwise.
		public bool ContainsSheetName(string name, int excludeSheetIdx)
		{
			string text = name;
			if (text.Length > 31)
			{
				text = text.Substring(0, 31);
			}
			for (int i = 0; i < boundsheets.Count; i++)
			{
				BoundSheetRecord boundSheetRec = GetBoundSheetRec(i);
				if (excludeSheetIdx != i)
				{
					string text2 = boundSheetRec.Sheetname;
					if (text2.Length > 31)
					{
						text2 = text2.Substring(0, 31);
					}
					if (text.Equals(text2, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// Sets the name for a given sheet forcing the encoding. This is STILL A BAD IDEA.
		/// Poi now automatically detects Unicode
		///
		///             @deprecated 3-Jan-06 Simply use SetSheetNam e(int sheetnum, String sheetname)
		/// @param sheetnum the sheet number (0 based)
		/// @param sheetname the name for the sheet
		public void SetSheetName(int sheetnum, string sheetname, short encoding)
		{
			CheckSheets(sheetnum);
			BoundSheetRecord boundSheetRecord = boundsheets[sheetnum];
			boundSheetRecord.Sheetname = sheetname;
		}

		/// Sets the order of appearance for a given sheet.
		///
		/// @param sheetname the name of the sheet to reorder
		/// @param pos the position that we want to Insert the sheet into (0 based)
		public void SetSheetOrder(string sheetname, int pos)
		{
			int sheetIndex = GetSheetIndex(sheetname);
			BoundSheetRecord item = boundsheets[sheetIndex];
			boundsheets.RemoveAt(sheetIndex);
			boundsheets.Insert(pos, item);
		}

		/// Gets the name for a given sheet.
		///
		/// @param sheetnum the sheet number (0 based)
		/// @return sheetname the name for the sheet
		public string GetSheetName(int sheetIndex)
		{
			return GetBoundSheetRec(sheetIndex).Sheetname;
		}

		/// Gets the hidden flag for a given sheet.
		///
		/// @param sheetnum the sheet number (0 based)
		/// @return True if sheet is hidden
		public bool IsSheetHidden(int sheetnum)
		{
			return GetBoundSheetRec(sheetnum).IsHidden;
		}

		/// Gets the hidden flag for a given sheet.
		/// Note that a sheet could instead be 
		///  set to be very hidden, which is different
		///  ({@link #isSheetVeryHidden(int)})
		///
		/// @param sheetnum the sheet number (0 based)
		/// @return True if sheet is hidden
		public bool IsSheetVeryHidden(int sheetnum)
		{
			return GetBoundSheetRec(sheetnum).IsVeryHidden;
		}

		/// Hide or Unhide a sheet
		///
		/// @param sheetnum The sheet number
		/// @param hidden True to mark the sheet as hidden, false otherwise
		public void SetSheetHidden(int sheetnum, bool hidden)
		{
			BoundSheetRecord boundSheetRecord = boundsheets[sheetnum];
			boundSheetRecord.IsHidden = hidden;
		}

		/// Hide or unhide a sheet.
		///  0 = not hidden
		///  1 = hidden
		///  2 = very hidden.
		///
		/// @param sheetnum The sheet number
		/// @param hidden 0 for not hidden, 1 for hidden, 2 for very hidden
		public void SetSheetHidden(int sheetnum, int hidden)
		{
			BoundSheetRecord boundSheetRec = GetBoundSheetRec(sheetnum);
			bool isHidden = false;
			bool isVeryHidden = false;
			switch (hidden)
			{
			case 1:
				isHidden = true;
				break;
			case 2:
				isVeryHidden = true;
				break;
			default:
				throw new ArgumentException("Invalid hidden flag " + hidden + " given, must be 0, 1 or 2");
			case 0:
				break;
			}
			boundSheetRec.IsHidden = isHidden;
			boundSheetRec.IsVeryHidden = isVeryHidden;
		}

		/// Get the sheet's index
		/// @param name  sheet name
		/// @return sheet index or -1 if it was not found.
		public int GetSheetIndex(string name)
		{
			int result = -1;
			for (int i = 0; i < boundsheets.Count; i++)
			{
				string sheetName = GetSheetName(i);
				if (sheetName.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		/// if we're trying to Address one more sheet than we have, go ahead and Add it!  if we're
		/// trying to Address &gt;1 more than we have throw an exception!
		private void CheckSheets(int sheetnum)
		{
			if (boundsheets.Count <= sheetnum)
			{
				if (boundsheets.Count + 1 <= sheetnum)
				{
					throw new Exception("Sheet number out of bounds!");
				}
				BoundSheetRecord boundSheetRecord = (BoundSheetRecord)CreateBoundSheet(sheetnum);
				records.Add(records.Bspos + 1, boundSheetRecord);
				records.Bspos += 1;
				boundsheets.Add(boundSheetRecord);
				OrCreateLinkTable.CheckExternSheet(sheetnum);
				FixTabIdRecord();
			}
		}

		public void RemoveSheet(int sheetnum)
		{
			if (boundsheets.Count > sheetnum)
			{
				records.Remove(records.Bspos - (boundsheets.Count - 1) + sheetnum);
				boundsheets.RemoveAt(sheetnum);
				FixTabIdRecord();
			}
			int num = sheetnum + 1;
			for (int i = 0; i < NumNames; i++)
			{
				NameRecord nameRecord = GetNameRecord(i);
				if (nameRecord.SheetNumber == num)
				{
					nameRecord.SheetNumber = 0;
				}
				else if (nameRecord.SheetNumber > num)
				{
					nameRecord.SheetNumber--;
				}
			}
		}

		/// <summary>
		/// make the tabid record look like the current situation.
		/// </summary>
		/// <returns>number of bytes written in the TabIdRecord</returns>
		private int FixTabIdRecord()
		{
			TabIdRecord tabIdRecord = (TabIdRecord)records[records.Tabpos];
			int recordSize = tabIdRecord.RecordSize;
			short[] array = new short[boundsheets.Count];
			for (short num = 0; num < array.Length; num = (short)(num + 1))
			{
				array[num] = num;
			}
			tabIdRecord.SetTabIdArray(array);
			return tabIdRecord.RecordSize - recordSize;
		}

		/// Retrieves the index of the given font
		public int GetFontIndex(FontRecord font)
		{
			for (int i = 0; i <= numfonts; i++)
			{
				FontRecord fontRecord = (FontRecord)records[records.Fontpos - (numfonts - 1) + i];
				if (fontRecord == font)
				{
					if (i > 3)
					{
						return i + 1;
					}
					return i;
				}
			}
			throw new ArgumentException("Could not find that font!");
		}

		/// Returns the StyleRecord for the given
		///  xfIndex, or null if that ExtendedFormat doesn't
		///  have a Style set.
		public StyleRecord GetStyleRecord(int xfIndex)
		{
			bool flag = false;
			for (int i = records.Xfpos; i < records.Count; i++)
			{
				if (flag)
				{
					break;
				}
				NPOI.HSSF.Record.Record record = records[i];
				if (!(record is ExtendedFormatRecord))
				{
					if (record is StyleRecord)
					{
						StyleRecord styleRecord = (StyleRecord)record;
						if (styleRecord.XFIndex == xfIndex)
						{
							return styleRecord;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return null;
		}

		/// Gets the ExtendedFormatRecord at the given 0-based index
		///
		/// @param index of the Extended format record (0-based)
		/// @return ExtendedFormatRecord at the given index
		public ExtendedFormatRecord GetExFormatAt(int index)
		{
			int num = records.Xfpos - (numxfs - 1);
			num += index;
			return (ExtendedFormatRecord)records[num];
		}

		/// Creates a new Cell-type Extneded Format Record and Adds it to the end of
		///  ExtendedFormatRecords collection
		///
		/// @return ExtendedFormatRecord that was Created
		public ExtendedFormatRecord CreateCellXF()
		{
			ExtendedFormatRecord extendedFormatRecord = CreateExtendedFormat();
			records.Add(records.Xfpos + 1, extendedFormatRecord);
			records.Xfpos += 1;
			numxfs++;
			return extendedFormatRecord;
		}

		/// Adds a string to the SST table and returns its index (if its a duplicate
		/// just returns its index and update the counts) ASSUMES compressed Unicode
		/// (meaning 8bit)
		///
		/// @param string the string to be Added to the SSTRecord
		///
		/// @return index of the string within the SSTRecord
		public int AddSSTString(UnicodeString str)
		{
			if (sst == null)
			{
				InsertSST();
			}
			return sst.AddString(str);
		}

		/// given an index into the SST table, this function returns the corresponding String value
		/// @return String containing the SST String
		public UnicodeString GetSSTString(int str)
		{
			if (sst == null)
			{
				InsertSST();
			}
			return sst.GetString(str);
		}

		/// use this function to Add a Shared String Table to an existing sheet (say
		/// generated by a different java api) without an sst....
		/// @see #CreateSST()
		/// @see org.apache.poi.hssf.record.SSTRecord
		public void InsertSST()
		{
			sst = new SSTRecord();
			records.Add(records.Count - 1, CreateExtendedSST());
			records.Add(records.Count - 2, sst);
		}

		/// Serializes all records int the worksheet section into a big byte array. Use
		/// this to Write the Workbook out.
		/// @param offset of the data to be written
		/// @param data array of bytes to Write this to
		public int Serialize(int offset, byte[] data)
		{
			int num = 0;
			SSTRecord sSTRecord = null;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < records.Count; i++)
			{
				NPOI.HSSF.Record.Record record = records[i];
				if (record.Sid != 449 || ((RecalcIdRecord)record).IsNeeded)
				{
					int num3 = 0;
					if (record is SSTRecord)
					{
						sSTRecord = (SSTRecord)record;
						num2 = num;
					}
					if (record.Sid == 255 && sSTRecord != null)
					{
						record = sSTRecord.CreateExtSSTRecord(num2 + offset);
					}
					if (record is BoundSheetRecord)
					{
						if (!flag)
						{
							for (int j = 0; j < boundsheets.Count; j++)
							{
								num3 += boundsheets[j].Serialize(num + offset + num3, data);
							}
							flag = true;
						}
					}
					else
					{
						num3 = record.Serialize(num + offset, data);
					}
					num += num3;
				}
			}
			return num;
		}

		/// Perform any work necessary before the workbook is about to be serialized.
		///
		/// Include in it ant code that modifies the workbook record stream and affects its size.
		public void PreSerialize()
		{
			if (records.Tabpos > 0)
			{
				TabIdRecord tabIdRecord = (TabIdRecord)records[records.Tabpos];
				if (tabIdRecord._tabids.Length < boundsheets.Count)
				{
					FixTabIdRecord();
				}
			}
		}

		/// Creates the BOF record
		/// @see org.apache.poi.hssf.record.BOFRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a BOFRecord
		private static NPOI.HSSF.Record.Record CreateBOF()
		{
			BOFRecord bOFRecord = new BOFRecord();
			bOFRecord.Version = 1536;
			bOFRecord.Type = BOFRecordType.Workbook;
			bOFRecord.Build = 4307;
			bOFRecord.BuildYear = 1996;
			bOFRecord.HistoryBitMask = 65;
			bOFRecord.RequiredVersion = 6;
			return bOFRecord;
		}

		/// Creates the InterfaceHdr record
		/// @see org.apache.poi.hssf.record.InterfaceHdrRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a InterfaceHdrRecord
		[Obsolete]
		protected NPOI.HSSF.Record.Record CreateInterfaceHdr()
		{
			return null;
		}

		/// Creates an MMS record
		/// @see org.apache.poi.hssf.record.MMSRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a MMSRecord
		private static NPOI.HSSF.Record.Record CreateMMS()
		{
			MMSRecord mMSRecord = new MMSRecord();
			mMSRecord.AddMenuCount = 0;
			mMSRecord.DelMenuCount = 0;
			return mMSRecord;
		}

		/// Creates the InterfaceEnd record
		/// @see org.apache.poi.hssf.record.InterfaceEndRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a InterfaceEndRecord
		[Obsolete]
		protected NPOI.HSSF.Record.Record CreateInterfaceEnd()
		{
			return null;
		}

		/// Creates the WriteAccess record containing the logged in user's name
		/// @see org.apache.poi.hssf.record.WriteAccessRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a WriteAccessRecord
		private static NPOI.HSSF.Record.Record CreateWriteAccess()
		{
			WriteAccessRecord writeAccessRecord = new WriteAccessRecord();
			string text = "NPOI";
			try
			{
				string text2 = Environment.UserName;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = text;
				}
				writeAccessRecord.Username = text2;
				return writeAccessRecord;
			}
			catch (SecurityException)
			{
				writeAccessRecord.Username = text;
				return writeAccessRecord;
			}
		}

		/// Creates the Codepage record containing the constant stored in CODEPAGE
		/// @see org.apache.poi.hssf.record.CodepageRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a CodepageRecord
		private static NPOI.HSSF.Record.Record CreateCodepage()
		{
			CodepageRecord codepageRecord = new CodepageRecord();
			codepageRecord.Codepage = 1200;
			return codepageRecord;
		}

		/// Creates the DSF record containing a 0 since HSSF can't even Create Dual Stream Files
		/// @see org.apache.poi.hssf.record.DSFRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a DSFRecord
		private static NPOI.HSSF.Record.Record CreateDSF()
		{
			return new DSFRecord(isBiff5BookStreamPresent: false);
		}

		/// Creates the TabId record containing an array of 0,1,2.  This release of HSSF
		/// always has the default three sheets, no less, no more.
		/// @see org.apache.poi.hssf.record.TabIdRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a TabIdRecord
		private static NPOI.HSSF.Record.Record CreateTabId()
		{
			TabIdRecord tabIdRecord = new TabIdRecord();
			short[] array = new short[1];
			short[] tabIdArray = array;
			tabIdRecord.SetTabIdArray(tabIdArray);
			return tabIdRecord;
		}

		/// Creates the FnGroupCount record containing the Magic number constant of 14.
		/// @see org.apache.poi.hssf.record.FnGroupCountRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a FnGroupCountRecord
		private static NPOI.HSSF.Record.Record CreateFnGroupCount()
		{
			FnGroupCountRecord fnGroupCountRecord = new FnGroupCountRecord();
			fnGroupCountRecord.Count = 14;
			return fnGroupCountRecord;
		}

		/// Creates the WindowProtect record with protect Set to false.
		/// @see org.apache.poi.hssf.record.WindowProtectRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a WindowProtectRecord
		private static NPOI.HSSF.Record.Record CreateWindowProtect()
		{
			return new WindowProtectRecord(protect: false);
		}

		/// Creates the Protect record with protect Set to false.
		/// @see org.apache.poi.hssf.record.ProtectRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a ProtectRecord
		private static ProtectRecord CreateProtect()
		{
			return new ProtectRecord(isProtected: false);
		}

		/// Creates the Password record with password Set to 0.
		/// @see org.apache.poi.hssf.record.PasswordRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a PasswordRecord
		private static NPOI.HSSF.Record.Record CreatePassword()
		{
			return new PasswordRecord(0);
		}

		/// Creates the ProtectionRev4 record with protect Set to false.
		/// @see org.apache.poi.hssf.record.ProtectionRev4Record
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a ProtectionRev4Record
		private static ProtectionRev4Record CreateProtectionRev4()
		{
			return new ProtectionRev4Record(protect: false);
		}

		/// Creates the PasswordRev4 record with password Set to 0.
		/// @see org.apache.poi.hssf.record.PasswordRev4Record
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a PasswordRev4Record
		private static NPOI.HSSF.Record.Record CreatePasswordRev4()
		{
			return new PasswordRev4Record(0);
		}

		/// Creates the WindowOne record with the following magic values: 
		/// horizontal hold - 0x168 
		/// vertical hold   - 0x10e 
		/// width           - 0x3a5c 
		/// height          - 0x23be 
		/// options         - 0x38 
		/// selected tab    - 0 
		/// Displayed tab   - 0 
		/// num selected tab- 0 
		/// tab width ratio - 0x258 
		/// @see org.apache.poi.hssf.record.WindowOneRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a WindowOneRecord
		private static NPOI.HSSF.Record.Record CreateWindowOne()
		{
			WindowOneRecord windowOneRecord = new WindowOneRecord();
			windowOneRecord.HorizontalHold = 360;
			windowOneRecord.VerticalHold = 270;
			windowOneRecord.Width = 14940;
			windowOneRecord.Height = 9150;
			windowOneRecord.Options = 56;
			windowOneRecord.ActiveSheetIndex = 0;
			windowOneRecord.FirstVisibleTab = 0;
			windowOneRecord.NumSelectedTabs = 1;
			windowOneRecord.TabWidthRatio = 600;
			return windowOneRecord;
		}

		/// Creates the Backup record with backup Set to 0. (loose the data, who cares)
		/// @see org.apache.poi.hssf.record.BackupRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a BackupRecord
		private static NPOI.HSSF.Record.Record CreateBackup()
		{
			BackupRecord backupRecord = new BackupRecord();
			backupRecord.Backup = 0;
			return backupRecord;
		}

		/// Creates the HideObj record with hide object Set to 0. (don't hide)
		/// @see org.apache.poi.hssf.record.HideObjRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a HideObjRecord
		private static NPOI.HSSF.Record.Record CreateHideObj()
		{
			HideObjRecord hideObjRecord = new HideObjRecord();
			hideObjRecord.SetHideObj(0);
			return hideObjRecord;
		}

		/// Creates the DateWindow1904 record with windowing Set to 0. (don't window)
		/// @see org.apache.poi.hssf.record.DateWindow1904Record
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a DateWindow1904Record
		private static NPOI.HSSF.Record.Record CreateDateWindow1904()
		{
			DateWindow1904Record dateWindow1904Record = new DateWindow1904Record();
			dateWindow1904Record.Windowing = 0;
			return dateWindow1904Record;
		}

		/// Creates the Precision record with precision Set to true. (full precision)
		/// @see org.apache.poi.hssf.record.PrecisionRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a PrecisionRecord
		private static NPOI.HSSF.Record.Record CreatePrecision()
		{
			PrecisionRecord precisionRecord = new PrecisionRecord();
			precisionRecord.FullPrecision = true;
			return precisionRecord;
		}

		/// Creates the RefreshAll record with refreshAll Set to true. (refresh all calcs)
		/// @see org.apache.poi.hssf.record.RefreshAllRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a RefreshAllRecord
		private static NPOI.HSSF.Record.Record CreateRefreshAll()
		{
			return new RefreshAllRecord(refreshAll: false);
		}

		/// Creates the BookBool record with saveLinkValues Set to 0. (don't save link values)
		/// @see org.apache.poi.hssf.record.BookBoolRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a BookBoolRecord
		private static NPOI.HSSF.Record.Record CreateBookBool()
		{
			BookBoolRecord bookBoolRecord = new BookBoolRecord();
			bookBoolRecord.SaveLinkValues = 0;
			return bookBoolRecord;
		}

		/// Creates a Font record with the following magic values: 
		/// fontheight           = 0xc8
		/// attributes           = 0x0
		/// color palette index  = 0x7fff
		/// bold weight          = 0x190
		/// Font Name Length     = 5 
		/// Font Name            = Arial 
		///
		/// @see org.apache.poi.hssf.record.FontRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a FontRecord
		private static NPOI.HSSF.Record.Record CreateFont()
		{
			FontRecord fontRecord = new FontRecord();
			fontRecord.FontHeight = 200;
			fontRecord.Attributes = 0;
			fontRecord.ColorPaletteIndex = short.MaxValue;
			fontRecord.BoldWeight = 400;
			fontRecord.FontName = "Arial";
			return fontRecord;
		}

		/// Creates an ExtendedFormatRecord object
		/// @param id    the number of the extended format record to Create (meaning its position in
		///        a file as MS Excel would Create it.)
		///
		/// @return record containing an ExtendedFormatRecord
		/// @see org.apache.poi.hssf.record.ExtendedFormatRecord
		/// @see org.apache.poi.hssf.record.Record
		private static NPOI.HSSF.Record.Record CreateExtendedFormat(int id)
		{
			ExtendedFormatRecord extendedFormatRecord = new ExtendedFormatRecord();
			switch (id)
			{
			case 0:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 0;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 1:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 2:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 3:
				extendedFormatRecord.FontIndex = 2;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 4:
				extendedFormatRecord.FontIndex = 2;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 5:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 6:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 7:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 8:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 9:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 10:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 11:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 12:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 13:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 14:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -3072;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 15:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 0;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 16:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 43;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 17:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 41;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 18:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 44;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 19:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 42;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 20:
				extendedFormatRecord.FontIndex = 1;
				extendedFormatRecord.FormatIndex = 9;
				extendedFormatRecord.CellOptions = -11;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = -2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 21:
				extendedFormatRecord.FontIndex = 5;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 2048;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 22:
				extendedFormatRecord.FontIndex = 6;
				extendedFormatRecord.FormatIndex = 0;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 23552;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 23:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 49;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 23552;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 24:
				extendedFormatRecord.FontIndex = 0;
				extendedFormatRecord.FormatIndex = 8;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 23552;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			case 25:
				extendedFormatRecord.FontIndex = 6;
				extendedFormatRecord.FormatIndex = 8;
				extendedFormatRecord.CellOptions = 1;
				extendedFormatRecord.AlignmentOptions = 32;
				extendedFormatRecord.IndentionOptions = 23552;
				extendedFormatRecord.BorderOptions = 0;
				extendedFormatRecord.PaletteOptions = 0;
				extendedFormatRecord.AdtlPaletteOptions = 0;
				extendedFormatRecord.FillPaletteOptions = 8384;
				break;
			}
			return extendedFormatRecord;
		}

		/// Creates an default cell type ExtendedFormatRecord object.
		/// @return ExtendedFormatRecord with intial defaults (cell-type)
		private static ExtendedFormatRecord CreateExtendedFormat()
		{
			ExtendedFormatRecord extendedFormatRecord = new ExtendedFormatRecord();
			extendedFormatRecord.FontIndex = 0;
			extendedFormatRecord.FormatIndex = 0;
			extendedFormatRecord.CellOptions = 1;
			extendedFormatRecord.AlignmentOptions = 32;
			extendedFormatRecord.IndentionOptions = 0;
			extendedFormatRecord.BorderOptions = 0;
			extendedFormatRecord.PaletteOptions = 0;
			extendedFormatRecord.AdtlPaletteOptions = 0;
			extendedFormatRecord.FillPaletteOptions = 8384;
			extendedFormatRecord.TopBorderPaletteIdx = 8;
			extendedFormatRecord.BottomBorderPaletteIdx = 8;
			extendedFormatRecord.LeftBorderPaletteIdx = 8;
			extendedFormatRecord.RightBorderPaletteIdx = 8;
			return extendedFormatRecord;
		}

		/// Creates a new StyleRecord, for the given Extended
		///  Format index, and adds it onto the end of the
		///  records collection
		public StyleRecord CreateStyleRecord(int xfIndex)
		{
			StyleRecord styleRecord = new StyleRecord();
			styleRecord.XFIndex = (short)xfIndex;
			int num = -1;
			for (int i = records.Xfpos; i < records.Count; i++)
			{
				if (num != -1)
				{
					break;
				}
				NPOI.HSSF.Record.Record record = records[i];
				if (!(record is ExtendedFormatRecord) && !(record is StyleRecord))
				{
					num = i;
				}
			}
			if (num == -1)
			{
				throw new InvalidOperationException("No XF Records found!");
			}
			records.Add(num, styleRecord);
			return styleRecord;
		}

		/// Creates a StyleRecord object
		/// @param id        the number of the style record to Create (meaning its position in
		///                  a file as MS Excel would Create it.
		/// @return record containing a StyleRecord
		/// @see org.apache.poi.hssf.record.StyleRecord
		/// @see org.apache.poi.hssf.record.Record
		private static NPOI.HSSF.Record.Record CreateStyle(int id)
		{
			StyleRecord styleRecord = new StyleRecord();
			switch (id)
			{
			case 0:
				styleRecord.XFIndex = -32752;
				styleRecord.SetBuiltinStyle(3);
				styleRecord.OutlineStyleLevel = 255;
				break;
			case 1:
				styleRecord.XFIndex = -32751;
				styleRecord.SetBuiltinStyle(6);
				styleRecord.OutlineStyleLevel = 255;
				break;
			case 2:
				styleRecord.XFIndex = -32750;
				styleRecord.SetBuiltinStyle(4);
				styleRecord.OutlineStyleLevel = 255;
				break;
			case 3:
				styleRecord.XFIndex = -32749;
				styleRecord.SetBuiltinStyle(7);
				styleRecord.OutlineStyleLevel = 255;
				break;
			case 4:
				styleRecord.XFIndex = short.MinValue;
				styleRecord.SetBuiltinStyle(0);
				styleRecord.OutlineStyleLevel = 255;
				break;
			case 5:
				styleRecord.XFIndex = -32748;
				styleRecord.SetBuiltinStyle(5);
				styleRecord.OutlineStyleLevel = 255;
				break;
			}
			return styleRecord;
		}

		/// Creates a palette record initialized to the default palette
		/// @return a PaletteRecord instance populated with the default colors
		/// @see org.apache.poi.hssf.record.PaletteRecord
		private static PaletteRecord CreatePalette()
		{
			return new PaletteRecord();
		}

		/// Creates the UseSelFS object with the use natural language flag Set to 0 (false)
		/// @return record containing a UseSelFSRecord
		/// @see org.apache.poi.hssf.record.UseSelFSRecord
		/// @see org.apache.poi.hssf.record.Record
		private static UseSelFSRecord CreateUseSelFS()
		{
			return new UseSelFSRecord(b: false);
		}

		/// Create a "bound sheet" or "bundlesheet" (depending who you ask) record
		/// Always Sets the sheet's bof to 0.  You'll need to Set that yourself.
		/// @param id either sheet 0,1 or 2.
		/// @return record containing a BoundSheetRecord
		/// @see org.apache.poi.hssf.record.BoundSheetRecord
		/// @see org.apache.poi.hssf.record.Record
		private static NPOI.HSSF.Record.Record CreateBoundSheet(int id)
		{
			return new BoundSheetRecord("Sheet" + (id + 1));
		}

		/// Creates the Country record with the default country Set to 1
		/// and current country Set to 7 in case of russian locale ("ru_RU") and 1 otherwise
		/// @return record containing a CountryRecord
		/// @see org.apache.poi.hssf.record.CountryRecord
		/// @see org.apache.poi.hssf.record.Record
		private static NPOI.HSSF.Record.Record CreateCountry()
		{
			CountryRecord countryRecord = new CountryRecord();
			countryRecord.DefaultCountry = 1;
			if (Thread.CurrentThread.CurrentCulture.Name.Equals("ru_RU"))
			{
				countryRecord.CurrentCountry = 7;
			}
			else
			{
				countryRecord.CurrentCountry = 1;
			}
			return countryRecord;
		}

		/// Creates the ExtendedSST record with numstrings per bucket Set to 0x8.  HSSF
		/// doesn't yet know what to do with this thing, but we Create it with nothing in
		/// it hardly just to make Excel happy and our sheets look like Excel's
		///
		/// @return record containing an ExtSSTRecord
		/// @see org.apache.poi.hssf.record.ExtSSTRecord
		/// @see org.apache.poi.hssf.record.Record
		private static NPOI.HSSF.Record.Record CreateExtendedSST()
		{
			ExtSSTRecord extSSTRecord = new ExtSSTRecord();
			extSSTRecord.NumStringsPerBucket = 8;
			return extSSTRecord;
		}

		/// Finds the sheet name by his extern sheet index
		/// @param num extern sheet index
		/// @return sheet name
		public string FindSheetNameFromExternSheet(int externSheetIndex)
		{
			int indexToInternalSheet = linkTable.GetIndexToInternalSheet(externSheetIndex);
			if (indexToInternalSheet < 0)
			{
				return "";
			}
			if (indexToInternalSheet >= boundsheets.Count)
			{
				return "";
			}
			return GetSheetName(indexToInternalSheet);
		}

		/// Finds the sheet index for a particular external sheet number.
		/// @param externSheetNumber     The external sheet number to Convert
		/// @return  The index to the sheet found.
		public int GetSheetIndexFromExternSheetIndex(int externSheetNumber)
		{
			return linkTable.GetSheetIndexFromExternSheetIndex(externSheetNumber);
		}

		/// returns the extern sheet number for specific sheet number ,
		///  if this sheet doesn't exist in extern sheet , Add it
		/// @param sheetNumber sheet number
		/// @return index to extern sheet
		public int CheckExternSheet(int sheetNumber)
		{
			return OrCreateLinkTable.CheckExternSheet(sheetNumber);
		}

		public int GetExternalSheetIndex(string workbookName, string sheetName)
		{
			return OrCreateLinkTable.GetExternalSheetIndex(workbookName, sheetName);
		}

		/// @param name the  name of an external function, typically a name of a UDF
		/// @param udf  locator of user-defiend functions to resolve names of VBA and Add-In functions
		/// @return the external name or null
		public NameXPtg GetNameXPtg(string name, UDFFinder udf)
		{
			LinkTable orCreateLinkTable = OrCreateLinkTable;
			NameXPtg nameXPtg = orCreateLinkTable.GetNameXPtg(name);
			if (nameXPtg == null && udf.FindFunction(name) != null)
			{
				nameXPtg = orCreateLinkTable.AddNameXPtg(name);
			}
			return nameXPtg;
		}

		/// Gets the name record
		/// @param index name index
		/// @return name record
		public NameRecord GetNameRecord(int index)
		{
			return linkTable.GetNameRecord(index);
		}

		/// Creates new name
		/// @return new name record
		public NameRecord CreateName()
		{
			return AddName(new NameRecord());
		}

		/// Creates new name
		/// @return new name record
		public NameRecord AddName(NameRecord name)
		{
			OrCreateLinkTable.AddName(name);
			return name;
		}

		/// Generates a NameRecord to represent a built-in region
		/// @return a new NameRecord Unless the index is invalid
		public NameRecord CreateBuiltInName(byte builtInName, int index)
		{
			if (index == -1 || index + 1 > 32767)
			{
				throw new ArgumentException("Index is not valid [" + index + "]");
			}
			NameRecord nameRecord = new NameRecord(builtInName, (short)index);
			AddName(nameRecord);
			return nameRecord;
		}

		/// Removes the name
		/// @param namenum name index
		public void RemoveName(int namenum)
		{
			if (linkTable.NumNames > namenum)
			{
				int num = FindFirstRecordLocBySid(24);
				records.Remove(num + namenum);
				linkTable.RemoveName(namenum);
			}
		}

		/// If a {@link NameCommentRecord} is added or the name it references
		///  is renamed, then this will update the lookup cache for it.
		public void UpdateNameCommentRecordCache(NameCommentRecord commentRecord)
		{
			if (commentRecords.ContainsValue(commentRecord))
			{
				foreach (KeyValuePair<string, NameCommentRecord> commentRecord2 in commentRecords)
				{
					if (commentRecord2.Value.Equals(commentRecord))
					{
						commentRecords.Remove(commentRecord2.Key);
						break;
					}
				}
			}
			commentRecords[commentRecord.NameText] = commentRecord;
		}

		/// Returns a format index that matches the passed in format.  It does not tie into HSSFDataFormat.
		/// @param format the format string
		/// @param CreateIfNotFound Creates a new format if format not found
		/// @return the format id of a format that matches or -1 if none found and CreateIfNotFound
		public short GetFormat(string format, bool CreateIfNotFound)
		{
			IEnumerator enumerator = formats.GetEnumerator();
			while (enumerator.MoveNext())
			{
				FormatRecord formatRecord = (FormatRecord)enumerator.Current;
				if (formatRecord.FormatString.Equals(format))
				{
					return (short)formatRecord.IndexCode;
				}
			}
			if (CreateIfNotFound)
			{
				return (short)CreateFormat(format);
			}
			return -1;
		}

		/// Creates a FormatRecord, Inserts it, and returns the index code.
		/// @param format the format string
		/// @return the index code of the format record.
		/// @see org.apache.poi.hssf.record.FormatRecord
		/// @see org.apache.poi.hssf.record.Record
		public int CreateFormat(string formatString)
		{
			maxformatid = ((maxformatid >= 164) ? ((short)(maxformatid + 1)) : 164);
			FormatRecord formatRecord = new FormatRecord(maxformatid, formatString);
			int i;
			for (i = 0; i < records.Count && records[i].Sid != 1054; i++)
			{
			}
			i += formats.Count;
			formats.Add(formatRecord);
			records.Add(i, formatRecord);
			return maxformatid;
		}

		/// Creates a FormatRecord object
		/// @param id    the number of the format record to create (meaning its position in
		///        a file as M$ Excel would create it.)
		private static FormatRecord CreateFormat(int id)
		{
			switch (id)
			{
			case 0:
				return new FormatRecord(5, BuiltinFormats.GetBuiltinFormat(5));
			case 1:
				return new FormatRecord(6, BuiltinFormats.GetBuiltinFormat(6));
			case 2:
				return new FormatRecord(7, BuiltinFormats.GetBuiltinFormat(7));
			case 3:
				return new FormatRecord(8, BuiltinFormats.GetBuiltinFormat(8));
			case 4:
				return new FormatRecord(42, BuiltinFormats.GetBuiltinFormat(42));
			case 5:
				return new FormatRecord(41, BuiltinFormats.GetBuiltinFormat(41));
			case 6:
				return new FormatRecord(44, BuiltinFormats.GetBuiltinFormat(44));
			case 7:
				return new FormatRecord(43, BuiltinFormats.GetBuiltinFormat(43));
			default:
				throw new ArgumentException("Unexpected id " + id);
			}
		}

		/// Returns the first occurance of a record matching a particular sid.
		public NPOI.HSSF.Record.Record FindFirstRecordBySid(short sid)
		{
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record.Sid == sid)
				{
					return record;
				}
			}
			return null;
		}

		/// Returns the index of a record matching a particular sid.
		/// @param sid   The sid of the record to match
		/// @return      The index of -1 if no match made.
		public int FindFirstRecordLocBySid(short sid)
		{
			int num = 0;
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record.Sid == sid)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// Returns the next occurance of a record matching a particular sid.
		public NPOI.HSSF.Record.Record FindNextRecordBySid(short sid, int pos)
		{
			int num = 0;
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record.Sid == sid && num++ == pos)
				{
					return record;
				}
			}
			return null;
		}

		/// Finds the primary drawing Group, if one already exists
		public DrawingManager2 FindDrawingGroup()
		{
			if (drawingManager != null)
			{
				return drawingManager;
			}
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record is DrawingGroupRecord)
				{
					DrawingGroupRecord drawingGroupRecord = (DrawingGroupRecord)record;
					drawingGroupRecord.ProcessChildRecords();
					EscherContainerRecord escherContainer = drawingGroupRecord.GetEscherContainer();
					if (escherContainer != null)
					{
						EscherDggRecord escherDggRecord = null;
						EscherContainerRecord escherContainerRecord = null;
						IEnumerator enumerator2 = escherContainer.ChildRecords.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							EscherRecord escherRecord = (EscherRecord)enumerator2.Current;
							if (escherRecord is EscherDggRecord)
							{
								escherDggRecord = (EscherDggRecord)escherRecord;
							}
							else if (escherRecord.RecordId == -4095)
							{
								escherContainerRecord = (EscherContainerRecord)escherRecord;
							}
						}
						if (escherDggRecord != null)
						{
							drawingManager = new DrawingManager2(escherDggRecord);
							if (escherContainerRecord != null)
							{
								foreach (EscherRecord childRecord in escherContainerRecord.ChildRecords)
								{
									if (childRecord is EscherBSERecord)
									{
										escherBSERecords.Add((EscherBSERecord)childRecord);
									}
								}
							}
							return drawingManager;
						}
					}
				}
			}
			int num = FindFirstRecordLocBySid(235);
			if (num != -1)
			{
				DrawingGroupRecord drawingGroupRecord2 = (DrawingGroupRecord)records[num];
				EscherDggRecord escherDggRecord2 = null;
				EscherContainerRecord escherContainerRecord2 = null;
				IEnumerator enumerator4 = drawingGroupRecord2.EscherRecords.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					EscherRecord escherRecord2 = (EscherRecord)enumerator4.Current;
					if (escherRecord2 is EscherDggRecord)
					{
						escherDggRecord2 = (EscherDggRecord)escherRecord2;
					}
					else if (escherRecord2.RecordId == -4095)
					{
						escherContainerRecord2 = (EscherContainerRecord)escherRecord2;
					}
				}
				if (escherDggRecord2 != null)
				{
					drawingManager = new DrawingManager2(escherDggRecord2);
					if (escherContainerRecord2 != null)
					{
						foreach (EscherRecord childRecord2 in escherContainerRecord2.ChildRecords)
						{
							if (childRecord2 is EscherBSERecord)
							{
								escherBSERecords.Add((EscherBSERecord)childRecord2);
							}
						}
					}
				}
			}
			return drawingManager;
		}

		/// Creates a primary drawing Group record.  If it already 
		///  exists then it's modified.
		public void CreateDrawingGroup()
		{
			if (drawingManager == null)
			{
				EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
				EscherDggRecord escherDggRecord = new EscherDggRecord();
				EscherOptRecord escherOptRecord = new EscherOptRecord();
				EscherSplitMenuColorsRecord escherSplitMenuColorsRecord = new EscherSplitMenuColorsRecord();
				escherContainerRecord.RecordId = -4096;
				escherContainerRecord.Options = 15;
				escherDggRecord.RecordId = -4090;
				escherDggRecord.Options = 0;
				escherDggRecord.ShapeIdMax = 1024;
				escherDggRecord.NumShapesSaved = 0;
				escherDggRecord.DrawingsSaved = 0;
				escherDggRecord.FileIdClusters = new EscherDggRecord.FileIdCluster[0];
				drawingManager = new DrawingManager2(escherDggRecord);
				EscherContainerRecord escherContainerRecord2 = null;
				if (escherBSERecords.Count > 0)
				{
					escherContainerRecord2 = new EscherContainerRecord();
					escherContainerRecord2.RecordId = -4095;
					escherContainerRecord2.Options = (short)((escherBSERecords.Count << 4) | 0xF);
					IEnumerator enumerator = escherBSERecords.GetEnumerator();
					while (enumerator.MoveNext())
					{
						EscherRecord record = (EscherRecord)enumerator.Current;
						escherContainerRecord2.AddChildRecord(record);
					}
				}
				escherOptRecord.RecordId = -4085;
				escherOptRecord.Options = 51;
				escherOptRecord.AddEscherProperty(new EscherBoolProperty(191, 524296));
				escherOptRecord.AddEscherProperty(new EscherRGBProperty(385, 134217793));
				escherOptRecord.AddEscherProperty(new EscherRGBProperty(448, 134217792));
				escherSplitMenuColorsRecord.RecordId = -3810;
				escherSplitMenuColorsRecord.Options = 64;
				escherSplitMenuColorsRecord.Color1 = 134217741;
				escherSplitMenuColorsRecord.Color2 = 134217740;
				escherSplitMenuColorsRecord.Color3 = 134217751;
				escherSplitMenuColorsRecord.Color4 = 268435703;
				escherContainerRecord.AddChildRecord(escherDggRecord);
				if (escherContainerRecord2 != null)
				{
					escherContainerRecord.AddChildRecord(escherContainerRecord2);
				}
				escherContainerRecord.AddChildRecord(escherOptRecord);
				escherContainerRecord.AddChildRecord(escherSplitMenuColorsRecord);
				int num = FindFirstRecordLocBySid(235);
				if (num == -1)
				{
					DrawingGroupRecord drawingGroupRecord = new DrawingGroupRecord();
					drawingGroupRecord.AddEscherRecord(escherContainerRecord);
					int num2 = FindFirstRecordLocBySid(140);
					Records.Insert(num2 + 1, drawingGroupRecord);
				}
				else
				{
					DrawingGroupRecord drawingGroupRecord2 = new DrawingGroupRecord();
					drawingGroupRecord2.AddEscherRecord(escherContainerRecord);
					Records[num] = drawingGroupRecord2;
				}
			}
		}

		/// Removes the given font record from the
		///  file's list. This will make all 
		///  subsequent font indicies drop by one,
		///  so you'll need to update those yourself!
		public void RemoveFontRecord(FontRecord rec)
		{
			records.Remove(rec);
			numfonts--;
		}

		/// Removes the given ExtendedFormatRecord record from the
		///  file's list. This will make all 
		///  subsequent font indicies drop by one,
		///  so you'll need to update those yourself!
		public void RemoveExFormatRecord(ExtendedFormatRecord rec)
		{
			records.Remove(rec);
			numxfs--;
		}

		/// <summary>
		/// Removes ExtendedFormatRecord record with given index from the file's list. This will make all
		/// subsequent font indicies drop by one,so you'll need to update those yourself!
		/// </summary>
		/// <param name="index">index of the Extended format record (0-based)</param>
		public void RemoveExFormatRecord(int index)
		{
			int pos = records.Xfpos - (numxfs - 1) + index;
			records.Remove(pos);
			numxfs--;
		}

		public EscherBSERecord GetBSERecord(int pictureIndex)
		{
			return (EscherBSERecord)escherBSERecords[pictureIndex - 1];
		}

		public int AddBSERecord(EscherBSERecord e)
		{
			CreateDrawingGroup();
			escherBSERecords.Add(e);
			int index = FindFirstRecordLocBySid(235);
			DrawingGroupRecord drawingGroupRecord = (DrawingGroupRecord)Records[index];
			EscherContainerRecord escherContainerRecord = (EscherContainerRecord)drawingGroupRecord.GetEscherRecord(0);
			EscherContainerRecord escherContainerRecord2;
			if (escherContainerRecord.GetChild(1).RecordId == -4095)
			{
				escherContainerRecord2 = (EscherContainerRecord)escherContainerRecord.GetChild(1);
			}
			else
			{
				escherContainerRecord2 = new EscherContainerRecord();
				escherContainerRecord2.RecordId = -4095;
				List<EscherRecord> childRecords = escherContainerRecord.ChildRecords;
				childRecords.Insert(1, escherContainerRecord2);
				escherContainerRecord.ChildRecords = childRecords;
			}
			escherContainerRecord2.Options = (short)((escherBSERecords.Count << 4) | 0xF);
			escherContainerRecord2.AddChildRecord(e);
			return escherBSERecords.Count;
		}

		/// protect a workbook with a password (not encypted, just Sets Writeprotect
		/// flags and the password.
		/// @param password to Set
		public void WriteProtectWorkbook(string password, string username)
		{
			FileSharingRecord fileSharing = FileSharing;
			WriteAccessRecord writeAccessRecord = WriteAccess;
			WriteProtectRecord writeProtect2 = WriteProtect;
			fileSharing.ReadOnly = 1;
			fileSharing.Password = FileSharingRecord.HashPassword(password);
			fileSharing.Username = username;
			writeAccessRecord.Username = username;
		}

		/// Removes the Write protect flag
		public void UnwriteProtectWorkbook()
		{
			records.Remove(fileShare);
			records.Remove(WriteProtect);
			fileShare = null;
			writeProtect = null;
		}

		/// @param reFindex Index to REF entry in EXTERNSHEET record in the Link Table
		/// @param definedNameIndex zero-based to DEFINEDNAME or EXTERNALNAME record
		/// @return the string representation of the defined or external name
		public string ResolveNameXText(int reFindex, int definedNameIndex)
		{
			return linkTable.ResolveNameXText(reFindex, definedNameIndex);
		}

		public NameRecord CloneFilter(int filterDbNameIndex, int newSheetIndex)
		{
			NameRecord nameRecord = GetNameRecord(filterDbNameIndex);
			int externSheetIndex = CheckExternSheet(newSheetIndex);
			Ptg[] nameDefinition = nameRecord.NameDefinition;
			for (int i = 0; i < nameDefinition.Length; i++)
			{
				Ptg ptg = nameDefinition[i];
				if (ptg is Area3DPtg)
				{
					Area3DPtg area3DPtg = (Area3DPtg)((OperandPtg)ptg).Copy();
					area3DPtg.ExternSheetIndex = externSheetIndex;
					nameDefinition[i] = area3DPtg;
				}
				else if (ptg is Ref3DPtg)
				{
					Ref3DPtg ref3DPtg = (Ref3DPtg)((OperandPtg)ptg).Copy();
					ref3DPtg.ExternSheetIndex = externSheetIndex;
					nameDefinition[i] = ref3DPtg;
				}
			}
			NameRecord nameRecord2 = CreateBuiltInName(13, newSheetIndex + 1);
			nameRecord2.NameDefinition = nameDefinition;
			nameRecord2.IsHiddenName = true;
			return nameRecord2;
		}

		/// Updates named ranges due to moving of cells
		public void UpdateNamesAfterCellShift(FormulaShifter shifter)
		{
			for (int i = 0; i < NumNames; i++)
			{
				NameRecord nameRecord = GetNameRecord(i);
				Ptg[] nameDefinition = nameRecord.NameDefinition;
				if (shifter.AdjustFormula(nameDefinition, nameRecord.SheetNumber))
				{
					nameRecord.NameDefinition = nameDefinition;
				}
			}
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
			return linkTable.ChangeExternalReference(oldUrl, newUrl);
		}
	}
}
