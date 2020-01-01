using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel
{
	public class XSSFDialogsheet : XSSFSheet, ISheet
	{
		protected CT_Dialogsheet dialogsheet;

		int ISheet.PhysicalNumberOfRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		int ISheet.FirstRowNum
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		int ISheet.LastRowNum
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.ForceFormulaRecalculation
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

		int ISheet.DefaultColumnWidth
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

		short ISheet.DefaultRowHeight
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

		float ISheet.DefaultRowHeightInPoints
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

		bool ISheet.HorizontallyCenter
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

		bool ISheet.VerticallyCenter
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

		int ISheet.NumMergedRegions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.DisplayZeros
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

		bool ISheet.Autobreaks
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

		bool ISheet.DisplayGuts
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

		bool ISheet.FitToPage
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

		bool ISheet.RowSumsBelow
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

		bool ISheet.RowSumsRight
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

		bool ISheet.IsPrintGridlines
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

		IPrintSetup ISheet.PrintSetup
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		IHeader ISheet.Header
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		IFooter ISheet.Footer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.Protect
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.ScenarioProtect
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		short ISheet.TabColorIndex
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

		IDrawing ISheet.DrawingPatriarch
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		short ISheet.TopRow
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

		short ISheet.LeftCol
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

		PaneInformation ISheet.PaneInformation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.DisplayGridlines
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

		bool ISheet.DisplayFormulas
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

		bool ISheet.DisplayRowColHeadings
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

		bool ISheet.IsActive
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

		int[] ISheet.RowBreaks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		int[] ISheet.ColumnBreaks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		IWorkbook ISheet.Workbook
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		string ISheet.SheetName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ISheet.IsSelected
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

		ISheetConditionalFormatting ISheet.SheetConditionalFormatting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		private new bool IsRightToLeft
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

		public XSSFDialogsheet(XSSFSheet sheet)
			: base(sheet.GetPackagePart(), sheet.GetPackageRelationship())
		{
			dialogsheet = new CT_Dialogsheet();
			worksheet = new CT_Worksheet();
		}

		public override IRow CreateRow(int rowNum)
		{
			return null;
		}

		protected CT_HeaderFooter GetSheetTypeHeaderFooter()
		{
			if (dialogsheet.headerFooter == null)
			{
				dialogsheet.headerFooter = new CT_HeaderFooter();
			}
			return dialogsheet.headerFooter;
		}

		protected CT_SheetPr GetSheetTypeSheetPr()
		{
			if (dialogsheet.sheetPr == null)
			{
				dialogsheet.sheetPr = new CT_SheetPr();
			}
			return dialogsheet.sheetPr;
		}

		protected CT_PageBreak GetSheetTypeColumnBreaks()
		{
			return null;
		}

		protected CT_SheetFormatPr GetSheetTypeSheetFormatPr()
		{
			if (dialogsheet.sheetFormatPr == null)
			{
				dialogsheet.sheetFormatPr = new CT_SheetFormatPr();
			}
			return dialogsheet.sheetFormatPr;
		}

		protected CT_PageMargins GetSheetTypePageMargins()
		{
			if (dialogsheet.pageMargins == null)
			{
				dialogsheet.pageMargins = new CT_PageMargins();
			}
			return dialogsheet.pageMargins;
		}

		protected CT_PageBreak GetSheetTypeRowBreaks()
		{
			return null;
		}

		protected CT_SheetViews GetSheetTypeSheetViews()
		{
			if (dialogsheet.sheetViews == null)
			{
				dialogsheet.sheetViews = new CT_SheetViews();
				dialogsheet.sheetViews.AddNewSheetView();
			}
			return dialogsheet.sheetViews;
		}

		protected CT_PrintOptions GetSheetTypePrintOptions()
		{
			if (dialogsheet.printOptions == null)
			{
				dialogsheet.printOptions = new CT_PrintOptions();
			}
			return dialogsheet.printOptions;
		}

		protected CT_SheetProtection GetSheetTypeProtection()
		{
			if (dialogsheet.sheetProtection == null)
			{
				dialogsheet.sheetProtection = new CT_SheetProtection();
			}
			return dialogsheet.sheetProtection;
		}

		public bool GetDialog()
		{
			return true;
		}

		IRow ISheet.CreateRow(int rownum)
		{
			throw new NotImplementedException();
		}

		void ISheet.RemoveRow(IRow row)
		{
			throw new NotImplementedException();
		}

		IRow ISheet.GetRow(int rownum)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetColumnHidden(int columnIndex, bool hidden)
		{
			throw new NotImplementedException();
		}

		bool ISheet.IsColumnHidden(int columnIndex)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetColumnWidth(int columnIndex, int width)
		{
			throw new NotImplementedException();
		}

		int ISheet.GetColumnWidth(int columnIndex)
		{
			throw new NotImplementedException();
		}

		ICellStyle ISheet.GetColumnStyle(int column)
		{
			throw new NotImplementedException();
		}

		int ISheet.AddMergedRegion(CellRangeAddress region)
		{
			throw new NotImplementedException();
		}

		void ISheet.RemoveMergedRegion(int index)
		{
			throw new NotImplementedException();
		}

		CellRangeAddress ISheet.GetMergedRegion(int index)
		{
			throw new NotImplementedException();
		}

		IEnumerator ISheet.GetRowEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator ISheet.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		double ISheet.GetMargin(MarginType margin)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetMargin(MarginType margin, double size)
		{
			throw new NotImplementedException();
		}

		void ISheet.ProtectSheet(string password)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetZoom(int numerator, int denominator)
		{
			throw new NotImplementedException();
		}

		void ISheet.ShowInPane(short toprow, short leftcol)
		{
			throw new NotImplementedException();
		}

		void ISheet.ShiftRows(int startRow, int endRow, int n)
		{
			throw new NotImplementedException();
		}

		void ISheet.ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight)
		{
			throw new NotImplementedException();
		}

		void ISheet.CreateFreezePane(int colSplit, int rowSplit, int leftmostColumn, int topRow)
		{
			throw new NotImplementedException();
		}

		void ISheet.CreateFreezePane(int colSplit, int rowSplit)
		{
			throw new NotImplementedException();
		}

		void ISheet.CreateSplitPane(int xSplitPos, int ySplitPos, int leftmostColumn, int topRow, PanePosition activePane)
		{
			throw new NotImplementedException();
		}

		bool ISheet.IsRowBroken(int row)
		{
			throw new NotImplementedException();
		}

		void ISheet.RemoveRowBreak(int row)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetActiveCell(int row, int column)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetActiveCellRange(int firstRow, int lastRow, int firstColumn, int lastColumn)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetActiveCellRange(List<CellRangeAddress8Bit> cellranges, int activeRange, int activeRow, int activeColumn)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetColumnBreak(int column)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetRowBreak(int row)
		{
			throw new NotImplementedException();
		}

		bool ISheet.IsColumnBroken(int column)
		{
			throw new NotImplementedException();
		}

		void ISheet.RemoveColumnBreak(int column)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetColumnGroupCollapsed(int columnNumber, bool collapsed)
		{
			throw new NotImplementedException();
		}

		void ISheet.GroupColumn(int fromColumn, int toColumn)
		{
			throw new NotImplementedException();
		}

		void ISheet.UngroupColumn(int fromColumn, int toColumn)
		{
			throw new NotImplementedException();
		}

		void ISheet.GroupRow(int fromRow, int toRow)
		{
			throw new NotImplementedException();
		}

		void ISheet.UngroupRow(int fromRow, int toRow)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetRowGroupCollapsed(int row, bool collapse)
		{
			throw new NotImplementedException();
		}

		void ISheet.SetDefaultColumnStyle(int column, ICellStyle style)
		{
			throw new NotImplementedException();
		}

		void ISheet.AutoSizeColumn(int column)
		{
			throw new NotImplementedException();
		}

		void ISheet.AutoSizeColumn(int column, bool useMergedCells)
		{
			throw new NotImplementedException();
		}

		IComment ISheet.GetCellComment(int row, int column)
		{
			throw new NotImplementedException();
		}

		IDrawing ISheet.CreateDrawingPatriarch()
		{
			throw new NotImplementedException();
		}

		void ISheet.SetActive(bool sel)
		{
			throw new NotImplementedException();
		}

		ICellRange<ICell> ISheet.SetArrayFormula(string formula, CellRangeAddress range)
		{
			throw new NotImplementedException();
		}

		ICellRange<ICell> ISheet.RemoveArrayFormula(ICell cell)
		{
			throw new NotImplementedException();
		}

		bool ISheet.IsMergedRegion(CellRangeAddress mergedRegion)
		{
			throw new NotImplementedException();
		}

		IDataValidationHelper ISheet.GetDataValidationHelper()
		{
			throw new NotImplementedException();
		}

		void ISheet.AddValidationData(IDataValidation dataValidation)
		{
			throw new NotImplementedException();
		}

		IAutoFilter ISheet.SetAutoFilter(CellRangeAddress range)
		{
			throw new NotImplementedException();
		}
	}
}
