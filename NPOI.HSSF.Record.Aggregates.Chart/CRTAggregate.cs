using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// CRT = ChartFormat Begin (Bar / Line / (BopPop [BopPopCustom]) / Pie / Area / Scatter / Radar / 
	/// RadarArea / Surf) CrtLink [SeriesList] [Chart3d] [LD] [2DROPBAR] *4(CrtLine LineFormat) 
	/// *2DFTTEXT [DataLabExtContents] [SS] *4SHAPEPROPS End
	/// </summary>
	public class CRTAggregate : ChartRecordAggregate
	{
		private ChartFormatRecord chartForamt;

		private Record chartTypeRecord;

		private BopPopCustomRecord bopPopCustom;

		private CrtLinkRecord crtLink;

		private SeriesListRecord seriesList;

		private Chart3dRecord chart3d;

		private LDAggregate ld;

		private DropBarAggregate dropBar1;

		private DropBarAggregate dropBar2;

		private Dictionary<CrtLineRecord, LineFormatRecord> dicLines = new Dictionary<CrtLineRecord, LineFormatRecord>();

		private DFTTextAggregate dft1;

		private DFTTextAggregate dft2;

		private DataLabExtContentsRecord dataLabExtContents;

		private SSAggregate ss;

		private List<ShapePropsAggregate> shapeList = new List<ShapePropsAggregate>();

		public CRTAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("CRT", container)
		{
			chartForamt = (ChartFormatRecord)rs.GetNext();
			rs.GetNext();
			chartTypeRecord = rs.GetNext();
			if (rs.PeekNextChartSid() == 4199)
			{
				bopPopCustom = (BopPopCustomRecord)rs.GetNext();
			}
			crtLink = (CrtLinkRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4118)
			{
				seriesList = (SeriesListRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4154)
			{
				chart3d = (Chart3dRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4117)
			{
				ld = new LDAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 4157)
			{
				dropBar1 = new DropBarAggregate(rs, this);
				dropBar2 = new DropBarAggregate(rs, this);
			}
			while (rs.PeekNextChartSid() == 4124)
			{
				dicLines.Add((CrtLineRecord)rs.GetNext(), (LineFormatRecord)rs.GetNext());
			}
			if (rs.PeekNextChartSid() == 2154 || rs.PeekNextChartSid() == 4132)
			{
				dft1 = new DFTTextAggregate(rs, this);
				if (rs.PeekNextChartSid() == 2154 || rs.PeekNextChartSid() == 4132)
				{
					dft2 = new DFTTextAggregate(rs, this);
				}
			}
			if (rs.PeekNextChartSid() == 2155)
			{
				dataLabExtContents = (DataLabExtContentsRecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4102)
			{
				ss = new SSAggregate(rs, this);
			}
			while (rs.PeekNextChartSid() == 2212)
			{
				shapeList.Add(new ShapePropsAggregate(rs, this));
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(chartForamt);
			rv.VisitRecord(BeginRecord.instance);
			rv.VisitRecord(chartTypeRecord);
			if (bopPopCustom != null)
			{
				rv.VisitRecord(bopPopCustom);
			}
			rv.VisitRecord(crtLink);
			if (seriesList != null)
			{
				rv.VisitRecord(crtLink);
			}
			if (chart3d != null)
			{
				rv.VisitRecord(chart3d);
			}
			if (ld != null)
			{
				ld.VisitContainedRecords(rv);
			}
			if (dropBar1 != null)
			{
				dropBar1.VisitContainedRecords(rv);
				dropBar2.VisitContainedRecords(rv);
			}
			foreach (KeyValuePair<CrtLineRecord, LineFormatRecord> dicLine in dicLines)
			{
				rv.VisitRecord(dicLine.Key);
				rv.VisitRecord(dicLine.Value);
			}
			if (dft1 != null)
			{
				dft1.VisitContainedRecords(rv);
				if (dft2 != null)
				{
					dft2.VisitContainedRecords(rv);
				}
			}
			if (dataLabExtContents != null)
			{
				rv.VisitRecord(dataLabExtContents);
			}
			if (ss != null)
			{
				ss.VisitContainedRecords(rv);
			}
			foreach (ShapePropsAggregate shape in shapeList)
			{
				shape.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
