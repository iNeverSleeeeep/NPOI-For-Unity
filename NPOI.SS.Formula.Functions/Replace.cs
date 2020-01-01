using NPOI.SS.Formula.Eval;
using System;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// An implementation of the Replace function:
	/// Replaces part of a text string based on the number of Chars 
	/// you specify, with another text string.
	/// @author Manda Wilson &lt; wilson at c bio dot msk cc dot org &gt;
	public class Replace : TextFunction
	{
		/// Replaces part of a text string based on the number of Chars 
		/// you specify, with another text string.
		///
		/// @see org.apache.poi.hssf.record.formula.eval.Eval
		public override ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length != 4)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string text = TextFunction.EvaluateStringArg(args[0], srcCellRow, srcCellCol);
			int num = TextFunction.EvaluateIntArg(args[1], srcCellRow, srcCellCol);
			int num2 = TextFunction.EvaluateIntArg(args[2], srcCellRow, srcCellCol);
			string value = TextFunction.EvaluateStringArg(args[3], srcCellRow, srcCellCol);
			if (num < 1 || num2 < 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			if (num <= text.Length && num2 != 0)
			{
				stringBuilder.Remove(num - 1, Math.Min(num2, text.Length - num + 1));
			}
			if (num > stringBuilder.Length)
			{
				stringBuilder.Append(value);
			}
			else
			{
				stringBuilder.Insert(num - 1, value);
			}
			return new StringEval(stringBuilder.ToString());
		}
	}
}
