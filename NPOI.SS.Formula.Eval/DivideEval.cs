namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class DivideEval : TwoOperandNumericOperation
	{
		public override double Evaluate(double d0, double d1)
		{
			if (d1 == 0.0)
			{
				throw new EvaluationException(ErrorEval.DIV_ZERO);
			}
			return d0 / d1;
		}
	}
}
