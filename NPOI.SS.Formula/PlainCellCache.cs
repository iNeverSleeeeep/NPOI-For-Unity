using System.Collections;

namespace NPOI.SS.Formula
{
	/// @author Josh Micich
	public class PlainCellCache
	{
		private Hashtable _plainValueEntriesByLoc;

		public PlainCellCache()
		{
			_plainValueEntriesByLoc = new Hashtable();
		}

		public void Put(Loc key, PlainValueCellCacheEntry cce)
		{
			_plainValueEntriesByLoc[key] = cce;
		}

		public void Clear()
		{
			_plainValueEntriesByLoc.Clear();
		}

		public PlainValueCellCacheEntry Get(Loc key)
		{
			return (PlainValueCellCacheEntry)_plainValueEntriesByLoc[key];
		}

		public void Remove(Loc key)
		{
			_plainValueEntriesByLoc.Remove(key);
		}
	}
}
