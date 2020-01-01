namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Ceiling : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			return MathX.ceiling(d0, d1);
		}
	}
}
