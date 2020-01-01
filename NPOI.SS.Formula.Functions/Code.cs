using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for Excel CODE () function.<p />
	/// <p />
	/// <b>Syntax</b>:<br /> <b>CODE   </b>(<b>text</b> )<br />
	/// <p />
	/// Returns a numeric code for the first character in a text string. The returned code corresponds to the character set used by your computer.
	/// <p />
	/// text The text for which you want the code of the first character.
	///
	/// @author cedric dot walter @ gmail dot com
	public class Code : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval textArg)
		{
			ValueEval singleValue;
			try
			{
				singleValue = OperandResolver.GetSingleValue(textArg, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			string text = OperandResolver.CoerceValueToString(singleValue);
			if (text.Length == 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return new StringEval(((int)text[0]).ToString());
		}
	}
}
