using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of the VLOOKUP() function.<p />
	///
	/// VLOOKUP Finds a row in a lookup table by the first column value and returns the value from another column.<br />
	///
	/// <b>Syntax</b>:<br />
	/// <b>VLOOKUP</b>(<b>lookup_value</b>, <b>table_array</b>, <b>col_index_num</b>, range_lookup)<p />
	///
	/// <b>lookup_value</b>  The value to be found in the first column of the table array.<br />
	/// <b>table_array</b> An area reference for the lookup data. <br />
	/// <b>col_index_num</b> a 1 based index specifying which column value of the lookup data will be returned.<br />
	/// <b>range_lookup</b> If TRUE (default), VLOOKUP Finds the largest value less than or equal to 
	/// the lookup_value.  If FALSE, only exact Matches will be considered<br />   
	///
	/// @author Josh Micich
	public class Vlookup : Var3or4ArgFunction
	{
		private static ValueEval DEFAULT_ARG3 = BoolEval.TRUE;

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			return Evaluate(srcRowIndex, srcColumnIndex, arg0, arg1, arg2, DEFAULT_ARG3);
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval lookup_value, ValueEval table_array, ValueEval col_index, ValueEval range_lookup)
		{
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(lookup_value, srcRowIndex, srcColumnIndex);
				TwoDEval tableArray = LookupUtils.ResolveTableArrayArg(table_array);
				bool isRangeLookup = LookupUtils.ResolveRangeLookupArg(range_lookup, srcRowIndex, srcColumnIndex);
				int index = LookupUtils.LookupIndexOfValue(singleValue, LookupUtils.CreateColumnVector(tableArray, 0), isRangeLookup);
				int colIndex = LookupUtils.ResolveRowOrColIndexArg(col_index, srcRowIndex, srcColumnIndex);
				ValueVector valueVector = CreateResultColumnVector(tableArray, colIndex);
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
		private ValueVector CreateResultColumnVector(TwoDEval tableArray, int colIndex)
		{
			if (colIndex >= tableArray.Width)
			{
				throw EvaluationException.InvalidRef();
			}
			return LookupUtils.CreateColumnVector(tableArray, colIndex);
		}
	}
}
