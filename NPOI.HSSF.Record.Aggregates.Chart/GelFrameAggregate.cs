using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// GELFRAME = 1*2GelFrame *Continue [PICF]
	/// PICF = Begin PicF End
	/// </summary>
	public class GelFrameAggregate : ChartRecordAggregate
	{
		private GelFrameRecord gelFrame1;

		private GelFrameRecord gelFrame2;

		private List<ContinueRecord> continues = new List<ContinueRecord>();

		private PicFRecord picF;

		public GelFrameAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("GELFRAME", container)
		{
			gelFrame1 = (GelFrameRecord)rs.GetNext();
			int num = rs.PeekNextChartSid();
			if (num == 4198)
			{
				gelFrame2 = (GelFrameRecord)rs.GetNext();
				num = rs.PeekNextChartSid();
			}
			if (num == 60)
			{
				while (rs.PeekNextChartSid() == 60)
				{
					continues.Add((ContinueRecord)rs.GetNext());
				}
			}
			if (rs.PeekNextChartSid() == 4147)
			{
				rs.GetNext();
				picF = (PicFRecord)rs.GetNext();
				rs.GetNext();
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(gelFrame1);
			if (gelFrame2 != null)
			{
				rv.VisitRecord(gelFrame2);
			}
			foreach (ContinueRecord @continue in continues)
			{
				rv.VisitRecord(@continue);
			}
			if (picF != null)
			{
				rv.VisitRecord(BeginRecord.instance);
				rv.VisitRecord(picF);
				rv.VisitRecord(EndRecord.instance);
			}
		}
	}
}
