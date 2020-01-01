namespace NPOI.SS.Formula.Functions
{
	public class SUM : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			return MathX.sum(values);
		}
	}
}
