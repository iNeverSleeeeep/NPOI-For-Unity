using System;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This class provides a wrapper over an OutputStream so that Document
	/// writers can't accidently go over their size limits
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	[Obsolete]
	public class POIFSDocumentWriter : Stream
	{
		private int limit;

		private Stream stream;

		private int written;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports reading; otherwise, false.
		/// </returns>
		public override bool CanRead => false;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports seeking; otherwise, false.
		/// </returns>
		public override bool CanSeek => false;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
		/// </summary>
		/// <value></value>
		/// <returns>true if the stream supports writing; otherwise, false.
		/// </returns>
		public override bool CanWrite => true;

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
		public override long Length => stream.Length;

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
				return stream.Position;
			}
			set
			{
				stream.Position = value;
			}
		}

		/// <summary>
		/// Create a POIFSDocumentWriter
		/// </summary>
		/// <param name="stream">the OutputStream to which the data is actually</param>
		/// <param name="limit">the maximum number of bytes that can be written</param>
		public POIFSDocumentWriter(Stream stream, int limit)
		{
			this.stream = stream;
			this.limit = limit;
			written = 0;
		}

		/// <summary>
		/// Closes this output stream and releases any system resources
		/// associated with this stream. The general contract of close is
		/// that it closes the output stream. A closed stream cannot
		/// perform output operations and cannot be reopened.
		/// </summary>
		public override void Close()
		{
			stream.Close();
		}

		/// <summary>
		/// Flushes this output stream and forces any buffered output bytes
		/// to be written out.
		/// </summary>
		public override void Flush()
		{
			stream.Flush();
		}

		private void LimitCheck(int toBeWritten)
		{
			if (written + toBeWritten > limit)
			{
				throw new IOException("tried to write too much data");
			}
			written += toBeWritten;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public void Write(int b)
		{
			LimitCheck(1);
			stream.WriteByte((byte)b);
		}

		/// <summary>
		/// Writes b.length bytes from the specified byte array
		/// to this output stream.
		/// </summary>
		/// <param name="b">the data.</param>
		public void Write(byte[] b)
		{
			Write(b, 0, b.Length);
		}

		/// <summary>
		/// Writes len bytes from the specified byte array starting at
		/// offset off to this output stream.  The general contract for
		/// write(b, off, len) is that some of the bytes in the array b are
		/// written to the output stream in order; element b[off] is the
		/// first byte written and b[off+len-1] is the last byte written by
		/// this operation.
		/// If b is null, a NullPointerException is thrown.
		/// If off is negative, or len is negative, or off+len is greater
		/// than the length of the array b, then an
		/// IndexOutOfBoundsException is thrown.
		/// </summary>
		/// <param name="b">the data.</param>
		/// <param name="off">the start offset in the data.</param>
		/// <param name="len">the number of bytes to write.</param>
		public override void Write(byte[] b, int off, int len)
		{
			LimitCheck(len);
			stream.Write(b, off, len);
		}

		/// <summary>
		/// Writes the specified byte to this output stream. The general
		/// contract for write is that one byte is written to the output
		/// stream. The byte to be written is the eight low-order bits of
		/// the argument b. The 24 high-order bits of b are ignored.
		/// </summary>
		/// <param name="b">the byte.</param>
		public override void WriteByte(byte b)
		{
			LimitCheck(1);
			stream.WriteByte(b);
		}

		/// <summary>
		/// write the rest of the document's data (fill in at the end)
		/// </summary>
		/// <param name="totalLimit">the actual number of bytes the corresponding         
		/// document must fill</param>
		/// <param name="fill">the byte to fill remaining space with</param>
		public virtual void WriteFiller(int totalLimit, byte fill)
		{
			if (totalLimit > written)
			{
				byte[] array = new byte[totalLimit - written];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = fill;
				}
				stream.Write(array, 0, array.Length);
			}
		}
	}
}
