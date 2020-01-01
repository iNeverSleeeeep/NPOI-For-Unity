namespace NPOI.SS.Formula
{
	/// Should be implemented by any {@link Ptg} subclass that needs a workbook To render its formula.
	/// <br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface WorkbookDependentFormula
	{
		string ToFormulaString(IFormulaRenderingWorkbook book);
	}
}
