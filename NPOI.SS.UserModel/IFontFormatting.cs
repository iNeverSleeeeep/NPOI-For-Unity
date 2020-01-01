namespace NPOI.SS.UserModel
{
	/// High level representation for Font Formatting component
	/// of Conditional Formatting Settings
	///
	/// @author Dmitriy Kumshayev
	/// @author Yegor Kozlov
	public interface IFontFormatting
	{
		/// <summary>
		/// get or set the type of super or subscript for the font
		/// </summary>
		FontSuperScript EscapementType
		{
			get;
			set;
		}

		/// <summary>
		/// get or set font color index
		/// </summary>
		short FontColorIndex
		{
			get;
			set;
		}

		/// <summary>
		/// get or set the height of the font in 1/20th point units
		/// </summary>
		int FontHeight
		{
			get;
			set;
		}

		/// <summary>
		/// get or set the type of underlining for the font
		/// </summary>
		FontUnderlineType UnderlineType
		{
			get;
			set;
		}

		/// Get whether the font weight is Set to bold or not
		///
		/// @return bold - whether the font is bold or not
		bool IsBold
		{
			get;
		}

		/// @return true if font style was Set to <i>italic</i>
		bool IsItalic
		{
			get;
		}

		/// Set font style options.
		///
		/// @param italic - if true, Set posture style to italic, otherwise to normal
		/// @param bold if true, Set font weight to bold, otherwise to normal
		void SetFontStyle(bool italic, bool bold);

		/// Set font style options to default values (non-italic, non-bold)
		void ResetFontStyle();
	}
}
