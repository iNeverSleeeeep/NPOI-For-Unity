using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author Glen Stampoultzis (glens at apache.org)
	public class UnionPtg : OperationPtg
	{
		public const byte sid = 16;

		public static OperationPtg instance = new UnionPtg();

		public override bool IsBaseToken => true;

		public override int Size => 1;

		public override int NumberOfOperands => 2;

		private UnionPtg()
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(16 + base.PtgClass);
		}

		public override string ToFormulaString()
		{
			return ",";
		}

		/// implementation of method from OperationsPtg
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(",");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
