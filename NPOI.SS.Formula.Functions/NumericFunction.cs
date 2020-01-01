using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at yahoo dot com &gt;
	public abstract class NumericFunction : Function
	{
		public const double TEN = 10.0;

		public const double E = 2.7182818284590451;

		public const double PI = 3.1415926535897931;

		public const double ZERO = 0.0;

		public static readonly double LOG_10_TO_BASE_e = Math.Log(10.0);

		public static readonly Function ABS = new Abs();

		public static readonly Function COS = new Cos();

		public static readonly Function COSH = new Cosh();

		public static readonly Function ACOS = new Acos();

		public static readonly Function ACOSH = new Acosh();

		public static readonly Function ASIN = new Asin();

		public static readonly Function ASINH = new Asinh();

		public static readonly Function SIN = new Sin();

		public static readonly Function SINH = new Sinh();

		public static readonly Function TAN = new Tan();

		public static readonly Function TANH = new Tanh();

		public static readonly Function ATAN = new Atan();

		public static readonly Function ATANH = new Atanh();

		public static readonly Function ATAN2 = new Atan2();

		public static readonly Function DEGREES = new Degrees();

		public static readonly Function DOLLAR = new Dollar();

		public static readonly Function EXP = new Exp();

		public static readonly Function FACT = new Fact();

		public static readonly Function INT = new Int();

		public static readonly Function LN = new Ln();

		public static readonly Function LOG10 = new Log10();

		public static readonly Function RADIANS = new Radians();

		public static readonly Function SIGN = new Sign();

		public static readonly Function SQRT = new Sqrt();

		public static readonly Function CEILING = new Ceiling();

		public static readonly Function COMBIN = new Combin();

		public static readonly Function FLOOR = new Floor();

		public static readonly Function MOD = new Mod();

		public static readonly Function POWER = new Power();

		public static readonly Function ROUND = new Round();

		public static readonly Function ROUNDDOWN = new Rounddown();

		public static readonly Function ROUNDUP = new Roundup();

		public static readonly Function LOG = new Log();

		public static readonly Function TRUNC = new Trunc();

		public static readonly Function POISSON = new Poisson();

		public static double SingleOperandEvaluate(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			double result = OperandResolver.CoerceValueToDouble(singleValue);
			CheckValue(result);
			return result;
		}

		public static void CheckValue(double result)
		{
			if (double.IsNaN(result) || double.IsInfinity(result))
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
		}

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			double num;
			try
			{
				num = Eval(args, srcCellRow, srcCellCol);
				CheckValue(num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		protected abstract double Eval(ValueEval[] evals, int srcCellRow, int srcCellCol);
	}
}
