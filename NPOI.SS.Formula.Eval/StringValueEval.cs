namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public interface StringValueEval : ValueEval
	{
		/// @return never <code>null</code>, possibly empty string.
		string StringValue
		{
			get;
		}
	}
}
