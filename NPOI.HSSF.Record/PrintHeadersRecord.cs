using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Print Headers Record
	/// Description:  Whether or not to print the row/column headers when you
	///               enjoy your spReadsheet in the physical form.
	/// REFERENCE:  PG 373 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class PrintHeadersRecord : StandardRecord
	{
		public const short sid = 42;

		private short field_1_print_headers;

		/// <summary>
		/// Get whether to print the headers - y/n
		/// </summary>
		/// <value><c>true</c> if [print headers]; otherwise, <c>false</c>.</value>
		public bool PrintHeaders
		{
			get
			{
				return field_1_print_headers == 1;
			}
			set
			{
				field_1_print_headers = (short)(value ? 1 : 0);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 42;

		public PrintHeadersRecord()
		{
		}

		/// Constructs a PrintHeaders record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PrintHeadersRecord(RecordInputStream in1)
		{
			field_1_print_headers = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRINTHEADERS]\n");
			stringBuilder.Append("    .printheaders   = ").Append(PrintHeaders).Append("\n");
			stringBuilder.Append("[/PRINTHEADERS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_print_headers);
		}

		public override object Clone()
		{
			PrintHeadersRecord printHeadersRecord = new PrintHeadersRecord();
			printHeadersRecord.field_1_print_headers = field_1_print_headers;
			return printHeadersRecord;
		}
	}
}
