namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// Specifies the possible ways to store a chart element's position.
	/// </summary>
	/// <remarks>
	/// @author Roman Kashitsyn
	/// </remarks>
	public enum LayoutMode
	{
		/// <summary>
		/// Specifies that the Width or Height shall be interpreted as the Right or Bottom of the chart element.
		/// </summary>
		Edge,
		/// <summary>
		/// Specifies that the Width or Height shall be interpreted as the width or Height of the chart element.
		/// </summary>
		Factor
	}
}
