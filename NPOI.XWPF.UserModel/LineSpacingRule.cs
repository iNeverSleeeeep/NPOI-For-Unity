namespace NPOI.XWPF.UserModel
{
	/// Specifies the logic which shall be used to calculate the line spacing of the
	/// parent object when it is displayed in the document.
	///
	/// @author Gisella Bronzetti
	public enum LineSpacingRule
	{
		/// Specifies that the line spacing of the parent object shall be
		/// automatically determined by the size of its contents, with no
		/// predetermined minimum or maximum size.
		AUTO = 1,
		/// Specifies that the height of the line shall be exactly the value
		/// specified, regardless of the size of the contents If the contents are too
		/// large for the specified height, then they shall be clipped as necessary.
		EXACT,
		/// Specifies that the height of the line shall be at least the value
		/// specified, but may be expanded to fit its content as needed.
		ATLEAST
	}
}
