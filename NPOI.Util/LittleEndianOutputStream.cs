using System;
using System.IO;

namespace NPOI.Util
{
	/// <summary>
	/// Wraps an <see cref="T:System.IO.Stream" /> providing <see cref="T:NPOI.Util.ILittleEndianOutput" />
	/// </summary>
	/// <remarks>@author Josh Micich</remarks>
	public class LittleEndianOutputStream : ILittleEndianOutput, IDisposable
	{
		private Stream out1;

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && out1 != null)
			{
				out1.Dispose();
				out1 = null;
			}
		}

		public LittleEndianOutputStream(Stream out1)
		{
			this.out1 = out1;
		}

		public void WriteByte(int v)
		{
			try
			{
				out1.WriteByte((byte)v);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		public void WriteDouble(double v)
		{
			WriteLong(BitConverter.DoubleToInt64Bits(v));
		}

		public void WriteInt(int v)
		{
			int num = (v >> 24) & 0xFF;
			int num2 = (v >> 16) & 0xFF;
			int num3 = (v >> 8) & 0xFF;
			int num4 = v & 0xFF;
			try
			{
				out1.WriteByte((byte)num4);
				out1.WriteByte((byte)num3);
				out1.WriteByte((byte)num2);
				out1.WriteByte((byte)num);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		public void WriteLong(long v)
		{
			WriteInt((int)v);
			WriteInt((int)(v >> 32));
		}

		public void WriteShort(int v)
		{
			int num = (v >> 8) & 0xFF;
			int num2 = v & 0xFF;
			try
			{
				out1.WriteByte((byte)num2);
				out1.WriteByte((byte)num);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		public void Write(byte[] b)
		{
			try
			{
				out1.Write(b, 0, b.Length);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		public void Write(byte[] b, int off, int len)
		{
			try
			{
				out1.Write(b, off, len);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

		public void Flush()
		{
			out1.Flush();
		}
	}
}
