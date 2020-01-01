using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Convenience base class for any function which must take two or three
	/// arguments
	///
	/// @author Josh Micich
	public abstract class Var1or2ArgFunction : Function1Arg, Function2Arg, Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 1:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0]);
			case 2:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1]);
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0);

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1);
	}
}
