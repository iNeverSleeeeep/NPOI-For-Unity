using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// LD = Legend Begin Pos ATTACHEDLABEL [FRAME] [CrtLayout12] [TEXTPROPS] [CRTMLFRT] End
	/// </summary>
	public class LDAggregate : ChartRecordAggregate
	{
		private LegendRecord legend;

		private PosRecord pos;

		private AttachedLabelAggregate attachedLabel;

		private FrameAggregate frame;

		private CrtLayout12Record crtLayout;

		private TextPropsAggregate textProps;

		private CrtMlFrtAggregate crtMlFrt;

		public LDAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("LD", container)
		{
			legend = (LegendRecord)rs.GetNext();
			rs.GetNext();
			pos = (PosRecord)rs.GetNext();
			attachedLabel = new AttachedLabelAggregate(rs, this);
			if (rs.PeekNextChartSid() == 4146)
			{
				frame = new FrameAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2205)
			{
				crtLayout = (CrtLayout12Record)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 2213)
			{
				textProps = new TextPropsAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2206)
			{
				crtMlFrt = new CrtMlFrtAggregate(rs, this);
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(legend);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(pos);
			attachedLabel.VisitContainedRecords(rv);
			if (frame != null)
			{
				frame.VisitContainedRecords(rv);
			}
			if (crtLayout != null)
			{
				rv.VisitRecord(crtLayout);
			}
			if (textProps != null)
			{
				textProps.VisitContainedRecords(rv);
			}
			if (crtMlFrt != null)
			{
				crtMlFrt.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
