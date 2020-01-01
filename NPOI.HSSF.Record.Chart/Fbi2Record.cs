using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The Fbi2 record specifies the font information at the time the scalable font is added to the chart.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class Fbi2Record : RowDataRecord
	{
		public const short sid = 4200;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4200;

		public Fbi2Record(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
