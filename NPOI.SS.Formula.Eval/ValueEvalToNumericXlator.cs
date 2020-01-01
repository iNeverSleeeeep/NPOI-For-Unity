using System;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class ValueEvalToNumericXlator
	{
		public const int STRING_IS_PARSED = 1;

		public const int BOOL_IS_PARSED = 2;

		public const int BLANK_IS_PARSED = 4;

		public const int REF_STRING_IS_PARSED = 8;

		public const int REF_BOOL_IS_PARSED = 16;

		public const int REF_BLANK_IS_PARSED = 32;

		public const int STRING_IS_INVALID_VALUE = 2048;

		private int flags;

		public ValueEvalToNumericXlator(int flags)
		{
			this.flags = flags;
		}

		/// returned value can be either A NumericValueEval, BlankEval or ErrorEval.
		/// The params can be either NumberEval, BoolEval, StringEval, or
		/// RefEval
		/// @param eval
		public ValueEval AttemptXlateToNumeric(ValueEval eval)
		{
			ValueEval valueEval = null;
			if (eval == null)
			{
				return BlankEval.instance;
			}
			if (eval is NumberEval)
			{
				return eval;
			}
			if (eval is BoolEval)
			{
				return ((flags & 2) > 0) ? ((NumericValueEval)eval) : XlateBlankEval(4);
			}
			if (eval is StringEval)
			{
				return XlateStringEval((StringEval)eval);
			}
			if (eval is RefEval)
			{
				return XlateRefEval((RefEval)eval);
			}
			if (eval is ErrorEval)
			{
				return eval;
			}
			if (!(eval is BlankEval))
			{
				throw new Exception("Invalid ValueEval type passed for conversion: " + eval.GetType());
			}
			return XlateBlankEval(4);
		}

		/// no args are required since BlankEval has only one 
		/// instance. If flag is Set, a zero
		/// valued numbereval is returned, else BlankEval.INSTANCE
		/// is returned.
		private ValueEval XlateBlankEval(int flag)
		{
			if ((flags & flag) <= 0)
			{
				return BlankEval.instance;
			}
			return NumberEval.ZERO;
		}

		/// uses the relevant flags to decode the supplied RefVal
		/// @param eval
		private ValueEval XlateRefEval(RefEval reval)
		{
			ValueEval innerValueEval = reval.InnerValueEval;
			if (innerValueEval is NumberEval)
			{
				return innerValueEval;
			}
			if (innerValueEval is BoolEval)
			{
				if ((flags & 0x10) <= 0)
				{
					return BlankEval.instance;
				}
				return innerValueEval;
			}
			if (innerValueEval is StringEval)
			{
				return XlateRefStringEval((StringEval)innerValueEval);
			}
			if (innerValueEval is ErrorEval)
			{
				return innerValueEval;
			}
			if (innerValueEval is BlankEval)
			{
				return XlateBlankEval(32);
			}
			throw new Exception("Invalid ValueEval type passed for conversion: (" + innerValueEval.GetType().Name + ")");
		}

		/// uses the relevant flags to decode the StringEval
		/// @param eval
		private ValueEval XlateStringEval(StringEval eval)
		{
			if ((flags & 1) > 0)
			{
				string stringValue = eval.StringValue;
				double num = OperandResolver.ParseDouble(stringValue);
				if (double.IsNaN(num))
				{
					return ErrorEval.VALUE_INVALID;
				}
				return new NumberEval(num);
			}
			if ((flags & 0x800) > 0)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return XlateBlankEval(4);
		}

		/// uses the relevant flags to decode the StringEval
		/// @param eval
		private ValueEval XlateRefStringEval(StringEval sve)
		{
			if ((flags & 8) > 0)
			{
				string stringValue = sve.StringValue;
				double num = OperandResolver.ParseDouble(stringValue);
				if (double.IsNaN(num))
				{
					return ErrorEval.VALUE_INVALID;
				}
				return new NumberEval(num);
			}
			return BlankEval.instance;
		}
	}
}
