using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of Excel function LOOKUP.<p />
	///
	/// LOOKUP Finds an index  row in a lookup table by the first column value and returns the value from another column.
	///
	/// <b>Syntax</b>:<br />
	/// <b>VLOOKUP</b>(<b>lookup_value</b>, <b>lookup_vector</b>, result_vector)<p />
	///
	/// <b>lookup_value</b>  The value to be found in the lookup vector.<br />
	/// <b>lookup_vector</b> An area reference for the lookup data. <br />
	/// <b>result_vector</b> Single row or single column area reference from which the result value Is chosen.<br />
	///
	/// @author Josh Micich
	public class Lookup : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			switch (args.Length)
			{
			case 2:
				throw new Exception("Two arg version of LOOKUP not supported yet");
			default:
				return ErrorEval.VALUE_INVALID;
			case 3:
				try
				{
					ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcCellRow, srcCellCol);
					AreaEval ae = LookupUtils.ResolveTableArrayArg(args[1]);
					AreaEval ae2 = LookupUtils.ResolveTableArrayArg(args[2]);
					ValueVector valueVector = CreateVector(ae);
					ValueVector valueVector2 = CreateVector(ae2);
					if (valueVector.Size > valueVector2.Size)
					{
						throw new Exception("Lookup vector and result vector of differing sizes not supported yet");
					}
					int index = LookupUtils.LookupIndexOfValue(singleValue, valueVector, isRangeLookup: true);
					return valueVector2.GetItem(index);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
		}

		private static ValueVector CreateVector(AreaEval ae)
		{
			ValueVector valueVector = LookupUtils.CreateVector(ae);
			if (valueVector != null)
			{
				return valueVector;
			}
			throw new InvalidOperationException("non-vector lookup or result areas not supported yet");
		}
	}
}
