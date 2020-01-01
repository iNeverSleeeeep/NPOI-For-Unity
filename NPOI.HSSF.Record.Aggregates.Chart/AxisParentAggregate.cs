using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// AXISPARENT = AxisParent Begin Pos [AXES] 1*4CRT End
	/// </summary>
	public class AxisParentAggregate : ChartRecordAggregate
	{
		private AxisParentRecord axisPraent;

		private PosRecord pos;

		private AxesAggregate axes;

		private List<CRTAggregate> crtList = new List<CRTAggregate>();

		public AxisParentRecord AxisParent => axisPraent;

		public AxisParentAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("AXISPARENT", container)
		{
			axisPraent = (AxisParentRecord)rs.GetNext();
			rs.GetNext();
			pos = (PosRecord)rs.GetNext();
			if (4116 != rs.PeekNextChartSid())
			{
				try
				{
					axes = new AxesAggregate(rs, this);
				}
				catch
				{
					axes = null;
				}
			}
			while (4116 == rs.PeekNextChartSid())
			{
				crtList.Add(new CRTAggregate(rs, this));
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(axisPraent);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(pos);
			if (axes != null)
			{
				axes.VisitContainedRecords(rv);
			}
			foreach (CRTAggregate crt in crtList)
			{
				crt.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
