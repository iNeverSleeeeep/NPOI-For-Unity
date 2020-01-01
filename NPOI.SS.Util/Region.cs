using System;

namespace NPOI.SS.Util
{
	/// Represents a from/to row/col square.  This is a object primitive
	/// that can be used to represent row,col - row,col just as one would use String
	/// to represent a string of characters.  Its really only useful for HSSF though.
	///
	/// @author  Andrew C. Oliver acoliver at apache dot org
	[Obsolete]
	public class Region
	{
		private int rowFrom;

		private int colFrom;

		private int rowTo;

		private int colTo;

		/// Get the upper left hand corner column number
		///
		/// @return column number for the upper left hand corner
		public int ColumnFrom
		{
			get
			{
				return colFrom;
			}
			set
			{
				colFrom = value;
			}
		}

		/// Get the upper left hand corner row number
		///
		/// @return row number for the upper left hand corner
		public int RowFrom
		{
			get
			{
				return rowFrom;
			}
			set
			{
				rowFrom = value;
			}
		}

		/// Get the lower right hand corner column number
		///
		/// @return column number for the lower right hand corner
		public int ColumnTo
		{
			get
			{
				return colTo;
			}
			set
			{
				colTo = value;
			}
		}

		/// Get the lower right hand corner row number
		///
		/// @return row number for the lower right hand corner
		public int RowTo
		{
			get
			{
				return rowTo;
			}
			set
			{
				rowTo = value;
			}
		}

		/// Creates a new instance of Region (0,0 - 0,0)
		public Region()
		{
		}

		public Region(int rowFrom, int colFrom, int rowTo, int colTo)
		{
			this.rowFrom = rowFrom;
			this.rowTo = rowTo;
			this.colFrom = colFrom;
			this.colTo = colTo;
		}

		private static Region ConvertToRegion(CellRangeAddress cr)
		{
			return new Region(cr.FirstRow, cr.FirstColumn, cr.LastRow, cr.LastColumn);
		}

		/// Convert a List of CellRange objects to an array of regions 
		///
		/// @param List of CellRange objects
		/// @return regions
		public static Region[] ConvertCellRangesToRegions(CellRangeAddress[] cellRanges)
		{
			int num = cellRanges.Length;
			if (num < 1)
			{
				return new Region[0];
			}
			Region[] array = new Region[num];
			for (int i = 0; i != num; i++)
			{
				array[i] = ConvertToRegion(cellRanges[i]);
			}
			return array;
		}

		public static CellRangeAddress[] ConvertRegionsToCellRanges(Region[] regions)
		{
			int num = regions.Length;
			if (num < 1)
			{
				return new CellRangeAddress[0];
			}
			CellRangeAddress[] array = new CellRangeAddress[num];
			for (int i = 0; i != num; i++)
			{
				array[i] = ConvertToCellRangeAddress(regions[i]);
			}
			return array;
		}

		public static CellRangeAddress ConvertToCellRangeAddress(Region r)
		{
			return new CellRangeAddress(r.RowFrom, r.RowTo, r.ColumnFrom, r.ColumnTo);
		}
	}
}
