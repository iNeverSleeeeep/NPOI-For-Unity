using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.AutoFilter
{
	public class AutoFilterInfoRecord : StandardRecord
	{
		public const short sid = 157;

		private short field_1_cEntries;

		public override short Sid => 157;

		protected override int DataSize => 2;

		public short NumEntries
		{
			get
			{
				return field_1_cEntries;
			}
			set
			{
				field_1_cEntries = value;
			}
		}

		public AutoFilterInfoRecord()
		{
		}

		public AutoFilterInfoRecord(RecordInputStream in1)
		{
			field_1_cEntries = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AUTOFILTERINFO]\n");
			stringBuilder.Append("    .numEntries          = ").Append(field_1_cEntries).Append("\n");
			stringBuilder.Append("[/AUTOFILTERINFO]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_cEntries);
		}

		public override object Clone()
		{
			AutoFilterInfoRecord autoFilterInfoRecord = new AutoFilterInfoRecord();
			autoFilterInfoRecord.field_1_cEntries = field_1_cEntries;
			return autoFilterInfoRecord;
		}
	}
}
