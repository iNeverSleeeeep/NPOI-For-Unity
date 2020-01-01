using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// DATALABEXT - Chart Data Label Extension (0x086A) <br />
	///
	/// @author Patrick Cheng
	public class DataLabelExtensionRecord : StandardRecord
	{
		public static short sid = 2154;

		private int rt;

		private int grbitFrt;

		private byte[] unused = new byte[8];

		protected override int DataSize => 12;

		public override short Sid => sid;

		public DataLabelExtensionRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			in1.ReadFully(unused);
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.Write(unused);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DATALABEXT]\n");
			stringBuilder.Append("    .rt      =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt=").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .unused  =").Append(HexDump.ToHex(unused)).Append('\n');
			stringBuilder.Append("[/DATALABEXT]\n");
			return stringBuilder.ToString();
		}
	}
}
