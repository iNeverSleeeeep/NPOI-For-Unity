using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Counts the number of cells that contain data within the list of arguments. 
	///
	/// Excel Syntax
	/// COUNTA(value1,value2,...)
	/// Value1, value2, ...   are 1 to 30 arguments representing the values or ranges to be Counted.
	///
	/// @author Josh Micich
	public class Counta : Function
	{
		public class DefaultPredicate : IMatchPredicate
		{
			public bool Matches(ValueEval valueEval)
			{
				if (valueEval == BlankEval.instance)
				{
					return false;
				}
				return true;
			}
		}

		public class SubtotalPredicate : I_MatchAreaPredicate, IMatchPredicate
		{
			public bool Matches(ValueEval valueEval)
			{
				return defaultPredicate.Matches(valueEval);
			}

			/// don't count cells that are subtotals
			public bool Matches(TwoDEval areEval, int rowIndex, int columnIndex)
			{
				return !areEval.IsSubTotal(rowIndex, columnIndex);
			}
		}

		private IMatchPredicate _predicate;

		private static IMatchPredicate defaultPredicate = new DefaultPredicate();

		private static IMatchPredicate subtotalPredicate = new SubtotalPredicate();

		public Counta()
		{
			_predicate = defaultPredicate;
		}

		private Counta(IMatchPredicate criteriaPredicate)
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

		public static Counta SubtotalInstance()
		{
			return new Counta(subtotalPredicate);
		}
	}
}
