using NPOI.SS.Formula.Eval;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// Parser for java dates.
	///
	/// @author jfaenomoto@gmail.com
	public class DateParser
	{
		public DateParser instance = new DateParser();

		private DateParser()
		{
		}

		/// Parses a date from a string.
		///
		/// @param strVal a string with a date pattern.
		/// @return a date parsed from argument.
		/// @throws EvaluationException exception upon parsing.
		public static DateTime ParseDate(string strVal)
		{
			string[] array = strVal.Split("-/".ToCharArray());
			if (array.Length != 3)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			string text = array[2];
			int num = text.IndexOf(' ');
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			int num2;
			int num3;
			int num4;
			try
			{
				num2 = int.Parse(array[0]);
				num3 = int.Parse(array[1]);
				num4 = int.Parse(text);
			}
			catch (FormatException)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			if (num2 < 0 || num3 < 0 || num4 < 0 || (num2 > 12 && num3 > 12 && num4 > 12))
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			if (num2 >= 1900 && num2 < 9999)
			{
				return MakeDate(num2, num3, num4);
			}
			throw new RuntimeException("Unable to determine date format for text '" + strVal + "'");
		}

		/// @param month 1-based
		private static DateTime MakeDate(int year, int month, int day)
		{
			if (month < 1 || month > 12)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			int day2 = new DateTime(year, month, 1, 0, 0, 0).AddMonths(1).AddDays(-1.0).Day;
			if (day < 1 || day > day2)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			return new DateTime(year, month, day);
		}
	}
}
