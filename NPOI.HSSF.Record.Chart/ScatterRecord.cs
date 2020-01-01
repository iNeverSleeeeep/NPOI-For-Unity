using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The Scatter record specifies that the chart group is a scatter chart group or 
	/// a bubble chart group, and specifies the chart group attributes.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class ScatterRecord : RowDataRecord
	{
		public const short sid = 4123;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4123;

		public ScatterRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
