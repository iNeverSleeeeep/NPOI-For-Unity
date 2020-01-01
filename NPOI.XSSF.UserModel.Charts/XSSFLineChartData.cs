using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using System;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel.Charts
{
	public class XSSFLineChartData<Tx, Ty> : ILineChartData<Tx, Ty>, IChartData
	{
		public class Serie : AbstractXSSFChartSerie, ILineChartSerie<Tx, Ty>, IChartSerie
		{
			private int id;

			private int order;

			private IChartDataSource<Tx> categories;

			private IChartDataSource<Ty> values;

			internal Serie(int id, int order, IChartDataSource<Tx> categories, IChartDataSource<Ty> values)
			{
				this.id = id;
				this.order = order;
				this.categories = categories;
				this.values = values;
			}

			public IChartDataSource<Tx> GetCategoryAxisData()
			{
				return categories;
			}

			public IChartDataSource<Ty> GetValues()
			{
				return values;
			}

			internal void AddToChart(CT_LineChart ctLineChart)
			{
				CT_LineSer cT_LineSer = ctLineChart.AddNewSer();
				CT_Grouping cT_Grouping = ctLineChart.AddNewGrouping();
				cT_Grouping.val = ST_Grouping.standard;
				cT_LineSer.AddNewIdx().val = (uint)id;
				cT_LineSer.AddNewOrder().val = (uint)order;
				cT_LineSer.AddNewMarker().AddNewSymbol().val = ST_MarkerStyle.none;
				CT_AxDataSource ctAxDataSource = cT_LineSer.AddNewCat();
				XSSFChartUtil.BuildAxDataSource(ctAxDataSource, categories);
				CT_NumDataSource ctNumDataSource = cT_LineSer.AddNewVal();
				XSSFChartUtil.BuildNumDataSource(ctNumDataSource, values);
				if (base.IsTitleSet)
				{
					cT_LineSer.tx = GetCTSerTx();
				}
			}
		}

		/// List of all data series.
		private List<ILineChartSerie<Tx, Ty>> series;

		public XSSFLineChartData()
		{
			series = new List<ILineChartSerie<Tx, Ty>>();
		}

		public ILineChartSerie<Tx, Ty> AddSerie(IChartDataSource<Tx> categoryAxisData, IChartDataSource<Ty> values)
		{
			if (!values.IsNumeric)
			{
				throw new ArgumentException("Value data source must be numeric.");
			}
			int count = series.Count;
			Serie serie = new Serie(count, count, categoryAxisData, values);
			series.Add(serie);
			return serie;
		}

		public List<ILineChartSerie<Tx, Ty>> GetSeries()
		{
			return series;
		}

		public void FillChart(IChart chart, params IChartAxis[] axis)
		{
			if (!(chart is XSSFChart))
			{
				throw new ArgumentException("Chart must be instance of XSSFChart");
			}
			XSSFChart xSSFChart = (XSSFChart)chart;
			CT_PlotArea plotArea = xSSFChart.GetCTChart().plotArea;
			CT_LineChart cT_LineChart = plotArea.AddNewLineChart();
			cT_LineChart.AddNewVaryColors().val = 0;
			foreach (Serie item in series)
			{
				item.AddToChart(cT_LineChart);
			}
			foreach (IChartAxis chartAxis in axis)
			{
				cT_LineChart.AddNewAxId().val = (uint)chartAxis.GetId();
			}
		}
	}
}
