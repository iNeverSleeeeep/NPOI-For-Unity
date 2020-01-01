using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the function COUNTIF<p />
	///
	/// Syntax: COUNTIF ( range, criteria )
	///    <table border="0" cellpAdding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><th>range </th><td>is the range of cells to be Counted based on the criteria</td></tr>
	///      <tr><th>criteria</th><td>is used to determine which cells to Count</td></tr>
	///    </table>
	/// <p />
	///
	/// @author Josh Micich
	public class Countif : Fixed2ArgFunction
	{
		internal class CmpOp
		{
			public const int NONE = 0;

			public const int EQ = 1;

			public const int NE = 2;

			public const int LE = 3;

			public const int LT = 4;

			public const int GT = 5;

			public const int GE = 6;

			public static readonly CmpOp OP_NONE = op("", 0);

			public static readonly CmpOp OP_EQ = op("=", 1);

			public static readonly CmpOp OP_NE = op("<>", 2);

			public static readonly CmpOp OP_LE = op("<=", 3);

			public static readonly CmpOp OP_LT = op("<", 4);

			public static readonly CmpOp OP_GT = op(">", 5);

			public static readonly CmpOp OP_GE = op(">=", 6);

			private string _representation;

			private int _code;

			/// @return number of characters used to represent this operator
			public int Length => _representation.Length;

			public int Code => _code;

			public string Representation => _representation;

			private static CmpOp op(string rep, int code)
			{
				return new CmpOp(rep, code);
			}

			private CmpOp(string representation, int code)
			{
				_representation = representation;
				_code = code;
			}

			public static CmpOp GetOperator(string value)
			{
				int length = value.Length;
				if (length >= 1)
				{
					switch (value[0])
					{
					case '=':
						return OP_EQ;
					case '>':
						if (length > 1)
						{
							char c = value[1];
							if (c == '=')
							{
								return OP_GE;
							}
						}
						return OP_GT;
					case '<':
						if (length > 1)
						{
							switch (value[1])
							{
							case '=':
								return OP_LE;
							case '>':
								return OP_NE;
							}
						}
						return OP_LT;
					default:
						return OP_NONE;
					}
				}
				return OP_NONE;
			}

			public bool Evaluate(bool cmpResult)
			{
				switch (_code)
				{
				case 0:
				case 1:
					return cmpResult;
				case 2:
					return !cmpResult;
				default:
					throw new Exception("Cannot call bool Evaluate on non-equality operator '" + _representation + "'");
				}
			}

			public bool Evaluate(int cmpResult)
			{
				switch (_code)
				{
				case 0:
				case 1:
					return cmpResult == 0;
				case 2:
					return cmpResult != 0;
				case 4:
					return cmpResult < 0;
				case 3:
					return cmpResult <= 0;
				case 5:
					return cmpResult > 0;
				case 6:
					return cmpResult >= 0;
				default:
					throw new Exception("Cannot call bool Evaluate on non-equality operator '" + _representation + "'");
				}
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name);
				stringBuilder.Append(" [").Append(_representation).Append("]");
				return stringBuilder.ToString();
			}
		}

		internal abstract class MatcherBase : IMatchPredicate
		{
			private CmpOp _operator;

			protected int Code => _operator.Code;

			protected abstract string ValueText
			{
				get;
			}

			protected MatcherBase(CmpOp operator1)
			{
				_operator = operator1;
			}

			protected bool Evaluate(int cmpResult)
			{
				return _operator.Evaluate(cmpResult);
			}

			protected bool Evaluate(bool cmpResult)
			{
				return _operator.Evaluate(cmpResult);
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name).Append(" [");
				stringBuilder.Append(_operator.Representation);
				stringBuilder.Append(ValueText);
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			public abstract bool Matches(ValueEval x);
		}

		private class ErrorMatcher : MatcherBase
		{
			private int _value;

			protected override string ValueText => ErrorConstants.GetText(_value);

			public ErrorMatcher(int errorCode, CmpOp operator1)
				: base(operator1)
			{
				_value = errorCode;
			}

			public override bool Matches(ValueEval x)
			{
				if (x is ErrorEval)
				{
					int errorCode = ((ErrorEval)x).ErrorCode;
					return Evaluate(errorCode - _value);
				}
				return false;
			}
		}

		private class NumberMatcher : MatcherBase
		{
			private double _value;

			protected override string ValueText => _value.ToString(CultureInfo.InvariantCulture);

			public NumberMatcher(double value, CmpOp optr)
				: base(optr)
			{
				_value = value;
			}

			public override bool Matches(ValueEval x)
			{
				if (x is StringEval)
				{
					switch (base.Code)
					{
					case 2:
						return true;
					default:
						return false;
					case 0:
					case 1:
					{
						StringEval stringEval = (StringEval)x;
						double num = OperandResolver.ParseDouble(stringEval.StringValue);
						if (double.IsNaN(num))
						{
							return false;
						}
						return _value == num;
					}
					}
				}
				if (!(x is NumberEval))
				{
					if (x is BlankEval)
					{
						int code = base.Code;
						if (code == 2)
						{
							return true;
						}
						return false;
					}
					return false;
				}
				NumberEval numberEval = (NumberEval)x;
				return Evaluate(numberEval.NumberValue.CompareTo(_value));
			}
		}

		private class BooleanMatcher : MatcherBase
		{
			private int _value;

			protected override string ValueText
			{
				get
				{
					if (_value != 1)
					{
						return "FALSE";
					}
					return "TRUE";
				}
			}

			public BooleanMatcher(bool value, CmpOp optr)
				: base(optr)
			{
				_value = BoolToInt(value);
			}

			private static int BoolToInt(bool value)
			{
				if (!value)
				{
					return 0;
				}
				return 1;
			}

			public override bool Matches(ValueEval x)
			{
				if (x is StringEval)
				{
					return false;
				}
				if (!(x is BoolEval))
				{
					if (x is BlankEval)
					{
						int code = base.Code;
						if (code == 2)
						{
							return true;
						}
						return false;
					}
					if (x is NumberEval)
					{
						int code2 = base.Code;
						if (code2 == 2)
						{
							return true;
						}
						return false;
					}
					return false;
				}
				BoolEval boolEval = (BoolEval)x;
				int num = BoolToInt(boolEval.BooleanValue);
				return Evaluate(num - _value);
			}
		}

		internal class StringMatcher : MatcherBase
		{
			private string _value;

			private CmpOp _operator;

			private Regex _pattern;

			protected override string ValueText
			{
				get
				{
					if (_pattern == null)
					{
						return _value;
					}
					return _pattern.ToString();
				}
			}

			public StringMatcher(string value, CmpOp optr)
				: base(optr)
			{
				_value = value;
				_operator = optr;
				switch (optr.Code)
				{
				case 0:
				case 1:
				case 2:
					_pattern = GetWildCardPattern(value);
					break;
				default:
					_pattern = null;
					break;
				}
			}

			public override bool Matches(ValueEval x)
			{
				if (x is BlankEval)
				{
					switch (_operator.Code)
					{
					case 0:
					case 1:
						return _value.Length == 0;
					case 2:
						return _value.Length != 0;
					default:
						return false;
					}
				}
				if (!(x is StringEval))
				{
					return false;
				}
				string stringValue = ((StringEval)x).StringValue;
				if (stringValue.Length < 1 && _value.Length < 1)
				{
					switch (_operator.Code)
					{
					case 0:
						return true;
					case 1:
						return false;
					case 2:
						return true;
					default:
						return false;
					}
				}
				if (_pattern != null)
				{
					return Evaluate(_pattern.IsMatch(stringValue));
				}
				return Evaluate(string.Compare(stringValue, _value, StringComparison.CurrentCultureIgnoreCase));
			}

			/// <summary>
			/// Translates Excel countif wildcard strings into .NET regex strings
			/// </summary>
			/// <param name="value">Excel wildcard expression</param>
			/// <returns>return null if the specified value contains no special wildcard characters.</returns>
			internal static Regex GetWildCardPattern(string value)
			{
				int length = value.Length;
				StringBuilder stringBuilder = new StringBuilder(length);
				stringBuilder.Append("^");
				bool flag = false;
				for (int i = 0; i < length; i++)
				{
					char c = value[i];
					switch (c)
					{
					case '?':
						flag = true;
						stringBuilder.Append('.');
						break;
					case '*':
						flag = true;
						stringBuilder.Append(".*");
						break;
					case '~':
						if (i + 1 < length)
						{
							c = value[i + 1];
							char c2 = c;
							if (c2 == '*' || c2 == '?')
							{
								flag = true;
								stringBuilder.Append('[').Append(c).Append(']');
								i++;
								break;
							}
						}
						stringBuilder.Append('~');
						break;
					case '$':
					case '(':
					case ')':
					case '.':
					case '[':
					case ']':
					case '^':
						stringBuilder.Append("\\").Append(c);
						break;
					default:
						stringBuilder.Append(c);
						break;
					}
				}
				stringBuilder.Append("$");
				if (flag)
				{
					return new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase);
				}
				return null;
			}
		}

		/// @return the number of evaluated cells in the range that match the specified criteria
		private double CountMatchingCellsInArea(ValueEval rangeArg, IMatchPredicate criteriaPredicate)
		{
			if (rangeArg is RefEval)
			{
				return (double)CountUtils.CountMatchingCell((RefEval)rangeArg, criteriaPredicate);
			}
			if (rangeArg is TwoDEval)
			{
				return (double)CountUtils.CountMatchingCellsInArea((TwoDEval)rangeArg, criteriaPredicate);
			}
			throw new ArgumentException("Bad range arg type (" + rangeArg.GetType().Name + ")");
		}

		/// @return the de-referenced criteria arg (possibly {@link ErrorEval})
		private static ValueEval EvaluateCriteriaArg(ValueEval arg, int srcRowIndex, int srcColumnIndex)
		{
			try
			{
				return OperandResolver.GetSingleValue(arg, srcRowIndex, (short)srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		/// When the second argument is a string, many things are possible
		private static IMatchPredicate CreateGeneralMatchPredicate(StringEval stringEval)
		{
			string stringValue = stringEval.StringValue;
			CmpOp @operator = CmpOp.GetOperator(stringValue);
			stringValue = stringValue.Substring(@operator.Length);
			bool? flag = ParseBoolean(stringValue);
			if (flag.HasValue)
			{
				return new BooleanMatcher(flag.Value, @operator);
			}
			double num = OperandResolver.ParseDouble(stringValue);
			if (!double.IsNaN(num))
			{
				return new NumberMatcher(num, @operator);
			}
			ErrorEval errorEval = ParseError(stringValue);
			if (errorEval != null)
			{
				return new ErrorMatcher(errorEval.ErrorCode, @operator);
			}
			return new StringMatcher(stringValue, @operator);
		}

		/// Creates a criteria predicate object for the supplied criteria arg
		/// @return <code>null</code> if the arg evaluates to blank.
		public static IMatchPredicate CreateCriteriaPredicate(ValueEval arg, int srcRowIndex, int srcColumnIndex)
		{
			ValueEval valueEval = EvaluateCriteriaArg(arg, srcRowIndex, srcColumnIndex);
			if (valueEval is NumberEval)
			{
				return new NumberMatcher(((NumberEval)valueEval).NumberValue, CmpOp.OP_NONE);
			}
			if (valueEval is BoolEval)
			{
				return new BooleanMatcher(((BoolEval)valueEval).BooleanValue, CmpOp.OP_NONE);
			}
			if (valueEval is StringEval)
			{
				return CreateGeneralMatchPredicate((StringEval)valueEval);
			}
			if (valueEval is ErrorEval)
			{
				return new ErrorMatcher(((ErrorEval)valueEval).ErrorCode, CmpOp.OP_NONE);
			}
			if (valueEval == BlankEval.instance)
			{
				return null;
			}
			throw new Exception("Unexpected type for criteria (" + valueEval.GetType().Name + ")");
		}

		private static ErrorEval ParseError(string value)
		{
			if (value.Length < 4 || value[0] != '#')
			{
				return null;
			}
			if (value.Equals("#NULL!"))
			{
				return ErrorEval.NULL_INTERSECTION;
			}
			if (value.Equals("#DIV/0!"))
			{
				return ErrorEval.DIV_ZERO;
			}
			if (value.Equals("#VALUE!"))
			{
				return ErrorEval.VALUE_INVALID;
			}
			if (value.Equals("#REF!"))
			{
				return ErrorEval.REF_INVALID;
			}
			if (value.Equals("#NAME?"))
			{
				return ErrorEval.NAME_INVALID;
			}
			if (value.Equals("#NUM!"))
			{
				return ErrorEval.NUM_ERROR;
			}
			if (value.Equals("#N/A"))
			{
				return ErrorEval.NA;
			}
			return null;
		}

		/// bool literals ('TRUE', 'FALSE') treated similarly but NOT same as numbers. 
		public static bool? ParseBoolean(string strRep)
		{
			if (strRep.Length < 1)
			{
				return null;
			}
			switch (strRep[0])
			{
			case 'T':
			case 't':
				if ("TRUE".Equals(strRep, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				break;
			case 'F':
			case 'f':
				if ("FALSE".Equals(strRep, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				break;
			}
			return null;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			IMatchPredicate matchPredicate = CreateCriteriaPredicate(arg1, srcRowIndex, srcColumnIndex);
			if (matchPredicate == null)
			{
				return NumberEval.ZERO;
			}
			double value = CountMatchingCellsInArea(arg0, matchPredicate);
			return new NumberEval(value);
		}
	}
}
