using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// Log: LOG(number,[base])
	public class Log : Var1or2ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			double num;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				num = Math.Log(d) / NumericFunction.LOG_10_TO_BASE_e;
				NumericFunction.CheckValue(num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num4;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double num2 = Math.Log(d);
				double num3 = num;
				num4 = ((num3 != 2.7182818284590451) ? (num2 / Math.Log(num3)) : num2);
				NumericFunction.CheckValue(num4);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num4);
		}
	}
}
