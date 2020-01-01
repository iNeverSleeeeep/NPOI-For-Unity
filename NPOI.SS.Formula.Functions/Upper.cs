using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Upper : SingleArgTextFunc
	{
		public override ValueEval Evaluate(string arg)
		{
			return new StringEval(arg.ToUpper());
		}
	}
}
