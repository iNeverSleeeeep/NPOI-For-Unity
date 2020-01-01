namespace NPOI.SS.Formula.Functions
{
	public class Fv : FinanceFunction
	{
		public override double Evaluate(double rate, double arg1, double arg2, double arg3, bool type)
		{
			return FinanceLib.fv(rate, arg1, arg2, arg3, type);
		}
	}
}
