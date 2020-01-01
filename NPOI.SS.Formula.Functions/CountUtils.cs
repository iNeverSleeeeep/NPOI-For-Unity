using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Common logic for COUNT, COUNTA and COUNTIF
	///
	/// @author Josh Micich 
	internal class CountUtils
	{
		private CountUtils()
		{
		}

		/// @return 1 if the evaluated cell matches the specified criteria
		public static int CountMatchingCell(RefEval refEval, IMatchPredicate criteriaPredicate)
		{
			if (criteriaPredicate.Matches(refEval.InnerValueEval))
			{
				return 1;
			}
			return 0;
		}

		public static int CountArg(ValueEval eval, IMatchPredicate criteriaPredicate)
		{
			if (eval == null)
			{
				throw new ArgumentException("eval must not be null");
			}
			if (eval is TwoDEval)
			{
				return CountMatchingCellsInArea((TwoDEval)eval, criteriaPredicate);
			}
			if (eval is RefEval)
			{
				return CountMatchingCell((RefEval)eval, criteriaPredicate);
			}
			if (!criteriaPredicate.Matches(eval))
			{
				return 0;
			}
			return 1;
		}

		/// @return the number of evaluated cells in the range that match the specified criteria
		public static int CountMatchingCellsInArea(TwoDEval areaEval, IMatchPredicate criteriaPredicate)
		{
			int num = 0;
			int height = areaEval.Height;
			int width = areaEval.Width;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					ValueEval value = areaEval.GetValue(i, j);
					if (criteriaPredicate is I_MatchAreaPredicate)
					{
						I_MatchAreaPredicate i_MatchAreaPredicate = (I_MatchAreaPredicate)criteriaPredicate;
						if (!i_MatchAreaPredicate.Matches(areaEval, i, j))
						{
							continue;
						}
					}
					if (criteriaPredicate.Matches(value))
					{
						num++;
					}
				}
			}
			return num;
		}
	}
}
