using NPOI.Util;
using System.IO;

namespace NPOI.HPSF
{
	public class Filetime
	{
		public const int SIZE = 8;

		private int _dwHighDateTime;

		private int _dwLowDateTime;

		public long High => _dwHighDateTime;

		public long Low => _dwLowDateTime;

		public Filetime(byte[] data, int offset)
		{
			_dwLowDateTime = LittleEndian.GetInt(data, offset);
			_dwHighDateTime = LittleEndian.GetInt(data, offset + 4);
		}

		public Filetime(int low, int high)
		{
			_dwLowDateTime = low;
			_dwHighDateTime = high;
		}

		public byte[] ToByteArray()
		{
			byte[] array = new byte[8];
			LittleEndian.PutInt(array, 0, _dwLowDateTime);
			LittleEndian.PutInt(array, 4, _dwHighDateTime);
			return array;
		}

		public int Write(Stream out1)
		{
			LittleEndian.PutInt(_dwLowDateTime, out1);
			LittleEndian.PutInt(_dwHighDateTime, out1);
			return 8;
		}
	}
}
