using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The dat record is used to store options for the chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class DatRecord : StandardRecord
	{
		public const short sid = 4195;

		private short field_1_options;

		private BitField horizontalBorder = BitFieldFactory.GetInstance(1);

		private BitField verticalBorder = BitFieldFactory.GetInstance(2);

		private BitField border = BitFieldFactory.GetInstance(4);

		private BitField showSeriesKey = BitFieldFactory.GetInstance(8);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4195;

		/// Get the options field for the Dat record.
		public short Options
		{
			get
			{
				return field_1_options;
			}
			set
			{
				field_1_options = value;
			}
		}

		public DatRecord()
		{
		}

		/// Constructs a Dat record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public DatRecord(RecordInputStream in1)
		{
			field_1_options = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DAT]\n");
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .horizontalBorder         = ").Append(IsHorizontalBorder()).Append('\n');
			stringBuilder.Append("         .verticalBorder           = ").Append(IsVerticalBorder()).Append('\n');
			stringBuilder.Append("         .border                   = ").Append(IsBorder()).Append('\n');
			stringBuilder.Append("         .showSeriesKey            = ").Append(IsShowSeriesKey()).Append('\n');
			stringBuilder.Append("[/DAT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_options);
		}

		public override object Clone()
		{
			DatRecord datRecord = new DatRecord();
			datRecord.field_1_options = field_1_options;
			return datRecord;
		}

		/// Sets the horizontal border field value.
		/// has a horizontal border
		public void SetHorizontalBorder(bool value)
		{
			field_1_options = horizontalBorder.SetShortBoolean(field_1_options, value);
		}

		/// has a horizontal border
		/// @return  the horizontal border field value.
		public bool IsHorizontalBorder()
		{
			return horizontalBorder.IsSet(field_1_options);
		}

		/// Sets the vertical border field value.
		/// has vertical border
		public void SetVerticalBorder(bool value)
		{
			field_1_options = verticalBorder.SetShortBoolean(field_1_options, value);
		}

		/// has vertical border
		/// @return  the vertical border field value.
		public bool IsVerticalBorder()
		{
			return verticalBorder.IsSet(field_1_options);
		}

		/// Sets the border field value.
		/// data table has a border
		public void SetBorder(bool value)
		{
			field_1_options = border.SetShortBoolean(field_1_options, value);
		}

		/// data table has a border
		/// @return  the border field value.
		public bool IsBorder()
		{
			return border.IsSet(field_1_options);
		}

		/// Sets the show series key field value.
		/// shows the series key
		public void SetShowSeriesKey(bool value)
		{
			field_1_options = showSeriesKey.SetShortBoolean(field_1_options, value);
		}

		/// shows the series key
		/// @return  the show series key field value.
		public bool IsShowSeriesKey()
		{
			return showSeriesKey.IsSet(field_1_options);
		}
	}
}
