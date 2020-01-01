namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// High level representation of chart legend.
	/// </summary>
	/// <remarks>@author Roman Kashitsyn</remarks>
	public interface IChartLegend : ManuallyPositionable
	{
		/// <summary>
		/// legend position
		/// </summary>
		/// <returns></returns>
		LegendPosition Position
		{
			get;
			set;
		}
	}
}
