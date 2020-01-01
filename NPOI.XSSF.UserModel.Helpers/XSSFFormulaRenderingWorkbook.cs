using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;

namespace NPOI.XSSF.UserModel.Helpers
{
	internal class XSSFFormulaRenderingWorkbook : IFormulaRenderingWorkbook
	{
		private XSSFEvaluationWorkbook _fpwb;

		private int _sheetIndex;

		private string _name;

		public XSSFFormulaRenderingWorkbook(XSSFEvaluationWorkbook fpwb, int sheetIndex, string name)
		{
			_fpwb = fpwb;
			_sheetIndex = sheetIndex;
			_name = name;
		}

		public ExternalSheet GetExternalSheet(int externSheetIndex)
		{
			return _fpwb.GetExternalSheet(externSheetIndex);
		}

		public string GetSheetNameByExternSheet(int externSheetIndex)
		{
			if (externSheetIndex == _sheetIndex)
			{
				return _name;
			}
			return _fpwb.GetSheetNameByExternSheet(externSheetIndex);
		}

		public string ResolveNameXText(NameXPtg nameXPtg)
		{
			return _fpwb.ResolveNameXText(nameXPtg);
		}

		public string GetNameText(NamePtg namePtg)
		{
			return _fpwb.GetNameText(namePtg);
		}
	}
}
