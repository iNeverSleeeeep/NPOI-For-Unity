using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * The Group marker record is used as a position holder for Groups.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class GroupMarkerSubRecord : SubRecord
	{
		public const short sid = 6;

		private byte[] reserved;

		private static byte[] EMPTY_BYTE_ARRAY = new byte[0];

		/// Size of record (exluding 4 byte header)
		public override int DataSize => reserved.Length;

		public override short Sid => 6;

		public GroupMarkerSubRecord()
		{
			reserved = EMPTY_BYTE_ARRAY;
		}

		/// Constructs a Group marker record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public GroupMarkerSubRecord(ILittleEndianInput in1, int size)
		{
			byte[] buf = new byte[size];
			in1.ReadFully(buf);
			reserved = buf;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			stringBuilder.Append("[ftGmo]" + newLine);
			stringBuilder.Append("  reserved = ").Append(HexDump.ToHex(reserved)).Append(newLine);
			stringBuilder.Append("[/ftGmo]" + newLine);
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(6);
			out1.WriteShort(reserved.Length);
			out1.Write(reserved);
		}

		public override object Clone()
		{
			GroupMarkerSubRecord groupMarkerSubRecord = new GroupMarkerSubRecord();
			groupMarkerSubRecord.reserved = new byte[reserved.Length];
			for (int i = 0; i < reserved.Length; i++)
			{
				groupMarkerSubRecord.reserved[i] = reserved[i];
			}
			return groupMarkerSubRecord;
		}
	}
}
