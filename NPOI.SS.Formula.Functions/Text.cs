using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// An implementation of the TEXT function
	/// TEXT returns a number value formatted with the given number formatting string. 
	/// This function is not a complete implementation of the Excel function, but
	///  handles most of the common cases. All work is passed down to 
	///  {@link DataFormatter} to be done, as this works much the same as the
	///  display focused work that that does. 
	public class Text : Fixed2ArgFunction
	{
		public static DataFormatter Formatter = new DataFormatter();

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double value;
			string formatString;
			try
			{
				value = TextFunction.EvaluateDoubleArg(arg0, srcRowIndex, srcColumnIndex);
				formatString = TextFunction.EvaluateStringArg(arg1, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			try
			{
				string value2 = Formatter.FormatRawCellContents(value, -1, formatString);
				return new StringEval(value2);
			}
			catch (Exception)
			{
				return ErrorEval.VALUE_INVALID;
			}
		}
	}
}
