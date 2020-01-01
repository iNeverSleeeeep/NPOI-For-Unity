using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// Format class for Excel's SSN Format. This class mimics Excel's built-in
	/// SSN Formatting.
	///
	/// @author James May
	public class SSNFormat : FormatBase
	{
		public static FormatBase instance = new SSNFormat();

		private static string df = "000000000";

		private SSNFormat()
		{
		}

		/// Format a number as an SSN 
		public override string Format(object obj, CultureInfo culture)
		{
			string text = ((double)obj).ToString(df, culture);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(text.Substring(0, 3)).Append('-');
			stringBuilder.Append(text.Substring(3, 2)).Append('-');
			stringBuilder.Append(text.Substring(5, 4));
			return stringBuilder.ToString();
		}

		public override StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture)
		{
			return toAppendTo.Append(Format((long)obj, culture));
		}

		public override object ParseObject(string source, int pos)
		{
			string s = source.Substring(pos);
			return long.Parse(s, CultureInfo.InvariantCulture);
		}
	}
}
