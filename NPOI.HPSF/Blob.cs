using NPOI.Util;

namespace NPOI.HPSF
{
	internal class Blob
	{
		private byte[] _value;

		public int Size => 4 + _value.Length;

		public Blob(byte[] data, int offset)
		{
			int @int = LittleEndian.GetInt(data, offset);
			if (@int == 0)
			{
				_value = new byte[0];
			}
			else
			{
				_value = LittleEndian.GetByteArray(data, offset + 4, @int);
			}
		}
	}
}
