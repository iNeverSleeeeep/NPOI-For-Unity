using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the MATCH() Excel function.<p />
	///
	/// <b>Syntax:</b><br />
	/// <b>MATCH</b>(<b>lookup_value</b>, <b>lookup_array</b>, match_type)<p />
	///
	/// Returns a 1-based index specifying at what position in the <b>lookup_array</b> the specified 
	/// <b>lookup_value</b> Is found.<p />
	///
	/// Specific matching behaviour can be modified with the optional <b>match_type</b> parameter.
	///
	///    <table border="0" cellpAdding="1" cellspacing="0" summary="match_type parameter description">
	///      <tr><th>Value</th><th>Matching Behaviour</th></tr>
	///      <tr><td>1</td><td>(default) Find the largest value that Is less than or equal to lookup_value.
	///        The lookup_array must be in ascending <i>order</i>*.</td></tr>
	///      <tr><td>0</td><td>Find the first value that Is exactly equal to lookup_value.
	///        The lookup_array can be in any order.</td></tr>
	///      <tr><td>-1</td><td>Find the smallest value that Is greater than or equal to lookup_value.
	///        The lookup_array must be in descending <i>order</i>*.</td></tr>
	///    </table>
	///
	/// * Note regarding <i>order</i> - For the <b>match_type</b> cases that require the lookup_array to
	///  be ordered, MATCH() can produce incorrect results if this requirement Is not met.  Observed
	///  behaviour in Excel Is to return the lowest index value for which every item after that index
	///  breaks the match rule.<br />
	///  The (ascending) sort order expected by MATCH() Is:<br />
	///  numbers (low to high), strings (A to Z), bool (FALSE to TRUE)<br />
	///  MATCH() ignores all elements in the lookup_array with a different type to the lookup_value. 
	///  Type conversion of the lookup_array elements Is never performed.
	///
	///
	/// @author Josh Micich
	public class Match : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			double num = 1.0;
			switch (args.Length)
			{
			case 3:
				try
				{
					num = EvaluateMatchTypeArg(args[2], srcCellRow, srcCellCol);
				}
				catch (EvaluationException)
				{
					return ErrorEval.REF_INVALID;
				}
				break;
			default:
				return ErrorEval.VALUE_INVALID;
			case 2:
				break;
			}
			bool matchExact = num == 0.0;
			bool findLargestLessThanOrEqual = num > 0.0;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcCellRow, srcCellCol);
				ValueVector lookupRange = EvaluateLookupRange(args[1]);
				int num2 = FindIndexOfValue(singleValue, lookupRange, matchExact, findLargestLessThanOrEqual);
				return new NumberEval((double)(num2 + 1));
			}
			catch (EvaluationException ex2)
			{
				return ex2.GetErrorEval();
			}
		}

		private static ValueVector EvaluateLookupRange(ValueEval eval)
		{
			if (eval is RefEval)
			{
				RefEval refEval = (RefEval)eval;
				return new SingleValueVector(refEval.InnerValueEval);
			}
			if (eval is TwoDEval)
			{
				ValueVector valueVector = LookupUtils.CreateVector((TwoDEval)eval);
				if (valueVector == null)
				{
					throw new EvaluationException(ErrorEval.NA);
				}
				return valueVector;
			}
			if (eval is NumericValueEval)
			{
				throw new EvaluationException(ErrorEval.NA);
			}
			if (eval is StringEval)
			{
				StringEval stringEval = (StringEval)eval;
				double d = OperandResolver.ParseDouble(stringEval.StringValue);
				if (double.IsNaN(d))
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				throw new EvaluationException(ErrorEval.NA);
			}
			throw new Exception("Unexpected eval type (" + eval.GetType().Name + ")");
		}

		private static double EvaluateMatchTypeArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			if (singleValue is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)singleValue);
			}
			if (singleValue is NumericValueEval)
			{
				NumericValueEval numericValueEval = (NumericValueEval)singleValue;
				return numericValueEval.NumberValue;
			}
			if (singleValue is StringEval)
			{
				StringEval stringEval = (StringEval)singleValue;
				double num = OperandResolver.ParseDouble(stringEval.StringValue);
				if (double.IsNaN(num))
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				return num;
			}
			throw new Exception("Unexpected match_type type (" + singleValue.GetType().Name + ")");
		}

		/// @return zero based index
		private static int FindIndexOfValue(ValueEval lookupValue, ValueVector lookupRange, bool matchExact, bool FindLargestLessThanOrEqual)
		{
			LookupValueComparer lookupValueComparer = CreateLookupComparer(lookupValue, matchExact);
			if (matchExact)
			{
				for (int i = 0; i < lookupRange.Size; i++)
				{
					if (lookupValueComparer.CompareTo(lookupRange.GetItem(i)).IsEqual)
					{
						return i;
					}
				}
				throw new EvaluationException(ErrorEval.NA);
			}
			if (FindLargestLessThanOrEqual)
			{
				for (int num = lookupRange.Size - 1; num >= 0; num--)
				{
					CompareResult compareResult = lookupValueComparer.CompareTo(lookupRange.GetItem(num));
					if (!compareResult.IsTypeMismatch && !compareResult.IsLessThan)
					{
						return num;
					}
				}
				throw new EvaluationException(ErrorEval.NA);
			}
			for (int j = 0; j < lookupRange.Size; j++)
			{
				CompareResult compareResult2 = lookupValueComparer.CompareTo(lookupRange.GetItem(j));
				if (compareResult2.IsEqual)
				{
					return j;
				}
				if (compareResult2.IsGreaterThan)
				{
					if (j < 1)
					{
						throw new EvaluationException(ErrorEval.NA);
					}
					return j - 1;
				}
			}
			throw new EvaluationException(ErrorEval.NA);
		}

		private static LookupValueComparer CreateLookupComparer(ValueEval lookupValue, bool matchExact)
		{
			return LookupUtils.CreateLookupComparer(lookupValue, matchExact, isMatchFunction: true);
		}

		private static bool IsLookupValueWild(string stringValue)
		{
			if (stringValue.IndexOf('?') >= 0 || stringValue.IndexOf('*') >= 0)
			{
				return true;
			}
			return false;
		}
	}
}
