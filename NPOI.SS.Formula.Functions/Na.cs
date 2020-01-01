using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of Excel function NA()
	///
	/// @author Josh Micich
	public class Na : Fixed0ArgFunction
	{
		public override ValueEval Evaluate(int srcCellRow, int srcCellCol)
		{
			return ErrorEval.NA;
		}
	}
}
