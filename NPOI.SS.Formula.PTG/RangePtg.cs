using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class RangePtg : OperationPtg
	{
		public const int SIZE = 1;

		public const byte sid = 17;

		public static OperationPtg instance = new RangePtg();

		public override bool IsBaseToken => true;

		public override int Size => 1;

		public override int NumberOfOperands => 2;

		private RangePtg()
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(17 + base.PtgClass);
		}

		public override string ToFormulaString()
		{
			return ":";
		}

		/// implementation of method from OperationsPtg
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(":");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
