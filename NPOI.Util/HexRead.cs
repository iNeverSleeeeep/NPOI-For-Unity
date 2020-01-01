using System.Collections;
using System.IO;
using System.Text;

namespace NPOI.Util
{
	public class HexRead
	{
		/// <summary>
		/// This method reads hex data from a filename and returns a byte array.
		/// The file may contain line comments that are preceeded with a # symbol.
		/// </summary>
		/// <param name="filename">The filename to read</param>
		/// <returns>The bytes read from the file.</returns>
		/// <exception cref="T:System.IO.IOException">If there was a problem while reading the file.</exception>
		public static byte[] ReadData(string filename)
		{
			FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
			try
			{
				return ReadData(fileStream, -1);
			}
			finally
			{
				fileStream.Close();
			}
		}

		/// <summary>
		/// Same as ReadData(String) except that this method allows you to specify sections within
		/// a file.  Sections are referenced using section headers in the form:
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="section">The section.</param>
		/// <returns></returns>
		public static byte[] ReadData(Stream stream, string section)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				for (int num = stream.ReadByte(); num != -1; num = stream.ReadByte())
				{
					switch (num)
					{
					case 91:
						flag = true;
						break;
					case 10:
					case 13:
						flag = false;
						stringBuilder = new StringBuilder();
						break;
					case 93:
						flag = false;
						if (stringBuilder.ToString().Equals(section))
						{
							return ReadData(stream, 91);
						}
						stringBuilder = new StringBuilder();
						break;
					default:
						if (flag)
						{
							stringBuilder.Append((char)num);
						}
						break;
					}
				}
			}
			finally
			{
				stream.Close();
			}
			throw new IOException("Section '" + section + "' not found");
		}

		/// <summary>
		/// Reads the data.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="section">The section.</param>
		/// <returns></returns>
		public static byte[] ReadData(string filename, string section)
		{
			using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				return ReadData(stream, section);
			}
		}

		/// <summary>
		/// Reads the data.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="eofChar">The EOF char.</param>
		/// <returns></returns>
		public static byte[] ReadData(Stream stream, int eofChar)
		{
			int num = 0;
			byte b = 0;
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			while (!flag)
			{
				int num2 = stream.ReadByte();
				char c = 'a';
				if (num2 == eofChar)
				{
					break;
				}
				switch (num2)
				{
				case 35:
					ReadToEOL(stream);
					break;
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
					b = (byte)(b << 4);
					b = (byte)(b + (byte)(num2 - 48));
					num++;
					if (num == 2)
					{
						arrayList.Add(b);
						num = 0;
						b = 0;
					}
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
					c = 'A';
					b = (byte)(b << 4);
					b = (byte)(b + (byte)(num2 + 10 - c));
					num++;
					if (num == 2)
					{
						arrayList.Add(b);
						num = 0;
						b = 0;
					}
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
					b = (byte)(b << 4);
					b = (byte)(b + (byte)(num2 + 10 - c));
					num++;
					if (num == 2)
					{
						arrayList.Add(b);
						num = 0;
						b = 0;
					}
					break;
				case -1:
					flag = true;
					break;
				}
			}
			return (byte[])arrayList.ToArray(typeof(byte));
		}

		/// <summary>
		/// Reads from string.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static byte[] ReadFromString(string data)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			{
				return ReadData(stream, -1);
			}
		}

		/// <summary>
		/// Reads to EOL.
		/// </summary>
		/// <param name="stream">The stream.</param>
		private static void ReadToEOL(Stream stream)
		{
			int num = stream.ReadByte();
			while (num != -1 && num != 10 && num != 13)
			{
				num = stream.ReadByte();
			}
		}
	}
}
