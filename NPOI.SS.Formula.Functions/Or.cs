namespace NPOI.SS.Formula.Functions
{
	public class Or : BooleanFunction
	{
		protected override bool InitialResultValue => false;

		protected override bool PartialEvaluate(bool cumulativeResult, bool currentValue)
		{
			if (!cumulativeResult)
			{
				return currentValue;
			}
			return true;
		}
	}
}
