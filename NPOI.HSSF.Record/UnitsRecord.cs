using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * The Units record describes Units.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class UnitsRecord : StandardRecord
	{
		public const short sid = 4097;

		private short field_1_units;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 2;

		public override short Sid => 4097;

		/// Get the Units field for the Units record.
		public short Units
		{
			get
			{
				return field_1_units;
			}
			set
			{
				field_1_units = value;
			}
		}

		public UnitsRecord()
		{
		}

		/// Constructs a Units record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public UnitsRecord(RecordInputStream in1)
		{
			field_1_units = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[UNITS]\n");
			stringBuilder.Append("    .units                = ").Append("0x").Append(HexDump.ToHex(Units))
				.Append(" (")
				.Append(Units)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/UNITS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_units);
		}

		public override object Clone()
		{
			UnitsRecord unitsRecord = new UnitsRecord();
			unitsRecord.field_1_units = field_1_units;
			return unitsRecord;
		}
	}
}
