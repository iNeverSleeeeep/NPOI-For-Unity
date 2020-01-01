using System.Collections;

namespace NPOI.SS.Util
{
	/// Holds a collection of Sheet names and their associated
	/// reference numbers.
	///
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	public class SheetReferences
	{
		private Hashtable map;

		public SheetReferences()
		{
			map = new Hashtable(5);
		}

		public void AddSheetReference(string sheetName, int number)
		{
			map[number] = sheetName;
		}

		public string GetSheetName(int number)
		{
			return (string)map[number];
		}
	}
}
