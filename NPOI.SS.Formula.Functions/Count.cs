using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Counts the number of cells that contain numeric data within
	///  the list of arguments. 
	///
	/// Excel Syntax
	/// COUNT(value1,value2,...)
	/// Value1, value2, ...   are 1 to 30 arguments representing the values or ranges to be Counted.
	///
	/// TODO: Check this properly Matches excel on edge cases
	///  like formula cells, error cells etc
	public class Count : Function
	{
		private class DefaultPredicate : IMatchPredicate
		{
			public bool Matches(ValueEval valueEval)
			{
				if (valueEval is NumberEval)
				{
					return true;
				}
				if (valueEval == MissingArgEval.instance)
				{
					return true;
				}
				return false;
			}
		}

		private class SubtotalPredicate : I_MatchAreaPredicate, IMatchPredicate
		{
			public bool Matches(ValueEval valueEval)
			{
				return defaultPredicate.Matches(valueEval);
			}

			public bool Matches(TwoDEval x, int rowIndex, int columnIndex)
			{
				return !x.IsSubTotal(rowIndex, columnIndex);
			}
		}

		private IMatchPredicate _predicate;

		private static IMatchPredicate defaultPredicate = new DefaultPredicate();

		private static IMatchPredicate subtotalPredicate = new SubtotalPredicate();

		public Count()
		{
			_predicate = defaultPredicate;
		}

		private Count(IMatchPredicate criteriaPredicate)
		{
			_predicate = criteriaPredicate;
		}

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			int num = args.Length;
			if (num < 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			if (num > 30)
			{
				return ErrorEval.VALUE_INVALID;
			}
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 += CountUtils.CountArg(args[i], _predicate);
			}
			return new NumberEval((double)num2);
		}

		/// Create an instance of Count to use in {@link Subtotal}
		///              <p>
		///    If there are other subtotals within argument refs (or nested subtotals),
		///    these nested subtotals are ignored to avoid double counting.
		///              </p>
		///
		/// @see Subtotal
		public static Count SubtotalInstance()
		{
			return new Count(subtotalPredicate);
		}
	}
}
