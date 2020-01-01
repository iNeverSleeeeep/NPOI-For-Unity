using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for Excel ROWS function.
	///
	/// @author Josh Micich
	public class Rows : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			int num;
			if (arg0 is TwoDEval)
			{
				num = ((TwoDEval)arg0).Height;
			}
			else
			{
				if (!(arg0 is RefEval))
				{
					return ErrorEval.VALUE_INVALID;
				}
				num = 1;
			}
			return new NumberEval((double)num);
		}
	}
}
