namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Even : OneArg
	{
		private const long PARITY_MASK = -2L;

		public override double Evaluate(double d)
		{
			if (d == 0.0)
			{
				return 0.0;
			}
			long num = (!(d > 0.0)) ? (-calcEven(0.0 - d)) : calcEven(d);
			return (double)num;
		}

		private static long calcEven(double d)
		{
			long num = (long)d & -2;
			if ((double)num == d)
			{
				return num;
			}
			return num + 2;
		}
	}
}
