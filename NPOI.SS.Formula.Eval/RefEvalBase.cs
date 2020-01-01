namespace NPOI.SS.Formula.Eval
{
	public abstract class RefEvalBase : RefEval, ValueEval
	{
		private int _rowIndex;

		private int _columnIndex;

		public int Row => _rowIndex;

		public int Column => _columnIndex;

		public abstract ValueEval InnerValueEval
		{
			get;
		}

		protected RefEvalBase(int rowIndex, int columnIndex)
		{
			_rowIndex = rowIndex;
			_columnIndex = columnIndex;
		}

		public abstract AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx);
	}
}
