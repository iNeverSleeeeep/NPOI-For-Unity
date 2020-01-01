namespace NPOI.SS.UserModel.Charts
{
	/// Specifies the possible crossing states of an axis.
	///
	///              @author Roman Kashitsyn
	public enum AxisCrossBetween
	{
		/// Specifies the value axis shall cross the category axis
		/// between data markers.
		Between,
		/// Specifies the value axis shall cross the category axis at
		/// the midpoint of a category.
		MidpointCategory
	}
}
