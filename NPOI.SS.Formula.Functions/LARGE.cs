using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class LARGE : AggregateFunction
	{
		protected internal override double Evaluate(double[] ops)
		{
			if (ops.Length < 2)
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
			double[] array = new double[ops.Length - 1];
			int k = (int)ops[ops.Length - 1];
			Array.Copy(ops, 0, array, 0, array.Length);
			return StatsLib.kthLargest(array, k);
		}
	}
}
