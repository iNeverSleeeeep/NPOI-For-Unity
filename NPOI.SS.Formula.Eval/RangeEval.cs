using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula.Eval
{
	/// @author Josh Micich 
	public class RangeEval : Fixed2ArgFunction
	{
		public static NPOI.SS.Formula.Functions.Function instance = new RangeEval();

		private RangeEval()
		{
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			try
			{
				AreaEval aeA = EvaluateRef(arg0);
				AreaEval aeB = EvaluateRef(arg1);
				return ResolveRange(aeA, aeB);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		private static AreaEval ResolveRange(AreaEval aeA, AreaEval aeB)
		{
			int firstRow = aeA.FirstRow;
			int firstColumn = aeA.FirstColumn;
			int num = Math.Min(firstRow, aeB.FirstRow);
			int num2 = Math.Max(aeA.LastRow, aeB.LastRow);
			int num3 = Math.Min(firstColumn, aeB.FirstColumn);
			int num4 = Math.Max(aeA.LastColumn, aeB.LastColumn);
			return aeA.Offset(num - firstRow, num2 - firstRow, num3 - firstColumn, num4 - firstColumn);
		}

		private static AreaEval EvaluateRef(ValueEval arg)
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
