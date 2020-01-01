using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula.Eval
{
	/// @author Josh Micich
	public class IntersectionEval : Fixed2ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function instance = new IntersectionEval();

		private IntersectionEval()
		{
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			try
			{
				AreaEval aeA = EvaluateRef(arg0);
				AreaEval aeB = EvaluateRef(arg1);
				AreaEval areaEval = ResolveRange(aeA, aeB);
				if (areaEval == null)
				{
					return ErrorEval.NULL_INTERSECTION;
				}
				return areaEval;
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		/// @return simple rectangular {@link AreaEval} which represents the intersection of areas
		/// <c>aeA</c> and <c>aeB</c>. If the two areas do not intersect, the result is <code>null</code>.
		private static AreaEval ResolveRange(AreaEval aeA, AreaEval aeB)
		{
			int firstRow = aeA.FirstRow;
			int firstColumn = aeA.FirstColumn;
			int lastColumn = aeB.LastColumn;
			if (firstColumn > lastColumn)
			{
				return null;
			}
			int firstColumn2 = aeB.FirstColumn;
			if (firstColumn2 > aeA.LastColumn)
			{
				return null;
			}
			int lastRow = aeB.LastRow;
			if (firstRow > lastRow)
			{
				return null;
			}
			int firstRow2 = aeB.FirstRow;
			int lastRow2 = aeA.LastRow;
			if (firstRow2 > lastRow2)
			{
				return null;
			}
			int num = Math.Max(firstRow, firstRow2);
			int num2 = Math.Min(lastRow2, lastRow);
			int num3 = Math.Max(firstColumn, firstColumn2);
			int num4 = Math.Min(aeA.LastColumn, lastColumn);
			return aeA.Offset(num - firstRow, num2 - firstRow, num3 - firstColumn, num4 - firstColumn);
		}

		private AreaEval EvaluateRef(ValueEval arg)
		{
			if (arg is AreaEval)
			{
				return (AreaEval)arg;
			}
			if (arg is RefEval)
			{
				return ((RefEval)arg).Offset(0, 0, 0, 0);
			}
			if (arg is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)arg);
			}
			throw new ArgumentException("Unexpected ref arg class (" + arg.GetType().Name + ")");
		}
	}
}
