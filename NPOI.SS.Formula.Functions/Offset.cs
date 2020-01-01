using NPOI.SS.Formula.Eval;
using System;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for Excel function OFFSet()<p />
	///
	/// OFFSet returns an area reference that Is a specified number of rows and columns from a 
	/// reference cell or area.<p />
	///
	/// <b>Syntax</b>:<br />
	/// <b>OFFSet</b>(<b>reference</b>, <b>rows</b>, <b>cols</b>, height, width)<p />
	/// <b>reference</b> Is the base reference.<br />
	/// <b>rows</b> Is the number of rows up or down from the base reference.<br />
	/// <b>cols</b> Is the number of columns left or right from the base reference.<br />
	/// <b>height</b> (default same height as base reference) Is the row Count for the returned area reference.<br />
	/// <b>width</b> (default same width as base reference) Is the column Count for the returned area reference.<br />
	///
	/// @author Josh Micich
	public class Offset : Function
	{
		/// Exceptions are used within this class to help simplify flow control when error conditions
		/// are enCountered 
		[Serializable]
		private class EvalEx : Exception
		{
			private ErrorEval _error;

			public EvalEx(ErrorEval error)
			{
				_error = error;
			}

			public ErrorEval GetError()
			{
				return _error;
			}
		}

		/// A one dimensional base + offset.  Represents either a row range or a column range.
		/// Two instances of this class toGether specify an area range.
		public class LinearOffsetRange
		{
			private int _offset;

			private int _Length;

			public short FirstIndex => (short)_offset;

			public short LastIndex => (short)(_offset + _Length - 1);

			public LinearOffsetRange(int offset, int length)
			{
				if (length == 0)
				{
					throw new ArgumentException("Length may not be zero");
				}
				_offset = offset;
				_Length = length;
			}

			/// Moves the range by the specified translation amount.<p />
			///
			/// This method also 'normalises' the range: Excel specifies that the width and height 
			/// parameters (Length field here) cannot be negative.  However, OFFSet() does produce
			/// sensible results in these cases.  That behavior Is replicated here. <p />
			///
			/// @param translationAmount may be zero negative or positive
			///
			/// @return the equivalent <c>LinearOffsetRange</c> with a positive Length, moved by the
			/// specified translationAmount.
			public LinearOffsetRange NormaliseAndTranslate(int translationAmount)
			{
				if (_Length > 0)
				{
					if (translationAmount == 0)
					{
						return this;
					}
					return new LinearOffsetRange(translationAmount + _offset, _Length);
				}
				return new LinearOffsetRange(translationAmount + _offset + _Length + 1, -_Length);
			}

			public bool IsOutOfBounds(int lowValidIx, int highValidIx)
			{
				if (_offset < lowValidIx)
				{
					return true;
				}
				if (LastIndex > highValidIx)
				{
					return true;
				}
				return false;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name).Append(" [");
				stringBuilder.Append(_offset).Append("...").Append(LastIndex);
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		/// Encapsulates either an area or cell reference which may be 2d or 3d.
		private class BaseRef
		{
			private const int INVALID_SHEET_INDEX = -1;

			private int _firstRowIndex;

			private int _firstColumnIndex;

			private int _width;

			private int _height;

			private RefEval _refEval;

			private AreaEval _areaEval;

			public int Width => _width;

			public int Height => _height;

			public int FirstRowIndex => _firstRowIndex;

			public int FirstColumnIndex => _firstColumnIndex;

			public BaseRef(RefEval re)
			{
				_refEval = re;
				_areaEval = null;
				_firstRowIndex = re.Row;
				_firstColumnIndex = re.Column;
				_height = 1;
				_width = 1;
			}

			public BaseRef(AreaEval ae)
			{
				_refEval = null;
				_areaEval = ae;
				_firstRowIndex = ae.FirstRow;
				_firstColumnIndex = ae.FirstColumn;
				_height = ae.LastRow - ae.FirstRow + 1;
				_width = ae.LastColumn - ae.FirstColumn + 1;
			}

			public AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx)
			{
				if (_refEval == null)
				{
					return _areaEval.Offset(relFirstRowIx, relLastRowIx, relFirstColIx, relLastColIx);
				}
				return _refEval.Offset(relFirstRowIx, relLastRowIx, relFirstColIx, relLastColIx);
			}
		}

		private const int LAST_VALID_ROW_INDEX = 65535;

		private const int LAST_VALID_COLUMN_INDEX = 255;

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			if (args.Length >= 3 && args.Length <= 5)
			{
				try
				{
					BaseRef baseRef = EvaluateBaseRef(args[0]);
					int offset = EvaluateIntArg(args[1], srcCellRow, srcCellCol);
					int offset2 = EvaluateIntArg(args[2], srcCellRow, srcCellCol);
					int num = baseRef.Height;
					int num2 = baseRef.Width;
					switch (args.Length)
					{
					case 5:
						num2 = EvaluateIntArg(args[4], srcCellRow, srcCellCol);
						break;
					case 4:
						num = EvaluateIntArg(args[3], srcCellRow, srcCellCol);
						break;
					}
					if (num == 0 || num2 == 0)
					{
						return ErrorEval.REF_INVALID;
					}
					LinearOffsetRange orRow = new LinearOffsetRange(offset, num);
					LinearOffsetRange orCol = new LinearOffsetRange(offset2, num2);
					return CreateOffset(baseRef, orRow, orCol);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		private static AreaEval CreateOffset(BaseRef baseRef, LinearOffsetRange orRow, LinearOffsetRange orCol)
		{
			LinearOffsetRange linearOffsetRange = orRow.NormaliseAndTranslate(baseRef.FirstRowIndex);
			LinearOffsetRange linearOffsetRange2 = orCol.NormaliseAndTranslate(baseRef.FirstColumnIndex);
			if (linearOffsetRange.IsOutOfBounds(0, 65535))
			{
				throw new EvaluationException(ErrorEval.REF_INVALID);
			}
			if (linearOffsetRange2.IsOutOfBounds(0, 255))
			{
				throw new EvaluationException(ErrorEval.REF_INVALID);
			}
			return baseRef.Offset(orRow.FirstIndex, orRow.LastIndex, orCol.FirstIndex, orCol.LastIndex);
		}

		private static BaseRef EvaluateBaseRef(ValueEval eval)
		{
			if (eval is RefEval)
			{
				return new BaseRef((RefEval)eval);
			}
			if (eval is AreaEval)
			{
				return new BaseRef((AreaEval)eval);
			}
			if (eval is ErrorEval)
			{
				throw new EvalEx((ErrorEval)eval);
			}
			throw new EvalEx(ErrorEval.VALUE_INVALID);
		}

		/// OFFSet's numeric arguments (2..5) have similar Processing rules
		public static int EvaluateIntArg(ValueEval eval, int srcCellRow, int srcCellCol)
		{
			double d = EvaluateDoubleArg(eval, srcCellRow, srcCellCol);
			return ConvertDoubleToInt(d);
		}

		/// Fractional values are silently truncated by Excel.
		/// Truncation Is toward negative infinity.
		public static int ConvertDoubleToInt(double d)
		{
			return (int)Math.Floor(d);
		}

		private static double EvaluateDoubleArg(ValueEval eval, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(eval, srcCellRow, srcCellCol);
			if (singleValue is NumericValueEval)
			{
				return ((NumericValueEval)singleValue).NumberValue;
			}
			if (singleValue is StringEval)
			{
				StringEval stringEval = (StringEval)singleValue;
				double num = OperandResolver.ParseDouble(stringEval.StringValue);
				if (double.IsNaN(num))
				{
					throw new EvalEx(ErrorEval.VALUE_INVALID);
				}
				return num;
			}
			if (singleValue is BoolEval)
			{
				if (((BoolEval)singleValue).BooleanValue)
				{
					return 1.0;
				}
				return 0.0;
			}
			throw new Exception("Unexpected eval type (" + singleValue.GetType().Name + ")");
		}
	}
}
