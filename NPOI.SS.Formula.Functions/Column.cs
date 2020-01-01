using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public class Column : Function0Arg, Function1Arg, Function
	{
		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return new NumberEval((double)(srcColumnIndex + 1));
		}

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			int num;
			if (arg0 is AreaEval)
			{
				num = ((AreaEval)arg0).FirstColumn;
			}
			else
			{
				if (!(arg0 is RefEval))
				{
					return ErrorEval.VALUE_INVALID;
				}
				num = ((RefEval)arg0).Column;
			}
			return new NumberEval((double)(num + 1));
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 1:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0]);
			case 0:
				return new NumberEval((double)(srcColumnIndex + 1));
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}
	}
}
