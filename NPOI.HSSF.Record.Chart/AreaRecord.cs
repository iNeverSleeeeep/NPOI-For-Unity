using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The area record is used to define a area chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class AreaRecord : StandardRecord
	{
		public const short sid = 4122;

		private short field_1_formatFlags;

		private BitField stacked = BitFieldFactory.GetInstance(1);

		private BitField DisplayAsPercentage = BitFieldFactory.GetInstance(2);

		private BitField shadow = BitFieldFactory.GetInstance(4);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4122;

		/// the format flags field for the Area record.
		public short FormatFlags
		{
			get
			{
				return field_1_formatFlags;
			}
			set
			{
				field_1_formatFlags = value;
			}
		}

		/// series is stacked
		/// @return  the stacked field value.
		public bool IsStacked
		{
			get
			{
				return stacked.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = stacked.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// results Displayed as percentages
		/// @return  the Display as percentage field value.
		public bool IsDisplayAsPercentage
		{
			get
			{
				return DisplayAsPercentage.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = DisplayAsPercentage.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// Display a shadow for the chart
		/// @return  the shadow field value.
		public bool IsShadow
		{
			get
			{
				return shadow.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = shadow.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		public AreaRecord()
		{
		}

		/// Constructs a Area record and s its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AreaRecord(RecordInputStream in1)
		{
			field_1_formatFlags = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AREA]\n");
			stringBuilder.Append("    .formatFlags          = ").Append("0x").Append(HexDump.ToHex(FormatFlags))
				.Append(" (")
				.Append(FormatFlags)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .stacked                  = ").Append(IsStacked).Append('\n');
			stringBuilder.Append("         .DisplayAsPercentage      = ").Append(IsDisplayAsPercentage).Append('\n');
			stringBuilder.Append("         .shadow                   = ").Append(IsShadow).Append('\n');
			stringBuilder.Append("[/AREA]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_formatFlags);
		}

		public override object Clone()
		{
			AreaRecord areaRecord = new AreaRecord();
			areaRecord.field_1_formatFlags = field_1_formatFlags;
			return areaRecord;
		}
	}
}
