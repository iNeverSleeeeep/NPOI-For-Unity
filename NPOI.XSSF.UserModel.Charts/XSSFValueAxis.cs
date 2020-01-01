using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel.Charts;
using System;

namespace NPOI.XSSF.UserModel.Charts
{
	/// Value axis type.
	///
	/// @author Roman Kashitsyn
	public class XSSFValueAxis : XSSFChartAxis, IValueAxis, IChartAxis
	{
		private CT_ValAx ctValAx;

		public XSSFValueAxis(XSSFChart chart, long id, AxisPosition pos)
			: base(chart)
		{
			CreateAxis(id, pos);
		}

		public XSSFValueAxis(XSSFChart chart, CT_ValAx ctValAx)
			: base(chart)
		{
			this.ctValAx = ctValAx;
		}

		public override long GetId()
		{
			return ctValAx.axId.val;
		}

		public void SetCrossBetween(AxisCrossBetween crossBetween)
		{
			ctValAx.crossBetween.val = fromCrossBetween(crossBetween);
		}

		public AxisCrossBetween GetCrossBetween()
		{
			return ToCrossBetween(ctValAx.crossBetween.val);
		}

		protected override CT_Boolean GetDelete()
		{
			return ctValAx.delete;
		}

		protected override CT_TickMark GetMajorCTTickMark()
		{
			return ctValAx.majorTickMark;
		}

		protected override CT_TickMark GetMinorCTTickMark()
		{
			return ctValAx.minorTickMark;
		}

		protected override CT_AxPos GetCTAxPos()
		{
			return ctValAx.axPos;
		}

		protected override CT_NumFmt GetCTNumFmt()
		{
			if (ctValAx.IsSetNumFmt())
			{
				return ctValAx.numFmt;
			}
			return ctValAx.AddNewNumFmt();
		}

		protected override CT_Scaling GetCTScaling()
		{
			return ctValAx.scaling;
		}

		protected override CT_Crosses GetCTCrosses()
		{
			return ctValAx.crosses;
		}

		public override void CrossAxis(IChartAxis axis)
		{
			ctValAx.crossAx.val = (uint)axis.GetId();
		}

		private void CreateAxis(long id, AxisPosition pos)
		{
			ctValAx = chart.GetCTChart().plotArea.AddNewValAx();
			ctValAx.AddNewAxId().val = (uint)id;
			ctValAx.AddNewAxPos();
			ctValAx.AddNewScaling();
			ctValAx.AddNewCrossBetween();
			ctValAx.AddNewCrosses();
			ctValAx.AddNewCrossAx();
			ctValAx.AddNewTickLblPos().val = ST_TickLblPos.nextTo;
			SetPosition(pos);
			SetOrientation(AxisOrientation.MinToMax);
			SetCrossBetween(AxisCrossBetween.MidpointCategory);
			SetCrosses(AxisCrosses.AutoZero);
		}

		private static ST_CrossBetween fromCrossBetween(AxisCrossBetween crossBetween)
		{
			switch (crossBetween)
			{
			case AxisCrossBetween.Between:
				return ST_CrossBetween.between;
			case AxisCrossBetween.MidpointCategory:
				return ST_CrossBetween.midCat;
			default:
				throw new ArgumentException();
			}
		}

		private static AxisCrossBetween ToCrossBetween(ST_CrossBetween ctCrossBetween)
		{
			switch (ctCrossBetween)
			{
			case ST_CrossBetween.between:
				return AxisCrossBetween.Between;
			case ST_CrossBetween.midCat:
				return AxisCrossBetween.MidpointCategory;
			default:
				throw new ArgumentException();
			}
		}
	}
}