using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// DVAXIS = Axis Begin [ValueRange] [AXM] AXS [CRTMLFRT] End
	/// </summary>
	public class DVAxisAggregate : ChartRecordAggregate
	{
		private AxisRecord axis;

		private ValueRangeRecord valueRange;

		private AXMAggregate axm;

		private AXSAggregate axs;

		private CrtMlFrtAggregate crtmlfrt;

		public AxisRecord Axis => axis;

		public DVAxisAggregate(RecordStream rs, ChartRecordAggregate container, AxisRecord axis)
			: base("DVAXIS", container)
		{
			if (axis == null)
			{
				this.axis = (AxisRecord)rs.GetNext();
				rs.GetNext();
			}
			else
			{
				this.axis = axis;
			}
			if (rs.PeekNextChartSid() == 4127)
			{
				valueRange = (ValueRangeRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 2135)
			{
				axm = new AXMAggregate(rs, this);
			}
			axs = new AXSAggregate(rs, this);
			if (rs.PeekNextChartSid() == 2206)
			{
				crtmlfrt = new CrtMlFrtAggregate(rs, this);
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(axis);
			rv.VisitRecord(BeginRecord.instance);
			if (valueRange != null)
			{
				rv.VisitRecord(valueRange);
			}
			if (axm != null)
			{
				axm.VisitContainedRecords(rv);
			}
			axs.VisitContainedRecords(rv);
			if (crtmlfrt != null)
			{
				crtmlfrt.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
