using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Percent PTG.
	///
	/// @author Daniel Noll (daniel at nuix.com.au)
	public class PercentPtg : ValueOperatorPtg
	{
		public const int SIZE = 1;

		public const byte sid = 20;

		private const string PERCENT = "%";

		public static readonly ValueOperatorPtg instance = new PercentPtg();

		protected override byte Sid => 20;

		public override int NumberOfOperands => 1;

		private PercentPtg()
		{
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("%");
			return stringBuilder.ToString();
		}
	}
}
