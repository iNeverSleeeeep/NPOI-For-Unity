using System.Collections.Generic;

namespace NPOI.SS.UserModel.Charts
{
	/// @author Roman Kashitsyn
	public interface IScatterChartData<Tx, Ty> : IChartData
	{
		/// @param xs data source to be used for X axis values
		/// @param ys data source to be used for Y axis values
		/// @return a new scatter charts serie
		IScatterChartSerie<Tx, Ty> AddSerie(IChartDataSource<Tx> xs, IChartDataSource<Ty> ys);

		/// @return list of all series
		List<IScatterChartSerie<Tx, Ty>> GetSeries();
	}
}
