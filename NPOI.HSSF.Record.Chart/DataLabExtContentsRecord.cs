using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The DataLabExtContents record specifies the contents of an extended data label.
	/// </summary>
	public class DataLabExtContentsRecord : RowDataRecord
	{
		public const short sid = 2155;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2155;

		public DataLabExtContentsRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
