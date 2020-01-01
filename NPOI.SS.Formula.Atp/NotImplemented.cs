using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula.Atp
{
	public class NotImplemented : FreeRefFunction
	{
		private string _functionName;

		public NotImplemented(string functionName)
		{
			_functionName = functionName;
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			throw new NotImplementedException(_functionName);
		}
	}
}
