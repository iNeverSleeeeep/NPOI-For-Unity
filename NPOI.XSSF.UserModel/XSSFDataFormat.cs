using NPOI.SS.UserModel;
using NPOI.XSSF.Model;

namespace NPOI.XSSF.UserModel
{
	/// Handles data formats for XSSF.
	public class XSSFDataFormat : IDataFormat
	{
		private StylesTable stylesSource;

		public XSSFDataFormat(StylesTable stylesSource)
		{
			this.stylesSource = stylesSource;
		}

		/// Get the format index that matches the given format
		///  string, creating a new format entry if required.
		/// Aliases text to the proper format as required.
		///
		/// @param format string matching a built in format
		/// @return index of format.
		public short GetFormat(string format)
		{
			int num = BuiltinFormats.GetBuiltinFormat(format);
			if (num == -1)
			{
				num = stylesSource.PutNumberFormat(format);
			}
			return (short)num;
		}

		/// Get the format string that matches the given format index
		/// @param index of a format
		/// @return string represented at index of format or null if there is not a  format at that index
		public string GetFormat(short index)
		{
			string text = stylesSource.GetNumberFormatAt(index);
			if (text == null)
			{
				text = BuiltinFormats.GetBuiltinFormat(index);
			}
			return text;
		}
	}
}
