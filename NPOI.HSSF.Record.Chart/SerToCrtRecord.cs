using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The SerToCrt record specifies the chart group for the current series.
	/// </summary>
	public class SerToCrtRecord : StandardRecord
	{
		public const short sid = 4165;

		private short field_1_chartGroupIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4165;

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

		public SerToCrtRecord()
		{
		}

		/// Constructs a SeriesChartGroupIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SerToCrtRecord(RecordInputStream in1)
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
			SerToCrtRecord serToCrtRecord = new SerToCrtRecord();
			serToCrtRecord.field_1_chartGroupIndex = field_1_chartGroupIndex;
			return serToCrtRecord;
		}
	}
}
