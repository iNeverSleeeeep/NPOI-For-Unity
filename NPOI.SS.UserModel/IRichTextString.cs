namespace NPOI.SS.UserModel
{
	/// Rich text unicode string.  These strings can have fonts 
	///  applied to arbitary parts of the string.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Jason Height (jheight at apache.org)
	public interface IRichTextString
	{
		/// Returns the plain string representation.
		string String
		{
			get;
		}

		/// @return  the number of characters in the font.
		int Length
		{
			get;
		}

		/// @return  The number of formatting Runs used.
		int NumFormattingRuns
		{
			get;
		}

		/// Applies a font to the specified characters of a string.
		///
		/// @param startIndex    The start index to apply the font to (inclusive)
		/// @param endIndex      The end index to apply the font to (exclusive)
		/// @param fontIndex     The font to use.
		void ApplyFont(int startIndex, int endIndex, short fontIndex);

		/// Applies a font to the specified characters of a string.
		///
		/// @param startIndex    The start index to apply the font to (inclusive)
		/// @param endIndex      The end index to apply to font to (exclusive)
		/// @param font          The index of the font to use.
		void ApplyFont(int startIndex, int endIndex, IFont font);

		/// Sets the font of the entire string.
		/// @param font          The font to use.
		void ApplyFont(IFont font);

		short GetFontAtIndex(int i);

		/// Removes any formatting that may have been applied to the string.
		void ClearFormatting();

		/// The index within the string to which the specified formatting run applies.
		/// @param index     the index of the formatting run
		/// @return  the index within the string.
		int GetIndexOfFormattingRun(int index);

		/// Applies the specified font to the entire string.
		///
		/// @param fontIndex  the font to apply.
		void ApplyFont(short fontIndex);
	}
}
