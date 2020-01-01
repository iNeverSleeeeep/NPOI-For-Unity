using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using System;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel.Charts
{
	/// Represents DrawingML scatter chart.
	///
	/// @author Roman Kashitsyn
	public class XSSFScatterChartData<Tx, Ty> : IScatterChartData<Tx, Ty>, IChartData
	{
		/// Package private ScatterChartSerie implementation.
		internal class Serie : AbstractXSSFChartSerie, IScatterChartSerie<Tx, Ty>, IChartSerie
		{
			private int id;

			private int order;

			private bool useCache;

			private IChartDataSource<Tx> xs;

			private IChartDataSource<Ty> ys;

			internal Serie(int id, int order, IChartDataSource<Tx> xs, IChartDataSource<Ty> ys)
			{
				this.id = id;
				this.order = order;
				this.xs = xs;
				this.ys = ys;
			}

			/// Returns data source used for X axis values.
			/// @return data source used for X axis values
			public IChartDataSource<Tx> GetXValues()
			{
				return xs;
			}

			/// Returns data source used for Y axis values.
			/// @return data source used for Y axis values
			public IChartDataSource<Ty> GetYValues()
			{
				return ys;
			}

			/// @param useCache if true, cached results will be Added on plot
			public void SetUseCache(bool useCache)
			{
				this.useCache = useCache;
			}

			internal void AddToChart(CT_ScatterChart ctScatterChart)
			{
				CT_ScatterSer cT_ScatterSer = ctScatterChart.AddNewSer();
				cT_ScatterSer.AddNewIdx().val = (uint)id;
				cT_ScatterSer.AddNewOrder().val = (uint)order;
				CT_AxDataSource ctAxDataSource = cT_ScatterSer.AddNewXVal();
				XSSFChartUtil.BuildAxDataSource(ctAxDataSource, xs);
				CT_NumDataSource ctNumDataSource = cT_ScatterSer.AddNewYVal();
				XSSFChartUtil.BuildNumDataSource(ctNumDataSource, ys);
				if (base.IsTitleSet)
				{
					cT_ScatterSer.tx = GetCTSerTx();
				}
			}
		}

		/// List of all data series.
		private List<IScatterChartSerie<Tx, Ty>> series;

		public XSSFScatterChartData()
		{
			series = new List<IScatterChartSerie<Tx, Ty>>();
		}

		public IScatterChartSerie<Tx, Ty> AddSerie(IChartDataSource<Tx> xs, IChartDataSource<Ty> ys)
		{
			if (!ys.IsNumeric)
			{
				throw new ArgumentException("Y axis data source must be numeric.");
			}
			int count = series.Count;
			Serie serie = new Serie(count, count, xs, ys);
			series.Add(serie);
			return serie;
		}

		public void FillChart(IChart chart, IChartAxis[] axis)
		{
			if (!(chart is XSSFChart))
			{
				throw new ArgumentException("Chart must be instance of XSSFChart");
			}
			XSSFChart xSSFChart = (XSSFChart)chart;
			CT_PlotArea plotArea = xSSFChart.GetCTChart().plotArea;
			CT_ScatterChart cT_ScatterChart = plotArea.AddNewScatterChart();
			AddStyle(cT_ScatterChart);
			foreach (Serie item in series)
			{
				item.AddToChart(cT_ScatterChart);
			}
			foreach (IChartAxis chartAxis in axis)
			{
				cT_ScatterChart.AddNewAxId().val = (uint)chartAxis.GetId();
			}
		}

		public List<IScatterChartSerie<Tx, Ty>> GetSeries()
		{
			return series;
		}

		private void AddStyle(CT_ScatterChart ctScatterChart)
		{
			CT_ScatterStyle cT_ScatterStyle = ctScatterChart.AddNewScatterStyle();
			cT_ScatterStyle.val = ST_ScatterStyle.lineMarker;
		}
	}
}
