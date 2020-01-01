using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// This ptg indicates a data table.
	/// It only occurs in a FORMULA record, never in an
	///  ARRAY or NAME record.  When ptgTbl occurs in a
	///  formula, it is the only token in the formula.
	///
	/// This indicates that the cell containing the
	///  formula is an interior cell in a data table;
	///  the table description is found in a TABLE
	///  record. Rows and columns which contain input
	///  values to be substituted in the table do
	///  not contain ptgTbl.
	/// See page 811 of the june 08 binary docs.
	public class TblPtg : ControlPtg
	{
		private const int SIZE = 5;

		public const byte sid = 2;

		/// The row number of the upper left corner 
		private int field_1_first_row;

		/// The column number of the upper left corner 
		private int field_2_first_col;

		public override int Size => 5;

		public int Row => field_1_first_row;

		public int Column => field_2_first_col;

		public TblPtg(ILittleEndianInput in1)
		{
			field_1_first_row = in1.ReadUShort();
			field_2_first_col = in1.ReadUShort();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(2 + base.PtgClass);
			out1.WriteShort(field_1_first_row);
			out1.WriteShort(field_2_first_col);
		}

		public override string ToFormulaString()
		{
			throw new RecordFormatException("Table and Arrays are not yet supported");
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[Data Table - Parent cell is an interior cell in a data table]\n");
			stringBuilder.Append("top left row = ").Append(Row).Append("\n");
			stringBuilder.Append("top left col = ").Append(Column).Append("\n");
			return stringBuilder.ToString();
		}
	}
}
