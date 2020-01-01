using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace NPOI.Util
{
	/// <summary>
	/// dump data in hexadecimal format; derived from a HexDump utility I
	/// wrote in June 2001.
	/// @author Marc Johnson
	/// @author Glen Stampoultzis  (glens at apache.org)
	/// </summary>
	public class HexDump
	{
		private static readonly char[] _hexcodes = new char[18]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'\0',
			'\0'
		};

		private static readonly int[] _shifts = new int[16]
		{
			60,
			56,
			52,
			48,
			44,
			40,
			36,
			32,
			28,
			24,
			20,
			16,
			12,
			8,
			4,
			0
		};

		public static readonly string EOL = Environment.NewLine;

		private HexDump()
		{
		}

		private static string Dump(byte value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			for (int i = 0; i < 2; i++)
			{
				stringBuilder.Append(_hexcodes[(value >> _shifts[i + 6]) & 0xF]);
			}
			return stringBuilder.ToString();
		}

		private static string Dump(long value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			for (int i = 0; i < 8; i++)
			{
				stringBuilder.Append(_hexcodes[(int)(value >> _shifts[i + _shifts.Length - 8]) & 0xF]);
			}
			return stringBuilder.ToString();
		}

		public static string Dump(byte[] data, long offset, int index)
		{
			if (index < 0 || index >= data.Length)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "illegal index: {0} into array of length {1}", new object[2]
				{
					index,
					data.Length
				});
				throw new IndexOutOfRangeException(message);
			}
			long num = offset + index;
			StringBuilder stringBuilder = new StringBuilder(74);
			for (int i = index; i < data.Length; i += 16)
			{
				int num2 = data.Length - i;
				if (num2 > 16)
				{
					num2 = 16;
				}
				stringBuilder.Append(Dump(num)).Append(' ');
				for (int j = 0; j < 16; j++)
				{
					if (j < num2)
					{
						stringBuilder.Append(Dump(data[j + i]));
					}
					else
					{
						stringBuilder.Append("  ");
					}
					stringBuilder.Append(' ');
				}
				for (int k = 0; k < num2; k++)
				{
					if (data[k + i] >= 32 && data[k + i] < 127)
					{
						stringBuilder.Append((char)data[k + i]);
					}
					else
					{
						stringBuilder.Append('.');
					}
				}
				stringBuilder.Append(EOL);
				num += num2;
			}
			return stringBuilder.ToString();
		}

		public static void Dump(byte[] data, long offset, Stream stream, int index)
		{
			Dump(data, offset, stream, index, data.Length - index);
		}

		public static void Dump(Stream inStream, int start, int bytesToDump)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (bytesToDump == -1)
				{
					for (int num = inStream.ReadByte(); num != -1; num = inStream.ReadByte())
					{
						memoryStream.WriteByte((byte)num);
					}
				}
				else
				{
					int num2 = bytesToDump;
					while (num2-- > 0)
					{
						int num4 = inStream.ReadByte();
						if (num4 == -1)
						{
							break;
						}
						memoryStream.WriteByte((byte)num4);
					}
				}
				byte[] array = memoryStream.ToArray();
				Dump(array, 0L, null, start, array.Length);
			}
		}

		public static void Dump(byte[] data, long offset, Stream stream, int index, int length)
		{
			if (data.Length == 0)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "No Data{0}", new object[1]
				{
					Environment.NewLine
				}));
				if (stream != null)
				{
					stream.Write(bytes, 0, bytes.Length);
					stream.Flush();
				}
			}
			else
			{
				if (index < 0 || index >= data.Length)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "illegal index: {0} into array of length {1}", new object[2]
					{
						index,
						data.Length
					});
					throw new IndexOutOfRangeException(message);
				}
				if (data.Length != 0)
				{
					long num = offset + index;
					StringBuilder stringBuilder = new StringBuilder(74);
					int num2 = Math.Min(data.Length, index + length);
					for (int i = index; i < num2; i += 16)
					{
						int num3 = num2 - i;
						if (num3 > 16)
						{
							num3 = 16;
						}
						stringBuilder.Append(Dump(num)).Append(' ');
						for (int j = 0; j < 16; j++)
						{
							if (j < num3)
							{
								stringBuilder.Append(Dump(data[j + i]));
							}
							else
							{
								stringBuilder.Append("  ");
							}
							stringBuilder.Append(' ');
						}
						for (int k = 0; k < num3; k++)
						{
							if (data[k + i] >= 32 && data[k + i] < 127)
							{
								stringBuilder.Append((char)data[k + i]);
							}
							else
							{
								stringBuilder.Append('.');
							}
						}
						stringBuilder.Append(EOL);
						byte[] bytes2 = Encoding.UTF8.GetBytes(stringBuilder.ToString());
						if (stream != null)
						{
							stream.Write(bytes2, 0, bytes2.Length);
							stream.Flush();
						}
						stringBuilder.Length = 0;
						num += num3;
					}
				}
			}
		}

		/// <summary>
		/// Shorts to hex.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>char array of 2 (zero padded) uppercase hex chars and prefixed with '0x'</returns>
		public static char[] ShortToHex(int value)
		{
			return ToHexChars(value, 2);
		}

		/// <summary>
		/// Bytes to hex.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>char array of 1 (zero padded) uppercase hex chars and prefixed with '0x'</returns>
		public static char[] ByteToHex(int value)
		{
			return ToHexChars(value, 1);
		}

		/// <summary>
		/// Ints to hex.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>char array of 4 (zero padded) uppercase hex chars and prefixed with '0x'</returns>
		public static char[] IntToHex(int value)
		{
			return ToHexChars(value, 4);
		}

		/// <summary>
		/// char array of 4 (zero padded) uppercase hex chars and prefixed with '0x'
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>char array of 4 (zero padded) uppercase hex chars and prefixed with '0x'</returns>
		public static char[] LongToHex(long value)
		{
			return ToHexChars(value, 8);
		}

		/// <summary>
		/// Toes the hex chars.
		/// </summary>
		/// <param name="pValue">The p value.</param>
		/// <param name="nBytes">The n bytes.</param>
		/// <returns>char array of uppercase hex chars, zero padded and prefixed with '0x'</returns>
		private static char[] ToHexChars(long pValue, int nBytes)
		{
			int num = 2 + nBytes * 2;
			char[] array = new char[num];
			long num2 = pValue;
			do
			{
				array[--num] = _hexcodes[(int)(num2 & 0xF)];
				num2 >>= 4;
			}
			while (num > 1);
			array[0] = '0';
			array[1] = 'x';
			return array;
		}

		public static string ToHex(byte value)
		{
			return ToHex(value, 2);
		}

		public static string ToHex(short value)
		{
			return ToHex(value, 4);
		}

		public static string ToHex(int value)
		{
			return ToHex(value, 8);
		}

		public static string ToHex(long value)
		{
			return ToHex(value, 16);
		}

		public static string ToHex(byte[] value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(ToHex(value[i]));
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static string ToHex(short[] value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(ToHex(value[i]));
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		private static string ToHex(long value, int digits)
		{
			StringBuilder stringBuilder = new StringBuilder(digits);
			for (int i = 0; i < digits; i++)
			{
				stringBuilder.Append(_hexcodes[(int)((value >> _shifts[i + (16 - digits)]) & 0xF)]);
			}
			return stringBuilder.ToString();
		}

		public static string ToHex(byte[] value, int bytesPerLine)
		{
			int num = (int)Math.Round(Math.Log((double)value.Length) / Math.Log(10.0) + 0.5);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(": ");
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(0.0.ToString(stringBuilder.ToString(), CultureInfo.InvariantCulture));
			int num2 = -1;
			for (int j = 0; j < value.Length; j++)
			{
				if (++num2 == bytesPerLine)
				{
					stringBuilder2.Append('\n');
					stringBuilder2.Append(((double)j).ToString(stringBuilder.ToString(), CultureInfo.InvariantCulture));
					num2 = 0;
				}
				stringBuilder2.Append(ToHex(value[j]));
				stringBuilder2.Append(", ");
			}
			return stringBuilder2.ToString();
		}
	}
}
