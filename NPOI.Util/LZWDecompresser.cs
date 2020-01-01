using System.IO;

namespace NPOI.Util
{
	/// This class provides common functionality for the
	///  various LZW implementations in the different file
	///  formats.
	/// It's currently used by HDGF and HMEF.
	///
	/// Two good resources on LZW are:
	///  http://en.wikipedia.org/wiki/LZW
	///  http://marknelson.us/1989/10/01/lzw-data-compression/
	public abstract class LZWDecompresser
	{
		/// Does the mask bit mean it's compressed or uncompressed?
		private bool maskMeansCompressed;

		/// How much to append to the code length in the stream
		///  to Get the real code length? Normally 2 or 3
		private int codeLengthIncrease;

		/// Does the 12 bits of the position Get stored in
		///  Little Endian or Big Endian form?
		/// This controls whether a pos+length of 0x12 0x34
		///  becomes a position of 0x123 or 0x312
		private bool positionIsBigEndian;

		protected LZWDecompresser(bool maskMeansCompressed, int codeLengthIncrease, bool positionIsBigEndian)
		{
			this.maskMeansCompressed = maskMeansCompressed;
			this.codeLengthIncrease = codeLengthIncrease;
			this.positionIsBigEndian = positionIsBigEndian;
		}

		/// Populates the dictionary, and returns where in it
		///  to begin writing new codes.
		/// Generally, if the dictionary is pre-populated, then new
		///  codes should be placed at the end of that block.
		/// Equally, if the dictionary is left with all zeros, then
		///  usually the new codes can go in at the start.
		protected abstract int populateDictionary(byte[] dict);

		/// Adjusts the position offset if needed when looking
		///  something up in the dictionary.
		protected abstract int adjustDictionaryOffset(int offset);

		/// Decompresses the given input stream, returning the array of bytes
		///  of the decompressed input.
		public byte[] decompress(Stream src)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				decompress(src, memoryStream);
				return memoryStream.ToArray();
			}
		}

		/// Perform a streaming decompression of the input.
		/// Works by:
		/// 1) Reading a flag byte, the 8 bits of which tell you if the
		///     following 8 codes are compressed our un-compressed
		/// 2) Consider the 8 bits in turn
		/// 3) If the bit is Set, the next code is un-compressed, so
		///     add it to the dictionary and output it
		/// 4) If the bit isn't Set, then read in the length and start
		///     position in the dictionary, and output the bytes there
		/// 5) Loop until we've done all 8 bits, then read in the next
		///     flag byte
		public void decompress(Stream src, Stream res)
		{
			byte[] array = new byte[4096];
			int num = populateDictionary(array);
			byte[] array2 = new byte[16 + codeLengthIncrease];
			int num2;
			while ((num2 = src.ReadByte()) != -1)
			{
				for (int num3 = 1; num3 < 256; num3 <<= 1)
				{
					bool flag = (num2 & num3) > 0;
					if (flag ^ maskMeansCompressed)
					{
						int b;
						if ((b = src.ReadByte()) != -1)
						{
							array[num & 0xFFF] = fromInt(b);
							num++;
							res.WriteByte(fromInt(b));
						}
					}
					else
					{
						int num4 = src.ReadByte();
						int num5 = src.ReadByte();
						if (num4 == -1 || num5 == -1)
						{
							break;
						}
						int num6 = (num5 & 0xF) + codeLengthIncrease;
						int offset = (!positionIsBigEndian) ? (num4 + ((num5 & 0xF0) << 4)) : ((num4 << 4) + (num5 >> 4));
						offset = adjustDictionaryOffset(offset);
						for (int i = 0; i < num6; i++)
						{
							array2[i] = array[(offset + i) & 0xFFF];
							array[(num + i) & 0xFFF] = array2[i];
						}
						res.Write(array2, 0, num6);
						num += num6;
					}
				}
			}
		}

		/// Given an integer, turn it into a java byte, handling
		///  the wrapping.
		/// This is a convenience method
		public static byte fromInt(int b)
		{
			if (b < 128)
			{
				return (byte)b;
			}
			return (byte)(b - 256);
		}

		/// Given a java byte, turn it into an integer between 0
		///  and 255 (i.e. handle the unwrapping).
		/// This is a convenience method
		public static int fromByte(byte b)
		{
			if (b >= 0)
			{
				return b;
			}
			return b + 256;
		}
	}
}
