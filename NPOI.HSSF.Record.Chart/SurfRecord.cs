using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The Surf record specifies that the chart group is a surface chart group and specifies the chart group attributes.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class SurfRecord : RowDataRecord
	{
		public const short sid = 4159;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4159;

		public SurfRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
