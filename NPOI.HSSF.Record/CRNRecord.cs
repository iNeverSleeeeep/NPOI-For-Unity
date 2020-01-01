using NPOI.SS.Formula.Constant;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:       CRN  
	/// Description: This record stores the contents of an external cell or cell range 
	/// REFERENCE:  5.23
	///
	/// @author josh micich
	public class CRNRecord : StandardRecord
	{
		public const short sid = 90;

		private int field_1_last_column_index;

		private int field_2_first_column_index;

		private int field_3_row_index;

		private object[] field_4_constant_values;

		public int NumberOfCRNs => field_1_last_column_index;

		protected override int DataSize => 4 + ConstantValueParser.GetEncodedSize(field_4_constant_values);

		/// return the non static version of the id for this record.
		public override short Sid => 90;

		public CRNRecord()
		{
			throw new NotImplementedException("incomplete code");
		}

		public CRNRecord(RecordInputStream in1)
		{
			field_1_last_column_index = (in1.ReadByte() & 0xFF);
			field_2_first_column_index = (in1.ReadByte() & 0xFF);
			field_3_row_index = in1.ReadShort();
			int nValues = field_1_last_column_index - field_2_first_column_index + 1;
			field_4_constant_values = ConstantValueParser.Parse(in1, nValues);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [CRN");
			stringBuilder.Append(" rowIx=").Append(field_3_row_index);
			stringBuilder.Append(" firstColIx=").Append(field_2_first_column_index);
			stringBuilder.Append(" lastColIx=").Append(field_1_last_column_index);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(field_1_last_column_index);
			out1.WriteByte(field_2_first_column_index);
			out1.WriteShort(field_3_row_index);
			ConstantValueParser.Encode(out1, field_4_constant_values);
		}
	}
}
