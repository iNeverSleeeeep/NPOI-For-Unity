using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// DFTTEXT = [DataLabExt StartObject] DefaultText ATTACHEDLABEL [EndObject]
	/// </summary>
	public class DFTTextAggregate : ChartRecordAggregate
	{
		private DataLabExtRecord dataLabExt;

		private ChartStartObjectRecord startObject;

		private DefaultTextRecord defaultText;

		private AttachedLabelAggregate attachedLabel;

		private ChartEndObjectRecord endObject;

		public DefaultTextRecord DefaultText => defaultText;

		public DFTTextAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("DFTTEXT", container)
		{
			if (rs.PeekNextChartSid() == 2154)
			{
				dataLabExt = (DataLabExtRecord)rs.GetNext();
				startObject = (ChartStartObjectRecord)rs.GetNext();
			}
			defaultText = (DefaultTextRecord)rs.GetNext();
			attachedLabel = new AttachedLabelAggregate(rs, this);
			if (startObject != null)
			{
				endObject = (ChartEndObjectRecord)rs.GetNext();
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			if (dataLabExt != null)
			{
				rv.VisitRecord(dataLabExt);
				rv.VisitRecord(startObject);
				base.IsInStartObject = true;
			}
			rv.VisitRecord(defaultText);
			attachedLabel.VisitContainedRecords(rv);
			if (endObject != null)
			{
				rv.VisitRecord(endObject);
				base.IsInStartObject = false;
			}
		}
	}
}
