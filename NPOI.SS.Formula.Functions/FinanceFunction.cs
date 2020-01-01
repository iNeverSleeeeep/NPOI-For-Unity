using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// Super class for all Evals for financial function evaluation.
	public abstract class FinanceFunction : Function3Arg, Function4Arg, Function
	{
		private static ValueEval DEFAULT_ARG3 = NumberEval.ZERO;

		private static ValueEval DEFAULT_ARG4 = BoolEval.FALSE;

		public static readonly Function FV = new Fv();

		public static readonly Function NPER = new Nper();

		public static readonly Function PMT = new Pmt();

		public static readonly Function PV = new Pv();

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			return Evaluate(srcRowIndex, srcColumnIndex, arg0, arg1, arg2, DEFAULT_ARG3);
		}

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3)
		{
			return Evaluate(srcRowIndex, srcColumnIndex, arg0, arg1, arg2, arg3, DEFAULT_ARG4);
		}

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3, ValueEval arg4)
		{
			double num2;
			try
			{
				double rate = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double arg5 = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double arg6 = NumericFunction.SingleOperandEvaluate(arg2, srcRowIndex, srcColumnIndex);
				double arg7 = NumericFunction.SingleOperandEvaluate(arg3, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg4, srcRowIndex, srcColumnIndex);
				num2 = Evaluate(rate, arg5, arg6, arg7, num != 0.0);
				NumericFunction.CheckValue(num2);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num2);
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 3:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], DEFAULT_ARG3, DEFAULT_ARG4);
			case 4:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], args[3], DEFAULT_ARG4);
			case 5:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], args[3], args[4]);
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}

		public double Evaluate(double[] ds)
		{
			double arg = 0.0;
			double num = 0.0;
			switch (ds.Length)
			{
			case 5:
				num = ds[4];
				break;
			case 4:
				arg = ds[3];
				break;
			default:
				throw new ArgumentException("Wrong number of arguments");
			case 3:
				break;
			}
			return Evaluate(ds[0], ds[1], ds[2], arg, num != 0.0);
		}

		public abstract double Evaluate(double rate, double arg1, double arg2, double arg3, bool type);
	}
}
