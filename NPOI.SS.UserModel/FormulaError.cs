using System;

namespace NPOI.SS.UserModel
{
	/// Enumerates error values in SpreadsheetML formula calculations.
	///
	/// @author Yegor Kozlov
	public class FormulaError
	{
		/// Intended to indicate when two areas are required to intersect, but do not.
		/// <p>Example:
		/// In the case of SUM(B1 C1), the space between B1 and C1 is treated as the binary
		/// intersection operator, when a comma was intended. end example]
		/// </p>
		public static readonly FormulaError NULL = new FormulaError(0, "#NULL!");

		/// Intended to indicate when any number, including zero, is divided by zero.
		/// Note: However, any error code divided by zero results in that error code.
		public static readonly FormulaError DIV0 = new FormulaError(7, "#DIV/0!");

		/// Intended to indicate when an incompatible type argument is passed to a function, or
		/// an incompatible type operand is used with an operator.
		/// <p>Example:
		/// In the case of a function argument, text was expected, but a number was provided
		/// </p>
		public static readonly FormulaError VALUE = new FormulaError(15, "#VALUE!");

		/// Intended to indicate when a cell reference is invalid.
		/// <p>Example:
		/// If a formula Contains a reference to a cell, and then the row or column Containing that cell is deleted,
		/// a #REF! error results. If a worksheet does not support 20,001 columns,
		/// OFFSET(A1,0,20000) will result in a #REF! error.
		/// </p>
		public static readonly FormulaError REF = new FormulaError(23, "#REF!");

		public static readonly FormulaError NAME = new FormulaError(29, "#NAME?");

		/// Intended to indicate when an argument to a function has a compatible type, but has a
		/// value that is outside the domain over which that function is defined. (This is known as
		/// a domain error.)
		/// <p>Example:
		/// Certain calls to ASIN, ATANH, FACT, and SQRT might result in domain errors.
		/// </p>
		/// Intended to indicate that the result of a function cannot be represented in a value of
		/// the specified type, typically due to extreme magnitude. (This is known as a range
		/// error.)
		/// <p>Example: FACT(1000) might result in a range error. </p>
		public static readonly FormulaError NUM = new FormulaError(36, "#NUM!");

		/// Intended to indicate when a designated value is not available.
		/// <p>Example:
		/// Some functions, such as SUMX2MY2, perform a series of operations on corresponding
		/// elements in two arrays. If those arrays do not have the same number of elements, then
		/// for some elements in the longer array, there are no corresponding elements in the
		/// shorter one; that is, one or more values in the shorter array are not available.
		/// </p>
		/// This error value can be produced by calling the function NA
		public static readonly FormulaError NA = new FormulaError(42, "#N/A");

		private byte type;

		private string repr;

		/// @return numeric code of the error
		public byte Code => type;

		/// @return string representation of the error
		public string String => repr;

		private FormulaError(int type, string repr)
		{
			this.type = (byte)type;
			this.repr = repr;
		}

		public static FormulaError ForInt(byte type)
		{
			switch (type)
			{
			case 0:
				return NULL;
			case 7:
				return DIV0;
			case 15:
				return VALUE;
			case 23:
				return REF;
			case 29:
				return NAME;
			case 36:
				return NUM;
			case 42:
				return NA;
			default:
				throw new ArgumentException("Unknown error type: " + type);
			}
		}

		public static FormulaError ForString(string code)
		{
			switch (code)
			{
			case "#NULL!":
				return NULL;
			case "#DIV/0!":
				return DIV0;
			case "#VALUE!":
				return VALUE;
			case "#REF!":
				return REF;
			case "#NAME?":
				return NAME;
			case "#NUM!":
				return NUM;
			case "#N/A":
				return NA;
			default:
				throw new ArgumentException("Unknown error code: " + code);
			}
		}
	}
}
