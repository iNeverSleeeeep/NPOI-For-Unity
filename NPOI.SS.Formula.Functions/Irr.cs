using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Calculates the internal rate of return.
	///
	/// Syntax is IRR(values) or IRR(values,guess)
	///
	/// @author Marcel May
	/// @author Yegor Kozlov
	///
	/// @see <a href="http://en.wikipedia.org/wiki/Internal_rate_of_return#Numerical_solution">Wikipedia on IRR</a>
	/// @see <a href="http://office.microsoft.com/en-us/excel-help/irr-HP005209146.aspx">Excel IRR</a>
	public class Irr : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length != 0 && args.Length <= 2)
			{
				try
				{
					double[] values = AggregateFunction.ValueCollector.CollectValues(args[0]);
					double guess = (args.Length != 2) ? 0.1 : NumericFunction.SingleOperandEvaluate(args[1], srcRowIndex, srcColumnIndex);
					double num = irr(values, guess);
					NumericFunction.CheckValue(num);
					return new NumberEval(num);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		/// Computes the internal rate of return using an estimated irr of 10 percent.
		///
		/// @param income the income values.
		/// @return the irr.
		public static double irr(double[] income)
		{
			return irr(income, 0.1);
		}

		/// Calculates IRR using the Newton-Raphson Method.
		/// <p>
		/// Starting with the guess, the method cycles through the calculation until the result
		/// is accurate within 0.00001 percent. If IRR can't find a result that works
		/// after 20 tries, the Double.NaN is returned.
		/// </p>
		/// <p>
		///   The implementation is inspired by the NewtonSolver from the Apache Commons-Math library,
		///   @see <a href="http://commons.apache.org">http://commons.apache.org</a>
		/// </p>
		///
		/// @param values        the income values.
		/// @param guess         the initial guess of irr.
		/// @return the irr value. The method returns <code>Double.NaN</code>
		///  if the maximum iteration count is exceeded
		///
		/// @see <a href="http://en.wikipedia.org/wiki/Internal_rate_of_return#Numerical_solution">
		///     http://en.wikipedia.org/wiki/Internal_rate_of_return#Numerical_solution</a>
		/// @see <a href="http://en.wikipedia.org/wiki/Newton%27s_method">
		///     http://en.wikipedia.org/wiki/Newton%27s_method</a>
		public static double irr(double[] values, double guess)
		{
			int num = 20;
			double num2 = 1E-07;
			double num3 = guess;
			for (int i = 0; i < num; i++)
			{
				double num4 = 0.0;
				double num5 = 0.0;
				for (int j = 0; j < values.Length; j++)
				{
					num4 += values[j] / Math.Pow(1.0 + num3, (double)j);
					num5 += (double)(-j) * values[j] / Math.Pow(1.0 + num3, (double)(j + 1));
				}
				double num6 = num3 - num4 / num5;
				if (Math.Abs(num6 - num3) <= num2)
				{
					return num6;
				}
				num3 = num6;
			}
			return double.NaN;
		}
	}
}
