using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public class SimpleValueVector : ValueVector
	{
		private ValueEval[] _values;

		public int Size => _values.Length;

		public SimpleValueVector(ValueEval[] values)
		{
			_values = values;
		}

		public ValueEval GetItem(int index)
		{
			return _values[index];
		}
	}
}
