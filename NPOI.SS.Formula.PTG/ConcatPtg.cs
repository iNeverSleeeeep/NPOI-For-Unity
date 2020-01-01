using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class ConcatPtg : ValueOperatorPtg
	{
		public const byte sid = 8;

		private const string CONCAT = "&";

		public static readonly ValueOperatorPtg instance = new ConcatPtg();

		protected override byte Sid => 8;

		public override int NumberOfOperands => 2;

		private ConcatPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("&");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
