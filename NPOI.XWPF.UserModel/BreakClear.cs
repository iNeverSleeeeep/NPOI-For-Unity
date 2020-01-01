namespace NPOI.XWPF.UserModel
{
	/// Specifies the Set of possible restart locations which may be used as to
	/// determine the next available line when a break's type attribute has a value
	/// of textWrapping.
	///
	/// @author Gisella Bronzetti
	public enum BreakClear
	{
		/// Specifies that the text wrapping break shall advance the text to the next
		/// line in the WordProcessingML document, regardless of its position left to
		/// right or the presence of any floating objects which intersect with the
		/// line,
		///
		/// This is the Setting for a typical line break in a document.
		NONE = 1,
		/// Specifies that the text wrapping break shall behave as follows:
		/// <ul>
		/// <li> If this line is broken into multiple regions (a floating object in
		/// the center of the page has text wrapping on both sides:
		/// <ul>
		/// <li> If this is the leftmost region of text flow on this line, advance
		/// the text to the next position on the line </li>
		/// <li>Otherwise, treat this as a text wrapping break of type all. </li>
		/// </ul>
		/// </li>
		/// <li> If this line is not broken into multiple regions, then treat this
		/// break as a text wrapping break of type none. </li>
		/// </ul>
		/// <li> If the parent paragraph is right to left, then these behaviors are
		/// also reversed. </li>
		LEFT,
		RIGHT,
		/// Specifies that the text wrapping break shall advance the text to the next
		/// line in the WordProcessingML document which spans the full width of the
		/// line.
		ALL
	}
}
