using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class LogicalFunction : Fixed1ArgFunction
	{
		public static readonly Function ISLOGICAL = new Islogical();

		public static readonly Function ISNONTEXT = new Isnontext();

		public static readonly Function ISNUMBER = new Isnumber();

		public static readonly Function ISTEXT = new Istext();

		public static readonly Function ISBLANK = new Isblank();

		public static readonly Function ISERROR = new Iserror();

		public static readonly Function ISNA = new Isna();

		public static readonly Function ISREF = new Isref();

		public static readonly Function ISERR = new Iserr();

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			ValueEval arg;
			try
			{
				arg = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				arg = ex.GetErrorEval();
			}
			return BoolEval.ValueOf(Evaluate(arg));
		}

		/// @param arg any {@link ValueEval}, potentially {@link BlankEval} or {@link ErrorEval}.
		protected abstract bool Evaluate(ValueEval arg);
	}
}
