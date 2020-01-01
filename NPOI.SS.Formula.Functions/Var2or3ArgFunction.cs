using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Convenience base class for any function which must take two or three
	/// arguments
	///
	/// @author Josh Micich
	public abstract class Var2or3ArgFunction : Function2Arg, Function3Arg, Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 2:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1]);
			case 3:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2]);
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1);

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2);
	}
}
