using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Isnontext : LogicalFunction
	{
		protected override bool Evaluate(ValueEval arg)
		{
			return !(arg is StringEval);
		}
	}
}
