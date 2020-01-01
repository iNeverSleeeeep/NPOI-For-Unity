using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * links a series to its position in the series list.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class SeriesIndexRecord : StandardRecord
	{
		public const short sid = 4197;

		private short field_1_index;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4197;

		/// Get the index field for the SeriesIndex record.
		public short Index
		{
			get
			{
				return field_1_index;
			}
			set
			{
				field_1_index = value;
			}
		}

		public SeriesIndexRecord()
		{
		}

		/// Constructs a SeriesIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesIndexRecord(RecordInputStream in1)
		{
			field_1_index = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SINDEX]\n");
			stringBuilder.Append("    .index                = ").Append("0x").Append(HexDump.ToHex(Index))
				.Append(" (")
				.Append(Index)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SINDEX]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_index);
		}

		public override object Clone()
		{
			SeriesIndexRecord seriesIndexRecord = new SeriesIndexRecord();
			seriesIndexRecord.field_1_index = field_1_index;
			return seriesIndexRecord;
		}
	}
}
