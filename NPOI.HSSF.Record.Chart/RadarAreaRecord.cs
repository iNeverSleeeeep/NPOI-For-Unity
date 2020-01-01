using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The RadarArea record specifies that the chart group is a filled radar chart group and specifies the chart group attributes.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class RadarAreaRecord : RowDataRecord
	{
		public const short sid = 4160;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4160;

		public RadarAreaRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
