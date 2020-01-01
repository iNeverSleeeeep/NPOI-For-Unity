using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class PowerPtg : ValueOperatorPtg
	{
		public const byte sid = 7;

		public static ValueOperatorPtg instance = new PowerPtg();

		protected override byte Sid => 7;

		public override int NumberOfOperands => 2;

		private PowerPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("^");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
