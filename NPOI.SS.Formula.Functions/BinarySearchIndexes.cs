namespace NPOI.SS.Formula.Functions
{
	/// Encapsulates some standard binary search functionality so the Unusual Excel behaviour can
	/// be clearly distinguished. 
	internal class BinarySearchIndexes
	{
		private int _lowIx;

		private int _highIx;

		public BinarySearchIndexes(int highIx)
		{
			_lowIx = -1;
			_highIx = highIx;
		}

		/// @return -1 if the search range Is empty
		public int GetMidIx()
		{
			int num = _highIx - _lowIx;
			if (num < 2)
			{
				return -1;
			}
			return _lowIx + num / 2;
		}

		public int GetLowIx()
		{
			return _lowIx;
		}

		public int GetHighIx()
		{
			return _highIx;
		}

		public void NarrowSearch(int midIx, bool isLessThan)
		{
			if (isLessThan)
			{
				_highIx = midIx;
			}
			else
			{
				_lowIx = midIx;
			}
		}
	}
}
