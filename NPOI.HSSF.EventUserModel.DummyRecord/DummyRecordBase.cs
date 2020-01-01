using NPOI.HSSF.Record;
using NPOI.Util;

namespace NPOI.HSSF.EventUserModel.DummyRecord
{
	public abstract class DummyRecordBase : NPOI.HSSF.Record.Record
	{
		public override short Sid => -1;

		public override int RecordSize
		{
			get
			{
				throw new RecordFormatException("Cannot serialize a dummy record");
			}
		}

		public override int Serialize(int offset, byte[] data)
		{
			throw new RecordFormatException("Cannot serialize a dummy record");
		}
	}
}
