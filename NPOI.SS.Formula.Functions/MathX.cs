using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// This class Is an extension to the standard math library
	/// provided by java.lang.Math class. It follows the Math class
	/// in that it has a private constructor and all static methods.
	public class MathX
	{
		private MathX()
		{
		}

		/// Returns a value rounded to p digits after decimal.
		/// If p Is negative, then the number Is rounded to
		/// places to the left of the decimal point. eg. 
		/// 10.23 rounded to -1 will give: 10. If p Is zero,
		/// the returned value Is rounded to the nearest integral
		/// value.
		/// If n Is negative, the resulting value Is obtained
		/// as the round value of absolute value of n multiplied
		/// by the sign value of n (@see MathX.sign(double d)). 
		/// Thus, -0.6666666 rounded to p=0 will give -1 not 0.
		/// If n Is NaN, returned value Is NaN.
		/// @param n
		/// @param p
		public static double round(double n, int p)
		{
			if (double.IsNaN(n) || double.IsInfinity(n))
			{
				return double.NaN;
			}
			decimal d = (decimal)Math.Pow(10.0, (double)p);
			return (double)(Math.Round((decimal)n * d) / d);
		}

		/// Returns a value rounded-up to p digits after decimal.
		/// If p Is negative, then the number Is rounded to
		/// places to the left of the decimal point. eg. 
		/// 10.23 rounded to -1 will give: 20. If p Is zero,
		/// the returned value Is rounded to the nearest integral
		/// value.
		/// If n Is negative, the resulting value Is obtained
		/// as the round-up value of absolute value of n multiplied
		/// by the sign value of n (@see MathX.sign(double d)). 
		/// Thus, -0.2 rounded-up to p=0 will give -1 not 0.
		/// If n Is NaN, returned value Is NaN.
		/// @param n
		/// @param p
		public static double roundUp(double n, int p)
		{
			if (double.IsNaN(n) || double.IsInfinity(n))
			{
				return double.NaN;
			}
			if (p != 0)
			{
				double num = Math.Pow(10.0, (double)p);
				double num2 = Math.Abs(n * num);
				return (double)sign(n) * ((num2 == (double)(long)num2) ? (num2 / num) : (Math.Round(num2 + 0.5) / num));
			}
			double num3 = Math.Abs(n);
			return (double)sign(n) * ((num3 == (double)(long)num3) ? num3 : ((double)((long)num3 + 1)));
		}

		/// Returns a value rounded to p digits after decimal.
		/// If p Is negative, then the number Is rounded to
		/// places to the left of the decimal point. eg. 
		/// 10.23 rounded to -1 will give: 10. If p Is zero,
		/// the returned value Is rounded to the nearest integral
		/// value.
		/// If n Is negative, the resulting value Is obtained
		/// as the round-up value of absolute value of n multiplied
		/// by the sign value of n (@see MathX.sign(double d)). 
		/// Thus, -0.8 rounded-down to p=0 will give 0 not -1.
		/// If n Is NaN, returned value Is NaN.
		/// @param n
		/// @param p
		public static double roundDown(double n, int p)
		{
			if (double.IsNaN(n) || double.IsInfinity(n))
			{
				return double.NaN;
			}
			if (p != 0)
			{
				double num = Math.Pow(10.0, (double)p);
				return (double)sign(n) * Math.Round(Math.Abs(n) * num - 0.5, MidpointRounding.AwayFromZero) / num;
			}
			return (double)(long)n;
		}

		public static short sign(double d)
		{
			return (short)((d != 0.0) ? ((!(d < 0.0)) ? 1 : (-1)) : 0);
		}

		/// average of all values
		/// @param values
		public static double average(double[] values)
		{
			double num = 0.0;
			double num2 = 0.0;
			int i = 0;
			for (int num3 = values.Length; i < num3; i++)
			{
				num2 += values[i];
			}
			return num2 / (double)values.Length;
		}

		/// sum of all values
		/// @param values
		public static double sum(double[] values)
		{
			double num = 0.0;
			int i = 0;
			for (int num2 = values.Length; i < num2; i++)
			{
				num += values[i];
			}
			return num;
		}

		/// sum of squares of all values
		/// @param values
		public static double sumsq(double[] values)
		{
			double num = 0.0;
			int i = 0;
			for (int num2 = values.Length; i < num2; i++)
			{
				num += values[i] * values[i];
			}
			return num;
		}

		/// product of all values
		/// @param values
		public static double product(double[] values)
		{
			double num = 0.0;
			if (values != null && values.Length > 0)
			{
				num = 1.0;
				int i = 0;
				for (int num2 = values.Length; i < num2; i++)
				{
					num *= values[i];
				}
			}
			return num;
		}

		/// min of all values. If supplied array Is zero Length,
		/// double.POSITIVE_INFINITY Is returned.
		/// @param values
		public static double min(double[] values)
		{
			double num = double.PositiveInfinity;
			int i = 0;
			for (int num2 = values.Length; i < num2; i++)
			{
				num = Math.Min(num, values[i]);
			}
			return num;
		}

		/// min of all values. If supplied array Is zero Length,
		/// double.NEGATIVE_INFINITY Is returned.
		/// @param values
		public static double max(double[] values)
		{
			double num = double.NegativeInfinity;
			int i = 0;
			for (int num2 = values.Length; i < num2; i++)
			{
				num = Math.Max(num, values[i]);
			}
			return num;
		}

		/// Note: this function Is different from java.lang.Math.floor(..).
		///
		/// When n and s are "valid" arguments, the returned value Is: Math.floor(n/s) * s;
		/// <br />
		/// n and s are invalid if any of following conditions are true:
		/// <ul>
		/// <li>s Is zero</li>
		/// <li>n Is negative and s Is positive</li>
		/// <li>n Is positive and s Is negative</li>
		/// </ul>
		/// In all such cases, double.NaN Is returned.
		/// @param n
		/// @param s
		public static double floor(double n, double s)
		{
			if ((n < 0.0 && s > 0.0) || (n > 0.0 && s < 0.0) || (s == 0.0 && n != 0.0))
			{
				return double.NaN;
			}
			return (n == 0.0 || s == 0.0) ? 0.0 : (Math.Floor(n / s) * s);
		}

		/// Note: this function Is different from java.lang.Math.ceil(..).
		///
		/// When n and s are "valid" arguments, the returned value Is: Math.ceiling(n/s) * s;
		/// <br />
		/// n and s are invalid if any of following conditions are true:
		/// <ul>
		/// <li>s Is zero</li>
		/// <li>n Is negative and s Is positive</li>
		/// <li>n Is positive and s Is negative</li>
		/// </ul>
		/// In all such cases, double.NaN Is returned.
		/// @param n
		/// @param s
		public static double ceiling(double n, double s)
		{
			if ((n < 0.0 && s > 0.0) || (n > 0.0 && s < 0.0))
			{
				return double.NaN;
			}
			return (n == 0.0 || s == 0.0) ? 0.0 : (Math.Ceiling(n / s) * s);
		}

		/// <br /> for all n &gt;= 1; factorial n = n * (n-1) * (n-2) * ... * 1 
		/// <br /> else if n == 0; factorial n = 1
		/// <br /> else if n &lt; 0; factorial n = double.NaN
		/// <br /> Loss of precision can occur if n Is large enough.
		/// If n Is large so that the resulting value would be greater 
		/// than double.MAX_VALUE; double.POSITIVE_INFINITY Is returned.
		/// If n &lt; 0, double.NaN Is returned. 
		/// @param n
		public static double factorial(int n)
		{
			double num = 1.0;
			if (n >= 0)
			{
				if (n <= 170)
				{
					for (int i = 1; i <= n; i++)
					{
						num *= (double)i;
					}
				}
				else
				{
					num = double.PositiveInfinity;
				}
			}
			else
			{
				num = double.NaN;
			}
			return num;
		}

		/// returns the remainder resulting from operation:
		/// n / d. 
		/// <br /> The result has the sign of the divisor.
		/// <br /> Examples:
		/// <ul>
		/// <li>mod(3.4, 2) = 1.4</li>
		/// <li>mod(-3.4, 2) = 0.6</li>
		/// <li>mod(-3.4, -2) = -1.4</li>
		/// <li>mod(3.4, -2) = -0.6</li>
		/// </ul>
		/// If d == 0, result Is NaN
		/// @param n
		/// @param d
		public static double mod(double n, double d)
		{
			double num = 0.0;
			if (d == 0.0)
			{
				return double.NaN;
			}
			if (sign(n) == sign(d))
			{
				return n % d;
			}
			return (n % d + d) % d;
		}

		/// inverse hyperbolic cosine
		/// @param d
		public static double acosh(double d)
		{
			return Math.Log(Math.Sqrt(Math.Pow(d, 2.0) - 1.0) + d);
		}

		/// inverse hyperbolic sine
		/// @param d
		public static double asinh(double d)
		{
			return Math.Log(Math.Sqrt(d * d + 1.0) + d);
		}

		/// inverse hyperbolic tangent
		/// @param d
		public static double atanh(double d)
		{
			return Math.Log((1.0 + d) / (1.0 - d)) / 2.0;
		}

		/// hyperbolic cosine
		/// @param d
		public static double cosh(double d)
		{
			double num = Math.Pow(2.7182818284590451, d);
			double num2 = Math.Pow(2.7182818284590451, 0.0 - d);
			d = (num + num2) / 2.0;
			return d;
		}

		/// hyperbolic sine
		/// @param d
		public static double sinh(double d)
		{
			double num = Math.Pow(2.7182818284590451, d);
			double num2 = Math.Pow(2.7182818284590451, 0.0 - d);
			d = (num - num2) / 2.0;
			return d;
		}

		/// hyperbolic tangent
		/// @param d
		public static double tanh(double d)
		{
			double num = Math.Pow(2.7182818284590451, d);
			double num2 = Math.Pow(2.7182818284590451, 0.0 - d);
			d = (num - num2) / (num + num2);
			return d;
		}

		/// returns the sum of product of corresponding double value in each
		/// subarray. It Is the responsibility of the caller to Ensure that
		/// all the subarrays are of equal Length. If the subarrays are
		/// not of equal Length, the return value can be Unpredictable.
		/// @param arrays
		public static double sumproduct(double[][] arrays)
		{
			double num = 0.0;
			try
			{
				int num2 = arrays.Length;
				int num3 = arrays[0].Length;
				for (int i = 0; i < num3; i++)
				{
					double num4 = 1.0;
					for (int j = 0; j < num2; j++)
					{
						num4 *= arrays[j][i];
					}
					num += num4;
				}
				return num;
			}
			catch (IndexOutOfRangeException)
			{
				return double.NaN;
			}
		}

		/// returns the sum of difference of squares of corresponding double 
		/// value in each subarray: ie. sigma (xarr[i]^2-yarr[i]^2) 
		/// <br />
		/// It Is the responsibility of the caller 
		/// to Ensure that the two subarrays are of equal Length. If the 
		/// subarrays are not of equal Length, the return value can be 
		/// Unpredictable.
		/// @param xarr
		/// @param yarr
		public static double sumx2my2(double[] xarr, double[] yarr)
		{
			double num = 0.0;
			try
			{
				int i = 0;
				for (int num2 = xarr.Length; i < num2; i++)
				{
					num += (xarr[i] + yarr[i]) * (xarr[i] - yarr[i]);
				}
				return num;
			}
			catch (IndexOutOfRangeException)
			{
				return double.NaN;
			}
		}

		/// returns the sum of sum of squares of corresponding double 
		/// value in each subarray: ie. sigma (xarr[i]^2 + yarr[i]^2) 
		/// <br />
		/// It Is the responsibility of the caller 
		/// to Ensure that the two subarrays are of equal Length. If the 
		/// subarrays are not of equal Length, the return value can be 
		/// Unpredictable.
		/// @param xarr
		/// @param yarr
		public static double sumx2py2(double[] xarr, double[] yarr)
		{
			double num = 0.0;
			try
			{
				int i = 0;
				for (int num2 = xarr.Length; i < num2; i++)
				{
					num += xarr[i] * xarr[i] + yarr[i] * yarr[i];
				}
				return num;
			}
			catch (IndexOutOfRangeException)
			{
				return double.NaN;
			}
		}

		/// returns the sum of squares of difference of corresponding double 
		/// value in each subarray: ie. sigma ( (xarr[i]-yarr[i])^2 ) 
		/// <br />
		/// It Is the responsibility of the caller 
		/// to Ensure that the two subarrays are of equal Length. If the 
		/// subarrays are not of equal Length, the return value can be 
		/// Unpredictable.
		/// @param xarr
		/// @param yarr
		public static double sumxmy2(double[] xarr, double[] yarr)
		{
			double num = 0.0;
			try
			{
				int i = 0;
				for (int num2 = xarr.Length; i < num2; i++)
				{
					double num3 = xarr[i] - yarr[i];
					num += num3 * num3;
				}
				return num;
			}
			catch (IndexOutOfRangeException)
			{
				return double.NaN;
			}
		}

		/// returns the total number of combinations possible when
		/// k items are chosen out of total of n items. If the number
		/// Is too large, loss of precision may occur (since returned
		/// value Is double). If the returned value Is larger than
		/// double.MAX_VALUE, double.POSITIVE_INFINITY Is returned.
		/// If either of the parameters Is negative, double.NaN Is returned.
		/// @param n
		/// @param k
		public static double nChooseK(int n, int k)
		{
			double num = 1.0;
			if (n < 0 || k < 0 || n < k)
			{
				return double.NaN;
			}
			int n2 = Math.Min(n - k, k);
			int num2 = Math.Max(n - k, k);
			for (int i = num2; i < n; i++)
			{
				num *= (double)(i + 1);
			}
			return num / factorial(n2);
		}
	}
}
