using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of the HLOOKUP() function.<p />
	///
	/// HLOOKUP Finds a column in a lookup table by the first row value and returns the value from another row.<br />
	///
	/// <b>Syntax</b>:<br />
	/// <b>HLOOKUP</b>(<b>lookup_value</b>, <b>table_array</b>, <b>row_index_num</b>, range_lookup)<p />
	///
	/// <b>lookup_value</b>  The value to be found in the first column of the table array.<br />
	/// <b>table_array</b> An area reference for the lookup data. <br />
	/// <b>row_index_num</b> a 1 based index specifying which row value of the lookup data will be returned.<br />
	/// <b>range_lookup</b> If TRUE (default), HLOOKUP Finds the largest value less than or equal to 
	/// the lookup_value.  If FALSE, only exact Matches will be considered<br />   
	///
	/// @author Josh Micich
	public class Hlookup : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			ValueEval rangeLookupArg = null;
			switch (args.Length)
			{
			case 4:
				rangeLookupArg = args[3];
				break;
			default:
				return ErrorEval.VALUE_INVALID;
			case 3:
				break;
			}
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcCellRow, srcCellCol);
				AreaEval tableArray = LookupUtils.ResolveTableArrayArg(args[1]);
				bool isRangeLookup = LookupUtils.ResolveRangeLookupArg(rangeLookupArg, srcCellRow, srcCellCol);
				int index = LookupUtils.LookupIndexOfValue(singleValue, LookupUtils.CreateRowVector(tableArray, 0), isRangeLookup);
				int rowIndex = LookupUtils.ResolveRowOrColIndexArg(args[2], srcCellRow, srcCellCol);
				ValueVector valueVector = CreateResultColumnVector(tableArray, rowIndex);
				return valueVector.GetItem(index);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		/// Returns one column from an <c>AreaEval</c>
		///
		/// @(#VALUE!) if colIndex Is negative, (#REF!) if colIndex Is too high
		private ValueVector CreateResultColumnVector(AreaEval tableArray, int rowIndex)
		{
			if (rowIndex >= tableArray.Height)
			{
				throw EvaluationException.InvalidRef();
			}
			return LookupUtils.CreateRowVector(tableArray, rowIndex);
		}
	}
}
