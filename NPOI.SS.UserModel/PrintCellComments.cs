namespace NPOI.SS.UserModel
{
	/// These enumerations specify how cell comments shall be displayed for paper printing purposes.
	///
	/// @author Gisella Bronzetti
	public class PrintCellComments
	{
		/// Do not print cell comments.
		public static PrintCellComments NONE;

		/// Print cell comments as displayed.
		public static PrintCellComments AS_DISPLAYED;

		/// Print cell comments at end of document.
		public static PrintCellComments AT_END;

		private int comments;

		private static PrintCellComments[] _table;

		public int Value => comments;

		static PrintCellComments()
		{
			_table = new PrintCellComments[4];
			NONE = new PrintCellComments(1);
			AS_DISPLAYED = new PrintCellComments(2);
			AT_END = new PrintCellComments(3);
		}

		private PrintCellComments(int comments)
		{
			this.comments = comments;
			_table[Value] = this;
		}

		public static PrintCellComments ValueOf(int value)
		{
			return _table[value];
		}
	}
}
