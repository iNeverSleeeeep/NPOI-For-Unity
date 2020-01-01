using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;

namespace NPOI.SS.Formula
{
	/// Abstracts a workbook for the purpose of formula evaluation.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IEvaluationWorkbook
	{
		string GetSheetName(int sheetIndex);

		/// @return -1 if the specified sheet is from a different book
		int GetSheetIndex(IEvaluationSheet sheet);

		int GetSheetIndex(string sheetName);

		IEvaluationSheet GetSheet(int sheetIndex);

		/// @return <c>null</c> if externSheetIndex refers To a sheet inside the current workbook
		ExternalSheet GetExternalSheet(int externSheetIndex);

		int ConvertFromExternSheetIndex(int externSheetIndex);

		ExternalName GetExternalName(int externSheetIndex, int externNameIndex);

		IEvaluationName GetName(NamePtg namePtg);

		IEvaluationName GetName(string name, int sheetIndex);

		string ResolveNameXText(NameXPtg ptg);

		Ptg[] GetFormulaTokens(IEvaluationCell cell);

		UDFFinder GetUDFFinder();
	}
}
