using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// <summary>
	/// Internal calculation methods for Excel 'Analysis ToolPak' function YEARFRAC()
	/// Algorithm inspired by www.dwheeler.com/yearfrac
	/// @author Josh Micich
	/// </summary>
	/// <remarks>
	/// Date Count convention 
	/// http://en.wikipedia.org/wiki/Day_count_convention
	/// </remarks>
	/// <remarks>
	/// Office Online Help on YEARFRAC
	/// http://office.microsoft.com/en-us/excel/HP052093441033.aspx
	/// </remarks>
	public class YearFracCalculator
	{
		/// <summary>
		/// Simple Date Wrapper
		/// </summary>
		private class SimpleDate
		{
			public const int JANUARY = 1;

			public const int FEBRUARY = 2;

			public int year;

			/// 1-based month 
			public int month;

			/// day of month 
			public int day;

			/// milliseconds since 1970 
			public long ticks;

			public SimpleDate(DateTime date)
			{
				year = date.Year;
				month = date.Month;
				day = date.Day;
				ticks = date.Ticks;
			}
		}

		/// use UTC time-zone to avoid daylight savings issues 
		private const int MS_PER_HOUR = 3600000;

		private const int MS_PER_DAY = 86400000;

		private const int DAYS_PER_NORMAL_YEAR = 365;

		private const int DAYS_PER_LEAP_YEAR = 366;

		/// the length of normal long months i.e. 31 
		private const int LONG_MONTH_LEN = 31;

		/// the length of normal short months i.e. 30 
		private const int SHORT_MONTH_LEN = 30;

		private const int SHORT_FEB_LEN = 28;

		private const int LONG_FEB_LEN = 29;

		/// <summary>
		/// Calculates YEARFRAC()
		/// </summary>
		/// <param name="pStartDateVal">The start date.</param>
		/// <param name="pEndDateVal">The end date.</param>
		/// <param name="basis">The basis value.</param>
		/// <returns></returns>
		public static double Calculate(double pStartDateVal, double pEndDateVal, int basis)
		{
			if (basis < 0 || basis >= 5)
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
			int num = (int)Math.Floor(pStartDateVal);
			int num2 = (int)Math.Floor(pEndDateVal);
			if (num != num2)
			{
				if (num > num2)
				{
					int num3 = num;
					num = num2;
					num2 = num3;
				}
				switch (basis)
				{
				case 0:
					return Basis0(num, num2);
				case 1:
					return Basis1(num, num2);
				case 2:
					return Basis2(num, num2);
				case 3:
					return Basis3((double)num, (double)num2);
				case 4:
					return Basis4(num, num2);
				default:
					throw new InvalidOperationException("cannot happen");
				}
			}
			return 0.0;
		}

		/// <summary>
		/// Basis 0, 30/360 date convention 
		/// </summary>
		/// <param name="startDateVal">The start date value assumed to be less than or equal to endDateVal.</param>
		/// <param name="endDateVal">The end date value assumed to be greater than or equal to startDateVal.</param>
		/// <returns></returns>
		public static double Basis0(int startDateVal, int endDateVal)
		{
			SimpleDate simpleDate = CreateDate(startDateVal);
			SimpleDate simpleDate2 = CreateDate(endDateVal);
			int num = simpleDate.day;
			int num2 = simpleDate2.day;
			if (num != 31 || num2 != 31)
			{
				switch (num)
				{
				case 31:
					num = 30;
					break;
				case 30:
					if (num2 == 31)
					{
						num2 = 30;
						break;
					}
					goto default;
				default:
					if (simpleDate.month == 2 && IsLastDayOfMonth(simpleDate))
					{
						num = 30;
						if (simpleDate2.month == 2 && IsLastDayOfMonth(simpleDate2))
						{
							num2 = 30;
						}
					}
					break;
				}
			}
			else
			{
				num = 30;
				num2 = 30;
			}
			return CalculateAdjusted(simpleDate, simpleDate2, num, num2);
		}

		/// <summary>
		/// Basis 1, Actual/Actual date convention 
		/// </summary>
		/// <param name="startDateVal">The start date value assumed to be less than or equal to endDateVal.</param>
		/// <param name="endDateVal">The end date value assumed to be greater than or equal to startDateVal.</param>
		/// <returns></returns>
		public static double Basis1(int startDateVal, int endDateVal)
		{
			SimpleDate simpleDate = CreateDate(startDateVal);
			SimpleDate simpleDate2 = CreateDate(endDateVal);
			double num = IsGreaterThanOneYear(simpleDate, simpleDate2) ? AverageYearLength(simpleDate.year, simpleDate2.year) : ((!ShouldCountFeb29(simpleDate, simpleDate2)) ? 365.0 : 366.0);
			return DateDiff(simpleDate.ticks, simpleDate2.ticks) / num;
		}

		/// <summary>
		/// Basis 2, Actual/360 date convention 
		/// </summary>
		/// <param name="startDateVal">The start date value assumed to be less than or equal to endDateVal.</param>
		/// <param name="endDateVal">The end date value assumed to be greater than or equal to startDateVal.</param>
		/// <returns></returns>
		public static double Basis2(int startDateVal, int endDateVal)
		{
			return (double)(endDateVal - startDateVal) / 360.0;
		}

		/// <summary>
		/// Basis 3, Actual/365 date convention 
		/// </summary>
		/// <param name="startDateVal">The start date value assumed to be less than or equal to endDateVal.</param>
		/// <param name="endDateVal">The end date value assumed to be greater than or equal to startDateVal.</param>
		/// <returns></returns>
		public static double Basis3(double startDateVal, double endDateVal)
		{
			return (endDateVal - startDateVal) / 365.0;
		}

		/// <summary>
		/// Basis 4, European 30/360 date convention 
		/// </summary>
		/// <param name="startDateVal">The start date value assumed to be less than or equal to endDateVal.</param>
		/// <param name="endDateVal">The end date value assumed to be greater than or equal to startDateVal.</param>
		/// <returns></returns>
		public static double Basis4(int startDateVal, int endDateVal)
		{
			SimpleDate simpleDate = CreateDate(startDateVal);
			SimpleDate simpleDate2 = CreateDate(endDateVal);
			int num = simpleDate.day;
			int num2 = simpleDate2.day;
			if (num == 31)
			{
				num = 30;
			}
			if (num2 == 31)
			{
				num2 = 30;
			}
			return CalculateAdjusted(simpleDate, simpleDate2, num, num2);
		}

		/// <summary>
		/// Calculates the adjusted.
		/// </summary>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <param name="date1day">The date1day.</param>
		/// <param name="date2day">The date2day.</param>
		/// <returns></returns>
		private static double CalculateAdjusted(SimpleDate startDate, SimpleDate endDate, int date1day, int date2day)
		{
			double num = (double)((endDate.year - startDate.year) * 360 + (endDate.month - startDate.month) * 30 + (date2day - date1day));
			return num / 360.0;
		}

		/// <summary>
		/// Determines whether [is last day of month] [the specified date].
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns>
		/// 	<c>true</c> if [is last day of month] [the specified date]; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsLastDayOfMonth(SimpleDate date)
		{
			if (date.day < 28)
			{
				return false;
			}
			return date.day == GetLastDayOfMonth(date);
		}

		/// <summary>
		/// Gets the last day of month.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		private static int GetLastDayOfMonth(SimpleDate date)
		{
			switch (date.month)
			{
			case 1:
			case 3:
			case 5:
			case 7:
			case 8:
			case 10:
			case 12:
				return 31;
			case 4:
			case 6:
			case 9:
			case 11:
				return 30;
			default:
				if (IsLeapYear(date.year))
				{
					return 29;
				}
				return 28;
			}
		}

		/// <summary>
		/// Assumes dates are no more than 1 year apart.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns><c>true</c>
		///  if dates both within a leap year, or span a period including Feb 29</returns>
		private static bool ShouldCountFeb29(SimpleDate start, SimpleDate end)
		{
			bool flag = IsLeapYear(start.year);
			if (flag && start.year == end.year)
			{
				return true;
			}
			bool flag2 = IsLeapYear(end.year);
			if (!flag && !flag2)
			{
				return false;
			}
			if (flag)
			{
				switch (start.month)
				{
				case 1:
				case 2:
					return true;
				default:
					return false;
				}
			}
			if (flag2)
			{
				switch (end.month)
				{
				case 1:
					return false;
				default:
					return true;
				case 2:
					return end.day == 29;
				}
			}
			return false;
		}

		/// <summary>
		/// return the whole number of days between the two time-stamps.  Both time-stamps are
		/// assumed to represent 12:00 midnight on the respective day.
		/// </summary>
		/// <param name="startDateTicks">The start date ticks.</param>
		/// <param name="endDateTicks">The end date ticks.</param>
		/// <returns></returns>
		private static double DateDiff(long startDateTicks, long endDateTicks)
		{
			return new TimeSpan(endDateTicks - startDateTicks).TotalDays;
		}

		/// <summary>
		/// Averages the length of the year.
		/// </summary>
		/// <param name="startYear">The start year.</param>
		/// <param name="endYear">The end year.</param>
		/// <returns></returns>
		private static double AverageYearLength(int startYear, int endYear)
		{
			int num = 0;
			for (int i = startYear; i <= endYear; i++)
			{
				num += 365;
				if (IsLeapYear(i))
				{
					num++;
				}
			}
			double num2 = (double)(endYear - startYear + 1);
			return (double)num / num2;
		}

		/// <summary>
		/// determine Leap Year
		/// </summary>
		/// <param name="i">the year</param>
		/// <returns></returns>
		private static bool IsLeapYear(int i)
		{
			if (i % 4 != 0)
			{
				return false;
			}
			if (i % 400 == 0)
			{
				return true;
			}
			if (i % 100 == 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Determines whether [is greater than one year] [the specified start].
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		/// <returns>
		/// 	<c>true</c> if [is greater than one year] [the specified start]; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsGreaterThanOneYear(SimpleDate start, SimpleDate end)
		{
			if (start.year == end.year)
			{
				return false;
			}
			if (start.year + 1 != end.year)
			{
				return true;
			}
			if (start.month > end.month)
			{
				return false;
			}
			if (start.month < end.month)
			{
				return true;
			}
			return start.day < end.day;
		}

		/// <summary>
		/// Creates the date.
		/// </summary>
		/// <param name="dayCount">The day count.</param>
		/// <returns></returns>
		private static SimpleDate CreateDate(int dayCount)
		{
			return new SimpleDate(DateUtil.GetJavaDate((double)dayCount));
		}
	}
}
