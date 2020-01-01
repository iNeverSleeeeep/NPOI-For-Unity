namespace NPOI.SS.Formula.Functions
{
	public class MEDIAN : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			return StatsLib.median(values);
		}
	}
}
