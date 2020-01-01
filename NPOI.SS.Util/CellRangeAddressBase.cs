using System;

namespace NPOI.SS.Util
{
	/// See OOO documentation: excelfileformat.pdf sec 2.5.14 - 'Cell Range Address'<p />
	///
	/// Common subclass of 8-bit and 16-bit versions
	///
	/// @author Josh Micich
	public abstract class CellRangeAddressBase
	{
		private int _firstRow;

		private int _firstCol;

		private int _lastRow;

		private int _lastCol;

		public bool IsFullColumnRange
		{
			get
			{
				if (_firstRow != 0 || _lastRow != SpreadsheetVersion.EXCEL97.LastRowIndex)
				{
					if (_firstRow == -1)
					{
						return _lastRow == -1;
					}
					return false;
				}
				return true;
			}
		}

		public bool IsFullRowRange
		{
			get
			{
				if (_firstCol != 0 || _lastCol != SpreadsheetVersion.EXCEL97.LastColumnIndex)
				{
					if (_firstCol == -1)
					{
						return _lastCol == -1;
					}
					return false;
				}
				return true;
			}
		}

		/// @return column number for the upper left hand corner
		public int FirstColumn
		{
			get
			{
				return _firstCol;
			}
			set
			{
				_firstCol = value;
			}
		}

		/// @return row number for the upper left hand corner
		public int FirstRow
		{
			get
			{
				return _firstRow;
			}
			set
			{
				_firstRow = value;
			}
		}

		/// @return column number for the lower right hand corner
		public int LastColumn
		{
			get
			{
				return _lastCol;
			}
			set
			{
				_lastCol = value;
			}
		}

		/// @return row number for the lower right hand corner
		public int LastRow
		{
			get
			{
				return _lastRow;
			}
			set
			{
				_lastRow = value;
			}
		}

		/// @return the size of the range (number of cells in the area).
		public int NumberOfCells => (_lastRow - _firstRow + 1) * (_lastCol - _firstCol + 1);

		protected CellRangeAddressBase(int firstRow, int lastRow, int firstCol, int lastCol)
		{
			_firstRow = firstRow;
			_lastRow = lastRow;
			_firstCol = firstCol;
			_lastCol = lastCol;
		}

		/// Validate the range limits against the supplied version of Excel
		///
		/// @param ssVersion the version of Excel to validate against
		/// @throws IllegalArgumentException if the range limits are outside of the allowed range
		public void Validate(SpreadsheetVersion ssVersion)
		{
			ValidateRow(_firstRow, ssVersion);
			ValidateRow(_lastRow, ssVersion);
			ValidateColumn(_firstCol, ssVersion);
			ValidateColumn(_lastCol, ssVersion);
		}

		/// Runs a bounds check for row numbers
		/// @param row
		private static void ValidateRow(int row, SpreadsheetVersion ssVersion)
		{
			int lastRowIndex = ssVersion.LastRowIndex;
			if (row > lastRowIndex)
			{
				throw new ArgumentException("Maximum row number is " + lastRowIndex);
			}
			if (row < 0)
			{
				throw new ArgumentException("Minumum row number is 0");
			}
		}

		/// Runs a bounds check for column numbers
		/// @param column
		private static void ValidateColumn(int column, SpreadsheetVersion ssVersion)
		{
			int lastColumnIndex = ssVersion.LastColumnIndex;
			if (column > lastColumnIndex)
			{
				throw new ArgumentException("Maximum column number is " + lastColumnIndex);
			}
			if (column < 0)
			{
				throw new ArgumentException("Minimum column number is 0");
			}
		}

		public bool IsInRange(int rowInd, int colInd)
		{
			if (_firstRow <= rowInd && rowInd <= _lastRow && _firstCol <= colInd)
			{
				return colInd <= _lastCol;
			}
			return false;
		}

		public override string ToString()
		{
			CellReference cellReference = new CellReference(_firstRow, _firstCol);
			CellReference cellReference2 = new CellReference(_lastRow, _lastCol);
			return GetType().Name + " [" + cellReference.FormatAsString() + ":" + cellReference2.FormatAsString() + "]";
		}
	}
}
