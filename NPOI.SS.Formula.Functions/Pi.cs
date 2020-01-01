using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Pi : Fixed0ArgFunction
	{
		private static readonly NumberEval PI_EVAL = new NumberEval(3.1415926535897931);

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return PI_EVAL;
		}
	}
}
