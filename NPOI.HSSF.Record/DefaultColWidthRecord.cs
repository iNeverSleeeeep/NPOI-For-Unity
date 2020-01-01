using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Default Column Width Record
	/// Description:  Specifies the default width for columns that have no specific
	///               width Set.
	/// REFERENCE:  PG 302 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class DefaultColWidthRecord : StandardRecord
	{
		public const short sid = 85;

		/// The default column width is 8 characters
		public const int DEFAULT_COLUMN_WIDTH = 8;

		private int field_1_col_width;

		internal int offsetForFilePointer;

		/// Get the default column width
		/// @return defaultwidth for columns
		public int ColWidth
		{
			get
			{
				return field_1_col_width;
			}
			set
			{
				field_1_col_width = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 85;

		public DefaultColWidthRecord()
		{
			field_1_col_width = 8;
		}

		/// Constructs a DefaultColumnWidth record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public DefaultColWidthRecord(RecordInputStream in1)
		{
			field_1_col_width = in1.ReadUShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DEFAULTCOLWIDTH]\n");
			stringBuilder.Append("    .colwidth      = ").Append(StringUtil.ToHexString(ColWidth)).Append("\n");
			stringBuilder.Append("[/DEFAULTCOLWIDTH]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(ColWidth);
		}

		public override object Clone()
		{
			DefaultColWidthRecord defaultColWidthRecord = new DefaultColWidthRecord();
			defaultColWidthRecord.field_1_col_width = field_1_col_width;
			return defaultColWidthRecord;
		}
	}
}
