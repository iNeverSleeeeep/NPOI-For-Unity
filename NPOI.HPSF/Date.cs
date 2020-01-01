using NPOI.Util;

namespace NPOI.HPSF
{
	internal class Date
	{
		public const int SIZE = 8;

		private byte[] _value;

		public Date(byte[] data, int offset)
		{
			_value = LittleEndian.GetByteArray(data, offset, 8);
		}
	}
}
