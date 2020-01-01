using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class SingleValueVector : ValueVector
	{
		private ValueEval _value;

		public int Size => 1;

		public SingleValueVector(ValueEval value)
		{
			_value = value;
		}

		public ValueEval GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentException("Invalid index (" + index + ") only zero is allowed");
			}
			return _value;
		}
	}
}
