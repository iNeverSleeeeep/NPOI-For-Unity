using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Util
{
	public class GUID
	{
		private const int TEXT_FORMAT_LENGTH = 36;

		public const int ENCODED_SIZE = 16;

		/// 4 bytes - little endian 
		private int _d1;

		/// 2 bytes - little endian 
		private int _d2;

		/// 2 bytes - little endian 
		private int _d3;

		/// 8 bytes - serialized as big endian,  stored with inverted endianness here
		private long _d4;

		public int D1 => _d1;

		public int D2 => _d2;

		public int D3 => _d3;

		public long D4
		{
			get
			{
				byte[] array;
				using (MemoryStream memoryStream = new MemoryStream(8))
				{
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(_d4);
					array = memoryStream.ToArray();
					binaryWriter.Close();
				}
				Array.Reverse(array);
				return new LittleEndianByteArrayInputStream(array).ReadLong();
			}
		}

		public GUID(ILittleEndianInput in1)
			: this(in1.ReadInt(), in1.ReadUShort(), in1.ReadUShort(), in1.ReadLong())
		{
		}

		public GUID(int d1, int d2, int d3, long d4)
		{
			_d1 = d1;
			_d2 = d2;
			_d3 = d3;
			_d4 = d4;
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(_d1);
			out1.WriteShort(_d2);
			out1.WriteShort(_d3);
			out1.WriteLong(_d4);
		}

		public override bool Equals(object obj)
		{
			GUID gUID = (GUID)obj;
			if (_d1 == gUID._d1 && _d2 == gUID._d2 && _d3 == gUID._d3)
			{
				return _d4 == gUID._d4;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _d1 ^ _d2 ^ _d3 ^ _d4.GetHashCode();
		}

		public string FormatAsString()
		{
			StringBuilder stringBuilder = new StringBuilder(36);
			int length = "0x".Length;
			stringBuilder.Append(HexDump.IntToHex(_d1), length, 8);
			stringBuilder.Append("-");
			stringBuilder.Append(HexDump.ShortToHex(_d2), length, 4);
			stringBuilder.Append("-");
			stringBuilder.Append(HexDump.ShortToHex(_d3), length, 4);
			stringBuilder.Append("-");
			char[] value = HexDump.LongToHex(D4);
			stringBuilder.Append(value, length, 4);
			stringBuilder.Append("-");
			stringBuilder.Append(value, length + 4, 12);
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(FormatAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		/// Read a GUID in standard text form e.g.<br />
		/// 13579BDF-0246-8ACE-0123-456789ABCDEF 
		/// <br /> -&gt; <br />
		///  0x13579BDF, 0x0246, 0x8ACE 0x0123456789ABCDEF
		public static GUID Parse(string rep)
		{
			char[] array = rep.ToCharArray();
			if (array.Length != 36)
			{
				throw new RecordFormatException("supplied text is the wrong length for a GUID");
			}
			int d = (ParseShort(array, 0) << 16) + ParseShort(array, 4);
			int d2 = ParseShort(array, 9);
			int d3 = ParseShort(array, 14);
			for (int num = 23; num > 19; num--)
			{
				array[num] = array[num - 1];
			}
			long d4 = ParseLELong(array, 20);
			return new GUID(d, d2, d3, d4);
		}

		private static long ParseLELong(char[] cc, int startIndex)
		{
			long num = 0L;
			for (int num2 = startIndex + 14; num2 >= startIndex; num2 -= 2)
			{
				num <<= 4;
				num += ParseHexChar(cc[num2]);
				num <<= 4;
				num += ParseHexChar(cc[num2 + 1]);
			}
			return num;
		}

		private static int ParseShort(char[] cc, int startIndex)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				num <<= 4;
				num += ParseHexChar(cc[startIndex + i]);
			}
			return num;
		}

		private static int ParseHexChar(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return c - 48;
			}
			if (c >= 'A' && c <= 'F')
			{
				return c - 65 + 10;
			}
			if (c >= 'a' && c <= 'f')
			{
				return c - 97 + 10;
			}
			throw new RecordFormatException("Bad hex char '" + c + "'");
		}
	}
}
