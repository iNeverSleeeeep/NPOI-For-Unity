using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class FtCblsSubRecord : SubRecord
	{
		public const short sid = 12;

		private const int ENCODED_SIZE = 20;

		private byte[] reserved;

		public override int DataSize => reserved.Length;

		/// @return id of this record.
		public override short Sid => 12;

		/// Construct a new <code>FtCblsSubRecord</code> and
		/// fill its data with the default values
		public FtCblsSubRecord()
		{
			reserved = new byte[20];
		}

		public FtCblsSubRecord(ILittleEndianInput in1, int size)
		{
			if (size != 20)
			{
				throw new RecordFormatException("Unexpected size (" + size + ")");
			}
			byte[] buf = new byte[size];
			in1.ReadFully(buf);
			reserved = buf;
		}

		/// Convert this record to string.
		/// Used by BiffViewer and other utilities.
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FtCbls ]").Append("\n");
			stringBuilder.Append("  size     = ").Append(DataSize).Append("\n");
			stringBuilder.Append("  reserved = ").Append(HexDump.ToHex(reserved)).Append("\n");
			stringBuilder.Append("[/FtCbls ]").Append("\n");
			return stringBuilder.ToString();
		}

		/// Serialize the record data into the supplied array of bytes
		///
		/// @param out the stream to serialize into
		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(12);
			out1.WriteShort(reserved.Length);
			out1.Write(reserved);
		}

		public override object Clone()
		{
			FtCblsSubRecord ftCblsSubRecord = new FtCblsSubRecord();
			byte[] array = new byte[reserved.Length];
			Array.Copy(reserved, 0, array, 0, array.Length);
			ftCblsSubRecord.reserved = array;
			return ftCblsSubRecord;
		}
	}
}
