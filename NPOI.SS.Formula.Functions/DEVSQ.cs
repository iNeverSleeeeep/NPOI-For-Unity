namespace NPOI.SS.Formula.Functions
{
	public class DEVSQ : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			return StatsLib.devsq(values);
		}
	}
}
