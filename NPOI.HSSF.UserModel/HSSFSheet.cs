using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.HSSF.Record.AutoFilter;
using NPOI.HSSF.Util;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation of a worksheet.
	/// </summary>
	/// <remarks>
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author  Libin Roman (romal at vistaportal.com)
	/// @author  Shawn Laubach (slaubach at apache dot org) (Just a little)
	/// @author  Jean-Pierre Paris (jean-pierre.paris at m4x dot org) (Just a little, too)
	/// @author  Yegor Kozlov (yegor at apache.org) (Autosizing columns)
	/// </remarks>
	[Serializable]
	public class HSSFSheet : ISheet
	{
		/// Used for compile-time optimization.  This is the initial size for the collection of
		/// rows.  It is currently Set to 20.  If you generate larger sheets you may benefit
		/// by Setting this to a higher number and recompiling a custom edition of HSSFSheet.
		public const int INITIAL_CAPACITY = 20;

		/// reference to the low level Sheet object
		private InternalSheet _sheet;

		private Dictionary<int, IRow> rows;

		public InternalWorkbook book;

		protected HSSFWorkbook _workbook;

		private int firstrow;

		private int lastrow;

		[NonSerialized]
		private HSSFPatriarch _patriarch;

		/// Gets the flag indicating whether the window should show 0 (zero) in cells containing zero value.
		/// When false, cells with zero value appear blank instead of showing the number zero.
		/// In Excel 2003 this option can be changed in the Options dialog on the View tab.
		/// @return whether all zero values on the worksheet are displayed
		public bool DisplayZeros
		{
			get
			{
				return _sheet.WindowTwo.DisplayZeros;
			}
			set
			{
				_sheet.WindowTwo.DisplayZeros = value;
			}
		}

		/// <summary>
		/// Returns the number of phsyically defined rows (NOT the number of rows in the _sheet)
		/// </summary>
		/// <value>The physical number of rows.</value>
		public int PhysicalNumberOfRows => rows.Count;

		/// <summary>
		/// Gets the first row on the _sheet
		/// </summary>
		/// <value>the number of the first logical row on the _sheet</value>
		public int FirstRowNum => firstrow;

		/// <summary>
		/// Gets the last row on the _sheet
		/// </summary>
		/// <value>last row contained n this _sheet.</value>
		public int LastRowNum => lastrow;

		/// <summary>
		/// Gets or sets the default width of the column.
		/// </summary>
		/// <value>The default width of the column.</value>
		public int DefaultColumnWidth
		{
			get
			{
				return _sheet.DefaultColumnWidth;
			}
			set
			{
				_sheet.DefaultColumnWidth = value;
			}
		}

		/// <summary>
		/// Get the default row height for the _sheet (if the rows do not define their own height) in
		/// twips (1/20 of  a point)
		/// </summary>
		/// <value>The default height of the row.</value>
		public short DefaultRowHeight
		{
			get
			{
				return _sheet.DefaultRowHeight;
			}
			set
			{
				_sheet.DefaultRowHeight = value;
			}
		}

		/// <summary>
		/// Get the default row height for the _sheet (if the rows do not define their own height) in
		/// points.
		/// </summary>
		/// <value>The default row height in points.</value>
		public float DefaultRowHeightInPoints
		{
			get
			{
				return (float)((double)_sheet.DefaultRowHeight / 20.0);
			}
			set
			{
				_sheet.DefaultRowHeight = (short)((double)value * 20.0);
			}
		}

		/// <summary>
		/// Get whether gridlines are printed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if printed; otherwise, <c>false</c>.
		/// </value>
		[Obsolete("Please use IsPrintGridlines instead")]
		public bool IsGridsPrinted
		{
			get
			{
				return _sheet.IsGridsPrinted;
			}
			set
			{
				_sheet.IsGridsPrinted = value;
			}
		}

		/// <summary>
		/// Whether a record must be Inserted or not at generation to indicate that
		/// formula must be recalculated when _workbook is opened.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [force formula recalculation]; otherwise, <c>false</c>.
		/// </value>
		/// @return true if an Uncalced record must be Inserted or not at generation
		public bool ForceFormulaRecalculation
		{
			get
			{
				return _sheet.IsUncalced;
			}
			set
			{
				_sheet.IsUncalced = value;
			}
		}

		/// <summary>
		/// Determine whether printed output for this _sheet will be vertically centered.
		/// </summary>
		/// <value><c>true</c> if [vertically center]; otherwise, <c>false</c>.</value>
		public bool VerticallyCenter
		{
			get
			{
				return _sheet.PageSettings.VCenter.VCenter;
			}
			set
			{
				_sheet.PageSettings.VCenter.VCenter = value;
			}
		}

		/// <summary>
		/// Determine whether printed output for this _sheet will be horizontally centered.
		/// </summary>
		/// <value><c>true</c> if [horizontally center]; otherwise, <c>false</c>.</value>
		public bool HorizontallyCenter
		{
			get
			{
				return _sheet.PageSettings.HCenter.HCenter;
			}
			set
			{
				_sheet.PageSettings.HCenter.HCenter = value;
			}
		}

		/// <summary>
		/// returns the number of merged regions
		/// </summary>
		/// <value>The number of merged regions</value>
		public int NumMergedRegions => _sheet.NumMergedRegions;

		/// <summary>
		/// used internally in the API to Get the low level Sheet record represented by this
		/// Object.
		/// </summary>
		/// <value>low level representation of this HSSFSheet.</value>
		public InternalSheet Sheet => _sheet;

		/// <summary>
		/// Gets or sets whether alternate expression evaluation is on
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [alternative expression]; otherwise, <c>false</c>.
		/// </value>
		public bool AlternativeExpression
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).AlternateExpression;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.AlternateExpression = value;
			}
		}

		/// <summary>
		/// whether alternative formula entry is on
		/// </summary>
		/// <value><c>true</c> alternative formulas or not; otherwise, <c>false</c>.</value>
		public bool AlternativeFormula
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).AlternateFormula;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.AlternateFormula = value;
			}
		}

		/// <summary>
		/// show automatic page breaks or not
		/// </summary>
		/// <value>whether to show auto page breaks</value>
		public bool Autobreaks
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).Autobreaks;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.Autobreaks = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether _sheet is a dialog _sheet
		/// </summary>
		/// <value><c>true</c> if is dialog; otherwise, <c>false</c>.</value>
		public bool Dialog
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).Dialog;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.Dialog = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to Display the guts or not.
		/// </summary>
		/// <value><c>true</c> if guts or no guts (or glory); otherwise, <c>false</c>.</value>
		public bool DisplayGuts
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).DisplayGuts;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.DisplayGuts = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether fit to page option is on
		/// </summary>
		/// <value><c>true</c> if [fit to page]; otherwise, <c>false</c>.</value>
		public bool FitToPage
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).FitToPage;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.FitToPage = value;
			}
		}

		/// <summary>
		/// Get if row summaries appear below detail in the outline
		/// </summary>
		/// <value><c>true</c> if below or not; otherwise, <c>false</c>.</value>
		public bool RowSumsBelow
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).RowSumsBelow;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.RowSumsBelow = value;
			}
		}

		/// <summary>
		/// Get if col summaries appear right of the detail in the outline
		/// </summary>
		/// <value><c>true</c> right or not; otherwise, <c>false</c>.</value>
		public bool RowSumsRight
		{
			get
			{
				return ((WSBoolRecord)_sheet.FindFirstRecordBySid(129)).RowSumsRight;
			}
			set
			{
				WSBoolRecord wSBoolRecord = (WSBoolRecord)_sheet.FindFirstRecordBySid(129);
				wSBoolRecord.RowSumsRight = value;
			}
		}

		/// <summary>
		/// Gets or sets whether gridlines are printed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> Gridlines are printed; otherwise, <c>false</c>.
		/// </value>
		public bool IsPrintGridlines
		{
			get
			{
				return Sheet.PrintGridlines.PrintGridlines;
			}
			set
			{
				Sheet.PrintGridlines.PrintGridlines = value;
			}
		}

		/// <summary>
		/// Gets the print setup object.
		/// </summary>
		/// <value>The user model for the print setup object.</value>
		public IPrintSetup PrintSetup => new HSSFPrintSetup(_sheet.PageSettings.PrintSetup);

		/// <summary>
		/// Gets the user model for the document header.
		/// </summary>
		/// <value>The Document header.</value>
		public IHeader Header => new HSSFHeader(_sheet.PageSettings);

		/// <summary>
		/// Gets the user model for the document footer.
		/// </summary>
		/// <value>The Document footer.</value>
		public IFooter Footer => new HSSFFooter(_sheet.PageSettings);

		/// <summary>
		/// Gets or sets whether the worksheet is displayed from right to left instead of from left to right.
		/// </summary>
		/// <value>true for right to left, false otherwise</value>
		/// <remarks>poi bug 47970</remarks>
		public bool IsRightToLeft
		{
			get
			{
				return _sheet.WindowTwo.Arabic;
			}
			set
			{
				_sheet.WindowTwo.Arabic = value;
			}
		}

		/// <summary>
		/// Note - this is not the same as whether the _sheet is focused (isActive)
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this _sheet is currently selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected
		{
			get
			{
				return Sheet.GetWindowTwo().IsSelected;
			}
			set
			{
				Sheet.GetWindowTwo().IsSelected = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if this _sheet is currently focused.
		/// </summary>
		/// <value><c>true</c> if this _sheet is currently focused; otherwise, <c>false</c>.</value>
		public bool IsActive
		{
			get
			{
				return Sheet.GetWindowTwo().IsActive;
			}
			set
			{
				Sheet.GetWindowTwo().IsActive = value;
			}
		}

		private WorksheetProtectionBlock ProtectionBlock => _sheet.ProtectionBlock;

		/// <summary>
		/// Answer whether protection is enabled or disabled
		/// </summary>
		/// <value><c>true</c> if protection enabled; otherwise, <c>false</c>.</value>
		public bool Protect => ProtectionBlock.IsSheetProtected;

		/// <summary>
		/// Gets the hashed password
		/// </summary>
		/// <value>The password.</value>
		public int Password => ProtectionBlock.PasswordHash;

		/// <summary>
		/// Answer whether object protection is enabled or disabled
		/// </summary>
		/// <value><c>true</c> if protection enabled; otherwise, <c>false</c>.</value>
		public bool ObjectProtect => ProtectionBlock.IsObjectProtected;

		/// <summary>
		/// Answer whether scenario protection is enabled or disabled
		/// </summary>
		/// <value><c>true</c> if protection enabled; otherwise, <c>false</c>.</value>
		public bool ScenarioProtect => ProtectionBlock.IsScenarioProtected;

		/// <summary>
		/// The top row in the visible view when the _sheet is
		/// first viewed after opening it in a viewer
		/// </summary>
		/// <value>the rownum (0 based) of the top row</value>
		public short TopRow
		{
			get
			{
				return _sheet.TopRow;
			}
			set
			{
				_sheet.TopRow = value;
			}
		}

		/// <summary>
		/// The left col in the visible view when the _sheet Is
		/// first viewed after opening it in a viewer
		/// </summary>
		/// <value>the rownum (0 based) of the top row</value>
		public short LeftCol
		{
			get
			{
				return _sheet.LeftCol;
			}
			set
			{
				_sheet.LeftCol = value;
			}
		}

		/// <summary>
		/// Returns the information regarding the currently configured pane (split or freeze).
		/// </summary>
		/// <value>null if no pane configured, or the pane information.</value>
		public PaneInformation PaneInformation => Sheet.PaneInformation;

		/// <summary>
		/// Gets or sets if gridlines are Displayed.
		/// </summary>
		/// <value>whether gridlines are Displayed</value>
		public bool DisplayGridlines
		{
			get
			{
				return _sheet.DisplayGridlines;
			}
			set
			{
				_sheet.DisplayGridlines = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether formulas are displayed.
		/// </summary>
		/// <value>whether formulas are Displayed</value>
		public bool DisplayFormulas
		{
			get
			{
				return _sheet.DisplayFormulas;
			}
			set
			{
				_sheet.DisplayFormulas = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether RowColHeadings are displayed.
		/// </summary>
		/// <value>
		/// 	whether RowColHeadings are displayed
		/// </value>
		public bool DisplayRowColHeadings
		{
			get
			{
				return _sheet.DisplayRowColHeadings;
			}
			set
			{
				_sheet.DisplayRowColHeadings = value;
			}
		}

		/// <summary>
		/// Retrieves all the horizontal page breaks
		/// </summary>
		/// <value>all the horizontal page breaks, or null if there are no row page breaks</value>
		public int[] RowBreaks => _sheet.PageSettings.RowBreaks;

		/// <summary>
		/// Retrieves all the vertical page breaks
		/// </summary>
		/// <value>all the vertical page breaks, or null if there are no column page breaks</value>
		public int[] ColumnBreaks => _sheet.PageSettings.ColumnBreaks;

		/// <summary>
		/// Returns the agregate escher records for this _sheet,
		/// it there is one.
		/// WARNING - calling this will trigger a parsing of the
		/// associated escher records. Any that aren't supported
		/// (such as charts and complex drawing types) will almost
		/// certainly be lost or corrupted when written out.
		/// </summary>
		/// <value>The drawing escher aggregate.</value>
		public EscherAggregate DrawingEscherAggregate
		{
			get
			{
				book.FindDrawingGroup();
				if (book.DrawingManager == null)
				{
					return null;
				}
				int num = _sheet.AggregateDrawingRecords(book.DrawingManager, CreateIfMissing: false);
				if (num == -1)
				{
					return null;
				}
				return (EscherAggregate)_sheet.FindFirstRecordBySid(9876);
			}
		}

		/// This will hold any graphics or charts for the sheet.
		///
		/// @return the top-level drawing patriarch, if there is one, else returns null
		public IDrawing DrawingPatriarch
		{
			get
			{
				_patriarch = GetPatriarch(createIfMissing: false);
				return _patriarch;
			}
		}

		/// <summary>
		/// Gets or sets the tab color of the _sheet
		/// </summary>
		public short TabColorIndex
		{
			get
			{
				return _sheet.TabColorIndex;
			}
			set
			{
				_sheet.TabColorIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the tab color of _sheet is automatic
		/// </summary>
		public bool IsAutoTabColor
		{
			get
			{
				return _sheet.IsAutoTabColor;
			}
			set
			{
				_sheet.IsAutoTabColor = value;
			}
		}

		/// <summary>
		/// Gets the sheet conditional formatting.
		/// </summary>
		/// <value>The sheet conditional formatting.</value>
		public ISheetConditionalFormatting SheetConditionalFormatting => new HSSFSheetConditionalFormatting(this);

		/// <summary>
		/// Get the DVRecords objects that are associated to this _sheet
		/// </summary>
		/// <value>a list of DVRecord instances</value>
		public IList DVRecords
		{
			get
			{
				IList list = new ArrayList();
				IList records = _sheet.Records;
				for (int i = 0; i < records.Count; i++)
				{
					if (records[i] is DVRecord)
					{
						list.Add(records[i]);
					}
				}
				return list;
			}
		}

		/// <summary>
		/// Provide a reference to the parent workbook.
		/// </summary>
		public IWorkbook Workbook => _workbook;

		/// <summary>
		/// Returns the name of this _sheet
		/// </summary>
		public string SheetName
		{
			get
			{
				IWorkbook workbook = Workbook;
				int sheetIndex = workbook.GetSheetIndex(this);
				return workbook.GetSheetName(sheetIndex);
			}
		}

		public CellRangeAddress RepeatingRows
		{
			get
			{
				return GetRepeatingRowsOrColums(rows: true);
			}
			set
			{
				CellRangeAddress repeatingColumns = RepeatingColumns;
				SetRepeatingRowsAndColumns(value, repeatingColumns);
			}
		}

		public CellRangeAddress RepeatingColumns
		{
			get
			{
				return GetRepeatingRowsOrColums(rows: false);
			}
			set
			{
				CellRangeAddress repeatingRows = RepeatingRows;
				SetRepeatingRowsAndColumns(repeatingRows, value);
			}
		}

		/// <summary>
		/// Creates new HSSFSheet - called by HSSFWorkbook to create a _sheet from
		/// scratch. You should not be calling this from application code (its protected anyhow).
		/// </summary>
		/// <param name="workbook">The HSSF Workbook object associated with the _sheet.</param>
		/// <see cref="M:NPOI.HSSF.UserModel.HSSFWorkbook.CreateSheet" />
		public HSSFSheet(HSSFWorkbook workbook)
		{
			_sheet = InternalSheet.CreateSheet();
			rows = new Dictionary<int, IRow>();
			_workbook = workbook;
			book = workbook.Workbook;
		}

		/// <summary>
		/// Creates an HSSFSheet representing the given Sheet object.  Should only be
		/// called by HSSFWorkbook when reading in an exisiting file.
		/// </summary>
		/// <param name="workbook">The HSSF Workbook object associated with the _sheet.</param>
		/// <param name="sheet">lowlevel Sheet object this _sheet will represent</param>
		/// <see cref="M:NPOI.HSSF.UserModel.HSSFWorkbook.#ctor(NPOI.POIFS.FileSystem.DirectoryNode,System.Boolean)" />
		public HSSFSheet(HSSFWorkbook workbook, InternalSheet sheet)
		{
			_sheet = sheet;
			rows = new Dictionary<int, IRow>();
			_workbook = workbook;
			book = _workbook.Workbook;
			SetPropertiesFromSheet(_sheet);
		}

		/// <summary>
		/// Clones the _sheet.
		/// </summary>
		/// <param name="workbook">The _workbook.</param>
		/// <returns>the cloned sheet</returns>
		public ISheet CloneSheet(HSSFWorkbook workbook)
		{
			IDrawing drawingPatriarch = DrawingPatriarch;
			HSSFSheet hSSFSheet = new HSSFSheet(workbook, _sheet.CloneSheet());
			int index = hSSFSheet._sheet.FindFirstRecordLocBySid(236);
			DrawingRecord drawingRecord = (DrawingRecord)hSSFSheet._sheet.FindFirstRecordBySid(236);
			if (drawingRecord != null)
			{
				hSSFSheet._sheet.Records.Remove(drawingRecord);
			}
			if (DrawingPatriarch != null)
			{
				HSSFPatriarch hSSFPatriarch = HSSFPatriarch.CreatePatriarch(DrawingPatriarch as HSSFPatriarch, hSSFSheet);
				hSSFSheet._sheet.Records.Insert(index, hSSFPatriarch.GetBoundAggregate());
				hSSFSheet._patriarch = hSSFPatriarch;
			}
			return hSSFSheet;
		}

		internal void PreSerialize()
		{
			if (_patriarch != null)
			{
				_patriarch.PreSerialize();
			}
		}

		/// <summary>
		/// Copy one row to the target row
		/// </summary>
		/// <param name="sourceIndex">index of the source row</param>
		/// <param name="targetIndex">index of the target row</param>
		public IRow CopyRow(int sourceIndex, int targetIndex)
		{
			return SheetUtil.CopyRow(this, sourceIndex, targetIndex);
		}

		/// <summary>
		/// used internally to Set the properties given a Sheet object
		/// </summary>
		/// <param name="sheet">The _sheet.</param>
		private void SetPropertiesFromSheet(InternalSheet sheet)
		{
			RowRecord nextRow = sheet.NextRow;
			bool flag = nextRow != null;
			while (nextRow != null)
			{
				CreateRowFromRecord(nextRow);
				nextRow = sheet.NextRow;
			}
			CellValueRecordInterface[] valueRecords = sheet.GetValueRecords();
			int millisecond = DateTime.Now.Millisecond;
			HSSFRow hSSFRow = null;
			int num = 0;
			while (true)
			{
				if (num >= valueRecords.Length)
				{
					return;
				}
				CellValueRecordInterface cellValueRecordInterface = valueRecords[num];
				int millisecond2 = DateTime.Now.Millisecond;
				HSSFRow hSSFRow2 = hSSFRow;
				if (hSSFRow == null || hSSFRow.RowNum != cellValueRecordInterface.Row)
				{
					hSSFRow2 = (HSSFRow)GetRow(cellValueRecordInterface.Row);
					if (hSSFRow2 == null)
					{
						if (flag)
						{
							break;
						}
						RowRecord row = new RowRecord(cellValueRecordInterface.Row);
						_sheet.AddRow(row);
						hSSFRow2 = CreateRowFromRecord(row);
					}
				}
				if (hSSFRow2 != null)
				{
					hSSFRow = hSSFRow2;
					hSSFRow2.CreateCellFromRecord(cellValueRecordInterface);
				}
				else
				{
					cellValueRecordInterface = null;
				}
				num++;
			}
			throw new Exception("Unexpected missing row when some rows already present, the file is wrong");
		}

		/// <summary>
		/// Create a new row within the _sheet and return the high level representation
		/// </summary>
		/// <param name="rownum">The row number.</param>
		/// <returns></returns>
		/// @see org.apache.poi.hssf.usermodel.HSSFRow
		/// @see #RemoveRow(HSSFRow)
		public IRow CreateRow(int rownum)
		{
			HSSFRow hSSFRow = new HSSFRow(_workbook, this, rownum);
			hSSFRow.Height = DefaultRowHeight;
			hSSFRow.RowRecord.BadFontHeight = false;
			AddRow(hSSFRow, addLow: true);
			return hSSFRow;
		}

		/// <summary>
		/// Used internally to Create a high level Row object from a low level row object.
		/// USed when Reading an existing file
		/// </summary>
		/// <param name="row">low level record to represent as a high level Row and Add to _sheet.</param>
		/// <returns>HSSFRow high level representation</returns>
		private HSSFRow CreateRowFromRecord(RowRecord row)
		{
			HSSFRow hSSFRow = new HSSFRow(_workbook, this, row);
			AddRow(hSSFRow, addLow: false);
			return hSSFRow;
		}

		/// <summary>
		/// Remove a row from this _sheet.  All cells contained in the row are Removed as well
		/// </summary>
		/// <param name="row">the row to Remove.</param>
		public void RemoveRow(IRow row)
		{
			if (row.Sheet != this)
			{
				throw new ArgumentException("Specified row does not belong to this sheet");
			}
			foreach (ICell item in row)
			{
				HSSFCell hSSFCell = (HSSFCell)item;
				if (hSSFCell.IsPartOfArrayFormulaGroup)
				{
					string msg = "Row[rownum=" + row.RowNum + "] contains cell(s) included in a multi-cell array formula. You cannot change part of an array.";
					hSSFCell.NotifyArrayFormulaChanging(msg);
				}
			}
			if (rows.Count > 0)
			{
				int rowNum = row.RowNum;
				HSSFRow hSSFRow = (HSSFRow)rows[rowNum];
				rows.Remove(rowNum);
				if (hSSFRow != row)
				{
					if (hSSFRow != null)
					{
						rows[rowNum] = hSSFRow;
					}
					throw new InvalidOperationException("Specified row does not belong to this _sheet");
				}
				if (row.RowNum == LastRowNum)
				{
					lastrow = FindLastRow(lastrow);
				}
				if (row.RowNum == FirstRowNum)
				{
					firstrow = FindFirstRow(firstrow);
				}
				CellValueRecordInterface[] valueRecords = _sheet.GetValueRecords();
				for (int i = 0; i < valueRecords.Length; i++)
				{
					if (valueRecords[i].Row == rowNum)
					{
						_sheet.RemoveValueRecord(rowNum, valueRecords[i]);
					}
				}
				_sheet.RemoveRow(((HSSFRow)row).RowRecord);
			}
		}

		/// <summary>
		/// used internally to refresh the "last row" when the last row is Removed.
		/// </summary>
		/// <param name="lastrow">The last row.</param>
		/// <returns></returns>
		private int FindLastRow(int lastrow)
		{
			if (lastrow < 1)
			{
				return 0;
			}
			int num = lastrow - 1;
			IRow row = GetRow(num);
			while (row == null && num > 0)
			{
				row = GetRow(--num);
			}
			if (row == null)
			{
				return 0;
			}
			return num;
		}

		/// <summary>
		/// used internally to refresh the "first row" when the first row is Removed.
		/// </summary>
		/// <param name="firstrow">The first row.</param>
		/// <returns></returns>
		private int FindFirstRow(int firstrow)
		{
			int num = firstrow + 1;
			IRow row = GetRow(num);
			while (row == null && num <= LastRowNum)
			{
				row = GetRow(++num);
			}
			if (num > LastRowNum)
			{
				return 0;
			}
			return num;
		}

		/// Add a row to the _sheet
		///
		/// @param AddLow whether to Add the row to the low level model - false if its already there
		private void AddRow(HSSFRow row, bool addLow)
		{
			rows[row.RowNum] = row;
			if (addLow)
			{
				_sheet.AddRow(row.RowRecord);
			}
			bool flag = rows.Count == 1;
			if (row.RowNum > LastRowNum || flag)
			{
				lastrow = row.RowNum;
			}
			if (row.RowNum < FirstRowNum || flag)
			{
				firstrow = row.RowNum;
			}
		}

		/// <summary>
		/// Returns the HSSFCellStyle that applies to the given
		/// (0 based) column, or null if no style has been
		/// set for that column
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public ICellStyle GetColumnStyle(int column)
		{
			short xFIndexForColAt = _sheet.GetXFIndexForColAt((short)column);
			if (xFIndexForColAt == 15)
			{
				return null;
			}
			ExtendedFormatRecord exFormatAt = book.GetExFormatAt(xFIndexForColAt);
			return new HSSFCellStyle(xFIndexForColAt, exFormatAt, book);
		}

		/// <summary>
		/// Returns the logical row (not physical) 0-based.  If you ask for a row that is not
		/// defined you get a null.  This is to say row 4 represents the fifth row on a _sheet.
		/// </summary>
		/// <param name="rowIndex">Index of the row to get.</param>
		/// <returns>the row number or null if its not defined on the _sheet</returns>
		public IRow GetRow(int rowIndex)
		{
			if (!rows.ContainsKey(rowIndex))
			{
				return null;
			}
			return (HSSFRow)rows[rowIndex];
		}

		/// <summary>
		/// Creates a data validation object
		/// </summary>
		/// <param name="dataValidation">The data validation object settings</param>
		public void AddValidationData(IDataValidation dataValidation)
		{
			if (dataValidation == null)
			{
				throw new ArgumentException("objValidation must not be null");
			}
			HSSFDataValidation hSSFDataValidation = (HSSFDataValidation)dataValidation;
			DataValidityTable orCreateDataValidityTable = _sheet.GetOrCreateDataValidityTable();
			DVRecord dvRecord = hSSFDataValidation.CreateDVRecord(this);
			orCreateDataValidityTable.AddDataValidation(dvRecord);
		}

		/// <summary>
		/// Get the visibility state for a given column.F:\Gloria\�о�\�ļ���ʽ\NPOI\src\NPOI\HSSF\Util\HSSFDataValidation.cs
		/// </summary>
		/// <param name="column">the column to Get (0-based).</param>
		/// <param name="hidden">the visiblity state of the column.</param>
		public void SetColumnHidden(int column, bool hidden)
		{
			_sheet.SetColumnHidden(column, hidden);
		}

		/// <summary>
		/// Get the hidden state for a given column.
		/// </summary>
		/// <param name="column">the column to Set (0-based)</param>
		/// <returns>the visiblity state of the column;
		/// </returns>
		public bool IsColumnHidden(int column)
		{
			return _sheet.IsColumnHidden(column);
		}

		/// <summary>
		/// Set the width (in Units of 1/256th of a Char width)
		/// </summary>
		/// <param name="column">the column to Set (0-based)</param>
		/// <param name="width">the width in Units of 1/256th of a Char width</param>
		public void SetColumnWidth(int column, int width)
		{
			_sheet.SetColumnWidth(column, width);
		}

		/// <summary>
		/// Get the width (in Units of 1/256th of a Char width )
		/// </summary>
		/// <param name="column">the column to Set (0-based)</param>
		/// <returns>the width in Units of 1/256th of a Char width</returns>
		public int GetColumnWidth(int column)
		{
			return _sheet.GetColumnWidth(column);
		}

		/// <summary>
		/// Adds a merged region of cells (hence those cells form one)
		/// </summary>
		/// <param name="region">The region (rowfrom/colfrom-rowto/colto) to merge.</param>
		/// <returns>index of this region</returns>
		[Obsolete]
		public int AddMergedRegion(NPOI.SS.Util.Region region)
		{
			return _sheet.AddMergedRegion(region.RowFrom, region.ColumnFrom, region.RowTo, region.ColumnTo);
		}

		/// <summary>
		/// adds a merged region of cells (hence those cells form one)
		/// </summary>
		/// <param name="region">region (rowfrom/colfrom-rowto/colto) to merge</param>
		/// <returns>index of this region</returns>
		public int AddMergedRegion(CellRangeAddress region)
		{
			region.Validate(SpreadsheetVersion.EXCEL97);
			ValidateArrayFormulas(region);
			return _sheet.AddMergedRegion(region.FirstRow, region.FirstColumn, region.LastRow, region.LastColumn);
		}

		private void ValidateArrayFormulas(CellRangeAddress region)
		{
			int firstRow = region.FirstRow;
			int firstColumn = region.FirstColumn;
			int lastRow = region.LastRow;
			int lastColumn = region.LastColumn;
			for (int i = firstRow; i <= lastRow; i++)
			{
				for (int j = firstColumn; j <= lastColumn; j++)
				{
					HSSFRow hSSFRow = (HSSFRow)GetRow(i);
					if (hSSFRow != null)
					{
						HSSFCell hSSFCell = (HSSFCell)hSSFRow.GetCell(j);
						if (hSSFCell != null && hSSFCell.IsPartOfArrayFormulaGroup)
						{
							CellRangeAddress arrayFormulaRange = hSSFCell.ArrayFormulaRange;
							if (arrayFormulaRange.NumberOfCells > 1 && (arrayFormulaRange.IsInRange(region.FirstRow, region.FirstColumn) || arrayFormulaRange.IsInRange(region.FirstRow, region.FirstColumn)))
							{
								string message = "The range " + region.FormatAsString() + " intersects with a multi-cell array formula. You cannot merge cells of an array.";
								throw new InvalidOperationException(message);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Removes a merged region of cells (hence letting them free)
		/// </summary>
		/// <param name="index">index of the region to Unmerge</param>
		public void RemoveMergedRegion(int index)
		{
			_sheet.RemoveMergedRegion(index);
		}

		/// <summary>
		/// Gets the row enumerator.
		/// </summary>
		/// <returns>
		/// an iterator of the PHYSICAL rows.  Meaning the 3rd element may not
		/// be the third row if say for instance the second row is undefined.
		/// Call <see cref="P:NPOI.SS.UserModel.IRow.RowNum" /> on each row 
		/// if you care which one it is.
		/// </returns>
		public IEnumerator GetRowEnumerator()
		{
			return rows.Values.GetEnumerator();
		}

		/// <summary>
		/// Alias for GetRowEnumerator() to allow <c>foreach</c> loops.
		/// </summary>
		/// <returns>
		/// an iterator of the PHYSICAL rows.  Meaning the 3rd element may not
		/// be the third row if say for instance the second row is undefined.
		/// Call <see cref="P:NPOI.SS.UserModel.IRow.RowNum" /> on each row 
		/// if you care which one it is.
		/// </returns>
		public IEnumerator GetEnumerator()
		{
			return GetRowEnumerator();
		}

		/// <summary>
		/// Sets the active cell.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		public void SetActiveCell(int row, int column)
		{
			_sheet.SetActiveCellRange(row, row, column, column);
		}

		/// <summary>
		/// Sets the active cell range.
		/// </summary>
		/// <param name="firstRow">The first row.</param>
		/// <param name="lastRow">The last row.</param>
		/// <param name="firstColumn">The first column.</param>
		/// <param name="lastColumn">The last column.</param>
		public void SetActiveCellRange(int firstRow, int lastRow, int firstColumn, int lastColumn)
		{
			_sheet.SetActiveCellRange(firstRow, lastRow, firstColumn, lastColumn);
		}

		/// <summary>
		/// Sets the active cell range.
		/// </summary>
		/// <param name="cellranges">The cellranges.</param>
		/// <param name="activeRange">The index of the active range.</param>
		/// <param name="activeRow">The active row in the active range</param>
		/// <param name="activeColumn">The active column in the active range</param>
		public void SetActiveCellRange(List<CellRangeAddress8Bit> cellranges, int activeRange, int activeRow, int activeColumn)
		{
			_sheet.SetActiveCellRange(cellranges, activeRange, activeRow, activeColumn);
		}

		/// <summary>
		/// Sets whether sheet is selected.
		/// </summary>
		/// <param name="sel">Whether to select the sheet or deselect the sheet.</param> 
		public void SetActive(bool sel)
		{
			Sheet.WindowTwo.IsActive = sel;
		}

		/// <summary>
		/// Sets the protection enabled as well as the password
		/// </summary>
		/// <param name="password">password to set for protection, pass <code>null</code> to remove protection</param>
		public void ProtectSheet(string password)
		{
			ProtectionBlock.ProtectSheet(password, shouldProtectObjects: true, shouldProtectScenarios: true);
		}

		/// <summary>
		/// Sets the zoom magnication for the _sheet.  The zoom is expressed as a
		/// fraction.  For example to express a zoom of 75% use 3 for the numerator
		/// and 4 for the denominator.
		/// </summary>
		/// <param name="numerator">The numerator for the zoom magnification.</param>
		/// <param name="denominator">The denominator for the zoom magnification.</param>
		public void SetZoom(int numerator, int denominator)
		{
			if (numerator < 1 || numerator > 65535)
			{
				throw new ArgumentException("Numerator must be greater than 1 and less than 65536");
			}
			if (denominator < 1 || denominator > 65535)
			{
				throw new ArgumentException("Denominator must be greater than 1 and less than 65536");
			}
			SCLRecord sCLRecord = new SCLRecord();
			sCLRecord.Numerator = (short)numerator;
			sCLRecord.Denominator = (short)denominator;
			Sheet.SetSCLRecord(sCLRecord);
		}

		/// <summary>
		/// Sets the enclosed border of region.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="borderType">Type of the border.</param>
		/// <param name="color">The color.</param>
		public void SetEnclosedBorderOfRegion(CellRangeAddress region, BorderStyle borderType, short color)
		{
			HSSFRegionUtil.SetRightBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderRight(borderType, region, this, _workbook);
			HSSFRegionUtil.SetLeftBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderLeft(borderType, region, this, _workbook);
			HSSFRegionUtil.SetTopBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderTop(borderType, region, this, _workbook);
			HSSFRegionUtil.SetBottomBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderBottom(borderType, region, this, _workbook);
		}

		/// <summary>
		/// Sets the right border of region.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="borderType">Type of the border.</param>
		/// <param name="color">The color.</param>
		public void SetBorderRightOfRegion(CellRangeAddress region, BorderStyle borderType, short color)
		{
			HSSFRegionUtil.SetRightBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderRight(borderType, region, this, _workbook);
		}

		/// <summary>
		/// Sets the left border of region.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="borderType">Type of the border.</param>
		/// <param name="color">The color.</param>
		public void SetBorderLeftOfRegion(CellRangeAddress region, BorderStyle borderType, short color)
		{
			HSSFRegionUtil.SetLeftBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderLeft(borderType, region, this, _workbook);
		}

		/// <summary>
		/// Sets the top border of region.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="borderType">Type of the border.</param>
		/// <param name="color">The color.</param>
		public void SetBorderTopOfRegion(CellRangeAddress region, BorderStyle borderType, short color)
		{
			HSSFRegionUtil.SetTopBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderTop(borderType, region, this, _workbook);
		}

		/// <summary>
		/// Sets the bottom border of region.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <param name="borderType">Type of the border.</param>
		/// <param name="color">The color.</param>
		public void SetBorderBottomOfRegion(CellRangeAddress region, BorderStyle borderType, short color)
		{
			HSSFRegionUtil.SetBottomBorderColor(color, region, this, _workbook);
			HSSFRegionUtil.SetBorderBottom(borderType, region, this, _workbook);
		}

		/// <summary>
		/// Sets desktop window pane display area, when the
		/// file is first opened in a viewer.
		/// </summary>
		/// <param name="toprow">the top row to show in desktop window pane</param>
		/// <param name="leftcol">the left column to show in desktop window pane</param>
		public void ShowInPane(short toprow, short leftcol)
		{
			_sheet.TopRow = toprow;
			_sheet.LeftCol = leftcol;
		}

		/// <summary>
		/// Shifts the merged regions left or right depending on mode
		/// TODO: MODE , this is only row specific
		/// </summary>
		/// <param name="startRow">The start row.</param>
		/// <param name="endRow">The end row.</param>
		/// <param name="n">The n.</param>
		/// <param name="IsRow">if set to <c>true</c> [is row].</param>
		protected void ShiftMerged(int startRow, int endRow, int n, bool IsRow)
		{
			List<CellRangeAddress> list = new List<CellRangeAddress>();
			for (int i = 0; i < NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = GetMergedRegion(i);
				bool flag = mergedRegion.FirstRow >= startRow || mergedRegion.LastRow >= startRow;
				bool flag2 = mergedRegion.FirstRow <= endRow || mergedRegion.LastRow <= endRow;
				if (flag && flag2 && !SheetUtil.ContainsCell(mergedRegion, startRow - 1, 0) && !SheetUtil.ContainsCell(mergedRegion, endRow + 1, 0))
				{
					mergedRegion.FirstRow += n;
					mergedRegion.LastRow += n;
					list.Add(mergedRegion);
					RemoveMergedRegion(i);
					i--;
				}
			}
			IEnumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CellRangeAddress region = (CellRangeAddress)enumerator.Current;
				AddMergedRegion(region);
			}
		}

		[Obsolete]
		private static bool ContainsCell(CellRangeAddress cr, int rowIx, int colIx)
		{
			if (cr.FirstRow <= rowIx && cr.LastRow >= rowIx && cr.FirstColumn <= colIx && cr.LastColumn >= colIx)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Shifts rows between startRow and endRow n number of rows.
		/// If you use a negative number, it will Shift rows up.
		/// Code Ensures that rows don't wrap around.
		/// Calls ShiftRows(startRow, endRow, n, false, false);
		/// Additionally Shifts merged regions that are completely defined in these
		/// rows (ie. merged 2 cells on a row to be Shifted).
		/// </summary>
		/// <param name="startRow">the row to start Shifting</param>
		/// <param name="endRow">the row to end Shifting</param>
		/// <param name="n">the number of rows to Shift</param>
		public void ShiftRows(int startRow, int endRow, int n)
		{
			ShiftRows(startRow, endRow, n, copyRowHeight: false, resetOriginalRowHeight: false);
		}

		/// <summary>
		/// Shifts rows between startRow and endRow n number of rows.
		/// If you use a negative number, it will shift rows up.
		/// Code ensures that rows don't wrap around
		/// Additionally shifts merged regions that are completely defined in these
		/// rows (ie. merged 2 cells on a row to be shifted).
		/// TODO Might want to add bounds checking here
		/// </summary>
		/// <param name="startRow">the row to start shifting</param>
		/// <param name="endRow">the row to end shifting</param>
		/// <param name="n">the number of rows to shift</param>
		/// <param name="copyRowHeight">whether to copy the row height during the shift</param>
		/// <param name="resetOriginalRowHeight">whether to set the original row's height to the default</param>
		public void ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight)
		{
			ShiftRows(startRow, endRow, n, copyRowHeight, resetOriginalRowHeight, moveComments: true);
		}

		/// <summary>
		/// Shifts rows between startRow and endRow n number of rows.
		/// If you use a negative number, it will Shift rows up.
		/// Code Ensures that rows don't wrap around
		/// Additionally Shifts merged regions that are completely defined in these
		/// rows (ie. merged 2 cells on a row to be Shifted).
		/// TODO Might want to Add bounds Checking here
		/// </summary>
		/// <param name="startRow">the row to start Shifting</param>
		/// <param name="endRow">the row to end Shifting</param>
		/// <param name="n">the number of rows to Shift</param>
		/// <param name="copyRowHeight">whether to copy the row height during the Shift</param>
		/// <param name="resetOriginalRowHeight">whether to Set the original row's height to the default</param>
		/// <param name="moveComments">if set to <c>true</c> [move comments].</param>
		public void ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight, bool moveComments)
		{
			int num;
			int num2;
			if (n < 0)
			{
				num = startRow;
				num2 = 1;
			}
			else
			{
				if (n <= 0)
				{
					return;
				}
				num = endRow;
				num2 = -1;
			}
			if (moveComments)
			{
				_sheet.GetNoteRecords();
			}
			else
			{
				NoteRecord[] eMPTY_ARRAY = NoteRecord.EMPTY_ARRAY;
			}
			ShiftMerged(startRow, endRow, n, IsRow: true);
			_sheet.PageSettings.ShiftRowBreaks(startRow, endRow, n);
			for (int i = num; i >= startRow && i <= endRow && i >= 0 && i < 65536; i += num2)
			{
				HSSFRow hSSFRow = (HSSFRow)GetRow(i);
				if (hSSFRow != null)
				{
					NotifyRowShifting(hSSFRow);
				}
				HSSFRow hSSFRow2 = (HSSFRow)GetRow(i + n);
				if (hSSFRow2 == null)
				{
					hSSFRow2 = (HSSFRow)CreateRow(i + n);
				}
				hSSFRow2.RemoveAllCells();
				if (hSSFRow != null)
				{
					if (copyRowHeight)
					{
						hSSFRow2.Height = hSSFRow.Height;
					}
					if (resetOriginalRowHeight)
					{
						hSSFRow.Height = 255;
					}
					List<ICell> cells = hSSFRow.Cells;
					foreach (ICell item in cells)
					{
						hSSFRow.RemoveCell(item);
						CellValueRecordInterface cellValueRecord = ((HSSFCell)item).CellValueRecord;
						cellValueRecord.Row = i + n;
						hSSFRow2.CreateCellFromRecord(cellValueRecord);
						_sheet.AddValueRecord(i + n, cellValueRecord);
						IHyperlink hyperlink = item.Hyperlink;
						if (hyperlink != null)
						{
							hyperlink.FirstRow += n;
							hyperlink.LastRow += n;
						}
					}
					hSSFRow.RemoveAllCells();
					if (moveComments)
					{
						HSSFPatriarch hSSFPatriarch = CreateDrawingPatriarch() as HSSFPatriarch;
						for (int num3 = hSSFPatriarch.Children.Count - 1; num3 >= 0; num3--)
						{
							HSSFShape hSSFShape = hSSFPatriarch.Children[num3];
							if (hSSFShape is HSSFComment)
							{
								HSSFComment hSSFComment = (HSSFComment)hSSFShape;
								if (hSSFComment.Row == i)
								{
									hSSFComment.Row = i + n;
								}
							}
						}
					}
				}
			}
			if (n > 0)
			{
				if (startRow == firstrow)
				{
					firstrow = Math.Max(startRow + n, 0);
					for (int j = startRow + 1; j < startRow + n; j++)
					{
						if (GetRow(j) != null)
						{
							firstrow = j;
							break;
						}
					}
				}
				if (endRow + n > lastrow)
				{
					lastrow = Math.Min(endRow + n, SpreadsheetVersion.EXCEL97.LastRowIndex);
				}
			}
			else
			{
				if (startRow + n < firstrow)
				{
					firstrow = Math.Max(startRow + n, 0);
				}
				if (endRow == lastrow)
				{
					lastrow = Math.Min(endRow + n, SpreadsheetVersion.EXCEL97.LastRowIndex);
					for (int k = endRow - 1; k > endRow + n; k++)
					{
						if (GetRow(k) != null)
						{
							lastrow = k;
							break;
						}
					}
				}
			}
			int sheetIndex = _workbook.GetSheetIndex(this);
			int externSheetIndex = book.CheckExternSheet(sheetIndex);
			FormulaShifter shifter = FormulaShifter.CreateForRowShift(externSheetIndex, startRow, endRow, n);
			_sheet.UpdateFormulasAfterCellShift(shifter, externSheetIndex);
			int numberOfSheets = _workbook.NumberOfSheets;
			for (int l = 0; l < numberOfSheets; l++)
			{
				InternalSheet sheet = ((HSSFSheet)_workbook.GetSheetAt(l)).Sheet;
				if (sheet != _sheet)
				{
					int externSheetIndex2 = book.CheckExternSheet(l);
					sheet.UpdateFormulasAfterCellShift(shifter, externSheetIndex2);
				}
			}
			_workbook.Workbook.UpdateNamesAfterCellShift(shifter);
		}

		/// <summary>
		/// Inserts the chart records.
		/// </summary>
		/// <param name="records">The records.</param>
		public void InsertChartRecords(List<RecordBase> records)
		{
			int index = _sheet.FindFirstRecordLocBySid(574);
			_sheet.Records.InsertRange(index, records);
		}

		private void NotifyRowShifting(HSSFRow row)
		{
			string msg = "Row[rownum=" + row.RowNum + "] contains cell(s) included in a multi-cell array formula. You cannot change part of an array.";
			foreach (ICell cell in row.Cells)
			{
				HSSFCell hSSFCell = (HSSFCell)cell;
				if (hSSFCell.IsPartOfArrayFormulaGroup)
				{
					hSSFCell.NotifyArrayFormulaChanging(msg);
				}
			}
		}

		/// <summary>
		/// Creates a split (freezepane). Any existing freezepane or split pane is overwritten.
		/// </summary>
		/// <param name="colSplit">Horizonatal position of split.</param>
		/// <param name="rowSplit">Vertical position of split.</param>
		/// <param name="leftmostColumn">Top row visible in bottom pane</param>
		/// <param name="topRow">Left column visible in right pane.</param>
		public void CreateFreezePane(int colSplit, int rowSplit, int leftmostColumn, int topRow)
		{
			ValidateColumn(colSplit);
			ValidateRow(rowSplit);
			if (leftmostColumn < colSplit)
			{
				throw new ArgumentException("leftmostColumn parameter must not be less than colSplit parameter");
			}
			if (topRow < rowSplit)
			{
				throw new ArgumentException("topRow parameter must not be less than leftmostColumn parameter");
			}
			Sheet.CreateFreezePane(colSplit, rowSplit, topRow, leftmostColumn);
		}

		/// <summary>
		/// Creates a split (freezepane). Any existing freezepane or split pane is overwritten.
		/// </summary>
		/// <param name="colSplit">Horizonatal position of split.</param>
		/// <param name="rowSplit">Vertical position of split.</param>
		public void CreateFreezePane(int colSplit, int rowSplit)
		{
			CreateFreezePane(colSplit, rowSplit, colSplit, rowSplit);
		}

		/// <summary>
		/// Creates a split pane. Any existing freezepane or split pane is overwritten.
		/// </summary>
		/// <param name="xSplitPos">Horizonatal position of split (in 1/20th of a point).</param>
		/// <param name="ySplitPos">Vertical position of split (in 1/20th of a point).</param>
		/// <param name="leftmostColumn">Left column visible in right pane.</param>
		/// <param name="topRow">Top row visible in bottom pane.</param>
		/// <param name="activePane">Active pane.  One of: PANE_LOWER_RIGHT,PANE_UPPER_RIGHT, PANE_LOWER_LEFT, PANE_UPPER_LEFT</param>
		public void CreateSplitPane(int xSplitPos, int ySplitPos, int leftmostColumn, int topRow, PanePosition activePane)
		{
			Sheet.CreateSplitPane(xSplitPos, ySplitPos, topRow, leftmostColumn, activePane);
		}

		/// <summary>
		/// Gets the size of the margin in inches.
		/// </summary>
		/// <param name="margin">which margin to get.</param>
		/// <returns>the size of the margin</returns>
		public double GetMargin(MarginType margin)
		{
			switch (margin)
			{
			case MarginType.FooterMargin:
				return _sheet.PageSettings.PrintSetup.FooterMargin;
			case MarginType.HeaderMargin:
				return _sheet.PageSettings.PrintSetup.HeaderMargin;
			default:
				return _sheet.PageSettings.GetMargin(margin);
			}
		}

		/// <summary>
		/// Sets the size of the margin in inches.
		/// </summary>
		/// <param name="margin">which margin to get.</param>
		/// <param name="size">the size of the margin</param>
		public void SetMargin(MarginType margin, double size)
		{
			switch (margin)
			{
			case MarginType.FooterMargin:
				_sheet.PageSettings.PrintSetup.FooterMargin = size;
				break;
			case MarginType.HeaderMargin:
				_sheet.PageSettings.PrintSetup.HeaderMargin = size;
				break;
			default:
				_sheet.PageSettings.SetMargin(margin, size);
				break;
			}
		}

		/// <summary>
		/// Sets a page break at the indicated row
		/// </summary>
		/// <param name="row">The row.</param>
		public void SetRowBreak(int row)
		{
			ValidateRow(row);
			_sheet.PageSettings.SetRowBreak(row, 0, 255);
		}

		/// <summary>
		/// Determines if there is a page break at the indicated row
		/// </summary>
		/// <param name="row">The row.</param>
		/// <returns>
		/// 	<c>true</c> if [is row broken] [the specified row]; otherwise, <c>false</c>.
		/// </returns>        
		public bool IsRowBroken(int row)
		{
			return _sheet.PageSettings.IsRowBroken(row);
		}

		/// <summary>
		/// Removes the page break at the indicated row
		/// </summary>
		/// <param name="row">The row.</param>
		public void RemoveRowBreak(int row)
		{
			_sheet.PageSettings.RemoveRowBreak(row);
		}

		/// <summary>
		/// Sets a page break at the indicated column
		/// </summary>
		/// <param name="column">The column.</param>
		public void SetColumnBreak(int column)
		{
			ValidateColumn(column);
			_sheet.PageSettings.SetColumnBreak(column, 0, -1);
		}

		/// <summary>
		/// Determines if there is a page break at the indicated column
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns>
		/// 	<c>true</c> if [is column broken] [the specified column]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsColumnBroken(int column)
		{
			return _sheet.PageSettings.IsColumnBroken(column);
		}

		/// <summary>
		/// Removes a page break at the indicated column
		/// </summary>
		/// <param name="column">The column.</param>
		public void RemoveColumnBreak(int column)
		{
			_sheet.PageSettings.RemoveColumnBreak(column);
		}

		/// <summary>
		/// Runs a bounds Check for row numbers
		/// </summary>
		/// <param name="row">The row.</param>
		protected void ValidateRow(int row)
		{
			int lastRowIndex = SpreadsheetVersion.EXCEL97.LastRowIndex;
			if (row > lastRowIndex)
			{
				throw new ArgumentException("Maximum row number is " + lastRowIndex.ToString(CultureInfo.CurrentCulture));
			}
			if (row < 0)
			{
				throw new ArgumentException("Minumum row number is 0");
			}
		}

		/// <summary>
		/// Runs a bounds Check for column numbers
		/// </summary>
		/// <param name="column">The column.</param>
		protected void ValidateColumn(int column)
		{
			int lastColumnIndex = SpreadsheetVersion.EXCEL97.LastColumnIndex;
			if (column > lastColumnIndex)
			{
				throw new ArgumentException("Maximum column number is " + lastColumnIndex.ToString(CultureInfo.CurrentCulture));
			}
			if (column < 0)
			{
				throw new ArgumentException("Minimum column number is 0");
			}
		}

		/// <summary>
		/// Aggregates the drawing records and dumps the escher record hierarchy
		/// to the standard output.
		/// </summary>
		/// <param name="fat">if set to <c>true</c> [fat].</param>
		public void DumpDrawingRecords(bool fat)
		{
			_sheet.AggregateDrawingRecords(book.DrawingManager, CreateIfMissing: false);
			EscherAggregate escherAggregate = (EscherAggregate)Sheet.FindFirstRecordBySid(9876);
			IList escherRecords = escherAggregate.EscherRecords;
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

		/// Creates the top-level drawing patriarch.  This will have
		/// the effect of removing any existing drawings on this
		/// sheet.
		/// This may then be used to add graphics or charts
		///
		/// @return The new patriarch.
		public IDrawing CreateDrawingPatriarch()
		{
			_patriarch = GetPatriarch(createIfMissing: true);
			return _patriarch;
		}

		private HSSFPatriarch GetPatriarch(bool createIfMissing)
		{
			HSSFPatriarch hSSFPatriarch = null;
			if (_patriarch != null)
			{
				return _patriarch;
			}
			DrawingManager2 drawingManager = book.FindDrawingGroup();
			if (drawingManager == null)
			{
				if (!createIfMissing)
				{
					return null;
				}
				book.CreateDrawingGroup();
				drawingManager = book.DrawingManager;
			}
			EscherAggregate escherAggregate = (EscherAggregate)_sheet.FindFirstRecordBySid(9876);
			if (escherAggregate == null || escherAggregate.GetEscherContainer() == null)
			{
				int num = _sheet.AggregateDrawingRecords(drawingManager, CreateIfMissing: false);
				if (-1 == num || (escherAggregate = (EscherAggregate)_sheet.Records[num]) == null || escherAggregate.GetEscherContainer() == null)
				{
					if (createIfMissing)
					{
						num = _sheet.AggregateDrawingRecords(drawingManager, CreateIfMissing: true);
						escherAggregate = (EscherAggregate)_sheet.Records[num];
						hSSFPatriarch = new HSSFPatriarch(this, escherAggregate);
						hSSFPatriarch.AfterCreate();
						return hSSFPatriarch;
					}
					return null;
				}
			}
			return new HSSFPatriarch(this, escherAggregate);
		}

		/// <summary>
		/// Expands or collapses a column Group.
		/// </summary>
		/// <param name="columnNumber">One of the columns in the Group.</param>
		/// <param name="collapsed">true = collapse Group, false = expand Group.</param>
		public void SetColumnGroupCollapsed(int columnNumber, bool collapsed)
		{
			_sheet.SetColumnGroupCollapsed(columnNumber, collapsed);
		}

		/// <summary>
		/// Create an outline for the provided column range.
		/// </summary>
		/// <param name="fromColumn">beginning of the column range.</param>
		/// <param name="toColumn">end of the column range.</param>
		public void GroupColumn(int fromColumn, int toColumn)
		{
			_sheet.GroupColumnRange(fromColumn, toColumn, indent: true);
		}

		/// <summary>
		/// Ungroups the column.
		/// </summary>
		/// <param name="fromColumn">From column.</param>
		/// <param name="toColumn">To column.</param>
		public void UngroupColumn(int fromColumn, int toColumn)
		{
			_sheet.GroupColumnRange(fromColumn, toColumn, indent: false);
		}

		/// <summary>
		/// Groups the row.
		/// </summary>
		/// <param name="fromRow">From row.</param>
		/// <param name="toRow">To row.</param>
		public void GroupRow(int fromRow, int toRow)
		{
			_sheet.GroupRowRange(fromRow, toRow, indent: true);
		}

		/// <summary>
		/// Remove a Array Formula from this sheet.  All cells contained in the Array Formula range are removed as well
		/// </summary>
		/// <param name="cell">any cell within Array Formula range</param>
		/// <returns>the <see cref="T:NPOI.SS.UserModel.ICellRange`1" /> of cells affected by this change</returns>
		public ICellRange<ICell> RemoveArrayFormula(ICell cell)
		{
			if (cell.Sheet != this)
			{
				throw new ArgumentException("Specified cell does not belong to this sheet.");
			}
			CellValueRecordInterface cellValueRecord = ((HSSFCell)cell).CellValueRecord;
			if (!(cellValueRecord is FormulaRecordAggregate))
			{
				string str = new CellReference(cell).FormatAsString();
				throw new ArgumentException("Cell " + str + " is not part of an array formula.");
			}
			FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)cellValueRecord;
			CellRangeAddress range = formulaRecordAggregate.RemoveArrayFormula(cell.RowIndex, cell.ColumnIndex);
			ICellRange<ICell> cellRange = GetCellRange(range);
			foreach (ICell item in cellRange)
			{
				item.SetCellType(CellType.Blank);
			}
			return cellRange;
		}

		/// <summary>
		/// Also creates cells if they don't exist.
		/// </summary>
		private ICellRange<ICell> GetCellRange(CellRangeAddress range)
		{
			int firstRow = range.FirstRow;
			int firstColumn = range.FirstColumn;
			int lastRow = range.LastRow;
			int lastColumn = range.LastColumn;
			int num = lastRow - firstRow + 1;
			int num2 = lastColumn - firstColumn + 1;
			List<ICell> list = new List<ICell>(num * num2);
			for (int i = firstRow; i <= lastRow; i++)
			{
				for (int j = firstColumn; j <= lastColumn; j++)
				{
					IRow row = GetRow(i);
					if (row == null)
					{
						row = CreateRow(i);
					}
					ICell cell = row.GetCell(j);
					if (cell == null)
					{
						cell = row.CreateCell(j);
					}
					list.Add(cell);
				}
			}
			return SSCellRange<ICell>.Create(firstRow, firstColumn, num, num2, list, typeof(HSSFCell));
		}

		/// <summary>
		/// Sets array formula to specified region for result.
		/// </summary>
		/// <param name="formula">text representation of the formula</param>
		/// <param name="range">Region of array formula for result</param>
		/// <returns>the <see cref="T:NPOI.SS.UserModel.ICellRange`1" /> of cells affected by this change</returns>
		public ICellRange<ICell> SetArrayFormula(string formula, CellRangeAddress range)
		{
			int sheetIndex = _workbook.GetSheetIndex(this);
			Ptg[] ptgs = HSSFFormulaParser.Parse(formula, _workbook, FormulaType.Array, sheetIndex);
			ICellRange<ICell> cellRange = GetCellRange(range);
			foreach (HSSFCell item in cellRange)
			{
				item.SetCellArrayFormula(range);
			}
			HSSFCell hSSFCell2 = (HSSFCell)cellRange.TopLeftCell;
			FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)hSSFCell2.CellValueRecord;
			formulaRecordAggregate.SetArrayFormula(range, ptgs);
			return cellRange;
		}

		/// <summary>
		/// Ungroups the row.
		/// </summary>
		/// <param name="fromRow">From row.</param>
		/// <param name="toRow">To row.</param>
		public void UngroupRow(int fromRow, int toRow)
		{
			_sheet.GroupRowRange(fromRow, toRow, indent: false);
		}

		/// <summary>
		/// Sets the row group collapsed.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="collapse">if set to <c>true</c> [collapse].</param>
		public void SetRowGroupCollapsed(int row, bool collapse)
		{
			if (collapse)
			{
				_sheet.RowsAggregate.CollapseRow(row);
			}
			else
			{
				_sheet.RowsAggregate.ExpandRow(row);
			}
		}

		/// <summary>
		/// Sets the default column style for a given column.  POI will only apply this style to new cells Added to the _sheet.
		/// </summary>
		/// <param name="column">the column index</param>
		/// <param name="style">the style to set</param>
		public void SetDefaultColumnStyle(int column, ICellStyle style)
		{
			_sheet.SetDefaultColumnStyle(column, style.Index);
		}

		/// <summary>
		/// Adjusts the column width to fit the contents.
		/// This Process can be relatively slow on large sheets, so this should
		/// normally only be called once per column, at the end of your
		/// Processing.
		/// </summary>
		/// <param name="column">the column index.</param>
		public void AutoSizeColumn(int column)
		{
			AutoSizeColumn(column, useMergedCells: false);
		}

		/// <summary>
		/// Adjusts the column width to fit the contents.
		/// This Process can be relatively slow on large sheets, so this should
		/// normally only be called once per column, at the end of your
		/// Processing.
		/// You can specify whether the content of merged cells should be considered or ignored.
		/// Default is to ignore merged cells.
		/// </summary>
		/// <param name="column">the column index</param>
		/// <param name="useMergedCells">whether to use the contents of merged cells when calculating the width of the column</param>
		public void AutoSizeColumn(int column, bool useMergedCells)
		{
			double columnWidth = SheetUtil.GetColumnWidth(this, column, useMergedCells);
			if (columnWidth != -1.0)
			{
				columnWidth *= 256.0;
				int num = 65280;
				if (columnWidth > (double)num)
				{
					columnWidth = (double)num;
				}
				SetColumnWidth(column, (int)columnWidth);
			}
		}

		/// <summary>
		/// Checks if the provided region is part of the merged regions.
		/// </summary>
		/// <param name="mergedRegion">Region searched in the merged regions</param>
		/// <returns><c>true</c>, when the region is contained in at least one of the merged regions</returns>
		public bool IsMergedRegion(CellRangeAddress mergedRegion)
		{
			foreach (CellRangeAddress mergedRegion2 in _sheet.MergedRecords.MergedRegions)
			{
				if (mergedRegion2.FirstColumn <= mergedRegion.FirstColumn && mergedRegion2.LastColumn >= mergedRegion.LastColumn && mergedRegion2.FirstRow <= mergedRegion.FirstRow && mergedRegion2.LastRow >= mergedRegion.LastRow)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets the merged region at the specified index
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public CellRangeAddress GetMergedRegion(int index)
		{
			return _sheet.GetMergedRegionAt(index);
		}

		/// <summary>
		/// Convert HSSFFont to Font.
		/// </summary>
		/// <param name="font1">The font.</param>
		/// <returns></returns>
		public Font HSSFFont2Font(HSSFFont font1)
		{
			return new Font(font1.FontName, (float)font1.FontHeightInPoints);
		}

		/// <summary>
		/// Returns cell comment for the specified row and column
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns>cell comment or null if not found</returns>
		public IComment GetCellComment(int row, int column)
		{
			return FindCellComment(row, column);
		}

		/// <summary>
		/// Create an instance of a DataValidationHelper.
		/// </summary>
		/// <returns>Instance of a DataValidationHelper</returns>
		public IDataValidationHelper GetDataValidationHelper()
		{
			return new HSSFDataValidationHelper(this);
		}

		/// <summary>
		/// Enable filtering for a range of cells
		/// </summary>
		/// <param name="range">the range of cells to filter</param>
		public IAutoFilter SetAutoFilter(CellRangeAddress range)
		{
			InternalWorkbook workbook = _workbook.Workbook;
			int sheetIndex = _workbook.GetSheetIndex(this);
			NameRecord nameRecord = workbook.GetSpecificBuiltinRecord(13, sheetIndex + 1);
			if (nameRecord == null)
			{
				nameRecord = workbook.CreateBuiltInName(13, sheetIndex + 1);
			}
			Area3DPtg area3DPtg = new Area3DPtg(range.FirstRow, range.LastRow, range.FirstColumn, range.LastColumn, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false, sheetIndex);
			nameRecord.NameDefinition = new Ptg[1]
			{
				area3DPtg
			};
			AutoFilterInfoRecord autoFilterInfoRecord = new AutoFilterInfoRecord();
			int num = 1 + range.LastColumn - range.FirstColumn;
			autoFilterInfoRecord.NumEntries = (short)num;
			int index = _sheet.FindFirstRecordLocBySid(512);
			_sheet.Records.Insert(index, autoFilterInfoRecord);
			HSSFPatriarch hSSFPatriarch = (HSSFPatriarch)CreateDrawingPatriarch();
			for (int i = range.FirstColumn; i <= range.LastColumn; i++)
			{
				hSSFPatriarch.CreateComboBox(new HSSFClientAnchor(0, 0, 0, 0, (short)i, range.FirstRow, (short)(i + 1), range.FirstRow + 1));
			}
			return new HSSFAutoFilter(this);
		}

		protected internal HSSFComment FindCellComment(int row, int column)
		{
			HSSFPatriarch hSSFPatriarch = DrawingPatriarch as HSSFPatriarch;
			if (hSSFPatriarch == null)
			{
				hSSFPatriarch = (CreateDrawingPatriarch() as HSSFPatriarch);
			}
			return LookForComment(hSSFPatriarch, row, column);
		}

		private HSSFComment LookForComment(HSSFShapeContainer container, int row, int column)
		{
			foreach (HSSFShape child in container.Children)
			{
				HSSFShape hSSFShape = child;
				if (hSSFShape is HSSFShapeGroup)
				{
					HSSFShape hSSFShape2 = LookForComment((HSSFShapeContainer)hSSFShape, row, column);
					if (hSSFShape2 != null)
					{
						return (HSSFComment)hSSFShape2;
					}
				}
				else if (hSSFShape is HSSFComment)
				{
					HSSFComment hSSFComment = (HSSFComment)hSSFShape;
					if (hSSFComment.Column == column && hSSFComment.Row == row)
					{
						return hSSFComment;
					}
				}
			}
			return null;
		}

		private void SetRepeatingRowsAndColumns(CellRangeAddress rowDef, CellRangeAddress colDef)
		{
			int sheetIndex = _workbook.GetSheetIndex(this);
			int lastRowIndex = SpreadsheetVersion.EXCEL97.LastRowIndex;
			int lastColumnIndex = SpreadsheetVersion.EXCEL97.LastColumnIndex;
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			if (rowDef != null)
			{
				num3 = rowDef.FirstRow;
				num4 = rowDef.LastRow;
				if ((num3 == -1 && num4 != -1) || num3 > num4 || num3 < 0 || num3 > lastRowIndex || num4 < 0 || num4 > lastRowIndex)
				{
					throw new ArgumentException("Invalid row range specification");
				}
			}
			if (colDef != null)
			{
				num = colDef.FirstColumn;
				num2 = colDef.LastColumn;
				if ((num == -1 && num2 != -1) || num > num2 || num < 0 || num > lastColumnIndex || num2 < 0 || num2 > lastColumnIndex)
				{
					throw new ArgumentException("Invalid column range specification");
				}
			}
			short externalSheetIndex = (short)_workbook.Workbook.CheckExternSheet(sheetIndex);
			bool flag = rowDef != null && colDef != null;
			bool flag2 = rowDef == null && colDef == null;
			HSSFName hSSFName = _workbook.GetBuiltInName(7, sheetIndex);
			if (flag2)
			{
				if (hSSFName != null)
				{
					_workbook.RemoveName(hSSFName);
				}
			}
			else
			{
				if (hSSFName == null)
				{
					hSSFName = _workbook.CreateBuiltInName(7, sheetIndex);
				}
				List<Ptg> list = new List<Ptg>();
				if (flag)
				{
					int subExprLen = 23;
					list.Add(new MemFuncPtg(subExprLen));
				}
				if (colDef != null)
				{
					Area3DPtg item = new Area3DPtg(0, lastRowIndex, num, num2, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false, externalSheetIndex);
					list.Add(item);
				}
				if (rowDef != null)
				{
					Area3DPtg item2 = new Area3DPtg(num3, num4, 0, lastColumnIndex, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false, externalSheetIndex);
					list.Add(item2);
				}
				if (flag)
				{
					list.Add(UnionPtg.instance);
				}
				Ptg[] nameDefinition = list.ToArray();
				hSSFName.SetNameDefinition(nameDefinition);
				HSSFPrintSetup hSSFPrintSetup = (HSSFPrintSetup)PrintSetup;
				hSSFPrintSetup.ValidSettings = false;
				SetActive(sel: true);
			}
		}

		private CellRangeAddress GetRepeatingRowsOrColums(bool rows)
		{
			NameRecord builtinNameRecord = GetBuiltinNameRecord(7);
			if (builtinNameRecord == null)
			{
				return null;
			}
			Ptg[] nameDefinition = builtinNameRecord.NameDefinition;
			if (builtinNameRecord.NameDefinition == null)
			{
				return null;
			}
			int lastRowIndex = SpreadsheetVersion.EXCEL97.LastRowIndex;
			int lastColumnIndex = SpreadsheetVersion.EXCEL97.LastColumnIndex;
			Ptg[] array = nameDefinition;
			foreach (Ptg ptg in array)
			{
				if (ptg is Area3DPtg)
				{
					Area3DPtg area3DPtg = (Area3DPtg)ptg;
					if (area3DPtg.FirstColumn == 0 && area3DPtg.LastColumn == lastColumnIndex)
					{
						if (rows)
						{
							return new CellRangeAddress(area3DPtg.FirstRow, area3DPtg.LastRow, -1, -1);
						}
					}
					else if (area3DPtg.FirstRow == 0 && area3DPtg.LastRow == lastRowIndex && !rows)
					{
						return new CellRangeAddress(-1, -1, area3DPtg.FirstColumn, area3DPtg.LastColumn);
					}
				}
			}
			return null;
		}

		private NameRecord GetBuiltinNameRecord(byte builtinCode)
		{
			int sheetIndex = _workbook.GetSheetIndex(this);
			int num = _workbook.FindExistingBuiltinNameRecordIdx(sheetIndex, builtinCode);
			if (num == -1)
			{
				return null;
			}
			return _workbook.GetNameRecord(num);
		}

		public ISheet CopySheet()
		{
			return CopySheet(SheetName + " - Copy", copyStyle: true);
		}

		public ISheet CopySheet(bool CopyStyle)
		{
			return CopySheet(SheetName + " - Copy", CopyStyle);
		}

		public ISheet CopySheet(string Name)
		{
			return CopySheet(Name, copyStyle: true);
		}

		public ISheet CopySheet(string Name, bool copyStyle)
		{
			int num = 0;
			HSSFSheet hSSFSheet = (HSSFSheet)Workbook.CreateSheet(Name);
			hSSFSheet._sheet = Sheet.CloneSheet();
			IDictionary<int, HSSFCellStyle> styleMap = copyStyle ? new Dictionary<int, HSSFCellStyle>() : null;
			for (int i = FirstRowNum; i <= LastRowNum; i++)
			{
				HSSFRow hSSFRow = (HSSFRow)GetRow(i);
				HSSFRow destRow = (HSSFRow)hSSFSheet.CreateRow(i);
				if (hSSFRow != null)
				{
					CopyRow(this, hSSFSheet, hSSFRow, destRow, styleMap, new Dictionary<short, short>(), keepFormulas: true);
					if (hSSFRow.LastCellNum > num)
					{
						num = hSSFRow.LastCellNum;
					}
				}
			}
			for (int j = 0; j <= num; j++)
			{
				hSSFSheet.SetColumnWidth(j, GetColumnWidth(j));
			}
			hSSFSheet.ForceFormulaRecalculation = true;
			hSSFSheet.PrintSetup.Landscape = PrintSetup.Landscape;
			hSSFSheet.PrintSetup.HResolution = PrintSetup.HResolution;
			hSSFSheet.PrintSetup.VResolution = PrintSetup.VResolution;
			hSSFSheet.SetMargin(MarginType.LeftMargin, GetMargin(MarginType.LeftMargin));
			hSSFSheet.SetMargin(MarginType.RightMargin, GetMargin(MarginType.RightMargin));
			hSSFSheet.SetMargin(MarginType.TopMargin, GetMargin(MarginType.TopMargin));
			hSSFSheet.SetMargin(MarginType.BottomMargin, GetMargin(MarginType.BottomMargin));
			hSSFSheet.PrintSetup.HeaderMargin = PrintSetup.HeaderMargin;
			hSSFSheet.PrintSetup.FooterMargin = PrintSetup.FooterMargin;
			hSSFSheet.Header.Left = Header.Left;
			hSSFSheet.Header.Center = Header.Center;
			hSSFSheet.Header.Right = Header.Right;
			hSSFSheet.Footer.Left = Footer.Left;
			hSSFSheet.Footer.Center = Footer.Center;
			hSSFSheet.Footer.Right = Footer.Right;
			hSSFSheet.PrintSetup.Scale = PrintSetup.Scale;
			hSSFSheet.PrintSetup.FitHeight = PrintSetup.FitHeight;
			hSSFSheet.PrintSetup.FitWidth = PrintSetup.FitWidth;
			return hSSFSheet;
		}

		public void CopyTo(HSSFWorkbook dest, string name, bool copyStyle, bool keepFormulas)
		{
			int num = 0;
			HSSFSheet hSSFSheet = (HSSFSheet)dest.CreateSheet(name);
			hSSFSheet._sheet = Sheet.CloneSheet();
			Dictionary<short, short> paletteMap = new Dictionary<short, short>();
			if (dest.NumberOfSheets == 1)
			{
				dest.Workbook.CustomPalette.ClearColors();
				paletteMap = MergePalettes(Workbook as HSSFWorkbook, dest);
			}
			else if (dest != Workbook)
			{
				paletteMap = MergePalettes(Workbook as HSSFWorkbook, dest);
			}
			IDictionary<int, HSSFCellStyle> styleMap = copyStyle ? new Dictionary<int, HSSFCellStyle>() : null;
			for (int i = FirstRowNum; i <= LastRowNum; i++)
			{
				HSSFRow hSSFRow = (HSSFRow)GetRow(i);
				HSSFRow destRow = (HSSFRow)hSSFSheet.CreateRow(i);
				if (hSSFRow != null)
				{
					CopyRow(this, hSSFSheet, hSSFRow, destRow, styleMap, paletteMap, keepFormulas);
					if (hSSFRow.LastCellNum > num)
					{
						num = hSSFRow.LastCellNum;
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				hSSFSheet.SetColumnWidth(j, GetColumnWidth(j));
			}
			hSSFSheet.ForceFormulaRecalculation = true;
			hSSFSheet.PrintSetup.Landscape = PrintSetup.Landscape;
			hSSFSheet.PrintSetup.HResolution = PrintSetup.HResolution;
			hSSFSheet.PrintSetup.VResolution = PrintSetup.VResolution;
			hSSFSheet.SetMargin(MarginType.LeftMargin, GetMargin(MarginType.LeftMargin));
			hSSFSheet.SetMargin(MarginType.RightMargin, GetMargin(MarginType.RightMargin));
			hSSFSheet.SetMargin(MarginType.TopMargin, GetMargin(MarginType.TopMargin));
			hSSFSheet.SetMargin(MarginType.BottomMargin, GetMargin(MarginType.BottomMargin));
			hSSFSheet.PrintSetup.HeaderMargin = PrintSetup.HeaderMargin;
			hSSFSheet.PrintSetup.FooterMargin = PrintSetup.FooterMargin;
			hSSFSheet.Header.Left = Header.Left;
			hSSFSheet.Header.Center = Header.Center;
			hSSFSheet.Header.Right = Header.Right;
			hSSFSheet.Footer.Left = Footer.Left;
			hSSFSheet.Footer.Center = Footer.Center;
			hSSFSheet.Footer.Right = Footer.Right;
			hSSFSheet.PrintSetup.Scale = PrintSetup.Scale;
			hSSFSheet.PrintSetup.FitHeight = PrintSetup.FitHeight;
			hSSFSheet.PrintSetup.FitWidth = PrintSetup.FitWidth;
			EscherAggregate drawingEscherAggregate = DrawingEscherAggregate;
			if (drawingEscherAggregate != null)
			{
				if (dest.Workbook.DrawingManager == null)
				{
					dest.Workbook.CreateDrawingGroup();
				}
				EscherAggregate drawingEscherAggregate2 = hSSFSheet.DrawingEscherAggregate;
				IEnumerable<int> enumerable = FindUsedPictures(drawingEscherAggregate.EscherRecords);
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				IList allPictures = Workbook.GetAllPictures();
				foreach (int item in enumerable)
				{
					if (item <= allPictures.Count)
					{
						HSSFPictureData hSSFPictureData = (HSSFPictureData)allPictures[item - 1];
						int value = dest.AddPicture(hSSFPictureData.Data, (PictureType)hSSFPictureData.Format);
						dictionary.Add(item, value);
					}
				}
				foreach (EscherRecord escherRecord in drawingEscherAggregate2.EscherRecords)
				{
					ApplyEscherRemap(escherRecord, dictionary);
				}
			}
		}

		private IEnumerable<int> FindUsedPictures(IEnumerable<EscherRecord> escherRecords)
		{
			List<int> list = new List<int>();
			foreach (EscherRecord escherRecord in escherRecords)
			{
				GetSheetImageIds(escherRecord, list);
			}
			return list;
		}

		private void GetSheetImageIds(EscherRecord parent, List<int> usedIds)
		{
			foreach (EscherRecord childRecord in parent.ChildRecords)
			{
				if (childRecord is EscherOptRecord)
				{
					EscherOptRecord escherOptRecord = (EscherOptRecord)childRecord;
					foreach (EscherProperty escherProperty in escherOptRecord.EscherProperties)
					{
						if (escherProperty.PropertyNumber == 260)
						{
							int propertyValue = ((EscherSimpleProperty)escherProperty).PropertyValue;
							if (!usedIds.Contains(propertyValue))
							{
								usedIds.Add(propertyValue);
							}
							break;
						}
					}
				}
				if (childRecord.ChildRecords.Count > 0)
				{
					foreach (EscherRecord childRecord2 in childRecord.ChildRecords)
					{
						GetSheetImageIds(childRecord2, usedIds);
					}
				}
			}
		}

		private void ApplyEscherRemap(EscherRecord parent, Dictionary<int, int> mappings)
		{
			foreach (EscherRecord childRecord in parent.ChildRecords)
			{
				if (childRecord is EscherOptRecord)
				{
					EscherOptRecord escherOptRecord = (EscherOptRecord)childRecord;
					foreach (EscherProperty escherProperty in escherOptRecord.EscherProperties)
					{
						if (escherProperty.PropertyNumber == 260)
						{
							int propertyValue = ((EscherSimpleProperty)escherProperty).PropertyValue;
							if (mappings.ContainsKey(propertyValue))
							{
								((EscherSimpleProperty)escherProperty).PropertyValue = mappings[propertyValue];
							}
							break;
						}
					}
				}
				if (childRecord.ChildRecords.Count > 0)
				{
					foreach (EscherRecord childRecord2 in childRecord.ChildRecords)
					{
						ApplyEscherRemap(childRecord2, mappings);
					}
				}
			}
		}

		private static Dictionary<short, short> MergePalettes(HSSFWorkbook source, HSSFWorkbook dest)
		{
			Dictionary<short, short> dictionary = new Dictionary<short, short>();
			for (short num = 0; num < source.Workbook.CustomPalette.NumColors; num = (short)(num + 1))
			{
				byte[] color = source.Workbook.CustomPalette.GetColor((short)(num + 8));
				bool flag = false;
				for (short num2 = 0; num2 < dest.Workbook.CustomPalette.NumColors; num2 = (short)(num2 + 1))
				{
					byte[] color2 = dest.Workbook.CustomPalette.GetColor((short)(num2 + 8));
					if (color[0] == color2[0] && color[1] == color2[1] && color[2] == color2[2])
					{
						flag = true;
						dictionary.Add((short)(num + 8), (short)(num2 + 8));
						break;
					}
				}
				if (!flag)
				{
					short numColors = dest.Workbook.CustomPalette.NumColors;
					dest.Workbook.CustomPalette.SetColor((short)(numColors + 8), color[0], color[1], color[2]);
					dictionary.Add((short)(num + 8), (short)(numColors + 8));
				}
			}
			return dictionary;
		}

		private static void CopyRow(HSSFSheet srcSheet, HSSFSheet destSheet, HSSFRow srcRow, HSSFRow destRow, IDictionary<int, HSSFCellStyle> styleMap, Dictionary<short, short> paletteMap, bool keepFormulas)
		{
			List<CellRangeAddress> mergedRegions = destSheet.Sheet.MergedRecords.MergedRegions;
			destRow.Height = srcRow.Height;
			destRow.IsHidden = srcRow.IsHidden;
			destRow.RowRecord.OptionFlags = srcRow.RowRecord.OptionFlags;
			for (int i = srcRow.FirstCellNum; i <= srcRow.LastCellNum; i++)
			{
				HSSFCell hSSFCell = (HSSFCell)srcRow.GetCell(i);
				HSSFCell hSSFCell2 = (HSSFCell)destRow.GetCell(i);
				if (srcSheet.Workbook == destSheet.Workbook)
				{
					hSSFCell2 = (HSSFCell)destRow.GetCell(i);
				}
				if (hSSFCell != null)
				{
					if (hSSFCell2 == null)
					{
						hSSFCell2 = (HSSFCell)destRow.CreateCell(i);
					}
					HSSFCellUtil.CopyCell(hSSFCell, hSSFCell2, styleMap, paletteMap, keepFormulas);
					CellRangeAddress mergedRegion = GetMergedRegion(srcSheet, srcRow.RowNum, (short)hSSFCell.ColumnIndex);
					if (mergedRegion != null)
					{
						CellRangeAddress cellRangeAddress = new CellRangeAddress(mergedRegion.FirstRow, mergedRegion.LastRow, mergedRegion.FirstColumn, mergedRegion.LastColumn);
						if (IsNewMergedRegion(cellRangeAddress, mergedRegions))
						{
							mergedRegions.Add(cellRangeAddress);
						}
					}
				}
			}
		}

		public static CellRangeAddress GetMergedRegion(HSSFSheet sheet, int rowNum, short cellNum)
		{
			for (int i = 0; i < sheet.NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = sheet.GetMergedRegion(i);
				if (rowNum >= mergedRegion.FirstRow && rowNum <= mergedRegion.LastRow && cellNum >= mergedRegion.FirstColumn && cellNum <= mergedRegion.LastColumn)
				{
					return mergedRegion;
				}
			}
			return null;
		}

		private static bool AreAllTrue(params bool[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (!values[i])
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsNewMergedRegion(CellRangeAddress newMergedRegion, List<CellRangeAddress> mergedRegions)
		{
			bool result = true;
			foreach (CellRangeAddress mergedRegion in mergedRegions)
			{
				bool flag = mergedRegion.FirstRow == newMergedRegion.FirstRow;
				bool flag2 = mergedRegion.LastRow == newMergedRegion.LastRow;
				bool flag3 = mergedRegion.FirstColumn == newMergedRegion.FirstColumn;
				bool flag4 = mergedRegion.LastColumn == newMergedRegion.LastColumn;
				if (AreAllTrue(flag, flag2, flag3, flag4))
				{
					result = false;
				}
			}
			return result;
		}
	}
}
