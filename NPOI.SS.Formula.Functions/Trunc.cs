using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Trunc : Var1or2ArgFunction
	{
		private static NumberEval TRUNC_ARG2_DEFAULT = new NumberEval(0.0);

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			return Evaluate(srcRowIndex, srcColumnIndex, arg0, TRUNC_ARG2_DEFAULT);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num3;
			try
			{
				double num = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double y = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double num2 = Math.Pow(10.0, y);
				num3 = ((!(num < 0.0)) ? (Math.Floor(num * num2) / num2) : ((0.0 - Math.Floor((0.0 - num) * num2)) / num2));
				NumericFunction.CheckValue(num3);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num3);
		}
	}
}
