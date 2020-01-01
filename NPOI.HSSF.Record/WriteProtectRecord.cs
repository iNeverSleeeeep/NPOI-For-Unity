using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Write Protect Record
	/// Description:  Indicated that the sheet/workbook Is Write protected. 
	/// REFERENCE:  PG 425 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @version 3.0-pre
	public class WriteProtectRecord : StandardRecord
	{
		public const short sid = 134;

		protected override int DataSize => 0;

		public override short Sid => 134;

		public WriteProtectRecord()
		{
		}

		/// Constructs a WriteAccess record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public WriteProtectRecord(RecordInputStream in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WritePROTECT]\n");
			stringBuilder.Append("[/WritePROTECT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}
	}
}
