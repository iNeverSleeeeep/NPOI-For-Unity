namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt; This class is a
	///         marker class. It is a special value for empty cells.
	public class BlankEval : ValueEval
	{
		public static BlankEval instance = new BlankEval();

		private BlankEval()
		{
		}
	}
}
