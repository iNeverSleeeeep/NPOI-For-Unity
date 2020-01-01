using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// A calculator for workdays, considering dates as excel representations.
	///
	/// @author jfaenomoto@gmail.com
	public class WorkdayCalculator
	{
		public static WorkdayCalculator instance = new WorkdayCalculator();

		/// Constructor.
		private WorkdayCalculator()
		{
		}

		/// Calculate how many workdays are there between a start and an end date, as excel representations, considering a range of holidays.
		///
		/// @param start start date.
		/// @param end end date.
		/// @param holidays an array of holidays.
		/// @return number of workdays between start and end dates, including both dates.
		public int CalculateWorkdays(double start, double end, double[] holidays)
		{
			int num = PastDaysOfWeek(start, end, DayOfWeek.Saturday);
			int num2 = PastDaysOfWeek(start, end, DayOfWeek.Sunday);
			int num3 = CalculateNonWeekendHolidays(start, end, holidays);
			return (int)(end - start + 1.0) - num - num2 - num3;
		}

		/// Calculate the workday past x workdays from a starting date, considering a range of holidays.
		///
		/// @param start start date.
		/// @param workdays number of workdays to be past from starting date.
		/// @param holidays an array of holidays.
		/// @return date past x workdays.
		public DateTime CalculateWorkdays(double start, int workdays, double[] holidays)
		{
			DateTime dateTime = DateUtil.GetJavaDate(start).AddDays((double)workdays);
			int num = 0;
			do
			{
				double excelDate = DateUtil.GetExcelDate(dateTime);
				int num2 = PastDaysOfWeek(start, excelDate, DayOfWeek.Saturday);
				int num3 = PastDaysOfWeek(start, excelDate, DayOfWeek.Sunday);
				int num4 = CalculateNonWeekendHolidays(start, excelDate, holidays);
				num = num2 + num3 + num4;
				dateTime = dateTime.AddDays((double)num);
				start = excelDate + (double)IsNonWorkday(excelDate, holidays);
			}
			while (num != 0);
			return dateTime;
		}

		/// Calculates how many days of week past between a start and an end date.
		///
		/// @param start start date.
		/// @param end end date.
		/// @param dayOfWeek a day of week as represented by {@link Calendar} constants.
		/// @return how many days of week past in this interval.
		public int PastDaysOfWeek(double start, double end, DayOfWeek dayOfWeek)
		{
			int num = 0;
			int i = (int)Math.Floor((start < end) ? start : end);
			for (int num2 = (int)Math.Floor((end > start) ? end : start); i <= num2; i++)
			{
				if (DateUtil.GetJavaDate((double)i).DayOfWeek == dayOfWeek)
				{
					num++;
				}
			}
			if (!(start < end))
			{
				return -num;
			}
			return num;
		}

		/// Calculates how many holidays in a list are workdays, considering an interval of dates.
		///
		/// @param start start date.
		/// @param end end date.
		/// @param holidays an array of holidays.
		/// @return number of holidays that occur in workdays, between start and end dates.
		private int CalculateNonWeekendHolidays(double start, double end, double[] holidays)
		{
			int num = 0;
			double start2 = (start < end) ? start : end;
			double end2 = (end > start) ? end : start;
			for (int i = 0; i < holidays.Length; i++)
			{
				if (IsInARange(start2, end2, holidays[i]) && !IsWeekend(holidays[i]))
				{
					num++;
				}
			}
			if (!(start < end))
			{
				return -num;
			}
			return num;
		}

		/// @param aDate a given date.
		/// @return <code>true</code> if date is weekend, <code>false</code> otherwise.
		private bool IsWeekend(double aDate)
		{
			DateTime javaDate = DateUtil.GetJavaDate(aDate);
			if (javaDate.DayOfWeek != DayOfWeek.Saturday)
			{
				return javaDate.DayOfWeek == DayOfWeek.Sunday;
			}
			return true;
		}

		/// @param aDate a given date.
		/// @param holidays an array of holidays.
		/// @return <code>true</code> if date is a holiday, <code>false</code> otherwise.
		private bool IsHoliday(double aDate, double[] holidays)
		{
			for (int i = 0; i < holidays.Length; i++)
			{
				if (Math.Round(holidays[i]) == Math.Round(aDate))
				{
					return true;
				}
			}
			return false;
		}

		/// @param aDate a given date.
		/// @param holidays an array of holidays.
		/// @return <code>1</code> is not a workday, <code>0</code> otherwise.
		private int IsNonWorkday(double aDate, double[] holidays)
		{
			if (!IsWeekend(aDate) && !IsHoliday(aDate, holidays))
			{
				return 0;
			}
			return 1;
		}

		/// @param start start date.
		/// @param end end date.
		/// @param aDate a date to be analyzed.
		/// @return <code>true</code> if aDate is between start and end dates, <code>false</code> otherwise.
		private bool IsInARange(double start, double end, double aDate)
		{
			if (aDate >= start)
			{
				return aDate <= end;
			}
			return false;
		}
	}
}
