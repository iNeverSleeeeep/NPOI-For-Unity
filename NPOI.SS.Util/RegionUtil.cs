using NPOI.SS.UserModel;

namespace NPOI.SS.Util
{
	/// Various utility functions that make working with a region of cells easier.
	///
	/// @author Eric Pugh epugh@upstate.com
	/// @author (secondary) Avinash Kewalramani akewalramani@accelrys.com
	public class RegionUtil
	{
		/// For setting the same property on many cells to the same value
		private class CellPropertySetter
		{
			private IWorkbook _workbook;

			private string _propertyName;

			private short _propertyValue;

			public CellPropertySetter(IWorkbook workbook, string propertyName, int value)
			{
				_workbook = workbook;
				_propertyName = propertyName;
				_propertyValue = (short)value;
			}

			public void SetProperty(IRow row, int column)
			{
				ICell cell = CellUtil.GetCell(row, column);
				CellUtil.SetCellStyleProperty(cell, _workbook, _propertyName, _propertyValue);
			}
		}

		private RegionUtil()
		{
		}

		/// Sets the left border for a region of cells by manipulating the cell style of the individual
		/// cells on the left
		///
		/// @param border The new border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetBorderLeft(int border, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int firstColumn = region.FirstColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderLeft", border);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(CellUtil.GetRow(i, sheet), firstColumn);
			}
		}

		/// Sets the leftBorderColor attribute of the RegionUtil object
		///
		/// @param color The color of the border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetLeftBorderColor(int color, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int firstColumn = region.FirstColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "leftBorderColor", color);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(CellUtil.GetRow(i, sheet), firstColumn);
			}
		}

		/// Sets the borderRight attribute of the RegionUtil object
		///
		/// @param border The new border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetBorderRight(int border, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int lastColumn = region.LastColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderRight", border);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(CellUtil.GetRow(i, sheet), lastColumn);
			}
		}

		/// Sets the rightBorderColor attribute of the RegionUtil object
		///
		/// @param color The color of the border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetRightBorderColor(int color, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int lastColumn = region.LastColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "rightBorderColor", color);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(CellUtil.GetRow(i, sheet), lastColumn);
			}
		}

		/// Sets the borderBottom attribute of the RegionUtil object
		///
		/// @param border The new border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetBorderBottom(int border, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int lastRow = region.LastRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderBottom", border);
			IRow row = CellUtil.GetRow(lastRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// Sets the bottomBorderColor attribute of the RegionUtil object
		///
		/// @param color The color of the border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetBottomBorderColor(int color, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int lastRow = region.LastRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "bottomBorderColor", color);
			IRow row = CellUtil.GetRow(lastRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// Sets the borderBottom attribute of the RegionUtil object
		///
		/// @param border The new border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetBorderTop(int border, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int firstRow = region.FirstRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderTop", border);
			IRow row = CellUtil.GetRow(firstRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// Sets the topBorderColor attribute of the RegionUtil object
		///
		/// @param color The color of the border
		/// @param region The region that should have the border
		/// @param workbook The workbook that the region is on.
		/// @param sheet The sheet that the region is on.
		public static void SetTopBorderColor(int color, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int firstRow = region.FirstRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "topBorderColor", color);
			IRow row = CellUtil.GetRow(firstRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}
	}
}
