using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// PTG class to implement greater or equal to
	///
	/// @author  fred at stsci dot edu
	public class GreaterEqualPtg : ValueOperatorPtg
	{
		public const int SIZE = 1;

		public const byte sid = 12;

		public static ValueOperatorPtg instance = new GreaterEqualPtg();

		protected override byte Sid => 12;

		public override int NumberOfOperands => 2;

		private GreaterEqualPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(">=");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
