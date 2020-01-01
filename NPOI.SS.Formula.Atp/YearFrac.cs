using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// Implementation of Excel 'Analysis ToolPak' function YEARFRAC()<br />
	///
	/// Returns the fraction of the year spanned by two dates.<p />
	///
	/// <b>Syntax</b><br />
	/// <b>YEARFRAC</b>(<b>startDate</b>, <b>endDate</b>, basis)<p />
	///
	/// The <b>basis</b> optionally specifies the behaviour of YEARFRAC as follows:
	///
	/// <table border="0" cellpadding="1" cellspacing="0" summary="basis parameter description">
	///   <tr><th>Value</th><th>Days per Month</th><th>Days per Year</th></tr>
	///   <tr align="center"><td>0 (default)</td><td>30</td><td>360</td></tr>
	///   <tr align="center"><td>1</td><td>actual</td><td>actual</td></tr>
	///   <tr align="center"><td>2</td><td>actual</td><td>360</td></tr>
	///   <tr align="center"><td>3</td><td>actual</td><td>365</td></tr>
	///   <tr align="center"><td>4</td><td>30</td><td>360</td></tr>
	/// </table>
	internal class YearFrac : FreeRefFunction
	{
		public static FreeRefFunction instance = new YearFrac();

		private YearFrac()
		{
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			int rowIndex = ec.RowIndex;
			int columnIndex = ec.ColumnIndex;
			double value;
			try
			{
				int basis = 0;
				switch (args.Length)
				{
				case 3:
					basis = EvaluateIntArg(args[2], rowIndex, columnIndex);
					break;
				default:
					return ErrorEval.VALUE_INVALID;
				case 2:
					break;
				}
				double pStartDateVal = EvaluateDateArg(args[0], rowIndex, columnIndex);
				double pEndDateVal = EvaluateDateArg(args[1], rowIndex, columnIndex);
				value = YearFracCalculator.Calculate(pStartDateVal, pEndDateVal, basis);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}

		private static double EvaluateDateArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, (short)srcCellCol);
			if (singleValue is StringEval)
			{
				string stringValue = ((StringEval)singleValue).StringValue;
				double num = OperandResolver.ParseDouble(stringValue);
				if (!double.IsNaN(num))
				{
					return num;
				}
				DateTime date = DateParser.ParseDate(stringValue);
				return DateUtil.GetExcelDate(date, use1904windowing: false);
			}
			return OperandResolver.CoerceValueToDouble(singleValue);
		}

		private static int EvaluateIntArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			return OperandResolver.CoerceValueToInt(singleValue);
		}
	}
}
