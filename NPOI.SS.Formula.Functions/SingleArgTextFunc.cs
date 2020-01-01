using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public abstract class SingleArgTextFunc : TextFunction
	{
		public override ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length != 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string arg = TextFunction.EvaluateStringArg(args[0], srcCellRow, srcCellCol);
			return Evaluate(arg);
		}

		public abstract ValueEval Evaluate(string arg);
	}
}
