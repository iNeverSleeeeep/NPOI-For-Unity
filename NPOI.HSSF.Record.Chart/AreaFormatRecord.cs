using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The area format record is used to define the colours and patterns for an area.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class AreaFormatRecord : StandardRecord
	{
		public const short FILL_PATTERN_NONE = 0;

		public const short FILL_PATTERN_SOLID = 1;

		public const short FILL_PATTERN_MEDIUM_GRAY = 2;

		public const short FILL_PATTERN_DARK_GRAY = 3;

		public const short FILL_PATTERN_LIGHT_GRAY = 4;

		public const short FILL_PATTERN_HORIZONTAL_STRIPES = 5;

		public const short FILL_PATTERN_VERTICAL_STRIPES = 6;

		public const short FILL_PATTERN_DOWNWARD_DIAGONAL_STRIPES = 7;

		public const short FILL_PATTERN_UPWARD_DIAGONAL_STRIPES = 8;

		public const short FILL_PATTERN_GRID = 9;

		public const short FILL_PATTERN_TRELLIS = 10;

		public const short FILL_PATTERN_LIGHT_HORIZONTAL_STRIPES = 11;

		public const short FILL_PATTERN_LIGHT_VERTICAL_STRIPES = 12;

		public const short FILL_PATTERN_LIGHTDOWN = 13;

		public const short FILL_PATTERN_LIGHTUP = 14;

		public const short FILL_PATTERN_LIGHT_GRID = 15;

		public const short FILL_PATTERN_LIGHT_TRELLIS = 16;

		public const short FILL_PATTERN_GRAYSCALE_1_8 = 17;

		public const short FILL_PATTERN_GRAYSCALE_1_16 = 18;

		public const short sid = 4106;

		private int field_1_foregroundColor;

		private int field_2_backgroundColor;

		private short field_3_pattern;

		private short field_4_formatFlags;

		private BitField automatic = BitFieldFactory.GetInstance(1);

		private BitField invert = BitFieldFactory.GetInstance(2);

		private short field_5_forecolorIndex;

		private short field_6_backcolorIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 16;

		public override short Sid => 4106;

		/// the foreground color field for the AreaFormat record.
		public int ForegroundColor
		{
			get
			{
				return field_1_foregroundColor;
			}
			set
			{
				field_1_foregroundColor = value;
			}
		}

		/// the background color field for the AreaFormat record.
		public int BackgroundColor
		{
			get
			{
				return field_2_backgroundColor;
			}
			set
			{
				field_2_backgroundColor = value;
			}
		}

		/// the pattern field for the AreaFormat record.
		public short Pattern
		{
			get
			{
				return field_3_pattern;
			}
			set
			{
				field_3_pattern = value;
			}
		}

		/// the format flags field for the AreaFormat record.
		public short FormatFlags
		{
			get
			{
				return field_4_formatFlags;
			}
			set
			{
				field_4_formatFlags = value;
			}
		}

		/// the forecolor index field for the AreaFormat record.
		public short ForecolorIndex
		{
			get
			{
				return field_5_forecolorIndex;
			}
			set
			{
				field_5_forecolorIndex = value;
			}
		}

		/// the backcolor index field for the AreaFormat record.
		public short BackcolorIndex
		{
			get
			{
				return field_6_backcolorIndex;
			}
			set
			{
				field_6_backcolorIndex = value;
			}
		}

		/// automatic formatting
		/// @return  the automatic field value.
		public bool IsAutomatic
		{
			get
			{
				return automatic.IsSet(field_4_formatFlags);
			}
			set
			{
				field_4_formatFlags = automatic.SetShortBoolean(field_4_formatFlags, value);
			}
		}

		/// swap foreground and background colours when data is negative
		/// @return  the invert field value.
		public bool IsInvert
		{
			get
			{
				return invert.IsSet(field_4_formatFlags);
			}
			set
			{
				field_4_formatFlags = invert.SetShortBoolean(field_4_formatFlags, value);
			}
		}

		public AreaFormatRecord()
		{
		}

		/// Constructs a AreaFormat record and s its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AreaFormatRecord(RecordInputStream in1)
		{
			field_1_foregroundColor = in1.ReadInt();
			field_2_backgroundColor = in1.ReadInt();
			field_3_pattern = in1.ReadShort();
			field_4_formatFlags = in1.ReadShort();
			field_5_forecolorIndex = in1.ReadShort();
			field_6_backcolorIndex = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AREAFORMAT]\n");
			stringBuilder.Append("    .foregroundColor      = ").Append("0x").Append(HexDump.ToHex(ForegroundColor))
				.Append(" (")
				.Append(ForegroundColor)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .backgroundColor      = ").Append("0x").Append(HexDump.ToHex(BackgroundColor))
				.Append(" (")
				.Append(BackgroundColor)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .pattern              = ").Append("0x").Append(HexDump.ToHex(Pattern))
				.Append(" (")
				.Append(Pattern)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .formatFlags          = ").Append("0x").Append(HexDump.ToHex(FormatFlags))
				.Append(" (")
				.Append(FormatFlags)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .automatic                = ").Append(IsAutomatic).Append('\n');
			stringBuilder.Append("         .invert                   = ").Append(IsInvert).Append('\n');
			stringBuilder.Append("    .forecolorIndex       = ").Append("0x").Append(HexDump.ToHex(ForecolorIndex))
				.Append(" (")
				.Append(ForecolorIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .backcolorIndex       = ").Append("0x").Append(HexDump.ToHex(BackcolorIndex))
				.Append(" (")
				.Append(BackcolorIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/AREAFORMAT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_foregroundColor);
			out1.WriteInt(field_2_backgroundColor);
			out1.WriteShort(field_3_pattern);
			out1.WriteShort(field_4_formatFlags);
			out1.WriteShort(field_5_forecolorIndex);
			out1.WriteShort(field_6_backcolorIndex);
		}

		public override object Clone()
		{
			AreaFormatRecord areaFormatRecord = new AreaFormatRecord();
			areaFormatRecord.field_1_foregroundColor = field_1_foregroundColor;
			areaFormatRecord.field_2_backgroundColor = field_2_backgroundColor;
			areaFormatRecord.field_3_pattern = field_3_pattern;
			areaFormatRecord.field_4_formatFlags = field_4_formatFlags;
			areaFormatRecord.field_5_forecolorIndex = field_5_forecolorIndex;
			areaFormatRecord.field_6_backcolorIndex = field_6_backcolorIndex;
			return areaFormatRecord;
		}
	}
}
