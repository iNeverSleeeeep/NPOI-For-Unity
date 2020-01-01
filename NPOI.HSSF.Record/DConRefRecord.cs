using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// DConRef records specify a range in a workbook (internal or external) that serves as a data source
	/// for pivot tables or data consolidation.
	///
	/// Represents a <code>DConRef</code> Structure
	/// <a href="http://msdn.microsoft.com/en-us/library/dd923854(office.12).aspx">[MS-XLS s.
	/// 2.4.86]</a>, and the contained <code>DConFile</code> structure
	/// <a href="http://msdn.microsoft.com/en-us/library/dd950157(office.12).aspx">
	/// [MS-XLS s. 2.5.69]</a>. This in turn contains a <code>XLUnicodeStringNoCch</code>
	/// <a href="http://msdn.microsoft.com/en-us/library/dd910585(office.12).aspx">
	/// [MS-XLS s. 2.5.296]</a>.
	///
	/// <pre>
	///         _______________________________
	///        |          DConRef              |
	///             (bytes) +-+-+-+-+-+-+-+-+-+-+...+-+-+-+-+
	///        |    ref    |cch|  stFile   | un|
	///        +-+-+-+-+-+-+-+-+-+-+...+-+-+-+-+
	///                              |
	///                     _________|_____________________
	///                    |DConFile / XLUnicodeStringNoCch|
	///                    +-+-+-+-+-+-+-+-+-+-+-+...+-+-+-+
	///             (bits) |h|   reserved  |      rgb      |
	///                    +-+-+-+-+-+-+-+-+-+-+-+...+-+-+-+
	/// </pre>
	/// Where
	/// <ul>
	/// <li><code>DConFile.h = 0x00</code> if the characters in<code>rgb</code> are single byte, and
	/// <code>DConFile.h = 0x01</code> if they are double byte. <br />
	/// If they are double byte, then<br />
	/// <ul>
	/// <li> If it exists, the length of <code>DConRef.un = 2</code>. Otherwise it is 1.</li>
	/// <li> The length of <code>DConFile.rgb = (2 * DConRef.cch)</code>. Otherwise it is equal to
	/// <code>DConRef.cch</code></li>.
	/// </ul>
	/// </li>
	/// <li><code>DConRef.rgb</code> starts with <code>0x01</code> if it is an external reference,
	/// and with <code>0x02</code> if it is a self-reference.</li>
	/// </ul>
	///
	/// At the moment this class is read-only.
	///
	/// @author Niklas Rehfeld
	public class DConRefRecord : StandardRecord
	{
		/// The id of the record type,
		/// <code>sid = {@value}</code>
		public const short sid = 81;

		/// A RefU structure specifying the range of cells if this record is part of an SXTBL.
		/// <a href="http://msdn.microsoft.com/en-us/library/dd920420(office.12).aspx">
		/// [MS XLS s.2.5.211]</a>
		private int firstRow;

		/// A RefU structure specifying the range of cells if this record is part of an SXTBL.
		/// <a href="http://msdn.microsoft.com/en-us/library/dd920420(office.12).aspx">
		/// [MS XLS s.2.5.211]</a>
		private int lastRow;

		/// A RefU structure specifying the range of cells if this record is part of an SXTBL.
		/// <a href="http://msdn.microsoft.com/en-us/library/dd920420(office.12).aspx">
		/// [MS XLS s.2.5.211]</a>
		private int firstCol;

		/// A RefU structure specifying the range of cells if this record is part of an SXTBL.
		/// <a href="http://msdn.microsoft.com/en-us/library/dd920420(office.12).aspx">
		/// [MS XLS s.2.5.211]</a>
		private int lastCol;

		/// the number of chars in the link
		private int charCount;

		/// the type of characters (single or double byte)
		private int charType;

		/// The link's path string. This is the <code>rgb</code> field of a
		/// <code>XLUnicodeStringNoCch</code>. Therefore it will contain at least one leading special
		/// character (0x01 or 0x02) and probably other ones.<p />
		/// @see <a href="http://msdn.microsoft.com/en-us/library/dd923491(office.12).aspx">
		/// DConFile [MS-XLS s. 2.5.77]</a> and
		/// <a href="http://msdn.microsoft.com/en-us/library/dd950157(office.12).aspx">
		/// VirtualPath [MS-XLS s. 2.5.69]</a>
		/// <p />
		private byte[] path;

		/// unused bits at the end, must be set to 0.
		private byte[] _unused;

		protected override int DataSize
		{
			get
			{
				int num = 9 + path.Length;
				if (path[0] == 2)
				{
					num += _unused.Length;
				}
				return num;
			}
		}

		public override short Sid => 81;

		/// @return The first column of the range.
		public int FirstColumn => firstCol;

		/// @return The first row of the range.
		public int FirstRow => firstRow;

		/// @return The last column of the range.
		public int LastColumn => lastCol;

		/// @return The last row of the range.
		public int LastRow => lastRow;

		/// @return the link's path, with the special characters stripped/replaced. May be null.
		/// See MS-XLS 2.5.277 (VirtualPath)
		public string ReadablePath
		{
			get
			{
				if (path != null)
				{
					int i;
					for (i = 1; path[i] < 32 && i < path.Length; i++)
					{
					}
					string @string = Encoding.UTF8.GetString(Arrays.CopyOfRange(path, i, path.Length));
					return @string.Replace("\u0003", "/");
				}
				return null;
			}
		}

		/// Checks if the data source in this reference record is external to this sheet or internal.
		///
		/// @return true iff this is an external reference.
		public bool IsExternalRef
		{
			get
			{
				if (path[0] == 1)
				{
					return true;
				}
				return false;
			}
		}

		/// Read constructor.
		///
		/// @param data byte array containing a DConRef Record, including the header.
		public DConRefRecord(byte[] data)
		{
			int num = 0;
			if (LittleEndian.GetShort(data, num) != 81)
			{
				throw new RecordFormatException("incompatible sid.");
			}
			num += 2;
			num += 2;
			firstRow = LittleEndian.GetUShort(data, num);
			num += 2;
			lastRow = LittleEndian.GetUShort(data, num);
			num += 2;
			firstCol = LittleEndian.GetUByte(data, num);
			num++;
			lastCol = LittleEndian.GetUByte(data, num);
			num++;
			charCount = LittleEndian.GetUShort(data, num);
			num += 2;
			if (charCount < 2)
			{
				throw new RecordFormatException("Character count must be >= 2");
			}
			charType = LittleEndian.GetUByte(data, num);
			num++;
			int num2 = charCount * ((charType & 1) + 1);
			path = LittleEndian.GetByteArray(data, num, num2);
			num += num2;
			if (path[0] == 2)
			{
				_unused = LittleEndian.GetByteArray(data, num, charType + 1);
			}
		}

		/// Read Constructor.
		///
		/// @param inStream RecordInputStream containing a DConRefRecord structure.
		public DConRefRecord(RecordInputStream inStream)
		{
			if (inStream.Sid != 81)
			{
				throw new RecordFormatException("Wrong sid: " + inStream.Sid);
			}
			firstRow = inStream.ReadUShort();
			lastRow = inStream.ReadUShort();
			firstCol = inStream.ReadUByte();
			lastCol = inStream.ReadUByte();
			charCount = inStream.ReadUShort();
			charType = (inStream.ReadUByte() & 1);
			int num = charCount * (charType + 1);
			path = new byte[num];
			inStream.ReadFully(path);
			if (path[0] == 2)
			{
				_unused = inStream.ReadRemainder();
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(firstRow);
			out1.WriteShort(lastRow);
			out1.WriteByte(firstCol);
			out1.WriteByte(lastCol);
			out1.WriteShort(charCount);
			out1.WriteByte(charType);
			out1.Write(path);
			if (path[0] == 2)
			{
				out1.Write(_unused);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DCONREF]\n");
			stringBuilder.Append("    .ref\n");
			stringBuilder.Append("        .firstrow   = ").Append(firstRow).Append("\n");
			stringBuilder.Append("        .lastrow    = ").Append(lastRow).Append("\n");
			stringBuilder.Append("        .firstcol   = ").Append(firstCol).Append("\n");
			stringBuilder.Append("        .lastcol    = ").Append(lastCol).Append("\n");
			stringBuilder.Append("    .cch            = ").Append(charCount).Append("\n");
			stringBuilder.Append("    .stFile\n");
			stringBuilder.Append("        .h          = ").Append(charType).Append("\n");
			stringBuilder.Append("        .rgb        = ").Append(ReadablePath).Append("\n");
			stringBuilder.Append("[/DCONREF]\n");
			return stringBuilder.ToString();
		}

		/// @return raw path byte array.
		public byte[] GetPath()
		{
			return Arrays.CopyOf(path, path.Length);
		}
	}
}
