using NPOI.Util;
using System;

namespace NPOI.HSSF.Record.Cont
{
	/// An augmented {@link LittleEndianOutput} used for serialization of {@link ContinuableRecord}s.
	/// This class keeps track of how much remaining space is available in the current BIFF record and
	/// can start new {@link ContinueRecord}s as required. 
	///
	/// @author Josh Micich
	public class ContinuableRecordOutput : ILittleEndianOutput
	{
		private class DelayableLittleEndianOutput1 : IDelayableLittleEndianOutput, ILittleEndianOutput
		{
			public ILittleEndianOutput CreateDelayedOutput(int size)
			{
				return this;
			}

			public void Write(byte[] b)
			{
			}

			public void Write(byte[] b, int offset, int len)
			{
			}

			public void WriteByte(int v)
			{
			}

			public void WriteDouble(double v)
			{
			}

			public void WriteInt(int v)
			{
			}

			public void WriteLong(long v)
			{
			}

			public void WriteShort(int v)
			{
			}
		}

		private ILittleEndianOutput _out;

		private UnknownLengthRecordOutput _ulrOutput;

		private int _totalPreviousRecordsSize;

		/// **
		private static ILittleEndianOutput NOPOutput = new DelayableLittleEndianOutput1();

		/// @return total number of bytes written so far (including all BIFF headers) 
		public int TotalSize => _totalPreviousRecordsSize + _ulrOutput.TotalSize;

		/// @return number of remaining bytes of space in current record
		public int AvailableSpace => _ulrOutput.AvailableSpace;

		internal ContinuableRecordOutput(ILittleEndianOutput out1, int sid)
		{
			_ulrOutput = new UnknownLengthRecordOutput(out1, sid);
			_out = out1;
			_totalPreviousRecordsSize = 0;
		}

		public static ContinuableRecordOutput CreateForCountingOnly()
		{
			return new ContinuableRecordOutput(NOPOutput, -777);
		}

		/// Terminates the last record (also updates its 'ushort size' field)
		public void Terminate()
		{
			_ulrOutput.Terminate();
		}

		/// Terminates the current record and starts a new {@link ContinueRecord} (regardless
		/// of how much space is still available in the current record).
		public void WriteContinue()
		{
			_ulrOutput.Terminate();
			_totalPreviousRecordsSize += _ulrOutput.TotalSize;
			_ulrOutput = new UnknownLengthRecordOutput(_out, 60);
		}

		public void WriteContinueIfRequired(int requiredContinuousSize)
		{
			if (_ulrOutput.AvailableSpace < requiredContinuousSize)
			{
				WriteContinue();
			}
		}

		/// Writes the 'optionFlags' byte and encoded character data of a unicode string.  This includes:
		/// <ul>
		/// <li>byte optionFlags</li>
		/// <li>encoded character data (in "ISO-8859-1" or "UTF-16LE" encoding)</li>
		/// </ul>
		///
		/// Notes:
		/// <ul>
		/// <li>The value of the 'is16bitEncoded' flag is determined by the actual character data 
		/// of <c>text</c></li>
		/// <li>The string options flag is never separated (by a {@link ContinueRecord}) from the
		/// first chunk of character data it refers to.</li>
		/// <li>The 'ushort Length' field is assumed to have been explicitly written earlier.  Hence, 
		/// there may be an intervening {@link ContinueRecord}</li>
		/// </ul>
		public void WriteStringData(string text)
		{
			bool flag = StringUtil.HasMultibyte(text);
			int num = 2;
			int num2 = 0;
			if (flag)
			{
				num2 |= 1;
				num++;
			}
			WriteContinueIfRequired(num);
			WriteByte(num2);
			WriteCharacterData(text, flag);
		}

		/// Writes a unicode string complete with header and character data.  This includes:
		/// <ul>
		/// <li>ushort Length</li>
		/// <li>byte optionFlags</li>
		/// <li>ushort numberOfRichTextRuns (optional)</li>
		/// <li>ushort extendedDataSize (optional)</li>
		/// <li>encoded character data (in "ISO-8859-1" or "UTF-16LE" encoding)</li>
		/// </ul>
		///
		/// The following bits of the 'optionFlags' byte will be set as appropriate:
		/// <table border="1">
		/// <tr><th>Mask</th><th>Description</th></tr>
		/// <tr><td>0x01</td><td>is16bitEncoded</td></tr>
		/// <tr><td>0x04</td><td>hasExtendedData</td></tr>
		/// <tr><td>0x08</td><td>isRichText</td></tr>
		/// </table>
		/// Notes:
		/// <ul> 
		/// <li>The value of the 'is16bitEncoded' flag is determined by the actual character data 
		/// of <c>text</c></li>
		/// <li>The string header fields are never separated (by a {@link ContinueRecord}) from the
		/// first chunk of character data (i.e. the first character is always encoded in the same
		/// record as the string header).</li>
		/// </ul>
		public void WriteString(string text, int numberOfRichTextRuns, int extendedDataSize)
		{
			bool flag = StringUtil.HasMultibyte(text);
			int num = 4;
			int num2 = 0;
			if (flag)
			{
				num2 |= 1;
				num++;
			}
			if (numberOfRichTextRuns > 0)
			{
				num2 |= 8;
				num += 2;
			}
			if (extendedDataSize > 0)
			{
				num2 |= 4;
				num += 4;
			}
			WriteContinueIfRequired(num);
			WriteShort(text.Length);
			WriteByte(num2);
			if (numberOfRichTextRuns > 0)
			{
				WriteShort(numberOfRichTextRuns);
			}
			if (extendedDataSize > 0)
			{
				WriteInt(extendedDataSize);
			}
			WriteCharacterData(text, flag);
		}

		private void WriteCharacterData(string text, bool is16bitEncoded)
		{
			int length = text.Length;
			int num = 0;
			if (is16bitEncoded)
			{
				while (true)
				{
					for (int num2 = Math.Min(length - num, _ulrOutput.AvailableSpace / 2); num2 > 0; num2--)
					{
						_ulrOutput.WriteShort(text[num++]);
					}
					if (num >= length)
					{
						break;
					}
					WriteContinue();
					WriteByte(1);
				}
			}
			else
			{
				while (true)
				{
					for (int num4 = Math.Min(length - num, _ulrOutput.AvailableSpace / 1); num4 > 0; num4--)
					{
						_ulrOutput.WriteByte(text[num++]);
					}
					if (num >= length)
					{
						break;
					}
					WriteContinue();
					WriteByte(0);
				}
			}
		}

		public void Write(byte[] b)
		{
			WriteContinueIfRequired(b.Length);
			_ulrOutput.Write(b);
		}

		public void Write(byte[] b, int offset, int len)
		{
			int num = 0;
			while (true)
			{
				for (int num2 = Math.Min(len - num, _ulrOutput.AvailableSpace / 1); num2 > 0; num2--)
				{
					_ulrOutput.WriteByte(b[offset + num++]);
				}
				if (num >= len)
				{
					break;
				}
				WriteContinue();
			}
		}

		public void WriteByte(int v)
		{
			WriteContinueIfRequired(1);
			_ulrOutput.WriteByte(v);
		}

		public void WriteDouble(double v)
		{
			WriteContinueIfRequired(8);
			_ulrOutput.WriteDouble(v);
		}

		public void WriteInt(int v)
		{
			WriteContinueIfRequired(4);
			_ulrOutput.WriteInt(v);
		}

		public void WriteLong(long v)
		{
			WriteContinueIfRequired(8);
			_ulrOutput.WriteLong(v);
		}

		public void WriteShort(int v)
		{
			WriteContinueIfRequired(2);
			_ulrOutput.WriteShort(v);
		}
	}
}
