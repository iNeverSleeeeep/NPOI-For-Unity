using NPOI.HSSF.Record;
using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of a SpreadsheetML worksheet.
	///
	/// <p>
	/// Sheets are the central structures within a workbook, and are where a user does most of his spreadsheet work.
	/// The most common type of sheet is the worksheet, which is represented as a grid of cells. Worksheet cells can
	/// contain text, numbers, dates, and formulas. Cells can also be formatted.
	/// </p>
	public class XSSFSheet : POIXMLDocumentPart, ISheet
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(XSSFSheet));

		internal CT_Sheet sheet;

		internal CT_Worksheet worksheet;

		private SortedList<int, XSSFRow> _rows;

		private List<XSSFHyperlink> hyperlinks;

		private ColumnHelper columnHelper;

		private CommentsTable sheetComments;

		/// cache of master shared formulas in this sheet.
		/// Master shared formula is the first formula in a group of shared formulas is saved in the f element.
		private Dictionary<int, CT_CellFormula> sharedFormulas;

		private Dictionary<string, XSSFTable> tables;

		private List<CellRangeAddress> arrayFormulas;

		private XSSFDataValidationHelper dataValidationHelper;

		private XSSFDrawing drawing;

		/// Returns the parent XSSFWorkbook
		///
		/// @return the parent XSSFWorkbook
		public IWorkbook Workbook
		{
			get
			{
				return (XSSFWorkbook)GetParent();
			}
		}

		/// Returns the name of this sheet
		///
		/// @return the name of this sheet
		public string SheetName
		{
			get
			{
				return sheet.name;
			}
		}

		/// Vertical page break information used for print layout view, page layout view, drawing print breaks
		/// in normal view, and for printing the worksheet.
		///
		/// @return column indexes of all the vertical page breaks, never <code>null</code>
		public int[] ColumnBreaks
		{
			get
			{
				if (!worksheet.IsSetColBreaks() || worksheet.colBreaks.sizeOfBrkArray() == 0)
				{
					return new int[0];
				}
				List<CT_Break> brk = worksheet.colBreaks.brk;
				int[] array = new int[brk.Count];
				for (int i = 0; i < brk.Count; i++)
				{
					CT_Break cT_Break = brk[i];
					array[i] = (int)(cT_Break.id - 1);
				}
				return array;
			}
		}

		/// Get the default column width for the sheet (if the columns do not define their own width) in
		/// characters.
		/// <p>
		/// Note, this value is different from {@link #GetColumnWidth(int)}. The latter is always greater and includes
		/// 4 pixels of margin pAdding (two on each side), plus 1 pixel pAdding for the gridlines.
		/// </p>
		/// @return column width, default value is 8
		public int DefaultColumnWidth
		{
			get
			{
				CT_SheetFormatPr sheetFormatPr = worksheet.sheetFormatPr;
				if (sheetFormatPr != null)
				{
					return (int)sheetFormatPr.baseColWidth;
				}
				return 8;
			}
			set
			{
				GetSheetTypeSheetFormatPr().baseColWidth = (uint)value;
			}
		}

		/// Get the default row height for the sheet (if the rows do not define their own height) in
		/// twips (1/20 of  a point)
		///
		/// @return  default row height
		public short DefaultRowHeight
		{
			get
			{
				return (short)((decimal)DefaultRowHeightInPoints * 20m);
			}
			set
			{
				DefaultRowHeightInPoints = (float)value / 20f;
			}
		}

		/// Get the default row height for the sheet measued in point size (if the rows do not define their own height).
		///
		/// @return  default row height in points
		public float DefaultRowHeightInPoints
		{
			get
			{
				CT_SheetFormatPr sheetFormatPr = worksheet.sheetFormatPr;
				return (float)((sheetFormatPr == null) ? 0.0 : sheetFormatPr.defaultRowHeight);
			}
			set
			{
				CT_SheetFormatPr sheetTypeSheetFormatPr = GetSheetTypeSheetFormatPr();
				sheetTypeSheetFormatPr.defaultRowHeight = (double)value;
				sheetTypeSheetFormatPr.customHeight = true;
			}
		}

		/// Whether the text is displayed in right-to-left mode in the window
		///
		/// @return whether the text is displayed in right-to-left mode in the window
		public bool RightToLeft
		{
			get
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				if (defaultSheetView != null)
				{
					return defaultSheetView.rightToLeft;
				}
				return false;
			}
			set
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				defaultSheetView.rightToLeft = value;
			}
		}

		/// Get whether to display the guts or not,
		/// default value is true
		///
		/// @return bool - guts or no guts
		public bool DisplayGuts
		{
			get
			{
				CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
				CT_OutlinePr cT_OutlinePr = (sheetTypeSheetPr.outlinePr == null) ? new CT_OutlinePr() : sheetTypeSheetPr.outlinePr;
				return cT_OutlinePr.showOutlineSymbols;
			}
			set
			{
				CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
				CT_OutlinePr cT_OutlinePr = (sheetTypeSheetPr.outlinePr == null) ? sheetTypeSheetPr.AddNewOutlinePr() : sheetTypeSheetPr.outlinePr;
				cT_OutlinePr.showOutlineSymbols = value;
			}
		}

		/// Gets the flag indicating whether the window should show 0 (zero) in cells Containing zero value.
		/// When false, cells with zero value appear blank instead of Showing the number zero.
		///
		/// @return whether all zero values on the worksheet are displayed
		public bool DisplayZeros
		{
			get
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				if (defaultSheetView != null)
				{
					return defaultSheetView.showZeros;
				}
				return true;
			}
			set
			{
				CT_SheetView sheetTypeSheetView = GetSheetTypeSheetView();
				sheetTypeSheetView.showZeros = value;
			}
		}

		/// Gets the first row on the sheet
		///
		/// @return the number of the first logical row on the sheet, zero based
		public int FirstRowNum
		{
			get
			{
				if (_rows.Count == 0)
				{
					return 0;
				}
				using (IEnumerator<int> enumerator = _rows.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		/// Flag indicating whether the Fit to Page print option is enabled.
		///
		/// @return <code>true</code>
		public bool FitToPage
		{
			get
			{
				CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
				CT_PageSetUpPr cT_PageSetUpPr = (sheetTypeSheetPr == null || !sheetTypeSheetPr.IsSetPageSetUpPr()) ? new CT_PageSetUpPr() : sheetTypeSheetPr.pageSetUpPr;
				return cT_PageSetUpPr.fitToPage;
			}
			set
			{
				GetSheetTypePageSetUpPr().fitToPage = value;
			}
		}

		/// Returns the default footer for the sheet,
		///  creating one as needed.
		/// You may also want to look at
		///  {@link #GetFirstFooter()},
		///  {@link #GetOddFooter()} and
		///  {@link #GetEvenFooter()}
		public IFooter Footer
		{
			get
			{
				return OddFooter;
			}
		}

		/// Returns the default header for the sheet,
		///  creating one as needed.
		/// You may also want to look at
		///  {@link #GetFirstHeader()},
		///  {@link #GetOddHeader()} and
		///  {@link #GetEvenHeader()}
		public IHeader Header
		{
			get
			{
				return OddHeader;
			}
		}

		/// Returns the odd footer. Used on all pages unless
		///  other footers also present, when used on only
		///  odd pages.
		public IFooter OddFooter
		{
			get
			{
				return new XSSFOddFooter(GetSheetTypeHeaderFooter());
			}
		}

		/// Returns the even footer. Not there by default, but
		///  when Set, used on even pages.
		public IFooter EvenFooter
		{
			get
			{
				return new XSSFEvenFooter(GetSheetTypeHeaderFooter());
			}
		}

		/// Returns the first page footer. Not there by
		///  default, but when Set, used on the first page.
		public IFooter FirstFooter
		{
			get
			{
				return new XSSFFirstFooter(GetSheetTypeHeaderFooter());
			}
		}

		/// Returns the odd header. Used on all pages unless
		///  other headers also present, when used on only
		///  odd pages.
		public IHeader OddHeader
		{
			get
			{
				return new XSSFOddHeader(GetSheetTypeHeaderFooter());
			}
		}

		/// Returns the even header. Not there by default, but
		///  when Set, used on even pages.
		public IHeader EvenHeader
		{
			get
			{
				return new XSSFEvenHeader(GetSheetTypeHeaderFooter());
			}
		}

		/// Returns the first page header. Not there by
		///  default, but when Set, used on the first page.
		public IHeader FirstHeader
		{
			get
			{
				return new XSSFFirstHeader(GetSheetTypeHeaderFooter());
			}
		}

		/// Determine whether printed output for this sheet will be horizontally centered.
		public bool HorizontallyCenter
		{
			get
			{
				CT_PrintOptions printOptions = worksheet.printOptions;
				if (printOptions != null)
				{
					return printOptions.horizontalCentered;
				}
				return false;
			}
			set
			{
				CT_PrintOptions cT_PrintOptions = worksheet.IsSetPrintOptions() ? worksheet.printOptions : worksheet.AddNewPrintOptions();
				cT_PrintOptions.horizontalCentered = value;
			}
		}

		public int LastRowNum
		{
			get
			{
				if (_rows.Count != 0)
				{
					return GetLastKey(_rows.Keys);
				}
				return 0;
			}
		}

		/// Returns the number of merged regions defined in this worksheet
		///
		/// @return number of merged regions in this worksheet
		public int NumMergedRegions
		{
			get
			{
				CT_MergeCells mergeCells = worksheet.mergeCells;
				if (mergeCells != null)
				{
					return mergeCells.sizeOfMergeCellArray();
				}
				return 0;
			}
		}

		public int NumHyperlinks
		{
			get
			{
				return hyperlinks.Count;
			}
		}

		/// Returns the information regarding the currently configured pane (split or freeze).
		///
		/// @return null if no pane configured, or the pane information.
		public PaneInformation PaneInformation
		{
			get
			{
				CT_Pane pane = GetDefaultSheetView().pane;
				if (pane == null)
				{
					return null;
				}
				CellReference cellReference = pane.IsSetTopLeftCell() ? new CellReference(pane.topLeftCell) : null;
				return new PaneInformation((short)pane.xSplit, (short)pane.ySplit, (short)((cellReference != null) ? ((short)cellReference.Row) : 0), (short)((cellReference != null) ? cellReference.Col : 0), (byte)pane.activePane, pane.state == ST_PaneState.frozen);
			}
		}

		/// Returns the number of phsyically defined rows (NOT the number of rows in the sheet)
		///
		/// @return the number of phsyically defined rows
		public int PhysicalNumberOfRows
		{
			get
			{
				return _rows.Count;
			}
		}

		/// Gets the print Setup object.
		///
		/// @return The user model for the print Setup object.
		public IPrintSetup PrintSetup
		{
			get
			{
				return new XSSFPrintSetup(worksheet);
			}
		}

		/// Answer whether protection is enabled or disabled
		///
		/// @return true =&gt; protection enabled; false =&gt; protection disabled
		public bool Protect
		{
			get
			{
				if (worksheet.IsSetSheetProtection())
				{
					return sheetProtectionEnabled();
				}
				return false;
			}
		}

		/// Horizontal page break information used for print layout view, page layout view, drawing print breaks in normal
		///  view, and for printing the worksheet.
		///
		/// @return row indexes of all the horizontal page breaks, never <code>null</code>
		public int[] RowBreaks
		{
			get
			{
				if (!worksheet.IsSetRowBreaks() || worksheet.rowBreaks.sizeOfBrkArray() == 0)
				{
					return new int[0];
				}
				List<CT_Break> brk = worksheet.rowBreaks.brk;
				int[] array = new int[brk.Count];
				for (int i = 0; i < brk.Count; i++)
				{
					CT_Break cT_Break = brk[i];
					array[i] = (int)(cT_Break.id - 1);
				}
				return array;
			}
		}

		/// Flag indicating whether summary rows appear below detail in an outline, when Applying an outline.
		///
		/// <p>
		/// When true a summary row is inserted below the detailed data being summarized and a
		/// new outline level is established on that row.
		/// </p>
		/// <p>
		/// When false a summary row is inserted above the detailed data being summarized and a new outline level
		/// is established on that row.
		/// </p>
		/// @return <code>true</code> if row summaries appear below detail in the outline
		public bool RowSumsBelow
		{
			get
			{
				CT_SheetPr sheetPr = worksheet.sheetPr;
				CT_OutlinePr cT_OutlinePr = (sheetPr != null && sheetPr.IsSetOutlinePr()) ? sheetPr.outlinePr : null;
				if (cT_OutlinePr != null)
				{
					return cT_OutlinePr.summaryBelow;
				}
				return true;
			}
			set
			{
				ensureOutlinePr().summaryBelow = value;
			}
		}

		/// Flag indicating whether summary columns appear to the right of detail in an outline, when Applying an outline.
		///
		/// <p>
		/// When true a summary column is inserted to the right of the detailed data being summarized
		/// and a new outline level is established on that column.
		/// </p>
		/// <p>
		/// When false a summary column is inserted to the left of the detailed data being
		/// summarized and a new outline level is established on that column.
		/// </p>
		/// @return <code>true</code> if col summaries appear right of the detail in the outline
		public bool RowSumsRight
		{
			get
			{
				CT_SheetPr sheetPr = worksheet.sheetPr;
				CT_OutlinePr cT_OutlinePr = (sheetPr != null && sheetPr.IsSetOutlinePr()) ? sheetPr.outlinePr : new CT_OutlinePr();
				return cT_OutlinePr.summaryRight;
			}
			set
			{
				ensureOutlinePr().summaryRight = value;
			}
		}

		/// <summary>
		/// A flag indicating whether scenarios are locked when the sheet is protected.
		/// </summary>
		public bool ScenarioProtect
		{
			get
			{
				if (worksheet.IsSetSheetProtection())
				{
					return worksheet.sheetProtection.scenarios;
				}
				return false;
			}
		}

		public short LeftCol
		{
			get
			{
				string topLeftCell = GetPane().topLeftCell;
				if (topLeftCell == null)
				{
					return 0;
				}
				CellReference cellReference = new CellReference(topLeftCell);
				return cellReference.Col;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// The top row in the visible view when the sheet is first viewed after opening it in a viewer
		/// </summary>
		public short TopRow
		{
			get
			{
				string topLeftCell = GetPane().topLeftCell;
				if (topLeftCell == null)
				{
					return 0;
				}
				CellReference cellReference = new CellReference(topLeftCell);
				return (short)cellReference.Row;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// Determine whether printed output for this sheet will be vertically centered.
		///
		/// @return whether printed output for this sheet will be vertically centered.
		public bool VerticallyCenter
		{
			get
			{
				CT_PrintOptions printOptions = worksheet.printOptions;
				if (printOptions != null)
				{
					return printOptions.verticalCentered;
				}
				return false;
			}
			set
			{
				CT_PrintOptions cT_PrintOptions = worksheet.IsSetPrintOptions() ? worksheet.printOptions : worksheet.AddNewPrintOptions();
				cT_PrintOptions.verticalCentered = value;
			}
		}

		/// Gets the flag indicating whether this sheet should display formulas.
		///
		/// @return <code>true</code> if this sheet should display formulas.
		public bool DisplayFormulas
		{
			get
			{
				return GetSheetTypeSheetView().showFormulas;
			}
			set
			{
				GetSheetTypeSheetView().showFormulas = value;
			}
		}

		/// Gets the flag indicating whether this sheet displays the lines
		/// between rows and columns to make editing and Reading easier.
		///
		/// @return <code>true</code> if this sheet displays gridlines.
		/// @see #isPrintGridlines() to check if printing of gridlines is turned on or off
		public bool DisplayGridlines
		{
			get
			{
				return GetSheetTypeSheetView().showGridLines;
			}
			set
			{
				GetSheetTypeSheetView().showGridLines = value;
			}
		}

		/// Gets the flag indicating whether this sheet should display row and column headings.
		/// <p>
		/// Row heading are the row numbers to the side of the sheet
		/// </p>
		/// <p>
		/// Column heading are the letters or numbers that appear above the columns of the sheet
		/// </p>
		///
		/// @return <code>true</code> if this sheet should display row and column headings.
		public bool DisplayRowColHeadings
		{
			get
			{
				return GetSheetTypeSheetView().showRowColHeaders;
			}
			set
			{
				GetSheetTypeSheetView().showRowColHeaders = value;
			}
		}

		/// Returns whether gridlines are printed.
		///
		/// @return whether gridlines are printed
		public bool IsPrintGridlines
		{
			get
			{
				CT_PrintOptions printOptions = worksheet.printOptions;
				if (printOptions != null)
				{
					return printOptions.gridLines;
				}
				return false;
			}
			set
			{
				CT_PrintOptions cT_PrintOptions = worksheet.IsSetPrintOptions() ? worksheet.printOptions : worksheet.AddNewPrintOptions();
				cT_PrintOptions.gridLines = value;
			}
		}

		/// Whether Excel will be asked to recalculate all formulas when the
		///  workbook is opened.  
		public bool ForceFormulaRecalculation
		{
			get
			{
				if (worksheet.IsSetSheetCalcPr())
				{
					CT_SheetCalcPr sheetCalcPr = worksheet.sheetCalcPr;
					return sheetCalcPr.fullCalcOnLoad;
				}
				return false;
			}
			set
			{
				CT_CalcPr calcPr = (Workbook as XSSFWorkbook).GetCTWorkbook().calcPr;
				if (worksheet.IsSetSheetCalcPr())
				{
					CT_SheetCalcPr sheetCalcPr = worksheet.sheetCalcPr;
					sheetCalcPr.fullCalcOnLoad = value;
				}
				else if (value)
				{
					CT_SheetCalcPr cT_SheetCalcPr = worksheet.AddNewSheetCalcPr();
					cT_SheetCalcPr.fullCalcOnLoad = value;
				}
				if (value && calcPr != null && calcPr.calcMode == ST_CalcMode.manual)
				{
					calcPr.calcMode = ST_CalcMode.auto;
				}
			}
		}

		/// Flag indicating whether the sheet displays Automatic Page Breaks.
		///
		/// @return <code>true</code> if the sheet displays Automatic Page Breaks.
		public bool Autobreaks
		{
			get
			{
				CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
				CT_PageSetUpPr cT_PageSetUpPr = (sheetTypeSheetPr == null || !sheetTypeSheetPr.IsSetPageSetUpPr()) ? new CT_PageSetUpPr() : sheetTypeSheetPr.pageSetUpPr;
				return cT_PageSetUpPr.autoPageBreaks;
			}
			set
			{
				CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
				CT_PageSetUpPr cT_PageSetUpPr = sheetTypeSheetPr.IsSetPageSetUpPr() ? sheetTypeSheetPr.pageSetUpPr : sheetTypeSheetPr.AddNewPageSetUpPr();
				cT_PageSetUpPr.autoPageBreaks = value;
			}
		}

		/// Returns a flag indicating whether this sheet is selected.
		/// <p>
		/// When only 1 sheet is selected and active, this value should be in synch with the activeTab value.
		/// In case of a conflict, the Start Part Setting wins and Sets the active sheet tab.
		/// </p>
		/// Note: multiple sheets can be selected, but only one sheet can be active at one time.
		///
		/// @return <code>true</code> if this sheet is selected
		public bool IsSelected
		{
			get
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				if (defaultSheetView != null)
				{
					return defaultSheetView.tabSelected;
				}
				return false;
			}
			set
			{
				CT_SheetViews sheetTypeSheetViews = GetSheetTypeSheetViews();
				foreach (CT_SheetView item in sheetTypeSheetViews.sheetView)
				{
					item.tabSelected = value;
				}
			}
		}

		/// Return location of the active cell, e.g. <code>A1</code>.
		///
		/// @return the location of the active cell.
		public string ActiveCell
		{
			get
			{
				return GetSheetTypeSelection().activeCell;
			}
		}

		/// Does this sheet have any comments on it? We need to know,
		///  so we can decide about writing it to disk or not
		public bool HasComments
		{
			get
			{
				if (sheetComments == null)
				{
					return false;
				}
				return sheetComments.GetNumberOfComments() > 0;
			}
		}

		internal int NumberOfComments
		{
			get
			{
				if (sheetComments == null)
				{
					return 0;
				}
				return sheetComments.GetNumberOfComments();
			}
		}

		/// @return true when Autofilters are locked and the sheet is protected.
		public bool IsAutoFilterLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.autoFilter;
				}
				return false;
			}
		}

		/// @return true when Deleting columns is locked and the sheet is protected.
		public bool IsDeleteColumnsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.deleteColumns;
				}
				return false;
			}
		}

		/// @return true when Deleting rows is locked and the sheet is protected.
		public bool IsDeleteRowsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.deleteRows;
				}
				return false;
			}
		}

		/// @return true when Formatting cells is locked and the sheet is protected.
		public bool IsFormatCellsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.formatCells;
				}
				return false;
			}
		}

		/// @return true when Formatting columns is locked and the sheet is protected.
		public bool IsFormatColumnsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.formatColumns;
				}
				return false;
			}
		}

		/// @return true when Formatting rows is locked and the sheet is protected.
		public bool IsFormatRowsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.formatRows;
				}
				return false;
			}
		}

		/// @return true when Inserting columns is locked and the sheet is protected.
		public bool IsInsertColumnsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.insertColumns;
				}
				return false;
			}
		}

		/// @return true when Inserting hyperlinks is locked and the sheet is protected.
		public bool IsInsertHyperlinksLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.insertHyperlinks;
				}
				return false;
			}
		}

		/// @return true when Inserting rows is locked and the sheet is protected.
		public bool IsInsertRowsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.insertRows;
				}
				return false;
			}
		}

		/// @return true when Pivot tables are locked and the sheet is protected.
		public bool IsPivotTablesLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.pivotTables;
				}
				return false;
			}
		}

		/// @return true when Sorting is locked and the sheet is protected.
		public bool IsSortLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.sort;
				}
				return false;
			}
		}

		/// @return true when Objects are locked and the sheet is protected.
		public bool IsObjectsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.objects;
				}
				return false;
			}
		}

		/// @return true when Scenarios are locked and the sheet is protected.
		public bool IsScenariosLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.scenarios;
				}
				return false;
			}
		}

		/// @return true when Selection of locked cells is locked and the sheet is protected.
		public bool IsSelectLockedCellsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.selectLockedCells;
				}
				return false;
			}
		}

		/// @return true when Selection of unlocked cells is locked and the sheet is protected.
		public bool IsSelectUnlockedCellsLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.selectUnlockedCells;
				}
				return false;
			}
		}

		/// @return true when Sheet is Protected.
		public bool IsSheetLocked
		{
			get
			{
				CreateProtectionFieldIfNotPresent();
				if (sheetProtectionEnabled())
				{
					return worksheet.sheetProtection.sheet;
				}
				return false;
			}
		}

		public ISheetConditionalFormatting SheetConditionalFormatting
		{
			get
			{
				return new XSSFSheetConditionalFormatting(this);
			}
		}

		public IDrawing DrawingPatriarch
		{
			get
			{
				if (drawing == null)
				{
					CT_Drawing cTDrawing = GetCTDrawing();
					if (cTDrawing == null)
					{
						return null;
					}
					foreach (POIXMLDocumentPart relation in GetRelations())
					{
						if (relation is XSSFDrawing)
						{
							XSSFDrawing xSSFDrawing = (XSSFDrawing)relation;
							string id = xSSFDrawing.GetPackageRelationship().Id;
							if (id.Equals(cTDrawing.id))
							{
								drawing = xSSFDrawing;
							}
							break;
						}
					}
				}
				return drawing;
			}
		}

		public bool IsActive
		{
			get
			{
				return IsSelected;
			}
			set
			{
				IsSelected = value;
			}
		}

		public short TabColorIndex
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public bool IsRightToLeft
		{
			get
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				if (defaultSheetView != null)
				{
					return defaultSheetView.rightToLeft;
				}
				return false;
			}
			set
			{
				CT_SheetView defaultSheetView = GetDefaultSheetView();
				defaultSheetView.rightToLeft = value;
			}
		}

		public CellRangeAddress RepeatingRows
		{
			get
			{
				return GetRepeatingRowsOrColums(true);
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
				return GetRepeatingRowsOrColums(false);
			}
			set
			{
				CellRangeAddress repeatingRows = RepeatingRows;
				SetRepeatingRowsAndColumns(repeatingRows, value);
			}
		}

		private CT_Pane Pane
		{
			get
			{
				if (GetDefaultSheetView().pane == null)
				{
					GetDefaultSheetView().AddNewPane();
				}
				return GetDefaultSheetView().pane;
			}
		}

		/// Creates new XSSFSheet   - called by XSSFWorkbook to create a sheet from scratch.
		///
		/// @see NPOI.XSSF.usermodel.XSSFWorkbook#CreateSheet()
		public XSSFSheet()
		{
			dataValidationHelper = new XSSFDataValidationHelper(this);
			OnDocumentCreate();
		}

		/// Creates an XSSFSheet representing the given namespace part and relationship.
		/// Should only be called by XSSFWorkbook when Reading in an exisiting file.
		///
		/// @param part - The namespace part that holds xml data represenring this sheet.
		/// @param rel - the relationship of the given namespace part in the underlying OPC namespace
		internal XSSFSheet(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			dataValidationHelper = new XSSFDataValidationHelper(this);
		}

		/// Initialize worksheet data when Reading in an exisiting file.
		internal override void OnDocumentRead()
		{
			try
			{
				Read(GetPackagePart().GetInputStream());
			}
			catch (IOException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		internal virtual void Read(Stream is1)
		{
			try
			{
				XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(is1);
				worksheet = WorksheetDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager).GetWorksheet();
			}
			catch (XmlException ex)
			{
				throw new POIXMLException(ex);
			}
			InitRows(worksheet);
			columnHelper = new ColumnHelper(worksheet);
			foreach (POIXMLDocumentPart relation in GetRelations())
			{
				if (relation is CommentsTable)
				{
					sheetComments = (CommentsTable)relation;
				}
				if (relation is XSSFTable)
				{
					tables[relation.GetPackageRelationship().Id] = (XSSFTable)relation;
				}
			}
			InitHyperlinks();
		}

		/// Initialize worksheet data when creating a new sheet.
		internal override void OnDocumentCreate()
		{
			worksheet = NewSheet();
			InitRows(worksheet);
			columnHelper = new ColumnHelper(worksheet);
			hyperlinks = new List<XSSFHyperlink>();
		}

		private void InitRows(CT_Worksheet worksheet)
		{
			_rows = new SortedList<int, XSSFRow>();
			tables = new Dictionary<string, XSSFTable>();
			sharedFormulas = new Dictionary<int, CT_CellFormula>();
			arrayFormulas = new List<CellRangeAddress>();
			if (0 < worksheet.sheetData.SizeOfRowArray())
			{
				foreach (CT_Row item in worksheet.sheetData.row)
				{
					XSSFRow xSSFRow = new XSSFRow(item, this);
					if (!_rows.ContainsKey(xSSFRow.RowNum))
					{
						_rows.Add(xSSFRow.RowNum, xSSFRow);
					}
				}
			}
		}

		/// Read hyperlink relations, link them with CT_Hyperlink beans in this worksheet
		/// and Initialize the internal array of XSSFHyperlink objects
		private void InitHyperlinks()
		{
			hyperlinks = new List<XSSFHyperlink>();
			if (worksheet.IsSetHyperlinks())
			{
				try
				{
					PackageRelationshipCollection relationshipsByType = GetPackagePart().GetRelationshipsByType(XSSFRelation.SHEET_HYPERLINKS.Relation);
					foreach (CT_Hyperlink item in worksheet.hyperlinks.hyperlink)
					{
						PackageRelationship packageRelationship = null;
						if (item.id != null)
						{
							packageRelationship = relationshipsByType.GetRelationshipByID(item.id);
						}
						if (packageRelationship != null)
						{
							hyperlinks.Add(new XSSFHyperlink(item, packageRelationship));
						}
					}
				}
				catch (InvalidFormatException ex)
				{
					throw new POIXMLException(ex);
				}
			}
		}

		/// Create a new CT_Worksheet instance with all values set to defaults
		///
		/// @return a new instance
		private static CT_Worksheet NewSheet()
		{
			CT_Worksheet cT_Worksheet = new CT_Worksheet();
			CT_SheetFormatPr cT_SheetFormatPr = cT_Worksheet.AddNewSheetFormatPr();
			cT_SheetFormatPr.defaultRowHeight = 15.0;
			CT_SheetView cT_SheetView = cT_Worksheet.AddNewSheetViews().AddNewSheetView();
			cT_SheetView.workbookViewId = 0u;
			cT_Worksheet.AddNewDimension().@ref = "A1";
			cT_Worksheet.AddNewSheetData();
			CT_PageMargins cT_PageMargins = cT_Worksheet.AddNewPageMargins();
			cT_PageMargins.bottom = 0.75;
			cT_PageMargins.footer = 0.3;
			cT_PageMargins.header = 0.3;
			cT_PageMargins.left = 0.7;
			cT_PageMargins.right = 0.7;
			cT_PageMargins.top = 0.75;
			return cT_Worksheet;
		}

		/// Provide access to the CT_Worksheet bean holding this sheet's data
		///
		/// @return the CT_Worksheet bean holding this sheet's data
		internal CT_Worksheet GetCTWorksheet()
		{
			return worksheet;
		}

		public ColumnHelper GetColumnHelper()
		{
			return columnHelper;
		}

		/// Adds a merged region of cells (hence those cells form one).
		///
		/// @param region (rowfrom/colfrom-rowto/colto) to merge
		/// @return index of this region
		public int AddMergedRegion(CellRangeAddress region)
		{
			region.Validate(SpreadsheetVersion.EXCEL2007);
			ValidateArrayFormulas(region);
			CT_MergeCells cT_MergeCells = worksheet.IsSetMergeCells() ? worksheet.mergeCells : worksheet.AddNewMergeCells();
			CT_MergeCell cT_MergeCell = cT_MergeCells.AddNewMergeCell();
			cT_MergeCell.@ref = region.FormatAsString();
			return cT_MergeCells.sizeOfMergeCellArray();
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
					IRow row = GetRow(i);
					if (row != null)
					{
						ICell cell = row.GetCell(j);
						if (cell != null && cell.IsPartOfArrayFormulaGroup)
						{
							CellRangeAddress arrayFormulaRange = cell.ArrayFormulaRange;
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

		/// Adjusts the column width to fit the contents.
		///
		/// This process can be relatively slow on large sheets, so this should
		///  normally only be called once per column, at the end of your
		///  Processing.
		///
		/// @param column the column index
		public void AutoSizeColumn(int column)
		{
			AutoSizeColumn(column, false);
		}

		/// Adjusts the column width to fit the contents.
		/// <p>
		/// This process can be relatively slow on large sheets, so this should
		///  normally only be called once per column, at the end of your
		///  Processing.
		/// </p>
		/// You can specify whether the content of merged cells should be considered or ignored.
		///  Default is to ignore merged cells.
		///
		/// @param column the column index
		/// @param useMergedCells whether to use the contents of merged cells when calculating the width of the column
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
				columnHelper.SetColBestFit(column, true);
			}
		}

		/// Create a new SpreadsheetML drawing. If this sheet already Contains a drawing - return that.
		///
		/// @return a SpreadsheetML drawing
		public IDrawing CreateDrawingPatriarch()
		{
			CT_Drawing cTDrawing = GetCTDrawing();
			if (cTDrawing == null)
			{
				int idx = GetPackagePart().Package.GetPartsByContentType(XSSFRelation.DRAWINGS.ContentType).Count + 1;
				drawing = (XSSFDrawing)CreateRelationship(XSSFRelation.DRAWINGS, XSSFFactory.GetInstance(), idx);
				string id = drawing.GetPackageRelationship().Id;
				cTDrawing = worksheet.AddNewDrawing();
				cTDrawing.id = id;
			}
			else
			{
				foreach (POIXMLDocumentPart relation in GetRelations())
				{
					if (relation is XSSFDrawing)
					{
						XSSFDrawing xSSFDrawing = (XSSFDrawing)relation;
						string id2 = xSSFDrawing.GetPackageRelationship().Id;
						if (id2.Equals(cTDrawing.id))
						{
							drawing = xSSFDrawing;
						}
						break;
					}
				}
				if (drawing == null)
				{
					logger.Log(7, "Can't find drawing with id=" + cTDrawing.id + " in the list of the sheet's relationships");
				}
			}
			return drawing;
		}

		/// Get VML drawing for this sheet (aka 'legacy' drawig)
		///
		/// @param autoCreate if true, then a new VML drawing part is Created
		///
		/// @return the VML drawing of <code>null</code> if the drawing was not found and autoCreate=false
		internal XSSFVMLDrawing GetVMLDrawing(bool autoCreate)
		{
			XSSFVMLDrawing xSSFVMLDrawing = null;
			CT_LegacyDrawing cTLegacyDrawing = GetCTLegacyDrawing();
			if (cTLegacyDrawing == null)
			{
				if (autoCreate)
				{
					int idx = GetPackagePart().Package.GetPartsByContentType(XSSFRelation.VML_DRAWINGS.ContentType).Count + 1;
					xSSFVMLDrawing = (XSSFVMLDrawing)CreateRelationship(XSSFRelation.VML_DRAWINGS, XSSFFactory.GetInstance(), idx);
					string id = xSSFVMLDrawing.GetPackageRelationship().Id;
					cTLegacyDrawing = worksheet.AddNewLegacyDrawing();
					cTLegacyDrawing.id = id;
				}
			}
			else
			{
				foreach (POIXMLDocumentPart relation in GetRelations())
				{
					if (relation is XSSFVMLDrawing)
					{
						XSSFVMLDrawing xSSFVMLDrawing2 = (XSSFVMLDrawing)relation;
						string id2 = xSSFVMLDrawing2.GetPackageRelationship().Id;
						if (id2.Equals(cTLegacyDrawing.id))
						{
							xSSFVMLDrawing = xSSFVMLDrawing2;
						}
						break;
					}
				}
				if (xSSFVMLDrawing == null)
				{
					logger.Log(7, "Can't find VML drawing with id=" + cTLegacyDrawing.id + " in the list of the sheet's relationships");
				}
			}
			return xSSFVMLDrawing;
		}

		protected virtual CT_Drawing GetCTDrawing()
		{
			return worksheet.drawing;
		}

		protected virtual CT_LegacyDrawing GetCTLegacyDrawing()
		{
			return worksheet.legacyDrawing;
		}

		/// Creates a split (freezepane). Any existing freezepane or split pane is overwritten.
		/// @param colSplit      Horizonatal position of split.
		/// @param rowSplit      Vertical position of split.
		public void CreateFreezePane(int colSplit, int rowSplit)
		{
			CreateFreezePane(colSplit, rowSplit, colSplit, rowSplit);
		}

		/// Creates a split (freezepane). Any existing freezepane or split pane is overwritten.
		///
		/// <p>
		///     If both colSplit and rowSplit are zero then the existing freeze pane is Removed
		/// </p>
		///
		/// @param colSplit      Horizonatal position of split.
		/// @param rowSplit      Vertical position of split.
		/// @param leftmostColumn   Left column visible in right pane.
		/// @param topRow        Top row visible in bottom pane
		public void CreateFreezePane(int colSplit, int rowSplit, int leftmostColumn, int topRow)
		{
			CT_SheetView defaultSheetView = GetDefaultSheetView();
			if (colSplit == 0 && rowSplit == 0)
			{
				if (defaultSheetView.IsSetPane())
				{
					defaultSheetView.UnsetPane();
				}
				defaultSheetView.SetSelectionArray(null);
			}
			else
			{
				if (!defaultSheetView.IsSetPane())
				{
					defaultSheetView.AddNewPane();
				}
				CT_Pane pane = defaultSheetView.pane;
				if (colSplit > 0)
				{
					pane.xSplit = (double)colSplit;
				}
				else if (pane.IsSetXSplit())
				{
					pane.UnsetXSplit();
				}
				if (rowSplit > 0)
				{
					pane.ySplit = (double)rowSplit;
				}
				else if (pane.IsSetYSplit())
				{
					pane.UnsetYSplit();
				}
				pane.state = ST_PaneState.frozen;
				if (rowSplit == 0)
				{
					pane.topLeftCell = new CellReference(0, leftmostColumn).FormatAsString();
					pane.activePane = ST_Pane.topRight;
				}
				else if (colSplit == 0)
				{
					pane.topLeftCell = new CellReference(topRow, 0).FormatAsString();
					pane.activePane = ST_Pane.bottomLeft;
				}
				else
				{
					pane.topLeftCell = new CellReference(topRow, leftmostColumn).FormatAsString();
					pane.activePane = ST_Pane.bottomRight;
				}
				defaultSheetView.selection = null;
				CT_Selection cT_Selection = defaultSheetView.AddNewSelection();
				cT_Selection.pane = pane.activePane;
			}
		}

		/// Creates a new comment for this sheet. You still
		///  need to assign it to a cell though
		///
		/// @deprecated since Nov 2009 this method is not compatible with the common SS interfaces,
		/// use {@link NPOI.XSSF.usermodel.XSSFDrawing#CreateCellComment
		///  (NPOI.SS.usermodel.ClientAnchor)} instead
		public IComment CreateComment()
		{
			return CreateDrawingPatriarch().CreateCellComment(new XSSFClientAnchor());
		}

		private int GetLastKey(IList<int> keys)
		{
			int count = keys.Count;
			return keys[keys.Count - 1];
		}

		private SortedList<int, XSSFRow> HeadMap(SortedList<int, XSSFRow> rows, int rownum)
		{
			SortedList<int, XSSFRow> sortedList = new SortedList<int, XSSFRow>();
			foreach (int key in rows.Keys)
			{
				if (key < rownum)
				{
					sortedList.Add(key, rows[key]);
				}
			}
			return sortedList;
		}

		/// Create a new row within the sheet and return the high level representation
		///
		/// @param rownum  row number
		/// @return High level {@link XSSFRow} object representing a row in the sheet
		/// @see #RemoveRow(NPOI.SS.usermodel.Row)
		public virtual IRow CreateRow(int rownum)
		{
			XSSFRow xSSFRow = _rows.ContainsKey(rownum) ? _rows[rownum] : null;
			CT_Row cT_Row;
			if (xSSFRow != null)
			{
				cT_Row = xSSFRow.GetCTRow();
				cT_Row.Set(new CT_Row());
			}
			else if (_rows.Count == 0 || rownum > GetLastKey(_rows.Keys))
			{
				cT_Row = worksheet.sheetData.AddNewRow();
			}
			else
			{
				int count = HeadMap(_rows, rownum).Count;
				cT_Row = worksheet.sheetData.InsertNewRow(count);
			}
			XSSFRow xSSFRow2 = new XSSFRow(cT_Row, this);
			xSSFRow2.RowNum = rownum;
			_rows[rownum] = xSSFRow2;
			return xSSFRow2;
		}

		/// Creates a split pane. Any existing freezepane or split pane is overwritten.
		/// @param xSplitPos      Horizonatal position of split (in 1/20th of a point).
		/// @param ySplitPos      Vertical position of split (in 1/20th of a point).
		/// @param topRow        Top row visible in bottom pane
		/// @param leftmostColumn   Left column visible in right pane.
		/// @param activePane    Active pane.  One of: PANE_LOWER_RIGHT,
		///                      PANE_UPPER_RIGHT, PANE_LOWER_LEFT, PANE_UPPER_LEFT
		/// @see NPOI.SS.usermodel.Sheet#PANE_LOWER_LEFT
		/// @see NPOI.SS.usermodel.Sheet#PANE_LOWER_RIGHT
		/// @see NPOI.SS.usermodel.Sheet#PANE_UPPER_LEFT
		/// @see NPOI.SS.usermodel.Sheet#PANE_UPPER_RIGHT
		public void CreateSplitPane(int xSplitPos, int ySplitPos, int leftmostColumn, int topRow, PanePosition activePane)
		{
			CreateFreezePane(xSplitPos, ySplitPos, leftmostColumn, topRow);
			GetPane().state = ST_PaneState.split;
			GetPane().activePane = (ST_Pane)activePane;
		}

		public IComment GetCellComment(int row, int column)
		{
			if (sheetComments == null)
			{
				return null;
			}
			string cellRef = new CellReference(row, column).FormatAsString();
			CT_Comment cTComment = sheetComments.GetCTComment(cellRef);
			if (cTComment == null)
			{
				return null;
			}
			XSSFVMLDrawing vMLDrawing = GetVMLDrawing(false);
			return new XSSFComment(sheetComments, cTComment, (vMLDrawing == null) ? null : vMLDrawing.FindCommentShape(row, column));
		}

		public XSSFHyperlink GetHyperlink(int row, int column)
		{
			string value = new CellReference(row, column).FormatAsString();
			foreach (XSSFHyperlink hyperlink in hyperlinks)
			{
				if (hyperlink.GetCellRef().Equals(value))
				{
					return hyperlink;
				}
			}
			return null;
		}

		/// Get the actual column width (in units of 1/256th of a character width )
		///
		/// <p>
		/// Note, the returned  value is always gerater that {@link #GetDefaultColumnWidth()} because the latter does not include margins.
		/// Actual column width measured as the number of characters of the maximum digit width of the
		/// numbers 0, 1, 2, ..., 9 as rendered in the normal style's font. There are 4 pixels of margin
		/// pAdding (two on each side), plus 1 pixel pAdding for the gridlines.
		/// </p>
		///
		/// @param columnIndex - the column to set (0-based)
		/// @return width - the width in units of 1/256th of a character width
		public int GetColumnWidth(int columnIndex)
		{
			CT_Col column = columnHelper.GetColumn(columnIndex, false);
			double num = (column == null || !column.IsSetWidth()) ? ((double)DefaultColumnWidth) : column.width;
			return (int)(num * 256.0);
		}

		private CT_SheetFormatPr GetSheetTypeSheetFormatPr()
		{
			if (!worksheet.IsSetSheetFormatPr())
			{
				return worksheet.AddNewSheetFormatPr();
			}
			return worksheet.sheetFormatPr;
		}

		/// Returns the CellStyle that applies to the given
		///  (0 based) column, or null if no style has been
		///  set for that column
		public ICellStyle GetColumnStyle(int column)
		{
			int colDefaultStyle = columnHelper.GetColDefaultStyle(column);
			return Workbook.GetCellStyleAt((short)((colDefaultStyle != -1) ? colDefaultStyle : 0));
		}

		private CT_SheetPr GetSheetTypeSheetPr()
		{
			if (worksheet.sheetPr == null)
			{
				worksheet.sheetPr = new CT_SheetPr();
			}
			return worksheet.sheetPr;
		}

		private CT_HeaderFooter GetSheetTypeHeaderFooter()
		{
			if (worksheet.headerFooter == null)
			{
				worksheet.headerFooter = new CT_HeaderFooter();
			}
			return worksheet.headerFooter;
		}

		/// Gets the size of the margin in inches.
		///
		/// @param margin which margin to get
		/// @return the size of the margin
		/// @see Sheet#LeftMargin
		/// @see Sheet#RightMargin
		/// @see Sheet#TopMargin
		/// @see Sheet#BottomMargin
		/// @see Sheet#HeaderMargin
		/// @see Sheet#FooterMargin
		public double GetMargin(MarginType margin)
		{
			if (worksheet.IsSetPageMargins())
			{
				CT_PageMargins pageMargins = worksheet.pageMargins;
				switch (margin)
				{
				case MarginType.LeftMargin:
					return pageMargins.left;
				case MarginType.RightMargin:
					return pageMargins.right;
				case MarginType.TopMargin:
					return pageMargins.top;
				case MarginType.BottomMargin:
					return pageMargins.bottom;
				case MarginType.HeaderMargin:
					return pageMargins.header;
				case MarginType.FooterMargin:
					return pageMargins.footer;
				default:
					throw new ArgumentException("Unknown margin constant:  " + margin);
				}
			}
			return 0.0;
		}

		/// Sets the size of the margin in inches.
		///
		/// @param margin which margin to get
		/// @param size the size of the margin
		/// @see Sheet#LeftMargin
		/// @see Sheet#RightMargin
		/// @see Sheet#TopMargin
		/// @see Sheet#BottomMargin
		/// @see Sheet#HeaderMargin
		/// @see Sheet#FooterMargin
		public void SetMargin(MarginType margin, double size)
		{
			CT_PageMargins cT_PageMargins = worksheet.IsSetPageMargins() ? worksheet.pageMargins : worksheet.AddNewPageMargins();
			switch (margin)
			{
			case MarginType.LeftMargin:
				cT_PageMargins.left = size;
				break;
			case MarginType.RightMargin:
				cT_PageMargins.right = size;
				break;
			case MarginType.TopMargin:
				cT_PageMargins.top = size;
				break;
			case MarginType.BottomMargin:
				cT_PageMargins.bottom = size;
				break;
			case MarginType.HeaderMargin:
				cT_PageMargins.header = size;
				break;
			case MarginType.FooterMargin:
				cT_PageMargins.footer = size;
				break;
			default:
				throw new InvalidOperationException("Unknown margin constant:  " + margin);
			}
		}

		/// @return the merged region at the specified index
		/// @throws InvalidOperationException if this worksheet does not contain merged regions
		public CellRangeAddress GetMergedRegion(int index)
		{
			CT_MergeCells mergeCells = worksheet.mergeCells;
			if (mergeCells == null)
			{
				throw new InvalidOperationException("This worksheet does not contain merged regions");
			}
			CT_MergeCell mergeCellArray = mergeCells.GetMergeCellArray(index);
			string @ref = mergeCellArray.@ref;
			return CellRangeAddress.ValueOf(@ref);
		}

		/// Enables sheet protection and Sets the password for the sheet.
		/// Also Sets some attributes on the {@link CT_SheetProtection} that correspond to
		/// the default values used by Excel
		///
		/// @param password to set for protection. Pass <code>null</code> to remove protection
		public void ProtectSheet(string password)
		{
			if (password != null)
			{
				CT_SheetProtection cT_SheetProtection = worksheet.AddNewSheetProtection();
				cT_SheetProtection.password = StringToExcelPassword(password);
				cT_SheetProtection.sheet = true;
				cT_SheetProtection.scenarios = true;
				cT_SheetProtection.objects = true;
			}
			else
			{
				worksheet.UnsetSheetProtection();
			}
		}

		/// Converts a String to a {@link STUnsignedshortHex} value that Contains the {@link PasswordRecord#hashPassword(String)}
		/// value in hexadecimal format
		///
		/// @param password the password string you wish convert to an {@link STUnsignedshortHex}
		/// @return {@link STUnsignedshortHex} that Contains Excel hashed password in Hex format
		private string StringToExcelPassword(string password)
		{
			return PasswordRecord.HashPassword(password).ToString("x");
		}

		/// Returns the logical row ( 0-based).  If you ask for a row that is not
		/// defined you get a null.  This is to say row 4 represents the fifth row on a sheet.
		///
		/// @param rownum  row to get
		/// @return <code>XSSFRow</code> representing the rownumber or <code>null</code> if its not defined on the sheet
		public IRow GetRow(int rownum)
		{
			if (_rows.ContainsKey(rownum))
			{
				return _rows[rownum];
			}
			return null;
		}

		/// Ensure CT_Worksheet.CT_SheetPr.CT_OutlinePr
		private CT_OutlinePr ensureOutlinePr()
		{
			CT_SheetPr cT_SheetPr = worksheet.IsSetSheetPr() ? worksheet.sheetPr : worksheet.AddNewSheetPr();
			if (!cT_SheetPr.IsSetOutlinePr())
			{
				return cT_SheetPr.AddNewOutlinePr();
			}
			return cT_SheetPr.outlinePr;
		}

		/// Group between (0 based) columns
		public void GroupColumn(int fromColumn, int toColumn)
		{
			GroupColumn1Based(fromColumn + 1, toColumn + 1);
		}

		private void GroupColumn1Based(int fromColumn, int toColumn)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col cT_Col = new CT_Col();
			cT_Col.min = (uint)fromColumn;
			cT_Col.max = (uint)toColumn;
			columnHelper.AddCleanColIntoCols(colsArray, cT_Col);
			int num;
			for (num = fromColumn; num <= toColumn; num++)
			{
				CT_Col column1Based = columnHelper.GetColumn1Based(num, false);
				column1Based.outlineLevel++;
				num = (int)column1Based.max;
			}
			worksheet.SetColsArray(0, colsArray);
			SetSheetFormatPrOutlineLevelCol();
		}

		/// Tie a range of cell toGether so that they can be collapsed or expanded
		///
		/// @param fromRow   start row (0-based)
		/// @param toRow     end row (0-based)
		public void GroupRow(int fromRow, int toRow)
		{
			for (int i = fromRow; i <= toRow; i++)
			{
				XSSFRow xSSFRow = (XSSFRow)GetRow(i);
				if (xSSFRow == null)
				{
					xSSFRow = (XSSFRow)CreateRow(i);
				}
				CT_Row cTRow = xSSFRow.GetCTRow();
				cTRow.outlineLevel++;
			}
			SetSheetFormatPrOutlineLevelRow();
		}

		private short GetMaxOutlineLevelRows()
		{
			short num = 0;
			foreach (XSSFRow value in _rows.Values)
			{
				num = ((value.GetCTRow().outlineLevel > num) ? value.GetCTRow().outlineLevel : num);
			}
			return num;
		}

		private short GetMaxOutlineLevelCols()
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			short num = 0;
			foreach (CT_Col item in colsArray.col)
			{
				num = ((item.outlineLevel > num) ? item.outlineLevel : num);
			}
			return num;
		}

		/// Determines if there is a page break at the indicated column
		public bool IsColumnBroken(int column)
		{
			int[] columnBreaks = ColumnBreaks;
			for (int i = 0; i < columnBreaks.Length; i++)
			{
				if (columnBreaks[i] == column)
				{
					return true;
				}
			}
			return false;
		}

		/// Get the hidden state for a given column.
		///
		/// @param columnIndex - the column to set (0-based)
		/// @return hidden - <code>false</code> if the column is visible
		public bool IsColumnHidden(int columnIndex)
		{
			CT_Col column = columnHelper.GetColumn(columnIndex, false);
			if (column != null)
			{
				return column.hidden;
			}
			return false;
		}

		/// Tests if there is a page break at the indicated row
		///
		/// @param row index of the row to test
		/// @return <code>true</code> if there is a page break at the indicated row
		public bool IsRowBroken(int row)
		{
			int[] rowBreaks = RowBreaks;
			for (int i = 0; i < rowBreaks.Length; i++)
			{
				if (rowBreaks[i] == row)
				{
					return true;
				}
			}
			return false;
		}

		/// Sets a page break at the indicated row
		/// Breaks occur above the specified row and left of the specified column inclusive.
		///
		/// For example, <code>sheet.SetColumnBreak(2);</code> breaks the sheet into two parts
		/// with columns A,B,C in the first and D,E,... in the second. Simuilar, <code>sheet.SetRowBreak(2);</code>
		/// breaks the sheet into two parts with first three rows (rownum=1...3) in the first part
		/// and rows starting with rownum=4 in the second.
		///
		/// @param row the row to break, inclusive
		public void SetRowBreak(int row)
		{
			CT_PageBreak cT_PageBreak = worksheet.IsSetRowBreaks() ? worksheet.rowBreaks : worksheet.AddNewRowBreaks();
			if (!IsRowBroken(row))
			{
				CT_Break cT_Break = cT_PageBreak.AddNewBrk();
				cT_Break.id = (uint)(row + 1);
				cT_Break.man = true;
				cT_Break.max = (uint)SpreadsheetVersion.EXCEL2007.LastColumnIndex;
				cT_PageBreak.count = (uint)cT_PageBreak.sizeOfBrkArray();
				cT_PageBreak.manualBreakCount = (uint)cT_PageBreak.sizeOfBrkArray();
			}
		}

		/// Removes a page break at the indicated column
		public void RemoveColumnBreak(int column)
		{
			if (worksheet.IsSetColBreaks())
			{
				CT_PageBreak colBreaks = worksheet.colBreaks;
				List<CT_Break> brk = colBreaks.brk;
				for (int i = 0; i < brk.Count; i++)
				{
					if (brk[i].id == column + 1)
					{
						colBreaks.RemoveBrk(i);
					}
				}
			}
		}

		/// Removes a merged region of cells (hence letting them free)
		///
		/// @param index of the region to unmerge
		public void RemoveMergedRegion(int index)
		{
			CT_MergeCells mergeCells = worksheet.mergeCells;
			CT_MergeCell[] array = new CT_MergeCell[mergeCells.sizeOfMergeCellArray() - 1];
			for (int i = 0; i < mergeCells.sizeOfMergeCellArray(); i++)
			{
				if (i < index)
				{
					array[i] = mergeCells.GetMergeCellArray(i);
				}
				else if (i > index)
				{
					array[i - 1] = mergeCells.GetMergeCellArray(i);
				}
			}
			if (array.Length > 0)
			{
				mergeCells.SetMergeCellArray(array);
			}
			else
			{
				worksheet.UnsetMergeCells();
			}
		}

		/// Remove a row from this sheet.  All cells Contained in the row are Removed as well
		///
		/// @param row  the row to Remove.
		public void RemoveRow(IRow row)
		{
			if (row.Sheet != this)
			{
				throw new ArgumentException("Specified row does not belong to this sheet");
			}
			List<XSSFCell> list = new List<XSSFCell>();
			foreach (ICell item in row)
			{
				list.Add((XSSFCell)item);
			}
			foreach (XSSFCell item2 in list)
			{
				row.RemoveCell(item2);
			}
			int count = HeadMap(_rows, row.RowNum).Count;
			_rows.Remove(row.RowNum);
			worksheet.sheetData.RemoveRow(count);
		}

		/// Removes the page break at the indicated row
		public void RemoveRowBreak(int row)
		{
			if (worksheet.IsSetRowBreaks())
			{
				CT_PageBreak rowBreaks = worksheet.rowBreaks;
				List<CT_Break> brk = rowBreaks.brk;
				for (int i = 0; i < brk.Count; i++)
				{
					if (brk[i].id == row + 1)
					{
						rowBreaks.RemoveBrk(i);
					}
				}
			}
		}

		/// Sets a page break at the indicated column.
		/// Breaks occur above the specified row and left of the specified column inclusive.
		///
		/// For example, <code>sheet.SetColumnBreak(2);</code> breaks the sheet into two parts
		/// with columns A,B,C in the first and D,E,... in the second. Simuilar, <code>sheet.SetRowBreak(2);</code>
		/// breaks the sheet into two parts with first three rows (rownum=1...3) in the first part
		/// and rows starting with rownum=4 in the second.
		///
		/// @param column the column to break, inclusive
		public void SetColumnBreak(int column)
		{
			if (!IsColumnBroken(column))
			{
				CT_PageBreak cT_PageBreak = worksheet.IsSetColBreaks() ? worksheet.colBreaks : worksheet.AddNewColBreaks();
				CT_Break cT_Break = cT_PageBreak.AddNewBrk();
				cT_Break.id = (uint)(column + 1);
				cT_Break.man = true;
				cT_Break.max = (uint)SpreadsheetVersion.EXCEL2007.LastRowIndex;
				cT_PageBreak.count = (uint)cT_PageBreak.sizeOfBrkArray();
				cT_PageBreak.manualBreakCount = (uint)cT_PageBreak.sizeOfBrkArray();
			}
		}

		public void SetColumnGroupCollapsed(int columnNumber, bool collapsed)
		{
			if (collapsed)
			{
				CollapseColumn(columnNumber);
			}
			else
			{
				ExpandColumn(columnNumber);
			}
		}

		private void CollapseColumn(int columnNumber)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col column = columnHelper.GetColumn(columnNumber, false);
			int indexOfColumn = columnHelper.GetIndexOfColumn(colsArray, column);
			if (indexOfColumn != -1)
			{
				int num = FindStartOfColumnOutlineGroup(indexOfColumn);
				CT_Col colArray = colsArray.GetColArray(num);
				int num2 = SetGroupHidden(num, colArray.outlineLevel, true);
				SetColumn(num2 + 1, null, 0, null, null, true);
			}
		}

		private void SetColumn(int targetColumnIx, short? xfIndex, int? style, int? level, bool? hidden, bool? collapsed)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col cT_Col = null;
			int num = 0;
			for (num = 0; num < colsArray.sizeOfColArray(); num++)
			{
				CT_Col colArray = colsArray.GetColArray(num);
				if (colArray.min >= targetColumnIx && colArray.max <= targetColumnIx)
				{
					cT_Col = colArray;
					break;
				}
				if (colArray.min > targetColumnIx)
				{
					break;
				}
			}
			if (cT_Col == null)
			{
				CT_Col cT_Col2 = new CT_Col();
				cT_Col2.min = (uint)targetColumnIx;
				cT_Col2.max = (uint)targetColumnIx;
				UnsetCollapsed(collapsed.Value, cT_Col2);
				columnHelper.AddCleanColIntoCols(colsArray, cT_Col2);
			}
			else
			{
				bool flag = style.HasValue && cT_Col.style != style;
				bool flag2 = level.HasValue && cT_Col.outlineLevel != level;
				bool flag3 = hidden.HasValue && cT_Col.hidden != hidden;
				bool flag4 = collapsed.HasValue && cT_Col.collapsed != collapsed;
				if (flag2 || flag3 || flag4 || flag)
				{
					if (cT_Col.min == targetColumnIx && cT_Col.max == targetColumnIx)
					{
						UnsetCollapsed(collapsed.Value, cT_Col);
					}
					else if (cT_Col.min == targetColumnIx || cT_Col.max == targetColumnIx)
					{
						if (cT_Col.min == targetColumnIx)
						{
							cT_Col.min = (uint)(targetColumnIx + 1);
						}
						else
						{
							cT_Col.max = (uint)(targetColumnIx - 1);
							num++;
						}
						CT_Col cT_Col3 = columnHelper.CloneCol(colsArray, cT_Col);
						cT_Col3.min = (uint)targetColumnIx;
						UnsetCollapsed(collapsed.Value, cT_Col3);
						columnHelper.AddCleanColIntoCols(colsArray, cT_Col3);
					}
					else
					{
						CT_Col cT_Col4 = cT_Col;
						CT_Col cT_Col5 = columnHelper.CloneCol(colsArray, cT_Col);
						CT_Col cT_Col6 = columnHelper.CloneCol(colsArray, cT_Col);
						int max = (int)cT_Col.max;
						cT_Col4.max = (uint)(targetColumnIx - 1);
						cT_Col5.min = (uint)targetColumnIx;
						cT_Col5.max = (uint)targetColumnIx;
						UnsetCollapsed(collapsed.Value, cT_Col5);
						columnHelper.AddCleanColIntoCols(colsArray, cT_Col5);
						cT_Col6.min = (uint)(targetColumnIx + 1);
						cT_Col6.max = (uint)max;
						columnHelper.AddCleanColIntoCols(colsArray, cT_Col6);
					}
				}
			}
		}

		private void UnsetCollapsed(bool collapsed, CT_Col ci)
		{
			if (collapsed)
			{
				ci.collapsed = collapsed;
			}
			else
			{
				ci.UnsetCollapsed();
			}
		}

		/// Sets all adjacent columns of the same outline level to the specified
		/// hidden status.
		///
		/// @param pIdx
		///                the col info index of the start of the outline group
		/// @return the column index of the last column in the outline group
		private int SetGroupHidden(int pIdx, int level, bool hidden)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			int i = pIdx;
			CT_Col cT_Col = colsArray.GetColArray(i);
			for (; i < colsArray.sizeOfColArray(); i++)
			{
				cT_Col.hidden = hidden;
				if (i + 1 < colsArray.sizeOfColArray())
				{
					CT_Col colArray = colsArray.GetColArray(i + 1);
					if (!IsAdjacentBefore(cT_Col, colArray) || colArray.outlineLevel < level)
					{
						break;
					}
					cT_Col = colArray;
				}
			}
			return (int)cT_Col.max;
		}

		private bool IsAdjacentBefore(CT_Col col, CT_Col other_col)
		{
			return col.max == other_col.min - 1;
		}

		private int FindStartOfColumnOutlineGroup(int pIdx)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col cT_Col = colsArray.GetColArray(pIdx);
			int outlineLevel = cT_Col.outlineLevel;
			int num = pIdx;
			while (num != 0)
			{
				CT_Col colArray = colsArray.GetColArray(num - 1);
				if (!IsAdjacentBefore(colArray, cT_Col) || colArray.outlineLevel < outlineLevel)
				{
					break;
				}
				num--;
				cT_Col = colArray;
			}
			return num;
		}

		private int FindEndOfColumnOutlineGroup(int colInfoIndex)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col cT_Col = colsArray.GetColArray(colInfoIndex);
			int outlineLevel = cT_Col.outlineLevel;
			int num = colInfoIndex;
			while (num < colsArray.sizeOfColArray() - 1)
			{
				CT_Col colArray = colsArray.GetColArray(num + 1);
				if (!IsAdjacentBefore(cT_Col, colArray) || colArray.outlineLevel < outlineLevel)
				{
					break;
				}
				num++;
				cT_Col = colArray;
			}
			return num;
		}

		private void ExpandColumn(int columnIndex)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			CT_Col column = columnHelper.GetColumn(columnIndex, false);
			int indexOfColumn = columnHelper.GetIndexOfColumn(colsArray, column);
			int num = FindColInfoIdx((int)column.max, indexOfColumn);
			if (num != -1 && IsColumnGroupCollapsed(num))
			{
				int num2 = FindStartOfColumnOutlineGroup(num);
				int num3 = FindEndOfColumnOutlineGroup(num);
				CT_Col colArray = colsArray.GetColArray(num3);
				if (!IsColumnGroupHiddenByParent(num))
				{
					int outlineLevel = colArray.outlineLevel;
					bool flag = false;
					for (int i = num2; i <= num3; i++)
					{
						CT_Col colArray2 = colsArray.GetColArray(i);
						if (outlineLevel == colArray2.outlineLevel)
						{
							colArray2.UnsetHidden();
							if (flag)
							{
								flag = false;
								colArray2.collapsed = true;
							}
						}
						else
						{
							flag = true;
						}
					}
				}
				SetColumn((int)(colArray.max + 1), null, null, null, false, false);
			}
		}

		private bool IsColumnGroupHiddenByParent(int idx)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			int num = 0;
			bool result = false;
			int num2 = FindEndOfColumnOutlineGroup(idx);
			if (num2 < colsArray.sizeOfColArray())
			{
				CT_Col colArray = colsArray.GetColArray(num2 + 1);
				if (IsAdjacentBefore(colsArray.GetColArray(num2), colArray))
				{
					num = colArray.outlineLevel;
					result = colArray.hidden;
				}
			}
			int num3 = 0;
			bool result2 = false;
			int num4 = FindStartOfColumnOutlineGroup(idx);
			if (num4 > 0)
			{
				CT_Col colArray2 = colsArray.GetColArray(num4 - 1);
				if (IsAdjacentBefore(colArray2, colsArray.GetColArray(num4)))
				{
					num3 = colArray2.outlineLevel;
					result2 = colArray2.hidden;
				}
			}
			if (num > num3)
			{
				return result;
			}
			return result2;
		}

		private int FindColInfoIdx(int columnValue, int fromColInfoIdx)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			if (columnValue < 0)
			{
				throw new ArgumentException("column parameter out of range: " + columnValue);
			}
			if (fromColInfoIdx < 0)
			{
				throw new ArgumentException("fromIdx parameter out of range: " + fromColInfoIdx);
			}
			for (int i = fromColInfoIdx; i < colsArray.sizeOfColArray(); i++)
			{
				CT_Col colArray = colsArray.GetColArray(i);
				if (ContainsColumn(colArray, columnValue))
				{
					return i;
				}
				if (colArray.min > fromColInfoIdx)
				{
					break;
				}
			}
			return -1;
		}

		private bool ContainsColumn(CT_Col col, int columnIndex)
		{
			if (col.min <= columnIndex)
			{
				return columnIndex <= col.max;
			}
			return false;
		}

		/// 'Collapsed' state is stored in a single column col info record
		/// immediately after the outline group
		///
		/// @param idx
		/// @return a bool represented if the column is collapsed
		private bool IsColumnGroupCollapsed(int idx)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			int num = FindEndOfColumnOutlineGroup(idx);
			int num2 = num + 1;
			if (num2 >= colsArray.sizeOfColArray())
			{
				return false;
			}
			CT_Col colArray = colsArray.GetColArray(num2);
			CT_Col colArray2 = colsArray.GetColArray(num);
			if (!IsAdjacentBefore(colArray2, colArray))
			{
				return false;
			}
			return colArray.collapsed;
		}

		/// Get the visibility state for a given column.
		///
		/// @param columnIndex - the column to get (0-based)
		/// @param hidden - the visiblity state of the column
		public void SetColumnHidden(int columnIndex, bool hidden)
		{
			columnHelper.SetColHidden(columnIndex, hidden);
		}

		public void SetColumnWidth(int columnIndex, int width)
		{
			if (width > 65280)
			{
				throw new ArgumentException("The maximum column width for an individual cell is 255 characters.");
			}
			columnHelper.SetColWidth(columnIndex, (double)width / 256.0);
			columnHelper.SetCustomWidth(columnIndex, true);
		}

		public void SetDefaultColumnStyle(int column, ICellStyle style)
		{
			columnHelper.SetColDefaultStyle(column, style);
		}

		private CT_SheetView GetSheetTypeSheetView()
		{
			if (GetDefaultSheetView() == null)
			{
				GetSheetTypeSheetViews().SetSheetViewArray(0, new CT_SheetView());
			}
			return GetDefaultSheetView();
		}

		/// group the row It is possible for collapsed to be false and yet still have
		/// the rows in question hidden. This can be achieved by having a lower
		/// outline level collapsed, thus hiding all the child rows. Note that in
		/// this case, if the lowest level were expanded, the middle level would
		/// remain collapsed.
		///
		/// @param rowIndex -
		///                the row involved, 0 based
		/// @param collapse -
		///                bool value for collapse
		public void SetRowGroupCollapsed(int rowIndex, bool collapse)
		{
			if (collapse)
			{
				CollapseRow(rowIndex);
			}
			else
			{
				ExpandRow(rowIndex);
			}
		}

		/// @param rowIndex the zero based row index to collapse
		private void CollapseRow(int rowIndex)
		{
			XSSFRow xSSFRow = (XSSFRow)GetRow(rowIndex);
			if (xSSFRow != null)
			{
				int rowIndex2 = FindStartOfRowOutlineGroup(rowIndex);
				int rownum = WriteHidden(xSSFRow, rowIndex2, true);
				if (GetRow(rownum) != null)
				{
					((XSSFRow)GetRow(rownum)).GetCTRow().collapsed = true;
				}
				else
				{
					XSSFRow xSSFRow2 = (XSSFRow)CreateRow(rownum);
					xSSFRow2.GetCTRow().collapsed = true;
				}
			}
		}

		/// @param rowIndex the zero based row index to find from
		private int FindStartOfRowOutlineGroup(int rowIndex)
		{
			int outlineLevel = ((XSSFRow)GetRow(rowIndex)).GetCTRow().outlineLevel;
			int num = rowIndex;
			while (GetRow(num) != null)
			{
				if (((XSSFRow)GetRow(num)).GetCTRow().outlineLevel < outlineLevel)
				{
					return num + 1;
				}
				num--;
			}
			return num;
		}

		private int WriteHidden(XSSFRow xRow, int rowIndex, bool hidden)
		{
			int outlineLevel = xRow.GetCTRow().outlineLevel;
			IEnumerator rowEnumerator = GetRowEnumerator();
			while (rowEnumerator.MoveNext())
			{
				xRow = (XSSFRow)rowEnumerator.Current;
				if (xRow.GetCTRow().outlineLevel >= outlineLevel)
				{
					xRow.GetCTRow().hidden = hidden;
					rowIndex++;
				}
			}
			return rowIndex;
		}

		/// @param rowNumber the zero based row index to expand
		private void ExpandRow(int rowNumber)
		{
			if (rowNumber != -1)
			{
				XSSFRow xSSFRow = (XSSFRow)GetRow(rowNumber);
				if (xSSFRow.GetCTRow().IsSetHidden())
				{
					int num = FindStartOfRowOutlineGroup(rowNumber);
					int num2 = FindEndOfRowOutlineGroup(rowNumber);
					if (!IsRowGroupHiddenByParent(rowNumber))
					{
						for (int i = num; i < num2; i++)
						{
							if (xSSFRow.GetCTRow().outlineLevel == ((XSSFRow)GetRow(i)).GetCTRow().outlineLevel)
							{
								((XSSFRow)GetRow(i)).GetCTRow().unsetHidden();
							}
							else if (!IsRowGroupCollapsed(i))
							{
								((XSSFRow)GetRow(i)).GetCTRow().unsetHidden();
							}
						}
					}
					((XSSFRow)GetRow(num2)).GetCTRow().UnsetCollapsed();
				}
			}
		}

		/// @param row the zero based row index to find from
		public int FindEndOfRowOutlineGroup(int row)
		{
			int outlineLevel = ((XSSFRow)GetRow(row)).GetCTRow().outlineLevel;
			int i;
			for (i = row; i < LastRowNum && GetRow(i) != null && ((XSSFRow)GetRow(i)).GetCTRow().outlineLevel >= outlineLevel; i++)
			{
			}
			return i;
		}

		/// @param row the zero based row index to find from
		private bool IsRowGroupHiddenByParent(int row)
		{
			int rownum = FindEndOfRowOutlineGroup(row);
			int num;
			bool result;
			if (GetRow(rownum) == null)
			{
				num = 0;
				result = false;
			}
			else
			{
				num = ((XSSFRow)GetRow(rownum)).GetCTRow().outlineLevel;
				result = ((XSSFRow)GetRow(rownum)).GetCTRow().hidden;
			}
			int num2 = FindStartOfRowOutlineGroup(row);
			int num3;
			bool result2;
			if (num2 < 0 || GetRow(num2) == null)
			{
				num3 = 0;
				result2 = false;
			}
			else
			{
				num3 = ((XSSFRow)GetRow(num2)).GetCTRow().outlineLevel;
				result2 = ((XSSFRow)GetRow(num2)).GetCTRow().hidden;
			}
			if (num > num3)
			{
				return result;
			}
			return result2;
		}

		/// @param row the zero based row index to find from
		private bool IsRowGroupCollapsed(int row)
		{
			int rownum = FindEndOfRowOutlineGroup(row) + 1;
			if (GetRow(rownum) == null)
			{
				return false;
			}
			return ((XSSFRow)GetRow(rownum)).GetCTRow().collapsed;
		}

		/// Sets the zoom magnication for the sheet.  The zoom is expressed as a
		/// fraction.  For example to express a zoom of 75% use 3 for the numerator
		/// and 4 for the denominator.
		///
		/// @param numerator     The numerator for the zoom magnification.
		/// @param denominator   The denominator for the zoom magnification.
		/// @see #SetZoom(int)
		public void SetZoom(int numerator, int denominator)
		{
			int zoom = 100 * numerator / denominator;
			SetZoom(zoom);
		}

		public void SetZoom(int scale)
		{
			if (scale < 10 || scale > 400)
			{
				throw new ArgumentException("Valid scale values range from 10 to 400");
			}
			GetSheetTypeSheetView().zoomScale = (uint)scale;
		}

		public void ShiftRows(int startRow, int endRow, int n)
		{
			ShiftRows(startRow, endRow, n, false, false);
		}

		public void ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool reSetOriginalRowHeight)
		{
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, XSSFRow> row in _rows)
			{
				XSSFRow value = row.Value;
				int rowNum = value.RowNum;
				if (RemoveRow(startRow, endRow, n, rowNum))
				{
					int rowNum2 = row.Key + 1;
					worksheet.sheetData.RemoveRow(rowNum2);
					list.Add(row.Key);
				}
				if (!copyRowHeight)
				{
					value.Height = -1;
				}
				if (sheetComments != null && rowNum >= startRow && rowNum <= endRow)
				{
					CT_CommentList commentList = sheetComments.GetCTComments().commentList;
					foreach (CT_Comment item in commentList.comment)
					{
						CellReference cellReference = new CellReference(item.@ref);
						if (cellReference.Row == rowNum)
						{
							CellReference cellReference2 = new CellReference(rowNum + n, cellReference.Col);
							string @ref = item.@ref;
							item.@ref = cellReference2.FormatAsString();
							break;
						}
					}
				}
			}
			foreach (int item2 in list)
			{
				_rows.Remove(item2);
			}
			if (sheetComments != null)
			{
				sheetComments.RecreateReference();
			}
			foreach (XSSFRow value2 in _rows.Values)
			{
				int rowNum3 = value2.RowNum;
				if (rowNum3 >= startRow && rowNum3 <= endRow)
				{
					value2.Shift(n);
				}
			}
			XSSFRowShifter xSSFRowShifter = new XSSFRowShifter(this);
			int sheetIndex = Workbook.GetSheetIndex(this);
			FormulaShifter shifter = FormulaShifter.CreateForRowShift(sheetIndex, startRow, endRow, n);
			xSSFRowShifter.UpdateNamedRanges(shifter);
			xSSFRowShifter.UpdateFormulas(shifter);
			xSSFRowShifter.ShiftMerged(startRow, endRow, n);
			xSSFRowShifter.UpdateConditionalFormatting(shifter);
			SortedList<int, XSSFRow> sortedList = new SortedList<int, XSSFRow>();
			foreach (XSSFRow value3 in _rows.Values)
			{
				sortedList.Add(value3.RowNum, value3);
			}
			_rows = sortedList;
		}

		/// Location of the top left visible cell Location of the top left visible cell in the bottom right
		/// pane (when in Left-to-Right mode).
		///
		/// @param toprow the top row to show in desktop window pane
		/// @param leftcol the left column to show in desktop window pane
		public void ShowInPane(short toprow, short leftcol)
		{
			CellReference cellReference = new CellReference(toprow, leftcol);
			string topLeftCell = cellReference.FormatAsString();
			GetPane().topLeftCell = topLeftCell;
		}

		public void UngroupColumn(int fromColumn, int toColumn)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			for (int i = fromColumn; i <= toColumn; i++)
			{
				CT_Col column = columnHelper.GetColumn(i, false);
				if (column != null)
				{
					column.outlineLevel--;
					i = (int)column.max;
					if (column.outlineLevel <= 0)
					{
						int indexOfColumn = columnHelper.GetIndexOfColumn(colsArray, column);
						worksheet.GetColsArray(0).RemoveCol(indexOfColumn);
					}
				}
			}
			worksheet.SetColsArray(0, colsArray);
			SetSheetFormatPrOutlineLevelCol();
		}

		/// Ungroup a range of rows that were previously groupped
		///
		/// @param fromRow   start row (0-based)
		/// @param toRow     end row (0-based)
		public void UngroupRow(int fromRow, int toRow)
		{
			for (int i = fromRow; i <= toRow; i++)
			{
				XSSFRow xSSFRow = (XSSFRow)GetRow(i);
				if (xSSFRow != null)
				{
					CT_Row cTRow = xSSFRow.GetCTRow();
					cTRow.outlineLevel--;
					if (cTRow.outlineLevel == 0 && xSSFRow.FirstCellNum == -1)
					{
						RemoveRow(xSSFRow);
					}
				}
			}
			SetSheetFormatPrOutlineLevelRow();
		}

		private void SetSheetFormatPrOutlineLevelRow()
		{
			short maxOutlineLevelRows = GetMaxOutlineLevelRows();
			GetSheetTypeSheetFormatPr().outlineLevelRow = (byte)maxOutlineLevelRows;
		}

		private void SetSheetFormatPrOutlineLevelCol()
		{
			short maxOutlineLevelCols = GetMaxOutlineLevelCols();
			GetSheetTypeSheetFormatPr().outlineLevelCol = (byte)maxOutlineLevelCols;
		}

		private CT_SheetViews GetSheetTypeSheetViews()
		{
			if (worksheet.sheetViews == null)
			{
				worksheet.sheetViews = new CT_SheetViews();
				worksheet.sheetViews.AddNewSheetView();
			}
			return worksheet.sheetViews;
		}

		/// Assign a cell comment to a cell region in this worksheet
		///
		/// @param cellRef cell region
		/// @param comment the comment to assign
		/// @deprecated since Nov 2009 use {@link XSSFCell#SetCellComment(NPOI.SS.usermodel.Comment)} instead
		public static void SetCellComment(string cellRef, XSSFComment comment)
		{
			CellReference cellReference = new CellReference(cellRef);
			comment.Row = cellReference.Row;
			comment.Column = cellReference.Col;
		}

		/// Register a hyperlink in the collection of hyperlinks on this sheet
		///
		/// @param hyperlink the link to add
		public void AddHyperlink(XSSFHyperlink hyperlink)
		{
			hyperlinks.Add(hyperlink);
		}

		public void SetActiveCell(string cellref)
		{
			CT_Selection sheetTypeSelection = GetSheetTypeSelection();
			sheetTypeSelection.activeCell = cellref;
			sheetTypeSelection.SetSqref(new string[1]
			{
				cellref
			});
		}

		public void SetActiveCell(int row, int column)
		{
			CellReference cellReference = new CellReference(row, column);
			SetActiveCell(cellReference.FormatAsString());
		}

		private CT_Selection GetSheetTypeSelection()
		{
			if (GetSheetTypeSheetView().SizeOfSelectionArray() == 0)
			{
				GetSheetTypeSheetView().InsertNewSelection(0);
			}
			return GetSheetTypeSheetView().GetSelectionArray(0);
		}

		/// Return the default sheet view. This is the last one if the sheet's views, according to sec. 3.3.1.83
		/// of the OOXML spec: "A single sheet view defInition. When more than 1 sheet view is defined in the file,
		/// it means that when opening the workbook, each sheet view corresponds to a separate window within the
		/// spreadsheet application, where each window is Showing the particular sheet. Containing the same
		/// workbookViewId value, the last sheetView defInition is loaded, and the others are discarded.
		/// When multiple windows are viewing the same sheet, multiple sheetView elements (with corresponding
		/// workbookView entries) are saved."
		private CT_SheetView GetDefaultSheetView()
		{
			CT_SheetViews sheetTypeSheetViews = GetSheetTypeSheetViews();
			int num = (sheetTypeSheetViews != null) ? sheetTypeSheetViews.sizeOfSheetViewArray() : 0;
			if (num == 0)
			{
				return null;
			}
			return sheetTypeSheetViews.GetSheetViewArray(num - 1);
		}

		/// Returns the sheet's comments object if there is one,
		///  or null if not
		///
		/// @param create create a new comments table if it does not exist
		protected internal CommentsTable GetCommentsTable(bool create)
		{
			if (sheetComments == null && create)
			{
				try
				{
					sheetComments = (CommentsTable)CreateRelationship(XSSFRelation.SHEET_COMMENTS, XSSFFactory.GetInstance(), (int)sheet.sheetId);
				}
				catch (PartAlreadyExistsException)
				{
					sheetComments = (CommentsTable)CreateRelationship(XSSFRelation.SHEET_COMMENTS, XSSFFactory.GetInstance(), -1);
				}
			}
			return sheetComments;
		}

		private CT_PageSetUpPr GetSheetTypePageSetUpPr()
		{
			CT_SheetPr sheetTypeSheetPr = GetSheetTypeSheetPr();
			if (!sheetTypeSheetPr.IsSetPageSetUpPr())
			{
				return sheetTypeSheetPr.AddNewPageSetUpPr();
			}
			return sheetTypeSheetPr.pageSetUpPr;
		}

		private bool RemoveRow(int startRow, int endRow, int n, int rownum)
		{
			if (rownum >= startRow + n && rownum <= endRow + n)
			{
				if (n > 0 && rownum > endRow)
				{
					return true;
				}
				if (n < 0 && rownum < startRow)
				{
					return true;
				}
			}
			return false;
		}

		private CT_Pane GetPane()
		{
			if (GetDefaultSheetView().pane == null)
			{
				GetDefaultSheetView().AddNewPane();
			}
			return GetDefaultSheetView().pane;
		}

		/// Return a master shared formula by index
		///
		/// @param sid shared group index
		/// @return a CT_CellFormula bean holding shared formula or <code>null</code> if not found
		internal CT_CellFormula GetSharedFormula(int sid)
		{
			return sharedFormulas[sid];
		}

		internal void OnReadCell(XSSFCell cell)
		{
			CT_Cell cTCell = cell.GetCTCell();
			CT_CellFormula f = cTCell.f;
			if (f != null && f.t == ST_CellFormulaType.shared && f.isSetRef() && f.Value != null)
			{
				CT_CellFormula cT_CellFormula = f.Copy();
				CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(cT_CellFormula.@ref);
				CellReference cellReference = new CellReference(cell);
				if (cellReference.Col > cellRangeAddress.FirstColumn || cellReference.Row > cellRangeAddress.FirstRow)
				{
					string text2 = cT_CellFormula.@ref = new CellRangeAddress(Math.Max(cellReference.Row, cellRangeAddress.FirstRow), cellRangeAddress.LastRow, Math.Max(cellReference.Col, cellRangeAddress.FirstColumn), cellRangeAddress.LastColumn).FormatAsString();
				}
				sharedFormulas[(int)f.si] = cT_CellFormula;
			}
			if (f != null && f.t == ST_CellFormulaType.array && f.@ref != null)
			{
				arrayFormulas.Add(CellRangeAddress.ValueOf(f.@ref));
			}
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			Write(outputStream);
			outputStream.Close();
		}

		internal virtual void Write(Stream stream)
		{
			if (worksheet.sizeOfColsArray() == 1)
			{
				CT_Cols colsArray = worksheet.GetColsArray(0);
				if (colsArray.sizeOfColArray() == 0)
				{
					worksheet.SetColsArray(null);
				}
			}
			if (hyperlinks.Count > 0)
			{
				if (worksheet.hyperlinks == null)
				{
					worksheet.AddNewHyperlinks();
				}
				CT_Hyperlink[] array = new CT_Hyperlink[hyperlinks.Count];
				for (int i = 0; i < array.Length; i++)
				{
					XSSFHyperlink xSSFHyperlink = hyperlinks[i];
					xSSFHyperlink.GenerateRelationIfNeeded(GetPackagePart());
					array[i] = xSSFHyperlink.GetCTHyperlink();
				}
				worksheet.hyperlinks.SetHyperlinkArray(array);
			}
			foreach (XSSFRow value in _rows.Values)
			{
				value.OnDocumentWrite();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary[ST_RelationshipId.NamespaceURI] = "r";
			new WorksheetDocument(worksheet).Save(stream);
		}

		/// Enable sheet protection
		public void EnableLocking()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.sheet = true;
		}

		/// Disable sheet protection
		public void DisableLocking()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.sheet = false;
		}

		/// Enable Autofilters locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockAutoFilter()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.autoFilter = true;
		}

		/// Enable Deleting columns locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockDeleteColumns()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.deleteColumns = true;
		}

		/// Enable Deleting rows locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockDeleteRows()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.deleteRows = true;
		}

		/// Enable Formatting cells locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockFormatCells()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.deleteColumns = true;
		}

		/// Enable Formatting columns locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockFormatColumns()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.formatColumns = true;
		}

		/// Enable Formatting rows locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockFormatRows()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.formatRows = true;
		}

		/// Enable Inserting columns locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockInsertColumns()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.insertColumns = true;
		}

		/// Enable Inserting hyperlinks locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockInsertHyperlinks()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.insertHyperlinks = true;
		}

		/// Enable Inserting rows locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockInsertRows()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.insertRows = true;
		}

		/// Enable Pivot Tables locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockPivotTables()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.pivotTables = true;
		}

		/// Enable Sort locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockSort()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.sort = true;
		}

		/// Enable Objects locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockObjects()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.objects = true;
		}

		/// Enable Scenarios locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockScenarios()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.scenarios = true;
		}

		/// Enable Selection of locked cells locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockSelectLockedCells()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.selectLockedCells = true;
		}

		/// Enable Selection of unlocked cells locking.
		/// This does not modify sheet protection status.
		/// To enforce this locking, call {@link #enableLocking()}
		public void LockSelectUnlockedCells()
		{
			CreateProtectionFieldIfNotPresent();
			worksheet.sheetProtection.selectUnlockedCells = true;
		}

		private void CreateProtectionFieldIfNotPresent()
		{
			if (worksheet.sheetProtection == null)
			{
				worksheet.sheetProtection = new CT_SheetProtection();
			}
		}

		private bool sheetProtectionEnabled()
		{
			return worksheet.sheetProtection.sheet;
		}

		internal bool IsCellInArrayFormulaContext(ICell cell)
		{
			foreach (CellRangeAddress arrayFormula in arrayFormulas)
			{
				if (arrayFormula.IsInRange(cell.RowIndex, cell.ColumnIndex))
				{
					return true;
				}
			}
			return false;
		}

		internal XSSFCell GetFirstCellInArrayFormula(ICell cell)
		{
			foreach (CellRangeAddress arrayFormula in arrayFormulas)
			{
				if (arrayFormula.IsInRange(cell.RowIndex, cell.ColumnIndex))
				{
					return (XSSFCell)GetRow(arrayFormula.FirstRow).GetCell(arrayFormula.FirstColumn);
				}
			}
			return null;
		}

		/// Also Creates cells if they don't exist
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
			return SSCellRange<ICell>.Create(firstRow, firstColumn, num, num2, list, typeof(ICell));
		}

		public ICellRange<ICell> SetArrayFormula(string formula, CellRangeAddress range)
		{
			ICellRange<ICell> cellRange = GetCellRange(range);
			ICell topLeftCell = cellRange.TopLeftCell;
			((XSSFCell)topLeftCell).SetCellArrayFormula(formula, range);
			arrayFormulas.Add(range);
			return cellRange;
		}

		public ICellRange<ICell> RemoveArrayFormula(ICell cell)
		{
			if (cell.Sheet != this)
			{
				throw new ArgumentException("Specified cell does not belong to this sheet.");
			}
			foreach (CellRangeAddress arrayFormula in arrayFormulas)
			{
				if (arrayFormula.IsInRange(cell.RowIndex, cell.ColumnIndex))
				{
					arrayFormulas.Remove(arrayFormula);
					ICellRange<ICell> cellRange = GetCellRange(arrayFormula);
					foreach (ICell item in cellRange)
					{
						item.SetCellType(CellType.Blank);
					}
					return cellRange;
				}
			}
			string r = ((XSSFCell)cell).GetCTCell().r;
			throw new ArgumentException("Cell " + r + " is not part of an array formula.");
		}

		public IDataValidationHelper GetDataValidationHelper()
		{
			return dataValidationHelper;
		}

		public List<XSSFDataValidation> GetDataValidations()
		{
			List<XSSFDataValidation> list = new List<XSSFDataValidation>();
			CT_DataValidations dataValidations = worksheet.dataValidations;
			if (dataValidations != null && dataValidations.count != 0)
			{
				foreach (CT_DataValidation item2 in dataValidations.dataValidation)
				{
					CellRangeAddressList cellRangeAddressList = new CellRangeAddressList();
					string[] array = item2.sqref.Split(' ');
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Length != 0)
						{
							string[] array2 = array[i].Split(':');
							CellReference cellReference = new CellReference(array2[0]);
							CellReference cellReference2 = (array2.Length > 1) ? new CellReference(array2[1]) : cellReference;
							CellRangeAddress cra = new CellRangeAddress(cellReference.Row, cellReference2.Row, cellReference.Col, cellReference2.Col);
							cellRangeAddressList.AddCellRangeAddress(cra);
						}
					}
					XSSFDataValidation item = new XSSFDataValidation(cellRangeAddressList, item2);
					list.Add(item);
				}
				return list;
			}
			return list;
		}

		public void AddValidationData(IDataValidation dataValidation)
		{
			XSSFDataValidation xSSFDataValidation = (XSSFDataValidation)dataValidation;
			CT_DataValidations cT_DataValidations = worksheet.dataValidations;
			if (cT_DataValidations == null)
			{
				cT_DataValidations = worksheet.AddNewDataValidations();
			}
			int num = cT_DataValidations.sizeOfDataValidationArray();
			CT_DataValidation cT_DataValidation = cT_DataValidations.AddNewDataValidation();
			cT_DataValidation.Set(xSSFDataValidation.GetCTDataValidation());
			cT_DataValidations.count = (uint)(num + 1);
		}

		public IAutoFilter SetAutoFilter(CellRangeAddress range)
		{
			CT_AutoFilter cT_AutoFilter = worksheet.autoFilter;
			if (cT_AutoFilter == null)
			{
				cT_AutoFilter = worksheet.AddNewAutoFilter();
			}
			CellRangeAddress cellRangeAddress = new CellRangeAddress(range.FirstRow, range.LastRow, range.FirstColumn, range.LastColumn);
			string text2 = cT_AutoFilter.@ref = cellRangeAddress.FormatAsString();
			XSSFWorkbook xSSFWorkbook = (XSSFWorkbook)Workbook;
			int sheetIndex = Workbook.GetSheetIndex(this);
			XSSFName xSSFName = xSSFWorkbook.GetBuiltInName(XSSFName.BUILTIN_FILTER_DB, sheetIndex);
			if (xSSFName == null)
			{
				xSSFName = xSSFWorkbook.CreateBuiltInName(XSSFName.BUILTIN_FILTER_DB, sheetIndex);
			}
			xSSFName.GetCTName().hidden = true;
			CellReference cellReference = new CellReference(SheetName, range.FirstRow, range.FirstColumn, true, true);
			CellReference cellReference2 = new CellReference(null, range.LastRow, range.LastColumn, true, true);
			string text4 = xSSFName.RefersToFormula = cellReference.FormatAsString() + ":" + cellReference2.FormatAsString();
			return new XSSFAutoFilter(this);
		}

		/// Creates a new Table, and associates it with this Sheet
		public XSSFTable CreateTable()
		{
			if (!worksheet.IsSetTableParts())
			{
				worksheet.AddNewTableParts();
			}
			CT_TableParts tableParts = worksheet.tableParts;
			CT_TablePart cT_TablePart = tableParts.AddNewTablePart();
			int idx = GetPackagePart().Package.GetPartsByContentType(XSSFRelation.TABLE.ContentType).Count + 1;
			XSSFTable xSSFTable = (XSSFTable)CreateRelationship(XSSFRelation.TABLE, XSSFFactory.GetInstance(), idx);
			cT_TablePart.id = xSSFTable.GetPackageRelationship().Id;
			tables[cT_TablePart.id] = xSSFTable;
			return xSSFTable;
		}

		/// Returns any tables associated with this Sheet
		public List<XSSFTable> GetTables()
		{
			return new List<XSSFTable>(tables.Values);
		}

		/// Set background color of the sheet tab
		///
		/// @param colorIndex  the indexed color to set, must be a constant from {@link IndexedColors}
		public void SetTabColor(int colorIndex)
		{
			CT_SheetPr cT_SheetPr = worksheet.sheetPr;
			if (cT_SheetPr == null)
			{
				cT_SheetPr = worksheet.AddNewSheetPr();
			}
			CT_Color cT_Color = new CT_Color();
			cT_Color.indexed = (uint)colorIndex;
			cT_SheetPr.tabColor = cT_Color;
		}

		public IEnumerator GetEnumerator()
		{
			return GetRowEnumerator();
		}

		public IEnumerator GetRowEnumerator()
		{
			return _rows.Values.GetEnumerator();
		}

		public bool IsMergedRegion(CellRangeAddress mergedRegion)
		{
			if (worksheet.mergeCells == null || worksheet.mergeCells.mergeCell == null)
			{
				return false;
			}
			foreach (CT_MergeCell item in worksheet.mergeCells.mergeCell)
			{
				if (!string.IsNullOrEmpty(item.@ref))
				{
					CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(item.@ref);
					if (cellRangeAddress.FirstColumn <= mergedRegion.FirstColumn && cellRangeAddress.LastColumn >= mergedRegion.LastColumn && cellRangeAddress.FirstRow <= mergedRegion.FirstRow && cellRangeAddress.LastRow >= mergedRegion.LastRow)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void SetActive(bool value)
		{
			IsSelected = value;
		}

		public void SetActiveCellRange(List<CellRangeAddress8Bit> cellranges, int activeRange, int activeRow, int activeColumn)
		{
			throw new NotImplementedException();
		}

		public void SetActiveCellRange(int firstRow, int lastRow, int firstColumn, int lastColumn)
		{
			throw new NotImplementedException();
		}

		public IRow CopyRow(int sourceIndex, int targetIndex)
		{
			return SheetUtil.CopyRow(this, sourceIndex, targetIndex);
		}

		public void ShowInPane(int toprow, int leftcol)
		{
			CellReference cellReference = new CellReference(toprow, leftcol);
			string topLeftCell = cellReference.FormatAsString();
			Pane.topLeftCell = topLeftCell;
		}

		private void SetRepeatingRowsAndColumns(CellRangeAddress rowDef, CellRangeAddress colDef)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			if (rowDef != null)
			{
				num3 = rowDef.FirstRow;
				num4 = rowDef.LastRow;
				if ((num3 == -1 && num4 != -1) || num3 < -1 || num4 < -1 || num3 > num4)
				{
					throw new ArgumentException("Invalid row range specification");
				}
			}
			if (colDef != null)
			{
				num = colDef.FirstColumn;
				num2 = colDef.LastColumn;
				if ((num == -1 && num2 != -1) || num < -1 || num2 < -1 || num > num2)
				{
					throw new ArgumentException("Invalid column range specification");
				}
			}
			int sheetIndex = Workbook.GetSheetIndex(this);
			bool flag = rowDef == null && colDef == null;
			XSSFWorkbook xSSFWorkbook = Workbook as XSSFWorkbook;
			if (xSSFWorkbook == null)
			{
				throw new RuntimeException("Workbook should not be null");
			}
			XSSFName xSSFName = xSSFWorkbook.GetBuiltInName(XSSFName.BUILTIN_PRINT_TITLE, sheetIndex);
			if (flag)
			{
				if (xSSFName != null)
				{
					xSSFWorkbook.RemoveName(xSSFName);
				}
			}
			else
			{
				if (xSSFName == null)
				{
					xSSFName = xSSFWorkbook.CreateBuiltInName(XSSFName.BUILTIN_PRINT_TITLE, sheetIndex);
				}
				string text = xSSFName.RefersToFormula = GetReferenceBuiltInRecord(xSSFName.SheetName, num, num2, num3, num4);
				if (!worksheet.IsSetPageSetup() || !worksheet.IsSetPageMargins())
				{
					PrintSetup.ValidSettings = false;
				}
			}
		}

		private static string GetReferenceBuiltInRecord(string sheetName, int startC, int endC, int startR, int endR)
		{
			CellReference cellReference = new CellReference(sheetName, 0, startC, true, true);
			CellReference cellReference2 = new CellReference(sheetName, 0, endC, true, true);
			CellReference cellReference3 = new CellReference(sheetName, startR, 0, true, true);
			CellReference cellReference4 = new CellReference(sheetName, endR, 0, true, true);
			string text = SheetNameFormatter.Format(sheetName);
			string value = "";
			string text2 = "";
			if (startC != -1 || endC != -1)
			{
				value = text + "!$" + cellReference.CellRefParts[2] + ":$" + cellReference2.CellRefParts[2];
			}
			if ((startR != -1 || endR != -1) && !cellReference3.CellRefParts[1].Equals("0") && !cellReference4.CellRefParts[1].Equals("0"))
			{
				text2 = text + "!$" + cellReference3.CellRefParts[1] + ":$" + cellReference4.CellRefParts[1];
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			if (stringBuilder.Length > 0 && text2.Length > 0)
			{
				stringBuilder.Append(',');
			}
			stringBuilder.Append(text2);
			return stringBuilder.ToString();
		}

		private CellRangeAddress GetRepeatingRowsOrColums(bool rows)
		{
			int sheetIndex = Workbook.GetSheetIndex(this);
			XSSFWorkbook xSSFWorkbook = Workbook as XSSFWorkbook;
			if (xSSFWorkbook == null)
			{
				throw new RuntimeException("Workbook should not be null");
			}
			XSSFName builtInName = xSSFWorkbook.GetBuiltInName(XSSFName.BUILTIN_PRINT_TITLE, sheetIndex);
			if (builtInName == null)
			{
				return null;
			}
			string refersToFormula = builtInName.RefersToFormula;
			if (refersToFormula == null)
			{
				return null;
			}
			string[] array = refersToFormula.Split(",".ToCharArray());
			int lastRowIndex = SpreadsheetVersion.EXCEL2007.LastRowIndex;
			int lastColumnIndex = SpreadsheetVersion.EXCEL2007.LastColumnIndex;
			string[] array2 = array;
			foreach (string reference in array2)
			{
				CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(reference);
				if ((cellRangeAddress.FirstColumn == 0 && cellRangeAddress.LastColumn == lastColumnIndex) || (cellRangeAddress.FirstColumn == -1 && cellRangeAddress.LastColumn == -1))
				{
					if (rows)
					{
						return cellRangeAddress;
					}
				}
				else if (((cellRangeAddress.FirstRow == 0 && cellRangeAddress.LastRow == lastRowIndex) || (cellRangeAddress.FirstRow == -1 && cellRangeAddress.LastRow == -1)) && !rows)
				{
					return cellRangeAddress;
				}
			}
			return null;
		}

		public ISheet CopySheet(string Name)
		{
			return CopySheet(Name, true);
		}

		public ISheet CopySheet(string name, bool copyStyle)
		{
			string uniqueSheetName = SheetUtil.GetUniqueSheetName(Workbook, name);
			XSSFSheet xSSFSheet = (XSSFSheet)Workbook.CreateSheet(uniqueSheetName);
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					Write(memoryStream);
					xSSFSheet.Read(new MemoryStream(memoryStream.ToArray()));
				}
			}
			catch (IOException ex)
			{
				throw new POIXMLException("Failed to clone sheet", ex);
			}
			CT_Worksheet cTWorksheet = xSSFSheet.GetCTWorksheet();
			if (cTWorksheet.IsSetLegacyDrawing())
			{
				logger.Log(5, "Cloning sheets with comments is not yet supported.");
				cTWorksheet.UnsetLegacyDrawing();
			}
			xSSFSheet.IsSelected = false;
			List<POIXMLDocumentPart> relations = GetRelations();
			XSSFDrawing xSSFDrawing = null;
			foreach (POIXMLDocumentPart item in relations)
			{
				if (item is XSSFDrawing)
				{
					xSSFDrawing = (XSSFDrawing)item;
				}
				else
				{
					PackageRelationship packageRelationship = item.GetPackageRelationship();
					xSSFSheet.GetPackagePart().AddRelationship(packageRelationship.TargetUri, packageRelationship.TargetMode.Value, packageRelationship.RelationshipType);
					xSSFSheet.AddRelation(packageRelationship.Id, item);
				}
			}
			xSSFSheet.hyperlinks = new List<XSSFHyperlink>(hyperlinks);
			if (xSSFDrawing != null)
			{
				if (cTWorksheet.IsSetDrawing())
				{
					cTWorksheet.UnsetDrawing();
				}
				XSSFDrawing xSSFDrawing2 = xSSFSheet.CreateDrawingPatriarch() as XSSFDrawing;
				xSSFDrawing2.GetCTDrawing().Set(xSSFDrawing.GetCTDrawing());
				List<POIXMLDocumentPart> relations2 = xSSFDrawing.GetRelations();
				{
					foreach (POIXMLDocumentPart item2 in relations2)
					{
						PackageRelationship packageRelationship2 = item2.GetPackageRelationship();
						(xSSFSheet.CreateDrawingPatriarch() as XSSFDrawing).GetPackagePart().AddRelationship(packageRelationship2.TargetUri, packageRelationship2.TargetMode.Value, packageRelationship2.RelationshipType, packageRelationship2.Id);
					}
					return xSSFSheet;
				}
			}
			return xSSFSheet;
		}
	}
}
