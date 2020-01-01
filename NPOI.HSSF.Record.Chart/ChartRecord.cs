using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The chart record is used to define the location and size of a chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class ChartRecord : StandardRecord
	{
		public const short sid = 4098;

		private int field_1_x;

		private int field_2_y;

		private int field_3_width;

		private int field_4_height;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 16;

		public override short Sid => 4098;

		/// Get the x field for the Chart record.
		public int X
		{
			get
			{
				return field_1_x;
			}
			set
			{
				field_1_x = value;
			}
		}

		/// Get the y field for the Chart record.
		public int Y
		{
			get
			{
				return field_2_y;
			}
			set
			{
				field_2_y = value;
			}
		}

		/// Get the width field for the Chart record.
		public int Width
		{
			get
			{
				return field_3_width;
			}
			set
			{
				field_3_width = value;
			}
		}

		/// Get the height field for the Chart record.
		public int Height
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

		public ChartRecord()
		{
		}

		/// Constructs a Chart record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public ChartRecord(RecordInputStream in1)
		{
			field_1_x = in1.ReadInt();
			field_2_y = in1.ReadInt();
			field_3_width = in1.ReadInt();
			field_4_height = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CHART]\n");
			stringBuilder.Append("    .x                    = ").Append("0x").Append(HexDump.ToHex(X))
				.Append(" (")
				.Append(X)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .y                    = ").Append("0x").Append(HexDump.ToHex(Y))
				.Append(" (")
				.Append(Y)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .width                = ").Append("0x").Append(HexDump.ToHex(Width))
				.Append(" (")
				.Append(Width)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .height               = ").Append("0x").Append(HexDump.ToHex(Height))
				.Append(" (")
				.Append(Height)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/CHART]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_x);
			out1.WriteInt(field_2_y);
			out1.WriteInt(field_3_width);
			out1.WriteInt(field_4_height);
		}

		public override object Clone()
		{
			ChartRecord chartRecord = new ChartRecord();
			chartRecord.field_1_x = field_1_x;
			chartRecord.field_2_y = field_2_y;
			chartRecord.field_3_width = field_3_width;
			chartRecord.field_4_height = field_4_height;
			return chartRecord;
		}
	}
}
