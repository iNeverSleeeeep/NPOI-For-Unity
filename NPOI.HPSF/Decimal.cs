using NPOI.Util;

namespace NPOI.HPSF
{
	public class Decimal
	{
		public const int SIZE = 16;

		private short field_1_wReserved;

		private byte field_2_scale;

		private byte field_3_sign;

		private int field_4_hi32;

		private long field_5_lo64;

		public Decimal(byte[] data, int startOffset)
		{
			field_1_wReserved = LittleEndian.GetShort(data, startOffset);
			int num = startOffset + 2;
			field_2_scale = data[num];
			num++;
			field_3_sign = data[num];
			num++;
			field_4_hi32 = LittleEndian.GetInt(data, num);
			num += 4;
			field_5_lo64 = LittleEndian.GetLong(data, num);
			num += 8;
		}
	}
}
