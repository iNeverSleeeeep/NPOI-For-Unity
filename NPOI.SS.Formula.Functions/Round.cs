namespace NPOI.SS.Formula.Functions
{
	public class Round : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			return MathX.round(d0, (int)d1);
		}
	}
}
