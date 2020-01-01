using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Convenience base class for functions that must take exactly three arguments.
	///
	/// @author Josh Micich
	public abstract class Fixed3ArgFunction : Function3Arg, Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length != 3)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2]);
		}

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2);
	}
}
