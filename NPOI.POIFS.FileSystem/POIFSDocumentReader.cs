using System;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This class provides methods to read a DocumentEntry managed by a
	/// Filesystem instance.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	[Obsolete]
	public class POIFSDocumentReader : Stream
	{
		private const int _EOD = 0;

		private bool _closed;

		private int _current_offset;

		private POIFSDocument _document;

		private int _document_size;

		private byte[] _tiny_buffer;

		/// <summary>
		/// at the end Of document.
		/// </summary>
		/// <returns></returns>
		private bool EOD => _current_offset == _document_size;

		/// <summary>
		/// Returns the number of bytes that can be read (or skipped over)
		/// from this input stream without blocking by the next caller of a
		/// method for this input stream. The next caller might be the same
		/// thread or or another thread.
		/// </summary>
		/// <value>the number of bytes that can be read from this input
		/// stream without blocking.</value>
		public int Available
		{
			get
			{
				if (_closed)
				{
					throw new IOException("This stream is closed");
				}
				return (int)(Length - Position);
			}
		}

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports reading; otherwise, false.
		/// </returns>
		public override bool CanRead => true;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports seeking; otherwise, false.
		/// </returns>
		public override bool CanSeek => true;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports writing; otherwise, false.
		/// </returns>
		public override bool CanWrite => false;

		/// <summary>
		/// When overridden in a derived class, gets the length in bytes of the stream.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A long value representing the length of the stream in bytes.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// A class derived from Stream does not support seeking.
		/// </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		/// Methods were called after the stream was closed.
		/// </exception>
		public override long Length => _document_size;

		/// <summary>
		/// When overridden in a derived class, gets or sets the position within the current stream.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The current position within the stream.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		/// The stream does not support seeking.
		/// </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		/// Methods were called after the stream was closed.
		/// </exception>
		public override long Position
		{
			get
			{
				return _current_offset;
			}
			set
			{
				_current_offset = Convert.ToInt32(value);
			}
		}

		/// <summary>
		/// Create an InputStream from the specified DocumentEntry
		/// </summary>
		/// <param name="document">the DocumentEntry to be read</param>
		public POIFSDocumentReader(DocumentEntry document)
		{
			_current_offset = 0;
			_document_size = document.Size;
			_closed = false;
			_tiny_buffer = null;
			if (!(document is DocumentNode))
			{
				throw new IOException("Cannot open internal document storage");
			}
			_document = ((DocumentNode)document).Document;
		}

		/// <summary>
		/// Create an InputStream from the specified Document
		/// </summary>
		/// <param name="document">the Document to be read</param>
		public POIFSDocumentReader(POIFSDocument document)
		{
			_current_offset = 0;
			_document_size = document.Size;
			_closed = false;
			_tiny_buffer = null;
			_document = document;
		}

		/// <summary>
		/// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
		/// </summary>
		public override void Close()
		{
			_closed = true;
		}

		private void DieIfClosed()
		{
			if (_closed)
			{
				throw new IOException("cannot perform requested operation on a closed stream");
			}
		}

		public override void Flush()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads some number of bytes from the input stream and stores
		/// them into the buffer array b. The number of bytes actually read
		/// is returned as an integer. The definition of this method in
		/// java.io.InputStream allows this method to block, but it won't.
		/// If b is null, a NullPointerException is thrown. If the length
		/// of b is zero, then no bytes are read and 0 is returned;
		/// otherwise, there is an attempt to read at least one byte. If no
		/// byte is available because the stream is at end of file, the
		/// value -1 is returned; otherwise, at least one byte is read and
		/// stored into b.
		/// The first byte read is stored into element b[0], the next one
		/// into b[1], and so on. The number of bytes read is, at most,
		/// equal to the length of b. Let k be the number of bytes actually
		/// read; these bytes will be stored in elements b[0] through
		/// b[k-1], leaving elements b[k] through b[b.length-1] unaffected.
		/// If the first byte cannot be read for any reason other than end
		/// of file, then an IOException is thrown. In particular, an
		/// IOException is thrown if the input stream has been closed.
		/// The read(b) method for class InputStream has the same effect as:
		/// </summary>
		/// <param name="b">the buffer into which the data is read.</param>
		/// <returns>the total number of bytes read into the buffer, or -1
		/// if there is no more data because the end of the stream
		/// has been reached.</returns>
		public int Read(byte[] b)
		{
			return Read(b, 0, b.Length);
		}

		/// <summary>
		/// Reads up to len bytes of data from the input stream into an
		/// array of bytes. An attempt is made to read as many as len
		/// bytes, but a smaller number may be read, possibly zero. The
		/// number of bytes actually read is returned as an integer.
		/// The definition of this method in java.io.InputStream allows it
		/// to block, but it won't.
		/// If b is null, a NullPointerException is thrown.
		/// If off is negative, or len is negative, or off+len is greater
		/// than the length of the array b, then an
		/// IndexOutOfBoundsException is thrown.
		/// If len is zero, then no bytes are read and 0 is returned;
		/// otherwise, there is an attempt to read at least one byte. If no
		/// byte is available because the stream is at end of file, the
		/// value -1 is returned; otherwise, at least one byte is read and
		/// stored into b.
		/// The first byte read is stored into element b[off], the next one
		/// into b[off+1], and so on. The number of bytes read is, at most,
		/// equal to len. Let k be the number of bytes actually read; these
		/// bytes will be stored in elements b[off] through b[off+k-1],
		/// leaving elements b[off+k] through b[off+len-1] unaffected.
		/// In every case, elements b[0] through b[off] and elements
		/// b[off+len] through b[b.length-1] are unaffected.
		/// If the first byte cannot be read for any reason other than end
		/// of file, then an IOException is thrown. In particular, an
		/// IOException is thrown if the input stream has been closed.
		/// </summary>
		/// <param name="b">the buffer into which the data is read.</param>
		/// <param name="off">the start offset in array b at which the data is
		///            written.</param>
		/// <param name="len">the maximum number of bytes to read.</param>
		/// <returns>the total number of bytes read into the buffer, or -1
		///         if there is no more data because the end of the stream
		///         has been reached.</returns>
		public override int Read(byte[] b, int off, int len)
		{
			DieIfClosed();
			if (b == null)
			{
				throw new NullReferenceException("buffer is null");
			}
			if (off < 0 || len < 0 || b.Length < off + len)
			{
				throw new IndexOutOfRangeException("can't read past buffer boundaries");
			}
			if (len == 0)
			{
				return 0;
			}
			if (EOD)
			{
				return -1;
			}
			int num = Math.Min(Available, len);
			if (off == 0 && num == b.Length)
			{
				_document.Read(b, _current_offset);
			}
			else
			{
				byte[] array = new byte[num];
				_document.Read(array, _current_offset);
				Array.Copy(array, 0, b, off, num);
			}
			_current_offset += num;
			return num;
		}

		/// <summary>
		/// Reads the next byte of data from the input stream. The value
		/// byte is returned as an int in the range 0 to 255. If no byte is
		/// available because the end of the stream has been reached, the
		/// value -1 is returned. The definition of this method in
		/// java.io.InputStream allows this method to block, but it won't.        
		/// </summary>
		/// <returns>the next byte of data, or -1 if the end of the stream
		/// is reached.
		/// </returns>
		public override int ReadByte()
		{
			DieIfClosed();
			if (EOD)
			{
				return -1;
			}
			if (_tiny_buffer == null)
			{
				_tiny_buffer = new byte[1];
			}
			_document.Read(_tiny_buffer, _current_offset++);
			return _tiny_buffer[0] & 0xFF;
		}

		/// <summary>
		/// When overridden in a derived class, sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		/// The stream does not support seeking, such as if the stream is constructed from a pipe or console output.
		/// </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		/// Methods were called after the stream was closed.
		/// </exception>
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

		/// <summary>
		/// Skips the specified n.
		/// </summary>
		/// <param name="n">The n.</param>
		/// <returns></returns>
		public long Skip(long n)
		{
			DieIfClosed();
			if (n < 0)
			{
				return 0L;
			}
			int num = _current_offset + (int)n;
			if (num < _current_offset)
			{
				num = _document_size;
			}
			else if (num > _document_size)
			{
				num = _document_size;
			}
			long result = num - _current_offset;
			_current_offset = num;
			return result;
		}

		/// <summary>
		/// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="buffer" /> is null.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="offset" /> or <paramref name="count" /> is negative.
		/// </exception>
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		/// The stream does not support writing.
		/// </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		/// Methods were called after the stream was closed.
		/// </exception>
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}
}
