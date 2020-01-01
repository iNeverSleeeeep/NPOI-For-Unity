using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel.Charts;
using System;

namespace NPOI.XSSF.UserModel.Charts
{
	/// Represents a SpreadsheetML chart legend
	/// @author Roman Kashitsyn
	public class XSSFChartLegend : IChartLegend, ManuallyPositionable
	{
		/// Underlaying CTLagend bean
		private CT_Legend legend;

		public LegendPosition Position
		{
			get
			{
				if (legend.IsSetLegendPos())
				{
					return ToLegendPosition(legend.legendPos);
				}
				return LegendPosition.Right;
			}
			set
			{
				if (!legend.IsSetLegendPos())
				{
					legend.AddNewLegendPos();
				}
				legend.legendPos.val = FromLegendPosition(value);
				legend.legendPosSpecified = true;
			}
		}

		/// Create a new SpreadsheetML chart legend
		public XSSFChartLegend(XSSFChart chart)
		{
			CT_Chart cTChart = chart.GetCTChart();
			legend = (cTChart.IsSetLegend() ? cTChart.legend : cTChart.AddNewLegend());
		}

		/// Return the underlying CTLegend bean.
		///
		/// @return the underlying CTLegend bean
		internal CT_Legend GetCTLegend()
		{
			return legend;
		}

		public IManualLayout GetManualLayout()
		{
			if (!legend.IsSetLayout())
			{
				legend.AddNewLayout();
			}
			return new XSSFManualLayout(legend.layout);
		}

		private ST_LegendPos FromLegendPosition(LegendPosition position)
		{
			switch (position)
			{
			case LegendPosition.Bottom:
				return ST_LegendPos.b;
			case LegendPosition.Left:
				return ST_LegendPos.l;
			case LegendPosition.Right:
				return ST_LegendPos.r;
			case LegendPosition.Top:
				return ST_LegendPos.t;
			case LegendPosition.TopRight:
				return ST_LegendPos.tr;
			default:
				throw new ArgumentException();
			}
		}

		private LegendPosition ToLegendPosition(CT_LegendPos ctLegendPos)
		{
			switch (ctLegendPos.val)
			{
			case ST_LegendPos.b:
				return LegendPosition.Bottom;
			case ST_LegendPos.l:
				return LegendPosition.Left;
			case ST_LegendPos.r:
				return LegendPosition.Right;
			case ST_LegendPos.t:
				return LegendPosition.Top;
			case ST_LegendPos.tr:
				return LegendPosition.TopRight;
			default:
				throw new ArgumentException();
			}
		}
	}
}
