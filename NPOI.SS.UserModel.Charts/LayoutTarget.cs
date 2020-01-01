namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// Specifies whether to layout the plot area by its inside (not including axis
	/// and axis labels) or outside (including axis and axis labels).
	/// </summary>
	/// <remarks>
	/// @author Roman Kashitsyn
	/// </remarks>
	public enum LayoutTarget
	{
		/// <summary>
		/// Specifies that the plot area size shall determine the size of the plot area, not including the tick marks and axis labels.
		/// </summary>
		Inner,
		/// <summary>
		///             Specifies that the plot area size shall determine the
		///             size of the plot area, the tick marks, and the axis
		///             labels.
		/// </summary>
		Outer
	}
}
