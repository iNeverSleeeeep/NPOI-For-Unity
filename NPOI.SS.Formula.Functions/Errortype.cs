using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the ERROR.TYPE() Excel function.
	/// <p>
	/// <b>Syntax:</b><br />
	/// <b>ERROR.TYPE</b>(<b>errorValue</b>)</p>
	/// <p>
	/// Returns a number corresponding to the error type of the supplied argument.</p>
	/// <p>
	///    <table border="1" cellpadding="1" cellspacing="1" summary="Return values for ERROR.TYPE()">
	///      <tr><td>errorValue</td><td>Return Value</td></tr>
	///      <tr><td>#NULL!</td><td>1</td></tr>
	///      <tr><td>#DIV/0!</td><td>2</td></tr>
	///      <tr><td>#VALUE!</td><td>3</td></tr>
	///      <tr><td>#REF!</td><td>4</td></tr>
	///      <tr><td>#NAME?</td><td>5</td></tr>
	///      <tr><td>#NUM!</td><td>6</td></tr>
	///      <tr><td>#N/A!</td><td>7</td></tr>
	///      <tr><td>everything else</td><td>#N/A!</td></tr>
	///    </table>
	///
	/// Note - the results of ERROR.TYPE() are different to the constants defined in
	/// <tt>ErrorConstants</tt>.
	/// </p>
	///
	/// @author Josh Micich
	public class Errortype : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			try
			{
				OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				return ErrorEval.NA;
			}
			catch (EvaluationException ex)
			{
				int num = TranslateErrorCodeToErrorTypeValue(ex.GetErrorEval().ErrorCode);
				return new NumberEval((double)num);
			}
		}

		private int TranslateErrorCodeToErrorTypeValue(int errorCode)
		{
			switch (errorCode)
			{
			case 0:
				return 1;
			case 7:
				return 2;
			case 15:
				return 3;
			case 23:
				return 4;
			case 29:
				return 5;
			case 36:
				return 6;
			case 42:
				return 7;
			default:
				throw new ArgumentException("Invalid error code (" + errorCode + ")");
			}
		}
	}
}
