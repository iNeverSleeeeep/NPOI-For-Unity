namespace NPOI.SS.UserModel.Charts
{
	/// @author Roman Kashitsyn
	public interface IValueAxis : IChartAxis
	{
		/// @return cross between type
		AxisCrossBetween GetCrossBetween();

		/// @param crossBetween cross between type
		void SetCrossBetween(AxisCrossBetween crossBetween);
	}
}
