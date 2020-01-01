namespace NPOI.SS.Formula.Functions
{
	public class Roundup : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			return MathX.roundUp(d0, (int)d1);
		}
	}
}
