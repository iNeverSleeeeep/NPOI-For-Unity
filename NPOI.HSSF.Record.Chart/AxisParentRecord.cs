using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The axis size and location
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class AxisParentRecord : StandardRecord
	{
		public const short sid = 4161;

		public const short AXIS_TYPE_MAIN = 0;

		public const short AXIS_TYPE_SECONDARY = 1;

		private short field_1_axisType;

		private int field_2_x;

		private int field_3_y;

		private int field_4_width;

		private int field_5_height;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 18;

		public override short Sid => 4161;

		/// the axis type field for the AxisParent record.
		///
		///              @return  One of 
		///       AXIS_TYPE_MAIN
		///       AXIS_TYPE_SECONDARY
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

		/// the x field for the AxisParent record.
		public int X
		{
			get
			{
				return field_2_x;
			}
			set
			{
				field_2_x = value;
			}
		}

		/// the y field for the AxisParent record.
		public int Y
		{
			get
			{
				return field_3_y;
			}
			set
			{
				field_3_y = value;
			}
		}

		/// the width field for the AxisParent record.
		public int Width
		{
			get
			{
				return field_4_width;
			}
			set
			{
				field_4_width = value;
			}
		}

		/// the height field for the AxisParent record.
		public int Height
		{
			get
			{
				return field_5_height;
			}
			set
			{
				field_5_height = value;
			}
		}

		public AxisParentRecord()
		{
		}

		/// Constructs a AxisParent record and s its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AxisParentRecord(RecordInputStream in1)
		{
			field_1_axisType = in1.ReadShort();
			field_2_x = in1.ReadInt();
			field_3_y = in1.ReadInt();
			field_4_width = in1.ReadInt();
			field_5_height = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AXISPARENT]\n");
			stringBuilder.Append("    .axisType             = ").Append("0x").Append(HexDump.ToHex(AxisType))
				.Append(" (")
				.Append(AxisType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
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
			stringBuilder.Append("[/AXISPARENT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_axisType);
			out1.WriteInt(field_2_x);
			out1.WriteInt(field_3_y);
			out1.WriteInt(field_4_width);
			out1.WriteInt(field_5_height);
		}

		public override object Clone()
		{
			AxisParentRecord axisParentRecord = new AxisParentRecord();
			axisParentRecord.field_1_axisType = field_1_axisType;
			axisParentRecord.field_2_x = field_2_x;
			axisParentRecord.field_3_y = field_3_y;
			axisParentRecord.field_4_width = field_4_width;
			axisParentRecord.field_5_height = field_5_height;
			return axisParentRecord;
		}
	}
}
