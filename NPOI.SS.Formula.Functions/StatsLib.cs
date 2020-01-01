using NPOI.Util;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	///
	/// Library for common statistics functions
	public class StatsLib
	{
		private StatsLib()
		{
		}

		/// returns the mean of deviations from mean.
		/// @param v
		public static double avedev(double[] v)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			int i = 0;
			for (int num4 = v.Length; i < num4; i++)
			{
				num3 += v[i];
			}
			num2 = num3 / (double)v.Length;
			num3 = 0.0;
			int j = 0;
			for (int num5 = v.Length; j < num5; j++)
			{
				num3 += Math.Abs(v[j] - num2);
			}
			return num3 / (double)v.Length;
		}

		public static double stdev(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length > 1)
			{
				result = Math.Sqrt(devsq(v) / (double)(v.Length - 1));
			}
			return result;
		}

		public static double var(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length > 1)
			{
				result = devsq(v) / (double)(v.Length - 1);
			}
			return result;
		}

		public static double varp(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length > 1)
			{
				result = devsq(v) / (double)v.Length;
			}
			return result;
		}

		/// if v Is zero Length or Contains no duplicates, return value
		/// Is double.NaN. Else returns the value that occurs most times
		/// and if there Is a tie, returns the first such value. 
		/// @param v
		public static double mode(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length > 1)
			{
				int[] array = new int[v.Length];
				Arrays.Fill(array, 1);
				int i = 0;
				for (int num = v.Length; i < num; i++)
				{
					int j = i + 1;
					for (int num2 = v.Length; j < num2; j++)
					{
						if (v[i] == v[j])
						{
							array[i]++;
						}
					}
				}
				double num3 = 0.0;
				int num4 = 0;
				int k = 0;
				for (int num5 = array.Length; k < num5; k++)
				{
					if (array[k] > num4)
					{
						num3 = v[k];
						num4 = array[k];
					}
				}
				result = ((num4 > 1) ? num3 : double.NaN);
			}
			return result;
		}

		public static double median(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length >= 1)
			{
				int num = v.Length;
				Array.Sort(v);
				result = ((num % 2 == 0) ? ((v[num / 2] + v[num / 2 - 1]) / 2.0) : v[num / 2]);
			}
			return result;
		}

		public static double devsq(double[] v)
		{
			double result = double.NaN;
			if (v != null && v.Length >= 1)
			{
				double num = 0.0;
				double num2 = 0.0;
				int num3 = v.Length;
				for (int i = 0; i < num3; i++)
				{
					num2 += v[i];
				}
				num = num2 / (double)num3;
				num2 = 0.0;
				for (int j = 0; j < num3; j++)
				{
					num2 += (v[j] - num) * (v[j] - num);
				}
				result = ((num3 == 1) ? 0.0 : num2);
			}
			return result;
		}

		public static double kthLargest(double[] v, int k)
		{
			double result = double.NaN;
			k--;
			if (v != null && v.Length > k && k >= 0)
			{
				Array.Sort(v);
				result = v[v.Length - k - 1];
			}
			return result;
		}

		public static double kthSmallest(double[] v, int k)
		{
			double result = double.NaN;
			k--;
			if (v != null && v.Length > k && k >= 0)
			{
				Array.Sort(v);
				result = v[k];
			}
			return result;
		}
	}
}
