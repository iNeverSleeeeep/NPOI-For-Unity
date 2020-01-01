using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Save Recalc Record 
	/// Description:  defines whether to recalculate before saving (Set to true)
	/// REFERENCE:  PG 381 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class SaveRecalcRecord : StandardRecord
	{
		public const short sid = 95;

		private short field_1_recalc;

		/// Get whether to recalculate formulas/etc before saving or not
		/// @return recalc - whether to recalculate or not
		public bool Recalc
		{
			get
			{
				return field_1_recalc == 1;
			}
			set
			{
				field_1_recalc = (short)(value ? 1 : 0);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 95;

		public SaveRecalcRecord()
		{
		}

		/// Constructs an SaveRecalc record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public SaveRecalcRecord(RecordInputStream in1)
		{
			field_1_recalc = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SAVERECALC]\n");
			stringBuilder.Append("    .recalc         = ").Append(Recalc).Append("\n");
			stringBuilder.Append("[/SAVERECALC]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_recalc);
		}

		public override object Clone()
		{
			SaveRecalcRecord saveRecalcRecord = new SaveRecalcRecord();
			saveRecalcRecord.field_1_recalc = field_1_recalc;
			return saveRecalcRecord;
		}
	}
}
