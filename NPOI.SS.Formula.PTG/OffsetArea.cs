using System;

namespace NPOI.SS.Formula.PTG
{
	public class OffsetArea : AreaI
	{
		private int _firstColumn;

		private int _firstRow;

		private int _lastColumn;

		private int _lastRow;

		public int FirstColumn => _firstColumn;

		public int FirstRow => _firstRow;

		public int LastColumn => _lastColumn;

		public int LastRow => _lastRow;

		public OffsetArea(int baseRow, int baseColumn, int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx)
		{
			_firstRow = baseRow + Math.Min(relFirstRowIx, relLastRowIx);
			_lastRow = baseRow + Math.Max(relFirstRowIx, relLastRowIx);
			_firstColumn = baseColumn + Math.Min(relFirstColIx, relLastColIx);
			_lastColumn = baseColumn + Math.Max(relFirstColIx, relLastColIx);
		}
	}
}
