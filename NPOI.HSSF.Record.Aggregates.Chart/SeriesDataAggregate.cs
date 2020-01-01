using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// SERIESDATA = Dimensions 3(SIIndex *(Number / BoolErr / Blank / Label))
	/// </summary>
	public class SeriesDataAggregate : RecordAggregate
	{
		private DimensionsRecord dimensions;

		private Dictionary<SeriesIndexRecord, List<Record>> dicData = new Dictionary<SeriesIndexRecord, List<Record>>();

		public SeriesDataAggregate(RecordStream rs)
		{
			dimensions = (DimensionsRecord)rs.GetNext();
			while (rs.PeekNextChartSid() == 4197)
			{
				SeriesIndexRecord key = (SeriesIndexRecord)rs.GetNext();
				int num = rs.PeekNextChartSid();
				List<Record> list = new List<Record>();
				while (num == 515 || num == 517 || num == 513 || num == 516)
				{
					list.Add(rs.GetNext());
				}
				dicData.Add(key, list);
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(dimensions);
			foreach (KeyValuePair<SeriesIndexRecord, List<Record>> dicDatum in dicData)
			{
				rv.VisitRecord(dicDatum.Key);
				foreach (Record item in dicDatum.Value)
				{
					rv.VisitRecord(item);
				}
			}
		}
	}
}
