using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// ATTACHEDLABEL = Text Begin Pos [FontX] [AlRuns] AI [FRAME] [ObjectLink] [DataLabExtContents] [CrtLayout12] [TEXTPROPS] [CRTMLFRT] End
	/// AI = BRAI [SeriesText]
	/// </summary>
	public class AttachedLabelAggregate : ChartRecordAggregate
	{
		private TextRecord text;

		private PosRecord pos;

		private FontXRecord fontX;

		private AlRunsRecord alRuns;

		private BRAIRecord brai;

		private SeriesTextRecord seriesText;

		private FrameAggregate frame;

		private ObjectLinkRecord objectLink;

		private DataLabExtContentsRecord dataLab;

		private CrtLayout12Record crtLayout;

		private TextPropsAggregate textProps;

		private CrtMlFrtAggregate crtMlFrt;

		private bool _isFirst;

		public bool IsFirst
		{
			get
			{
				return _isFirst;
			}
			set
			{
				_isFirst = value;
			}
		}

		public ObjectLinkRecord ObjectLink => objectLink;

		public AttachedLabelAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("ATTACHEDLABEL", container)
		{
			ChartSheetAggregate container2 = GetContainer<ChartSheetAggregate>("CHARTSHEET");
			_isFirst = (container2.AttachLabelCount == 0);
			container2.AttachLabelCount++;
			text = (TextRecord)rs.GetNext();
			rs.GetNext();
			pos = (PosRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4134)
			{
				fontX = (FontXRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4176)
			{
				alRuns = (AlRunsRecord)rs.GetNext();
			}
			brai = (BRAIRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4109)
			{
				seriesText = (SeriesTextRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4146)
			{
				frame = new FrameAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 4135)
			{
				objectLink = (ObjectLinkRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 2155)
			{
				dataLab = (DataLabExtContentsRecord)rs.GetNext();
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
			rv.VisitRecord(text);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(pos);
			if (fontX != null)
			{
				rv.VisitRecord(fontX);
			}
			if (alRuns != null)
			{
				rv.VisitRecord(alRuns);
			}
			rv.VisitRecord(brai);
			if (seriesText != null)
			{
				rv.VisitRecord(seriesText);
			}
			if (frame != null)
			{
				frame.VisitContainedRecords(rv);
			}
			if (objectLink != null)
			{
				rv.VisitRecord(objectLink);
			}
			if (dataLab != null)
			{
				rv.VisitRecord(dataLab);
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
