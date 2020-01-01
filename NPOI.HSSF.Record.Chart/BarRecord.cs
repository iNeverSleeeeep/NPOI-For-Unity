using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The bar record is used to define a bar chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class BarRecord : StandardRecord
	{
		public const short sid = 4119;

		private short field_1_barSpace;

		private short field_2_categorySpace;

		private short field_3_formatFlags;

		private BitField horizontal = BitFieldFactory.GetInstance(1);

		private BitField stacked = BitFieldFactory.GetInstance(2);

		private BitField DisplayAsPercentage = BitFieldFactory.GetInstance(4);

		private BitField shadow = BitFieldFactory.GetInstance(8);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 6;

		public override short Sid => 4119;

		/// the bar space field for the Bar record.
		public short BarSpace
		{
			get
			{
				return field_1_barSpace;
			}
			set
			{
				field_1_barSpace = value;
			}
		}

		/// the category space field for the Bar record.
		public short CategorySpace
		{
			get
			{
				return field_2_categorySpace;
			}
			set
			{
				field_2_categorySpace = value;
			}
		}

		/// the format flags field for the Bar record.
		public short FormatFlags
		{
			get
			{
				return field_3_formatFlags;
			}
			set
			{
				field_3_formatFlags = value;
			}
		}

		/// true to Display horizontal bar charts, false for vertical
		/// @return  the horizontal field value.
		public bool IsHorizontal
		{
			get
			{
				return horizontal.IsSet(field_3_formatFlags);
			}
			set
			{
				field_3_formatFlags = horizontal.SetShortBoolean(field_3_formatFlags, value);
			}
		}

		/// stack Displayed values
		/// @return  the stacked field value.
		public bool IsStacked
		{
			get
			{
				return stacked.IsSet(field_3_formatFlags);
			}
			set
			{
				field_3_formatFlags = stacked.SetShortBoolean(field_3_formatFlags, value);
			}
		}

		/// Display chart values as a percentage
		/// @return  the Display as percentage field value.
		public bool IsDisplayAsPercentage
		{
			get
			{
				return DisplayAsPercentage.IsSet(field_3_formatFlags);
			}
			set
			{
				field_3_formatFlags = DisplayAsPercentage.SetShortBoolean(field_3_formatFlags, value);
			}
		}

		/// Display a shadow for the chart
		/// @return  the shadow field value.
		public bool IsShadow
		{
			get
			{
				return shadow.IsSet(field_3_formatFlags);
			}
			set
			{
				field_3_formatFlags = shadow.SetShortBoolean(field_3_formatFlags, value);
			}
		}

		public BarRecord()
		{
		}

		/// Constructs a Bar record and s its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public BarRecord(RecordInputStream in1)
		{
			field_1_barSpace = in1.ReadShort();
			field_2_categorySpace = in1.ReadShort();
			field_3_formatFlags = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BAR]\n");
			stringBuilder.Append("    .barSpace             = ").Append("0x").Append(HexDump.ToHex(BarSpace))
				.Append(" (")
				.Append(BarSpace)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .categorySpace        = ").Append("0x").Append(HexDump.ToHex(CategorySpace))
				.Append(" (")
				.Append(CategorySpace)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .formatFlags          = ").Append("0x").Append(HexDump.ToHex(FormatFlags))
				.Append(" (")
				.Append(FormatFlags)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .horizontal               = ").Append(IsHorizontal).Append('\n');
			stringBuilder.Append("         .stacked                  = ").Append(IsStacked).Append('\n');
			stringBuilder.Append("         .DisplayAsPercentage      = ").Append(IsDisplayAsPercentage).Append('\n');
			stringBuilder.Append("         .shadow                   = ").Append(IsShadow).Append('\n');
			stringBuilder.Append("[/BAR]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_barSpace);
			out1.WriteShort(field_2_categorySpace);
			out1.WriteShort(field_3_formatFlags);
		}

		public override object Clone()
		{
			BarRecord barRecord = new BarRecord();
			barRecord.field_1_barSpace = field_1_barSpace;
			barRecord.field_2_categorySpace = field_2_categorySpace;
			barRecord.field_3_formatFlags = field_3_formatFlags;
			return barRecord;
		}
	}
}
