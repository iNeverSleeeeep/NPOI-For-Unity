using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function SUMIFS<br />
	/// <p>
	/// Syntax : <br />
	///  SUMIFS ( <b>sum_range</b>, <b>criteria_range1</b>, <b>criteria1</b>,
	///  [<b>criteria_range2</b>,  <b>criteria2</b>], ...) <br />
	///    <ul>
	///      <li><b>sum_range</b> Required. One or more cells to sum, including numbers or names, ranges,
	///      or cell references that contain numbers. Blank and text values are ignored.</li>
	///      <li><b>criteria1_range</b> Required. The first range in which
	///      to evaluate the associated criteria.</li>
	///      <li><b>criteria1</b> Required. The criteria in the form of a number, expression,
	///        cell reference, or text that define which cells in the criteria_range1
	///        argument will be added</li>
	///      <li><b> criteria_range2, criteria2, ...</b>    Optional. Additional ranges and their associated criteria.
	///      Up to 127 range/criteria pairs are allowed.</li>
	///    </ul>
	/// </p>
	///
	/// @author Yegor Kozlov
	public class Sumifs : FreeRefFunction
	{
		public static FreeRefFunction instance = new Sumifs();

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length >= 3 && args.Length % 2 != 0)
			{
				try
				{
					AreaEval areaEval = ConvertRangeArg(args[0]);
					AreaEval[] array = new AreaEval[(args.Length - 1) / 2];
					IMatchPredicate[] array2 = new IMatchPredicate[array.Length];
					int num = 1;
					int num2 = 0;
					while (num < args.Length)
					{
						array[num2] = ConvertRangeArg(args[num]);
						array2[num2] = Countif.CreateCriteriaPredicate(args[num + 1], ec.RowIndex, ec.ColumnIndex);
						num += 2;
						num2++;
					}
					ValidateCriteriaRanges(array, areaEval);
					double value = SumMatchingCells(array, array2, areaEval);
					return new NumberEval(value);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		/// Verify that each <code>criteriaRanges</code> argument contains the same number of rows and columns
		/// as the <code>sumRange</code> argument
		///
		/// @throws EvaluationException if
		private void ValidateCriteriaRanges(AreaEval[] criteriaRanges, AreaEval sumRange)
		{
			int num = 0;
			while (true)
			{
				if (num >= criteriaRanges.Length)
				{
					return;
				}
				AreaEval areaEval = criteriaRanges[num];
				if (areaEval.Height != sumRange.Height || areaEval.Width != sumRange.Width)
				{
					break;
				}
				num++;
			}
			throw EvaluationException.InvalidValue();
		}

		/// @param ranges  criteria ranges, each range must be of the same dimensions as <code>aeSum</code>
		/// @param predicates  array of predicates, a predicate for each value in <code>ranges</code>
		/// @param aeSum  the range to sum
		///
		/// @return the computed value
		private static double SumMatchingCells(AreaEval[] ranges, IMatchPredicate[] predicates, AreaEval aeSum)
		{
			int height = aeSum.Height;
			int width = aeSum.Width;
			double num = 0.0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					bool flag = true;
					for (int k = 0; k < ranges.Length; k++)
					{
						AreaEval areaEval = ranges[k];
						IMatchPredicate matchPredicate = predicates[k];
						if (!matchPredicate.Matches(areaEval.GetRelativeValue(i, j)))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num += Accumulate(aeSum, i, j);
					}
				}
			}
			return num;
		}

		private static double Accumulate(AreaEval aeSum, int relRowIndex, int relColIndex)
		{
			ValueEval relativeValue = aeSum.GetRelativeValue(relRowIndex, relColIndex);
			if (relativeValue is NumberEval)
			{
				return ((NumberEval)relativeValue).NumberValue;
			}
			return 0.0;
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
