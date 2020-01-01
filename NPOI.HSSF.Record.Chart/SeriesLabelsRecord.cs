using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The series label record defines the type of label associated with the data format record.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class SeriesLabelsRecord : StandardRecord
	{
		public static short sid = 4108;

		private short field_1_formatFlags;

		private BitField showActual = BitFieldFactory.GetInstance(1);

		private BitField showPercent = BitFieldFactory.GetInstance(2);

		private BitField labelAsPercentage = BitFieldFactory.GetInstance(4);

		private BitField smoothedLine = BitFieldFactory.GetInstance(8);

		private BitField showLabel = BitFieldFactory.GetInstance(16);

		private BitField showBubbleSizes = BitFieldFactory.GetInstance(32);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => sid;

		/// Get the format flags field for the SeriesLabels record.
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

		/// show actual value of the data point
		/// @return  the show actual field value.
		public bool IsShowActual
		{
			get
			{
				return showActual.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = showActual.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// show value as percentage of total (pie charts only)
		/// @return  the show percent field value.
		public bool IsShowPercent
		{
			get
			{
				return showPercent.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = showPercent.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// show category label/value as percentage (pie charts only)
		/// @return  the label as percentage field value.
		public bool IsLabelAsPercentage
		{
			get
			{
				return labelAsPercentage.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = labelAsPercentage.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// show smooth line
		/// @return  the smoothed line field value.
		public bool IsSmoothedLine
		{
			get
			{
				return smoothedLine.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = smoothedLine.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// Display category label
		/// @return  the show label field value.
		public bool IsShowLabel
		{
			get
			{
				return showLabel.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = showLabel.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		/// ??
		/// @return  the show bubble sizes field value.
		public bool IsShowBubbleSizes
		{
			get
			{
				return showBubbleSizes.IsSet(field_1_formatFlags);
			}
			set
			{
				field_1_formatFlags = showBubbleSizes.SetShortBoolean(field_1_formatFlags, value);
			}
		}

		public SeriesLabelsRecord()
		{
		}

		/// Constructs a SeriesLabels record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesLabelsRecord(RecordInputStream in1)
		{
			field_1_formatFlags = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ATTACHEDLABEL]\n");
			stringBuilder.Append("    .formatFlags          = ").Append("0x").Append(HexDump.ToHex(FormatFlags))
				.Append(" (")
				.Append(FormatFlags)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .showActual               = ").Append(IsShowActual).Append('\n');
			stringBuilder.Append("         .showPercent              = ").Append(IsShowPercent).Append('\n');
			stringBuilder.Append("         .labelAsPercentage        = ").Append(IsLabelAsPercentage).Append('\n');
			stringBuilder.Append("         .smoothedLine             = ").Append(IsSmoothedLine).Append('\n');
			stringBuilder.Append("         .showLabel                = ").Append(IsShowLabel).Append('\n');
			stringBuilder.Append("         .showBubbleSizes          = ").Append(IsShowBubbleSizes).Append('\n');
			stringBuilder.Append("[/ATTACHEDLABEL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_formatFlags);
		}

		public override object Clone()
		{
			SeriesLabelsRecord seriesLabelsRecord = new SeriesLabelsRecord();
			seriesLabelsRecord.field_1_formatFlags = field_1_formatFlags;
			return seriesLabelsRecord;
		}
	}
}
