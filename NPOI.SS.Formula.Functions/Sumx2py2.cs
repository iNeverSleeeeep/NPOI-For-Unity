namespace NPOI.SS.Formula.Functions
{
	public class Sumx2py2 : XYNumericFunction
	{
		public class Accumulator3 : Accumulator
		{
			public double Accumulate(double x, double y)
			{
				return x * x + y * y;
			}
		}

		private static Accumulator XSquaredPlusYSquaredAccumulator = new Accumulator3();

		public override Accumulator CreateAccumulator()
		{
			return XSquaredPlusYSquaredAccumulator;
		}
	}
}
