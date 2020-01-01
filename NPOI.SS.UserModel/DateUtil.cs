using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Contains methods for dealing with Excel dates.
	/// @author  Michael Harhen
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author  Dan Sherman (dsherman at Isisph.com)
	/// @author  Hack Kampbjorn (hak at 2mba.dk)
	/// @author  Alex Jacoby (ajacoby at gmail.com)
	/// @author  Pavel Krupets (pkrupets at palmtreebusiness dot com)
	/// @author  Thies Wellpott
	/// </summary>
	public class DateUtil
	{
		public const int SECONDS_PER_MINUTE = 60;

		public const int MINUTES_PER_HOUR = 60;

		public const int HOURS_PER_DAY = 24;

		public const int SECONDS_PER_DAY = 86400;

		private const int BAD_DATE = -1;

		public const long DAY_MILLISECONDS = 86400000L;

		private static readonly char[] TIME_SEPARATOR_PATTERN = new char[1]
		{
			':'
		};

		/// <summary>
		/// Given a Calendar, return the number of days since 1899/12/31.
		/// </summary>
		/// <param name="cal">the date</param>
		/// <param name="use1904windowing">if set to <c>true</c> [use1904windowing].</param>
		/// <returns>number of days since 1899/12/31</returns>
		public static int AbsoluteDay(DateTime cal, bool use1904windowing)
		{
			int num = (cal - new DateTime(1899, 12, 31)).Days;
			if (cal > new DateTime(1900, 3, 1) && use1904windowing)
			{
				num++;
			}
			return num;
		}

		/// <summary>
		/// Given a Date, Converts it into a double representing its internal Excel representation,
		/// which Is the number of days since 1/1/1900. Fractional days represent hours, minutes, and seconds.
		/// </summary>
		/// <param name="date">Excel representation of Date (-1 if error - test for error by Checking for less than 0.1)</param>
		/// <returns>the Date</returns>
		public static double GetExcelDate(DateTime date)
		{
			return GetExcelDate(date, use1904windowing: false);
		}

		/// <summary>
		/// Gets the excel date.
		/// </summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month.</param>
		/// <param name="day">The day.</param>
		/// <param name="hour">The hour.</param>
		/// <param name="minute">The minute.</param>
		/// <param name="second">The second.</param>
		/// <param name="use1904windowing">Should 1900 or 1904 date windowing be used?</param>
		/// <returns></returns>
		public static double GetExcelDate(int year, int month, int day, int hour, int minute, int second, bool use1904windowing)
		{
			if ((!use1904windowing && year < 1900) || (use1904windowing && year < 1904))
			{
				return -1.0;
			}
			DateTime d = use1904windowing ? new DateTime(1904, 1, 1) : new DateTime(1900, 1, 1);
			int months = 0;
			if (month > 12)
			{
				months = month - 12;
				month = 12;
			}
			int num = 0;
			switch (month)
			{
			case 1:
			case 3:
			case 5:
			case 7:
			case 8:
			case 10:
			case 12:
				if (day > 31)
				{
					num = day - 31;
					day = 31;
				}
				break;
			case 4:
			case 6:
			case 9:
			case 11:
				if (day > 30)
				{
					num = day - 30;
					day = 30;
				}
				break;
			default:
				if (DateTime.IsLeapYear(year))
				{
					if (day > 29)
					{
						num = day - 29;
						day = 29;
					}
				}
				else if (day > 28)
				{
					num = day - 28;
					day = 28;
				}
				break;
			}
			if (day <= 0)
			{
				num = day - 1;
				day = 1;
			}
			DateTime d2 = new DateTime(year, month, day, hour, minute, second).AddMonths(months).AddDays((double)num);
			double num2 = (d2 - d).TotalDays + 1.0;
			if (!use1904windowing && num2 >= 60.0)
			{
				num2 += 1.0;
			}
			else if (use1904windowing)
			{
				num2 -= 1.0;
			}
			return num2;
		}

		/// <summary>
		/// Given a Date, Converts it into a double representing its internal Excel representation,
		/// which Is the number of days since 1/1/1900. Fractional days represent hours, minutes, and seconds.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="use1904windowing">Should 1900 or 1904 date windowing be used?</param>
		/// <returns>Excel representation of Date (-1 if error - test for error by Checking for less than 0.1)</returns>
		public static double GetExcelDate(DateTime date, bool use1904windowing)
		{
			if ((!use1904windowing && date.Year < 1900) || (use1904windowing && date.Year < 1904))
			{
				return -1.0;
			}
			DateTime d = use1904windowing ? new DateTime(1904, 1, 1) : new DateTime(1900, 1, 1);
			double num = (date - d).TotalDays + 1.0;
			if (!use1904windowing && num >= 60.0)
			{
				num += 1.0;
			}
			else if (use1904windowing)
			{
				num -= 1.0;
			}
			return num;
		}

		/// <summary>
		///  Given an Excel date with using 1900 date windowing, and converts it to a java.util.Date.
		///  Excel Dates and Times are stored without any timezone 
		///  information. If you know (through other means) that your file 
		///  uses a different TimeZone to the system default, you can use
		///  this version of the getJavaDate() method to handle it.
		/// </summary>
		/// <param name="date">The Excel date.</param>
		/// <returns>null if date is not a valid Excel date</returns>
		public static DateTime GetJavaDate(double date)
		{
			return GetJavaDate(date, use1904windowing: false);
		}

		/// Given an Excel date with either 1900 or 1904 date windowing,
		/// Converts it to a Date.
		///
		/// NOTE: If the default <c>TimeZone</c> in Java uses Daylight
		/// Saving Time then the conversion back to an Excel date may not give
		/// the same value, that Is the comparison
		/// <CODE>excelDate == GetExcelDate(GetJavaDate(excelDate,false))</CODE>
		/// Is not always true. For example if default timezone Is
		/// <c>Europe/Copenhagen</c>, on 2004-03-28 the minute after
		/// 01:59 CET Is 03:00 CEST, if the excel date represents a time between
		/// 02:00 and 03:00 then it Is Converted to past 03:00 summer time
		///
		/// @param date  The Excel date.
		/// @param use1904windowing  true if date uses 1904 windowing,
		///  or false if using 1900 date windowing.
		/// @return Java representation of the date, or null if date Is not a valid Excel date
		/// @see TimeZone
		public static DateTime GetJavaDate(double date, bool use1904windowing)
		{
			return GetJavaCalendar(date, use1904windowing);
		}

		public static void SetCalendar(ref DateTime calendar, int wholeDays, int millisecondsInDay, bool use1904windowing)
		{
			int year = 1900;
			int num = -1;
			if (use1904windowing)
			{
				year = 1904;
				num = 1;
			}
			else if (wholeDays < 61)
			{
				num = 0;
			}
			DateTime dateTime = calendar = new DateTime(year, 1, 1).AddDays((double)(wholeDays + num - 1)).AddMilliseconds((double)millisecondsInDay);
		}

		/// <summary>
		/// Get EXCEL date as Java Calendar (with default time zone). This is like GetJavaDate(double, boolean) but returns a Calendar object.
		/// </summary>
		/// <param name="date">The Excel date.</param>
		/// <param name="use1904windowing">true if date uses 1904 windowing, or false if using 1900 date windowing.</param>
		/// <returns>null if date is not a valid Excel date</returns>
		public static DateTime GetJavaCalendar(double date, bool use1904windowing)
		{
			if (!IsValidExcelDate(date))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid Excel date double value: {0}", new object[1]
				{
					date
				}));
			}
			int num = (int)Math.Floor(date);
			int millisecondsInDay = (int)((date - (double)num) * 86400000.0 + 0.5);
			DateTime calendar = DateTime.Now;
			SetCalendar(ref calendar, num, millisecondsInDay, use1904windowing);
			return calendar;
		}

		/// <summary>
		/// Converts a string of format "HH:MM" or "HH:MM:SS" to its (Excel) numeric equivalent
		/// </summary>
		/// <param name="timeStr">The time STR.</param>
		/// <returns> a double between 0 and 1 representing the fraction of the day</returns>
		public static double ConvertTime(string timeStr)
		{
			try
			{
				return ConvertTimeInternal(timeStr);
			}
			catch (FormatException ex)
			{
				string message = "Bad time format '" + timeStr + "' expected 'HH:MM' or 'HH:MM:SS' - " + ex.Message;
				throw new ArgumentException(message);
			}
		}

		/// <summary>
		/// Converts the time internal.
		/// </summary>
		/// <param name="timeStr">The time STR.</param>
		/// <returns></returns>
		private static double ConvertTimeInternal(string timeStr)
		{
			int length = timeStr.Length;
			if (length < 4 || length > 8)
			{
				throw new FormatException("Bad length");
			}
			string[] array = timeStr.Split(TIME_SEPARATOR_PATTERN);
			string strVal;
			switch (array.Length)
			{
			case 2:
				strVal = "00";
				break;
			case 3:
				strVal = array[2];
				break;
			default:
				throw new FormatException("Expected 2 or 3 fields but got (" + array.Length + ")");
			}
			string strVal2 = array[0];
			string strVal3 = array[1];
			int num = ParseInt(strVal2, "hour", 24);
			int num2 = ParseInt(strVal3, "minute", 60);
			int num3 = ParseInt(strVal, "second", 60);
			double num4 = (double)(num3 + (num2 + num * 60) * 60);
			return num4 / 86400.0;
		}

		/// <summary>
		/// Given a format ID and its format String, will Check to see if the
		/// format represents a date format or not.
		/// Firstly, it will Check to see if the format ID corresponds to an
		/// internal excel date format (eg most US date formats)
		/// If not, it will Check to see if the format string only Contains
		/// date formatting Chars (ymd-/), which covers most
		/// non US date formats.
		/// </summary>
		/// <param name="formatIndex">The index of the format, eg from ExtendedFormatRecord.GetFormatIndex</param>
		/// <param name="formatString">The format string, eg from FormatRecord.GetFormatString</param>
		/// <returns>
		/// 	<c>true</c> if [is A date format] [the specified format index]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsADateFormat(int formatIndex, string formatString)
		{
			if (IsInternalDateFormat(formatIndex))
			{
				return true;
			}
			if (formatString == null || formatString.Length == 0)
			{
				return false;
			}
			string text = Regex.Replace(formatString, ";@", "");
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				char c2;
				if (i < text.Length - 1)
				{
					c2 = text[i + 1];
					switch (c)
					{
					case '\\':
						break;
					case ';':
						goto IL_008a;
					default:
						goto IL_0096;
					}
					switch (c2)
					{
					case ' ':
					case ',':
					case '-':
					case '.':
					case '\\':
						continue;
					}
				}
				goto IL_0096;
				IL_0096:
				stringBuilder.Append(c);
				continue;
				IL_008a:
				if (c2 == '@')
				{
					i++;
					continue;
				}
				goto IL_0096;
			}
			text = stringBuilder.ToString();
			if (Regex.IsMatch(text, "^\\[([hH]+|[mM]+|[sS]+)\\]"))
			{
				return true;
			}
			text = Regex.Replace(text, "^\\[\\$\\-.*?\\]", "");
			text = Regex.Replace(text, "^\\[[a-zA-Z]+\\]", "");
			if (text.IndexOf(';') > 0 && text.IndexOf(';') < text.Length - 1)
			{
				text = text.Substring(0, text.IndexOf(';'));
			}
			text = Regex.Replace(text, "\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\"", "");
			if (Regex.IsMatch(text, "^[\\[\\]yYmMdDhHsS\\-/,. :\\\"\\\\]+0*[ampAMP/]*$"))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Converts a string of format "YYYY/MM/DD" to its (Excel) numeric equivalent
		/// </summary>
		/// <param name="dateStr">The date STR.</param>
		/// <returns>a double representing the (integer) number of days since the start of the Excel epoch</returns>
		public static DateTime ParseYYYYMMDDDate(string dateStr)
		{
			try
			{
				return ParseYYYYMMDDDateInternal(dateStr);
			}
			catch (FormatException ex)
			{
				string message = "Bad time format " + dateStr + " expected 'YYYY/MM/DD' - " + ex.Message;
				throw new ArgumentException(message);
			}
		}

		/// <summary>
		/// Parses the YYYYMMDD date internal.
		/// </summary>
		/// <param name="timeStr">The time string.</param>
		/// <returns></returns>
		private static DateTime ParseYYYYMMDDDateInternal(string timeStr)
		{
			if (timeStr.Length != 10)
			{
				throw new FormatException("Bad length");
			}
			string strVal = timeStr.Substring(0, 4);
			string strVal2 = timeStr.Substring(5, 2);
			string strVal3 = timeStr.Substring(8, 2);
			int year = ParseInt(strVal, "year", -32768, 32767);
			int month = ParseInt(strVal2, "month", 1, 12);
			int day = ParseInt(strVal3, "day", 1, 31);
			return new DateTime(year, month, day, 0, 0, 0);
		}

		/// <summary>
		/// Parses the int.
		/// </summary>
		/// <param name="strVal">The string value.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="rangeMax">The range max.</param>
		/// <returns></returns>
		private static int ParseInt(string strVal, string fieldName, int rangeMax)
		{
			return ParseInt(strVal, fieldName, 0, rangeMax - 1);
		}

		/// <summary>
		/// Parses the int.
		/// </summary>
		/// <param name="strVal">The STR val.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="lowerLimit">The lower limit.</param>
		/// <param name="upperLimit">The upper limit.</param>
		/// <returns></returns>
		private static int ParseInt(string strVal, string fieldName, int lowerLimit, int upperLimit)
		{
			int num;
			try
			{
				num = int.Parse(strVal, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new FormatException("Bad int format '" + strVal + "' for " + fieldName + " field");
			}
			if (num < lowerLimit || num > upperLimit)
			{
				throw new FormatException(fieldName + " value (" + num + ") is outside the allowable range(0.." + upperLimit + ")");
			}
			return num;
		}

		/// <summary>
		/// Given a format ID this will Check whether the format represents an internal excel date format or not.
		/// </summary>
		/// <param name="format">The format.</param>
		public static bool IsInternalDateFormat(int format)
		{
			bool flag = false;
			switch (format)
			{
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 45:
			case 46:
			case 47:
				return true;
			default:
				return false;
			}
		}

		/// <summary>
		/// Check if a cell Contains a date
		/// Since dates are stored internally in Excel as double values
		/// we infer it Is a date if it Is formatted as such.
		/// </summary>
		/// <param name="cell">The cell.</param>
		public static bool IsCellDateFormatted(ICell cell)
		{
			if (cell == null)
			{
				return false;
			}
			bool result = false;
			double numericCellValue = cell.NumericCellValue;
			if (IsValidExcelDate(numericCellValue))
			{
				ICellStyle cellStyle = cell.CellStyle;
				if (cellStyle == null)
				{
					return false;
				}
				int dataFormat = cellStyle.DataFormat;
				string dataFormatString = cellStyle.GetDataFormatString();
				result = IsADateFormat(dataFormat, dataFormatString);
			}
			return result;
		}

		/// <summary>
		/// Check if a cell contains a date, Checking only for internal excel date formats.
		/// As Excel stores a great many of its dates in "non-internal" date formats, you will not normally want to use this method.
		/// </summary>
		/// <param name="cell">The cell.</param>
		public static bool IsCellInternalDateFormatted(ICell cell)
		{
			if (cell == null)
			{
				return false;
			}
			bool result = false;
			double numericCellValue = cell.NumericCellValue;
			if (IsValidExcelDate(numericCellValue))
			{
				ICellStyle cellStyle = cell.CellStyle;
				int dataFormat = cellStyle.DataFormat;
				result = IsInternalDateFormat(dataFormat);
			}
			return result;
		}

		/// <summary>
		/// Given a double, Checks if it Is a valid Excel date.
		/// </summary>
		/// <param name="value">the double value.</param>
		/// <returns>
		/// 	<c>true</c> if [is valid excel date] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidExcelDate(double value)
		{
			return value > -4.94065645841247E-324;
		}
	}
}
