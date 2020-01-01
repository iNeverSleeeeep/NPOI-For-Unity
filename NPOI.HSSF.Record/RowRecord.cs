using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Row Record
	/// Description:  stores the row information for the sheet. 
	/// REFERENCE:  PG 379 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class RowRecord : StandardRecord, IComparable
	{
		public const short sid = 520;

		public const int ENCODED_SIZE = 20;

		private const int OPTION_BITS_ALWAYS_SET = 256;

		private const int DEFAULT_HEIGHT_BIT = 32768;

		/// The maximum row number that excel can handle (zero based) ie 65536 rows Is
		/// max number of rows.
		[Obsolete]
		public const int MAX_ROW_NUMBER = 65535;

		private int field_1_row_number;

		private int field_2_first_col;

		private int field_3_last_col;

		private short field_4_height;

		private short field_5_optimize;

		private short field_6_reserved;

		/// 16 bit options flags 
		private int field_7_option_flags;

		private static BitField outlineLevel = BitFieldFactory.GetInstance(7);

		private static BitField colapsed = BitFieldFactory.GetInstance(16);

		private static BitField zeroHeight = BitFieldFactory.GetInstance(32);

		private static BitField badFontHeight = BitFieldFactory.GetInstance(64);

		private static BitField formatted = BitFieldFactory.GetInstance(128);

		private int field_8_option_flags;

		private static BitField xfIndex = BitFieldFactory.GetInstance(4095);

		private static BitField topBorder = BitFieldFactory.GetInstance(4096);

		private static BitField bottomBorder = BitFieldFactory.GetInstance(8192);

		private static BitField phoeneticGuide = BitFieldFactory.GetInstance(16384);

		/// Get the logical row number for this row (0 based index)
		/// @return row - the row number
		public bool IsEmpty => (field_2_first_col | field_3_last_col) == 0;

		public int RowNumber
		{
			get
			{
				return field_1_row_number;
			}
			set
			{
				field_1_row_number = value;
			}
		}

		/// Get the logical col number for the first cell this row (0 based index)
		/// @return col - the col number
		public int FirstCol
		{
			get
			{
				return field_2_first_col;
			}
			set
			{
				field_2_first_col = value;
			}
		}

		/// Get the logical col number for the last cell this row plus one (0 based index)
		/// @return col - the last col number + 1
		public int LastCol
		{
			get
			{
				return field_3_last_col;
			}
			set
			{
				field_3_last_col = value;
			}
		}

		/// Get the height of the row
		/// @return height of the row
		public short Height
		{
			get
			{
				return field_4_height;
			}
			set
			{
				field_4_height = value;
			}
		}

		/// Get whether to optimize or not (Set to 0)
		/// @return optimize (Set to 0)
		public short Optimize
		{
			get
			{
				return field_5_optimize;
			}
			set
			{
				field_5_optimize = value;
			}
		}

		/// Gets the option bitmask.  (use the individual bit Setters that refer to this
		/// method)
		/// @return options - the bitmask
		public short OptionFlags
		{
			get
			{
				return (short)field_7_option_flags;
			}
			set
			{
				field_7_option_flags = (value | 0x100);
			}
		}

		/// Get the outline level of this row
		/// @return ol - the outline level
		/// @see #GetOptionFlags()
		public short OutlineLevel
		{
			get
			{
				return (short)outlineLevel.GetValue(field_7_option_flags);
			}
			set
			{
				field_7_option_flags = outlineLevel.SetValue(field_7_option_flags, value);
			}
		}

		/// Get whether or not to colapse this row
		/// @return c - colapse or not
		/// @see #GetOptionFlags()
		public bool Colapsed
		{
			get
			{
				return colapsed.IsSet(field_7_option_flags);
			}
			set
			{
				field_7_option_flags = colapsed.SetBoolean(field_7_option_flags, value);
			}
		}

		/// Get whether or not to Display this row with 0 height
		/// @return - z height is zero or not.
		/// @see #GetOptionFlags()
		public bool ZeroHeight
		{
			get
			{
				return zeroHeight.IsSet(field_7_option_flags);
			}
			set
			{
				field_7_option_flags = zeroHeight.SetBoolean(field_7_option_flags, value);
			}
		}

		/// Get whether the font and row height are not compatible
		/// @return - f -true if they aren't compatible (damn not logic)
		/// @see #GetOptionFlags()
		public bool BadFontHeight
		{
			get
			{
				return badFontHeight.IsSet(field_7_option_flags);
			}
			set
			{
				field_7_option_flags = badFontHeight.SetBoolean(field_7_option_flags, value);
			}
		}

		/// Get whether the row has been formatted (even if its got all blank cells)
		/// @return formatted or not
		/// @see #GetOptionFlags()
		public bool Formatted
		{
			get
			{
				return formatted.IsSet(field_7_option_flags);
			}
			set
			{
				field_7_option_flags = formatted.SetBoolean(field_7_option_flags, value);
			}
		}

		public short OptionFlags2 => (short)field_8_option_flags;

		/// if the row is formatted then this is the index to the extended format record
		/// @see org.apache.poi.hssf.record.ExtendedFormatRecord
		/// @return index to the XF record or bogus value (undefined) if Isn't formatted
		public short XFIndex
		{
			get
			{
				return xfIndex.GetShortValue((short)field_8_option_flags);
			}
			set
			{
				field_8_option_flags = xfIndex.SetValue(field_8_option_flags, value);
			}
		}

		public bool TopBorder
		{
			get
			{
				return topBorder.IsSet(field_8_option_flags);
			}
			set
			{
				field_8_option_flags = topBorder.SetBoolean(field_8_option_flags, value);
			}
		}

		public bool BottomBorder
		{
			get
			{
				return bottomBorder.IsSet(field_8_option_flags);
			}
			set
			{
				field_8_option_flags = bottomBorder.SetBoolean(field_8_option_flags, value);
			}
		}

		public bool PhoeneticGuide
		{
			get
			{
				return phoeneticGuide.IsSet(field_8_option_flags);
			}
			set
			{
				field_8_option_flags = phoeneticGuide.SetBoolean(field_8_option_flags, value);
			}
		}

		protected override int DataSize => 16;

		public override int RecordSize => 20;

		public override short Sid => 520;

		public RowRecord(int rowNumber)
		{
			field_1_row_number = rowNumber;
			field_4_height = 255;
			field_5_optimize = 0;
			field_6_reserved = 0;
			field_7_option_flags = 256;
			field_8_option_flags = 15;
			SetEmpty();
		}

		/// Constructs a Row record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public RowRecord(RecordInputStream in1)
		{
			field_1_row_number = in1.ReadUShort();
			field_2_first_col = in1.ReadShort();
			field_3_last_col = in1.ReadShort();
			field_4_height = in1.ReadShort();
			field_5_optimize = in1.ReadShort();
			field_6_reserved = in1.ReadShort();
			field_7_option_flags = in1.ReadShort();
			field_8_option_flags = in1.ReadShort();
		}

		public void SetEmpty()
		{
			field_2_first_col = 0;
			field_3_last_col = 0;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ROW]\n");
			stringBuilder.Append("    .rownumber      = ").Append(StringUtil.ToHexString(RowNumber)).Append("\n");
			stringBuilder.Append("    .firstcol       = ").Append(StringUtil.ToHexString(FirstCol)).Append("\n");
			stringBuilder.Append("    .lastcol        = ").Append(StringUtil.ToHexString(LastCol)).Append("\n");
			stringBuilder.Append("    .height         = ").Append(StringUtil.ToHexString(Height)).Append("\n");
			stringBuilder.Append("    .optimize       = ").Append(StringUtil.ToHexString(Optimize)).Append("\n");
			stringBuilder.Append("    .reserved       = ").Append(StringUtil.ToHexString(field_6_reserved)).Append("\n");
			stringBuilder.Append("    .optionflags    = ").Append(StringUtil.ToHexString(OptionFlags)).Append("\n");
			stringBuilder.Append("        .outlinelvl = ").Append(StringUtil.ToHexString(OutlineLevel)).Append("\n");
			stringBuilder.Append("        .colapsed   = ").Append(Colapsed).Append("\n");
			stringBuilder.Append("        .zeroheight = ").Append(ZeroHeight).Append("\n");
			stringBuilder.Append("        .badfontheig= ").Append(BadFontHeight).Append("\n");
			stringBuilder.Append("        .formatted  = ").Append(Formatted).Append("\n");
			stringBuilder.Append("        .optionsflags2  = ").Append(StringUtil.ToHexString(OptionFlags2)).Append("\n");
			stringBuilder.Append("        .xFindex    = ").Append(StringUtil.ToHexString(XFIndex)).Append("\n");
			stringBuilder.Append("        .topBorder  = ").Append(TopBorder).Append("\n");
			stringBuilder.Append("        .bottomBorder  = ").Append(BottomBorder).Append("\n");
			stringBuilder.Append("        .phoeneticGuide= ").Append(PhoeneticGuide).Append("\n");
			stringBuilder.Append("[/ROW]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(RowNumber);
			out1.WriteShort((FirstCol != -1) ? FirstCol : 0);
			out1.WriteShort((LastCol != -1) ? LastCol : 0);
			out1.WriteShort(Height);
			out1.WriteShort(Optimize);
			out1.WriteShort(field_6_reserved);
			out1.WriteShort(OptionFlags);
			out1.WriteShort(XFIndex);
		}

		public int CompareTo(object obj)
		{
			RowRecord rowRecord = (RowRecord)obj;
			if (RowNumber == rowRecord.RowNumber)
			{
				return 0;
			}
			if (RowNumber < rowRecord.RowNumber)
			{
				return -1;
			}
			if (RowNumber > rowRecord.RowNumber)
			{
				return 1;
			}
			return -1;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is RowRecord))
			{
				return false;
			}
			RowRecord rowRecord = (RowRecord)obj;
			if (RowNumber == rowRecord.RowNumber)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return RowNumber;
		}

		public override object Clone()
		{
			RowRecord rowRecord = new RowRecord(field_1_row_number);
			rowRecord.field_2_first_col = field_2_first_col;
			rowRecord.field_3_last_col = field_3_last_col;
			rowRecord.field_4_height = field_4_height;
			rowRecord.field_5_optimize = field_5_optimize;
			rowRecord.field_6_reserved = field_6_reserved;
			rowRecord.field_7_option_flags = field_7_option_flags;
			rowRecord.field_8_option_flags = field_8_option_flags;
			return rowRecord;
		}
	}
}
