namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class EqualEval : RelationalOperationEval
	{
		public override bool ConvertComparisonResult(int cmpResult)
		{
			return cmpResult == 0;
		}
	}
}
