using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The FontX record specifies the font for a given text element. 
	/// The Font record referenced by iFont can exist in this chart sheet substream or the workbook.
	/// </summary>
	public class FontXRecord : StandardRecord
	{
		public const short sid = 4134;

		private short field_1_fontIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4134;

		/// <summary>
		/// specifies the font to use for subsequent records.
		/// This font can either be the default font of the chart, part of the collection of Font records following 
		/// the FrtFontList record, or part of the collection of Font records in the globals substream. 
		/// If iFont is 0x0000, this record specifies the default font of the chart. 
		/// If iFont is less than or equal to the number of Font records in the globals substream, 
		///     iFont is a one-based index to a Font record in the globals substream. 
		/// Otherwise iFont is a one-based index into the collection of Font records in this chart sheet substream 
		///     where the index is equal to iFont – n, where n is the number of Font records in the globals substream.
		/// </summary>
		public short FontIndex
		{
			get
			{
				return field_1_fontIndex;
			}
			set
			{
				field_1_fontIndex = value;
			}
		}

		public FontXRecord()
		{
		}

		/// Constructs a FontIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public FontXRecord(RecordInputStream in1)
		{
			field_1_fontIndex = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FONTX]\n");
			stringBuilder.Append("    .fontIndex            = ").Append("0x").Append(HexDump.ToHex(FontIndex))
				.Append(" (")
				.Append(FontIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/FONTX]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_fontIndex);
		}

		public override object Clone()
		{
			FontXRecord fontXRecord = new FontXRecord();
			fontXRecord.field_1_fontIndex = field_1_fontIndex;
			return fontXRecord;
		}
	}
}
