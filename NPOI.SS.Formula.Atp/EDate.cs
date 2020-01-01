using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Atp
{
	/// Implementation of Excel 'Analysis ToolPak' function EDATE()<br />
	///
	/// Adds a specified number of months to the specified date.<p />
	///
	/// <b>Syntax</b><br />
	/// <b>EDATE</b>(<b>date</b>, <b>number</b>)
	///
	/// <p />
	///
	/// @author Tomas Herceg
	public class EDate : FreeRefFunction
	{
		public static FreeRefFunction Instance = new EDate();

		private EDate()
		{
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length == 2)
			{
				try
				{
					double value = GetValue(args[0]);
					NumberEval numberEval = (NumberEval)args[1];
					int months = (int)numberEval.NumberValue;
					DateTime date = DateUtil.GetJavaDate(value).AddMonths(months);
					double excelDate = DateUtil.GetExcelDate(date);
					NumericFunction.CheckValue(excelDate);
					return new NumberEval(excelDate);
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		private double GetValue(ValueEval arg)
		{
			if (arg is NumberEval)
			{
				return ((NumberEval)arg).NumberValue;
			}
			if (arg is RefEval)
			{
				ValueEval innerValueEval = ((RefEval)arg).InnerValueEval;
				return ((NumberEval)innerValueEval).NumberValue;
			}
			throw new EvaluationException(ErrorEval.REF_INVALID);
		}
	}
}
