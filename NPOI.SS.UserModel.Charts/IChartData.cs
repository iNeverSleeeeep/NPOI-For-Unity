namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// A base for all chart data types.
	/// </summary>
	/// <remarks>
	/// @author  Roman Kashitsyn
	/// </remarks>
	public interface IChartData
	{
		/// <summary>
		/// Fills a chart with data specified by implementation.
		/// </summary>
		/// <param name="chart">a chart to fill in</param>
		/// <param name="axis">chart axis to use</param>
		void FillChart(IChart chart, params IChartAxis[] axis);
	}
}
