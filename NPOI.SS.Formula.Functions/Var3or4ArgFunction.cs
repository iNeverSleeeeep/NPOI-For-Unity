using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Convenience base class for any function which must take three or four
	/// arguments
	///
	/// @author Josh Micich
	public abstract class Var3or4ArgFunction : Function3Arg, Function4Arg, Function
	{
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			switch (args.Length)
			{
			case 3:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2]);
			case 4:
				return Evaluate(srcRowIndex, srcColumnIndex, args[0], args[1], args[2], args[3]);
			default:
				return ErrorEval.VALUE_INVALID;
			}
		}

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2);

		public abstract ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2, ValueEval arg3);
	}
}
