using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implemented by all functions that can be called with zero arguments
	///
	/// @author Josh Micich
	public interface Function0Arg : Function
	{
		/// see {@link Function#Evaluate(ValueEval[], int, int)}
		ValueEval Evaluate(int srcRowIndex, int srcColumnIndex);
	}
}
