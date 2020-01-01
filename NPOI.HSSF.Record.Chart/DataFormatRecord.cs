using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The data format record is used to index into a series.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class DataFormatRecord : StandardRecord
	{
		public const short sid = 4102;

		private short field_1_pointNumber;

		private short field_2_seriesIndex;

		private short field_3_seriesNumber;

		private short field_4_formatFlags;

		private BitField useExcel4Colors = BitFieldFactory.GetInstance(1);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 8;

		public override short Sid => 4102;

		/// Get the point number field for the DataFormat record.
		public short PointNumber
		{
			get
			{
				return field_1_pointNumber;
			}
			set
			{
				field_1_pointNumber = value;
			}
		}

		/// Get the series index field for the DataFormat record.
		public short SeriesIndex
		{
			get
			{
				return field_2_seriesIndex;
			}
			set
			{
				field_2_seriesIndex = value;
			}
		}

		/// Get the series number field for the DataFormat record.
		public short SeriesNumber
		{
			get
			{
				return field_3_seriesNumber;
			}
			set
			{
				field_3_seriesNumber = value;
			}
		}

		/// Get the format flags field for the DataFormat record.
		public short FormatFlags
		{
			get
			{
				return field_4_formatFlags;
			}
			set
			{
				field_4_formatFlags = value;
			}
		}

		/// Set true to use excel 4 colors.
		/// @return  the use excel 4 colors field value.
		public bool UseExcel4Colors
		{
			get
			{
				return useExcel4Colors.IsSet(field_4_formatFlags);
			}
			set
			{
				field_4_formatFlags = useExcel4Colors.SetShortBoolean(field_4_formatFlags, value);
			}
		}

		public DataFormatRecord()
		{
		}

		/// Constructs a DataFormat record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public DataFormatRecord(RecordInputStream in1)
		{
			field_1_pointNumber = in1.ReadShort();
			field_2_seriesIndex = in1.ReadShort();
			field_3_seriesNumber = in1.ReadShort();
			field_4_formatFlags = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DATAFORMAT]\n");
			stringBuilder.Append("    .pointNumber          = ").Append("0x").Append(HexDump.ToHex(PointNumber))
				.Append(" (")
				.Append(PointNumber)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .seriesIndex          = ").Append("0x").Append(HexDump.ToHex(SeriesIndex))
				.Append(" (")
				.Append(SeriesIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .seriesNumber         = ").Append("0x").Append(HexDump.ToHex(SeriesNumber))
				.Append(" (")
				.Append(SeriesNumber)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .formatFlags          = ").Append("0x").Append(HexDump.ToHex(FormatFlags))
				.Append(" (")
				.Append(FormatFlags)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .useExcel4Colors          = ").Append(UseExcel4Colors).Append('\n');
			stringBuilder.Append("[/DATAFORMAT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_pointNumber);
			out1.WriteShort(field_2_seriesIndex);
			out1.WriteShort(field_3_seriesNumber);
			out1.WriteShort(field_4_formatFlags);
		}

		public override object Clone()
		{
			DataFormatRecord dataFormatRecord = new DataFormatRecord();
			dataFormatRecord.field_1_pointNumber = field_1_pointNumber;
			dataFormatRecord.field_2_seriesIndex = field_2_seriesIndex;
			dataFormatRecord.field_3_seriesNumber = field_3_seriesNumber;
			dataFormatRecord.field_4_formatFlags = field_4_formatFlags;
			return dataFormatRecord;
		}
	}
}
