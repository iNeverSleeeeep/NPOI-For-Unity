using NPOI.HSSF.Record.Crypto;
using NPOI.Util;
using System;
using System.IO;

namespace NPOI.HSSF.Record
{
	/// Title:  Record Input Stream
	/// Description:  Wraps a stream and provides helper methods for the construction of records.
	///
	/// @author Jason Height (jheight @ apache dot org)
	public class RecordInputStream : Stream, ILittleEndianInput
	{
		/// Maximum size of a single record (minus the 4 byte header) without a continue
		public const short MAX_RECORD_DATA_SIZE = 8224;

		private const int INVALID_SID_VALUE = -1;

		private const int DATA_LEN_NEEDS_TO_BE_READ = -1;

		protected int _currentSid;

		protected int _currentDataLength = -1;

		protected int _nextSid = -1;

		private int _currentDataOffset;

		private long pos;

		/// Header {@link LittleEndianInput} facet of the wrapped {@link InputStream} 
		private BiffHeaderInput _bhi;

		/// Data {@link LittleEndianInput} facet of the wrapped {@link InputStream} 
		private ILittleEndianInput _dataInput;

		/// the record identifier of the BIFF record currently being read 
		protected byte[] data = new byte[8224];

		public short Sid => (short)_currentSid;

		public override long Position
		{
			get
			{
				return pos;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public long CurrentLength => _currentDataLength;

		public int RecordOffset => _currentDataOffset;

		public bool HasNextRecord
		{
			get
			{
				if (_currentDataLength != -1 && _currentDataLength != _currentDataOffset)
				{
					throw new LeftoverDataException(_currentSid, Remaining);
				}
				if (_currentDataLength != -1)
				{
					_nextSid = ReadNextSid();
				}
				return _nextSid != -1;
			}
		}

		/// The remaining number of bytes in the <i>current</i> record.
		///
		/// @return The number of bytes remaining in the current record
		public int Remaining
		{
			get
			{
				if (_currentDataLength == -1)
				{
					return 0;
				}
				return _currentDataLength - _currentDataOffset;
			}
		}

		/// Returns true iif a Continue record is next in the excel stream _currentDataOffset
		///
		/// @return True when a ContinueRecord is next.
		public bool IsContinueNext
		{
			get
			{
				if (_currentDataLength != -1 && _currentDataOffset != _currentDataLength)
				{
					throw new InvalidOperationException("Should never be called before end of current record");
				}
				if (!HasNextRecord)
				{
					return false;
				}
				return _nextSid == 60;
			}
		}

		public override long Length => _currentDataLength;

		public override bool CanRead => true;

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public RecordInputStream(Stream in1)
			: this(in1, null, 0)
		{
		}

		public RecordInputStream(Stream in1, Biff8EncryptionKey key, int initialOffset)
		{
			if (key == null)
			{
				_dataInput = SimpleHeaderInput.GetLEI(in1);
				_bhi = new SimpleHeaderInput(in1);
			}
			else
			{
				_dataInput = (ILittleEndianInput)(_bhi = new Biff8DecryptingStream(in1, initialOffset, key));
			}
			_nextSid = ReadNextSid();
		}

		public int Available()
		{
			return Remaining;
		}

		/// This method will Read a byte from the current record
		public int Read()
		{
			CheckRecordPosition(1);
			_currentDataOffset++;
			pos++;
			return _dataInput.ReadByte();
		}

		/// @return the sid of the next record or {@link #INVALID_SID_VALUE} if at end of stream
		private int ReadNextSid()
		{
			int num = _bhi.Available();
			if (num < 4)
			{
				return -1;
			}
			int num2 = _bhi.ReadRecordSID();
			if (num2 == -1)
			{
				throw new RecordFormatException("Found invalid sid (" + num2 + ")");
			}
			_currentDataLength = -1;
			return num2;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// Moves to the next record in the stream.
		///
		/// <i>Note: The auto continue flag is Reset to true</i>
		public void NextRecord()
		{
			if (_nextSid == -1)
			{
				throw new InvalidDataException("EOF - next record not available");
			}
			if (_currentDataLength != -1)
			{
				throw new InvalidDataException("Cannot call nextRecord() without checking hasNextRecord() first");
			}
			_currentSid = _nextSid;
			_currentDataOffset = 0;
			_currentDataLength = _bhi.ReadDataSize();
			pos += 2L;
			if (_currentDataLength > 8224)
			{
				throw new RecordFormatException("The content of an excel record cannot exceed " + (short)8224 + " bytes");
			}
		}

		protected void CheckRecordPosition(int requiredByteCount)
		{
			int remaining = Remaining;
			if (remaining < requiredByteCount)
			{
				if (remaining != 0 || !IsContinueNext)
				{
					throw new RecordFormatException("Not enough data (" + remaining + ") to read requested (" + requiredByteCount + ") bytes");
				}
				NextRecord();
			}
		}

		/// Reads an 8 bit, signed value
		public override int ReadByte()
		{
			CheckRecordPosition(1);
			_currentDataOffset++;
			pos++;
			return _dataInput.ReadByte();
		}

		/// Reads a 16 bit, signed value
		public short ReadShort()
		{
			CheckRecordPosition(2);
			_currentDataOffset += 2;
			pos += 2L;
			return _dataInput.ReadShort();
		}

		public int ReadInt()
		{
			CheckRecordPosition(4);
			_currentDataOffset += 4;
			pos += 4L;
			return _dataInput.ReadInt();
		}

		public long ReadLong()
		{
			CheckRecordPosition(8);
			_currentDataOffset += 8;
			pos += 8L;
			return _dataInput.ReadLong();
		}

		/// Reads an 8 bit, Unsigned value
		public int ReadUByte()
		{
			int num = ReadByte();
			if (num < 0)
			{
				num += 256;
			}
			return num;
		}

		/// Reads a 16 bit,un- signed value.
		/// @return
		public int ReadUShort()
		{
			CheckRecordPosition(2);
			_currentDataOffset += 2;
			pos += 2L;
			return _dataInput.ReadUShort();
		}

		public double ReadDouble()
		{
			CheckRecordPosition(8);
			_currentDataOffset += 8;
			long value = _dataInput.ReadLong();
			double result = BitConverter.Int64BitsToDouble(value);
			pos += 8L;
			return result;
		}

		public void ReadFully(byte[] buf)
		{
			ReadFully(buf, 0, buf.Length);
		}

		public void ReadFully(byte[] buf, int off, int len)
		{
			CheckRecordPosition(len);
			_dataInput.ReadFully(buf, off, len);
			_currentDataOffset += len;
			pos += len;
		}

		/// given a byte array of 16-bit Unicode Chars, compress to 8-bit and     
		/// return a string     
		///
		///             { 0x16, 0x00 } -0x16     
		///
		///             @param Length the Length of the string
		///             @return                                     the Converted string
		///             @exception  ArgumentException        if len is too large (i.e.,
		///     there is not enough data in string to Create a String of that     
		///     Length)     
		public string ReadUnicodeLEString(int requestedLength)
		{
			return ReadStringCommon(requestedLength, pIsCompressedEncoding: false);
		}

		public string ReadCompressedUnicode(int requestedLength)
		{
			return ReadStringCommon(requestedLength, pIsCompressedEncoding: true);
		}

		private string ReadStringCommon(int requestedLength, bool pIsCompressedEncoding)
		{
			if (requestedLength < 0 || requestedLength > 1048576)
			{
				throw new ArgumentException("Bad requested string length (" + requestedLength + ")");
			}
			char[] array = new char[requestedLength];
			bool flag = pIsCompressedEncoding;
			int i = 0;
			while (true)
			{
				int num = flag ? Remaining : (Remaining / 2);
				if (requestedLength - i <= num)
				{
					for (; i < requestedLength; i++)
					{
						char c = array[i] = ((!flag) ? ((char)ReadShort()) : ((char)ReadUByte()));
					}
					return new string(array);
				}
				while (num > 0)
				{
					char c2 = array[i] = ((!flag) ? ((char)ReadShort()) : ((char)ReadUByte()));
					i++;
					num--;
				}
				if (!IsContinueNext)
				{
					throw new RecordFormatException("Expected to find a ContinueRecord in order to read remaining " + (requestedLength - i) + " of " + requestedLength + " chars");
				}
				if (Remaining != 0)
				{
					break;
				}
				NextRecord();
				byte b = (byte)ReadByte();
				flag = (b == 0);
			}
			throw new RecordFormatException("Odd number of bytes(" + Remaining + ") left behind");
		}

		public string ReadString()
		{
			int requestedLength = ReadUShort();
			byte b = (byte)ReadByte();
			return ReadStringCommon(requestedLength, b == 0);
		}

		/// Returns the remaining bytes for the current record.
		///
		/// @return The remaining bytes of the current record.
		public byte[] ReadRemainder()
		{
			int remaining = Remaining;
			if (remaining == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[remaining];
			ReadFully(array);
			return array;
		}

		/// Reads all byte data for the current record, including any
		///  that overlaps into any following continue records.
		///
		///  @deprecated Best to write a input stream that wraps this one where there Is
		///  special sub record that may overlap continue records.
		public byte[] ReadAllContinuedRemainder()
		{
			using (MemoryStream memoryStream = new MemoryStream(16448))
			{
				while (true)
				{
					byte[] array = ReadRemainder();
					memoryStream.Write(array, 0, array.Length);
					if (!IsContinueNext)
					{
						break;
					}
					NextRecord();
				}
				return memoryStream.ToArray();
			}
		}

		public override void SetLength(long value)
		{
			_currentDataLength = (int)value;
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override int Read(byte[] b, int off, int len)
		{
			Array.Copy(data, _currentDataOffset, b, off, len);
			_currentDataOffset += len;
			return Math.Min(data.Length, b.Length);
		}

		/// @return sid of next record. Can be called after hasNextRecord()
		public int GetNextSid()
		{
			return _nextSid;
		}
	}
}
