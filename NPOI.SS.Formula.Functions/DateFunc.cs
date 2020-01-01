using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;

namespace NPOI.SS.Formula.Functions
{
	/// @author Pavel Krupets (pkrupets at palmtreebusiness dot com)
	public class DateFunc : Fixed3ArgFunction
	{
		public static Function instance = new DateFunc();

		private DateFunc()
		{
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			double num3;
			try
			{
				double d = NumericFunction.SingleOperandEvaluate(arg0, srcRowIndex, srcColumnIndex);
				double num = NumericFunction.SingleOperandEvaluate(arg1, srcRowIndex, srcColumnIndex);
				double num2 = NumericFunction.SingleOperandEvaluate(arg2, srcRowIndex, srcColumnIndex);
				num3 = Evaluate(GetYear(d), (int)num, (int)num2);
				NumericFunction.CheckValue(num3);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(num3);
		}

		/// * Note - works with Java Calendar months, not Excel months
		///                      * Java Calendar month = Excel month + 1
		public double Evaluate(int year, int month, int pDay)
		{
			if (year < 0)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			while (month <= 0)
			{
				year--;
				month += 12;
			}
			if (year == 1900 && month == 2 && pDay == 29)
			{
				return 60.0;
			}
			int num = pDay;
			if (year == 1900 && ((month == 1 && num >= 60) || (month == 2 && num >= 30)))
			{
				num--;
			}
			bool use1904windowing = false;
			return DateUtil.GetExcelDate(year, month, num, 0, 0, 0, use1904windowing);
		}

		private int GetYear(double d)
		{
			int num = (int)d;
			if (num < 0)
			{
				return -1;
			}
			if (num >= 1900)
			{
				return num;
			}
			return 1900 + num;
		}
	}
}
