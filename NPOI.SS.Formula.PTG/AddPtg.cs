using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Addition operator PTG the "+" binomial operator.  If you need more 
	/// explanation than that then well...We really can't help you here.
	/// @author  Andrew C. Oliver (acoliver@apache.org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class AddPtg : ValueOperatorPtg
	{
		public const byte sid = 3;

		private static string Add = "+";

		public static ValueOperatorPtg instance = new AddPtg();

		protected override byte Sid => 3;

		public override int NumberOfOperands => 2;

		private AddPtg()
		{
		}

		/// implementation of method from OperationsPtg
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(Add);
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
