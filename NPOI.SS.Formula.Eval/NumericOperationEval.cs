namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class NumericOperationEval : OperationEval, Eval
	{
		protected abstract ValueEvalToNumericXlator Xlator
		{
			get;
		}

		public abstract int NumberOfOperands
		{
			get;
		}

		public abstract int Type
		{
			get;
		}

		protected ValueEval SingleOperandEvaluate(Eval eval, int srcRow, short srcCol)
		{
			if (eval is AreaEval)
			{
				AreaEval areaEval = (AreaEval)eval;
				if (areaEval.Contains(srcRow, srcCol))
				{
					return ErrorEval.CIRCULAR_REF_ERROR;
				}
				if (areaEval.IsRow)
				{
					if (areaEval.ContainsColumn(srcCol))
					{
						ValueEval value = areaEval.GetValue(areaEval.FirstRow, srcCol);
						value = Xlator.AttemptXlateToNumeric(value);
						return Xlator.AttemptXlateToNumeric(value);
					}
					return ErrorEval.VALUE_INVALID;
				}
				if (areaEval.IsColumn)
				{
					if (areaEval.ContainsRow(srcRow))
					{
						ValueEval value2 = areaEval.GetValue(srcRow, areaEval.FirstColumn);
						return Xlator.AttemptXlateToNumeric(value2);
					}
					return ErrorEval.VALUE_INVALID;
				}
				return ErrorEval.VALUE_INVALID;
			}
			return Xlator.AttemptXlateToNumeric((ValueEval)eval);
		}

		public abstract Eval Evaluate(Eval[] evals, int srcCellRow, short srcCellCol);
	}
}
