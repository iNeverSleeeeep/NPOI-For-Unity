namespace NPOI.SS.Formula.Functions
{
	public class Sumx2my2 : XYNumericFunction
	{
		public class Accumulator2 : Accumulator
		{
			public double Accumulate(double x, double y)
			{
				return x * x - y * y;
			}
		}

		private static Accumulator XSquaredMinusYSquaredAccumulator = new Accumulator2();

		public override Accumulator CreateAccumulator()
		{
			return XSquaredMinusYSquaredAccumulator;
		}
	}
}
