using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implemented by all functions that can be called with three arguments
	///
	/// @author Josh Micich
	public interface Function3Arg : Function
	{
		/// see {@link Function#Evaluate(ValueEval[], int, int)}
		ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2);
	}
}
