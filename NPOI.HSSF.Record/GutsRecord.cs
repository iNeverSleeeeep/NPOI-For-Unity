using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Guts Record 
	/// Description:  Row/column gutter sizes 
	/// REFERENCE:  PG 320 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class GutsRecord : StandardRecord
	{
		public const short sid = 128;

		private short field_1_left_row_gutter;

		private short field_2_top_col_gutter;

		private short field_3_row_level_max;

		private short field_4_col_level_max;

		/// Get the size of the gutter that appears at the left of the rows
		///
		/// @return gutter size in screen Units
		public short LeftRowGutter
		{
			get
			{
				return field_1_left_row_gutter;
			}
			set
			{
				field_1_left_row_gutter = value;
			}
		}

		/// Get the size of the gutter that appears at the above the columns
		///
		/// @return gutter size in screen Units
		public short TopColGutter
		{
			get
			{
				return field_2_top_col_gutter;
			}
			set
			{
				field_2_top_col_gutter = value;
			}
		}

		/// Get the maximum outline level for the row gutter.
		///
		/// @return maximum outline level
		public short RowLevelMax
		{
			get
			{
				return field_3_row_level_max;
			}
			set
			{
				field_3_row_level_max = value;
			}
		}

		/// Get the maximum outline level for the col gutter.
		///
		/// @return maximum outline level
		public short ColLevelMax
		{
			get
			{
				return field_4_col_level_max;
			}
			set
			{
				field_4_col_level_max = value;
			}
		}

		protected override int DataSize => 8;

		public override short Sid => 128;

		public GutsRecord()
		{
		}

		/// Constructs a Guts record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public GutsRecord(RecordInputStream in1)
		{
			field_1_left_row_gutter = in1.ReadShort();
			field_2_top_col_gutter = in1.ReadShort();
			field_3_row_level_max = in1.ReadShort();
			field_4_col_level_max = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GUTS]\n");
			stringBuilder.Append("    .leftgutter     = ").Append(StringUtil.ToHexString(LeftRowGutter)).Append("\n");
			stringBuilder.Append("    .topgutter      = ").Append(StringUtil.ToHexString(TopColGutter)).Append("\n");
			stringBuilder.Append("    .rowlevelmax    = ").Append(StringUtil.ToHexString(RowLevelMax)).Append("\n");
			stringBuilder.Append("    .collevelmax    = ").Append(StringUtil.ToHexString(ColLevelMax)).Append("\n");
			stringBuilder.Append("[/GUTS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(LeftRowGutter);
			out1.WriteShort(TopColGutter);
			out1.WriteShort(RowLevelMax);
			out1.WriteShort(ColLevelMax);
		}

		public override object Clone()
		{
			GutsRecord gutsRecord = new GutsRecord();
			gutsRecord.field_1_left_row_gutter = field_1_left_row_gutter;
			gutsRecord.field_2_top_col_gutter = field_2_top_col_gutter;
			gutsRecord.field_3_row_level_max = field_3_row_level_max;
			gutsRecord.field_4_col_level_max = field_4_col_level_max;
			return gutsRecord;
		}
	}
}
