using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * Indicates the chart-group index for a series.  The order probably defines the mapping.  So the 0th record probably means the 0th series.  The only field in this of course defines which chart Group the 0th series (for instance) would map to.  Confusing?  Well thats because it Is.  (p 522 BCG)
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	[Obsolete("duplication record, see SerToCrtRecord")]
	public class SeriesToChartGroupRecord : StandardRecord
	{
		public const short sid = 4165;

		private short field_1_chartGroupIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4165;

		/// Get the chart Group index field for the SeriesToChartGroup record.
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

		public SeriesToChartGroupRecord()
		{
		}

		/// Constructs a SeriesToChartGroup record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesToChartGroupRecord(RecordInputStream in1)
		{
			field_1_chartGroupIndex = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SeriesToChartGroup]\n");
			stringBuilder.Append("    .chartGroupIndex      = ").Append("0x").Append(HexDump.ToHex(ChartGroupIndex))
				.Append(" (")
				.Append(ChartGroupIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SeriesToChartGroup]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_chartGroupIndex);
		}

		public override object Clone()
		{
			SeriesToChartGroupRecord seriesToChartGroupRecord = new SeriesToChartGroupRecord();
			seriesToChartGroupRecord.field_1_chartGroupIndex = field_1_chartGroupIndex;
			return seriesToChartGroupRecord;
		}
	}
}
