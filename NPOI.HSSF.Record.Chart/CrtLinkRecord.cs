using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The CrtLink record is written but unused.
	/// </summary>
	public class CrtLinkRecord : StandardRecord
	{
		public const short sid = 4130;

		protected override int DataSize => 10;

		public override short Sid => 4130;

		public CrtLinkRecord()
		{
		}

		public CrtLinkRecord(RecordInputStream in1)
		{
			in1.ReadInt();
			in1.ReadInt();
			in1.ReadShort();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(0);
			out1.WriteInt(0);
			out1.WriteShort(0);
		}

		public override object Clone()
		{
			return new CrtLinkRecord();
		}

		public override string ToString()
		{
			return "[CrtLink]Unused[/CrtLink]";
		}
	}
}
