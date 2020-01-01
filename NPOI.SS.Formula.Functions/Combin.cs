using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Combin : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			if (d0 > 2147483647.0 || d1 > 2147483647.0)
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
			return MathX.nChooseK((int)d0, (int)d1);
		}
	}
}
