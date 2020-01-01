using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public class VARP : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			if (values.Length < 1)
			{
				throw new EvaluationException(ErrorEval.DIV_ZERO);
			}
			return StatsLib.varp(values);
		}
	}
}
