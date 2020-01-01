using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The SerParent record specifies the series to which the current trendline or error bar corresponds.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class SerParentRecord : RowDataRecord
	{
		public const short sid = 4170;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4170;

		public SerParentRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
