using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The series chart Group index record stores the index to the CHARTFORMAT record (0 based).
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class SeriesChartGroupIndexRecord : StandardRecord
	{
		public static short sid = 4165;

		private short field_1_chartGroupIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => sid;

		/// Get the chart Group index field for the SeriesChartGroupIndex record.
		public short ChartGroupIndex
		{
			get
			{
				return field_1_chartGroupIndex;
			}
			set
			{
				field_1_chartGroupIndex = value;
			}
		}

		public SeriesChartGroupIndexRecord()
		{
		}

		/// Constructs a SeriesChartGroupIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesChartGroupIndexRecord(RecordInputStream in1)
		{
			field_1_chartGroupIndex = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SERTOCRT]\n");
			stringBuilder.Append("    .chartGroupIndex      = ").Append("0x").Append(HexDump.ToHex(ChartGroupIndex))
				.Append(" (")
				.Append(ChartGroupIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SERTOCRT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_chartGroupIndex);
		}

		public override object Clone()
		{
			SeriesChartGroupIndexRecord seriesChartGroupIndexRecord = new SeriesChartGroupIndexRecord();
			seriesChartGroupIndexRecord.field_1_chartGroupIndex = field_1_chartGroupIndex;
			return seriesChartGroupIndexRecord;
		}
	}
}
