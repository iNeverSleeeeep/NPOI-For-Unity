using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// SS = DataFormat Begin [Chart3DBarShape] [LineFormat AreaFormat PieFormat] [SerFmt] 
	/// [GELFRAME] [MarkerFormat] [AttachedLabel] *2SHAPEPROPS [CRTMLFRT] End
	/// </summary>
	public class SSAggregate : ChartRecordAggregate
	{
		private DataFormatRecord dataFormat;

		private Chart3DBarShapeRecord chart3DBarShape;

		private LineFormatRecord lineFormat;

		private AreaFormatRecord areaFormat;

		private PieFormatRecord pieFormat;

		private SerFmtRecord serFmt;

		private GelFrameAggregate gelFrame;

		private MarkerFormatRecord markerFormat;

		private AttachedLabelRecord attachedLabel;

		private ShapePropsAggregate shapeProps1;

		private ShapePropsAggregate shapeProps2;

		private CrtMlFrtAggregate crtMlFrt;

		public DataFormatRecord DataFormat => dataFormat;

		public SSAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("SS", container)
		{
			dataFormat = (DataFormatRecord)rs.GetNext();
			rs.GetNext();
			if (rs.PeekNextChartSid() == 4191)
			{
				chart3DBarShape = (Chart3DBarShapeRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4103)
			{
				lineFormat = (LineFormatRecord)rs.GetNext();
				areaFormat = (AreaFormatRecord)rs.GetNext();
				pieFormat = (PieFormatRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4189)
			{
				serFmt = (SerFmtRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4198)
			{
				gelFrame = new GelFrameAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 4105)
			{
				markerFormat = (MarkerFormatRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4108)
			{
				attachedLabel = (AttachedLabelRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 2212)
			{
				shapeProps1 = new ShapePropsAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2212)
			{
				shapeProps2 = new ShapePropsAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2206)
			{
				crtMlFrt = new CrtMlFrtAggregate(rs, this);
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(dataFormat);
			rv.VisitRecord(BeginRecord.instance);
			if (chart3DBarShape != null)
			{
				rv.VisitRecord(chart3DBarShape);
			}
			if (lineFormat != null)
			{
				rv.VisitRecord(lineFormat);
				rv.VisitRecord(areaFormat);
				rv.VisitRecord(pieFormat);
			}
			if (serFmt != null)
			{
				rv.VisitRecord(serFmt);
			}
			if (gelFrame != null)
			{
				gelFrame.VisitContainedRecords(rv);
			}
			if (markerFormat != null)
			{
				rv.VisitRecord(markerFormat);
			}
			if (attachedLabel != null)
			{
				rv.VisitRecord(attachedLabel);
			}
			if (shapeProps1 != null)
			{
				shapeProps1.VisitContainedRecords(rv);
			}
			if (shapeProps2 != null)
			{
				shapeProps2.VisitContainedRecords(rv);
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
