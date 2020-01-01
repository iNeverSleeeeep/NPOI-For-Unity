using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// @author Glen Stampoultzis (glens at apache.org)
	public class MemFuncPtg : OperandPtg
	{
		public const byte sid = 41;

		private int field_1_len_ref_subexpression;

		public override int Size => 3;

		public override byte DefaultOperandClass => 0;

		public int NumberOfOperands => field_1_len_ref_subexpression;

		public int LenRefSubexpression => field_1_len_ref_subexpression;

		/// Creates new function pointer from a byte array
		/// usually called while Reading an excel file.
		public MemFuncPtg(ILittleEndianInput in1)
			: this(in1.ReadUShort())
		{
		}

		public MemFuncPtg(int subExprLen)
		{
			field_1_len_ref_subexpression = subExprLen;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(41 + base.PtgClass);
			out1.WriteShort(field_1_len_ref_subexpression);
		}

		public override string ToFormulaString()
		{
			return "";
		}
	}
}
