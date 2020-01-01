using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The plot growth record specifies the scaling factors used when a font is scaled.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class PlotGrowthRecord : StandardRecord
	{
		public const short sid = 4196;

		private int field_1_horizontalScale;

		private int field_2_verticalScale;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 8;

		public override short Sid => 4196;

		/// Get the horizontalScale field for the PlotGrowth record.
		public int HorizontalScale
		{
			get
			{
				return field_1_horizontalScale;
			}
			set
			{
				field_1_horizontalScale = value;
			}
		}

		/// Get the verticalScale field for the PlotGrowth record.
		public int VerticalScale
		{
			get
			{
				return field_2_verticalScale;
			}
			set
			{
				field_2_verticalScale = value;
			}
		}

		public PlotGrowthRecord()
		{
		}

		/// Constructs a PlotGrowth record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public PlotGrowthRecord(RecordInputStream in1)
		{
			field_1_horizontalScale = in1.ReadInt();
			field_2_verticalScale = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PLOTGROWTH]\n");
			stringBuilder.Append("    .horizontalScale      = ").Append("0x").Append(HexDump.ToHex(HorizontalScale))
				.Append(" (")
				.Append(HorizontalScale)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .verticalScale        = ").Append("0x").Append(HexDump.ToHex(VerticalScale))
				.Append(" (")
				.Append(VerticalScale)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/PLOTGROWTH]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_horizontalScale);
			out1.WriteInt(field_2_verticalScale);
		}

		public override object Clone()
		{
			PlotGrowthRecord plotGrowthRecord = new PlotGrowthRecord();
			plotGrowthRecord.field_1_horizontalScale = field_1_horizontalScale;
			plotGrowthRecord.field_2_verticalScale = field_2_verticalScale;
			return plotGrowthRecord;
		}
	}
}
