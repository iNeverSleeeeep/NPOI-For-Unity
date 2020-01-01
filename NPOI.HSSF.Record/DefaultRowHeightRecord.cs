using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Default Row Height Record
	/// Description:  Row height for rows with Undefined or not explicitly defined
	///               heights.
	/// REFERENCE:  PG 301 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class DefaultRowHeightRecord : StandardRecord
	{
		public const short sid = 549;

		/// The default row height for empty rows is 255 twips (255 / 20 == 12.75 points)
		public const short DEFAULT_ROW_HEIGHT = 255;

		private short field_1_option_flags;

		private short field_2_row_height;

		internal short OptionFlags
		{
			get
			{
				return field_1_option_flags;
			}
			set
			{
				field_1_option_flags = value;
			}
		}

		/// <summary>
		/// Get the default row height
		/// </summary>
		public short RowHeight
		{
			get
			{
				return field_2_row_height;
			}
			set
			{
				field_2_row_height = value;
			}
		}

		protected override int DataSize => 4;

		public override short Sid => 549;

		public DefaultRowHeightRecord()
		{
			field_1_option_flags = 0;
			field_2_row_height = 255;
		}

		/// <summary>
		/// Constructs a DefaultRowHeight record and Sets its fields appropriately.
		/// </summary>
		/// <param name="in1">the RecordInputstream to Read the record from</param>
		public DefaultRowHeightRecord(RecordInputStream in1)
		{
			field_1_option_flags = in1.ReadShort();
			field_2_row_height = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DEFAULTROWHEIGHT]\n");
			stringBuilder.Append("    .optionflags    = ").Append(StringUtil.ToHexString(OptionFlags)).Append("\n");
			stringBuilder.Append("    .rowheight      = ").Append(StringUtil.ToHexString(RowHeight)).Append("\n");
			stringBuilder.Append("[/DEFAULTROWHEIGHT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(OptionFlags);
			out1.WriteShort(RowHeight);
		}

		public override object Clone()
		{
			DefaultRowHeightRecord defaultRowHeightRecord = new DefaultRowHeightRecord();
			defaultRowHeightRecord.field_1_option_flags = field_1_option_flags;
			defaultRowHeightRecord.field_2_row_height = field_2_row_height;
			return defaultRowHeightRecord;
		}
	}
}
