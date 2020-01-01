using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// AXM = YMult StartObject ATTACHEDLABEL EndObject
	/// </summary>
	public class AXMAggregate : ChartRecordAggregate
	{
		private YMultRecord yMult;

		private ChartStartObjectRecord startObject;

		private AttachedLabelAggregate attachedLabel;

		private ChartEndObjectRecord endObject;

		public AXMAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("AXM", container)
		{
			yMult = (YMultRecord)rs.GetNext();
			startObject = (ChartStartObjectRecord)rs.GetNext();
			attachedLabel = new AttachedLabelAggregate(rs, this);
			endObject = (ChartEndObjectRecord)rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			WriteStartBlock(rv);
			rv.VisitRecord(yMult);
			rv.VisitRecord(startObject);
			base.IsInStartObject = true;
			attachedLabel.VisitContainedRecords(rv);
			base.IsInStartObject = false;
			rv.VisitRecord(endObject);
		}
	}
}
