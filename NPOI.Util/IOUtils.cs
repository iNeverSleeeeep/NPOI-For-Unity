using System.IO;

namespace NPOI.Util
{
	public class IOUtils
	{
		/// <summary>
		/// Reads all the data from the input stream, and returns
		/// the bytes Read.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		/// <remarks>Tony Qu changed the code</remarks>
		public static byte[] ToByteArray(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			return array;
		}

		public static byte[] ToByteArray(ByteBuffer buffer, int length)
		{
			if (buffer.HasBuffer && buffer.Offset == 0)
			{
				return buffer.Buffer;
			}
			byte[] array = new byte[length];
			buffer.Read(array);
			return array;
		}

		/// <summary>
		/// Reads the fully.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public static int ReadFully(Stream stream, byte[] b)
		{
			return ReadFully(stream, b, 0, b.Length);
		}

		/// <summary>
		/// Same as the normal 
		/// <c>in.Read(b, off, len)</c>
		/// , but tries to ensure that the entire len number of bytes is Read.
		/// If the end of file is reached before any bytes are Read, returns -1.
		/// If the end of the file is reached after some bytes are
		/// Read, returns the number of bytes Read.
		/// If the end of the file isn't reached before len
		/// bytes have been Read, will return len bytes.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="b">The b.</param>
		/// <param name="off">The off.</param>
		/// <param name="len">The len.</param>
		/// <returns></returns>
		public static int ReadFully(Stream stream, byte[] b, int off, int len)
		{
			int num = 0;
			do
			{
				int num2 = stream.Read(b, off + num, len - num - off);
				num += num2;
				if (stream.Position == stream.Length)
				{
					return num;
				}
			}
			while (num != len);
			return num;
		}

		/// <summary>
		/// Copies all the data from the given InputStream to the OutputStream. It
		/// leaves both streams open, so you will still need to close them once done.
		/// </summary>
		/// <param name="inp"></param>
		/// <param name="out1"></param>
		public static void Copy(Stream inp, Stream out1)
		{
			byte[] array = new byte[4096];
			inp.Position = 0L;
			int num;
			while ((num = inp.Read(array, 0, array.Length)) != -1)
			{
				if (num > 0)
				{
					out1.Write(array, 0, num);
				}
			}
		}

		public static long CalculateChecksum(byte[] data)
		{
			CRC32 cRC = new CRC32();
			return (long)cRC.ByteCRC(ref data);
		}
	}
}
