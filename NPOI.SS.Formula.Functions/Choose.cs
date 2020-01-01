using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Josh Micich
	public class Choose : Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length >= 2)
			{
				try
				{
					int num = EvaluateFirstArg(args[0], srcRowIndex, srcColumnIndex);
					if (num < 1 || num >= args.Length)
					{
						return ErrorEval.VALUE_INVALID;
					}
					ValueEval singleValue = OperandResolver.GetSingleValue(args[num], srcRowIndex, srcColumnIndex);
					if (singleValue == MissingArgEval.instance)
					{
						return BlankEval.instance;
					}
					return singleValue;
				}
				catch (EvaluationException ex)
				{
					return ex.GetErrorEval();
				}
			}
			return ErrorEval.VALUE_INVALID;
		}

		public static int EvaluateFirstArg(ValueEval arg0, int srcRowIndex, int srcColumnIndex)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
			return OperandResolver.CoerceValueToInt(singleValue);
		}
	}
}
