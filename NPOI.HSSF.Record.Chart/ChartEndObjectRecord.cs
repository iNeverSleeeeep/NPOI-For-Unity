using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// ENDOBJECT - Chart Future Record Type End Object (0x0855)<br />
	///
	/// @author Patrick Cheng
	public class ChartEndObjectRecord : StandardRecord
	{
		public const short sid = 2133;

		private short rt;

		private short grbitFrt;

		private short iObjectKind;

		private byte[] reserved;

		protected override int DataSize => 12;

		public override short Sid => 2133;

		public ChartEndObjectRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			iObjectKind = in1.ReadShort();
			reserved = new byte[6];
			if (in1.Available() != 0)
			{
				in1.ReadFully(reserved);
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteShort(iObjectKind);
			out1.Write(reserved);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ENDOBJECT]\n");
			stringBuilder.Append("    .rt         =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt   =").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .iObjectKind=").Append(HexDump.ShortToHex(iObjectKind)).Append('\n');
			stringBuilder.Append("    .unused     =").Append(HexDump.ToHex(reserved)).Append('\n');
			stringBuilder.Append("[/ENDOBJECT]\n");
			return stringBuilder.ToString();
		}
	}
}
