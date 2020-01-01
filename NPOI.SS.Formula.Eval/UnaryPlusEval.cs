using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class UnaryPlusEval : Fixed1ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function instance = new UnaryPlusEval();

		private UnaryPlusEval()
		{
		}

		public override ValueEval Evaluate(int srcCellRow, int srcCellCol, ValueEval arg0)
		{
			double value;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcCellRow, srcCellCol);
				if (singleValue is BlankEval)
				{
					return NumberEval.ZERO;
				}
				if (singleValue is StringEval)
				{
					return singleValue;
				}
				value = OperandResolver.CoerceValueToDouble(singleValue);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}
	}
}
