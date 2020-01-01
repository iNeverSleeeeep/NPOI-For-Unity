using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.XSSF.Util
{
	/// This is a seriously sick fix for the fact that some .xlsx
	///  files contain raw bits of HTML, without being escaped
	///  or properly turned into XML.
	/// The result is that they contain things like &gt;br&lt;,
	///  which breaks the XML parsing.
	/// This very sick InputStream wrapper attempts to spot
	///  these go past, and fix them.
	/// Only works for UTF-8 and US-ASCII based streams!
	/// It should only be used where experience Shows the problem
	///  can occur...
	public class EvilUnclosedBRFixingInputStream : Stream
	{
		private Stream source;

		private byte[] spare;

		private static byte[] detect = new byte[4]
		{
			60,
			98,
			114,
			62
		};

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				return source.Length;
			}
		}

		public override long Position
		{
			get
			{
				return source.Position;
			}
			set
			{
				source.Position = value;
			}
		}

		public EvilUnclosedBRFixingInputStream(Stream source)
		{
			this.source = source;
		}

		/// Warning - doesn't fix!
		public int Read()
		{
			return source.ReadByte();
		}

		public override int Read(byte[] b, int off, int len)
		{
			int num = ReadFromSpare(b, off, len);
			int num2 = source.Read(b, off + num, len - num);
			int num3 = (num2 != -1 && num2 != 0) ? (num + num2) : num;
			if (num3 > 0)
			{
				num3 = fixUp(b, off, num3);
			}
			return num3;
		}

		public int Read(byte[] b)
		{
			return Read(b, 0, b.Length);
		}

		/// Reads into the buffer from the spare bytes
		private int ReadFromSpare(byte[] b, int offset, int len)
		{
			if (spare == null)
			{
				return 0;
			}
			if (len == 0)
			{
				throw new ArgumentException("Asked to read 0 bytes");
			}
			if (spare.Length <= len)
			{
				Array.Copy(spare, 0, b, offset, spare.Length);
				int result = spare.Length;
				spare = null;
				return result;
			}
			byte[] array = new byte[spare.Length - len];
			Array.Copy(spare, 0, b, offset, len);
			Array.Copy(spare, len, array, 0, array.Length);
			spare = array;
			return len;
		}

		private void AddToSpare(byte[] b, int offset, int len, bool atTheEnd)
		{
			if (spare == null)
			{
				spare = new byte[len];
				Array.Copy(b, offset, spare, 0, len);
			}
			else
			{
				byte[] destinationArray = new byte[spare.Length + len];
				if (atTheEnd)
				{
					Array.Copy(spare, 0, destinationArray, 0, spare.Length);
					Array.Copy(b, offset, destinationArray, spare.Length, len);
				}
				else
				{
					Array.Copy(b, offset, destinationArray, 0, len);
					Array.Copy(spare, 0, destinationArray, len, spare.Length);
				}
				spare = destinationArray;
			}
		}

		private int fixUp(byte[] b, int offset, int read)
		{
			for (int i = 0; i < detect.Length - 1; i++)
			{
				int num = offset + read - 1 - i;
				if (num >= 0)
				{
					bool flag = true;
					for (int j = 0; j <= i; j++)
					{
						if (!flag)
						{
							break;
						}
						if (b[num + j] != detect[j])
						{
							flag = false;
						}
					}
					if (flag)
					{
						AddToSpare(b, num, i + 1, true);
						read--;
						read -= i;
						break;
					}
				}
			}
			List<int> list = new List<int>();
			for (int k = offset; k <= offset + read - detect.Length; k++)
			{
				bool flag2 = true;
				for (int l = 0; l < detect.Length; l++)
				{
					if (!flag2)
					{
						break;
					}
					if (b[k + l] != detect[l])
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					list.Add(k);
				}
			}
			if (list.Count == 0)
			{
				return read;
			}
			int num2 = offset + read + list.Count;
			int num3 = num2 - b.Length;
			if (num3 > 0)
			{
				int num4 = 0;
				foreach (int item in list)
				{
					if (item > offset + read - detect.Length - num3 - num4)
					{
						num3 = num2 - item - 1 - num4;
						break;
					}
					num4++;
				}
				AddToSpare(b, offset + read - num3, num3, false);
				read -= num3;
			}
			for (int num5 = list.Count - 1; num5 >= 0; num5--)
			{
				int num6 = list[num5];
				if (num6 < read + offset && num6 <= read - 3)
				{
					byte[] array = new byte[read - num6 - 3];
					Array.Copy(b, num6 + 3, array, 0, array.Length);
					b[num6 + 3] = 47;
					Array.Copy(array, 0, b, num6 + 4, array.Length);
					read++;
				}
			}
			return read;
		}

		public override void Flush()
		{
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return source.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}
	}
}
