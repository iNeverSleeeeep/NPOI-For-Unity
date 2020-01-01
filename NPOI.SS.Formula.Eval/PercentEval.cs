using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Eval
{
	/// Implementation of Excel formula token '%'. <p />
	/// @author Josh Micich
	public class PercentEval : Fixed1ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function instance = new PercentEval();

		private PercentEval()
		{
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			double num;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				num = OperandResolver.CoerceValueToDouble(singleValue);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (num == 0.0)
			{
				return NumberEval.ZERO;
			}
			return new NumberEval(num / 100.0);
		}
	}
}
