using NPOI.HSSF.UserModel;
using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// AreaErr - handles deleted cell area references.
	///
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class AreaErrPtg : OperandPtg
	{
		public const byte sid = 43;

		private int unused1;

		private int unused2;

		public override byte DefaultOperandClass => 0;

		public override int Size => 9;

		public AreaErrPtg(ILittleEndianInput in1)
		{
			unused1 = in1.ReadInt();
			unused2 = in1.ReadInt();
		}

		public AreaErrPtg()
		{
			unused1 = 0;
			unused2 = 0;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(43 + base.PtgClass);
			out1.WriteInt(unused1);
			out1.WriteInt(unused2);
		}

		public override string ToFormulaString()
		{
			return HSSFErrorConstants.GetText(23);
		}
	}
}
