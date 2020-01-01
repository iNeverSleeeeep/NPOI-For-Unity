using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public class NamePtg : OperandPtg, WorkbookDependentFormula
	{
		public const short sid = 35;

		private const int SIZE = 5;

		/// one-based index to defined name record 
		private int field_1_label_index;

		private short field_2_zero;

		/// @return zero based index to a defined name record in the LinkTable
		public int Index => field_1_label_index - 1;

		public override int Size => 5;

		public override byte DefaultOperandClass => 0;

		/// @param nameIndex zero-based index to name within workbook
		public NamePtg(int nameIndex)
		{
			field_1_label_index = 1 + nameIndex;
		}

		/// Creates new NamePtg 
		public NamePtg(ILittleEndianInput in1)
		{
			field_1_label_index = in1.ReadShort();
			field_2_zero = in1.ReadShort();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(35 + base.PtgClass);
			out1.WriteShort(field_1_label_index);
			out1.WriteShort(field_2_zero);
		}

		public string ToFormulaString(IFormulaRenderingWorkbook book)
		{
			return book.GetNameText(this);
		}

		public override string ToFormulaString()
		{
			throw new NotImplementedException("3D references need a workbook to determine formula text");
		}
	}
}
