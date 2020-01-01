using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implements the Excel Rate function
	public class Rate : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length < 3)
			{
				return ErrorEval.VALUE_INVALID;
			}
			double fv = 0.0;
			double type = 0.0;
			double guess = 0.1;
			double num;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcRowIndex, srcColumnIndex);
				ValueEval singleValue2 = OperandResolver.GetSingleValue(args[1], srcRowIndex, srcColumnIndex);
				ValueEval singleValue3 = OperandResolver.GetSingleValue(args[2], srcRowIndex, srcColumnIndex);
				ValueEval ev = null;
				if (args.Length >= 4)
				{
					ev = OperandResolver.GetSingleValue(args[3], srcRowIndex, srcColumnIndex);
				}
				ValueEval ev2 = null;
				if (args.Length >= 5)
				{
					ev2 = OperandResolver.GetSingleValue(args[4], srcRowIndex, srcColumnIndex);
				}
				ValueEval ev3 = null;
				if (args.Length >= 6)
				{
					ev3 = OperandResolver.GetSingleValue(args[5], srcRowIndex, srcColumnIndex);
				}
				double nper = OperandResolver.CoerceValueToDouble(singleValue);
				double pmt = OperandResolver.CoerceValueToDouble(singleValue2);
				double pv = OperandResolver.CoerceValueToDouble(singleValue3);
				if (args.Length >= 4)
				{
					fv = OperandResolver.CoerceValueToDouble(ev);
				}
				if (args.Length >= 5)
				{
					type = OperandResolver.CoerceValueToDouble(ev2);
				}
				if (args.Length >= 6)
				{
					guess = OperandResolver.CoerceValueToDouble(ev3);
				}
				num = CalculateRate(nper, pmt, pv, fv, type, guess);
				CheckValue(num);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num);
		}

		private double CalculateRate(double nper, double pmt, double pv, double fv, double type, double guess)
		{
			int num = 20;
			double num2 = 1E-07;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			double num6 = guess;
			if (Math.Abs(num6) < num2)
			{
				double num7 = pv * (1.0 + nper * num6) + pmt * (1.0 + num6 * type) * nper + fv;
			}
			else
			{
				num4 = Math.Exp(nper * Math.Log(1.0 + num6));
				double num7 = pv * num4 + pmt * (1.0 / num6 + type) * (num4 - 1.0) + fv;
			}
			double num8 = pv + pmt * nper + fv;
			double num9 = pv * num4 + pmt * (1.0 / num6 + type) * (num4 - 1.0) + fv;
			double num10;
			num5 = (num10 = 0.0);
			num3 = num6;
			while (Math.Abs(num8 - num9) > num2 && num5 < (double)num)
			{
				num6 = (num9 * num10 - num8 * num3) / (num9 - num8);
				num10 = num3;
				num3 = num6;
				double num7;
				if (Math.Abs(num6) < num2)
				{
					num7 = pv * (1.0 + nper * num6) + pmt * (1.0 + num6 * type) * nper + fv;
				}
				else
				{
					num4 = Math.Exp(nper * Math.Log(1.0 + num6));
					num7 = pv * num4 + pmt * (1.0 / num6 + type) * (num4 - 1.0) + fv;
				}
				num8 = num9;
				num9 = num7;
				num5 += 1.0;
			}
			return num6;
		}

		/// Excel does not support infinities and NaNs, rather, it gives a #NUM! error in these cases
		///
		/// @throws EvaluationException (#NUM!) if result is NaN or Infinity
		private static void CheckValue(double result)
		{
			if (double.IsNaN(result) || double.IsInfinity(result))
			{
				throw new EvaluationException(ErrorEval.NUM_ERROR);
			}
		}
	}
}
