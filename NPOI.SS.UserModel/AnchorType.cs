namespace NPOI.SS.UserModel
{
	public enum AnchorType
	{
		/// Move and Resize With Anchor Cells
		/// <p>
		/// Specifies that the current drawing shall move and
		/// resize to maintain its row and column anchors (i.e. the
		/// object is anchored to the actual from and to row and column)
		/// </p>
		MoveAndResize = 0,
		/// Move With Cells but Do Not Resize
		/// <p>
		/// Specifies that the current drawing shall move with its
		/// row and column (i.e. the object is anchored to the
		/// actual from row and column), but that the size shall remain absolute.
		/// </p>
		/// <p>
		/// If Additional rows/columns are Added between the from and to locations of the drawing,
		/// the drawing shall move its to anchors as needed to maintain this same absolute size.
		/// </p>
		MoveDontResize = 2,
		/// Do Not Move or Resize With Underlying Rows/Columns
		/// <p>
		/// Specifies that the current start and end positions shall
		/// be maintained with respect to the distances from the
		/// absolute start point of the worksheet.
		/// </p>
		/// <p>
		/// If Additional rows/columns are Added before the
		/// drawing, the drawing shall move its anchors as needed
		/// to maintain this same absolute position.
		/// </p>
		DontMoveAndResize = 3
	}
}
