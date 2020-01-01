using NPOI.SS.Formula.Eval;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NPOI.SS.Formula.Functions
{
	/// Common functionality used by VLOOKUP, HLOOKUP, LOOKUP and MATCH
	///
	/// @author Josh Micich
	internal class LookupUtils
	{
		internal class RowVector : ValueVector
		{
			private AreaEval _tableArray;

			private int _size;

			private int _rowIndex;

			public int Size => _size;

			public RowVector(AreaEval tableArray, int rowIndex)
			{
				_rowIndex = rowIndex;
				int row = tableArray.FirstRow + rowIndex;
				if (!tableArray.ContainsRow(row))
				{
					int num = tableArray.LastRow - tableArray.FirstRow;
					throw new ArgumentException("Specified row index (" + rowIndex + ") is outside the allowed range (0.." + num + ")");
				}
				_tableArray = tableArray;
				_size = tableArray.Width;
			}

			public ValueEval GetItem(int index)
			{
				if (index > _size)
				{
					throw new IndexOutOfRangeException("Specified index (" + index + ") is outside the allowed range (0.." + (_size - 1) + ")");
				}
				return _tableArray.GetRelativeValue(_rowIndex, index);
			}
		}

		internal class ColumnVector : ValueVector
		{
			private AreaEval _tableArray;

			private int _size;

			private int _columnIndex;

			public int Size => _size;

			public ColumnVector(AreaEval tableArray, int columnIndex)
			{
				_columnIndex = columnIndex;
				int num = tableArray.FirstColumn + columnIndex;
				if (!tableArray.ContainsColumn((short)num))
				{
					int num2 = tableArray.LastColumn - tableArray.FirstColumn;
					throw new ArgumentException("Specified column index (" + columnIndex + ") is outside the allowed range (0.." + num2 + ")");
				}
				_tableArray = tableArray;
				_size = _tableArray.Height;
			}

			public ValueEval GetItem(int index)
			{
				if (index > _size)
				{
					throw new IndexOutOfRangeException("Specified index (" + index + ") is outside the allowed range (0.." + (_size - 1) + ")");
				}
				return _tableArray.GetRelativeValue(index, _columnIndex);
			}
		}

		private class StringLookupComparer : LookupValueComparerBase
		{
			private string _value;

			private Regex _wildCardPattern;

			private bool _matchExact;

			private bool _isMatchFunction;

			public StringLookupComparer(StringEval se, bool matchExact, bool isMatchFunction)
				: base(se)
			{
				_value = se.StringValue;
				_wildCardPattern = Countif.StringMatcher.GetWildCardPattern(_value);
				_matchExact = matchExact;
				_isMatchFunction = isMatchFunction;
			}

			protected override CompareResult CompareSameType(ValueEval other)
			{
				StringEval stringEval = (StringEval)other;
				string stringValue = stringEval.StringValue;
				if (_wildCardPattern != null)
				{
					MatchCollection matchCollection = _wildCardPattern.Matches(stringValue);
					bool matches = matchCollection.Count > 0;
					if (_isMatchFunction || !_matchExact)
					{
						return CompareResult.ValueOf(matches);
					}
				}
				return CompareResult.ValueOf(string.Compare(_value, stringValue, ignoreCase: true));
			}

			protected override string GetValueAsString()
			{
				return _value;
			}
		}

		private class NumberLookupComparer : LookupValueComparerBase
		{
			private double _value;

			public NumberLookupComparer(NumberEval ne)
				: base(ne)
			{
				_value = ne.NumberValue;
			}

			protected override CompareResult CompareSameType(ValueEval other)
			{
				NumberEval numberEval = (NumberEval)other;
				return CompareResult.ValueOf(_value.CompareTo(numberEval.NumberValue));
			}

			protected override string GetValueAsString()
			{
				return _value.ToString(CultureInfo.InvariantCulture);
			}
		}

		public static ValueVector CreateRowVector(TwoDEval tableArray, int relativeRowIndex)
		{
			return new RowVector((AreaEval)tableArray, relativeRowIndex);
		}

		public static ValueVector CreateColumnVector(TwoDEval tableArray, int relativeColumnIndex)
		{
			return new ColumnVector((AreaEval)tableArray, relativeColumnIndex);
		}

		/// @return <c>null</c> if the supplied area is neither a single row nor a single colum
		public static ValueVector CreateVector(TwoDEval ae)
		{
			if (ae.IsColumn)
			{
				return CreateColumnVector(ae, 0);
			}
			if (ae.IsRow)
			{
				return CreateRowVector(ae, 0);
			}
			return null;
		}

		/// Processes the third argument to VLOOKUP, or HLOOKUP (<b>col_index_num</b> 
		/// or <b>row_index_num</b> respectively).<br />
		/// Sample behaviour:
		///    <table border="0" cellpAdding="1" cellspacing="2" summary="Sample behaviour">
		///      <tr><th>Input Return</th><th>Value </th><th>Thrown Error</th></tr>
		///      <tr><td>5</td><td>4</td><td> </td></tr>
		///      <tr><td>2.9</td><td>2</td><td> </td></tr>
		///      <tr><td>"5"</td><td>4</td><td> </td></tr>
		///      <tr><td>"2.18e1"</td><td>21</td><td> </td></tr>
		///      <tr><td>"-$2"</td><td>-3</td><td>*</td></tr>
		///      <tr><td>FALSE</td><td>-1</td><td>*</td></tr>
		///      <tr><td>TRUE</td><td>0</td><td> </td></tr>
		///      <tr><td>"TRUE"</td><td> </td><td>#REF!</td></tr>
		///      <tr><td>"abc"</td><td> </td><td>#REF!</td></tr>
		///      <tr><td>""</td><td> </td><td>#REF!</td></tr>
		///      <tr><td>&lt;blank&gt;</td><td> </td><td>#VALUE!</td></tr>
		///    </table><br />
		///
		///  * Note - out of range errors (both too high and too low) are handled by the caller. 
		/// @return column or row index as a zero-based value
		public static int ResolveRowOrColIndexArg(ValueEval rowColIndexArg, int srcCellRow, int srcCellCol)
		{
			if (rowColIndexArg == null)
			{
				throw new ArgumentException("argument must not be null");
			}
			ValueEval singleValue;
			try
			{
				singleValue = OperandResolver.GetSingleValue(rowColIndexArg, srcCellRow, (short)srcCellCol);
			}
			catch (EvaluationException)
			{
				throw EvaluationException.InvalidRef();
			}
			if (singleValue is StringEval)
			{
				StringEval stringEval = (StringEval)singleValue;
				string stringValue = stringEval.StringValue;
				double d = OperandResolver.ParseDouble(stringValue);
				if (double.IsNaN(d))
				{
					throw EvaluationException.InvalidRef();
				}
			}
			int num = OperandResolver.CoerceValueToInt(singleValue);
			if (num < 1)
			{
				throw EvaluationException.InvalidValue();
			}
			return num - 1;
		}

		/// The second argument (table_array) should be an area ref, but can actually be a cell ref, in
		/// which case it Is interpreted as a 1x1 area ref.  Other scalar values cause #VALUE! error.
		public static AreaEval ResolveTableArrayArg(ValueEval eval)
		{
			if (eval is AreaEval)
			{
				return (AreaEval)eval;
			}
			if (eval is RefEval)
			{
				RefEval refEval = (RefEval)eval;
				return refEval.Offset(0, 0, 0, 0);
			}
			throw EvaluationException.InvalidValue();
		}

		/// Resolves the last (optional) parameter (<b>range_lookup</b>) to the VLOOKUP and HLOOKUP functions. 
		/// @param rangeLookupArg
		/// @param srcCellRow
		/// @param srcCellCol
		/// @return
		/// @throws EvaluationException
		public static bool ResolveRangeLookupArg(ValueEval rangeLookupArg, int srcCellRow, int srcCellCol)
		{
			if (rangeLookupArg == null)
			{
				return true;
			}
			ValueEval singleValue = OperandResolver.GetSingleValue(rangeLookupArg, srcCellRow, srcCellCol);
			if (singleValue is BlankEval)
			{
				return false;
			}
			if (singleValue is BoolEval)
			{
				BoolEval boolEval = (BoolEval)singleValue;
				return boolEval.BooleanValue;
			}
			if (singleValue is StringEval)
			{
				string stringValue = ((StringEval)singleValue).StringValue;
				if (stringValue.Length < 1)
				{
					throw EvaluationException.InvalidValue();
				}
				bool? flag = Countif.ParseBoolean(stringValue);
				if (flag.HasValue)
				{
					if (flag != true)
					{
						return false;
					}
					return true;
				}
				throw EvaluationException.InvalidValue();
			}
			if (singleValue is NumericValueEval)
			{
				NumericValueEval numericValueEval = (NumericValueEval)singleValue;
				return 0.0 != numericValueEval.NumberValue;
			}
			throw new Exception("Unexpected eval type (" + singleValue.GetType().Name + ")");
		}

		public static int LookupIndexOfValue(ValueEval lookupValue, ValueVector vector, bool isRangeLookup)
		{
			LookupValueComparer lookupComparer = CreateLookupComparer(lookupValue, isRangeLookup, isMatchFunction: false);
			int num = (!isRangeLookup) ? LookupIndexOfExactValue(lookupComparer, vector) : PerformBinarySearch(vector, lookupComparer);
			if (num < 0)
			{
				throw new EvaluationException(ErrorEval.NA);
			}
			return num;
		}

		/// Finds first (lowest index) exact occurrence of specified value.
		/// @param lookupValue the value to be found in column or row vector
		/// @param vector the values to be searched. For VLOOKUP this Is the first column of the 
		/// 	tableArray. For HLOOKUP this Is the first row of the tableArray. 
		/// @return zero based index into the vector, -1 if value cannot be found
		private static int LookupIndexOfExactValue(LookupValueComparer lookupComparer, ValueVector vector)
		{
			int size = vector.Size;
			for (int i = 0; i < size; i++)
			{
				if (lookupComparer.CompareTo(vector.GetItem(i)).IsEqual)
				{
					return i;
				}
			}
			return -1;
		}

		/// Excel has funny behaviour when the some elements in the search vector are the wrong type.
		private static int PerformBinarySearch(ValueVector vector, LookupValueComparer lookupComparer)
		{
			BinarySearchIndexes binarySearchIndexes = new BinarySearchIndexes(vector.Size);
			int num;
			while (true)
			{
				num = binarySearchIndexes.GetMidIx();
				if (num < 0)
				{
					return binarySearchIndexes.GetLowIx();
				}
				CompareResult compareResult = lookupComparer.CompareTo(vector.GetItem(num));
				if (compareResult.IsTypeMismatch)
				{
					int num2 = HandleMidValueTypeMismatch(lookupComparer, vector, binarySearchIndexes, num);
					if (num2 < 0)
					{
						continue;
					}
					num = num2;
					compareResult = lookupComparer.CompareTo(vector.GetItem(num));
				}
				if (compareResult.IsEqual)
				{
					break;
				}
				binarySearchIndexes.NarrowSearch(num, compareResult.IsLessThan);
			}
			return FindLastIndexInRunOfEqualValues(lookupComparer, vector, num, binarySearchIndexes.GetHighIx());
		}

		/// Excel seems to handle mismatched types initially by just stepping 'mid' ix forward to the 
		/// first compatible value.
		/// @param midIx 'mid' index (value which has the wrong type)
		/// @return usually -1, signifying that the BinarySearchIndex has been narrowed to the new mid 
		/// index.  Zero or greater signifies that an exact match for the lookup value was found
		private static int HandleMidValueTypeMismatch(LookupValueComparer lookupComparer, ValueVector vector, BinarySearchIndexes bsi, int midIx)
		{
			int num = midIx;
			int highIx = bsi.GetHighIx();
			CompareResult compareResult;
			do
			{
				num++;
				if (num == highIx)
				{
					bsi.NarrowSearch(midIx, isLessThan: true);
					return -1;
				}
				compareResult = lookupComparer.CompareTo(vector.GetItem(num));
				if (compareResult.IsLessThan && num == highIx - 1)
				{
					bsi.NarrowSearch(midIx, isLessThan: true);
					return -1;
				}
			}
			while (compareResult.IsTypeMismatch);
			if (compareResult.IsEqual)
			{
				return num;
			}
			bsi.NarrowSearch(num, compareResult.IsLessThan);
			return -1;
		}

		/// Once the binary search has found a single match, (V/H)LOOKUP steps one by one over subsequent
		/// values to choose the last matching item.
		private static int FindLastIndexInRunOfEqualValues(LookupValueComparer lookupComparer, ValueVector vector, int firstFoundIndex, int maxIx)
		{
			for (int i = firstFoundIndex + 1; i < maxIx; i++)
			{
				if (!lookupComparer.CompareTo(vector.GetItem(i)).IsEqual)
				{
					return i - 1;
				}
			}
			return maxIx - 1;
		}

		public static LookupValueComparer CreateLookupComparer(ValueEval lookupValue, bool matchExact, bool isMatchFunction)
		{
			if (lookupValue == BlankEval.instance)
			{
				return new NumberLookupComparer(NumberEval.ZERO);
			}
			if (lookupValue is StringEval)
			{
				return new StringLookupComparer((StringEval)lookupValue, matchExact, isMatchFunction);
			}
			if (lookupValue is NumberEval)
			{
				return new NumberLookupComparer((NumberEval)lookupValue);
			}
			if (lookupValue is BoolEval)
			{
				return new BooleanLookupComparer((BoolEval)lookupValue);
			}
			throw new ArgumentException("Bad lookup value type (" + lookupValue.GetType().Name + ")");
		}
	}
}
