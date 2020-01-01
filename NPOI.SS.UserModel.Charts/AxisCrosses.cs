namespace NPOI.SS.UserModel.Charts
{
	/// Specifies the possible crossing points for an axis.
	///
	/// @author Roman Kashitsyn
	public enum AxisCrosses
	{
		/// The category axis crosses at the zero point of the value axis (if
		/// possible), or the minimum value (if the minimum is greater than zero) or
		/// the maximum (if the maximum is less than zero).
		AutoZero,
		/// The axis crosses at the maximum value.
		Min,
		/// Axis crosses at the minimum value of the chart.
		Max
	}
}
