using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Ptg class to implement less than or equal
	///
	/// @author fred at stsci dot edu
	public class LessEqualPtg : ValueOperatorPtg
	{
		public const byte sid = 10;

		public static ValueOperatorPtg instance = new LessEqualPtg();

		protected override byte Sid => 10;

		public override int NumberOfOperands => 2;

		private LessEqualPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("<=");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
