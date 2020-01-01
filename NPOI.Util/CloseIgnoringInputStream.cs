using System.IO;

namespace NPOI.Util
{
	internal class CloseIgnoringInputStream : Stream
	{
		private Stream _is;

		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => false;

		public override long Length => _is.Length;

		public override long Position
		{
			get
			{
				return _is.Position;
			}
			set
			{
				_is.Position = value;
			}
		}

		public CloseIgnoringInputStream(Stream stream)
		{
			_is = stream;
		}

		public int Read()
		{
			return _is.ReadByte();
		}

		public override int Read(byte[] b, int off, int len)
		{
			return _is.Read(b, off, len);
		}

		public override void Close()
		{
		}

		public override void Flush()
		{
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
		}
	}
}
