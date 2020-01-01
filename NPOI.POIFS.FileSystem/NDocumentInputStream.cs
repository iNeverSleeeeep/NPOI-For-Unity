using NPOI.POIFS.Properties;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// This class provides methods to read a DocumentEntry managed by a
	/// {@link NPOIFSFileSystem} instance.
	public class NDocumentInputStream : DocumentInputStream
	{
		/// current offset into the Document 
		private int _current_offset;

		/// current block count 
		private int _current_block_count;

		/// current marked offset into the Document (used by mark and Reset) 
		private int _marked_offset;

		/// and the block count for it 
		private int _marked_offset_count;

		/// the Document's size 
		private int _document_size;

		/// have we been closed? 
		private bool _closed;

		/// the actual Document 
		private NPOIFSDocument _document;

		private IEnumerator<ByteBuffer> _data;

		private ByteBuffer _buffer;

		public override long Length
		{
			get
			{
				if (_closed)
				{
					throw new InvalidOperationException("cannot perform requested operation on a closed stream");
				}
				return _document_size;
			}
		}

		public override long Position
		{
			get
			{
				if (_closed)
				{
					throw new InvalidOperationException("cannot perform requested operation on a closed stream");
				}
				return _current_offset;
			}
			set
			{
				_current_offset = (int)value;
			}
		}

		/// Create an InputStream from the specified DocumentEntry
		///
		/// @param document the DocumentEntry to be read
		///
		/// @exception IOException if the DocumentEntry cannot be opened (like, maybe it has
		///                been deleted?)
		public NDocumentInputStream(DocumentEntry document)
		{
			if (!(document is DocumentNode))
			{
				throw new IOException("Cannot open internal document storage, " + document + " not a Document Node");
			}
			_current_offset = 0;
			_current_block_count = 0;
			_marked_offset = 0;
			_marked_offset_count = 0;
			_document_size = document.Size;
			_closed = false;
			DocumentNode documentNode = (DocumentNode)document;
			DocumentProperty property = (DocumentProperty)documentNode.Property;
			_document = new NPOIFSDocument(property, ((DirectoryNode)documentNode.Parent).NFileSystem);
			_data = _document.GetBlockIterator();
		}

		/// Create an InputStream from the specified Document
		///
		/// @param document the Document to be read
		public NDocumentInputStream(NPOIFSDocument document)
		{
			_current_offset = 0;
			_current_block_count = 0;
			_marked_offset = 0;
			_marked_offset_count = 0;
			_document_size = document.Size;
			_closed = false;
			_document = document;
			_data = _document.GetBlockIterator();
		}

		public override int Available()
		{
			if (_closed)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return _document_size - _current_offset;
		}

		public override void Close()
		{
			_closed = true;
		}

		public override void Mark(int ignoredReadlimit)
		{
			_marked_offset = _current_offset;
			_marked_offset_count = Math.Max(0, _current_block_count - 1);
		}

		public override int Read()
		{
			DieIfClosed();
			if (atEOD())
			{
				return DocumentInputStream.EOF;
			}
			byte[] array = new byte[1];
			int num = Read(array, 0, 1);
			if (num >= 0)
			{
				if (array[0] < 0)
				{
					return array[0] + 256;
				}
				return array[0];
			}
			return num;
		}

		public override int Read(byte[] b, int off, int len)
		{
			DieIfClosed();
			if (b == null)
			{
				throw new ArgumentException("buffer must not be null");
			}
			if (off < 0 || len < 0 || b.Length < off + len)
			{
				throw new IndexOutOfRangeException("can't read past buffer boundaries");
			}
			if (len == 0)
			{
				return 0;
			}
			if (atEOD())
			{
				return DocumentInputStream.EOF;
			}
			int num = Math.Min(Available(), len);
			ReadFully(b, off, num);
			return num;
		}

		/// Repositions this stream to the position at the time the mark() method was
		/// last called on this input stream. If mark() has not been called this
		/// method repositions the stream to its beginning.
		public override void Reset()
		{
			if (_marked_offset == 0 && _marked_offset_count == 0)
			{
				_current_block_count = _marked_offset_count;
				_current_offset = _marked_offset;
				_data = _document.GetBlockIterator();
				_buffer = null;
			}
			else
			{
				_data = _document.GetBlockIterator();
				_current_offset = 0;
				for (int i = 0; i < _marked_offset_count; i++)
				{
					_data.MoveNext();
					_buffer = _data.Current;
					_current_offset += _buffer.Remain;
				}
				_current_block_count = _marked_offset_count;
				if (_current_offset != _marked_offset)
				{
					_data.MoveNext();
					_buffer = _data.Current;
					_current_block_count++;
					int num = _marked_offset - _current_offset;
					_buffer.Position += num;
				}
				_current_offset = _marked_offset;
			}
		}

		public override long Skip(long n)
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
			long num2 = num - _current_offset;
			byte[] buf = new byte[(int)num2];
			ReadFully(buf);
			return num2;
		}

		private void DieIfClosed()
		{
			if (_closed)
			{
				throw new IOException("cannot perform requested operation on a closed stream");
			}
		}

		private bool atEOD()
		{
			return _current_offset == _document_size;
		}

		private void CheckAvaliable(int requestedSize)
		{
			if (_closed)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			if (requestedSize > _document_size - _current_offset)
			{
				throw new Exception("Buffer underrun - requested " + requestedSize + " bytes but " + (_document_size - _current_offset) + " was available");
			}
		}

		public override void ReadFully(byte[] buf, int off, int len)
		{
			CheckAvaliable(len);
			int num;
			for (int i = 0; i < len; i += num)
			{
				if (_buffer == null || _buffer.Remain == 0)
				{
					_current_block_count++;
					_data.MoveNext();
					_buffer = _data.Current;
				}
				num = Math.Min(len - i, _buffer.Remain);
				_buffer.Read(buf, off + i, num);
				_current_offset += num;
			}
		}

		public override int ReadByte()
		{
			return ReadUByte();
		}

		public override double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(ReadLong());
		}

		public override long ReadLong()
		{
			CheckAvaliable(DocumentInputStream.SIZE_LONG);
			byte[] array = new byte[DocumentInputStream.SIZE_LONG];
			ReadFully(array, 0, DocumentInputStream.SIZE_LONG);
			return LittleEndian.GetLong(array, 0);
		}

		public override void ReadFully(byte[] buf)
		{
			ReadFully(buf, 0, buf.Length);
		}

		public override short ReadShort()
		{
			CheckAvaliable(DocumentInputStream.SIZE_SHORT);
			byte[] array = new byte[DocumentInputStream.SIZE_SHORT];
			ReadFully(array, 0, DocumentInputStream.SIZE_SHORT);
			return LittleEndian.GetShort(array);
		}

		public override int ReadInt()
		{
			CheckAvaliable(DocumentInputStream.SIZE_INT);
			byte[] array = new byte[DocumentInputStream.SIZE_INT];
			ReadFully(array, 0, DocumentInputStream.SIZE_INT);
			return LittleEndian.GetInt(array);
		}

		public override int ReadUShort()
		{
			CheckAvaliable(DocumentInputStream.SIZE_SHORT);
			byte[] array = new byte[DocumentInputStream.SIZE_SHORT];
			ReadFully(array, 0, DocumentInputStream.SIZE_SHORT);
			return LittleEndian.GetUShort(array);
		}

		public override int ReadUByte()
		{
			CheckAvaliable(1);
			byte[] array = new byte[1];
			ReadFully(array, 0, 1);
			if (array[0] >= 0)
			{
				return array[0];
			}
			return array[0] + 256;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (offset == 0)
			{
				Reset();
			}
			else
			{
				Mark((int)offset);
			}
			return 0L;
		}
	}
}
