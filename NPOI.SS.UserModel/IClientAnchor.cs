namespace NPOI.SS.UserModel
{
	/// A client anchor is attached to an excel worksheet.  It anchors against a
	/// top-left and bottom-right cell.
	///
	/// @author Yegor Kozlov
	public interface IClientAnchor
	{
		/// Returns the column (0 based) of the first cell.
		///
		/// @return 0-based column of the first cell.
		int Col1
		{
			get;
			set;
		}

		/// Returns the column (0 based) of the second cell.
		///
		/// @return 0-based column of the second cell.
		int Col2
		{
			get;
			set;
		}

		/// Returns the row (0 based) of the first cell.
		///
		/// @return 0-based row of the first cell.
		int Row1
		{
			get;
			set;
		}

		/// Returns the row (0 based) of the second cell.
		///
		/// @return 0-based row of the second cell.
		int Row2
		{
			get;
			set;
		}

		/// Returns the x coordinate within the first cell
		///
		/// @return the x coordinate within the first cell
		int Dx1
		{
			get;
			set;
		}

		/// Returns the y coordinate within the first cell
		///
		/// @return the y coordinate within the first cell
		int Dy1
		{
			get;
			set;
		}

		/// Sets the y coordinate within the second cell
		///
		/// @return the y coordinate within the second cell
		int Dy2
		{
			get;
			set;
		}

		/// Returns the x coordinate within the second cell
		///
		/// @return the x coordinate within the second cell
		int Dx2
		{
			get;
			set;
		}

		/// s the anchor type
		/// <p>
		/// 0 = Move and size with Cells, 2 = Move but don't size with cells, 3 = Don't move or size with cells.
		/// </p>
		/// @return the anchor type
		/// @see #MOVE_AND_RESIZE
		/// @see #MOVE_DONT_RESIZE
		/// @see #DONT_MOVE_AND_RESIZE
		int AnchorType
		{
			get;
			set;
		}
	}
}
