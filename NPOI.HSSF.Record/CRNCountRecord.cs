using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// XCT ?CRN Count 
	///
	/// REFERENCE:  5.114
	///
	/// @author Josh Micich
	public class CRNCountRecord : StandardRecord
	{
		public const short sid = 89;

		private const short DATA_SIZE = 4;

		private int field_1_number_crn_records;

		private int field_2_sheet_table_index;

		public int NumberOfCRNs => field_1_number_crn_records;

		protected override int DataSize => 4;

		/// return the non static version of the id for this record.
		public override short Sid => 89;

		public CRNCountRecord()
		{
			throw new RuntimeException("incomplete code");
		}

		public CRNCountRecord(RecordInputStream in1)
		{
			field_1_number_crn_records = in1.ReadShort();
			if (field_1_number_crn_records < 0)
			{
				field_1_number_crn_records = (short)(-field_1_number_crn_records);
			}
			field_2_sheet_table_index = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [XCT");
			stringBuilder.Append(" nCRNs=").Append(field_1_number_crn_records);
			stringBuilder.Append(" sheetIx=").Append(field_2_sheet_table_index);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort((short)field_1_number_crn_records);
			out1.WriteShort((short)field_2_sheet_table_index);
		}
	}
}
