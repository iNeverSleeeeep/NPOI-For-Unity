namespace NPOI.SS.Formula.Functions
{
	public class And : BooleanFunction
	{
		protected override bool InitialResultValue => true;

		protected override bool PartialEvaluate(bool cumulativeResult, bool currentValue)
		{
			if (cumulativeResult)
			{
				return currentValue;
			}
			return false;
		}
	}
}
