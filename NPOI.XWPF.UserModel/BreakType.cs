namespace NPOI.XWPF.UserModel
{
	/// Specifies the possible types of break characters in a WordProcessingML
	/// document.
	/// The break type determines the next location where text shall be
	/// placed After this manual break is applied to the text contents
	///
	/// @author Gisella Bronzetti
	public enum BreakType
	{
		/// Specifies that the current break shall restart itself on the next page of
		/// the document when the document is displayed in page view.
		PAGE = 1,
		/// Specifies that the current break shall restart itself on the next column
		/// available on the current page when the document is displayed in page
		/// view.
		/// <p>
		/// If the current section is not divided into columns, or the column break
		/// occurs in the last column on the current page when displayed, then the
		/// restart location for text shall be the next page in the document.
		/// </p>
		COLUMN,
		/// Specifies that the current break shall restart itself on the next line in
		/// the document when the document is displayed in page view.
		/// The determine of the next line shall be done subject to the value of the clear
		/// attribute on the specified break character.
		TEXTWRAPPING
	}
}
