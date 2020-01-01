using NPOI.SS.Formula.PTG;

namespace NPOI.SS.Formula
{
	/// Abstracts a workbook for the purpose of converting formula To text.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IFormulaRenderingWorkbook
	{
		/// @return <c>null</c> if externSheetIndex refers To a sheet inside the current workbook
		ExternalSheet GetExternalSheet(int externSheetIndex);

		string GetSheetNameByExternSheet(int externSheetIndex);

		string ResolveNameXText(NameXPtg nameXPtg);

		string GetNameText(NamePtg namePtg);
	}
}
