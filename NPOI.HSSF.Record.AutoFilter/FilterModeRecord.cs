using NPOI.Util;

namespace NPOI.HSSF.Record.AutoFilter
{
	public class FilterModeRecord : StandardRecord
	{
		public const short sid = 155;

		public override short Sid => 155;

		protected override int DataSize => 0;

		public FilterModeRecord()
		{
		}

		public FilterModeRecord(RecordInputStream in1)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		public override object Clone()
		{
			return new FilterModeRecord();
		}
	}
}
