using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// SERIESFORMAT = Series Begin 4AI *SS (SerToCrt / (SerParent (SerAuxTrend / SerAuxErrBar)))
	/// *(LegendException [Begin ATTACHEDLABEL [TEXTPROPS] End]) End
	/// </summary>
	public class SeriesFormatAggregate : ChartRecordAggregate
	{
		/// <summary>
		/// LegendException [Begin ATTACHEDLABEL [TEXTPROPS] End]
		/// </summary>
		public class LegendExceptionAggregate : ChartRecordAggregate
		{
			private LegendExceptionRecord legendException;

			private AttachedLabelAggregate attachedLabel;

			private TextPropsAggregate textProps;

			public LegendExceptionRecord LegendException => legendException;

			public LegendExceptionAggregate(RecordStream rs, ChartRecordAggregate container)
				: base("LEGENDEXCEPTION", container)
			{
				legendException = (LegendExceptionRecord)rs.GetNext();
				if (rs.PeekNextChartSid() == 4147)
				{
					rs.GetNext();
					attachedLabel = new AttachedLabelAggregate(rs, this);
					if (rs.PeekNextChartSid() == 2213 || rs.PeekNextChartSid() == 2214)
					{
						textProps = new TextPropsAggregate(rs, this);
					}
					rs.GetNext();
				}
			}

			public override void VisitContainedRecords(RecordVisitor rv)
			{
				rv.VisitRecord(legendException);
				if (attachedLabel != null)
				{
					rv.VisitRecord(BeginRecord.instance);
					attachedLabel.VisitContainedRecords(rv);
					if (textProps != null)
					{
						textProps.VisitContainedRecords(rv);
					}
					WriteEndBlock(rv);
					rv.VisitRecord(EndRecord.instance);
				}
			}
		}

		private SeriesRecord series;

		private Dictionary<BRAIRecord, SeriesTextRecord> dic4AI = new Dictionary<BRAIRecord, SeriesTextRecord>();

		private List<SSAggregate> ssList = new List<SSAggregate>();

		private SerToCrtRecord serToCrt;

		private SerParentRecord serParent;

		private SerAuxTrendRecord serAuxTrend;

		private SerAuxErrBarRecord serAuxErrBar;

		private List<LegendExceptionAggregate> leList = new List<LegendExceptionAggregate>();

		private short _seriesIndex;

		public short SeriesIndex
		{
			get
			{
				return _seriesIndex;
			}
			set
			{
				_seriesIndex = value;
			}
		}

		public SeriesFormatAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("SERIESFORMAT", container)
		{
			series = (SeriesRecord)rs.GetNext();
			rs.GetNext();
			while (rs.PeekNextChartSid() == 4177)
			{
				SeriesTextRecord value = null;
				BRAIRecord key = (BRAIRecord)rs.GetNext();
				if (rs.PeekNextChartSid() == 4109)
				{
					value = (SeriesTextRecord)rs.GetNext();
				}
				dic4AI.Add(key, value);
			}
			if (rs.PeekNextChartSid() == 4102)
			{
				while (rs.PeekNextChartSid() == 4102)
				{
					ssList.Add(new SSAggregate(rs, this));
				}
			}
			if (rs.PeekNextChartSid() == 4165)
			{
				serToCrt = (SerToCrtRecord)rs.GetNext();
			}
			else if (rs.PeekNextChartSid() == 4170)
			{
				serParent = (SerParentRecord)rs.GetNext();
				if (rs.PeekNextChartSid() == 4171)
				{
					serAuxTrend = (SerAuxTrendRecord)rs.GetNext();
				}
				else
				{
					serAuxErrBar = (SerAuxErrBarRecord)rs.GetNext();
				}
			}
			if (rs.PeekNextChartSid() == 4163)
			{
				while (rs.PeekNextChartSid() == 4163)
				{
					leList.Add(new LegendExceptionAggregate(rs, this));
				}
			}
			rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(series);
			rv.VisitRecord(BeginRecord.instance);
			foreach (KeyValuePair<BRAIRecord, SeriesTextRecord> item in dic4AI)
			{
				rv.VisitRecord(item.Key);
				if (item.Value != null)
				{
					rv.VisitRecord(item.Value);
				}
			}
			foreach (SSAggregate ss in ssList)
			{
				ss.VisitContainedRecords(rv);
			}
			if (serToCrt != null)
			{
				rv.VisitRecord(serToCrt);
			}
			else
			{
				if (serParent != null)
				{
					rv.VisitRecord(serParent);
				}
				if (serAuxTrend != null)
				{
					rv.VisitRecord(serAuxTrend);
				}
				else
				{
					rv.VisitRecord(serAuxErrBar);
				}
			}
			foreach (LegendExceptionAggregate le in leList)
			{
				le.VisitContainedRecords(rv);
			}
			WriteEndBlock(rv);
			rv.VisitRecord(EndRecord.instance);
		}
	}
}
