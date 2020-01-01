using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// <summary>
	/// Function serves as a marker interface.
	/// </summary>
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public interface Function
	{
		/// <summary>
		/// Evaluates the specified args.
		/// </summary>
		/// <param name="args">the evaluated function arguments.  Empty values are represented with BlankEval or MissingArgEval</param>
		/// <param name="srcRowIndex">row index of the cell containing the formula under evaluation</param>
		/// <param name="srcColumnIndex">column index of the cell containing the formula under evaluation</param>
		/// <returns></returns>
		ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex);
	}
}
