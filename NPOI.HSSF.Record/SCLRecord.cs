using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * Specifies the window's zoom magnification.  If this record Isn't present then the windows zoom is 100%. see p384 Excel Dev Kit
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class SCLRecord : StandardRecord
	{
		public const short sid = 160;

		private short field_1_numerator;

		private short field_2_denominator;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 4;

		public override short Sid => 160;

		/// Get the numerator field for the SCL record.
		public short Numerator
		{
			get
			{
				return field_1_numerator;
			}
			set
			{
				field_1_numerator = value;
			}
		}

		/// Get the denominator field for the SCL record.
		public short Denominator
		{
			get
			{
				return field_2_denominator;
			}
			set
			{
				field_2_denominator = value;
			}
		}

		public SCLRecord()
		{
		}

		/// Constructs a SCL record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SCLRecord(RecordInputStream in1)
		{
			field_1_numerator = in1.ReadShort();
			field_2_denominator = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SCL]\n");
			stringBuilder.Append("    .numerator            = ").Append("0x").Append(HexDump.ToHex(Numerator))
				.Append(" (")
				.Append(Numerator)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .denominator          = ").Append("0x").Append(HexDump.ToHex(Denominator))
				.Append(" (")
				.Append(Denominator)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/SCL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_numerator);
			out1.WriteShort(field_2_denominator);
		}

		public override object Clone()
		{
			SCLRecord sCLRecord = new SCLRecord();
			sCLRecord.field_1_numerator = field_1_numerator;
			sCLRecord.field_2_denominator = field_2_denominator;
			return sCLRecord;
		}
	}
}
