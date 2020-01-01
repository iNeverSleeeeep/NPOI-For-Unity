namespace NPOI.SS.UserModel.Charts
{
	/// Abstraction of chart element that can be positioned with manual
	/// layout.
	///
	/// @author Roman Kashitsyn
	public interface ManuallyPositionable
	{
		/// Returns manual layout for the chart element.
		/// @return manual layout for the chart element.
		IManualLayout GetManualLayout();
	}
}
