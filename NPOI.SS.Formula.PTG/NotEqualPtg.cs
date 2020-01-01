using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Ptg class to implement not equal
	///
	/// @author fred at stsci dot edu
	public class NotEqualPtg : ValueOperatorPtg
	{
		public const byte sid = 14;

		public static ValueOperatorPtg instance = new NotEqualPtg();

		protected override byte Sid => 14;

		public override int NumberOfOperands => 2;

		private NotEqualPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("<>");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
