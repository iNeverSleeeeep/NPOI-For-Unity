using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Calc Count Record
	/// Description:  Specifies the maximum times the gui should perform a formula
	///               recalculation.  For instance: in the case a formula includes
	///               cells that are themselves a result of a formula and a value
	///               Changes.  This Is essentially a failsafe against an infinate
	///               loop in the event the formulas are not independant. 
	/// REFERENCE:  PG 292 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.CalcModeRecord
	public class CalcCountRecord : StandardRecord
	{
		public const short sid = 12;

		private short field_1_iterations;

		/// Get the number of iterations to perform
		/// @return iterations
		public short Iterations
		{
			get
			{
				return field_1_iterations;
			}
			set
			{
				field_1_iterations = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 12;

		public CalcCountRecord()
		{
		}

		/// Constructs a CalcCountRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public CalcCountRecord(RecordInputStream in1)
		{
			field_1_iterations = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CALCCOUNT]\n");
			stringBuilder.Append("    .iterations     = ").Append(StringUtil.ToHexString(Iterations)).Append("\n");
			stringBuilder.Append("[/CALCCOUNT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Iterations);
		}

		public override object Clone()
		{
			CalcCountRecord calcCountRecord = new CalcCountRecord();
			calcCountRecord.field_1_iterations = field_1_iterations;
			return calcCountRecord;
		}
	}
}
