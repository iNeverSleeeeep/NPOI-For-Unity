using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel.Extensions;

namespace NPOI.XSSF.UserModel
{
	/// First page footer content. Corresponds to first printed page.  
	/// The first logical page in the sheet may not be printed, for example, if the print area is specified to 
	/// be a range such that it falls outside the first page's scope.
	public class XSSFFirstFooter : XSSFHeaderFooter, IFooter, IHeaderFooter
	{
		/// Get the content text representing the footer
		/// @return text
		public override string Text
		{
			get
			{
				return GetHeaderFooter().firstFooter;
			}
			set
			{
				if (value == null)
				{
					GetHeaderFooter().firstFooter = null;
				}
				else
				{
					GetHeaderFooter().firstFooter = value;
				}
			}
		}

		/// Create an instance of XSSFFirstFooter from the supplied XML bean
		/// @see XSSFSheet#getFirstFooter()
		/// @param headerFooter
		public XSSFFirstFooter(CT_HeaderFooter headerFooter)
			: base(headerFooter)
		{
			headerFooter.differentFirst = true;
		}
	}
}
