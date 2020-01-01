using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class SearchFind : Var2or3ArgFunction
	{
		private bool _isCaseSensitive;

		public SearchFind(bool isCaseSensitive)
		{
			_isCaseSensitive = isCaseSensitive;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			try
			{
				string needle = TextFunction.EvaluateStringArg(arg0, srcRowIndex, srcColumnIndex);
				string haystack = TextFunction.EvaluateStringArg(arg1, srcRowIndex, srcColumnIndex);
				return Eval(haystack, needle, 0);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			try
			{
				string needle = TextFunction.EvaluateStringArg(arg0, srcRowIndex, srcColumnIndex);
				string haystack = TextFunction.EvaluateStringArg(arg1, srcRowIndex, srcColumnIndex);
				int num = TextFunction.EvaluateIntArg(arg2, srcRowIndex, srcColumnIndex) - 1;
				if (num < 0)
				{
					return ErrorEval.VALUE_INVALID;
				}
				return Eval(haystack, needle, num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		private ValueEval Eval(string haystack, string needle, int startIndex)
		{
			int num = (!_isCaseSensitive) ? haystack.IndexOf(needle, startIndex, StringComparison.CurrentCultureIgnoreCase) : haystack.IndexOf(needle, startIndex, StringComparison.CurrentCulture);
			if (num == -1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return new NumberEval((double)(num + 1));
		}
	}
}
