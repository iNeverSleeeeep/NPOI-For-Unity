using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Mod : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			if (d1 == 0.0)
			{
				throw new EvaluationException(ErrorEval.DIV_ZERO);
			}
			return MathX.mod(d0, d1);
		}
	}
}
