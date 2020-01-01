using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// An implementation of the TRIM function:
	/// Removes leading and trailing spaces from value if Evaluated operand
	///  value Is string.
	/// @author Manda Wilson &lt; wilson at c bio dot msk cc dot org &gt;
	public class Trim : SingleArgTextFunc
	{
		public override ValueEval Evaluate(string arg)
		{
			return new StringEval(arg.Trim());
		}
	}
}
