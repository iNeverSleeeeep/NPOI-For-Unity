namespace NPOI.SS.UserModel
{
	public enum HyperlinkType
	{
		Unknown,
		/// <summary>
		/// Link to an existing file or web page
		/// </summary>
		Url,
		/// <summary>
		/// Link to a place in this document
		/// </summary>
		Document,
		/// <summary>
		/// Link to an E-mail Address
		/// </summary>
		Email,
		/// <summary>
		/// Link to a file
		/// </summary>
		File
	}
}
