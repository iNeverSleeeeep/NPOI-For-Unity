namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// A factory for different chart axis.
	/// </summary>
	/// <remarks>@author Roman Kashitsyn</remarks>
	public interface IChartAxisFactory
	{
		/// <summary>
		/// returns new value axis
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		IValueAxis CreateValueAxis(AxisPosition pos);

		IChartAxis CreateCategoryAxis(AxisPosition pos);
	}
}
