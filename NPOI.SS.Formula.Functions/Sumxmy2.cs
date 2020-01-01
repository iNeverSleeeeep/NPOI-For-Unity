namespace NPOI.SS.Formula.Functions
{
	public class Sumxmy2 : XYNumericFunction
	{
		public class Accumulator1 : Accumulator
		{
			public double Accumulate(double x, double y)
			{
				double num = x - y;
				return num * num;
			}
		}

		private static Accumulator XMinusYSquaredAccumulator = new Accumulator1();

		public override Accumulator CreateAccumulator()
		{
			return XMinusYSquaredAccumulator;
		}
	}
}
