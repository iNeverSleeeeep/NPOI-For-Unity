using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Common interface for the matching criteria.
	public interface IMatchPredicate
	{
		bool Matches(ValueEval x);
	}
}
