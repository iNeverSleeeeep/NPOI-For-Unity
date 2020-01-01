using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The series list record defines the series Displayed as an overlay to the main chart record.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class SeriesListRecord : StandardRecord
	{
		public const short sid = 4118;

		private short[] field_1_seriesNumbers;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => field_1_seriesNumbers.Length * 2 + 2;

		public override short Sid => 4118;

		/// Get the series numbers field for the SeriesList record.
		public short[] SeriesNumbers
		{
			get
			{
				return field_1_seriesNumbers;
			}
			set
			{
				field_1_seriesNumbers = value;
			}
		}

		public SeriesListRecord(short[] seriesNumbers)
		{
			field_1_seriesNumbers = seriesNumbers;
		}

		/// Constructs a SeriesList record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesListRecord(RecordInputStream in1)
		{
			int num = in1.ReadUShort();
			short[] array = new short[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = in1.ReadShort();
			}
			field_1_seriesNumbers = array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SERIESLIST]\n");
			stringBuilder.Append("    .seriesNumbers        = ").Append(" (").Append(SeriesNumbers)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SERIESLIST]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			int num = field_1_seriesNumbers.Length;
			out1.WriteShort(num);
			for (int i = 0; i < num; i++)
			{
				out1.WriteShort(field_1_seriesNumbers[i]);
			}
		}

		public override object Clone()
		{
			return new SeriesListRecord((short[])field_1_seriesNumbers.Clone());
		}
	}
}
