using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Isref : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			if (arg0 is RefEval || arg0 is AreaEval)
			{
				return BoolEval.TRUE;
			}
			return BoolEval.FALSE;
		}
	}
}
