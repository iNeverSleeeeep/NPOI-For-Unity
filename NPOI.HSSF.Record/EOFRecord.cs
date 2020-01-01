using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// End Of File record.
	///
	/// Description:  Marks the end of records belonging to a particular object in the
	///               HSSF File
	/// REFERENCE:  PG 307 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class EOFRecord : StandardRecord
	{
		public const short sid = 10;

		public const int ENCODED_SIZE = 4;

		public static readonly EOFRecord instance = new EOFRecord();

		protected override int DataSize => 0;

		public override short Sid => 10;

		public EOFRecord()
		{
		}

		/// Constructs a EOFRecord record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public EOFRecord(RecordInputStream in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[EOF]\n");
			stringBuilder.Append("[/EOF]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		public override object Clone()
		{
			return new EOFRecord();
		}
	}
}
