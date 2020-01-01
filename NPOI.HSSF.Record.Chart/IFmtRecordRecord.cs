using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The IFmtRecord record specifies the number format to use for the text on an axis.
	/// </summary>
	public class IFmtRecordRecord : StandardRecord
	{
		public const short sid = 4174;

		private short field_1_formatIndex;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4174;

		/// Get the format index field for the NumberFormatIndex record.
		public short FormatIndex
		{
			get
			{
				return field_1_formatIndex;
			}
			set
			{
				field_1_formatIndex = value;
			}
		}

		public IFmtRecordRecord()
		{
		}

		/// Constructs a NumberFormatIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public IFmtRecordRecord(RecordInputStream in1)
		{
			field_1_formatIndex = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[IFMT]\n");
			stringBuilder.Append("    .formatIndex          = ").Append("0x").Append(HexDump.ToHex(FormatIndex))
				.Append(" (")
				.Append(FormatIndex)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/IFMT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_formatIndex);
		}

		public override object Clone()
		{
			IFmtRecordRecord fmtRecordRecord = new IFmtRecordRecord();
			fmtRecordRecord.field_1_formatIndex = field_1_formatIndex;
			return fmtRecordRecord;
		}
	}
}
