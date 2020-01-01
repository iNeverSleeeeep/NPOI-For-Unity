using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Util
{
	public class DecimalFormat : FormatBase
	{
		private string pattern;

		private static readonly Regex RegexFraction = new Regex("#+/#+");

		private bool _parseIntegerOnly;

		public string Pattern => pattern;

		public bool ParseIntegerOnly
		{
			get
			{
				return _parseIntegerOnly;
			}
			set
			{
				_parseIntegerOnly = value;
			}
		}

		public DecimalFormat()
		{
		}

		public DecimalFormat(string pattern)
		{
			if (pattern.IndexOf("'", StringComparison.Ordinal) != -1)
			{
				throw new ArgumentException("invalid pattern");
			}
			this.pattern = pattern;
		}

		public override string Format(object obj, CultureInfo culture)
		{
			pattern = RegexFraction.Replace(pattern, "/");
			if (pattern.IndexOf("'", StringComparison.Ordinal) != -1)
			{
				return Convert.ToDouble(obj, CultureInfo.InvariantCulture).ToString(culture);
			}
			return Convert.ToDouble(obj, CultureInfo.InvariantCulture).ToString(pattern, culture);
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(Format((double)obj, culture));
		}

		public override object ParseObject(string source, int pos)
		{
			return decimal.Parse(source.Substring(pos), CultureInfo.CurrentCulture);
		}
	}
}
