using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Function Group Count Record
	/// Description:  Number of built in function Groups in the current version of the
	///               SpReadsheet (probably only used on Windoze)
	/// REFERENCE:  PG 315 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class FnGroupCountRecord : StandardRecord
	{
		public const short sid = 156;

		/// suggested default (14 dec)
		public const short COUNT = 14;

		private short field_1_count;

		/// Get the number of built-in functions
		///
		/// @return number of built-in functions
		public short Count
		{
			get
			{
				return field_1_count;
			}
			set
			{
				field_1_count = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 156;

		public FnGroupCountRecord()
		{
		}

		/// Constructs a FnGroupCount record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public FnGroupCountRecord(RecordInputStream in1)
		{
			field_1_count = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FNGROUPCOUNT]\n");
			stringBuilder.Append("    .count            = ").Append(Count).Append("\n");
			stringBuilder.Append("[/FNGROUPCOUNT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Count);
		}
	}
}
