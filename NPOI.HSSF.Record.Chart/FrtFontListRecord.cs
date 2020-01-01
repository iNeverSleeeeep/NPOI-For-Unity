using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The FrtFontList record specifies font information used on the chart and specifies the 
	/// beginning of a collection of Font records as defined by the Chart Sheet Substream ABNF.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class FrtFontListRecord : RowDataRecord
	{
		public const short sid = 2138;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2138;

		public FrtFontListRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
