using NPOI.SS.Formula.Eval;
using System;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// An implementation of the SUBSTITUTE function:
	/// Substitutes text in a text string with new text, some number of times.
	/// @author Manda Wilson &lt; wilson at c bio dot msk cc dot org &gt;
	public class Substitute : TextFunction
	{
		/// Substitutes text in a text string with new text, some number of times.
		///
		///  @see org.apache.poi.hssf.record.formula.eval.Eval
		public override ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length < 3 || args.Length > 4)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string oldStr = TextFunction.EvaluateStringArg(args[0], srcCellRow, srcCellCol);
			string searchStr = TextFunction.EvaluateStringArg(args[1], srcCellRow, srcCellCol);
			string newStr = TextFunction.EvaluateStringArg(args[2], srcCellRow, srcCellCol);
			string value;
			switch (args.Length)
			{
			case 4:
			{
				int num = TextFunction.EvaluateIntArg(args[3], srcCellRow, srcCellCol);
				if (num < 1)
				{
					return ErrorEval.VALUE_INVALID;
				}
				value = ReplaceOneOccurrence(oldStr, searchStr, newStr, num);
				break;
			}
			case 3:
				value = ReplaceAllOccurrences(oldStr, searchStr, newStr);
				break;
			default:
				throw new InvalidOperationException("Cannot happen");
			}
			return new StringEval(value);
		}

		private static string ReplaceAllOccurrences(string oldStr, string searchStr, string newStr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = -1;
			while (true)
			{
				num2 = oldStr.IndexOf(searchStr, num, StringComparison.CurrentCulture);
				if (num2 < 0)
				{
					break;
				}
				stringBuilder.Append(oldStr.Substring(num, num2 - num));
				stringBuilder.Append(newStr);
				num = num2 + searchStr.Length;
			}
			stringBuilder.Append(oldStr.Substring(num));
			return stringBuilder.ToString();
		}

		private static string ReplaceOneOccurrence(string oldStr, string searchStr, string newStr, int instanceNumber)
		{
			if (searchStr.Length < 1)
			{
				return oldStr;
			}
			int startIndex = 0;
			int num = -1;
			int num2 = 0;
			while (true)
			{
				num = oldStr.IndexOf(searchStr, startIndex, StringComparison.CurrentCulture);
				if (num < 0)
				{
					return oldStr;
				}
				num2++;
				if (num2 == instanceNumber)
				{
					break;
				}
				startIndex = num + searchStr.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(oldStr.Length + newStr.Length);
			stringBuilder.Append(oldStr.Substring(0, num));
			stringBuilder.Append(newStr);
			stringBuilder.Append(oldStr.Substring(num + searchStr.Length));
			return stringBuilder.ToString();
		}
	}
}
