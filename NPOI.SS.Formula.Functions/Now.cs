using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation of Excel NOW() Function
	///
	/// @author Frank Taffelt
	public class Now : Fixed0ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return new NumberEval(DateUtil.GetExcelDate(DateTime.Now));
		}
	}
}
