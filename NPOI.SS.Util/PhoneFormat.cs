using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// Format class for Excel phone number Format. This class mimics Excel's
	/// built-in phone number Formatting.
	/// @author James May
	public class PhoneFormat : FormatBase
	{
		public static FormatBase instance = new PhoneFormat();

		private static string df = "##########";

		private PhoneFormat()
		{
		}

		/// Format a number as a phone number 
		public override string Format(object obj, CultureInfo culture)
		{
			string text = ((double)obj).ToString(df, culture);
			StringBuilder stringBuilder = new StringBuilder();
			int length = text.Length;
			if (length <= 4)
			{
				return text;
			}
			string value = text.Substring(length - 4);
			int num = Math.Max(0, length - 7);
			string text2 = text.Substring(Math.Max(0, length - 7), length - 4 - num);
			num = Math.Max(0, length - 10);
			string text3 = text.Substring(num, Math.Max(0, length - 7) - num);
			if (text3 != null && text3.Trim().Length > 0)
			{
				stringBuilder.Append('(').Append(text3).Append(") ");
			}
			if (text2 != null && text2.Trim().Length > 0)
			{
				stringBuilder.Append(text2).Append('-');
			}
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(Format(obj, culture));
		}

		public override object ParseObject(string source, int pos)
		{
			string s = source.Substring(pos);
			return long.Parse(s, CultureInfo.InvariantCulture);
		}
	}
}
