using System.Text;

namespace NPOI.SS.Formula.PTG
{
	public class ExternSheetNameResolver
	{
		public static string PrependSheetName(IFormulaRenderingWorkbook book, int field_1_index_extern_sheet, string cellRefText)
		{
			ExternalSheet externalSheet = book.GetExternalSheet(field_1_index_extern_sheet);
			StringBuilder stringBuilder;
			if (externalSheet != null)
			{
				string workbookName = externalSheet.GetWorkbookName();
				string sheetName = externalSheet.GetSheetName();
				stringBuilder = new StringBuilder(workbookName.Length + sheetName.Length + cellRefText.Length + 4);
				SheetNameFormatter.AppendFormat(stringBuilder, workbookName, sheetName);
			}
			else
			{
				string sheetNameByExternSheet = book.GetSheetNameByExternSheet(field_1_index_extern_sheet);
				stringBuilder = new StringBuilder(sheetNameByExternSheet.Length + cellRefText.Length + 4);
				if (sheetNameByExternSheet.Length < 1)
				{
					stringBuilder.Append("#REF");
				}
				else
				{
					SheetNameFormatter.AppendFormat(stringBuilder, sheetNameByExternSheet);
				}
			}
			stringBuilder.Append('!');
			stringBuilder.Append(cellRefText);
			return stringBuilder.ToString();
		}
	}
}
