using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// PColor - element in the list of colors - consider it a "struct"
	internal class PColor
	{
		public const short ENCODED_SIZE = 4;

		public byte _red;

		public byte _green;

		public byte _blue;

		public PColor(int red, int green, int blue)
		{
			_red = (byte)red;
			_green = (byte)green;
			_blue = (byte)blue;
		}

		public PColor(RecordInputStream in1)
		{
			_red = (byte)in1.ReadByte();
			_green = (byte)in1.ReadByte();
			_blue = (byte)in1.ReadByte();
			in1.ReadByte();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(_red);
			out1.WriteByte(_green);
			out1.WriteByte(_blue);
			out1.WriteByte(0);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("  red           = ").Append(_red & 0xFF).Append('\n');
			stringBuilder.Append("  green         = ").Append(_green & 0xFF).Append('\n');
			stringBuilder.Append("  blue          = ").Append(_blue & 0xFF).Append('\n');
			return stringBuilder.ToString();
		}
	}
}
