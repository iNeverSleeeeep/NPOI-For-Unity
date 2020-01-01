using System;
using System.IO;

namespace NPOI.Util
{
	/// <summary>
	/// Wraps an <see cref="T:System.IO.Stream" /> providing <see cref="T:NPOI.Util.ILittleEndianInput" /><p />
	///
	/// This class does not buffer any input, so the stream Read position maintained
	/// by this class is consistent with that of the inner stream.
	/// </summary>
	/// <remarks>
	/// @author Josh Micich
	/// </remarks>
	public class LittleEndianInputStream : ILittleEndianInput
	{
		private Stream in1;

		public int Available()
		{
			return (int)(in1.Length - in1.Position);
		}

		public LittleEndianInputStream(Stream is1)
		{
			in1 = is1;
		}

		public int ReadByte()
		{
			return (byte)ReadUByte();
		}

		public int ReadUByte()
		{
			int num;
			try
			{
				num = in1.ReadByte();
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
			CheckEOF(num);
			return num;
		}

		public double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(ReadLong());
		}

		public int ReadInt()
		{
			int num;
			int num2;
			int num3;
			int num4;
			try
			{
				num = in1.ReadByte();
				num2 = in1.ReadByte();
				num3 = in1.ReadByte();
				num4 = in1.ReadByte();
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
			CheckEOF(num | num2 | num3 | num4);
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		public long ReadLong()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			try
			{
				num = in1.ReadByte();
				num2 = in1.ReadByte();
				num3 = in1.ReadByte();
				num4 = in1.ReadByte();
				num5 = in1.ReadByte();
				num6 = in1.ReadByte();
				num7 = in1.ReadByte();
				num8 = in1.ReadByte();
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
			CheckEOF(num | num2 | num3 | num4 | num5 | num6 | num7 | num8);
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		public short ReadShort()
		{
			return (short)ReadUShort();
		}

		public int ReadUShort()
		{
			int num;
			int num2;
			try
			{
				num = in1.ReadByte();
				num2 = in1.ReadByte();
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
			CheckEOF(num | num2);
			return (num2 << 8) + num;
		}

		private static void CheckEOF(int value)
		{
			if (value < 0)
			{
				throw new RuntimeException("Unexpected end-of-file");
			}
		}

		public void ReadFully(byte[] buf)
		{
			ReadFully(buf, 0, buf.Length);
		}

		public void ReadFully(byte[] buf, int off, int len)
		{
			int num = off + len;
			for (int i = off; i < num; i++)
			{
				byte b;
				try
				{
					b = (byte)in1.ReadByte();
				}
				catch (IOException e)
				{
					throw new RuntimeException(e);
				}
				CheckEOF(b);
				buf[i] = b;
			}
		}
	}
}
