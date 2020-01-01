using System;
using System.IO;

namespace NPOI.Util
{
	public class ByteArrayInputStream : Stream
	{
		protected byte[] buf;

		protected int pos;

		protected int mark;

		protected int count;

		public override bool CanRead => true;

		public override bool CanWrite => false;

		public override bool CanSeek => true;

		public override long Length => count;

		public override long Position
		{
			get
			{
				return pos;
			}
			set
			{
				pos = (int)value;
			}
		}

		public ByteArrayInputStream()
		{
		}

		public ByteArrayInputStream(byte[] buf)
		{
			this.buf = buf;
			pos = 0;
			count = buf.Length;
		}

		public ByteArrayInputStream(byte[] buf, int offset, int length)
		{
			this.buf = buf;
			pos = offset;
			count = Math.Min(offset + length, buf.Length);
			mark = offset;
		}

		public virtual int Read()
		{
			lock (this)
			{
				return (pos < count) ? (buf[pos++] & 0xFF) : (-1);
			}
		}

		public override int Read(byte[] b, int off, int len)
		{
			lock (this)
			{
				if (b == null)
				{
					throw new NullReferenceException();
				}
				if (off < 0 || len < 0 || len > b.Length - off)
				{
					throw new IndexOutOfRangeException();
				}
				if (pos >= count)
				{
					return -1;
				}
				int num = count - pos;
				if (len > num)
				{
					len = num;
				}
				if (len <= 0)
				{
					return 0;
				}
				Array.Copy(buf, pos, b, off, len);
				pos += len;
				return len;
			}
		}

		public virtual int Available()
		{
			return count - pos;
		}

		public virtual bool MarkSupported()
		{
			return true;
		}

		public virtual void Mark(int readAheadLimit)
		{
			mark = pos;
		}

		public virtual void Reset()
		{
			pos = mark;
		}

		public override void Close()
		{
		}

		public override void Flush()
		{
			throw new NotImplementedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!CanSeek)
			{
				throw new NotSupportedException();
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (0 > offset)
				{
					throw new ArgumentOutOfRangeException("offset", "offset must be positive");
				}
				Position = ((offset < Length) ? offset : Length);
				break;
			case SeekOrigin.Current:
				Position = ((Position + offset < Length) ? (Position + offset) : Length);
				break;
			case SeekOrigin.End:
				Position = Length;
				break;
			default:
				throw new ArgumentException("incorrect SeekOrigin", "origin");
			}
			return Position;
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}
}
