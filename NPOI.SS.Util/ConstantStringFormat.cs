using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// Format class that does nothing and always returns a constant string.
	///
	/// This format is used to simulate Excel's handling of a format string
	/// of all # when the value is 0. Excel will output "", Java will output "0".
	///
	/// @see DataFormatter#createFormat(double, int, String)
	public class ConstantStringFormat : FormatBase
	{
		private static DecimalFormat df = new DecimalFormat("##########");

		private string str;

		public ConstantStringFormat(string s)
		{
			str = s;
		}

		public override string Format(object obj, CultureInfo culture)
		{
			return str;
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(str);
		}

		public override object ParseObject(string source, int pos)
		{
			return df.ParseObject(source, pos);
		}
	}
}
