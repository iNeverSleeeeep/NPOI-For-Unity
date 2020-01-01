using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Represents a NoteStructure (0xD) sub record.
	///
	///
	/// The docs say nothing about it. The Length of this record is always 26 bytes.
	///
	///
	/// @author Yegor Kozlov
	public class NoteStructureSubRecord : SubRecord
	{
		public const short sid = 13;

		private const int ENCODED_SIZE = 22;

		private byte[] reserved;

		/// Size of record
		public override int DataSize => reserved.Length;

		/// @return id of this record.
		public override short Sid => 13;

		/// Construct a new <c>NoteStructureSubRecord</c> and
		/// Fill its data with the default values
		public NoteStructureSubRecord()
		{
			reserved = new byte[22];
		}

		/// Constructs a NoteStructureSubRecord and Sets its fields appropriately.
		public NoteStructureSubRecord(ILittleEndianInput in1, int size)
		{
			if (size != 22)
			{
				throw new RecordFormatException("Unexpected size (" + size + ")");
			}
			byte[] buf = new byte[size];
			in1.ReadFully(buf);
			reserved = buf;
		}

		/// Convert this record to string.
		/// Used by BiffViewer and other utulities.
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			stringBuilder.Append("[ftNts ]" + newLine);
			stringBuilder.Append("  size     = ").Append(DataSize).Append(newLine);
			stringBuilder.Append("  reserved = ").Append(HexDump.ToHex(reserved)).Append(newLine);
			stringBuilder.Append("[/ftNts ]" + newLine);
			return stringBuilder.ToString();
		}

		/// Serialize the record data into the supplied array of bytes
		///
		/// @param offset offset in the <c>data</c>
		/// @param data the data to Serialize into
		///
		/// @return size of the record
		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(13);
			out1.WriteShort(reserved.Length);
			out1.Write(reserved);
		}

		public override object Clone()
		{
			NoteStructureSubRecord noteStructureSubRecord = new NoteStructureSubRecord();
			byte[] array = new byte[reserved.Length];
			Array.Copy(reserved, 0, array, 0, array.Length);
			noteStructureSubRecord.reserved = array;
			return noteStructureSubRecord;
		}
	}
}
