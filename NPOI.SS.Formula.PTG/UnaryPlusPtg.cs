using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Unary Plus operator
	/// does not have any effect on the operand
	/// @author Avik Sengupta
	public class UnaryPlusPtg : ValueOperatorPtg
	{
		public const byte sid = 18;

		private static string Add = "+";

		public static ValueOperatorPtg instance = new UnaryPlusPtg();

		protected override byte Sid => 18;

		public override int NumberOfOperands => 1;

		private UnaryPlusPtg()
		{
		}

		/// implementation of method from OperationsPtg
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Add);
			stringBuilder.Append(operands[0]);
			return stringBuilder.ToString();
		}
	}
}
