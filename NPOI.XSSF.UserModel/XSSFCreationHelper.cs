using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	public class XSSFCreationHelper : ICreationHelper
	{
		private XSSFWorkbook workbook;

		public XSSFCreationHelper(XSSFWorkbook wb)
		{
			workbook = wb;
		}

		/// Creates a new XSSFRichTextString for you.
		public IRichTextString CreateRichTextString(string text)
		{
			XSSFRichTextString xSSFRichTextString = new XSSFRichTextString(text);
			xSSFRichTextString.SetStylesTableReference(workbook.GetStylesSource());
			return xSSFRichTextString;
		}

		public IDataFormat CreateDataFormat()
		{
			return workbook.CreateDataFormat();
		}

		public IHyperlink CreateHyperlink(HyperlinkType type)
		{
			return new XSSFHyperlink(type);
		}

		/// Creates a XSSFFormulaEvaluator, the object that Evaluates formula cells.
		///
		/// @return a XSSFFormulaEvaluator instance
		public IFormulaEvaluator CreateFormulaEvaluator()
		{
			return new XSSFFormulaEvaluator(workbook);
		}

		/// Creates a XSSFClientAnchor. Use this object to position Drawing object in
		/// a sheet
		///
		/// @return a XSSFClientAnchor instance
		/// @see NPOI.ss.usermodel.Drawing
		public IClientAnchor CreateClientAnchor()
		{
			return new XSSFClientAnchor();
		}
	}
}
