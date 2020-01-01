using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The CrtLine record specifies the presence of drop lines, high-low lines, series lines
	/// or leader lines on the chart group. This record is followed by a LineFormat record 
	/// which specifies the format of the lines.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class CrtLineRecord : RowDataRecord
	{
		public const short sid = 4124;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4124;

		public CrtLineRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
