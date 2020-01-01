using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function SUMIF<p>
	///
	/// Syntax : <br />
	///  SUMIF ( <b>range</b>, <b>criteria</b>, sum_range ) <br />
	///    <table border="0" cellpadding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><th>range</th><td>The range over which criteria is applied.  Also used for addend values when the third parameter is not present</td></tr>
	///      <tr><th>criteria</th><td>The value or expression used to filter rows from <b>range</b></td></tr>
	///      <tr><th>sum_range</th><td>Locates the top-left corner of the corresponding range of addends - values to be added (after being selected by the criteria)</td></tr>
	///    </table><br />
	/// </p>
	/// @author Josh Micich
	public class Sumif : Var2or3ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			AreaEval areaEval;
			try
			{
				areaEval = ConvertRangeArg(arg0);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return Eval(srcRowIndex, srcColumnIndex, arg1, areaEval, areaEval);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			AreaEval aeRange;
			AreaEval aeSum;
			try
			{
				aeRange = ConvertRangeArg(arg0);
				aeSum = CreateSumRange(arg2, aeRange);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return Eval(srcRowIndex, srcColumnIndex, arg1, aeRange, aeSum);
		}

		private static ValueEval Eval(int srcRowIndex, int srcColumnIndex, ValueEval arg1, AreaEval aeRange, AreaEval aeSum)
		{
			IMatchPredicate mp = Countif.CreateCriteriaPredicate(arg1, srcRowIndex, srcColumnIndex);
			double value = SumMatchingCells(aeRange, mp, aeSum);
			return new NumberEval(value);
		}

		private static double SumMatchingCells(AreaEval aeRange, IMatchPredicate mp, AreaEval aeSum)
		{
			int height = aeRange.Height;
			int width = aeRange.Width;
			double num = 0.0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					num += Accumulate(aeRange, mp, aeSum, i, j);
				}
			}
			return num;
		}

		private static double Accumulate(AreaEval aeRange, IMatchPredicate mp, AreaEval aeSum, int relRowIndex, int relColIndex)
		{
			if (!mp.Matches(aeRange.GetRelativeValue(relRowIndex, relColIndex)))
			{
				return 0.0;
			}
			ValueEval relativeValue = aeSum.GetRelativeValue(relRowIndex, relColIndex);
			if (relativeValue is NumberEval)
			{
				return ((NumberEval)relativeValue).NumberValue;
			}
			return 0.0;
		}

		/// @return a range of the same dimensions as aeRange using eval to define the top left corner.
		/// @throws EvaluationException if eval is not a reference
		private static AreaEval CreateSumRange(ValueEval eval, AreaEval aeRange)
		{
			if (eval is AreaEval)
			{
				return ((AreaEval)eval).Offset(0, aeRange.Height - 1, 0, aeRange.Width - 1);
			}
			if (eval is RefEval)
			{
				return ((RefEval)eval).Offset(0, aeRange.Height - 1, 0, aeRange.Width - 1);
			}
			throw new EvaluationException(ErrorEval.VALUE_INVALID);
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
