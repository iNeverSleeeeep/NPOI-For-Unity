namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class GreaterEqualEval : RelationalOperationEval
	{
		public override bool ConvertComparisonResult(int cmpResult)
		{
			return cmpResult >= 0;
		}
	}
}
