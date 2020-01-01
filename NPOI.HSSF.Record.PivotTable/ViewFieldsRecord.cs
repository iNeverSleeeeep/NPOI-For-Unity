using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.PivotTable
{
	/// SXVD - View Fields (0x00B1)<br />
	///
	/// @author Patrick Cheng
	public class ViewFieldsRecord : StandardRecord
	{
		/// values for the {@link ViewFieldsRecord#sxaxis} field
		private enum Axis
		{
			NoAxis = 0,
			Row = 1,
			Column = 2,
			Page = 4,
			Data = 8
		}

		public const short sid = 177;

		/// the value of the <c>cchName</c> field when the name is not present 
		private const int STRING_NOT_PRESENT_LEN = 65535;

		/// 5 shorts 
		private const int BASE_SIZE = 10;

		private int sxaxis;

		private int cSub;

		private int grbitSub;

		private int cItm;

		private string _name;

		protected override int DataSize
		{
			get
			{
				if (_name == null)
				{
					return 10;
				}
				return 11 + _name.Length * ((!StringUtil.HasMultibyte(_name)) ? 1 : 2);
			}
		}

		public override short Sid => 177;

		public ViewFieldsRecord(RecordInputStream in1)
		{
			sxaxis = in1.ReadShort();
			cSub = in1.ReadShort();
			grbitSub = in1.ReadShort();
			cItm = in1.ReadShort();
			int num = in1.ReadUShort();
			if (num != 65535)
			{
				int num2 = in1.ReadByte();
				if ((num2 & 1) != 0)
				{
					_name = in1.ReadUnicodeLEString(num);
				}
				else
				{
					_name = in1.ReadCompressedUnicode(num);
				}
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(sxaxis);
			out1.WriteShort(cSub);
			out1.WriteShort(grbitSub);
			out1.WriteShort(cItm);
			if (_name != null)
			{
				StringUtil.WriteUnicodeString(out1, _name);
			}
			else
			{
				out1.WriteShort(65535);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SXVD]\n");
			stringBuilder.Append("    .sxaxis    = ").Append(HexDump.ShortToHex(sxaxis)).Append('\n');
			stringBuilder.Append("    .cSub      = ").Append(HexDump.ShortToHex(cSub)).Append('\n');
			stringBuilder.Append("    .grbitSub  = ").Append(HexDump.ShortToHex(grbitSub)).Append('\n');
			stringBuilder.Append("    .cItm      = ").Append(HexDump.ShortToHex(cItm)).Append('\n');
			stringBuilder.Append("    .name      = ").Append(_name).Append('\n');
			stringBuilder.Append("[/SXVD]\n");
			return stringBuilder.ToString();
		}
	}
}
