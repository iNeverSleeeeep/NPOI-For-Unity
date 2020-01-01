using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	public class SimpleDateFormat : FormatBase
	{
		protected string pattern;

		public SimpleDateFormat()
		{
		}

		public SimpleDateFormat(string pattern)
		{
			this.pattern = pattern;
		}

		public override string Format(object obj, CultureInfo culture)
		{
			return ((DateTime)obj).ToString(pattern, culture);
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(Format((DateTime)obj, culture));
		}

		public override object ParseObject(string source, int pos)
		{
			return DateTime.Parse(source.Substring(pos), CultureInfo.InvariantCulture).ToUniversalTime();
		}

		public DateTime Parse(string source)
		{
			return DateTime.Parse(source, CultureInfo.InvariantCulture);
		}
	}
}
