using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The number of axes used on a chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class AxesUsedRecord : StandardRecord
	{
		public const short sid = 4166;

		private short field_1_numAxis;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4166;

		/// Get the num axis field for the AxisUsed record.
		public short NumAxis
		{
			get
			{
				return field_1_numAxis;
			}
			set
			{
				field_1_numAxis = value;
			}
		}

		public AxesUsedRecord()
		{
		}

		/// Constructs a AxisUsed record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public AxesUsedRecord(RecordInputStream in1)
		{
			field_1_numAxis = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AXISUSED]\n");
			stringBuilder.Append("    .numAxis              = ").Append("0x").Append(HexDump.ToHex(NumAxis))
				.Append(" (")
				.Append(NumAxis)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/AXISUSED]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_numAxis);
		}

		public override object Clone()
		{
			AxesUsedRecord axesUsedRecord = new AxesUsedRecord();
			axesUsedRecord.field_1_numAxis = field_1_numAxis;
			return axesUsedRecord;
		}
	}
}
