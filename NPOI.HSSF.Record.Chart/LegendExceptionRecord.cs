using NPOI.Util;
using System;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The LegendException record specifies information about a legend entry which was 
	/// changed from the default legend entry settings, and specifies the beginning of 
	/// a collection of records as defined by the Chart Sheet Substream ABNF. 
	/// The collection of records specifies legend entry formatting. On a chart where 
	/// the legend contains legend entries for the series and trendlines, as defined 
	/// in the legend overview, there MUST be zero instances or one instance of this 
	/// record in the sequence of records that conform to the SERIESFORMAT rule.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class LegendExceptionRecord : RowDataRecord
	{
		public const short sid = 4163;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4163;

		public short LegendEntry
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public LegendExceptionRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
