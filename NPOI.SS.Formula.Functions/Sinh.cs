namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Sinh : OneArg
	{
		public override double Evaluate(double d)
		{
			return MathX.sinh(d);
		}
	}
}
