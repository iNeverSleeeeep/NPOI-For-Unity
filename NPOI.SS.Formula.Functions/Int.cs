using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Int : OneArg
	{
		public override double Evaluate(double d)
		{
			if (d > 0.0)
			{
				return Math.Round(d - 0.49);
			}
			return Math.Round(d - 0.5);
		}
	}
}
