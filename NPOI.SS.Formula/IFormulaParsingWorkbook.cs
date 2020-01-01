using NPOI.SS.Formula.PTG;

namespace NPOI.SS.Formula
{
	/// Abstracts a workbook for the purpose of formula parsing.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IFormulaParsingWorkbook
	{
		/// <summary>
		/// named range name matching is case insensitive
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="sheetIndex">Index of the sheet.</param>
		/// <returns></returns>        
		IEvaluationName GetName(string name, int sheetIndex);

		/// <summary>
		/// Gets the name XPTG.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		NameXPtg GetNameXPtg(string name);

		/// <summary>
		/// Gets the externSheet index for a sheet from this workbook
		/// </summary>
		/// <param name="sheetName">Name of the sheet.</param>
		/// <returns></returns>
		int GetExternalSheetIndex(string sheetName);

		/// <summary>
		/// Gets the externSheet index for a sheet from an external workbook
		/// </summary>
		/// <param name="workbookName">Name of the workbook, e.g. "BudGet.xls"</param>
		/// <param name="sheetName">a name of a sheet in that workbook</param>
		/// <returns></returns>
		int GetExternalSheetIndex(string workbookName, string sheetName);

		/// <summary>
		/// Returns an enum holding spReadhseet properties specific to an Excel version (
		/// max column and row numbers, max arguments to a function, etc.)
		/// </summary>
		/// <returns></returns>
		SpreadsheetVersion GetSpreadsheetVersion();
	}
}
