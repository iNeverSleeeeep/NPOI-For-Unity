namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Indicate the position of the margin. One of left, right, top and bottom.
	/// </summary>
	public enum MarginType : short
	{
		/// <summary>
		/// referes to the left margin
		/// </summary>
		LeftMargin,
		/// <summary>
		/// referes to the right margin
		/// </summary>
		RightMargin,
		/// <summary>
		/// referes to the top margin
		/// </summary>
		TopMargin,
		/// <summary>
		/// referes to the bottom margin
		/// </summary>
		BottomMargin,
		HeaderMargin,
		FooterMargin
	}
}
