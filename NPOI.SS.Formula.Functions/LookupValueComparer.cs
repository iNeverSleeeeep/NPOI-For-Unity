using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public interface LookupValueComparer
	{
		/// @return one of 4 instances or <c>CompareResult</c>: <c>LESS_THAN</c>, <c>EQUAL</c>, 
		/// <c>GREATER_THAN</c> or <c>TYPE_MISMATCH</c>
		CompareResult CompareTo(ValueEval other);
	}
}
