using NPOI.SS.UserModel.Charts;

namespace NPOI.XSSF.UserModel.Charts
{
	/// @author Roman Kashitsyn
	public class XSSFChartDataFactory : IChartDataFactory
	{
		private static XSSFChartDataFactory instance;

		private XSSFChartDataFactory()
		{
		}

		/// @return new scatter chart data instance
		public IScatterChartData<Tx, Ty> CreateScatterChartData<Tx, Ty>()
		{
			return new XSSFScatterChartData<Tx, Ty>();
		}

		public ILineChartData<Tx, Ty> CreateLineChartData<Tx, Ty>()
		{
			return new XSSFLineChartData<Tx, Ty>();
		}

		/// @return factory instance
		public static XSSFChartDataFactory GetInstance()
		{
			if (instance == null)
			{
				instance = new XSSFChartDataFactory();
			}
			return instance;
		}
	}
}
