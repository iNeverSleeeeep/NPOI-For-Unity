namespace NPOI.SS.Formula
{
	public class ExternalSheet
	{
		private string _workbookName;

		private string _sheetName;

		public ExternalSheet(string workbookName, string sheetName)
		{
			_workbookName = workbookName;
			_sheetName = sheetName;
		}

		public string GetWorkbookName()
		{
			return _workbookName;
		}

		public string GetSheetName()
		{
			return _sheetName;
		}
	}
}
