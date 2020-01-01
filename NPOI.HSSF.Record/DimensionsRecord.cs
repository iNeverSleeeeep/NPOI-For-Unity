using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Dimensions Record
	/// Description:  provides the minumum and maximum bounds
	///               of a sheet.
	/// REFERENCE:  PG 303 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class DimensionsRecord : StandardRecord
	{
		public const short sid = 512;

		private int field_1_first_row;

		private int field_2_last_row;

		private int field_3_first_col;

		private int field_4_last_col;

		private short field_5_zero;

		/// Get the first row number for the sheet
		/// @return row - first row on the sheet
		public int FirstRow
		{
			get
			{
				return field_1_first_row;
			}
			set
			{
				field_1_first_row = value;
			}
		}

		/// Get the last row number for the sheet
		/// @return row - last row on the sheet
		public int LastRow
		{
			get
			{
				return field_2_last_row;
			}
			set
			{
				field_2_last_row = value;
			}
		}

		/// Get the first column number for the sheet
		/// @return column - first column on the sheet
		public int FirstCol
		{
			get
			{
				return field_3_first_col;
			}
			set
			{
				field_3_first_col = value;
			}
		}

		/// Get the last col number for the sheet
		/// @return column - last column on the sheet
		public int LastCol
		{
			get
			{
				return field_4_last_col;
			}
			set
			{
				field_4_last_col = value;
			}
		}

		protected override int DataSize => 14;

		public override short Sid => 512;

		public DimensionsRecord()
		{
		}

		/// Constructs a Dimensions record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public DimensionsRecord(RecordInputStream in1)
		{
			field_1_first_row = in1.ReadInt();
			field_2_last_row = in1.ReadInt();
			field_3_first_col = in1.ReadShort();
			field_4_last_col = in1.ReadShort();
			field_5_zero = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DIMENSIONS]\n");
			stringBuilder.Append("    .firstrow       = ").Append(StringUtil.ToHexString(FirstRow)).Append("\n");
			stringBuilder.Append("    .lastrow        = ").Append(StringUtil.ToHexString(LastRow)).Append("\n");
			stringBuilder.Append("    .firstcol       = ").Append(StringUtil.ToHexString(FirstCol)).Append("\n");
			stringBuilder.Append("    .lastcol        = ").Append(StringUtil.ToHexString(LastCol)).Append("\n");
			stringBuilder.Append("    .zero           = ").Append(StringUtil.ToHexString(field_5_zero)).Append("\n");
			stringBuilder.Append("[/DIMENSIONS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(FirstRow);
			out1.WriteInt(LastRow);
			out1.WriteShort(FirstCol);
			out1.WriteShort(LastCol);
			out1.WriteShort(0);
		}

		public override object Clone()
		{
			DimensionsRecord dimensionsRecord = new DimensionsRecord();
			dimensionsRecord.field_1_first_row = field_1_first_row;
			dimensionsRecord.field_2_last_row = field_2_last_row;
			dimensionsRecord.field_3_first_col = field_3_first_col;
			dimensionsRecord.field_4_last_col = field_4_last_col;
			dimensionsRecord.field_5_zero = field_5_zero;
			return dimensionsRecord;
		}
	}
}
