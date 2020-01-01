using NPOI.SS.Formula.Eval;
using NPOI.Util;
using System;
using System.Collections;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Mode : Function
	{
		/// if v is zero length or contains no duplicates, return value is
		/// Double.NaN. Else returns the value that occurs most times and if there is
		/// a tie, returns the first such value.
		///
		/// @param v
		public static double Evaluate(double[] v)
		{
			if (v.Length < 2)
			{
				throw new EvaluationException(ErrorEval.NA);
			}
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
			double result = 0.0;
			int num3 = 0;
			int k = 0;
			for (int num4 = array.Length; k < num4; k++)
			{
				if (array[k] > num3)
				{
					result = v[k];
					num3 = array[k];
				}
			}
			if (num3 > 1)
			{
				return result;
			}
			throw new EvaluationException(ErrorEval.NA);
		}

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			double value;
			try
			{
				IList list = new ArrayList();
				for (int i = 0; i < args.Length; i++)
				{
					CollectValues(args[i], list);
				}
				double[] array = new double[list.Count];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = (double)list[j];
				}
				value = Evaluate(array);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}

		private static void CollectValues(ValueEval arg, IList temp)
		{
			if (arg is TwoDEval)
			{
				TwoDEval twoDEval = (TwoDEval)arg;
				int width = twoDEval.Width;
				int height = twoDEval.Height;
				for (int i = 0; i < height; i++)
				{
					for (int j = 0; j < width; j++)
					{
						ValueEval value = twoDEval.GetValue(i, j);
						CollectValue(value, temp, mustBeNumber: false);
					}
				}
			}
			else if (arg is RefEval)
			{
				RefEval refEval = (RefEval)arg;
				CollectValue(refEval.InnerValueEval, temp, mustBeNumber: true);
			}
			else
			{
				CollectValue(arg, temp, mustBeNumber: true);
			}
		}

		private static void CollectValue(ValueEval arg, IList temp, bool mustBeNumber)
		{
			if (arg is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)arg);
			}
			if (arg == BlankEval.instance || arg is BoolEval || arg is StringEval)
			{
				if (mustBeNumber)
				{
					throw EvaluationException.InvalidValue();
				}
			}
			else
			{
				if (!(arg is NumberEval))
				{
					throw new InvalidOperationException("Unexpected value type (" + arg.GetType().Name + ")");
				}
				temp.Add(((NumberEval)arg).NumberValue);
			}
		}
	}
}
