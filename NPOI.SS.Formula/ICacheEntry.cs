using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula
{
	/// A (mostly) opaque interface To allow test clients To trace cache values
	/// Each spreadsheet cell Gets one unique cache entry instance.  These objects
	/// are safe To use as keys in {@link java.util.HashMap}s 
	public interface ICacheEntry
	{
		ValueEval GetValue();
	}
}
