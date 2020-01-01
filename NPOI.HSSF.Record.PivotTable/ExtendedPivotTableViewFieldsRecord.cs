using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.PivotTable
{
	/// SXVDEX - Extended PivotTable View Fields (0x0100)<br />
	///
	/// @author Patrick Cheng
	public class ExtendedPivotTableViewFieldsRecord : StandardRecord
	{
		public const short sid = 256;

		/// the value of the <c>cchSubName</c> field when the subName is not present 
		private const int STRING_NOT_PRESENT_LEN = 65535;

		private int grbit1;

		private int grbit2;

		private int citmShow;

		private int isxdiSort;

		private int isxdiShow;

		private int reserved1;

		private int reserved2;

		private string subName;

		protected override int DataSize => 20 + ((subName != null) ? (2 * subName.Length) : 0);

		public override short Sid => 256;

		public ExtendedPivotTableViewFieldsRecord(RecordInputStream in1)
		{
			grbit1 = in1.ReadInt();
			grbit2 = in1.ReadUByte();
			citmShow = in1.ReadUByte();
			isxdiSort = in1.ReadUShort();
			isxdiShow = in1.ReadUShort();
			switch (in1.Remaining)
			{
			case 0:
				reserved1 = 0;
				reserved2 = 0;
				subName = null;
				break;
			default:
				throw new RecordFormatException("Unexpected remaining size (" + in1.Remaining + ")");
			case 10:
			{
				int num = in1.ReadUShort();
				reserved1 = in1.ReadInt();
				reserved2 = in1.ReadInt();
				if (num != 65535)
				{
					subName = in1.ReadUnicodeLEString(num);
				}
				break;
			}
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(grbit1);
			out1.WriteByte(grbit2);
			out1.WriteByte(citmShow);
			out1.WriteShort(isxdiSort);
			out1.WriteShort(isxdiShow);
			if (subName == null)
			{
				out1.WriteShort(65535);
			}
			else
			{
				out1.WriteShort(subName.Length);
			}
			out1.WriteInt(reserved1);
			out1.WriteInt(reserved2);
			if (subName != null)
			{
				StringUtil.PutUnicodeLE(subName, out1);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SXVDEX]\n");
			stringBuilder.Append("    .grbit1 =").Append(HexDump.IntToHex(grbit1)).Append("\n");
			stringBuilder.Append("    .grbit2 =").Append(HexDump.ByteToHex(grbit2)).Append("\n");
			stringBuilder.Append("    .citmShow =").Append(HexDump.ByteToHex(citmShow)).Append("\n");
			stringBuilder.Append("    .isxdiSort =").Append(HexDump.ShortToHex(isxdiSort)).Append("\n");
			stringBuilder.Append("    .isxdiShow =").Append(HexDump.ShortToHex(isxdiShow)).Append("\n");
			stringBuilder.Append("    .subName =").Append(subName).Append("\n");
			stringBuilder.Append("[/SXVDEX]\n");
			return stringBuilder.ToString();
		}
	}
}
