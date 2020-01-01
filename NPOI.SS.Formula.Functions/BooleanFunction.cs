using NPOI.SS.Formula.Eval;
using System;
using System.Globalization;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// Here are the general rules concerning bool functions:
	///
	/// - Blanks are not either true or false
	/// - Strings are not either true or false (even strings "true" or "TRUE" or "0" etc.)
	/// - Numbers: 0 Is false. Any other number Is TRUE.
	/// - References are Evaluated and above rules apply.
	/// - Areas: Individual cells in area are Evaluated and Checked to 
	/// see if they are blanks, strings etc.
	public abstract class BooleanFunction : Function
	{
		protected abstract bool InitialResultValue
		{
			get;
		}

		protected abstract bool PartialEvaluate(bool cumulativeResult, bool currentValue);

		private bool Calculate(ValueEval[] args)
		{
			bool flag = InitialResultValue;
			bool flag2 = false;
			int i = 0;
			for (int num = args.Length; i < num; i++)
			{
				ValueEval valueEval = args[i];
				if (valueEval is TwoDEval)
				{
					TwoDEval twoDEval = (TwoDEval)valueEval;
					int height = twoDEval.Height;
					int width = twoDEval.Width;
					for (int j = 0; j < height; j++)
					{
						for (int k = 0; k < width; k++)
						{
							ValueEval value = twoDEval.GetValue(j, k);
							bool? flag3 = OperandResolver.CoerceValueToBoolean(value, stringsAreBlanks: true);
							if (flag3.HasValue)
							{
								flag = PartialEvaluate(flag, Convert.ToBoolean(flag3, CultureInfo.InvariantCulture));
								flag2 = true;
							}
						}
					}
				}
				else
				{
					bool? flag3;
					if (valueEval is RefEval)
					{
						ValueEval innerValueEval = ((RefEval)valueEval).InnerValueEval;
						flag3 = OperandResolver.CoerceValueToBoolean(innerValueEval, stringsAreBlanks: true);
					}
					else if (valueEval != null)
					{
						ValueEval ve = valueEval;
						flag3 = OperandResolver.CoerceValueToBoolean(ve, stringsAreBlanks: false);
					}
					else
					{
						if (valueEval != MissingArgEval.instance)
						{
							throw new InvalidOperationException("Unexpected eval (" + valueEval.GetType().Name + ")");
						}
						flag3 = null;
					}
					if (flag3.HasValue)
					{
						flag = PartialEvaluate(flag, Convert.ToBoolean(flag3, CultureInfo.InvariantCulture));
						flag2 = true;
					}
				}
			}
			if (!flag2)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			return flag;
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRow, int srcCol)
		{
			if (args.Length < 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			bool b;
			try
			{
				b = Calculate(args);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return BoolEval.ValueOf(b);
		}
	}
}
