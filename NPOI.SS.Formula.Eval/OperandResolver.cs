using System;
using System.Globalization;

namespace NPOI.SS.Formula.Eval
{
	/// Provides functionality for evaluating arguments to functions and operators.
	///
	/// @author Josh Micich
	public class OperandResolver
	{
		private const string Digits = "\\d+";

		private const string Exp = "[eE][+-]?\\d+";

		private const string fpRegex = "[\\x00-\\x20]*[+-]?((((\\d+(\\.)?(\\d+?)([eE][+-]?\\d+)?)|(\\.(\\d+)([eE][+-]?\\d+)?))))[\\x00-\\x20]*";

		private OperandResolver()
		{
		}

		/// Retrieves a single value from a variety of different argument types according to standard
		/// Excel rules.  Does not perform any type conversion.
		/// @param arg the Evaluated argument as passed to the function or operator.
		/// @param srcCellRow used when arg is a single column AreaRef
		/// @param srcCellCol used when arg is a single row AreaRef
		/// @return a <c>NumberEval</c>, <c>StringEval</c>, <c>BoolEval</c> or <c>BlankEval</c>.
		/// Never <c>null</c> or <c>ErrorEval</c>.
		/// @throws EvaluationException(#VALUE!) if srcCellRow or srcCellCol do not properly index into
		///  an AreaEval.  If the actual value retrieved is an ErrorEval, a corresponding 
		///  EvaluationException is thrown.
		public static ValueEval GetSingleValue(ValueEval arg, int srcCellRow, int srcCellCol)
		{
			ValueEval valueEval = (arg is RefEval) ? ((RefEval)arg).InnerValueEval : ((!(arg is AreaEval)) ? arg : ChooseSingleElementFromArea((AreaEval)arg, srcCellRow, srcCellCol));
			if (valueEval is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)valueEval);
			}
			return valueEval;
		}

		/// Implements (some perhaps not well known) Excel functionality to select a single cell from an
		/// area depending on the coordinates of the calling cell.  Here is an example demonstrating
		/// both selection from a single row area and a single column area in the same formula.
		///
		///    <table border="1" cellpAdding="1" cellspacing="1" summary="sample spReadsheet">
		///      <tr><th> </th><th> A </th><th> B </th><th> C </th><th> D </th></tr>
		///      <tr><th>1</th><td>15</td><td>20</td><td>25</td><td> </td></tr>
		///      <tr><th>2</th><td> </td><td> </td><td> </td><td>200</td></tr>
		///      <tr><th>3</th><td> </td><td> </td><td> </td><td>300</td></tr>
		///      <tr><th>3</th><td> </td><td> </td><td> </td><td>400</td></tr>
		///    </table>
		///
		/// If the formula "=1000+A1:B1+D2:D3" is put into the 9 cells from A2 to C4, the spReadsheet
		/// will look like this:
		///
		///    <table border="1" cellpAdding="1" cellspacing="1" summary="sample spReadsheet">
		///      <tr><th> </th><th> A </th><th> B </th><th> C </th><th> D </th></tr>
		///      <tr><th>1</th><td>15</td><td>20</td><td>25</td><td> </td></tr>
		///      <tr><th>2</th><td>1215</td><td>1220</td><td>#VALUE!</td><td>200</td></tr>
		///      <tr><th>3</th><td>1315</td><td>1320</td><td>#VALUE!</td><td>300</td></tr>
		///      <tr><th>4</th><td>#VALUE!</td><td>#VALUE!</td><td>#VALUE!</td><td>400</td></tr>
		///    </table>
		///
		/// Note that the row area (A1:B1) does not include column C and the column area (D2:D3) does 
		/// not include row 4, so the values in C1(=25) and D4(=400) are not accessible to the formula
		/// as written, but in the 4 cells A2:B3, the row and column selection works ok.<p />
		///
		/// The same concept is extended to references across sheets, such that even multi-row, 
		/// multi-column areas can be useful.<p />
		///
		/// Of course with carefully (or carelessly) chosen parameters, cyclic references can occur and
		/// hence this method <b>can</b> throw a 'circular reference' EvaluationException.  Note that 
		/// this method does not attempt to detect cycles.  Every cell in the specified Area <c>ae</c>
		/// has already been Evaluated prior to this method call.  Any cell (or cell<b>s</b>) part of 
		/// <c>ae</c> that would incur a cyclic reference error if selected by this method, will 
		/// already have the value <c>ErrorEval.CIRCULAR_REF_ERROR</c> upon entry to this method.  It
		/// is assumed logic exists elsewhere to produce this behaviour.
		///
		/// @return whatever the selected cell's Evaluated value Is.  Never <c>null</c>. Never
		///  <c>ErrorEval</c>.
		/// @if there is a problem with indexing into the area, or if the
		///  Evaluated cell has an error.
		public static ValueEval ChooseSingleElementFromArea(AreaEval ae, int srcCellRow, int srcCellCol)
		{
			ValueEval valueEval = ChooseSingleElementFromAreaInternal(ae, srcCellRow, srcCellCol);
			if (valueEval is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)valueEval);
			}
			return valueEval;
		}

		/// @return possibly  <c>ErrorEval</c>, and <c>null</c> 
		private static ValueEval ChooseSingleElementFromAreaInternal(AreaEval ae, int srcCellRow, int srcCellCol)
		{
			if (ae.IsColumn)
			{
				if (ae.IsRow)
				{
					return ae.GetRelativeValue(0, 0);
				}
				if (!ae.ContainsRow(srcCellRow))
				{
					throw EvaluationException.InvalidValue();
				}
				return ae.GetAbsoluteValue(srcCellRow, ae.FirstColumn);
			}
			if (!ae.IsRow)
			{
				if (ae.ContainsRow(srcCellRow) && ae.ContainsColumn(srcCellCol))
				{
					return ae.GetAbsoluteValue(ae.FirstRow, ae.FirstColumn);
				}
				throw EvaluationException.InvalidValue();
			}
			if (!ae.ContainsColumn(srcCellCol))
			{
				throw EvaluationException.InvalidValue();
			}
			return ae.GetAbsoluteValue(ae.FirstRow, srcCellCol);
		}

		/// Applies some conversion rules if the supplied value is not already an integer.<br />
		/// Value is first Coerced to a <c>double</c> ( See <c>CoerceValueTodouble()</c> ).<p />
		///
		/// Excel typically Converts doubles to integers by truncating toward negative infinity.<br />
		/// The equivalent java code Is:<br />
		///  <c>return (int)Math.floor(d);</c><br />
		/// <b>not</b>:<br />
		///  <c>return (int)d; // wrong - rounds toward zero</c> 
		public static int CoerceValueToInt(ValueEval ev)
		{
			if (ev == BlankEval.instance)
			{
				return 0;
			}
			double d = CoerceValueToDouble(ev);
			return (int)Math.Floor(d);
		}

		/// Applies some conversion rules if the supplied value is not already a number.
		/// Note - <c>BlankEval</c> is not supported and must be handled by the caller. 
		/// @param ev must be a <c>NumberEval</c>, <c>StringEval</c> or <c>BoolEval</c>
		/// @return actual, Parsed or interpreted double value (respectively).
		/// @throws EvaluationException(#VALUE!) only if a StringEval is supplied and cannot be Parsed
		/// as a double (See <c>Parsedouble()</c> for allowable formats).
		/// @throws Exception if the supplied parameter is not <c>NumberEval</c>,
		///  <c>StringEval</c> or <c>BoolEval</c>
		public static double CoerceValueToDouble(ValueEval ev)
		{
			if (ev == BlankEval.instance)
			{
				return 0.0;
			}
			if (ev is NumericValueEval)
			{
				return ((NumericValueEval)ev).NumberValue;
			}
			if (ev is StringEval)
			{
				double num = ParseDouble(((StringEval)ev).StringValue);
				if (double.IsNaN(num))
				{
					throw EvaluationException.InvalidValue();
				}
				return num;
			}
			throw new Exception("Unexpected arg eval type (" + ev.GetType().Name + ")");
		}

		/// Converts a string to a double using standard rules that Excel would use.<br />
		/// Tolerates currency prefixes, commas, leading and trailing spaces.<p />
		///
		///  Some examples:<br /> 
		///  " 123 " -&gt; 123.0<br />
		///  ".123" -&gt; 0.123<br />
		///  These not supported yet:<br />
		///  " $ 1,000.00 " -&gt; 1000.0<br />
		///  "$1.25E4" -&gt; 12500.0<br />
		///  "5**2" -&gt; 500<br />
		///  "250%" -&gt; 2.5<br />
		///
		/// @param text
		/// @return <c>null</c> if the specified text cannot be Parsed as a number
		public static double ParseDouble(string pText)
		{
			try
			{
				double num = double.Parse(pText, CultureInfo.CurrentCulture);
				if (double.IsInfinity(num))
				{
					return double.NaN;
				}
				return num;
			}
			catch (Exception)
			{
				return double.NaN;
			}
		}

		/// @param ve must be a <c>NumberEval</c>, <c>StringEval</c>, <c>BoolEval</c>, or <c>BlankEval</c>
		/// @return the Converted string value. never <c>null</c>
		public static string CoerceValueToString(ValueEval ve)
		{
			if (ve is StringValueEval)
			{
				StringValueEval stringValueEval = (StringValueEval)ve;
				return stringValueEval.StringValue;
			}
			if (ve is BlankEval)
			{
				return "";
			}
			throw new ArgumentException("Unexpected eval class (" + ve.GetType().Name + ")");
		}

		/// @return <c>null</c> to represent blank values
		/// @throws EvaluationException if ve is an ErrorEval, or if a string value cannot be converted
		public static bool? CoerceValueToBoolean(ValueEval ve, bool stringsAreBlanks)
		{
			if (ve == null || ve == BlankEval.instance)
			{
				return null;
			}
			if (ve is BoolEval)
			{
				return ((BoolEval)ve).BooleanValue;
			}
			if (ve is StringEval)
			{
				if (stringsAreBlanks)
				{
					return null;
				}
				string stringValue = ((StringEval)ve).StringValue;
				if (stringValue.Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				if (stringValue.Equals("false", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			if (ve is NumericValueEval)
			{
				NumericValueEval numericValueEval = (NumericValueEval)ve;
				double numberValue = numericValueEval.NumberValue;
				if (double.IsNaN(numberValue))
				{
					throw new EvaluationException(ErrorEval.VALUE_INVALID);
				}
				return numberValue != 0.0;
			}
			if (ve is ErrorEval)
			{
				throw new EvaluationException((ErrorEval)ve);
			}
			throw new InvalidOperationException("Unexpected eval (" + ve.GetType().Name + ")");
		}
	}
}
