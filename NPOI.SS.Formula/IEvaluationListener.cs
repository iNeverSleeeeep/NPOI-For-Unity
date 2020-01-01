using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula
{
	/// Tests can implement this class To track the internal working of the {@link WorkbookEvaluator}.<br />
	///
	/// For POI internal testing use only
	///
	/// @author Josh Micich
	public interface IEvaluationListener
	{
		void OnCacheHit(int sheetIndex, int rowIndex, int columnIndex, ValueEval result);

		void OnReadPlainValue(int sheetIndex, int rowIndex, int columnIndex, ICacheEntry entry);

		void OnStartEvaluate(IEvaluationCell cell, ICacheEntry entry);

		void OnEndEvaluate(ICacheEntry entry, ValueEval result);

		void OnClearWholeCache();

		void OnClearCachedValue(ICacheEntry entry);

		/// Internally, formula {@link ICacheEntry}s are stored in Sets which may Change ordering due 
		/// To seemingly trivial Changes.  This method is provided To make the order of call-backs To 
		/// {@link #onClearDependentCachedValue(ICacheEntry, int)} more deterministic.
		void SortDependentCachedValues(ICacheEntry[] formulaCells);

		void OnClearDependentCachedValue(ICacheEntry formulaCell, int depth);

		void OnChangeFromBlankValue(int sheetIndex, int rowIndex, int columnIndex, IEvaluationCell cell, ICacheEntry entry);
	}
}
