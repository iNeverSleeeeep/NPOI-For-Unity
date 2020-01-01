namespace NPOI.SS.UserModel.Charts
{
	/// <summary>
	/// A factory for different chart data types.
	/// </summary>
	/// <remarks>
	/// @author Roman Kashitsyn
	/// </remarks>
	public interface IChartDataFactory
	{
		/// <summary>
		/// returns an appropriate ScatterChartData instance
		/// </summary>
		/// <returns></returns>
		IScatterChartData<Tx, Ty> CreateScatterChartData<Tx, Ty>();

		ILineChartData<Tx, Ty> CreateLineChartData<Tx, Ty>();
	}
}
