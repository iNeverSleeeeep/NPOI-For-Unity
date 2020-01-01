using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// SHAPEPROPS = ShapePropsStream *ContinueFrt12
	/// </summary>
	public class ShapePropsAggregate : ChartRecordAggregate
	{
		private ShapePropsStreamRecord shapProps;

		private List<ContinueFrt12Record> continues = new List<ContinueFrt12Record>();

		public ShapePropsAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("SHAPEPROPS", container)
		{
			shapProps = (ShapePropsStreamRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 2207)
			{
				while (rs.PeekNextChartSid() == 2207)
				{
					continues.Add((ContinueFrt12Record)rs.GetNext());
				}
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			WriteStartBlock(rv);
			rv.VisitRecord(shapProps);
			foreach (ContinueFrt12Record @continue in continues)
			{
				rv.VisitRecord(@continue);
			}
		}
	}
}
