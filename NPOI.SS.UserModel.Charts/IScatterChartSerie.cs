namespace NPOI.SS.UserModel.Charts
{
	/// Represents scatter charts serie.
	/// @author Roman Kashitsyn
	public interface IScatterChartSerie<Tx, Ty> : IChartSerie
	{
		/// @return data source used for X axis values
		IChartDataSource<Tx> GetXValues();

		/// @return data source used for Y axis values
		IChartDataSource<Ty> GetYValues();
	}
}
