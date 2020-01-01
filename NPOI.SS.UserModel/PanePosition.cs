namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Define the position of the pane. One of lower/right, upper/right, lower/left and upper/left.
	/// </summary>
	public enum PanePosition : byte
	{
		/// <summary>
		/// referes to the lower/right corner
		/// </summary>
		LowerRight,
		/// <summary>
		/// referes to the upper/right corner
		/// </summary>
		UpperRight,
		/// <summary>
		/// referes to the lower/left corner
		/// </summary>
		LowerLeft,
		/// <summary>
		/// referes to the upper/left corner
		/// </summary>
		UpperLeft
	}
}
