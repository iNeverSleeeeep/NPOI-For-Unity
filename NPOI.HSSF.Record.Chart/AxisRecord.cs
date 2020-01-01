using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The axis record defines the type of an axis.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class AxisRecord : StandardRecord
	{
		public const short sid = 4125;

		public const short AXIS_TYPE_CATEGORY_OR_X_AXIS = 0;

		public const short AXIS_TYPE_VALUE_AXIS = 1;

		public const short AXIS_TYPE_SERIES_AXIS = 2;

		private short field_1_axisType;

		private int field_2_reserved1;

		private int field_3_reserved2;

		private int field_4_reserved3;

		private int field_5_reserved4;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 18;

		public override short Sid => 4125;

		/// Get the axis type field for the Axis record.
		///
		/// @return  One of 
		///        AXIS_TYPE_CATEGORY_OR_X_AXIS
		///        AXIS_TYPE_VALUE_AXIS
		///        AXIS_TYPE_SERIES_AXIS
		public short AxisType
		{
			get
			{
				return field_1_axisType;
			}
			set
			{
				field_1_axisType = value;
			}
		}

		/// Get the reserved1 field for the Axis record.
		public int Reserved1
		{
			get
			{
				return field_2_reserved1;
			}
			set
			{
				field_2_reserved1 = value;
			}
		}

		/// Get the reserved2 field for the Axis record.
		public int Reserved2
		{
			get
			{
				return field_3_reserved2;
			}
			set
			{
				field_3_reserved2 = value;
			}
		}

		/// Get the reserved3 field for the Axis record.
		public int Reserved3
		{
			get
			{
				return field_4_reserved3;
			}
			set
			{
				field_4_reserved3 = value;
			}
		}

		/// Get the reserved4 field for the Axis record.
		public int Reserved4
		{
			get
			{
				return field_5_reserved4;
			}
			set
			{
				field_5_reserved4 = value;
			}
		}

		public AxisRecord()
		{
		}

		/// Constructs a Axis record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AxisRecord(RecordInputStream in1)
		{
			field_1_axisType = in1.ReadShort();
			field_2_reserved1 = in1.ReadInt();
			field_3_reserved2 = in1.ReadInt();
			field_4_reserved3 = in1.ReadInt();
			field_5_reserved4 = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AXIS]\n");
			stringBuilder.Append("    .axisType             = ").Append("0x").Append(HexDump.ToHex(AxisType))
				.Append(" (")
				.Append(AxisType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved1            = ").Append("0x").Append(HexDump.ToHex(Reserved1))
				.Append(" (")
				.Append(Reserved1)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved2            = ").Append("0x").Append(HexDump.ToHex(Reserved2))
				.Append(" (")
				.Append(Reserved2)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved3            = ").Append("0x").Append(HexDump.ToHex(Reserved3))
				.Append(" (")
				.Append(Reserved3)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved4            = ").Append("0x").Append(HexDump.ToHex(Reserved4))
				.Append(" (")
				.Append(Reserved4)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/AXIS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_axisType);
			out1.WriteInt(field_2_reserved1);
			out1.WriteInt(field_3_reserved2);
			out1.WriteInt(field_4_reserved3);
			out1.WriteInt(field_5_reserved4);
		}

		public override object Clone()
		{
			AxisRecord axisRecord = new AxisRecord();
			axisRecord.field_1_axisType = field_1_axisType;
			axisRecord.field_2_reserved1 = field_2_reserved1;
			axisRecord.field_3_reserved2 = field_3_reserved2;
			axisRecord.field_4_reserved3 = field_4_reserved3;
			axisRecord.field_5_reserved4 = field_5_reserved4;
			return axisRecord;
		}
	}
}
