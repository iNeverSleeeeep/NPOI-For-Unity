using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	internal class BooleanLookupComparer : LookupValueComparerBase
	{
		private bool _value;

		public BooleanLookupComparer(BoolEval be)
			: base(be)
		{
			_value = be.BooleanValue;
		}

		protected override CompareResult CompareSameType(ValueEval other)
		{
			BoolEval boolEval = (BoolEval)other;
			bool booleanValue = boolEval.BooleanValue;
			if (_value == booleanValue)
			{
				return CompareResult.Equal;
			}
			if (_value)
			{
				return CompareResult.GreaterThan;
			}
			return CompareResult.LessThan;
		}

		protected override string GetValueAsString()
		{
			return _value.ToString();
		}
	}
}
