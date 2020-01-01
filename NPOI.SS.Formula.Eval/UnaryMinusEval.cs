using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class UnaryMinusEval : Fixed1ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function instance = new UnaryMinusEval();

		private UnaryMinusEval()
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
			return new NumberEval(0.0 - num);
		}
	}
}
