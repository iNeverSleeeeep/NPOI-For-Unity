using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Calc Mode Record
	/// Description:  Tells the gui whether to calculate formulas
	///               automatically, manually or automatically
	///               except for tables.
	/// REFERENCE:  PG 292 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.CalcCountRecord
	public class CalcModeRecord : StandardRecord
	{
		public const short sid = 13;

		/// manually calculate formulas (0)
		public const short MANUAL = 0;

		/// automatically calculate formulas (1)
		public const short AUTOMATIC = 1;

		/// automatically calculate formulas except for tables (-1)
		public const short AUTOMATIC_EXCEPT_TABLES = -1;

		private short field_1_calcmode;

		protected override int DataSize => 2;

		public override short Sid => 13;

		public CalcModeRecord()
		{
		}

		/// Constructs a CalcModeRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public CalcModeRecord(RecordInputStream in1)
		{
			field_1_calcmode = in1.ReadShort();
		}

		/// Set the calc mode flag for formulas
		///
		/// @see #MANUAL
		/// @see #AUTOMATIC
		/// @see #AUTOMATIC_EXCEPT_TABLES
		///
		/// @param calcmode one of the three flags above
		public void SetCalcMode(short calcmode)
		{
			field_1_calcmode = calcmode;
		}

		/// Get the calc mode flag for formulas
		///
		/// @see #MANUAL
		/// @see #AUTOMATIC
		/// @see #AUTOMATIC_EXCEPT_TABLES
		///
		/// @return calcmode one of the three flags above
		public short GetCalcMode()
		{
			return field_1_calcmode;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CALCMODE]\n");
			stringBuilder.Append("    .calcmode       = ").Append(StringUtil.ToHexString(GetCalcMode())).Append("\n");
			stringBuilder.Append("[/CALCMODE]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(GetCalcMode());
		}

		public override object Clone()
		{
			CalcModeRecord calcModeRecord = new CalcModeRecord();
			calcModeRecord.field_1_calcmode = field_1_calcmode;
			return calcModeRecord;
		}
	}
}
