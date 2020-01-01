namespace NPOI.SS.Formula.Functions
{
	public class AVEDEV : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			return StatsLib.avedev(values);
		}
	}
}
