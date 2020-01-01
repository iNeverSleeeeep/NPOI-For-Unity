using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function INDEX
	///
	/// Syntax : <br />
	///  INDEX ( reference, row_num[, column_num [, area_num]])<br />
	///  INDEX ( array, row_num[, column_num])
	///    <table border="0" cellpAdding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><th>reference</th><td>typically an area reference, possibly a union of areas</td></tr>
	///      <tr><th>array</th><td>a literal array value (currently not supported)</td></tr>
	///      <tr><th>row_num</th><td>selects the row within the array or area reference</td></tr>
	///      <tr><th>column_num</th><td>selects column within the array or area reference. default Is 1</td></tr>
	///      <tr><th>area_num</th><td>used when reference Is a union of areas</td></tr>
	///    </table>
	///
	/// @author Josh Micich
	public class Index : Function2Arg, Function3Arg, Function4Arg, Function
	{
		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			TwoDEval twoDEval = ConvertFirstArg(arg0);
			int pColumnIx = 0;
			try
			{
				int num = ResolveIndexArg(arg1, srcRowIndex, srcColumnIndex);
				if (!twoDEval.IsColumn)
				{
					if (!twoDEval.IsRow)
					{
						return ErrorEval.REF_INVALID;
					}
					pColumnIx = num;
					num = 0;
				}
				return GetValueFromArea(twoDEval, num, pColumnIx);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			TwoDEval ae = ConvertFirstArg(arg0);
			try
			{
				int pColumnIx = ResolveIndexArg(arg2, srcRowIndex, srcColumnIndex);
				int pRowIx = ResolveIndexArg(arg1, srcRowIndex, srcColumnIndex);
				return GetValueFromArea(ae, pRowIx, pColumnIx);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		public ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3)
		{
			throw new Exception("Incomplete code - don't know how to support the 'area_num' parameter yet)");
		}

		private static TwoDEval ConvertFirstArg(ValueEval arg0)
		{
			if (arg0 is RefEval)
			{
				return ((RefEval)arg0).Offset(0, 0, 0, 0);
			}
			if (arg0 is TwoDEval)
			{
				return (TwoDEval)arg0;
			}
			throw new Exception("Incomplete code - cannot handle first arg of type (" + arg0.GetType().Name + ")");
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 2:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1]);
			case 3:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2]);
			case 4:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], args[3]);
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}

		private static ValueEval GetValueFromArea(TwoDEval ae, int pRowIx, int pColumnIx)
		{
			TwoDEval twoDEval = ae;
			if (pRowIx != 0)
			{
				if (pRowIx > ae.Height)
				{
					throw new EvaluationException(ErrorEval.REF_INVALID);
				}
				twoDEval = twoDEval.GetRow(pRowIx - 1);
			}
			if (pColumnIx != 0)
			{
				if (pColumnIx > ae.Width)
				{
					throw new EvaluationException(ErrorEval.REF_INVALID);
				}
				twoDEval = twoDEval.GetColumn(pColumnIx - 1);
			}
			return twoDEval;
		}

		/// @param colArgWasPassed <c>false</c> if the INDEX argument lIst had just 2 items
		///            (exactly 1 comma).  If anything Is passed for the <c>column_num</c> argument
		///            (including {@link BlankEval} or {@link MIssingArgEval}) this parameter will be
		///            <c>true</c>.  ThIs parameter is needed because error codes are slightly
		///            different when only 2 args are passed.
		[Obsolete]
		private static ValueEval GetValueFromArea(AreaEval ae, int pRowIx, int pColumnIx, bool colArgWasPassed, int srcRowIx, int srcColIx)
		{
			bool flag = pRowIx == 0;
			bool flag2 = pColumnIx == 0;
			int num;
			int num2;
			if (ae.IsRow)
			{
				if (ae.IsColumn)
				{
					num = ((!flag) ? (pRowIx - 1) : 0);
					num2 = ((!flag2) ? (pColumnIx - 1) : 0);
				}
				else if (colArgWasPassed)
				{
					num = ((!flag) ? (pRowIx - 1) : 0);
					num2 = pColumnIx - 1;
				}
				else
				{
					num = 0;
					num2 = pRowIx - 1;
					flag2 = flag;
				}
			}
			else if (ae.IsColumn)
			{
				num = ((!flag) ? (pRowIx - 1) : (srcRowIx - ae.FirstRow));
				int num3;
				switch (flag2)
				{
				default:
					num2 = 0;
					break;
				case false:
					{
						num3 = pColumnIx - 1;
						goto IL_007b;
					}
					IL_007b:
					num2 = num3;
					break;
					IL_007a:
					num3 = 0;
					goto IL_007b;
				}
			}
			else
			{
				if (!colArgWasPassed)
				{
					throw new EvaluationException((pRowIx < 0) ? ErrorEval.VALUE_INVALID : ErrorEval.REF_INVALID);
				}
				num = ((!flag) ? (pRowIx - 1) : (srcRowIx - ae.FirstRow));
				num2 = ((!flag2) ? (pColumnIx - 1) : (srcColIx - ae.FirstColumn));
			}
			int width = ae.Width;
			int height = ae.Height;
			if ((!flag && num >= height) || (!flag2 && num2 >= width))
			{
				throw new EvaluationException(ErrorEval.REF_INVALID);
			}
			if (num < 0 || num2 < 0 || num >= height || num2 >= width)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			return ae.GetRelativeValue(num, num2);
		}

		/// @param arg a 1-based index.
		/// @return the Resolved 1-based index. Zero if the arg was missing or blank
		/// @throws EvaluationException if the arg Is an error value evaluates to a negative numeric value
		private static int ResolveIndexArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			if (singleValue == MissingArgEval.instance)
			{
				return 0;
			}
			if (singleValue == BlankEval.instance)
			{
				return 0;
			}
			int num = OperandResolver.CoerceValueToInt(singleValue);
			if (num < 0)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			return num;
		}
	}
}
