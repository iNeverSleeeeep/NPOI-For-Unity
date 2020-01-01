using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * Defines a series name
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class SeriesTextRecord : StandardRecord
	{
		/// the actual text cannot be longer than 255 characters 
		private const int MAX_LEN = 255;

		public const short sid = 4109;

		private short field_1_id;

		private bool is16bit;

		private string field_4_text;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 4 + field_4_text.Length * ((!is16bit) ? 1 : 2);

		public override short Sid => 4109;

		/// Get the id field for the SeriesText record.
		public short Id
		{
			get
			{
				return field_1_id;
			}
			set
			{
				field_1_id = value;
			}
		}

		/// Get the text field for the SeriesText record.
		public string Text
		{
			get
			{
				return field_4_text;
			}
			set
			{
				if (value.Length > 255)
				{
					throw new ArgumentException("Text is too long (" + value.Length + ">" + 255 + ")");
				}
				field_4_text = value;
				is16bit = StringUtil.HasMultibyte(value);
			}
		}

		public SeriesTextRecord()
		{
			field_4_text = "";
			is16bit = false;
		}

		/// Constructs a SeriesText record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SeriesTextRecord(RecordInputStream in1)
		{
			field_1_id = in1.ReadShort();
			int requestedLength = (byte)in1.ReadByte();
			is16bit = ((in1.ReadUByte() & 1) != 0);
			if (is16bit)
			{
				field_4_text = in1.ReadUnicodeLEString(requestedLength);
			}
			else
			{
				field_4_text = in1.ReadCompressedUnicode(requestedLength);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SERIESTEXT]\n");
			stringBuilder.Append("    .id                   = ").Append("0x").Append(HexDump.ToHex(Id))
				.Append(" (")
				.Append(Id)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .textLength           = ").Append(field_4_text.Length);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .is16bit         = ").Append(is16bit);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .text                 = ").Append(" (").Append(Text)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SERIESTEXT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_id);
			out1.WriteByte(field_4_text.Length);
			if (is16bit)
			{
				out1.WriteByte(1);
				StringUtil.PutUnicodeLE(field_4_text, out1);
			}
			else
			{
				out1.WriteByte(0);
				StringUtil.PutCompressedUnicode(field_4_text, out1);
			}
		}

		public override object Clone()
		{
			SeriesTextRecord seriesTextRecord = new SeriesTextRecord();
			seriesTextRecord.field_1_id = field_1_id;
			seriesTextRecord.is16bit = is16bit;
			seriesTextRecord.field_4_text = field_4_text;
			return seriesTextRecord;
		}
	}
}
