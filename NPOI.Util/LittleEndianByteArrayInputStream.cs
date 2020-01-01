using System;

namespace NPOI.Util
{
	/// <summary>
	/// Adapts a plain byte array to <see cref="T:NPOI.Util.ILittleEndianInput" />
	/// </summary>
	/// <remarks>@author Josh Micich</remarks>
	public class LittleEndianByteArrayInputStream : ILittleEndianInput
	{
		private byte[] _buf;

		private int _endIndex;

		private int _ReadIndex;

		public LittleEndianByteArrayInputStream(byte[] buf, int startOffset, int maxReadLen)
		{
			_buf = buf;
			_ReadIndex = startOffset;
			_endIndex = startOffset + maxReadLen;
		}

		public LittleEndianByteArrayInputStream(byte[] buf, int startOffset)
			: this(buf, startOffset, buf.Length - startOffset)
		{
		}

		public LittleEndianByteArrayInputStream(byte[] buf)
			: this(buf, 0, buf.Length)
		{
		}

		public int Available()
		{
			return _endIndex - _ReadIndex;
		}

		private void CheckPosition(int i)
		{
			if (i > _endIndex - _ReadIndex)
			{
				throw new RuntimeException("Buffer overrun");
			}
		}

		public int GetReadIndex()
		{
			return _ReadIndex;
		}

		public int ReadByte()
		{
			CheckPosition(1);
			return _buf[_ReadIndex++];
		}

		public int ReadInt()
		{
			CheckPosition(4);
			int num = _ReadIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			int num7 = _buf[num++] & 0xFF;
			int num9 = _buf[num++] & 0xFF;
			_ReadIndex = num;
			return (num9 << 24) + (num7 << 16) + (num5 << 8) + num3;
		}

		public long ReadLong()
		{
			CheckPosition(8);
			int num = _ReadIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			int num7 = _buf[num++] & 0xFF;
			int num9 = _buf[num++] & 0xFF;
			int num11 = _buf[num++] & 0xFF;
			int num13 = _buf[num++] & 0xFF;
			int num15 = _buf[num++] & 0xFF;
			int num17 = _buf[num++] & 0xFF;
			_ReadIndex = num;
			return ((long)num17 << 56) + ((long)num15 << 48) + ((long)num13 << 40) + ((long)num11 << 32) + ((long)num9 << 24) + (num7 << 16) + (num5 << 8) + num3;
		}

		public short ReadShort()
		{
			return (short)ReadUShort();
		}

		public int ReadUByte()
		{
			CheckPosition(1);
			return _buf[_ReadIndex++] & 0xFF;
		}

		public int ReadUShort()
		{
			CheckPosition(2);
			int num = _ReadIndex;
			int num3 = _buf[num++] & 0xFF;
			int num5 = _buf[num++] & 0xFF;
			_ReadIndex = num;
			return (num5 << 8) + num3;
		}

		public void ReadFully(byte[] buf, int off, int len)
		{
			CheckPosition(len);
			Array.Copy(_buf, _ReadIndex, buf, off, len);
			_ReadIndex += len;
		}

		public void ReadFully(byte[] buf)
		{
			ReadFully(buf, 0, buf.Length);
		}

		public double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(ReadLong());
		}
	}
}
