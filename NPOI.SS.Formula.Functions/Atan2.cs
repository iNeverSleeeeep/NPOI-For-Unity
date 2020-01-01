using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Atan2 : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			if (d0 == 0.0 && d1 == 0.0)
			{
				throw new EvaluationException(ErrorEval.DIV_ZERO);
			}
			return Math.Atan2(d1, d0);
		}
	}
}
