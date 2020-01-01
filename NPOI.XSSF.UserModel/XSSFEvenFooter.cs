using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Extensions;

namespace NPOI.XSSF.UserModel
{
	/// Even page footer value. Corresponds to even printed pages. 
	/// Even page(s) in the sheet may not be printed, for example, if the print area is specified to be 
	/// a range such that it falls outside an even page's scope. 
	/// If no even footer is specified, then the odd footer's value is assumed for even page footers. 
	public class XSSFEvenFooter : XSSFHeaderFooter, IFooter, IHeaderFooter
	{
		/// Get the content text representing the footer
		/// @return text
		public override string Text
		{
			get
			{
				return GetHeaderFooter().evenFooter;
			}
			set
			{
				GetHeaderFooter().evenFooter = value;
			}
		}

		/// Create an instance of XSSFEvenFooter from the supplied XML bean
		/// @see XSSFSheet#GetEvenFooter()
		/// @param headerFooter
		public XSSFEvenFooter(CT_HeaderFooter headerFooter)
			: base(headerFooter)
		{
			headerFooter.differentOddEven = true;
		}
	}
}
