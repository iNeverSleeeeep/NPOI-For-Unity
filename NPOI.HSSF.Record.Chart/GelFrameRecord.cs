using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// specifies the properties of a fill pattern for parts of a chart.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class GelFrameRecord : RowDataRecord
	{
		public const short sid = 4198;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4198;

		public GelFrameRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
