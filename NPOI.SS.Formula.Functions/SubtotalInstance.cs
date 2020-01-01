namespace NPOI.SS.Formula.Functions
{
	public class SubtotalInstance : AggregateFunction
	{
		private AggregateFunction _func;

		/// ignore nested subtotals.
		public override bool IsSubtotalCounted => false;

		public SubtotalInstance(AggregateFunction func)
		{
			_func = func;
		}

		protected internal override double Evaluate(double[] values)
		{
			return _func.Evaluate(values);
		}
	}
}
