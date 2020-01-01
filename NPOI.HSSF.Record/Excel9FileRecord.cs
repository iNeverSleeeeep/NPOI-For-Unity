using NPOI.Util;

namespace NPOI.HSSF.Record
{
	public class Excel9FileRecord : StandardRecord
	{
		public const short sid = 448;

		public override short Sid => 448;

		protected override int DataSize => 0;

		public Excel9FileRecord()
		{
		}

		public Excel9FileRecord(RecordInputStream in1)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}
	}
}
