using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class IntersectionPtg : OperationPtg
	{
		public const byte sid = 15;

		public static OperationPtg instance = new IntersectionPtg();

		public override bool IsBaseToken => true;

		public override int Size => 1;

		public override int NumberOfOperands => 2;

		private IntersectionPtg()
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(15 + base.PtgClass);
		}

		/// Implementation of method from Ptg 
		public override string ToFormulaString()
		{
			return " ";
		}

		/// implementation of method from OperationsPtg
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(" ");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
