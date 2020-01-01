using NPOI.Util;
using System;
using System.IO;

namespace NPOI.POIFS.NIO
{
	/// <summary>
	/// A POIFS <see cref="T:NPOI.POIFS.NIO.DataSource" /> backed by a byte array.
	/// </summary>
	public class ByteArrayBackedDataSource : DataSource
	{
		private byte[] buffer;

		private long size;

		public override long Size => size;

		public ByteArrayBackedDataSource(byte[] data, int size)
		{
			buffer = data;
			this.size = size;
		}

		public ByteArrayBackedDataSource(byte[] data)
			: this(data, data.Length)
		{
		}

		public override ByteBuffer Read(int length, long position)
		{
			if (position >= size)
			{
				throw new IndexOutOfRangeException("Unable to read " + length + " bytes from " + position + " in stream of length " + size);
			}
			int length2 = (int)Math.Min(length, size - position);
			return ByteBuffer.CreateBuffer(buffer, (int)position, length2);
		}

		public override void Write(ByteBuffer src, long position)
		{
			long num = position + src.Length;
			if (num > buffer.Length)
			{
				Extend(num);
			}
			src.Read(buffer, (int)position, src.Length);
			if (num > size)
			{
				size = num;
			}
		}

		private void Extend(long length)
		{
			long num = length - buffer.Length;
			if ((double)num < (double)buffer.Length * 0.25)
			{
				num = (long)((double)buffer.Length * 0.25);
			}
			if (num < 4096)
			{
				num = 4096L;
			}
			byte[] destinationArray = new byte[(int)(num + buffer.Length)];
			Array.Copy(buffer, 0, destinationArray, 0, (int)size);
			buffer = destinationArray;
		}

		public override void CopyTo(Stream stream)
		{
			stream.Write(buffer, 0, (int)size);
		}

		public override void Close()
		{
			buffer = null;
			size = -1L;
		}
	}
}
