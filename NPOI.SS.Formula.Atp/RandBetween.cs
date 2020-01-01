using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// * Implementation of Excel 'Analysis ToolPak' function RANDBETWEEN()<br />
	/// *
	/// * Returns a random integer number between the numbers you specify.<p />
	/// *
	/// * <b>Syntax</b><br />
	/// * <b>RANDBETWEEN</b>(<b>bottom</b>, <b>top</b>)<p />
	/// *
	/// * <b>bottom</b> is the smallest integer RANDBETWEEN will return.<br />
	/// * <b>top</b> is the largest integer RANDBETWEEN will return.<br />
	///
	/// * @author Brendan Nolan
	internal class RandBetween : FreeRefFunction
	{
		public static FreeRefFunction Instance = new RandBetween();

		private RandBetween()
		{
		}

		/// Evaluate for RANDBETWEEN(). Must be given two arguments. Bottom must be greater than top.
		/// Bottom is rounded up and top value is rounded down. After rounding top has to be set greater
		/// than top.
		///
		/// @see org.apache.poi.ss.formula.functions.FreeRefFunction#evaluate(org.apache.poi.ss.formula.eval.ValueEval[], org.apache.poi.ss.formula.OperationEvaluationContext)
		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length != 2)
			{
				return ErrorEval.VALUE_INVALID;
			}
			double num;
			double num2;
			try
			{
				num = OperandResolver.CoerceValueToDouble(OperandResolver.GetSingleValue(args[0], ec.RowIndex, ec.ColumnIndex));
				num2 = OperandResolver.CoerceValueToDouble(OperandResolver.GetSingleValue(args[1], ec.RowIndex, ec.ColumnIndex));
				if (num > num2)
				{
					return ErrorEval.NUM_ERROR;
				}
			}
			catch (EvaluationException)
			{
				return ErrorEval.VALUE_INVALID;
			}
			num = Math.Ceiling(num);
			num2 = Math.Floor(num2);
			if (num > num2)
			{
				num2 = num;
			}
			Random random = new Random();
			return new NumberEval(num + (double)(int)(random.NextDouble() * (num2 - num + 1.0)));
		}
	}
}
