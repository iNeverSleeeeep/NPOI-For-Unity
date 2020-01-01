using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Convenience base class for functions that must take exactly four arguments.
	///
	/// @author Josh Micich
	public abstract class Fixed4ArgFunction : Function4Arg, Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			if (args.Length != 4)
			{
				return ErrorEval.VALUE_INVALID;
			}
			return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], args[3]);
		}

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3);
	}
}
