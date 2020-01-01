using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Contains raw Excel error codes (as defined in OOO's excelfileformat.pdf (2.5.6)
	/// @author  Michael Harhen
	/// </summary>
	public class HSSFErrorConstants
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

		private HSSFErrorConstants()
		{
		}

		/// <summary>
		/// Gets standard Excel error literal for the specified error code.
		/// @throws ArgumentException if the specified error code is not one of the 7
		/// standard error codes
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Determines whether [is valid code] [the specified error code].
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <returns>
		/// 	<c>true</c> if the specified error code is a standard Excel error code.; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidCode(int errorCode)
		{
			if (errorCode == 0 || errorCode == 7 || errorCode == 15 || errorCode == 23 || errorCode == 29 || errorCode == 36 || errorCode == 42)
			{
				return true;
			}
			return false;
		}
	}
}
