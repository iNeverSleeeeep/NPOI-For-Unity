using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class If : Var2or3ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1)
		{
			bool flag;
			try
			{
				flag = EvaluateFirstArg(arg0, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (flag)
			{
				if (arg1 == MissingArgEval.instance)
				{
					return BlankEval.instance;
				}
				return arg1;
			}
			return BoolEval.FALSE;
		}

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			bool flag;
			try
			{
				flag = EvaluateFirstArg(arg0, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			if (flag)
			{
				if (arg1 == MissingArgEval.instance)
				{
					return BlankEval.instance;
				}
				return arg1;
			}
			if (arg2 == MissingArgEval.instance)
			{
				return BlankEval.instance;
			}
			return arg2;
		}

		public static bool EvaluateFirstArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			bool? flag = OperandResolver.CoerceValueToBoolean(singleValue, stringsAreBlanks: false);
			if (!flag.HasValue)
			{
				return false;
			}
			return flag.Value;
		}
	}
}
