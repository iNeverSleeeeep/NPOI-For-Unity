using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// This Is the default implementation of a Function class. 
	/// The default behaviour Is to return a non-standard ErrorEval
	/// "ErrorEval.FUNCTION_NOT_IMPLEMENTED". This error should alert 
	/// the user that the formula contained a function that Is not
	/// yet implemented.
	public class NotImplementedFunction : Function
	{
		private string _functionName;

		public string FunctionName => _functionName;

		internal NotImplementedFunction()
		{
			_functionName = GetType().Name;
		}

		public NotImplementedFunction(string name)
		{
			_functionName = name;
		}

		public ValueEval Evaluate(ValueEval[] operands, int srcRow, int srcCol)
		{
			throw new NotImplementedException(_functionName);
		}
	}
}
