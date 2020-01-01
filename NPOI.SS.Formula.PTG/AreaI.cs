namespace NPOI.SS.Formula.PTG
{
	/// Common interface for AreaPtg and Area3DPtg, and their
	///  child classes.
	public interface AreaI
	{
		/// @return the first row in the area
		int FirstRow
		{
			get;
		}

		/// @return last row in the range (x2 in x1,y1-x2,y2)
		int LastRow
		{
			get;
		}

		/// @return the first column number in the area.
		int FirstColumn
		{
			get;
		}

		/// @return lastcolumn in the area
		int LastColumn
		{
			get;
		}
	}
}
