using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// DAT = Dat Begin LD End
	/// </summary>
	public class DatAggregate : ChartRecordAggregate
	{
		private DatRecord dat;

		private LDAggregate ld;

		public DatAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("DAT", container)
		{
			dat = (DatRecord)rs.GetNext();
			rs.GetNext();
			ld = new LDAggregate(rs, this);
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(dat);
			rv.VisitRecord(BeginRecord.instance);
			ld.VisitContainedRecords(rv);
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
