using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class SubtractPtg : ValueOperatorPtg
	{
		public const byte sid = 4;

		public static ValueOperatorPtg instance = new SubtractPtg();

		protected override byte Sid => 4;

		public override int NumberOfOperands => 2;

		private SubtractPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("-");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
