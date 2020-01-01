using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// CHARTFOMATS = Chart Begin *2FONTLIST Scl PlotGrowth [FRAME] *SERIESFORMAT *SS ShtProps 
	/// *2DFTTEXT AxesUsed 1*2AXISPARENT [CrtLayout12A] [DAT] *ATTACHEDLABEL [CRTMLFRT] 
	/// *([DataLabExt StartObject] ATTACHEDLABEL [EndObject]) [TEXTPROPS] *2CRTMLFRT End
	/// </summary>
	public class ChartFormatsAggregate : ChartRecordAggregate
	{
		private class ChartFormatsAttachedLabelAggregate : ChartRecordAggregate
		{
			private DataLabExtRecord dataLabExt;

			private ChartStartObjectRecord startObject;

			private AttachedLabelAggregate attachedLabel;

			private ChartEndObjectRecord endObject;

			public ChartFormatsAttachedLabelAggregate(RecordStream rs, ChartRecordAggregate container)
				: base("ChartFormatsAttachedLabel", container)
			{
				if (rs.PeekNextChartSid() == 2154)
				{
					dataLabExt = (DataLabExtRecord)rs.GetNext();
					startObject = (ChartStartObjectRecord)rs.GetNext();
				}
				attachedLabel = new AttachedLabelAggregate(rs, this);
				if (startObject != null)
				{
					endObject = (ChartEndObjectRecord)rs.GetNext();
				}
			}

			public override void VisitContainedRecords(RecordVisitor rv)
			{
				if (dataLabExt != null)
				{
					rv.VisitRecord(dataLabExt);
					rv.VisitRecord(startObject);
					base.IsInStartObject = true;
				}
				attachedLabel.VisitContainedRecords(rv);
				if (endObject != null)
				{
					base.IsInStartObject = false;
					rv.VisitRecord(endObject);
				}
			}
		}

		private ChartRecord chart;

		private List<FontListAggregate> fontList = new List<FontListAggregate>();

		private SCLRecord scl;

		private PlotGrowthRecord plotGrowth;

		private FrameAggregate frame;

		private List<SeriesFormatAggregate> seriesFormatList = new List<SeriesFormatAggregate>();

		private List<SSAggregate> ssList = new List<SSAggregate>();

		private ShtPropsRecord shtProps;

		private List<DFTTextAggregate> dftTextList = new List<DFTTextAggregate>();

		private AxesUsedRecord axesUsed;

		private AxisParentAggregate axisParent1;

		private AxisParentAggregate axisParent2;

		private CrtLayout12ARecord crt12A;

		private DatAggregate dat;

		private List<AttachedLabelAggregate> attachedLabelList = new List<AttachedLabelAggregate>();

		private CrtMlFrtAggregate crtMlFrt;

		private List<ChartFormatsAttachedLabelAggregate> cfalList = new List<ChartFormatsAttachedLabelAggregate>();

		private TextPropsAggregate textProps;

		private List<CrtMlFrtAggregate> mlfrtList = new List<CrtMlFrtAggregate>();

		public ChartFormatsAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("CHARTFOMATS", container)
		{
			chart = (ChartRecord)rs.GetNext();
			rs.GetNext();
			while (rs.PeekNextChartSid() == 2138)
			{
				fontList.Add(new FontListAggregate(rs, this));
			}
			scl = (SCLRecord)rs.GetNext();
			plotGrowth = (PlotGrowthRecord)rs.GetNext();
			if (rs.PeekNextChartSid() == 4146)
			{
				frame = new FrameAggregate(rs, this);
			}
			while (rs.PeekNextChartSid() == 4099)
			{
				seriesFormatList.Add(new SeriesFormatAggregate(rs, this));
			}
			while (rs.PeekNextChartSid() == 4102)
			{
				ssList.Add(new SSAggregate(rs, this));
			}
			shtProps = (ShtPropsRecord)rs.GetNext();
			while (rs.PeekNextChartSid() == 4132 || rs.PeekNextChartSid() == 2154)
			{
				dftTextList.Add(new DFTTextAggregate(rs, this));
			}
			axesUsed = (AxesUsedRecord)rs.GetNext();
			axisParent1 = new AxisParentAggregate(rs, this);
			if (rs.PeekNextChartSid() == 4161)
			{
				axisParent2 = new AxisParentAggregate(rs, this);
			}
			if (rs.PeekNextChartSid() == 2215)
			{
				crt12A = (CrtLayout12ARecord)rs.GetNext();
			}
			if (rs.PeekNextChartSid() == 4195)
			{
				dat = new DatAggregate(rs, container);
			}
			if (rs.PeekNextChartSid() == 4133)
			{
				while (rs.PeekNextChartSid() == 4133)
				{
					attachedLabelList.Add(new AttachedLabelAggregate(rs, this));
				}
			}
			if (rs.PeekNextChartSid() == 2206)
			{
				crtMlFrt = new CrtMlFrtAggregate(rs, container);
			}
			while (rs.PeekNextChartSid() == 2154 || rs.PeekNextChartSid() == 4133)
			{
				cfalList.Add(new ChartFormatsAttachedLabelAggregate(rs, this));
			}
			if (rs.PeekNextChartSid() == 2213)
			{
				textProps = new TextPropsAggregate(rs, container);
			}
			while (rs.PeekNextChartSid() == 2206)
			{
				mlfrtList.Add(new CrtMlFrtAggregate(rs, container));
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(chart);
			rv.VisitRecord(BeginRecord.instance);
			foreach (FontListAggregate font in fontList)
			{
				font.VisitContainedRecords(rv);
			}
			rv.VisitRecord(scl);
			rv.VisitRecord(plotGrowth);
			if (frame != null)
			{
				frame.VisitContainedRecords(rv);
			}
			short num = 0;
			foreach (SeriesFormatAggregate seriesFormat in seriesFormatList)
			{
				seriesFormat.VisitContainedRecords(rv);
				SeriesFormatAggregate seriesFormatAggregate = seriesFormat;
				short num2 = num;
				num = (short)(num2 + 1);
				seriesFormatAggregate.SeriesIndex = num2;
			}
			foreach (SSAggregate ss in ssList)
			{
				ss.VisitContainedRecords(rv);
			}
			rv.VisitRecord(shtProps);
			foreach (DFTTextAggregate dftText in dftTextList)
			{
				dftText.VisitContainedRecords(rv);
			}
			rv.VisitRecord(axesUsed);
			axisParent1.VisitContainedRecords(rv);
			if (axisParent2 != null)
			{
				axisParent2.VisitContainedRecords(rv);
			}
			if (crt12A != null)
			{
				rv.VisitRecord(crt12A);
			}
			if (dat != null)
			{
				dat.VisitContainedRecords(rv);
			}
			foreach (AttachedLabelAggregate attachedLabel in attachedLabelList)
			{
				attachedLabel.VisitContainedRecords(rv);
			}
			if (crtMlFrt != null)
			{
				crtMlFrt.VisitContainedRecords(rv);
			}
			foreach (ChartFormatsAttachedLabelAggregate cfal in cfalList)
			{
				cfal.VisitContainedRecords(rv);
			}
			if (textProps != null)
			{
				textProps.VisitContainedRecords(rv);
			}
			foreach (CrtMlFrtAggregate mlfrt in mlfrtList)
			{
				mlfrt.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
