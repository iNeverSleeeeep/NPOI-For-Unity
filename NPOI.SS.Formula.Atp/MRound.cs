using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// Implementation of Excel 'Analysis ToolPak' function MROUND()<br />
	///
	/// Returns a number rounded to the desired multiple.<p />
	///
	/// <b>Syntax</b><br />
	/// <b>MROUND</b>(<b>number</b>, <b>multiple</b>)
	///
	/// <p />
	///
	/// @author Yegor Kozlov
	internal class MRound : FreeRefFunction
	{
		public static FreeRefFunction Instance = new MRound();

		private MRound()
		{
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length == 2)
			{
				try
				{
					double num = OperandResolver.CoerceValueToDouble(OperandResolver.GetSingleValue(args[0], ec.RowIndex, ec.ColumnIndex));
					double num2 = OperandResolver.CoerceValueToDouble(OperandResolver.GetSingleValue(args[1], ec.RowIndex, ec.ColumnIndex));
					double num3;
					if (num2 == 0.0)
					{
						num3 = 0.0;
					}
					else
					{
						if (num * num2 < 0.0)
						{
							throw new EvaluationException(ErrorEval.NUM_ERROR);
						}
						num3 = num2 * Math.Round(num / num2, MidpointRounding.AwayFromZero);
					}
					NumericFunction.CheckValue(num3);
					return new NumberEval(num3);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}
	}
}
