using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.PivotTable
{
	/// SXDI - Data Item (0x00C5)<br />
	///
	/// @author Patrick Cheng
	public class DataItemRecord : StandardRecord
	{
		public const short sid = 197;

		private int isxvdData;

		private int iiftab;

		private int df;

		private int isxvd;

		private int isxvi;

		private int ifmt;

		private string name;

		protected override int DataSize => 12 + StringUtil.GetEncodedSize(name);

		public override short Sid => 197;

		public DataItemRecord(RecordInputStream in1)
		{
			isxvdData = in1.ReadUShort();
			iiftab = in1.ReadUShort();
			df = in1.ReadUShort();
			isxvd = in1.ReadUShort();
			isxvi = in1.ReadUShort();
			ifmt = in1.ReadUShort();
			name = in1.ReadString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(isxvdData);
			out1.WriteShort(iiftab);
			out1.WriteShort(df);
			out1.WriteShort(isxvd);
			out1.WriteShort(isxvi);
			out1.WriteShort(ifmt);
			StringUtil.WriteUnicodeString(out1, name);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SXDI]\n");
			stringBuilder.Append("  .isxvdData = ").Append(HexDump.ShortToHex(isxvdData)).Append("\n");
			stringBuilder.Append("  .iiftab = ").Append(HexDump.ShortToHex(iiftab)).Append("\n");
			stringBuilder.Append("  .df = ").Append(HexDump.ShortToHex(df)).Append("\n");
			stringBuilder.Append("  .isxvd = ").Append(HexDump.ShortToHex(isxvd)).Append("\n");
			stringBuilder.Append("  .isxvi = ").Append(HexDump.ShortToHex(isxvi)).Append("\n");
			stringBuilder.Append("  .ifmt = ").Append(HexDump.ShortToHex(ifmt)).Append("\n");
			stringBuilder.Append("[/SXDI]\n");
			return stringBuilder.ToString();
		}
	}
}
