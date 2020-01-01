namespace NPOI.XWPF.UserModel
{
	/// Specifies all types of vertical alignment which are available to be applied to of all text 
	/// on each line displayed within a paragraph.
	///
	/// @author Gisella Bronzetti
	public enum TextAlignment
	{
		/// Specifies that all text in the parent object shall be 
		/// aligned to the top of each character when displayed
		TOP = 1,
		/// Specifies that all text in the parent object shall be 
		/// aligned to the center of each character when displayed.
		CENTER,
		/// Specifies that all text in the parent object shall be
		/// aligned to the baseline of each character when displayed.
		BASELINE,
		/// Specifies that all text in the parent object shall be
		/// aligned to the bottom of each character when displayed.
		BOTTOM,
		/// Specifies that all text in the parent object shall be 
		/// aligned automatically when displayed.
		AUTO
	}
}
