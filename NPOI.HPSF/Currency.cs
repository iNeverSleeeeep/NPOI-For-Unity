using NPOI.Util;

namespace NPOI.HPSF
{
	internal class Currency
	{
		public const int SIZE = 8;

		private byte[] _value;

		public Currency(byte[] data, int offset)
		{
			_value = LittleEndian.GetByteArray(data, offset, 8);
		}
	}
}
