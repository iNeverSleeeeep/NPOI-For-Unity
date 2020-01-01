using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Label Record - Read only support for strings stored directly in the cell..  Don't
	/// use this (except to Read), use LabelSST instead 
	/// REFERENCE:  PG 325 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.LabelSSTRecord
	public class LabelRecord : Record, CellValueRecordInterface
	{
		public const short sid = 516;

		private static POILogger logger = POILogFactory.GetLogger(typeof(LabelRecord));

		private int field_1_row;

		private int field_2_column;

		private short field_3_xf_index;

		private short field_4_string_len;

		private byte field_5_unicode_flag;

		private string field_6_value;

		public int Row
		{
			get
			{
				return field_1_row;
			}
			set
			{
				throw new NotSupportedException("Use LabelSST instead");
			}
		}

		public int Column
		{
			get
			{
				return field_2_column;
			}
			set
			{
				throw new NotSupportedException("Use LabelSST instead");
			}
		}

		public short XFIndex
		{
			get
			{
				return field_3_xf_index;
			}
			set
			{
				throw new NotSupportedException("Use LabelSST instead");
			}
		}

		/// Get the number of Chars this string Contains
		/// @return number of Chars
		public short StringLength => field_4_string_len;

		/// Is this Uncompressed Unicode (16bit)?  Or just 8-bit compressed?
		/// @return IsUnicode - True for 16bit- false for 8bit
		public bool IsUncompressedUnicode => (field_5_unicode_flag & 1) != 0;

		/// Get the value
		///
		/// @return the text string
		/// @see #GetStringLength
		public string Value => field_6_value;

		public override int RecordSize
		{
			get
			{
				throw new RecordFormatException("Label Records are supported READ ONLY...convert to LabelSST");
			}
		}

		public override short Sid => 516;

		/// Creates new LabelRecord 
		public LabelRecord()
		{
		}

		/// Constructs an Label record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public LabelRecord(RecordInputStream in1)
		{
			field_1_row = in1.ReadUShort();
			field_2_column = in1.ReadUShort();
			field_3_xf_index = in1.ReadShort();
			field_4_string_len = in1.ReadShort();
			field_5_unicode_flag = (byte)in1.ReadByte();
			if (field_4_string_len > 0)
			{
				if (IsUncompressedUnicode)
				{
					field_6_value = in1.ReadUnicodeLEString(field_4_string_len);
				}
				else
				{
					field_6_value = in1.ReadCompressedUnicode(field_4_string_len);
				}
			}
			else
			{
				field_6_value = "";
			}
			if (in1.Remaining > 0)
			{
				logger.Log(3, "LabelRecord data remains: " + in1.Remaining + " : " + HexDump.ToHex(in1.ReadRemainder()));
			}
		}

		/// THROWS A RUNTIME EXCEPTION..  USE LABELSSTRecords.  YOU HAVE NO REASON to use LABELRecord!!
		public override int Serialize(int offset, byte[] data)
		{
			throw new RecordFormatException("Label Records are supported Read ONLY...Convert to LabelSST");
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[LABEL]\n");
			stringBuilder.Append("    .row            = ").Append(StringUtil.ToHexString(Row)).Append("\n");
			stringBuilder.Append("    .column         = ").Append(StringUtil.ToHexString(Column)).Append("\n");
			stringBuilder.Append("    .xfindex        = ").Append(StringUtil.ToHexString(XFIndex)).Append("\n");
			stringBuilder.Append("    .string_len       = ").Append(StringUtil.ToHexString(field_4_string_len)).Append("\n");
			stringBuilder.Append("    .unicode_flag       = ").Append(StringUtil.ToHexString(field_5_unicode_flag)).Append("\n");
			stringBuilder.Append("    .value       = ").Append(Value).Append("\n");
			stringBuilder.Append("[/LABEL]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			LabelRecord labelRecord = new LabelRecord();
			labelRecord.field_1_row = field_1_row;
			labelRecord.field_2_column = field_2_column;
			labelRecord.field_3_xf_index = field_3_xf_index;
			labelRecord.field_4_string_len = field_4_string_len;
			labelRecord.field_5_unicode_flag = field_5_unicode_flag;
			labelRecord.field_6_value = field_6_value;
			return labelRecord;
		}
	}
}
