namespace NPOI.SS.Formula
{
	/// Abstracts a sheet for the purpose of formula evaluation.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public interface IEvaluationSheet
	{
		/// @return <c>null</c> if there is no cell at the specified coordinates
		IEvaluationCell GetCell(int rowIndex, int columnIndex);
	}
}
