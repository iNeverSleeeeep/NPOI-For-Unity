using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// FRAME = Frame Begin LineFormat AreaFormat [GELFRAME] [SHAPEPROPS] End
	/// </summary>
	public class FrameAggregate : ChartRecordAggregate
	{
		private FrameRecord frame;

		private LineFormatRecord lineFormat;

		private AreaFormatRecord areaFormat;

		private GelFrameAggregate gelFrame;

		private ShapePropsAggregate shapeProps;

		public FrameAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("FRAME", container)
		{
			frame = (FrameRecord)rs.GetNext();
			rs.GetNext();
			lineFormat = (LineFormatRecord)rs.GetNext();
			areaFormat = (AreaFormatRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4198)
			{
				gelFrame = new GelFrameAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2212)
			{
				shapeProps = new ShapePropsAggregate(rs, this);
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(frame);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(lineFormat);
			rv.VisitRecord(areaFormat);
			if (gelFrame != null)
			{
				gelFrame.VisitContainedRecords(rv);
			}
			if (shapeProps != null)
			{
				shapeProps.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}

		protected override bool ShoudWriteStartBlock()
		{
			if (base.IsInStartObject)
			{
				return false;
			}
			return false;
		}
	}
}
