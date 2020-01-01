using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Power : TwoArg
	{
		public override double Evaluate(double d0, double d1)
		{
			return Math.Pow(d0, d1);
		}
	}
}
