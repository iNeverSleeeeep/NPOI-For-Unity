using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Extensions;

namespace NPOI.XSSF.UserModel
{
	/// Odd page footer value. Corresponds to odd printed pages.
	/// Odd page(s) in the sheet may not be printed, for example, if the print area is specified to be 
	/// a range such that it falls outside an odd page's scope.
	public class XSSFOddFooter : XSSFHeaderFooter, IFooter, IHeaderFooter
	{
		/// Get the content text representing the footer
		/// @return text
		public override string Text
		{
			get
			{
				return GetHeaderFooter().oddFooter;
			}
			set
			{
				GetHeaderFooter().oddFooter = value;
			}
		}

		/// Create an instance of XSSFOddFooter from the supplied XML bean
		/// @see XSSFSheet#GetOddFooter()
		/// @param headerFooter
		public XSSFOddFooter(CT_HeaderFooter headerFooter)
			: base(headerFooter)
		{
		}
	}
}
