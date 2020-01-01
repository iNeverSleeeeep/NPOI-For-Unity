using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Extensions;

namespace NPOI.XSSF.UserModel
{
	/// Odd page header value. Corresponds to odd printed pages. 
	/// Odd page(s) in the sheet may not be printed, for example, if the print area is specified to be 
	/// a range such that it falls outside an odd page's scope.
	public class XSSFOddHeader : XSSFHeaderFooter, IHeader, IHeaderFooter
	{
		/// Get the content text representing this header
		/// @return text
		public override string Text
		{
			get
			{
				return GetHeaderFooter().oddHeader;
			}
			set
			{
				GetHeaderFooter().oddHeader = value;
			}
		}

		/// Create an instance of XSSFOddHeader from the supplied XML bean
		/// @see XSSFSheet#GetOddHeader()
		/// @param headerFooter
		public XSSFOddHeader(CT_HeaderFooter headerFooter)
			: base(headerFooter)
		{
		}
	}
}
