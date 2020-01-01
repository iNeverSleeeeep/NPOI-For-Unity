using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// TABLESTYLES (0x088E)<br />
	///
	/// @author Patrick Cheng
	public class TableStylesRecord : StandardRecord
	{
		public const short sid = 2190;

		private int rt;

		private int grbitFrt;

		private byte[] unused = new byte[8];

		private int cts;

		private string rgchDefListStyle;

		private string rgchDefPivotStyle;

		protected override int DataSize => 20 + 2 * rgchDefListStyle.Length + 2 * rgchDefPivotStyle.Length;

		public override short Sid => 2190;

		public TableStylesRecord(RecordInputStream in1)
		{
			rt = in1.ReadUShort();
			grbitFrt = in1.ReadUShort();
			in1.ReadFully(unused);
			cts = in1.ReadInt();
			int requestedLength = in1.ReadUShort();
			int requestedLength2 = in1.ReadUShort();
			rgchDefListStyle = in1.ReadUnicodeLEString(requestedLength);
			rgchDefPivotStyle = in1.ReadUnicodeLEString(requestedLength2);
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.Write(unused);
			out1.WriteInt(cts);
			out1.WriteShort(rgchDefListStyle.Length);
			out1.WriteShort(rgchDefPivotStyle.Length);
			StringUtil.PutUnicodeLE(rgchDefListStyle, out1);
			StringUtil.PutUnicodeLE(rgchDefPivotStyle, out1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TABLESTYLES]\n");
			stringBuilder.Append("    .rt      =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt=").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .unused  =").Append(HexDump.ToHex(unused)).Append('\n');
			stringBuilder.Append("    .cts=").Append(HexDump.IntToHex(cts)).Append('\n');
			stringBuilder.Append("    .rgchDefListStyle=").Append(rgchDefListStyle).Append('\n');
			stringBuilder.Append("    .rgchDefPivotStyle=").Append(rgchDefPivotStyle).Append('\n');
			stringBuilder.Append("[/TABLESTYLES]\n");
			return stringBuilder.ToString();
		}
	}
}
