using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of Excel functions Date parsing functions:
	///  Date - DAY, MONTH and YEAR
	///  Time - HOUR, MINUTE and SECOND
	///
	/// @author Others (not mentioned in code)
	/// @author Thies Wellpott
	public class CalendarFieldFunction : Fixed1ArgFunction
	{
		public const int YEAR_ID = 1;

		public const int MONTH_ID = 2;

		public const int DAY_OF_MONTH_ID = 3;

		public const int HOUR_OF_DAY_ID = 4;

		public const int MINUTE_ID = 5;

		public const int SECOND_ID = 6;

		public static readonly Function YEAR = new CalendarFieldFunction(1);

		public static readonly Function MONTH = new CalendarFieldFunction(2);

		public static readonly Function DAY = new CalendarFieldFunction(3);

		public static readonly Function HOUR = new CalendarFieldFunction(4);

		public static readonly Function MINUTE = new CalendarFieldFunction(5);

		public static readonly Function SECOND = new CalendarFieldFunction(6);

		private int _dateFieldId;

		private CalendarFieldFunction(int dateFieldId)
		{
			_dateFieldId = dateFieldId;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			double num;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				num = OperandResolver.CoerceValueToDouble(singleValue);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (num < 0.0)
			{
				return ErrorEval.NUM_ERROR;
			}
			return new NumberEval((double)GetCalField(num));
		}

		private int GetCalField(double serialDate)
		{
			if ((int)serialDate == 0)
			{
				switch (_dateFieldId)
				{
				case 1:
					return 1900;
				case 2:
					return 1;
				case 3:
					return 0;
				}
			}
			DateTime javaCalendar = DateUtil.GetJavaCalendar(serialDate + 5.78125E-06, use1904windowing: false);
			int result = 0;
			if (_dateFieldId == 1)
			{
				result = javaCalendar.Year;
			}
			else if (_dateFieldId == 2)
			{
				result = javaCalendar.Month;
			}
			else if (_dateFieldId == 3)
			{
				result = javaCalendar.Day;
			}
			else if (_dateFieldId == 4)
			{
				result = javaCalendar.Hour;
			}
			else if (_dateFieldId == 5)
			{
				result = javaCalendar.Minute;
			}
			else if (_dateFieldId == 6)
			{
				result = javaCalendar.Second;
			}
			return result;
		}
	}
}
