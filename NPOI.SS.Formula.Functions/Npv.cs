using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Npv : Function
	{
		[Obsolete]
		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num2;
			try
			{
				double rate = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				num2 = Evaluate(rate, num);
				NumericFunction.CheckValue(num2);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num2);
		}

		[Obsolete]
		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			double num3;
			try
			{
				double rate = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double num2 = NumericFunction.SingleOperandEvaluate(arg2, srcRowIndex, srcColumnIndex);
				num3 = Evaluate(rate, num, num2);
				NumericFunction.CheckValue(num3);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num3);
		}

		[Obsolete]
		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3)
		{
			double num4;
			try
			{
				double rate = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double num2 = NumericFunction.SingleOperandEvaluate(arg2, srcRowIndex, srcColumnIndex);
				double num3 = NumericFunction.SingleOperandEvaluate(arg3, srcRowIndex, srcColumnIndex);
				num4 = Evaluate(rate, num, num2, num3);
				NumericFunction.CheckValue(num4);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num4);
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			int num = args.Length;
			if (num >= 2)
			{
				try
				{
					double r = NumericFunction.SingleOperandEvaluate(args[0], srcRowIndex, srcColumnIndex);
					ValueEval[] array = new ValueEval[args.Length - 1];
					Array.Copy(args, 1, array, 0, array.Length);
					double[] cfs = AggregateFunction.ValueCollector.CollectValues(array);
					double num2 = FinanceLib.npv(r, cfs);
					NumericFunction.CheckValue(num2);
					return new NumberEval(num2);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		private static double Evaluate(double rate, params double[] ds)
		{
			double num = 0.0;
			for (int i = 0; i < ds.Length; i++)
			{
				num += ds[i] / Math.Pow(rate + 1.0, (double)i);
			}
			return num;
		}
	}
}
