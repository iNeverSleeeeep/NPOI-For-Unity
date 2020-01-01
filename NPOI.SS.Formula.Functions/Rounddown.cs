namespace NPOI.SS.Formula.Functions
{
	public class Rounddown : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			return MathX.roundDown(d0, (int)d1);
		}
	}
}
