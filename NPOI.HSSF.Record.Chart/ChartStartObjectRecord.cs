using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// STARTOBJECT - Chart Future Record Type Start Object (0x0854)<br />
	///
	/// @author Patrick Cheng
	public class ChartStartObjectRecord : StandardRecord
	{
		public const short sid = 2132;

		private short rt;

		private short grbitFrt;

		private short iObjectKind;

		private short iObjectContext;

		private short iObjectInstance1;

		private short iObjectInstance2;

		protected override int DataSize => 12;

		public override short Sid => 2132;

		public ChartStartObjectRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			iObjectKind = in1.ReadShort();
			iObjectContext = in1.ReadShort();
			iObjectInstance1 = in1.ReadShort();
			iObjectInstance2 = in1.ReadShort();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteShort(iObjectKind);
			out1.WriteShort(iObjectContext);
			out1.WriteShort(iObjectInstance1);
			out1.WriteShort(iObjectInstance2);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[STARTOBJECT]\n");
			stringBuilder.Append("    .rt              =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt        =").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .iObjectKind     =").Append(HexDump.ShortToHex(iObjectKind)).Append('\n');
			stringBuilder.Append("    .iObjectContext  =").Append(HexDump.ShortToHex(iObjectContext)).Append('\n');
			stringBuilder.Append("    .iObjectInstance1=").Append(HexDump.ShortToHex(iObjectInstance1)).Append('\n');
			stringBuilder.Append("    .iObjectInstance2=").Append(HexDump.ShortToHex(iObjectInstance2)).Append('\n');
			stringBuilder.Append("[/STARTOBJECT]\n");
			return stringBuilder.ToString();
		}
	}
}
