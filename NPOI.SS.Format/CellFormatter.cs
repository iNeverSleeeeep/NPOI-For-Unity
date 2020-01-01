using System.Globalization;
using System.Text;

namespace NPOI.SS.Format
{
	/// This is the abstract supertype for the various cell formatters.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public abstract class CellFormatter
	{
		/// The original specified format. 
		protected string format;

		/// This is the locale used to Get a consistent format result from which to
		/// work.
		public static readonly CultureInfo LOCALE = CultureInfo.GetCultureInfo("en-US");

		/// Creates a new formatter object, storing the format in {@link #format}.
		///
		/// @param format The format.
		public CellFormatter(string format)
		{
			this.format = format;
		}

		/// Format a value according the format string.
		///
		/// @param toAppendTo The buffer to append to.
		/// @param value      The value to format.
		public abstract void FormatValue(StringBuilder toAppendTo, object value);

		/// Format a value according to the type, in the most basic way.
		///
		/// @param toAppendTo The buffer to append to.
		/// @param value      The value to format.
		public abstract void SimpleValue(StringBuilder toAppendTo, object value);

		/// Formats the value, returning the resulting string.
		///
		/// @param value The value to format.
		///
		/// @return The value, formatted.
		public string Format(object value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			FormatValue(stringBuilder, value);
			return stringBuilder.ToString();
		}

		/// Formats the value in the most basic way, returning the resulting string.
		///
		/// @param value The value to format.
		///
		/// @return The value, formatted.
		public string SimpleFormat(object value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			SimpleValue(stringBuilder, value);
			return stringBuilder.ToString();
		}

		/// Returns the input string, surrounded by quotes.
		///
		/// @param str The string to quote.
		///
		/// @return The input string, surrounded by quotes.
		private static string Quote(string str)
		{
			return '"' + str + '"';
		}

		public override string ToString()
		{
			return format;
		}
	}
}
