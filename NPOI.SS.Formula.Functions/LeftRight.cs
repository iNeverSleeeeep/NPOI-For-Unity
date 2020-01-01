using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class LeftRight : Var1or2ArgFunction
	{
		private static ValueEval DEFAULT_ARG1 = new NumberEval(1.0);

		private bool _isLeft;

		public LeftRight(bool isLeft)
		{
			_isLeft = isLeft;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			return Evaluate(srcRowIndex, srcColumnIndex, arg0, DEFAULT_ARG1);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			string text;
			int num;
			try
			{
				text = TextFunction.EvaluateStringArg(arg0, srcRowIndex, srcColumnIndex);
				num = TextFunction.EvaluateIntArg(arg1, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (num < 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string value = (!_isLeft) ? text.Substring(Math.Max(0, text.Length - num)) : text.Substring(0, Math.Min(text.Length, num));
			return new StringEval(value);
		}
	}
}
