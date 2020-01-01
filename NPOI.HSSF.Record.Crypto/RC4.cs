using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Crypto
{
	/// Simple implementation of the alleged RC4 algorithm.
	///
	/// Inspired by <A HREF="http://en.wikipedia.org/wiki/RC4">wikipedia's RC4 article</A>
	///
	/// @author Josh Micich
	internal class RC4
	{
		private int _i;

		private int _j;

		private byte[] _s = new byte[256];

		public RC4(byte[] key)
		{
			int num = key.Length;
			for (int i = 0; i < 256; i++)
			{
				_s[i] = (byte)i;
			}
			int j = 0;
			int num2 = 0;
			for (; j < 256; j++)
			{
				num2 = ((num2 + key[j % num] + _s[j]) & 0xFF);
				byte b = _s[j];
				_s[j] = _s[num2];
				_s[num2] = b;
			}
			_i = 0;
			_j = 0;
		}

		public byte Output()
		{
			_i = ((_i + 1) & 0xFF);
			_j = ((_j + _s[_i]) & 0xFF);
			byte b = _s[_i];
			_s[_i] = _s[_j];
			_s[_j] = b;
			return _s[(_s[_i] + _s[_j]) & 0xFF];
		}

		public void Encrypt(byte[] in1)
		{
			for (int i = 0; i < in1.Length; i++)
			{
				in1[i] = (byte)(in1[i] ^ Output());
			}
		}

		public void Encrypt(byte[] in1, int OffSet, int len)
		{
			int num = OffSet + len;
			for (int i = OffSet; i < num; i++)
			{
				in1[i] = (byte)(in1[i] ^ Output());
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append("i=").Append(_i);
			stringBuilder.Append(" j=").Append(_j);
			stringBuilder.Append("]");
			stringBuilder.Append("\n");
			stringBuilder.Append(HexDump.Dump(_s, 0L, 0));
			return stringBuilder.ToString();
		}
	}
}
