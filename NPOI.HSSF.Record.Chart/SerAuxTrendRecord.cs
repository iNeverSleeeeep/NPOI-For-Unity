using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The SerAuxTrend record specifies a trendline.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class SerAuxTrendRecord : RowDataRecord
	{
		public const short sid = 4171;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4171;

		public SerAuxTrendRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
