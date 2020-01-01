using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The frame record indicates whether there is a border around the Displayed text of a chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class FrameRecord : StandardRecord
	{
		public const short sid = 4146;

		public const short BORDER_TYPE_REGULAR = 0;

		public const short BORDER_TYPE_SHADOW = 1;

		private short field_1_borderType;

		private short field_2_options;

		private BitField autoSize = BitFieldFactory.GetInstance(1);

		private BitField autoPosition = BitFieldFactory.GetInstance(2);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 4;

		public override short Sid => 4146;

		/// Get the border type field for the Frame record.
		///
		/// @return  One of 
		///        BORDER_TYPE_REGULAR
		///        BORDER_TYPE_SHADOW
		public short BorderType
		{
			get
			{
				return field_1_borderType;
			}
			set
			{
				field_1_borderType = value;
			}
		}

		/// Get the options field for the Frame record.
		public short Options
		{
			get
			{
				return field_2_options;
			}
			set
			{
				field_2_options = value;
			}
		}

		/// excel calculates the size automatically if true
		/// @return  the auto size field value.
		public bool IsAutoSize
		{
			get
			{
				return autoSize.IsSet(field_2_options);
			}
			set
			{
				field_2_options = autoSize.SetShortBoolean(field_2_options, value);
			}
		}

		/// excel calculates the position automatically
		/// @return  the auto position field value.
		public bool IsAutoPosition
		{
			get
			{
				return autoPosition.IsSet(field_2_options);
			}
			set
			{
				field_2_options = autoPosition.SetShortBoolean(field_2_options, value);
			}
		}

		public FrameRecord()
		{
		}

		/// Constructs a Frame record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public FrameRecord(RecordInputStream in1)
		{
			field_1_borderType = in1.ReadShort();
			field_2_options = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FRAME]\n");
			stringBuilder.Append("    .borderType           = ").Append("0x").Append(HexDump.ToHex(BorderType))
				.Append(" (")
				.Append(BorderType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .autoSize                 = ").Append(IsAutoSize).Append('\n');
			stringBuilder.Append("         .autoPosition             = ").Append(IsAutoPosition).Append('\n');
			stringBuilder.Append("[/FRAME]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_borderType);
			out1.WriteShort(field_2_options);
		}

		public override object Clone()
		{
			FrameRecord frameRecord = new FrameRecord();
			frameRecord.field_1_borderType = field_1_borderType;
			frameRecord.field_2_options = field_2_options;
			return frameRecord;
		}
	}
}
