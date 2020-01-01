using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Greater than operator PTG "&gt;"
	/// @author  Cameron Riley (criley at ekmail.com)
	public class GreaterThanPtg : ValueOperatorPtg
	{
		public const byte sid = 13;

		private const string GREATERTHAN = ">";

		public static readonly ValueOperatorPtg instance = new GreaterThanPtg();

		protected override byte Sid => 13;

		/// Get the number of operands for the Less than operator
		/// @return int the number of operands
		public override int NumberOfOperands => 2;

		private GreaterThanPtg()
		{
		}

		/// Implementation of method from OperationsPtg
		/// @param operands a String array of operands
		/// @return String the Formula as a String
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append(">");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
