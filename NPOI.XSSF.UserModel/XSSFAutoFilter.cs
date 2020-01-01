using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// Represents autofiltering for the specified worksheet.
	///
	/// @author Yegor Kozlov
	public class XSSFAutoFilter : IAutoFilter
	{
		private XSSFSheet _sheet;

		public XSSFAutoFilter(XSSFSheet sheet)
		{
			_sheet = sheet;
		}
	}
}
