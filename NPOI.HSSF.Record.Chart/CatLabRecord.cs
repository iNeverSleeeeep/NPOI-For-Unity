using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// CATLAB - Category Labels (0x0856)<br />
	///
	/// @author Patrick Cheng
	public class CatLabRecord : StandardRecord
	{
		public const short sid = 2134;

		private short rt;

		private short grbitFrt;

		private short wOffset;

		private short at;

		private short grbit;

		private short? unused;

		protected override int DataSize => 10 + (((int?)unused).HasValue ? 2 : 0);

		public override short Sid => 2134;

		public CatLabRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			wOffset = in1.ReadShort();
			at = in1.ReadShort();
			grbit = in1.ReadShort();
			if (in1.Available() == 0)
			{
				unused = null;
			}
			else
			{
				unused = in1.ReadShort();
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteShort(wOffset);
			out1.WriteShort(at);
			out1.WriteShort(grbit);
			if (((int?)unused).HasValue)
			{
				out1.WriteShort(unused.Value);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CATLAB]\n");
			stringBuilder.Append("    .rt      =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt=").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .wOffset =").Append(HexDump.ShortToHex(wOffset)).Append('\n');
			stringBuilder.Append("    .at      =").Append(HexDump.ShortToHex(at)).Append('\n');
			stringBuilder.Append("    .grbit   =").Append(HexDump.ShortToHex(grbit)).Append('\n');
			stringBuilder.Append("    .unused  =").Append(HexDump.ShortToHex(unused.Value)).Append('\n');
			stringBuilder.Append("[/CATLAB]\n");
			return stringBuilder.ToString();
		}
	}
}
