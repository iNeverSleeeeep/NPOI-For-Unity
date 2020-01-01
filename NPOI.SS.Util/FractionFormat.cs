using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Util
{
	/// Format class that handles Excel style fractions, such as "# #/#" and "#/###"
	public class FractionFormat : FormatBase
	{
		private string formatstr = string.Empty;

		private static Regex regex = new Regex("(#+)([^#]*?)(#+/#+)([^#]*?)");

		public FractionFormat(string s)
		{
			formatstr = s;
		}

		public string Format(double num)
		{
			string[] array = formatstr.Split(";".ToCharArray());
			int num2 = (!(num > 0.0)) ? ((num < 0.0) ? 1 : 2) : 0;
			string text = (num2 < array.Length) ? array[num2] : array[0];
			double num3 = Math.Floor(Math.Abs(num));
			double num4 = Math.Abs(num) - num3;
			if (num3 + num4 == 0.0)
			{
				return "0";
			}
			if (num < 0.0)
			{
				num3 *= -1.0;
			}
			string[] array2 = text.Replace("  *", " ").Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			string[] array3 = (array2.Length != 2) ? text.Split("/".ToCharArray()) : array2[1].Split("/".ToCharArray());
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = array3[i].Replace('?', '#');
			}
			if (array3.Length == 2)
			{
				int num5 = Math.Min(CountHashes(array3[1]), 4);
				double num6 = 1.0;
				double num7 = Math.Pow(10.0, (double)num5) - 1.0;
				double num8 = 0.0;
				for (int num9 = (int)(Math.Pow(10.0, (double)num5) - 1.0); num9 > 0; num9--)
				{
					for (int num10 = (int)(Math.Pow(10.0, (double)num5) - 1.0); num10 > 0; num10--)
					{
						if (num6 >= Math.Abs((double)num10 / (double)num9 - num4))
						{
							num7 = (double)num9;
							num8 = (double)num10;
							num6 = Math.Abs((double)num10 / (double)num9 - num4);
						}
					}
				}
				DecimalFormat decimalFormat = new DecimalFormat(array3[0]);
				DecimalFormat decimalFormat2 = new DecimalFormat(array3[1]);
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (array2.Length == 2)
				{
					DecimalFormat decimalFormat3 = new DecimalFormat(array2[0]);
					return decimalFormat3.Format(num3, currentCulture) + " " + decimalFormat.Format(num8, currentCulture) + "/" + decimalFormat2.Format(num7, currentCulture);
				}
				return decimalFormat.Format(num8 + num7 * num3, currentCulture) + "/" + decimalFormat2.Format(num7, currentCulture);
			}
			throw new ArgumentException("Fraction must have 2 parts, found " + array3.Length + " for fraction format " + formatstr);
		}

		private string ReplaceWS(Match m)
		{
			return m.Groups[0].Value + " " + m.Groups[2].Value + m.Groups[3].Value;
		}

		private int CountHashes(string format)
		{
			int num = 0;
			for (int num2 = format.Length - 1; num2 >= 0; num2--)
			{
				if (format[num2] == '#')
				{
					num++;
				}
			}
			return num;
		}

		public override string Format(object obj, CultureInfo culture)
		{
			return Format((double)obj);
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(Format((double)obj));
		}

		public override object ParseObject(string source, int pos)
		{
			throw new NotImplementedException("Reverse parsing not supported");
		}
	}
}
