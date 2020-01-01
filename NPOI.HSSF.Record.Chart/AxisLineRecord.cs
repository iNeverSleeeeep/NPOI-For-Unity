using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The AxisLine record specifies which part of the axis (section 2.2.3.6) is 
	/// specified by the LineFormat record (section 2.4.156) that follows.
	///
	/// Excel Binary File Format (.xls) Structure Specification 
	/// </summary>
	public class AxisLineRecord : StandardRecord
	{
		public const short sid = 4129;

		public const short AXIS_TYPE_AXIS_LINE = 0;

		public const short AXIS_TYPE_MAJOR_GRID_LINE = 1;

		public const short AXIS_TYPE_MINOR_GRID_LINE = 2;

		public const short AXIS_TYPE_WALLS_OR_FLOOR = 3;

		private short field_1_axisType;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4129;

		/// <summary>
		///
		/// </summary>
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

		public AxisLineRecord()
		{
		}

		/// Constructs a AxisLineFormat record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AxisLineRecord(RecordInputStream in1)
		{
			field_1_axisType = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AXISLINEFORMAT]\n");
			stringBuilder.Append("    .axisType             = ").Append("0x").Append(HexDump.ToHex(AxisType))
				.Append(" (")
				.Append(AxisType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/AXISLINEFORMAT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_axisType);
		}

		public override object Clone()
		{
			AxisLineRecord axisLineRecord = new AxisLineRecord();
			axisLineRecord.field_1_axisType = field_1_axisType;
			return axisLineRecord;
		}
	}
}
