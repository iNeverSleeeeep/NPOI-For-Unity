using System.Globalization;

namespace NPOI.SS.Util
{
	public class DateFormat
	{
		public const int FULL = 0;

		public const int LONG = 1;

		public const int MEDIUM = 2;

		public const int SHORT = 3;

		public const int DEFAULT = 2;

		public static string GetDateTimePattern(int dateStyle, int timeStyle, CultureInfo locale)
		{
			DateTimeFormatInfo dateTimeFormat = locale.DateTimeFormat;
			string datePattern = GetDatePattern(dateStyle, locale);
			string timePattern = GetTimePattern(timeStyle, locale);
			if (locale.TextInfo.IsRightToLeft)
			{
				return timePattern + " " + datePattern;
			}
			return datePattern + " " + timePattern;
		}

		public static string GetDatePattern(int dateStyle, CultureInfo locale)
		{
			DateTimeFormatInfo dateTimeFormat = locale.DateTimeFormat;
			switch (dateStyle)
			{
			case 3:
				return dateTimeFormat.ShortDatePattern.Replace("yyyy", "yy").Replace("YYYY", "YY");
			case 2:
				return dateTimeFormat.ShortDatePattern;
			case 1:
				return dateTimeFormat.LongDatePattern.Replace("dddd,", "").Trim();
			case 0:
				return dateTimeFormat.LongDatePattern;
			default:
				return dateTimeFormat.ShortDatePattern;
			}
		}

		public static string GetTimePattern(int timeStyle, CultureInfo locale)
		{
			DateTimeFormatInfo dateTimeFormat = locale.DateTimeFormat;
			switch (timeStyle)
			{
			case 3:
				return dateTimeFormat.ShortTimePattern;
			default:
				return dateTimeFormat.LongTimePattern;
			}
		}
	}
}
