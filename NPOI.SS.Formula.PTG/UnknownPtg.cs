using NPOI.HSSF.Record;
using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class UnknownPtg : Ptg
	{
		private short size = 1;

		public override bool IsBaseToken => true;

		public override int Size => size;

		public override byte DefaultOperandClass => 32;

		/// Creates new UnknownPtg 
		public UnknownPtg()
		{
		}

		public UnknownPtg(RecordInputStream in1)
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
		}

		public override string ToFormulaString()
		{
			return "UNKNOWN";
		}

		public override object Clone()
		{
			return new UnknownPtg();
		}
	}
}
