using NPOI.SS.Formula.PTG;
using System;

namespace NPOI.SS.Formula.Eval
{
	/// @author Josh Micich
	public abstract class AreaEvalBase : AreaEval, TwoDEval, ValueEval
	{
		private int _firstColumn;

		private int _firstRow;

		private int _lastColumn;

		private int _lastRow;

		private int _nColumns;

		private int _nRows;

		public int FirstColumn => _firstColumn;

		public int FirstRow => _firstRow;

		public int LastColumn => _lastColumn;

		public int LastRow => _lastRow;

		public bool IsColumn => _firstColumn == _lastColumn;

		public bool IsRow => _firstRow == _lastRow;

		public int Width => _lastColumn - _firstColumn + 1;

		public int Height => _lastRow - _firstRow + 1;

		protected AreaEvalBase(int firstRow, int firstColumn, int lastRow, int lastColumn)
		{
			_firstColumn = firstColumn;
			_firstRow = firstRow;
			_lastColumn = lastColumn;
			_lastRow = lastRow;
			_nColumns = _lastColumn - _firstColumn + 1;
			_nRows = _lastRow - _firstRow + 1;
		}

		protected AreaEvalBase(AreaI ptg)
		{
			_firstRow = ptg.FirstRow;
			_firstColumn = ptg.FirstColumn;
			_lastRow = ptg.LastRow;
			_lastColumn = ptg.LastColumn;
			_nColumns = _lastColumn - _firstColumn + 1;
			_nRows = _lastRow - _firstRow + 1;
		}

		public ValueEval GetValue(int row, int col)
		{
			return GetRelativeValue(row, col);
		}

		public bool Contains(int row, int col)
		{
			if (_firstRow <= row && _lastRow >= row && _firstColumn <= col)
			{
				return _lastColumn >= col;
			}
			return false;
		}

		public bool ContainsRow(int row)
		{
			if (_firstRow <= row)
			{
				return _lastRow >= row;
			}
			return false;
		}

		public bool ContainsColumn(int col)
		{
			if (_firstColumn <= col)
			{
				return _lastColumn >= col;
			}
			return false;
		}

		public ValueEval GetAbsoluteValue(int row, int col)
		{
			int num = row - _firstRow;
			int num2 = col - _firstColumn;
			if (num < 0 || num >= _nRows)
			{
				throw new ArgumentException("Specified row index (" + row + ") is outside the allowed range (" + _firstRow + ".." + _lastRow + ")");
			}
			if (num2 < 0 || num2 >= _nColumns)
			{
				throw new ArgumentException("Specified column index (" + col + ") is outside the allowed range (" + _firstColumn + ".." + col + ")");
			}
			return GetRelativeValue(num, num2);
		}

		public abstract ValueEval GetRelativeValue(int relativeRowIndex, int relativeColumnIndex);

		/// @return  whether cell at rowIndex and columnIndex is a subtotal.
		/// By default return false which means 'don't care about subtotals'
		public virtual bool IsSubTotal(int rowIndex, int columnIndex)
		{
			return false;
		}

		public abstract TwoDEval GetRow(int rowIndex);

		public abstract TwoDEval GetColumn(int columnIndex);

		public abstract AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx);
	}
}
