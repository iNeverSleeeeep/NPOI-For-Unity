using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The number format index record indexes format table.  This applies to an axis.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class NumberFormatIndexRecord : StandardRecord
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

		public NumberFormatIndexRecord()
		{
		}

		/// Constructs a NumberFormatIndex record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public NumberFormatIndexRecord(RecordInputStream in1)
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
			NumberFormatIndexRecord numberFormatIndexRecord = new NumberFormatIndexRecord();
			numberFormatIndexRecord.field_1_formatIndex = field_1_formatIndex;
			return numberFormatIndexRecord;
		}
	}
}
