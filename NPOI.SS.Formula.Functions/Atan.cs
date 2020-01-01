using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Atan : OneArg
	{
		public override double Evaluate(double d)
		{
			return Math.Atan(d);
		}
	}
}
