using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// Format class for Excel Zip + 4 Format. This class mimics Excel's
	/// built-in Formatting for Zip + 4.
	/// @author James May
	public class ZipPlusFourFormat : FormatBase
	{
		public static FormatBase instance = new ZipPlusFourFormat();

		private static string df = "000000000";

		private ZipPlusFourFormat()
		{
		}

		/// Format a number as Zip + 4 
		public override string Format(object obj, CultureInfo culture)
		{
			string text = ((double)obj).ToString(df, culture);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(text.Substring(0, 5)).Append('-');
			stringBuilder.Append(text.Substring(5, 4));
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
