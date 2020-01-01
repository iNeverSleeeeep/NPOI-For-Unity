namespace NPOI.SS.UserModel
{
	public interface ITextbox : IShape
	{
		/// @return  the rich text string for this textbox.
		IRichTextString String
		{
			get;
			set;
		}

		/// @return  Returns the left margin within the textbox.
		int MarginLeft
		{
			get;
			set;
		}

		/// @return    returns the right margin within the textbox.
		int MarginRight
		{
			get;
			set;
		}

		/// @return  returns the top margin within the textbox.
		int MarginTop
		{
			get;
			set;
		}

		/// s the bottom margin within the textbox.
		int MarginBottom
		{
			get;
			set;
		}

		short HorizontalAlignment
		{
			get;
			set;
		}

		short VerticalAlignment
		{
			get;
			set;
		}
	}
}
