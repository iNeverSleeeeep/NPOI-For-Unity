using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Exact : TextFunction
	{
		public override ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length != 2)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string text = TextFunction.EvaluateStringArg(args[0], srcCellRow, srcCellCol);
			string value = TextFunction.EvaluateStringArg(args[1], srcCellRow, srcCellCol);
			return BoolEval.ValueOf(text.Equals(value));
		}
	}
}
