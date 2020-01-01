namespace NPOI.SS.Util
{
	/// Holds information regarding a split plane or freeze plane for a sheet.
	public class PaneInformation
	{
		/// Constant for active pane being the lower right
		public const byte PANE_LOWER_RIGHT = 0;

		/// Constant for active pane being the upper right
		public const byte PANE_UPPER_RIGHT = 1;

		/// Constant for active pane being the lower left
		public const byte PANE_LOWER_LEFT = 2;

		/// Constant for active pane being the upper left
		public const byte PANE_UPPER_LEFT = 3;

		private short x;

		private short y;

		private short topRow;

		private short leftColumn;

		private byte activePane;

		private bool frozen;

		/// Returns the vertical position of the split.
		/// @return 0 if there is no vertical spilt,
		///         or for a freeze pane the number of columns in the TOP pane,
		///         or for a split plane the position of the split in 1/20th of a point.
		public short VerticalSplitPosition => x;

		/// Returns the horizontal position of the split.
		/// @return 0 if there is no horizontal spilt,
		///         or for a freeze pane the number of rows in the LEFT pane,
		///         or for a split plane the position of the split in 1/20th of a point.
		public short HorizontalSplitPosition => y;

		/// For a horizontal split returns the top row in the BOTTOM pane.
		/// @return 0 if there is no horizontal split, or the top row of the bottom pane.
		public short HorizontalSplitTopRow => topRow;

		/// For a vertical split returns the left column in the RIGHT pane.
		/// @return 0 if there is no vertical split, or the left column in the RIGHT pane.
		public short VerticalSplitLeftColumn => leftColumn;

		/// Returns the active pane
		/// @see #PANE_LOWER_RIGHT
		/// @see #PANE_UPPER_RIGHT
		/// @see #PANE_LOWER_LEFT
		/// @see #PANE_UPPER_LEFT
		/// @return the active pane.
		public byte ActivePane => activePane;

		public PaneInformation(short x, short y, short top, short left, byte active, bool frozen)
		{
			this.x = x;
			this.y = y;
			topRow = top;
			leftColumn = left;
			activePane = active;
			this.frozen = frozen;
		}

		/// Returns true if this is a Freeze pane, false if it is a split pane.
		public bool IsFreezePane()
		{
			return frozen;
		}
	}
}
