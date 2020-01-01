using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// DROPBAR = DropBar Begin LineFormat AreaFormat [GELFRAME] [SHAPEPROPS] End
	/// </summary>
	public class DropBarAggregate : ChartRecordAggregate
	{
		private DropBarRecord dropBar;

		private LineFormatRecord lineFormat;

		private AreaFormatRecord areaFormat;

		private GelFrameAggregate gelFrame;

		private ShapePropsAggregate shapProps;

		public DropBarAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("DROPBAR", container)
		{
			dropBar = (DropBarRecord)rs.GetNext();
			rs.GetNext();
			lineFormat = (LineFormatRecord)rs.GetNext();
			areaFormat = (AreaFormatRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4198)
			{
				gelFrame = new GelFrameAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2212)
			{
				shapProps = new ShapePropsAggregate(rs, this);
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(dropBar);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(lineFormat);
			rv.VisitRecord(areaFormat);
			if (gelFrame != null)
			{
				gelFrame.VisitContainedRecords(rv);
			}
			if (shapProps != null)
			{
				shapProps.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
