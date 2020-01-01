namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class StringOperationEval : OperationEval, Eval
	{
		public abstract int NumberOfOperands
		{
			get;
		}

		/// Returns an is StringValueEval or ErrorEval or BlankEval
		///
		/// @param eval
		/// @param srcRow
		/// @param srcCol
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
						areaEval.GetValue(areaEval.FirstRow, srcCol);
						return InternalResolveEval(eval);
					}
					return ErrorEval.NAME_INVALID;
				}
				if (areaEval.IsColumn)
				{
					if (areaEval.ContainsRow(srcRow))
					{
						areaEval.GetValue(srcRow, areaEval.FirstColumn);
						return InternalResolveEval(eval);
					}
					return ErrorEval.NAME_INVALID;
				}
				return ErrorEval.NAME_INVALID;
			}
			return InternalResolveEval(eval);
		}

		private ValueEval InternalResolveEval(Eval eval)
		{
			if (eval is StringValueEval)
			{
				return (StringValueEval)eval;
			}
			if (eval is RefEval)
			{
				RefEval refEval = (RefEval)eval;
				ValueEval innerValueEval = refEval.InnerValueEval;
				if (innerValueEval is StringValueEval || innerValueEval is BlankEval)
				{
					return innerValueEval;
				}
				return ErrorEval.NAME_INVALID;
			}
			if (eval is BlankEval)
			{
				return (BlankEval)eval;
			}
			return ErrorEval.NAME_INVALID;
		}

		public abstract Eval Evaluate(Eval[] evals, int srcCellRow, short srcCellCol);
	}
}
