using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Days360 : Var2or3ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double value;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double d2 = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				value = Evaluate(d, d2);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			double value;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double d2 = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				ValueEval singleValue = OperandResolver.GetSingleValue(arg2, srcRowIndex, srcColumnIndex);
				OperandResolver.CoerceValueToBoolean(singleValue, stringsAreBlanks: false);
				value = Evaluate(d, d2);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}

		private double Evaluate(double d0, double d1)
		{
			DateTime startingDate = GetStartingDate(d0);
			DateTime endingDateAccordingToStartingDate = GetEndingDateAccordingToStartingDate(d1, startingDate);
			long num = startingDate.Month * 30 + startingDate.Day;
			long num2 = (endingDateAccordingToStartingDate.Year - startingDate.Year) * 360 + endingDateAccordingToStartingDate.Month * 30 + endingDateAccordingToStartingDate.Day;
			return (double)(num2 - num);
		}

		private DateTime GetDate(double date)
		{
			return DateUtil.GetJavaDate(date);
		}

		private DateTime GetStartingDate(double date)
		{
			DateTime dateTime = GetDate(date);
			if (IsLastDayOfMonth(dateTime))
			{
				dateTime = new DateTime(dateTime.Year, dateTime.Month, 30, dateTime.Hour, dateTime.Minute, dateTime.Second);
			}
			return dateTime;
		}

		private DateTime GetEndingDateAccordingToStartingDate(double date, DateTime startingDate)
		{
			DateTime dateTime = DateUtil.GetJavaDate(date, use1904windowing: false);
			if (IsLastDayOfMonth(dateTime) && startingDate.Day < 30)
			{
				dateTime = GetFirstDayOfNextMonth(dateTime);
			}
			return dateTime;
		}

		private bool IsLastDayOfMonth(DateTime date)
		{
			return date.AddDays(1.0).Month != date.Month;
		}

		private DateTime GetFirstDayOfNextMonth(DateTime date)
		{
			return (date.Month < 12) ? new DateTime(date.Year, date.Month + 1, 1, date.Hour, date.Minute, date.Second) : new DateTime(date.Year + 1, 1, 1, date.Hour, date.Minute, date.Second);
		}
	}
}
