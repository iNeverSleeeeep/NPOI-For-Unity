using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Delta Record
	/// Description:  controls the accuracy of the calculations
	/// REFERENCE:  PG 303 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class DeltaRecord : StandardRecord
	{
		public const short sid = 16;

		public const double DEFAULT_VALUE = 0.001;

		private double field_1_max_change;

		/// Get the maximum Change
		/// @return maxChange - maximum rounding error
		public double MaxChange
		{
			get
			{
				return field_1_max_change;
			}
			set
			{
				field_1_max_change = value;
			}
		}

		protected override int DataSize => 8;

		public override short Sid => 16;

		public DeltaRecord(double maxChange)
		{
			field_1_max_change = maxChange;
		}

		/// Constructs a Delta record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public DeltaRecord(RecordInputStream in1)
		{
			field_1_max_change = in1.ReadDouble();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DELTA]\n");
			stringBuilder.Append("    .maxChange      = ").Append(MaxChange).Append("\n");
			stringBuilder.Append("[/DELTA]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteDouble(MaxChange);
		}

		public override object Clone()
		{
			return this;
		}
	}
}
