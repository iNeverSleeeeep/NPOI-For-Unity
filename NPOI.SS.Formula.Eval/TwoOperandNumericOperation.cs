using NPOI.SS.Formula.Functions;

namespace NPOI.SS.Formula.Eval
{
	public abstract class TwoOperandNumericOperation : Fixed2ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function AddEval = new AddEval();

		public static NPOI.SS.Formula.Functions.Function DivideEval = new DivideEval();

		public static NPOI.SS.Formula.Functions.Function MultiplyEval = new MultiplyEval();

		public static NPOI.SS.Formula.Functions.Function PowerEval = new PowerEval();

		public static NPOI.SS.Formula.Functions.Function SubtractEval = new SubtractEval();

		protected double SingleOperandEvaluate(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			return OperandResolver.CoerceValueToDouble(singleValue);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num;
			try
			{
				double d = SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double d2 = SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				num = Evaluate(d, d2);
				if (num == 0.0 && !(this is SubtractEval))
				{
					return NumberEval.ZERO;
				}
				if (double.IsNaN(num) || double.IsInfinity(num))
				{
					return ErrorEval.NUM_ERROR;
				}
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		public abstract double Evaluate(double d0, double d1);
	}
}
