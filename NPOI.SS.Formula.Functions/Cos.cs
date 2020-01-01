using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Cos : OneArg
	{
		public override double Evaluate(double d)
		{
			return Math.Cos(d);
		}
	}
}
