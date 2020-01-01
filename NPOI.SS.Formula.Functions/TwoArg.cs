using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public abstract class TwoArg : Fixed2ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double d2 = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				num = Evaluate(d, d2);
				NumericFunction.CheckValue(num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		public abstract double Evaluate(double d0, double d1);
	}
}
