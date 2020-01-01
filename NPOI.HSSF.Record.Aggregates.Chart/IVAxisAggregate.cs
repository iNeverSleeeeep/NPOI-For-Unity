using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// IVAXIS = Axis Begin [CatSerRange] AxcExt [CatLab] AXS [CRTMLFRT] End
	/// </summary>
	public class IVAxisAggregate : ChartRecordAggregate
	{
		private AxisRecord axis;

		private CatSerRangeRecord catSerRange;

		private AxcExtRecord axcExt;

		private CatLabRecord catLab;

		private AXSAggregate axs;

		private List<CrtMlFrtAggregate> crtmlfrtList = new List<CrtMlFrtAggregate>();

		public IVAxisAggregate(RecordStream rs, ChartRecordAggregate container, AxisRecord axis)
			: base("IVAXIS", container)
		{
			this.axis = axis;
			if (rs.PeekNextChartSid() == 4128)
			{
				catSerRange = (CatSerRangeRecord)rs.GetNext();
			}
			axcExt = (AxcExtRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 2134)
			{
				catLab = (CatLabRecord)rs.GetNext();
			}
			axs = new AXSAggregate(rs, this);
			while (rs.PeekNextChartSid() == 2206)
			{
				crtmlfrtList.Add(new CrtMlFrtAggregate(rs, this));
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(axis);
			rv.VisitRecord(BeginRecord.instance);
			if (catSerRange != null)
			{
				rv.VisitRecord(catSerRange);
			}
			rv.VisitRecord(axcExt);
			if (catLab != null)
			{
				WriteStartBlock(rv);
				rv.VisitRecord(catLab);
			}
			axs.VisitContainedRecords(rv);
			foreach (CrtMlFrtAggregate crtmlfrt in crtmlfrtList)
			{
				crtmlfrt.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
