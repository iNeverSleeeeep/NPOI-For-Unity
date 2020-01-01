namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public interface AreaEval : TwoDEval, ValueEval
	{
		/// returns the 0-based index of the first row in
		/// this area.
		int FirstRow
		{
			get;
		}

		/// returns the 0-based index of the last row in
		/// this area.
		int LastRow
		{
			get;
		}

		/// returns the 0-based index of the first col in
		/// this area.
		int FirstColumn
		{
			get;
		}

		/// returns the 0-based index of the last col in
		/// this area.
		int LastColumn
		{
			get;
		}

		/// returns true if the cell at row and col specified 
		/// as absolute indexes in the sheet is contained in 
		/// this area.
		/// @param row
		/// @param col
		bool Contains(int row, int col);

		/// returns true if the specified col is in range
		/// @param col
		bool ContainsColumn(int col);

		/// returns true if the specified row is in range
		/// @param row
		bool ContainsRow(int row);

		/// @return the ValueEval from within this area at the specified row and col index. Never
		/// <code>null</code> (possibly {@link BlankEval}).  The specified indexes should be absolute
		/// indexes in the sheet and not relative indexes within the area.
		ValueEval GetAbsoluteValue(int row, int col);

		/// @return the ValueEval from within this area at the specified relativeRowIndex and 
		/// relativeColumnIndex. Never <c>null</c> (possibly {@link BlankEval}). The
		/// specified indexes should relative to the top left corner of this area.  
		ValueEval GetRelativeValue(int relativeRowIndex, int relativeColumnIndex);

		/// Creates an {@link AreaEval} offset by a relative amount from from the upper left cell
		/// of this area
		AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx);
	}
}
