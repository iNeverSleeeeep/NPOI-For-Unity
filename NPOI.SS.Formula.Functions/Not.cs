using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	public class Not : Fixed1ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			bool flag2;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
				bool? flag = OperandResolver.CoerceValueToBoolean(singleValue, stringsAreBlanks: false);
				flag2 = (flag.HasValue && flag.Value);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return BoolEval.ValueOf(!flag2);
		}
	}
}
