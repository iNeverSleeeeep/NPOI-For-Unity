using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Atp
{
	/// Implementation of Excel 'Analysis ToolPak' function NETWORKDAYS()<br />
	/// Returns the number of workdays given a starting and an ending date, considering an interval of holidays. A workday is any non
	/// saturday/sunday date.
	/// <p />
	/// <b>Syntax</b><br />
	/// <b>NETWORKDAYS</b>(<b>startDate</b>, <b>endDate</b>, holidays)
	/// <p />
	///
	/// @author jfaenomoto@gmail.com
	public class NetworkdaysFunction : FreeRefFunction
	{
		public static FreeRefFunction instance = new NetworkdaysFunction(ArgumentsEvaluator.instance);

		private ArgumentsEvaluator evaluator;

		/// Constructor.
		///
		/// @param anEvaluator an injected {@link ArgumentsEvaluator}.
		private NetworkdaysFunction(ArgumentsEvaluator anEvaluator)
		{
			evaluator = anEvaluator;
		}

		/// Evaluate for NETWORKDAYS. Given two dates and a optional date or interval of holidays, determines how many working days are there
		/// between those dates.
		///
		/// @return {@link ValueEval} for the number of days between two dates.
		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length >= 2 && args.Length <= 3)
			{
				int rowIndex = ec.RowIndex;
				int columnIndex = ec.ColumnIndex;
				try
				{
					double num = evaluator.EvaluateDateArg(args[0], rowIndex, columnIndex);
					double num2 = evaluator.EvaluateDateArg(args[1], rowIndex, columnIndex);
					if (num > num2)
					{
						return ErrorEval.NAME_INVALID;
					}
					ValueEval arg = (args.Length == 3) ? args[2] : null;
					double[] holidays = evaluator.EvaluateDatesArg(arg, rowIndex, columnIndex);
					return new NumberEval((double)WorkdayCalculator.instance.CalculateWorkdays(num, num2, holidays));
				}
				catch (EvaluationException)
				{
					return ErrorEval.VALUE_INVALID;
				}
			}
			return ErrorEval.VALUE_INVALID;
		}
	}
}
