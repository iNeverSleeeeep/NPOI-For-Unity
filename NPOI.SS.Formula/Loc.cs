using NPOI.Util;

namespace NPOI.SS.Formula
{
	public class Loc
	{
		private long _bookSheetColumn;

		private int _rowIndex;

		public int RowIndex => _rowIndex;

		public int ColumnIndex => (int)(_bookSheetColumn & 0xFFFF);

		public int SheetIndex => (int)((_bookSheetColumn >> 32) & 0xFFFF);

		public int BookIndex => (int)((_bookSheetColumn >> 48) & 0xFFFF);

		public Loc(int bookIndex, int sheetIndex, int rowIndex, int columnIndex)
		{
			_bookSheetColumn = ToBookSheetColumn(bookIndex, sheetIndex, columnIndex);
			_rowIndex = rowIndex;
		}

		public static long ToBookSheetColumn(int bookIndex, int sheetIndex, int columnIndex)
		{
			return (((long)bookIndex & 65535L) << 48) + (((long)sheetIndex & 65535L) << 32) + ((long)columnIndex & 65535L);
		}

		public Loc(int bookSheetColumn, int rowIndex)
		{
			_bookSheetColumn = bookSheetColumn;
			_rowIndex = rowIndex;
		}

		public override int GetHashCode()
		{
			return (int)(_bookSheetColumn ^ Operator.UnsignedRightShift(_bookSheetColumn, 32)) + 17 * _rowIndex;
		}

		public override bool Equals(object obj)
		{
			Loc loc = (Loc)obj;
			if (_bookSheetColumn == loc._bookSheetColumn)
			{
				return _rowIndex == loc._rowIndex;
			}
			return false;
		}
	}
}
