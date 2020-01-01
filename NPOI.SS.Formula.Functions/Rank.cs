using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// * Returns the rank of a number in a list of numbers. The rank of a number is its size relative to other values in a list.
	///
	/// * Syntax:
	/// *    RANK(number,ref,order)
	/// *       Number   is the number whose rank you want to find.
	/// *       Ref     is an array of, or a reference to, a list of numbers. Nonnumeric values in ref are ignored.
	/// *       Order   is a number specifying how to rank number.
	///
	/// * If order is 0 (zero) or omitted, Microsoft Excel ranks number as if ref were a list sorted in descending order.
	/// * If order is any nonzero value, Microsoft Excel ranks number as if ref were a list sorted in ascending order.
	/// * 
	/// * @author Rubin Wang
	public class Rank : Var2or3ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			double num;
			AreaEval aeRange;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				num = OperandResolver.CoerceValueToDouble(singleValue);
				if (double.IsNaN(num) || double.IsInfinity(num))
				{
					throw new EvaluationException(ErrorEval.NUM_ERROR);
				}
				aeRange = ConvertRangeArg(arg1);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return eval(srcRowIndex, srcColumnIndex, num, aeRange, descending_order: true);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			bool flag = false;
			double num;
			AreaEval aeRange;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				num = OperandResolver.CoerceValueToDouble(singleValue);
				if (double.IsNaN(num) || double.IsInfinity(num))
				{
					throw new EvaluationException(ErrorEval.NUM_ERROR);
				}
				aeRange = ConvertRangeArg(arg1);
				singleValue = OperandResolver.GetSingleValue(arg2, srcRowIndex, srcColumnIndex);
				switch (OperandResolver.CoerceValueToInt(singleValue))
				{
				case 0:
					flag = true;
					break;
				case 1:
					flag = false;
					break;
				default:
					throw new EvaluationException(ErrorEval.NUM_ERROR);
				}
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return eval(srcRowIndex, srcColumnIndex, num, aeRange, flag);
		}

		private static ValueEval eval(int srcRowIndex, int srcColumnIndex, double arg0, AreaEval aeRange, bool descending_order)
		{
			int num = 1;
			int height = aeRange.Height;
			int width = aeRange.Width;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					double value = GetValue(aeRange, i, j);
					if (!double.IsNaN(value) && ((descending_order && value > arg0) || (!descending_order && value < arg0)))
					{
						num++;
					}
				}
			}
			return new NumberEval((double)num);
		}

		private static double GetValue(AreaEval aeRange, int relRowIndex, int relColIndex)
		{
			ValueEval relativeValue = aeRange.GetRelativeValue(relRowIndex, relColIndex);
			if (relativeValue is NumberEval)
			{
				return ((NumberEval)relativeValue).NumberValue;
			}
			return double.NaN;
		}

		private static AreaEval ConvertRangeArg(ValueEval eval)
		{
			if (eval is AreaEval)
			{
				return (AreaEval)eval;
			}
			if (eval is RefEval)
			{
				return ((RefEval)eval).Offset(0, 0, 0, 0);
			}
			throw new EvaluationException(ErrorEval.VALUE_INVALID);
		}
	}
}
