using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula
{
	/// Used for non-formula cells, primarily To keep track of the referencing (formula) cells.
	///
	/// @author Josh Micich
	public class PlainValueCellCacheEntry : CellCacheEntry
	{
		public PlainValueCellCacheEntry(ValueEval value)
		{
			UpdateValue(value);
		}
	}
}
