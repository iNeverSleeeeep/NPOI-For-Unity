namespace NPOI.SS.UserModel
{
	public interface IComment
	{
		/// Sets whether this comment is visible.
		///
		/// @return <c>true</c> if the comment is visible, <c>false</c> otherwise
		bool Visible
		{
			get;
			set;
		}

		/// Return the row of the cell that Contains the comment
		///
		/// @return the 0-based row of the cell that Contains the comment
		int Row
		{
			get;
			set;
		}

		/// Return the column of the cell that Contains the comment
		///
		/// @return the 0-based column of the cell that Contains the comment
		int Column
		{
			get;
			set;
		}

		/// Name of the original comment author
		///
		/// @return the name of the original author of the comment
		string Author
		{
			get;
			set;
		}

		/// Fetches the rich text string of the comment
		IRichTextString String
		{
			get;
			set;
		}
	}
}
