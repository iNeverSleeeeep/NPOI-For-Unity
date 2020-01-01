using NPOI.SS.Formula.Eval;
using System;
using System.Globalization;

namespace NPOI.SS.Formula.Functions
{
	public class CHAR : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			int num;
			try
			{
				num = TextFunction.EvaluateIntArg(arg0, srcRowIndex, srcColumnIndex);
				if (num < 0 || num >= 256)
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new StringEval(Convert.ToString((char)num, CultureInfo.CurrentCulture));
		}
	}
}
