using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class Rand : Fixed0ArgFunction
	{
		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
		{
			return new NumberEval(new Random().NextDouble());
		}
	}
}
