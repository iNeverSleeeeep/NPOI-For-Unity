using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using System;

namespace NPOI.SS.Formula
{
	public class UserDefinedFunction : FreeRefFunction
	{
		public static FreeRefFunction instance = new UserDefinedFunction();

		private UserDefinedFunction()
		{
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			int num = args.Length;
			if (num < 1)
			{
				throw new Exception("function name argument missing");
			}
			ValueEval valueEval = args[0];
			string empty = string.Empty;
			if (valueEval is NameEval)
			{
				empty = ((NameEval)valueEval).FunctionName;
			}
			else
			{
				if (!(valueEval is NameXEval))
				{
					throw new Exception("First argument should be a NameEval, but got (" + valueEval.GetType().Name + ")");
				}
				empty = ec.GetWorkbook().ResolveNameXText(((NameXEval)valueEval).Ptg);
			}
			FreeRefFunction freeRefFunction = ec.FindUserDefinedFunction(empty);
			if (freeRefFunction == null)
			{
				throw new NotImplementedException(empty);
			}
			int num2 = num - 1;
			ValueEval[] array = new ValueEval[num2];
			Array.Copy(args, 1, array, 0, num2);
			return freeRefFunction.Evaluate(array, ec);
		}
	}
}
