using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Print Gridlines Record
	/// Description:  whether to print the gridlines when you enjoy you spReadsheet on paper.
	/// REFERENCE:  PG 373 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class PrintGridlinesRecord : StandardRecord
	{
		public const short sid = 43;

		private short field_1_print_gridlines;

		/// Get whether or not to print the gridlines (and make your spReadsheet ugly)
		///
		/// @return make spReadsheet ugly - Y/N
		public bool PrintGridlines
		{
			get
			{
				return field_1_print_gridlines == 1;
			}
			set
			{
				if (value)
				{
					field_1_print_gridlines = 1;
				}
				else
				{
					field_1_print_gridlines = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 43;

		public PrintGridlinesRecord()
		{
		}

		/// Constructs a PrintGridlines record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PrintGridlinesRecord(RecordInputStream in1)
		{
			field_1_print_gridlines = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRINTGRIDLINES]\n");
			stringBuilder.Append("    .printgridlines = ").Append(PrintGridlines).Append("\n");
			stringBuilder.Append("[/PRINTGRIDLINES]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_print_gridlines);
		}

		public override object Clone()
		{
			PrintGridlinesRecord printGridlinesRecord = new PrintGridlinesRecord();
			printGridlinesRecord.field_1_print_gridlines = field_1_print_gridlines;
			return printGridlinesRecord;
		}
	}
}
