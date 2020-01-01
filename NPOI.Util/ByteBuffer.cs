using System;

namespace NPOI.Util
{
	public class ByteBuffer
	{
		private byte[] buffer;

		private int mark = -1;

		private int position;

		private int limit;

		private int capacity;

		private int offset;

		public int Position
		{
			get
			{
				return position;
			}
			set
			{
				if (value < 0 || value > limit)
				{
					throw new ArgumentException();
				}
				position = value;
				if (mark > position)
				{
					mark = -1;
				}
			}
		}

		public int Limit
		{
			get
			{
				return limit;
			}
			set
			{
				if (value > capacity || value < 0)
				{
					throw new ArgumentException();
				}
				limit = value;
				if (position > limit)
				{
					position = limit;
				}
				if (mark > limit)
				{
					mark = -1;
				}
			}
		}

		public byte this[int index]
		{
			get
			{
				return buffer[Index(CheckIndex(index))];
			}
			set
			{
				if (index < 0 || index >= limit)
				{
					throw new IndexOutOfRangeException();
				}
				buffer[Index(index)] = value;
			}
		}

		public int Remain => limit - position;

		public byte[] Buffer => buffer;

		public int Offset => offset;

		public bool HasBuffer => true;

		public int Length => capacity;

		private ByteBuffer(int mark, int pos, int lim, int cap, byte[] buffer, int offset)
		{
			if (cap < 0)
			{
				throw new ArgumentException();
			}
			capacity = cap;
			Limit = lim;
			Position = pos;
			if (mark >= 0)
			{
				if (mark > pos)
				{
					throw new ArgumentException();
				}
				this.mark = mark;
			}
			this.buffer = buffer;
			this.offset = offset;
		}

		public ByteBuffer(byte[] buffer, int off, int len)
			: this(-1, off, off + len, buffer.Length, buffer, 0)
		{
		}

		public ByteBuffer(int capacity, int limit)
			: this(-1, 0, limit, capacity, new byte[capacity], 0)
		{
		}

		protected ByteBuffer(byte[] buffer, int mark, int pos, int lim, int cap, int off)
			: this(mark, pos, lim, cap, buffer, off)
		{
		}

		/// <summary>
		/// Returns the number of elements between the current position and the limit.
		/// </summary>
		/// <returns>The number of elements remaining in this buffer</returns>
		public int Remaining()
		{
			return limit - position;
		}

		/// <summary>
		/// Tells whether there are any elements between the current position and the limit.
		/// </summary>
		/// <returns>true if, and only if, there is at least one element remaining in this buffer</returns>
		public bool HasRemaining()
		{
			return position < limit;
		}

		public static ByteBuffer CreateBuffer(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentException();
			}
			return new ByteBuffer(capacity, capacity);
		}

		public static ByteBuffer CreateBuffer(byte[] buffer, int offset, int length)
		{
			try
			{
				return new ByteBuffer(buffer, offset, length);
			}
			catch (ArgumentException)
			{
				throw new IndexOutOfRangeException();
			}
		}

		public static ByteBuffer CreateBuffer(byte[] buffer)
		{
			return CreateBuffer(buffer, 0, buffer.Length);
		}

		public ByteBuffer Slice()
		{
			return new ByteBuffer(buffer, -1, 0, Remain, Remain, Position + offset);
		}

		public ByteBuffer Duplicate()
		{
			return null;
		}

		protected int NextGetIndex()
		{
			if (position >= limit)
			{
				throw new IndexOutOfRangeException();
			}
			return position++;
		}

		protected int NextGetIndex(int nb)
		{
			if (limit - position < nb)
			{
				throw new IndexOutOfRangeException();
			}
			int result = position;
			position += nb;
			return result;
		}

		protected int NextPutIndex()
		{
			return NextGetIndex();
		}

		protected int NextPutIndex(int nb)
		{
			return NextGetIndex(nb);
		}

		protected int Index(int i)
		{
			return i + offset;
		}

		protected int CheckIndex(int i)
		{
			if (i < 0 || i >= limit)
			{
				throw new IndexOutOfRangeException();
			}
			return i;
		}

		protected int CheckIndex(int i, int nb)
		{
			if (i < 0 || nb > limit - i)
			{
				throw new IndexOutOfRangeException();
			}
			return i;
		}

		public byte Read()
		{
			return buffer[Index(NextGetIndex())];
		}

		protected void CheckBounds(int off, int len, int size)
		{
			if ((off | len | (off + len) | (size - (off + len))) < 0)
			{
				throw new IndexOutOfRangeException();
			}
		}

		public ByteBuffer Read(byte[] buf)
		{
			return Read(buf, 0, buf.Length);
		}

		public ByteBuffer Read(byte[] dst, int offset, int length)
		{
			CheckBounds(offset, length, dst.Length);
			if (length > Remain)
			{
				throw new ArgumentException();
			}
			Array.Copy(buffer, Index(position), dst, offset, length);
			Position += length;
			return this;
		}

		public ByteBuffer Write(byte x)
		{
			buffer[Index(NextPutIndex())] = x;
			return this;
		}

		public ByteBuffer Write(byte[] src, int offset, int length)
		{
			CheckBounds(offset, length, src.Length);
			if (length > Remain)
			{
				throw new IndexOutOfRangeException();
			}
			Array.Copy(src, offset, buffer, Index(Position), length);
			Position += length;
			return this;
		}

		public ByteBuffer Write(ByteBuffer src)
		{
			if (src == this)
			{
				throw new ArgumentException();
			}
			int remain = src.Remain;
			if (remain > Remain)
			{
				throw new IndexOutOfRangeException();
			}
			for (int i = 0; i < remain; i++)
			{
				Write(src.Read());
			}
			return this;
		}

		public ByteBuffer Write(byte[] data)
		{
			return Write(data, 0, data.Length);
		}
	}
}
