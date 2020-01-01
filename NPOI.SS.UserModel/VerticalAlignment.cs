namespace NPOI.SS.UserModel
{
	/// This enumeration value indicates the type of vertical alignment for a cell, i.e.,
	/// whether it is aligned top, bottom, vertically centered, justified or distributed.
	public enum VerticalAlignment
	{
		/// The vertical alignment is aligned-to-top.
		Top,
		/// The vertical alignment is centered across the height of the cell.
		Center,
		/// The vertical alignment is aligned-to-bottom.
		Bottom,
		/// <p>
		/// When text direction is horizontal: the vertical alignment of lines of text is distributed vertically,
		/// where each line of text inside the cell is evenly distributed across the height of the cell,
		/// with flush top and bottom margins.
		/// </p>
		/// <p>
		/// When text direction is vertical: similar behavior as horizontal justification.
		/// The alignment is justified (flush top and bottom in this case). For each line of text, each
		/// line of the wrapped text in a cell is aligned to the top and bottom (except the last line).
		/// If no single line of text wraps in the cell, then the text is not justified.
		///  </p>
		Justify,
		/// <p>
		/// When text direction is horizontal: the vertical alignment of lines of text is distributed vertically,
		/// where each line of text inside the cell is evenly distributed across the height of the cell,
		/// with flush top
		/// </p>
		/// <p>
		/// When text direction is vertical: behaves exactly as distributed horizontal alignment.
		/// The first words in a line of text (appearing at the top of the cell) are flush
		/// with the top edge of the cell, and the last words of a line of text are flush with the bottom edge of the cell,
		/// and the line of text is distributed evenly from top to bottom.
		/// </p>
		Distributed
	}
}
