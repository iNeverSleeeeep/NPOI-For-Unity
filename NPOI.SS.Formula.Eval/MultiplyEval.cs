namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class MultiplyEval : TwoOperandNumericOperation
	{
		public override double Evaluate(double d0, double d1)
		{
			return d0 * d1;
		}
	}
}
