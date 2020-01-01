using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The BopPop record specifies that the chart group is a bar of pie chart group or 
	/// a pie of pie chart group and specifies the chart group attributes.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class BopPopRecord : RowDataRecord
	{
		public const short sid = 4193;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4193;

		public BopPopRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
