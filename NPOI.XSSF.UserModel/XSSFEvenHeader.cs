using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Extensions;

namespace NPOI.XSSF.UserModel
{
	/// <p>
	/// Even page header value. Corresponds to even printed pages.
	/// Even page(s) in the sheet may not be printed, for example, if the print area is specified to be 
	/// a range such that it falls outside an even page's scope.
	/// If no even header is specified, then odd header value is assumed for even page headers.
	///             </p>
	public class XSSFEvenHeader : XSSFHeaderFooter, IHeader, IHeaderFooter
	{
		/// Get the content text representing this header
		/// @return text
		public override string Text
		{
			get
			{
				return GetHeaderFooter().evenHeader;
			}
			set
			{
				GetHeaderFooter().evenHeader = value;
			}
		}

		/// Create an instance of XSSFEvenHeader from the supplied XML bean
		/// @see XSSFSheet#GetEvenHeader()
		/// @param headerFooter
		public XSSFEvenHeader(CT_HeaderFooter headerFooter)
			: base(headerFooter)
		{
			headerFooter.differentOddEven = true;
		}
	}
}
