using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	public class Chart3DBarShapeRecord : StandardRecord
	{
		public const short sid = 4191;

		private byte field_1_riser;

		private byte field_2_taper;

		protected override int DataSize => 2;

		public override short Sid => 4191;

		/// <summary>
		/// the shape of the base of the data points in a bar or column chart group. 
		/// MUST be a value from the following table
		/// 0x00      The base of the data point is a rectangle.
		/// 0x01      The base of the data point is an ellipse.
		/// </summary>
		public byte Riser
		{
			get
			{
				return field_1_riser;
			}
			set
			{
				field_1_riser = value;
			}
		}

		/// <summary>
		/// how the data points in a bar or column chart group taper from base to tip. 
		/// MUST be a value from the following
		/// 0x00    The data points of the bar or column chart group do not taper. 
		///         The shape at the maximum value of the data point is the same as the shape at the base.:
		/// 0x01    The data points of the bar or column chart group taper to a point at the maximum value of each data point.
		/// 0x02    The data points of the bar or column chart group taper towards a projected point at the position of 
		///         the maximum value of all of the data points in the chart group, but are clipped at the value of each data point.
		/// </summary>
		public byte Taper
		{
			get
			{
				return field_2_taper;
			}
			set
			{
				field_2_taper = value;
			}
		}

		public Chart3DBarShapeRecord()
		{
		}

		public Chart3DBarShapeRecord(RecordInputStream in1)
		{
			field_1_riser = (byte)in1.ReadByte();
			field_2_taper = (byte)in1.ReadByte();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[Chart3DBarShape]\n");
			stringBuilder.Append("    .axisType             = ").Append("0x").Append(HexDump.ToHex(Riser))
				.Append(" (")
				.Append(Riser)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .x                    = ").Append("0x").Append(HexDump.ToHex(Taper))
				.Append(" (")
				.Append(Taper)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/Chart3DBarShape]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(field_1_riser);
			out1.WriteByte(field_2_taper);
		}

		public override object Clone()
		{
			Chart3DBarShapeRecord chart3DBarShapeRecord = new Chart3DBarShapeRecord();
			chart3DBarShapeRecord.Riser = Riser;
			chart3DBarShapeRecord.Taper = Taper;
			return chart3DBarShapeRecord;
		}
	}
}
