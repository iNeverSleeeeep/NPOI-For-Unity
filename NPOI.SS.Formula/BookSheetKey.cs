namespace NPOI.SS.Formula
{
	public class BookSheetKey
	{
		private int _bookIndex;

		private int _sheetIndex;

		public BookSheetKey(int bookIndex, int sheetIndex)
		{
			_bookIndex = bookIndex;
			_sheetIndex = sheetIndex;
		}

		public override int GetHashCode()
		{
			return _bookIndex * 17 + _sheetIndex;
		}

		public override bool Equals(object obj)
		{
			BookSheetKey bookSheetKey = (BookSheetKey)obj;
			if (_bookIndex == bookSheetKey._bookIndex)
			{
				return _sheetIndex == bookSheetKey._sheetIndex;
			}
			return false;
		}
	}
}
