using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// contribute by Pavel Egorov 
	/// https://github.com/xoposhiy/npoi/commit/27b34a2389030c7115a666ace65daafda40d61af
	public class Iserr : LogicalFunction
	{
		protected override bool Evaluate(ValueEval arg)
		{
			if (arg != ErrorEval.NA)
			{
				return arg is ErrorEval;
			}
			return false;
		}
	}
}
