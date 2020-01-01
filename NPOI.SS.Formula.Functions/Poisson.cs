using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Poisson : Fixed3ArgFunction
	{
		private const double DEFAULT_RETURN_RESULT = 1.0;

		/// All long-representable factorials 
		private long[] FACTORIALS = new long[21]
		{
			1L,
			1L,
			2L,
			6L,
			24L,
			120L,
			720L,
			5040L,
			40320L,
			362880L,
			3628800L,
			39916800L,
			479001600L,
			6227020800L,
			87178291200L,
			1307674368000L,
			20922789888000L,
			355687428096000L,
			6402373705728000L,
			121645100408832000L,
			2432902008176640000L
		};

		/// This checks is x = 0 and the mean = 0.
		/// Excel currently returns the value 1 where as the
		/// maths common implementation will error.
		/// @param x  The number.
		/// @param mean The mean.
		/// @return If a default value should be returned.
		private bool IsDefaultResult(double x, double mean)
		{
			if (x == 0.0 && mean == 0.0)
			{
				return true;
			}
			return false;
		}

		private bool CheckArgument(double aDouble)
		{
			NumericFunction.CheckValue(aDouble);
			if (aDouble < 0.0)
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
			return true;
		}

		private double probability(int k, double lambda)
		{
			return Math.Pow(lambda, (double)k) * Math.Exp(0.0 - lambda) / (double)Factorial(k);
		}

		private double cumulativeProbability(int x, double lambda)
		{
			double num = 0.0;
			for (int i = 0; i <= x; i++)
			{
				num += probability(i, lambda);
			}
			return num;
		}

		public long Factorial(int n)
		{
			if (n < 0 || n > 20)
			{
				throw new ArgumentException("Valid argument should be in the range [0..20]");
			}
			return FACTORIALS[n];
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			double num = 0.0;
			double num2 = 0.0;
			bool booleanValue = ((BoolEval)arg2).BooleanValue;
			double num3 = 0.0;
			try
			{
				num2 = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				if (IsDefaultResult(num2, num))
				{
					return new NumberEval(1.0);
				}
				CheckArgument(num2);
				CheckArgument(num);
				num3 = ((!booleanValue) ? probability((int)num2, num) : cumulativeProbability((int)num2, num));
				NumericFunction.CheckValue(num3);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num3);
		}
	}
}
