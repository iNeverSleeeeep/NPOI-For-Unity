using System.IO;
using System.Text;

namespace NPOI.Util
{
	/// <summary>
	/// CRC Verification
	/// </summary>
	public class CRC32
	{
		protected ulong[] crc32Table;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.CRC32" /> class.
		/// </summary>
		public CRC32()
		{
			crc32Table = new ulong[256];
			for (int i = 0; i < 256; i++)
			{
				ulong num = (ulong)i;
				for (int num2 = 8; num2 > 0; num2--)
				{
					num = (((num & 1) != 1) ? (num >> 1) : ((num >> 1) ^ 3988292384u));
				}
				crc32Table[i] = num;
			}
		}

		/// <summary>
		///  CRC Bytes.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		public ulong ByteCRC(ref byte[] buffer)
		{
			ulong num = 4294967295uL;
			ulong num2 = (ulong)buffer.Length;
			for (ulong num3 = 0uL; num3 < num2; num3++)
			{
				ulong num4 = num & 0xFF;
				num4 ^= buffer[num3];
				num >>= 8;
				num ^= crc32Table[num4];
			}
			return num ^ uint.MaxValue;
		}

		/// <summary>
		/// String CRC
		/// </summary>
		/// <param name="sInputString">the string</param>
		/// <returns></returns>
		public ulong StringCRC(string sInputString)
		{
			byte[] buffer = Encoding.Default.GetBytes(sInputString);
			return ByteCRC(ref buffer);
		}

		/// <summary>
		/// File CRC
		/// </summary>
		/// <param name="sInputFilename">the input file</param>
		/// <returns></returns>
		public long FileCRC(string sInputFilename)
		{
			using (FileStream fileStream = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
			{
				byte[] buffer = new byte[fileStream.Length];
				fileStream.Read(buffer, 0, buffer.Length);
				return (long)ByteCRC(ref buffer);
			}
		}

		/// <summary>
		/// Stream CRC
		/// </summary>
		/// <param name="inFile">the input stream</param>
		/// <returns></returns>
		public long StreamCRC(Stream inFile)
		{
			try
			{
				byte[] buffer = new byte[inFile.Length];
				inFile.Read(buffer, 0, buffer.Length);
				inFile.Close();
				return (long)ByteCRC(ref buffer);
			}
			catch (IOException)
			{
				throw;
			}
		}
	}
}
