using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class AggregateFunction : MultiOperandNumericFunction
	{
		internal class ValueCollector : MultiOperandNumericFunction
		{
			private static ValueCollector instance = new ValueCollector();

			public ValueCollector()
				: base(isReferenceBoolCounted: false, isBlankCounted: false)
			{
			}

			public static double[] CollectValues(params ValueEval[] operands)
			{
				return instance.GetNumberArray(operands);
			}

			protected internal override double Evaluate(double[] values)
			{
				throw new InvalidOperationException("should not be called");
			}
		}

		public static readonly Function AVEDEV = new AVEDEV();

		public static readonly Function AVERAGE = new AVERAGE();

		public static readonly Function DEVSQ = new DEVSQ();

		public static readonly Function LARGE = new LARGE();

		public static readonly Function MAX = new MAX();

		public static readonly Function MEDIAN = new MEDIAN();

		public static readonly Function MIN = new MIN();

		public static readonly Function PRODUCT = new PRODUCT();

		public static readonly Function SMALL = new SMALL();

		public static readonly Function STDEV = new STDEV();

		public static readonly Function SUM = new SUM();

		public static readonly Function SUMSQ = new SUMSQ();

		public static readonly Function VAR = new VAR();

		public static readonly Function VARP = new VARP();

		public static Function SubtotalInstance(Function func)
		{
			AggregateFunction func2 = (AggregateFunction)func;
			return new SubtotalInstance(func2);
		}

		protected AggregateFunction()
			: base(isReferenceBoolCounted: false, isBlankCounted: false)
		{
		}
	}
}
