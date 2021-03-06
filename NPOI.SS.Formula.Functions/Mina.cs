namespace NPOI.SS.Formula.Functions
{
	public class Mina : MinaMaxa
	{
		protected internal override double Evaluate(double[] values)
		{
			if (values.Length <= 0)
			{
				return 0.0;
			}
			return MathX.min(values);
		}
	}
}
