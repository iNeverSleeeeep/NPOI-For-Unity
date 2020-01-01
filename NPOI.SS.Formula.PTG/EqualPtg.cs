using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	public class EqualPtg : ValueOperatorPtg
	{
		public const byte sid = 11;

		public static ValueOperatorPtg instance = new EqualPtg();

		protected override byte Sid => 11;

		public override int NumberOfOperands => 2;

		private EqualPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("=");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
