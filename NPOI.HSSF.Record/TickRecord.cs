using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * The Tick record defines how tick marks and label positioning/formatting
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver(acoliver at apache.org)
	public class TickRecord : StandardRecord
	{
		public const short sid = 4126;

		private byte field_1_majorTickType;

		private byte field_2_minorTickType;

		private byte field_3_labelPosition;

		private byte field_4_background;

		private int field_5_labelColorRgb;

		private int field_6_zero1;

		private int field_7_zero2;

		private int field_8_zero3;

		private int field_9_zero4;

		private short field_10_options;

		private BitField autoTextColor = BitFieldFactory.GetInstance(1);

		private BitField autoTextBackground = BitFieldFactory.GetInstance(2);

		private BitField rotation = BitFieldFactory.GetInstance(28);

		private BitField autorotate = BitFieldFactory.GetInstance(32);

		private short field_11_tickColor;

		private short field_12_zero5;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 30;

		public override short Sid => 4126;

		/// Get the major tick type field for the Tick record.
		public byte MajorTickType
		{
			get
			{
				return field_1_majorTickType;
			}
			set
			{
				field_1_majorTickType = value;
			}
		}

		/// Get the minor tick type field for the Tick record.
		public byte MinorTickType
		{
			get
			{
				return field_2_minorTickType;
			}
			set
			{
				field_2_minorTickType = value;
			}
		}

		/// Get the label position field for the Tick record.
		public byte LabelPosition
		{
			get
			{
				return field_3_labelPosition;
			}
			set
			{
				field_3_labelPosition = value;
			}
		}

		/// Get the background field for the Tick record.
		public byte Background
		{
			get
			{
				return field_4_background;
			}
			set
			{
				field_4_background = value;
			}
		}

		/// Get the label color rgb field for the Tick record.
		public int LabelColorRgb
		{
			get
			{
				return field_5_labelColorRgb;
			}
			set
			{
				field_5_labelColorRgb = value;
			}
		}

		/// Get the zero 1 field for the Tick record.
		public int Zero1
		{
			get
			{
				return field_6_zero1;
			}
			set
			{
				field_6_zero1 = value;
			}
		}

		/// Get the zero 2 field for the Tick record.
		public int Zero2
		{
			get
			{
				return field_7_zero2;
			}
			set
			{
				field_7_zero2 = value;
			}
		}

		/// Get the options field for the Tick record.
		public short Options
		{
			get
			{
				return field_10_options;
			}
			set
			{
				field_10_options = value;
			}
		}

		/// Get the tick color field for the Tick record.
		public short TickColor
		{
			get
			{
				return field_11_tickColor;
			}
			set
			{
				field_11_tickColor = value;
			}
		}

		/// Get the zero 3 field for the Tick record.
		public short Zero3
		{
			get
			{
				return field_12_zero5;
			}
			set
			{
				field_12_zero5 = value;
			}
		}

		/// use the quote Unquote automatic color for text
		/// @return  the auto text color field value.
		public bool IsAutoTextColor
		{
			get
			{
				return autoTextColor.IsSet(field_10_options);
			}
			set
			{
				field_10_options = autoTextColor.SetShortBoolean(field_10_options, value);
			}
		}

		/// use the quote Unquote automatic color for text background
		/// @return  the auto text background field value.
		public bool IsAutoTextBackground
		{
			get
			{
				return autoTextBackground.IsSet(field_10_options);
			}
			set
			{
				field_10_options = autoTextBackground.SetShortBoolean(field_10_options, value);
			}
		}

		/// rotate text (0=none, 1=normal, 2=90 degrees counterclockwise, 3=90 degrees clockwise)
		/// @return  the rotation field value.
		public short Rotation
		{
			get
			{
				return rotation.GetShortValue(field_10_options);
			}
			set
			{
				field_10_options = rotation.SetShortValue(field_10_options, value);
			}
		}

		/// automatically rotate the text
		/// @return  the autorotate field value.
		public bool IsAutorotate
		{
			get
			{
				return autorotate.IsSet(field_10_options);
			}
			set
			{
				field_10_options = autorotate.SetShortBoolean(field_10_options, value);
			}
		}

		public TickRecord()
		{
		}

		/// Constructs a Tick record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public TickRecord(RecordInputStream in1)
		{
			field_1_majorTickType = (byte)in1.ReadByte();
			field_2_minorTickType = (byte)in1.ReadByte();
			field_3_labelPosition = (byte)in1.ReadByte();
			field_4_background = (byte)in1.ReadByte();
			field_5_labelColorRgb = (byte)in1.ReadInt();
			field_6_zero1 = in1.ReadInt();
			field_7_zero2 = in1.ReadInt();
			field_8_zero3 = in1.ReadInt();
			field_9_zero4 = in1.ReadInt();
			field_10_options = in1.ReadShort();
			field_11_tickColor = in1.ReadShort();
			field_12_zero5 = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TICK]\n");
			stringBuilder.Append("    .majorTickType        = ").Append("0x").Append(HexDump.ToHex(MajorTickType))
				.Append(" (")
				.Append(MajorTickType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .minorTickType        = ").Append("0x").Append(HexDump.ToHex(MinorTickType))
				.Append(" (")
				.Append(MinorTickType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .labelPosition        = ").Append("0x").Append(HexDump.ToHex(LabelPosition))
				.Append(" (")
				.Append(LabelPosition)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .background           = ").Append("0x").Append(HexDump.ToHex(Background))
				.Append(" (")
				.Append(Background)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .labelColorRgb        = ").Append("0x").Append(HexDump.ToHex(LabelColorRgb))
				.Append(" (")
				.Append(LabelColorRgb)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .zero1                = ").Append("0x").Append(HexDump.ToHex(Zero1))
				.Append(" (")
				.Append(Zero1)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .zero2                = ").Append("0x").Append(HexDump.ToHex(Zero2))
				.Append(" (")
				.Append(Zero2)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .autoTextColor            = ").Append(IsAutoTextColor).Append('\n');
			stringBuilder.Append("         .autoTextBackground       = ").Append(IsAutoTextBackground).Append('\n');
			stringBuilder.Append("         .rotation                 = ").Append(Rotation).Append('\n');
			stringBuilder.Append("         .autorotate               = ").Append(IsAutorotate).Append('\n');
			stringBuilder.Append("    .tickColor            = ").Append("0x").Append(HexDump.ToHex(TickColor))
				.Append(" (")
				.Append(TickColor)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .zero3                = ").Append("0x").Append(HexDump.ToHex(Zero3))
				.Append(" (")
				.Append(Zero3)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/TICK]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(field_1_majorTickType);
			out1.WriteByte(field_2_minorTickType);
			out1.WriteByte(field_3_labelPosition);
			out1.WriteByte(field_4_background);
			out1.WriteInt(field_5_labelColorRgb);
			out1.WriteInt(field_6_zero1);
			out1.WriteInt(field_7_zero2);
			out1.WriteInt(field_8_zero3);
			out1.WriteInt(field_9_zero4);
			out1.WriteShort(field_10_options);
			out1.WriteShort(field_11_tickColor);
			out1.WriteShort(field_12_zero5);
		}

		public override object Clone()
		{
			TickRecord tickRecord = new TickRecord();
			tickRecord.field_1_majorTickType = field_1_majorTickType;
			tickRecord.field_2_minorTickType = field_2_minorTickType;
			tickRecord.field_3_labelPosition = field_3_labelPosition;
			tickRecord.field_4_background = field_4_background;
			tickRecord.field_5_labelColorRgb = field_5_labelColorRgb;
			tickRecord.field_6_zero1 = field_6_zero1;
			tickRecord.field_7_zero2 = field_7_zero2;
			tickRecord.field_8_zero3 = field_8_zero3;
			tickRecord.field_9_zero4 = field_9_zero4;
			tickRecord.field_10_options = field_10_options;
			tickRecord.field_11_tickColor = field_11_tickColor;
			tickRecord.field_12_zero5 = field_12_zero5;
			return tickRecord;
		}
	}
}
