using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public abstract class OneArg : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			double num;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				num = Evaluate(d);
				NumericFunction.CheckValue(num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		protected double Eval(ValueEval[] args, int srcCellRow, short srcCellCol)
		{
			if (args.Length != 1)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			double d = NumericFunction.SingleOperandEvaluate(args[0], srcCellRow, srcCellCol);
			return Evaluate(d);
		}

		public abstract double Evaluate(double d);
	}
}
