using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Less than operator PTG "&lt;". The SID is taken from the 
	/// Openoffice.orgs Documentation of the Excel File Format,
	/// Table 3.5.7
	/// @author Cameron Riley (criley at ekmail.com)
	public class LessThanPtg : ValueOperatorPtg
	{
		/// the sid for the less than operator as hex 
		public const byte sid = 9;

		/// identifier for LESS THAN char 
		private const string LESSTHAN = "<";

		public static readonly ValueOperatorPtg instance = new LessThanPtg();

		protected override byte Sid => 9;

		/// Get the number of operands for the Less than operator
		/// @return int the number of operands
		public override int NumberOfOperands => 2;

		private LessThanPtg()
		{
		}

		/// Implementation of method from OperationsPtg
		/// @param operands a String array of operands
		/// @return String the Formula as a String
		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(operands[0]);
			stringBuilder.Append("<");
			stringBuilder.Append(operands[1]);
			return stringBuilder.ToString();
		}
	}
}
