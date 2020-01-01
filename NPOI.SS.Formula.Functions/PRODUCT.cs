namespace NPOI.SS.Formula.Functions
{
	public class PRODUCT : AggregateFunction
	{
		protected internal override double Evaluate(double[] values)
		{
			return MathX.product(values);
		}
	}
}
