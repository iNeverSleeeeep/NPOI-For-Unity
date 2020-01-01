using System;
using System.IO;

namespace NPOI.Util
{
	/// <summary>
	/// a utility class for handling little-endian numbers, which the 80x86 world is
	/// replete with. The methods are all static, and input/output is from/to byte
	/// arrays, or from InputStreams.
	/// </summary>
	/// <remarks>
	/// @author     Marc Johnson (mjohnson at apache dot org)
	/// @author     Andrew Oliver (acoliver at apache dot org)
	/// </remarks>
	public class LittleEndian : LittleEndianConsts
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.LittleEndian" /> class.
		/// </summary>
		private LittleEndian()
		{
		}

		/// <summary>
		/// get a short value from a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the short (16-bit) value</returns>
		public static short GetShort(byte[] data, int offset)
		{
			int num = data[offset] & 0xFF;
			int num2 = data[offset + 1] & 0xFF;
			return (short)((num2 << 8) + num);
		}

		/// <summary>
		/// get an unsigned short value from a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the unsigned short (16-bit) value in an integer</returns>
		public static int GetUShort(byte[] data, int offset)
		{
			int num = data[offset] & 0xFF;
			int num2 = data[offset + 1] & 0xFF;
			return (num2 << 8) + num;
		}

		/// <summary>
		/// get a short value from a byte array
		/// </summary>
		/// <param name="data">a starting offset into the byte array</param>
		/// <returns>the short (16-bit) value</returns>
		public static short GetShort(byte[] data)
		{
			return GetShort(data, 0);
		}

		/// <summary>
		/// get a short value from a byte array
		/// </summary>
		/// <param name="data">the unsigned short (16-bit) value in an integer</param>
		/// <returns></returns>
		public static int GetUShort(byte[] data)
		{
			return GetUShort(data, 0);
		}

		/// <summary>
		/// get an int value from a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the int (32-bit) value</returns>
		public static int GetInt(byte[] data, int offset)
		{
			int num = offset;
			int num3 = data[num++] & 0xFF;
			int num5 = data[num++] & 0xFF;
			int num7 = data[num++] & 0xFF;
			int num9 = data[num++] & 0xFF;
			return (num9 << 24) + (num7 << 16) + (num5 << 8) + num3;
		}

		/// <summary>
		/// get an int value from the beginning of a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <returns>the int (32-bit) value</returns>
		public static int GetInt(byte[] data)
		{
			return GetInt(data, 0);
		}

		/// <summary>
		/// Gets the U int.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the unsigned int (32-bit) value in a long</returns>
		public static long GetUInt(byte[] data, int offset)
		{
			long num = GetInt(data, offset);
			return num & uint.MaxValue;
		}

		/// <summary>
		/// Gets the U int.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <returns>the unsigned int (32-bit) value in a long</returns>
		public static long GetUInt(byte[] data)
		{
			return GetUInt(data, 0);
		}

		/// <summary>
		/// get a long value from a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the long (64-bit) value</returns>
		public static long GetLong(byte[] data, int offset)
		{
			long num = 0L;
			for (int num2 = offset + 8 - 1; num2 >= offset; num2--)
			{
				num <<= 8;
				num |= (255L & (long)data[num2]);
			}
			return num;
		}

		/// <summary>
		/// get a double value from a byte array, reads it in little endian format
		/// then converts the resulting revolting IEEE 754 (curse them) floating
		/// point number to a c# double
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns>the double (64-bit) value</returns>
		public static double GetDouble(byte[] data, int offset)
		{
			return BitConverter.Int64BitsToDouble(GetLong(data, offset));
		}

		/// <summary>
		/// Puts the short.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		public static void PutShort(byte[] data, int offset, short value)
		{
			int num = offset;
			data[num++] = (byte)(value & 0xFF);
			data[num++] = (byte)((value >> 8) & 0xFF);
		}

		/// <summary>
		/// Added for consistency with other put~() methods
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		public static void PutByte(byte[] data, int offset, int value)
		{
			data[offset] = (byte)value;
		}

		/// <summary>
		/// Puts the U short.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		public static void PutUShort(byte[] data, int offset, int value)
		{
			int num = offset;
			data[num++] = (byte)(value & 0xFF);
			data[num++] = (byte)((value >> 8) & 0xFF);
		}

		/// <summary>
		/// put a short value into beginning of a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">the short (16-bit) value</param>
		[Obsolete]
		public static void PutShort(byte[] data, short value)
		{
			PutShort(data, 0, value);
		}

		/// Put signed short into output stream
		///
		/// @param value
		///            the short (16-bit) value
		/// @param outputStream
		///            output stream
		/// @throws IOException
		///             if an I/O error occurs
		public static void PutShort(Stream outputStream, short value)
		{
			outputStream.WriteByte((byte)(value & 0xFF));
			outputStream.WriteByte((byte)((value >> 8) & 0xFF));
		}

		/// <summary>
		/// put an int value into a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">the int (32-bit) value</param>
		public static void PutInt(byte[] data, int offset, int value)
		{
			int num = offset;
			data[num++] = (byte)(value & 0xFF);
			data[num++] = (byte)((value >> 8) & 0xFF);
			data[num++] = (byte)((value >> 16) & 0xFF);
			data[num++] = (byte)((value >> 24) & 0xFF);
		}

		/// <summary>
		/// put an int value into beginning of a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">the int (32-bit) value</param>
		[Obsolete]
		public static void PutInt(byte[] data, int value)
		{
			PutInt(data, 0, value);
		}

		/// <summary>
		/// Put int into output stream
		/// </summary>
		/// <param name="value">the int (32-bit) value</param>
		/// <param name="outputStream">output stream</param>
		public static void PutInt(int value, Stream outputStream)
		{
			outputStream.WriteByte((byte)(value & 0xFF));
			outputStream.WriteByte((byte)((value >> 8) & 0xFF));
			outputStream.WriteByte((byte)((value >> 16) & 0xFF));
			outputStream.WriteByte((byte)((value >> 24) & 0xFF));
		}

		/// <summary>
		/// put a long value into a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">the long (64-bit) value</param>
		public static void PutLong(byte[] data, int offset, long value)
		{
			int num = 8 + offset;
			long num2 = value;
			for (int i = offset; i < num; i++)
			{
				data[i] = (byte)(num2 & 0xFF);
				num2 >>= 8;
			}
		}

		/// <summary>
		/// put a double value into a byte array
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">the double (64-bit) value</param>
		public static void PutDouble(byte[] data, int offset, double value)
		{
			long num = 0L;
			num = ((!double.IsNaN(value)) ? BitConverter.DoubleToInt64Bits(value) : (-276939487313920L));
			PutLong(data, offset, num);
		}

		/// <summary>
		/// Reads the short.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		public static short ReadShort(Stream stream)
		{
			return (short)ReadUShort(stream);
		}

		public static int ReadUShort(Stream stream)
		{
			int num = stream.ReadByte();
			int num2 = stream.ReadByte();
			if ((num | num2) < 0)
			{
				throw new BufferUnderrunException();
			}
			return (num2 << 8) + num;
		}

		/// <summary>
		/// get an int value from an Stream
		/// </summary>
		/// <param name="stream">the Stream from which the int is to be read</param>
		/// <returns>the int (32-bit) value</returns>
		/// <exception cref="T:System.IO.IOException">will be propagated back to the caller</exception>
		/// <exception cref="T:NPOI.Util.BufferUnderrunException">if the stream cannot provide enough bytes</exception>
		public static int ReadInt(Stream stream)
		{
			int num = stream.ReadByte();
			int num2 = stream.ReadByte();
			int num3 = stream.ReadByte();
			int num4 = stream.ReadByte();
			if ((num | num2 | num3 | num4) < 0)
			{
				throw new BufferUnderrunException();
			}
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		/// <summary>
		/// get a long value from a Stream
		/// </summary>
		/// <param name="stream">the Stream from which the long is to be read</param>
		/// <returns>the long (64-bit) value</returns>
		/// <exception cref="T:System.IO.IOException">will be propagated back to the caller</exception>
		/// <exception cref="T:NPOI.Util.BufferUnderrunException">if the stream cannot provide enough bytes</exception>
		public static long ReadLong(Stream stream)
		{
			int num = stream.ReadByte();
			int num2 = stream.ReadByte();
			int num3 = stream.ReadByte();
			int num4 = stream.ReadByte();
			int num5 = stream.ReadByte();
			int num6 = stream.ReadByte();
			int num7 = stream.ReadByte();
			int num8 = stream.ReadByte();
			if ((num | num2 | num3 | num4 | num5 | num6 | num7 | num8) < 0)
			{
				throw new BufferUnderrunException();
			}
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		/// <summary>
		/// Us the byte to int.
		/// </summary>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public static int UByteToInt(byte b)
		{
			if ((b & 0x80) == 0)
			{
				return b;
			}
			return (b & 0x7F) + 128;
		}

		/// <summary>
		/// get the unsigned value of a byte.
		/// </summary>
		/// <param name="data">the byte array.</param>
		/// <param name="offset">a starting offset into the byte array.</param>
		/// <returns>the unsigned value of the byte as a 32 bit integer</returns>
		[Obsolete]
		public static int GetUnsignedByte(byte[] data, int offset)
		{
			return data[offset] & 0xFF;
		}

		/// <summary>
		/// Copy a portion of a byte array
		/// </summary>
		/// <param name="data"> the original byte array</param>
		/// <param name="offset">Where to start copying from.</param>
		/// <param name="size">Number of bytes to copy.</param>
		/// <returns>The byteArray value</returns>
		///             <exception cref="T:System.IndexOutOfRangeException">
		///             if copying would cause access ofdata outside array bounds.
		///             </exception>
		public static byte[] GetByteArray(byte[] data, int offset, int size)
		{
			byte[] array = new byte[size];
			Array.Copy(data, offset, array, 0, size);
			return array;
		}

		[Obsolete]
		public static double GetDouble(byte[] data)
		{
			return GetDouble(data, 0);
		}

		[Obsolete]
		public static long GetLong(byte[] data)
		{
			return GetLong(data, 0);
		}

		[Obsolete]
		public static ulong GetULong(byte[] data)
		{
			return GetULong(data, 0);
		}

		[Obsolete]
		public static ulong GetULong(byte[] data, int offset)
		{
			return BitConverter.ToUInt64(data, offset);
		}

		private static long GetNumber(byte[] data, int offset, int size)
		{
			long num = 0L;
			for (int num2 = offset + size - 1; num2 >= offset; num2--)
			{
				num <<= 8;
				num |= (255L & (long)data[num2]);
			}
			return num;
		}

		/// <summary>
		/// Gets the unsigned byte.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <returns></returns>
		public static short GetUByte(byte[] data)
		{
			return (short)(data[0] & 0xFF);
		}

		/// <summary>
		/// Gets the unsigned byte.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <returns></returns>
		public static short GetUByte(byte[] data, int offset)
		{
			return (short)(data[offset] & 0xFF);
		}

		/// <summary>
		/// Puts the double.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutDouble(byte[] data, double value)
		{
			PutDouble(data, 0, value);
		}

		/// put a double value into a byte array
		///
		/// @param value
		///            the double (64-bit) value
		/// @param outputStream
		///            output stream
		/// @throws IOException
		///             if an I/O error occurs
		public static void PutDouble(double value, Stream outputStream)
		{
			PutLong(BitConverter.DoubleToInt64Bits(value), outputStream);
		}

		/// <summary>
		/// Puts the uint.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutUInt(byte[] data, uint value)
		{
			PutUInt(data, 0, value);
		}

		/// Put unsigned int into output stream
		///
		/// @param value
		///            the int (32-bit) value
		/// @param outputStream
		///            output stream
		/// @throws IOException
		///             if an I/O error occurs
		public static void PutUInt(long value, Stream outputStream)
		{
			outputStream.WriteByte((byte)(value & 0xFF));
			outputStream.WriteByte((byte)((value >> 8) & 0xFF));
			outputStream.WriteByte((byte)((value >> 16) & 0xFF));
			outputStream.WriteByte((byte)((value >> 24) & 0xFF));
		}

		/// <summary>
		/// Puts the uint.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutUInt(byte[] data, int offset, uint value)
		{
			PutNumber(data, offset, Convert.ToInt64(value), 4);
		}

		/// <summary>
		/// Puts the long.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutLong(byte[] data, long value)
		{
			PutLong(data, 0, value);
		}

		/// Put long into output stream
		///
		/// @param value
		///            the long (64-bit) value
		/// @param outputStream
		///            output stream
		/// @throws IOException
		///             if an I/O error occurs
		public static void PutLong(long value, Stream outputStream)
		{
			outputStream.WriteByte((byte)(value & 0xFF));
			outputStream.WriteByte((byte)((value >> 8) & 0xFF));
			outputStream.WriteByte((byte)((value >> 16) & 0xFF));
			outputStream.WriteByte((byte)((value >> 24) & 0xFF));
			outputStream.WriteByte((byte)((value >> 32) & 0xFF));
			outputStream.WriteByte((byte)((value >> 40) & 0xFF));
			outputStream.WriteByte((byte)((value >> 48) & 0xFF));
			outputStream.WriteByte((byte)((value >> 56) & 0xFF));
		}

		/// <summary>
		/// Puts the long.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutULong(byte[] data, ulong value)
		{
			PutULong(data, 0, value);
		}

		/// <summary>
		/// Puts the ulong.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutULong(byte[] data, int offset, ulong value)
		{
			PutNumber(data, offset, value, 8);
		}

		/// <summary>
		/// Puts the number.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		/// <param name="size">The size.</param>
		private static void PutNumber(byte[] data, int offset, long value, int size)
		{
			int num = size + offset;
			long num2 = value;
			for (int i = offset; i < num; i++)
			{
				data[i] = (byte)(num2 & 0xFF);
				num2 >>= 8;
			}
		}

		/// <summary>
		/// Puts the number.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		/// <param name="size">The size.</param>
		private static void PutNumber(byte[] data, int offset, ulong value, int size)
		{
			int num = size + offset;
			ulong num2 = value;
			for (int i = offset; i < num; i++)
			{
				data[i] = (byte)(num2 & 0xFF);
				num2 >>= 8;
			}
		}

		/// <summary>
		/// Puts the short array.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="offset">a starting offset into the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutShortArray(byte[] data, int offset, short[] value)
		{
			PutNumber(data, offset, Convert.ToInt64(value.Length), 2);
			for (int i = 0; i < value.Length; i++)
			{
				PutNumber(data, offset + 2 + i * 2, Convert.ToInt64(value[i]), 2);
			}
		}

		/// <summary>
		/// Puts the U short.
		/// </summary>
		/// <param name="data">the byte array</param>
		/// <param name="value">The value.</param>
		[Obsolete]
		public static void PutUShort(byte[] data, int value)
		{
			PutNumber(data, 0, Convert.ToInt64(value), 2);
		}

		/// Put unsigned short into output stream
		///
		/// @param value
		///            the unsigned short (16-bit) value
		/// @param outputStream
		///            output stream
		/// @throws IOException
		///             if an I/O error occurs
		public static void PutUShort(int value, Stream outputStream)
		{
			outputStream.WriteByte((byte)(value & 0xFF));
			outputStream.WriteByte((byte)((value >> 8) & 0xFF));
		}

		/// <summary>
		/// Reads from stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		[Obsolete]
		public static byte[] ReadFromStream(Stream stream, int size)
		{
			byte[] array = new byte[size];
			int num = stream.Read(array, 0, array.Length);
			if (num == 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				return array;
			}
			if (num != size)
			{
				throw new BufferUnderrunException();
			}
			return array;
		}

		/// <summary>
		/// Reads the long.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		[Obsolete]
		public static ulong ReadULong(Stream stream)
		{
			return BitConverter.ToUInt64(ReadFromStream(stream, 8), 0);
		}
	}
}
