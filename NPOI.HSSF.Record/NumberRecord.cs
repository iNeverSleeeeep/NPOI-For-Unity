using NPOI.SS.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Contains a numeric cell value. 
	/// REFERENCE:  PG 334 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class NumberRecord : CellRecord
	{
		public const short sid = 515;

		private double field_4_value;

		protected override string RecordName => "NUMBER";

		protected override int ValueDataSize => 8;

		/// Get the value for the cell
		///
		/// @return double representing the value
		public double Value
		{
			get
			{
				return field_4_value;
			}
			set
			{
				field_4_value = value;
			}
		}

		public override short Sid => 515;

		/// Creates new NumberRecord 
		public NumberRecord()
		{
		}

		/// Constructs a Number record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public NumberRecord(RecordInputStream in1)
			: base(in1)
		{
			field_4_value = in1.ReadDouble();
		}

		protected override void AppendValueText(StringBuilder sb)
		{
			sb.Append("  .value= ").Append(NumberToTextConverter.ToText(field_4_value));
		}

		protected override void SerializeValue(ILittleEndianOutput out1)
		{
			out1.WriteDouble(Value);
		}

		public override object Clone()
		{
			NumberRecord numberRecord = new NumberRecord();
			CopyBaseFields(numberRecord);
			numberRecord.field_4_value = field_4_value;
			return numberRecord;
		}
	}
}
