using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// Implementation of Excel 'Analysis ToolPak' function WORKDAY()<br />
	/// Returns the date past a number of workdays beginning at a start date, considering an interval of holidays. A workday is any non
	/// saturday/sunday date.
	/// <p />
	/// <b>Syntax</b><br />
	/// <b>WORKDAY</b>(<b>startDate</b>, <b>days</b>, holidays)
	/// <p />
	///
	/// @author jfaenomoto@gmail.com
	internal class WorkdayFunction : FreeRefFunction
	{
		public static FreeRefFunction instance = new WorkdayFunction(ArgumentsEvaluator.instance);

		private ArgumentsEvaluator evaluator;

		private WorkdayFunction(ArgumentsEvaluator anEvaluator)
		{
			evaluator = anEvaluator;
		}

		/// Evaluate for WORKDAY. Given a date, a number of days and a optional date or interval of holidays, determines which date it is past
		/// number of parametrized workdays.
		///
		/// @return {@link ValueEval} with date as its value.
		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length >= 2 && args.Length <= 3)
			{
				int rowIndex = ec.RowIndex;
				int columnIndex = ec.ColumnIndex;
				try
				{
					double start = evaluator.EvaluateDateArg(args[0], rowIndex, columnIndex);
					int workdays = (int)Math.Floor(evaluator.EvaluateNumberArg(args[1], rowIndex, columnIndex));
					ValueEval arg = (args.Length == 3) ? args[2] : null;
					double[] holidays = evaluator.EvaluateDatesArg(arg, rowIndex, columnIndex);
					return new NumberEval(DateUtil.GetExcelDate(WorkdayCalculator.instance.CalculateWorkdays(start, workdays, holidays)));
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
