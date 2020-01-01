using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// CRTMLFRT = CrtMlFrt *CrtMlFrtContinue
	/// </summary>
	public class CrtMlFrtAggregate : ChartRecordAggregate
	{
		private CrtMlFrtRecord crtmlFrt;

		private List<CrtMlFrtContinueRecord> continues = new List<CrtMlFrtContinueRecord>();

		public CrtMlFrtAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("CRTMLFRT", container)
		{
			crtmlFrt = (CrtMlFrtRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 2207)
			{
				while (rs.PeekNextChartSid() == 2207)
				{
					continues.Add((CrtMlFrtContinueRecord)rs.GetNext());
				}
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			WriteStartBlock(rv);
			rv.VisitRecord(crtmlFrt);
			foreach (CrtMlFrtContinueRecord @continue in continues)
			{
				rv.VisitRecord(@continue);
			}
		}
	}
}
