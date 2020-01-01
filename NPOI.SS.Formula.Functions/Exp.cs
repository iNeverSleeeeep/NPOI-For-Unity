using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Exp : OneArg
	{
		public override double Evaluate(double d)
		{
			return Math.Pow(2.7182818284590451, d);
		}
	}
}
