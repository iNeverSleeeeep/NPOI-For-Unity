using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// TEXTPROPS = (RichTextStream / TextPropsStream) *ContinueFrt12
	/// </summary>
	public class TextPropsAggregate : ChartRecordAggregate
	{
		private TextPropsStreamRecord textPropsStream;

		private RichTextStreamRecord richTextStream;

		private List<ContinueFrt12Record> continues = new List<ContinueFrt12Record>();

		public TextPropsAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("TEXTPROPS", container)
		{
			if (rs.PeekNextChartSid() == 2213)
			{
				textPropsStream = (TextPropsStreamRecord)rs.GetNext();
			}
			else
			{
				richTextStream = (RichTextStreamRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 2175)
			{
				while (rs.PeekNextChartSid() == 2175)
				{
					continues.Add((ContinueFrt12Record)rs.GetNext());
				}
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			WriteStartBlock(rv);
			if (textPropsStream != null)
			{
				rv.VisitRecord(textPropsStream);
			}
			if (richTextStream != null)
			{
				rv.VisitRecord(richTextStream);
			}
			foreach (ContinueFrt12Record @continue in continues)
			{
				rv.VisitRecord(@continue);
			}
		}
	}
}
