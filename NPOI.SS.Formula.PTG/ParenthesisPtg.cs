using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// While formula tokens are stored in RPN order and thus do not need parenthesis for 
	/// precedence reasons, Parenthesis tokens ARE written to Ensure that user entered
	/// parenthesis are Displayed as-is on Reading back
	///
	/// Avik Sengupta &lt;lists@aviksengupta.com&gt;
	/// Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class ParenthesisPtg : ControlPtg
	{
		private const int SIZE = 1;

		public const byte sid = 21;

		public static ControlPtg instance = new ParenthesisPtg();

		public override int Size => 1;

		private ParenthesisPtg()
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(21 + base.PtgClass);
		}

		public override string ToFormulaString()
		{
			return "()";
		}

		public string ToFormulaString(string[] operands)
		{
			return "(" + operands[0] + ")";
		}
	}
}
