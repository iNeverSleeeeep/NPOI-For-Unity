using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class False : Fixed0ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return BoolEval.FALSE;
		}
	}
}
