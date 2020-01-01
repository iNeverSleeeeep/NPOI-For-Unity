namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Degrees : OneArg
	{
		public override double Evaluate(double d)
		{
			return d * 180.0 / 3.1415926535897931;
		}
	}
}
