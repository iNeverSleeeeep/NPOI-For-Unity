using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Precision Record
	/// Description:  defines whether to store with full precision or what's Displayed by the gui
	///               (meaning have really screwed up and skewed figures or only think you do!)
	/// REFERENCE:  PG 372 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class PrecisionRecord : StandardRecord
	{
		public const short sid = 14;

		public short field_1_precision;

		/// Get whether to use full precision or just skew all you figures all to hell.
		///
		/// @return fullprecision - or not
		public bool FullPrecision
		{
			get
			{
				return field_1_precision == 1;
			}
			set
			{
				if (value)
				{
					field_1_precision = 1;
				}
				else
				{
					field_1_precision = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 14;

		public PrecisionRecord()
		{
		}

		/// Constructs a Precision record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PrecisionRecord(RecordInputStream in1)
		{
			field_1_precision = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PRECISION]\n");
			stringBuilder.Append("    .precision       = ").Append(FullPrecision).Append("\n");
			stringBuilder.Append("[/PRECISION]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_precision);
		}
	}
}
