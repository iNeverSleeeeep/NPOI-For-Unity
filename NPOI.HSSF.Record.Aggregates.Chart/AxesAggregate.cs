using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// AXES = [IVAXIS DVAXIS [SERIESAXIS] / DVAXIS DVAXIS] *3ATTACHEDLABEL [PlotArea FRAME]
	/// </summary>
	public class AxesAggregate : ChartRecordAggregate
	{
		private IVAxisAggregate ivaxis;

		private DVAxisAggregate dvaxis;

		private DVAxisAggregate dvaxisSecond;

		private SeriesAxisAggregate seriesAxis;

		private List<AttachedLabelAggregate> attachedLabelList = new List<AttachedLabelAggregate>();

		private PlotAreaRecord plotArea;

		private FrameAggregate frame;

		public AxesAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("AXES", container)
		{
			if (rs.PeekNextChartSid() == 4125)
			{
				AxisRecord axis = (AxisRecord)rs.GetNext();
				rs.GetNext();
				int num = rs.PeekNextChartSid();
				switch (num)
				{
				case 4128:
					ivaxis = new IVAxisAggregate(rs, this, axis);
					break;
				case 4127:
					dvaxis = new DVAxisAggregate(rs, this, axis);
					break;
				default:
					throw new InvalidOperationException($"Invalid record sid=0x{num:X}. Shoud be CatSerRangeRecord or ValueRangeRecord");
				}
				dvaxisSecond = new DVAxisAggregate(rs, this, null);
				if (rs.PeekNextChartSid() == 4125)
				{
					seriesAxis = new SeriesAxisAggregate(rs, this);
				}
				while (rs.PeekNextChartSid() == 4133)
				{
					attachedLabelList.Add(new AttachedLabelAggregate(rs, this));
				}
				if (rs.PeekNextChartSid() == 4149)
				{
					plotArea = (PlotAreaRecord)rs.GetNext();
					if (rs.PeekNextChartSid() == 4146)
					{
						frame = new FrameAggregate(rs, this);
					}
				}
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			if (ivaxis != null)
			{
				ivaxis.VisitContainedRecords(rv);
			}
			if (dvaxis != null)
			{
				dvaxis.VisitContainedRecords(rv);
			}
			dvaxisSecond.VisitContainedRecords(rv);
			if (seriesAxis != null)
			{
				seriesAxis.VisitContainedRecords(rv);
			}
			foreach (AttachedLabelAggregate attachedLabel in attachedLabelList)
			{
				attachedLabel.VisitContainedRecords(rv);
			}
			if (plotArea != null)
			{
				rv.VisitRecord(plotArea);
				if (frame != null)
				{
					frame.VisitContainedRecords(rv);
				}
			}
		}
	}
}
