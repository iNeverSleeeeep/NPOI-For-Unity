namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Odd : OneArg
	{
		private const long PARITY_MASK = -2L;

		public override double Evaluate(double d)
		{
			if (d == 0.0)
			{
				return 1.0;
			}
			long num = (!(d > 0.0)) ? (-CalcOdd(0.0 - d)) : CalcOdd(d);
			return (double)num;
		}

		private static long CalcOdd(double d)
		{
			double num = d + 1.0;
			long num2 = (long)num & -2;
			if ((double)num2 == num)
			{
				return num2 - 1;
			}
			return num2 + 1;
		}
	}
}
