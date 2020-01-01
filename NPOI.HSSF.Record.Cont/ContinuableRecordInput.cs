using NPOI.Util;

namespace NPOI.HSSF.Record.Cont
{
	/// A decorated {@link RecordInputStream} that can read primitive data types
	/// (short, int, long, etc.) spanned across a {@link ContinueRecord } boundary.
	///
	/// <p>
	///  Most records construct themselves from {@link RecordInputStream}.
	///  This class assumes that a {@link ContinueRecord} record break always occurs at the type boundary,
	///  however, it is not always so.
	/// </p>
	///  Two  attachments to <a href="https://issues.apache.org/bugzilla/Show_bug.cgi?id=50779">Bugzilla 50779</a>
	///  demonstrate that a CONTINUE break can appear right in between two bytes of a unicode character
	///  or between two bytes of a <code>short</code>. The problematic portion of the data is
	///  in a Asian Phonetic Settings Block (ExtRst) of a UnicodeString.
	/// <p>
	///  {@link RecordInputStream} greedily requests the bytes to be read and stumbles on such files with a
	///  "Not enough data (1) to read requested (2) bytes" exception.  The <code>ContinuableRecordInput</code>
	///   class circumvents this "type boundary" rule and Reads data byte-by-byte rolling over CONTINUE if necessary.
	/// </p>
	///
	/// <p>
	/// YK: For now (March 2011) this class is only used to read
	///   @link NPOI.HSSF.Record.Common.UnicodeString.ExtRst} blocks of a UnicodeString.
	///
	/// </p>
	///
	/// @author Yegor Kozlov
	public class ContinuableRecordInput : ILittleEndianInput
	{
		private RecordInputStream _in;

		public ContinuableRecordInput(RecordInputStream in1)
		{
			_in = in1;
		}

		public int Available()
		{
			return _in.Available();
		}

		public int ReadByte()
		{
			return _in.ReadByte();
		}

		public int ReadUByte()
		{
			return _in.ReadUByte();
		}

		public short ReadShort()
		{
			return _in.ReadShort();
		}

		public int ReadUShort()
		{
			int num = ReadUByte();
			int num2 = ReadUByte();
			return (num2 << 8) + num;
		}

		public int ReadInt()
		{
			int num = _in.ReadUByte();
			int num2 = _in.ReadUByte();
			int num3 = _in.ReadUByte();
			int num4 = _in.ReadUByte();
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		public long ReadLong()
		{
			int num = _in.ReadUByte();
			int num2 = _in.ReadUByte();
			int num3 = _in.ReadUByte();
			int num4 = _in.ReadUByte();
			int num5 = _in.ReadUByte();
			int num6 = _in.ReadUByte();
			int num7 = _in.ReadUByte();
			int num8 = _in.ReadUByte();
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		public double ReadDouble()
		{
			return _in.ReadDouble();
		}

		public void ReadFully(byte[] buf)
		{
			_in.ReadFully(buf);
		}

		public void ReadFully(byte[] buf, int off, int len)
		{
			_in.ReadFully(buf, off, len);
		}
	}
}
