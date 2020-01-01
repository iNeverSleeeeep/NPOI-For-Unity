using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	public class HSSFCreationHelper : ICreationHelper
	{
		private HSSFWorkbook workbook;

		private HSSFDataFormat dataFormat;

		public HSSFCreationHelper(HSSFWorkbook wb)
		{
			workbook = wb;
			dataFormat = new HSSFDataFormat(workbook.Workbook);
		}

		public IRichTextString CreateRichTextString(string text)
		{
			return new HSSFRichTextString(text);
		}

		public IDataFormat CreateDataFormat()
		{
			return dataFormat;
		}

		public IHyperlink CreateHyperlink(HyperlinkType type)
		{
			return new HSSFHyperlink(type);
		}

		/// Creates a HSSFFormulaEvaluator, the object that Evaluates formula cells.
		///
		/// @return a HSSFFormulaEvaluator instance
		public IFormulaEvaluator CreateFormulaEvaluator()
		{
			return new HSSFFormulaEvaluator(workbook);
		}

		/// Creates a HSSFClientAnchor. Use this object to position drawing object in a sheet
		///
		/// @return a HSSFClientAnchor instance
		/// @see NPOI.SS.usermodel.Drawing
		public IClientAnchor CreateClientAnchor()
		{
			return new HSSFClientAnchor();
		}
	}
}
