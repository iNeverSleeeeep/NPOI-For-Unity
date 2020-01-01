using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @author dmui (save existing implementation)
	public class ExpPtg : ControlPtg
	{
		private const int SIZE = 5;

		public const byte sid = 1;

		private short field_1_first_row;

		private short field_2_first_col;

		public override int Size => 5;

		public short Row => field_1_first_row;

		public short Column => field_2_first_col;

		public ExpPtg(ILittleEndianInput in1)
		{
			field_1_first_row = in1.ReadShort();
			field_2_first_col = in1.ReadShort();
		}

		public ExpPtg(int firstRow, int firstCol)
		{
			field_1_first_row = (short)firstRow;
			field_2_first_col = (short)firstCol;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(1 + base.PtgClass);
			out1.WriteShort(field_1_first_row);
			out1.WriteShort(field_2_first_col);
		}

		public override string ToFormulaString()
		{
			throw new RecordFormatException("Coding Error: Expected ExpPtg to be Converted from Shared to Non-Shared Formula by ValueRecordsAggregate, but it wasn't");
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[Array Formula or Shared Formula]\n");
			stringBuilder.Append("row = ").Append(Row).Append("\n");
			stringBuilder.Append("col = ").Append(Column).Append("\n");
			return stringBuilder.ToString();
		}
	}
}
