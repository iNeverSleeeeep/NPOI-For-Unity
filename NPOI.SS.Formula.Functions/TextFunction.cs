using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public abstract class TextFunction : Function
	{
		protected static string EMPTY_STRING = "";

		public static readonly Function LEN = new Len();

		public static readonly Function LOWER = new Lower();

		public static readonly Function UPPER = new Upper();

		/// @author Manda Wilson &lt; wilson at c bio dot msk cc dot org &gt;
		/// <summary>
		/// An implementation of the TRIM function:
		/// <para>
		///  Removes leading and trailing spaces from value if evaluated operand value is string.
		/// </para>
		/// </summary>
		public static readonly Function TRIM = new Trim();

		/// <summary>
		/// An implementation of the MID function
		///
		/// MID returns a specific number of
		/// characters from a text string, starting at the specified position.
		///
		///  Syntax: MID(text, start_num, num_chars)
		/// </summary>
		public static readonly Function MID = new Mid();

		public static readonly Function LEFT = new LeftRight(isLeft: true);

		public static readonly Function RIGHT = new LeftRight(isLeft: false);

		public static readonly Function CONCATENATE = new Concatenate();

		public static readonly Function EXACT = new Exact();

		public static readonly Function TEXT = new Text();

		/// @author Torstein Tauno Svendsen (torstei@officenet.no)
		/// <summary>
		/// Implementation of the FIND() function.
		/// <para>
		///  Syntax: FIND(Find_text, within_text, start_num)
		/// </para>
		/// <para> FIND returns the character position of the first (case sensitive) occurrence of
		///  Find_text inside within_text.  The third parameter,
		///  start_num, is optional (default=1) and specifies where to start searching
		///  from.  Character positions are 1-based.</para>
		/// </summary>
		public static readonly Function FIND = new SearchFind(isCaseSensitive: true);

		/// <summary>
		/// Implementation of the FIND() function. SEARCH is a case-insensitive version of FIND()
		/// <para>
		///  Syntax: SEARCH(Find_text, within_text, start_num)
		/// </para>
		/// </summary>
		public static readonly Function SEARCH = new SearchFind(isCaseSensitive: false);

		public static readonly Function CLEAN = new Clean();

		public static readonly Function CHAR = new CHAR();

		public static string EvaluateStringArg(ValueEval eval, int srcRow, int srcCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(eval, srcRow, srcCol);
			return OperandResolver.CoerceValueToString(singleValue);
		}

		public static int EvaluateIntArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			return OperandResolver.CoerceValueToInt(singleValue);
		}

		public static double EvaluateDoubleArg(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcCellRow, srcCellCol);
			return OperandResolver.CoerceValueToDouble(singleValue);
		}

		public ValueEval Evaluate(ValueEval[] args, int srcCellRow, int srcCellCol)
		{
			try
			{
				return EvaluateFunc(args, srcCellRow, srcCellCol);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}

		internal static bool IsPrintable(char c)
		{
			return c >= ' ';
		}

		public abstract ValueEval EvaluateFunc(ValueEval[] args, int srcCellRow, int srcCellCol);
	}
}
