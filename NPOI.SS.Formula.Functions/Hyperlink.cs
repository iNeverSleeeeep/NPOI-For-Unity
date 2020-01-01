using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of Excel HYPERLINK function.<p />
	///
	/// In Excel this function has special behaviour - it causes the displayed cell value to behave like
	/// a hyperlink in the GUI. From an evaluation perspective however, it is very simple.<p />
	///
	/// <b>Syntax</b>:<br />
	/// <b>HYPERLINK</b>(<b>link_location</b>, friendly_name)<p />
	///
	/// <b>link_location</b> The URL of the hyperlink <br />
	/// <b>friendly_name</b> (optional) the value to display<p />
	///
	///  Returns last argument.  Leaves type unchanged (does not convert to {@link org.apache.poi.ss.formula.eval.StringEval}).
	///
	/// @author Wayne Clingingsmith
	public class Hyperlink : Var1or2ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			return arg0;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			return arg1;
		}
	}
}
