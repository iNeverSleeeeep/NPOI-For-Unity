using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.HSSF.Util
{
	/// <summary>
	/// Various utility functions that make working with a region of cells easier.
	/// @author Eric Pugh epugh@upstate.com
	/// </summary>
	public class HSSFRegionUtil
	{
		/// <summary>
		/// For setting the same property on many cells to the same value
		/// </summary>
		private class CellPropertySetter
		{
			private HSSFWorkbook _workbook;

			private string _propertyName;

			private short _propertyValue;

			public CellPropertySetter(HSSFWorkbook workbook, string propertyName, int value)
			{
				_workbook = workbook;
				_propertyName = propertyName;
				_propertyValue = (short)value;
			}

			public void SetProperty(IRow row, int column)
			{
				ICell cell = HSSFCellUtil.GetCell(row, column);
				HSSFCellUtil.SetCellStyleProperty(cell, _workbook, _propertyName, _propertyValue);
			}
		}

		private HSSFRegionUtil()
		{
		}

		[Obsolete]
		private static CellRangeAddress toCRA(Region region)
		{
			return Region.ConvertToCellRangeAddress(region);
		}

		/// <summary>
		/// Sets the left border for a region of cells by manipulating the cell style
		/// of the individual cells on the left
		/// </summary>
		/// <param name="border">The new border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetBorderLeft(BorderStyle border, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int firstColumn = region.FirstColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderLeft", (int)border);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(HSSFCellUtil.GetRow(i, sheet), firstColumn);
			}
		}

		/// <summary>
		/// Sets the leftBorderColor attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="color">The color of the border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetLeftBorderColor(int color, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int firstColumn = region.FirstColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "leftBorderColor", color);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(HSSFCellUtil.GetRow(i, sheet), firstColumn);
			}
		}

		/// <summary>
		/// Sets the borderRight attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="border">The new border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetBorderRight(BorderStyle border, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int lastColumn = region.LastColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderRight", (int)border);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(HSSFCellUtil.GetRow(i, sheet), lastColumn);
			}
		}

		/// <summary>
		/// Sets the rightBorderColor attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="color">The color of the border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The workbook that the region is on.</param>
		/// <param name="workbook">The sheet that the region is on.</param>
		public static void SetRightBorderColor(int color, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstRow = region.FirstRow;
			int lastRow = region.LastRow;
			int lastColumn = region.LastColumn;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "rightBorderColor", color);
			for (int i = firstRow; i <= lastRow; i++)
			{
				cellPropertySetter.SetProperty(HSSFCellUtil.GetRow(i, sheet), lastColumn);
			}
		}

		/// <summary>
		/// Sets the borderBottom attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="border">The new border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetBorderBottom(BorderStyle border, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int lastRow = region.LastRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderBottom", (int)border);
			IRow row = HSSFCellUtil.GetRow(lastRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// <summary>
		/// Sets the bottomBorderColor attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="color">The color of the border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetBottomBorderColor(int color, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int lastRow = region.LastRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "bottomBorderColor", color);
			IRow row = HSSFCellUtil.GetRow(lastRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// <summary>
		/// Sets the borderBottom attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="border">The new border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetBorderTop(BorderStyle border, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int firstRow = region.FirstRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "borderTop", (int)border);
			IRow row = HSSFCellUtil.GetRow(firstRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}

		/// <summary>
		/// Sets the topBorderColor attribute of the HSSFRegionUtil object
		/// </summary>
		/// <param name="color">The color of the border</param>
		/// <param name="region">The region that should have the border</param>
		/// <param name="sheet">The sheet that the region is on.</param>
		/// <param name="workbook">The workbook that the region is on.</param>
		public static void SetTopBorderColor(int color, CellRangeAddress region, HSSFSheet sheet, HSSFWorkbook workbook)
		{
			int firstColumn = region.FirstColumn;
			int lastColumn = region.LastColumn;
			int firstRow = region.FirstRow;
			CellPropertySetter cellPropertySetter = new CellPropertySetter(workbook, "topBorderColor", color);
			IRow row = HSSFCellUtil.GetRow(firstRow, sheet);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				cellPropertySetter.SetProperty(row, i);
			}
		}
	}
}
