using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the function COUNTBLANK
	/// <p>
	///  Syntax: COUNTBLANK ( range )
	///    <table border="0" cellpadding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><th>range </th><td>is the range of cells to count blanks</td></tr>
	///    </table>
	/// </p>
	///
	/// @author Mads Mohr Christensen
	public class Countblank : Fixed1ArgFunction
	{
		private class BlankPredicate : IMatchPredicate
		{
			public bool Matches(ValueEval valueEval)
			{
				return valueEval == BlankEval.instance;
			}
		}

		private static IMatchPredicate predicate = new BlankPredicate();

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			double value;
			if (arg0 is RefEval)
			{
				value = (double)CountUtils.CountMatchingCell((RefEval)arg0, predicate);
			}
			else
			{
				if (!(arg0 is TwoDEval))
				{
					throw new ArgumentException("Bad range arg type (" + arg0.GetType().Name + ")");
				}
				value = (double)CountUtils.CountMatchingCellsInArea((TwoDEval)arg0, predicate);
			}
			return new NumberEval(value);
		}
	}
}
