using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Functions
{
	public class Today : Fixed0ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return new NumberEval(DateUtil.GetExcelDate(DateTime.Today));
		}
	}
}
