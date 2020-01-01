namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Radians : OneArg
	{
		public override double Evaluate(double d)
		{
			return d * 3.1415926535897931 / 180.0;
		}
	}
}
