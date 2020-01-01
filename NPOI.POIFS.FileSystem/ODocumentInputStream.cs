using NPOI.POIFS.Storage;
using System;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// This class provides methods to read a DocumentEntry managed by a
	/// {@link POIFSFileSystem} instance.
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	public class ODocumentInputStream : DocumentInputStream
	{
		/// current offset into the Document 
		private long _current_offset;

		/// current marked offset into the Document (used by mark and Reset) 
		private long _marked_offset;

		/// the Document's size 
		private int _document_size;

		/// have we been closed? 
		private bool _closed;

		/// the actual Document 
		private POIFSDocument _document;

		/// the data block Containing the current stream pointer 
		private DataInputBlock _currentBlock;

		public override long Length => _document_size;

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
		public ODocumentInputStream(DocumentEntry document)
		{
			if (!(document is DocumentNode))
			{
				throw new IOException("Cannot open internal document storage");
			}
			DocumentNode documentNode = (DocumentNode)document;
			if (documentNode.Document == null)
			{
				throw new IOException("Cannot open internal document storage");
			}
			_current_offset = 0L;
			_marked_offset = 0L;
			_document_size = document.Size;
			_closed = false;
			_document = documentNode.Document;
			_currentBlock = GetDataInputBlock(0L);
		}

		/// Create an InputStream from the specified Document
		///
		/// @param document the Document to be read
		public ODocumentInputStream(POIFSDocument document)
		{
			_current_offset = 0L;
			_marked_offset = 0L;
			_document_size = document.Size;
			_closed = false;
			_document = document;
			_currentBlock = GetDataInputBlock(0L);
		}

		public override int Available()
		{
			if (_closed)
			{
				throw new InvalidOperationException("cannot perform requested operation on a closed stream");
			}
			return _document_size - (int)_current_offset;
		}

		public override void Close()
		{
			_closed = true;
		}

		public override void Mark(int ignoredReadlimit)
		{
			_marked_offset = _current_offset;
		}

		private DataInputBlock GetDataInputBlock(long offset)
		{
			return _document.GetDataInputBlock((int)offset);
		}

		public override int Read()
		{
			dieIfClosed();
			if (atEOD())
			{
				return DocumentInputStream.EOF;
			}
			int result = _currentBlock.ReadUByte();
			_current_offset++;
			if (_currentBlock.Available() < 1)
			{
				_currentBlock = GetDataInputBlock(_current_offset);
			}
			return result;
		}

		public override int Read(byte[] b, int off, int len)
		{
			dieIfClosed();
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
			_current_offset = _marked_offset;
			_currentBlock = GetDataInputBlock(_current_offset);
		}

		public override long Skip(long n)
		{
			dieIfClosed();
			if (n < 0)
			{
				return 0L;
			}
			long num = _current_offset + (int)n;
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
			_currentBlock = GetDataInputBlock(_current_offset);
			return result;
		}

		private void dieIfClosed()
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

		public override int ReadByte()
		{
			return ReadUByte();
		}

		public override double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(ReadLong());
		}

		public override short ReadShort()
		{
			return (short)ReadUShort();
		}

		public override void ReadFully(byte[] buf, int off, int len)
		{
			CheckAvaliable(len);
			int num = _currentBlock.Available();
			if (num > len)
			{
				_currentBlock.ReadFully(buf, off, len);
				_current_offset += len;
			}
			else
			{
				int num2 = len;
				int num3 = off;
				while (true)
				{
					if (num2 <= 0)
					{
						return;
					}
					bool flag = num2 >= num;
					int num4 = (!flag) ? num2 : num;
					_currentBlock.ReadFully(buf, num3, num4);
					num2 -= num4;
					num3 += num4;
					_current_offset += num4;
					if (flag)
					{
						if (_current_offset == _document_size)
						{
							break;
						}
						_currentBlock = GetDataInputBlock(_current_offset);
						num = _currentBlock.Available();
					}
				}
				if (num2 > 0)
				{
					throw new InvalidOperationException("reached end of document stream unexpectedly");
				}
				_currentBlock = null;
			}
		}

		public override long ReadLong()
		{
			CheckAvaliable(DocumentInputStream.SIZE_LONG);
			int num = _currentBlock.Available();
			long result;
			if (num > DocumentInputStream.SIZE_LONG)
			{
				result = _currentBlock.ReadLongLE();
			}
			else
			{
				DataInputBlock dataInputBlock = GetDataInputBlock(_current_offset + num);
				result = ((num != DocumentInputStream.SIZE_LONG) ? dataInputBlock.ReadLongLE(_currentBlock, num) : _currentBlock.ReadLongLE());
				_currentBlock = dataInputBlock;
			}
			_current_offset += DocumentInputStream.SIZE_LONG;
			return result;
		}

		public override int ReadInt()
		{
			CheckAvaliable(DocumentInputStream.SIZE_INT);
			int num = _currentBlock.Available();
			int result;
			if (num > DocumentInputStream.SIZE_INT)
			{
				result = _currentBlock.ReadIntLE();
			}
			else
			{
				DataInputBlock dataInputBlock = GetDataInputBlock(_current_offset + num);
				result = ((num != DocumentInputStream.SIZE_INT) ? dataInputBlock.ReadIntLE(_currentBlock, num) : _currentBlock.ReadIntLE());
				_currentBlock = dataInputBlock;
			}
			_current_offset += DocumentInputStream.SIZE_INT;
			return result;
		}

		public override int ReadUShort()
		{
			CheckAvaliable(DocumentInputStream.SIZE_SHORT);
			int num = _currentBlock.Available();
			int result;
			if (num > DocumentInputStream.SIZE_SHORT)
			{
				result = _currentBlock.ReadUshortLE();
			}
			else
			{
				DataInputBlock dataInputBlock = GetDataInputBlock(_current_offset + num);
				result = ((num != DocumentInputStream.SIZE_SHORT) ? dataInputBlock.ReadUshortLE(_currentBlock) : _currentBlock.ReadUshortLE());
				_currentBlock = dataInputBlock;
			}
			_current_offset += DocumentInputStream.SIZE_SHORT;
			return result;
		}

		public override int ReadUByte()
		{
			CheckAvaliable(1);
			int result = _currentBlock.ReadUByte();
			_current_offset++;
			if (_currentBlock.Available() < 1)
			{
				_currentBlock = GetDataInputBlock(_current_offset);
			}
			return result;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Current:
				if (_current_offset + offset >= Length || _current_offset + offset < 0)
				{
					throw new ArgumentException("invalid offset");
				}
				_current_offset += (int)offset;
				break;
			case SeekOrigin.Begin:
				if (offset >= Length || offset < 0)
				{
					throw new ArgumentException("invalid offset");
				}
				_current_offset = offset;
				break;
			case SeekOrigin.End:
				if (Length + offset >= Length || Length + offset < 0)
				{
					throw new ArgumentException("invalid offset");
				}
				_current_offset = Length + offset;
				break;
			}
			return _current_offset;
		}
	}
}
