using NPOI.SS.Formula.Eval;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for Excel REPT () function.<p />
	/// <p />
	/// <b>Syntax</b>:<br /> <b>REPT  </b>(<b>text</b>,<b>number_times</b> )<br />
	/// <p />
	/// Repeats text a given number of times. Use REPT to fill a cell with a number of instances of a text string.
	///
	/// text : text The text that you want to repeat.
	/// number_times:	A positive number specifying the number of times to repeat text.
	///
	/// If number_times is 0 (zero), REPT returns "" (empty text).
	/// If this argument contains a decimal value, this function ignores the numbers to the right side of the decimal point.
	///
	/// The result of the REPT function cannot be longer than 32,767 characters, or REPT returns #VALUE!.
	///
	/// @author cedric dot walter @ gmail dot com
	public class Rept : Fixed2ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval text, ValueEval number_times)
		{
			ValueEval singleValue;
			try
			{
				singleValue = OperandResolver.GetSingleValue(text, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			string text2 = OperandResolver.CoerceValueToString(singleValue);
			double num = 0.0;
			try
			{
				num = OperandResolver.CoerceValueToDouble(number_times);
			}
			catch (EvaluationException)
			{
				return ErrorEval.VALUE_INVALID;
			}
			int num2 = (int)num;
			StringBuilder stringBuilder = new StringBuilder(text2.Length * num2);
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.Append(text2);
			}
			if (stringBuilder.ToString().Length > 32767)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return new StringEval(stringBuilder.ToString());
		}
	}
}
