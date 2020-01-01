using System;

namespace NPOI.SS.UserModel
{
	/// Contains raw Excel error codes (as defined in OOO's excelfileformat.pdf (2.5.6)
	///
	/// @author  Michael Harhen
	public class ErrorConstants
	{
		/// <b>#NULL!</b>  - Intersection of two cell ranges is empty 
		public const int ERROR_NULL = 0;

		/// <b>#DIV/0!</b> - Division by zero 
		public const int ERROR_DIV_0 = 7;

		/// <b>#VALUE!</b> - Wrong type of operand 
		public const int ERROR_VALUE = 15;

		/// <b>#REF!</b> - Illegal or deleted cell reference 
		public const int ERROR_REF = 23;

		/// <b>#NAME?</b> - Wrong function or range name 
		public const int ERROR_NAME = 29;

		/// <b>#NUM!</b> - Value range overflow 
		public const int ERROR_NUM = 36;

		/// <b>#N/A</b> - Argument or function not available 
		public const int ERROR_NA = 42;

		protected ErrorConstants()
		{
		}

		/// @return Standard Excel error literal for the specified error code. 
		/// @throws ArgumentException if the specified error code is not one of the 7 
		/// standard error codes
		public static string GetText(int errorCode)
		{
			switch (errorCode)
			{
			case 0:
				return "#NULL!";
			case 7:
				return "#DIV/0!";
			case 15:
				return "#VALUE!";
			case 23:
				return "#REF!";
			case 29:
				return "#NAME?";
			case 36:
				return "#NUM!";
			case 42:
				return "#N/A";
			default:
				throw new ArgumentException("Bad error code (" + errorCode + ")");
			}
		}

		/// @return <c>true</c> if the specified error code is a standard Excel error code. 
		public static bool IsValidCode(int errorCode)
		{
			switch (errorCode)
			{
			case 0:
			case 7:
			case 15:
			case 23:
			case 29:
			case 36:
			case 42:
				return true;
			default:
				return false;
			}
		}
	}
}
