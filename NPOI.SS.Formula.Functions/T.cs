using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public class T : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			ValueEval valueEval = arg0;
			if (valueEval is RefEval)
			{
				valueEval = ((RefEval)valueEval).InnerValueEval;
			}
			else if (valueEval is AreaEval)
			{
				valueEval = ((AreaEval)valueEval).GetRelativeValue(0, 0);
			}
			if (valueEval is StringEval)
			{
				return valueEval;
			}
			if (valueEval is ErrorEval)
			{
				return valueEval;
			}
			return StringEval.EMPTY_INSTANCE;
		}
	}
}
