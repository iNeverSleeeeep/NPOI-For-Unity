using NPOI.Util;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// This class provides a wrapper over an OutputStream so that Document
	/// Writers can't accidently go over their size limits
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	public class DocumentOutputStream : MemoryStream
	{
		private Stream _stream;

		private int _limit;

		private int _written;

		/// Create a DocumentOutputStream
		///
		/// @param stream the OutputStream to which the data is actually
		///               read
		/// @param limit the maximum number of bytes that can be written
		public DocumentOutputStream(Stream stream, int limit)
		{
			_stream = stream;
			_limit = limit;
			_written = 0;
		}

		/// Writes the specified byte to this output stream. The general
		/// contract for write is that one byte is written to the output
		/// stream. The byte to be written is the eight low-order bits of
		/// the argument b. The 24 high-order bits of b are ignored.
		///
		/// @param b the byte.
		/// @exception IOException if an I/O error occurs. In particular,
		///                        an IOException may be thrown if the
		///                        output stream has been closed, or if the
		///                        Writer tries to write too much data.
		public void Write(int b)
		{
			LimitCheck(1);
			_stream.WriteByte((byte)b);
		}

		/// Writes b.Length bytes from the specified byte array
		/// to this output stream.
		///
		/// @param b the data.
		/// @exception IOException if an I/O error occurs.
		public void Write(byte[] b)
		{
			Write(b, 0, b.Length);
		}

		/// <summary>
		///  Writes len bytes from the specified byte array starting at
		/// offset off to this output stream.  The general contract for
		/// Write(b, off, len) is that some of the bytes in the array b are
		/// written to the output stream in order; element b[off] is the
		/// first byte written and b[off+len-1] is the last byte written by
		/// this operation.
		/// </summary>
		/// <param name="b">the data.</param>
		/// <param name="off">the start offset in the data.</param>
		/// <param name="len">the number of bytes to Write.</param>
		public override void Write(byte[] b, int off, int len)
		{
			LimitCheck(len);
			_stream.Write(b, off, len);
		}

		/// <summary>
		/// Flushes this output stream and forces any buffered output bytes to be written out
		/// </summary>
		public override void Flush()
		{
			_stream.Flush();
		}

		/// Closes this output stream and releases any system resources
		/// associated with this stream. The general contract of close is
		/// that it closes the output stream. A closed stream cannot
		/// perform output operations and cannot be reopened.
		///
		/// @exception IOException if an I/O error occurs.
		public override void Close()
		{
		}

		/// write the rest of the document's data (fill in at the end)
		///
		/// @param totalLimit the actual number of bytes the corresponding
		///                   document must fill
		/// @param fill the byte to fill remaining space with
		///
		/// @exception IOException on I/O error
		public void WriteFiller(int totalLimit, byte Fill)
		{
			if (totalLimit > _written)
			{
				byte[] array = new byte[totalLimit - _written];
				Arrays.Fill(array, Fill);
				_stream.Write(array, 0, array.Length);
			}
		}

		private void LimitCheck(int toBeWritten)
		{
			if (_written + toBeWritten > _limit)
			{
				throw new IOException("tried to write too much data");
			}
			_written += toBeWritten;
		}
	}
}
