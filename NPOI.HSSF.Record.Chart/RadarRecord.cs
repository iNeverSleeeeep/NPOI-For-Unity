using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The Radar record specifies that the chart group is a radar chart group and specifies the chart group attributes.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class RadarRecord : RowDataRecord
	{
		public const short sid = 4158;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4158;

		public RadarRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
