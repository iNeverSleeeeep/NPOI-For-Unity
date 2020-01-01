using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * The end data record is used to denote the end of the subrecords.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class EndSubRecord : SubRecord
	{
		public const short sid = 0;

		private const int ENCODED_SIZE = 0;

		public override bool IsTerminating => true;

		/// Size of record (exluding 4 byte header)
		public override int DataSize => 0;

		public override short Sid => 0;

		public EndSubRecord()
		{
		}

		/// Constructs a End record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public EndSubRecord(ILittleEndianInput in1, int size)
		{
			if ((size & 0xFF) != 0)
			{
				throw new RecordFormatException("Unexpected size (" + size + ")");
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ftEnd]\n");
			stringBuilder.Append("[/ftEnd]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(0);
			out1.WriteShort(0);
		}

		public override object Clone()
		{
			return new EndSubRecord();
		}
	}
}
