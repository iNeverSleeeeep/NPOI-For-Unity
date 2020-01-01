using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// <summary>
	/// An implementation of the MID function
	/// MID returns a specific number of
	/// Chars from a text string, starting at the specified position.
	/// @author Manda Wilson &lt; wilson at c bio dot msk cc dot org;
	/// </summary>
	public class Mid : TextFunction
	{
		public override ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length != 3)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string text = TextFunction.EvaluateStringArg(args[0], srcCellRow, srcCellCol);
			int num = TextFunction.EvaluateIntArg(args[1], srcCellRow, srcCellCol);
			int num2 = TextFunction.EvaluateIntArg(args[2], srcCellRow, srcCellCol);
			int num3 = num - 1;
			if (num3 < 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			if (num2 < 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			int length = text.Length;
			if (num2 < 0 || num3 > length)
			{
				return new StringEval("");
			}
			int num4 = Math.Min(num3 + num2, length);
			string value = text.Substring(num3, num4 - num3);
			return new StringEval(value);
		}
	}
}
