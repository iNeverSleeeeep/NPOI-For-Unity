using NPOI.HSSF.UserModel;
using System;
using System.Text;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class ErrorEval : ValueEval
	{
		private const HSSFErrorConstants EC = null;

		private const int CIRCULAR_REF_ERROR_CODE = -60;

		private const int FUNCTION_NOT_IMPLEMENTED_CODE = -30;

		/// <b>#NULL!</b>  - Intersection of two cell ranges is empty 
		public static readonly ErrorEval NULL_INTERSECTION = new ErrorEval(0);

		/// <b>#DIV/0!</b> - Division by zero 
		public static readonly ErrorEval DIV_ZERO = new ErrorEval(7);

		/// <b>#VALUE!</b> - Wrong type of operand 
		public static readonly ErrorEval VALUE_INVALID = new ErrorEval(15);

		/// <b>#REF!</b> - Illegal or deleted cell reference 
		public static readonly ErrorEval REF_INVALID = new ErrorEval(23);

		/// <b>#NAME?</b> - Wrong function or range name 
		public static readonly ErrorEval NAME_INVALID = new ErrorEval(29);

		/// <b>#NUM!</b> - Value range overflow 
		public static readonly ErrorEval NUM_ERROR = new ErrorEval(36);

		/// <b>#N/A</b> - Argument or function not available 
		public static readonly ErrorEval NA = new ErrorEval(42);

		public static readonly ErrorEval FUNCTION_NOT_IMPLEMENTED = new ErrorEval(-30);

		public static readonly ErrorEval CIRCULAR_REF_ERROR = new ErrorEval(-60);

		private int _errorCode;

		public int ErrorCode => _errorCode;

		/// Translates an Excel internal error code into the corresponding POI ErrorEval instance
		/// @param errorCode
		public static ErrorEval ValueOf(int errorCode)
		{
			switch (errorCode)
			{
			case 0:
				return NULL_INTERSECTION;
			case 7:
				return DIV_ZERO;
			case 15:
				return VALUE_INVALID;
			case 23:
				return REF_INVALID;
			case 29:
				return NAME_INVALID;
			case 36:
				return NUM_ERROR;
			case 42:
				return NA;
			case -60:
				return CIRCULAR_REF_ERROR;
			case -30:
				return FUNCTION_NOT_IMPLEMENTED;
			default:
				throw new Exception("Unexpected error code (" + errorCode + ")");
			}
		}

		/// Converts error codes to text.  Handles non-standard error codes OK.  
		/// For debug/test purposes (and for formatting error messages).
		/// @return the String representation of the specified Excel error code.
		public static string GetText(int errorCode)
		{
			if (!HSSFErrorConstants.IsValidCode(errorCode))
			{
				switch (errorCode)
				{
				case -60:
					return "~CIRCULAR~REF~";
				case -30:
					return "~FUNCTION~NOT~IMPLEMENTED~";
				default:
					return "~non~std~err(" + errorCode + ")~";
				}
			}
			return HSSFErrorConstants.GetText(errorCode);
		}

		/// @param errorCode an 8-bit value
		private ErrorEval(int errorCode)
		{
			_errorCode = errorCode;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(GetText(_errorCode));
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
