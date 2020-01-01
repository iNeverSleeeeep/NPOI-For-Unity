using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Implements the standard mathmatical multiplication - *
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class MultiplyPtg : ValueOperatorPtg
	{
		public const byte sid = 5;

		public static ValueOperatorPtg instance = new MultiplyPtg();

		protected override byte Sid => 5;

		public override int NumberOfOperands => 2;

		private MultiplyPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("*");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
