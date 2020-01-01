namespace NPOI.XWPF.UserModel
{
	/// Specifies possible values for the alignment of the contents of this run in
	/// relation to the default appearance of the Run's text. This allows the text to
	/// be repositioned as subscript or superscript without altering the font size of
	/// the run properties.
	///
	/// @author Gisella Bronzetti
	public enum VerticalAlign
	{
		/// Specifies that the text in the parent run shall be located at the
		/// baseline and presented in the same size as surrounding text.
		BASELINE = 1,
		/// Specifies that this text should be subscript. This Setting shall lower
		/// the text in this run below the baseline and change it to a smaller size,
		/// if a smaller size is available.
		SUPERSCRIPT,
		/// Specifies that this text should be superscript. This Setting shall raise
		/// the text in this run above the baseline and change it to a smaller size,
		/// if a smaller size is available.
		SUBSCRIPT
	}
}
