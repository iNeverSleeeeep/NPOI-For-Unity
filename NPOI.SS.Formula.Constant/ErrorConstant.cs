using NPOI.HSSF.UserModel;
using System;
using System.Text;

namespace NPOI.SS.Formula.Constant
{
	/// <summary>
	/// Represents a constant error code value as encoded in a constant values array.
	/// This class is a type-safe wrapper for a 16-bit int value performing a similar job to
	/// <c>ErrorEval</c>
	/// </summary>
	///             <remarks> @author Josh Micich</remarks>
	public class ErrorConstant
	{
		private static readonly ErrorConstant NULL = new ErrorConstant(0);

		private static readonly ErrorConstant DIV_0 = new ErrorConstant(7);

		private static readonly ErrorConstant VALUE = new ErrorConstant(15);

		private static readonly ErrorConstant REF = new ErrorConstant(23);

		private static readonly ErrorConstant NAME = new ErrorConstant(29);

		private static readonly ErrorConstant NUM = new ErrorConstant(36);

		private static readonly ErrorConstant NA = new ErrorConstant(42);

		private int _errorCode;

		/// <summary>
		/// Gets the error code.
		/// </summary>
		/// <value>The error code.</value>
		public int ErrorCode => _errorCode;

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				if (HSSFErrorConstants.IsValidCode(_errorCode))
				{
					return HSSFErrorConstants.GetText(_errorCode);
				}
				return "unknown error code (" + _errorCode + ")";
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.SS.Formula.Constant.ErrorConstant" /> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		private ErrorConstant(int errorCode)
		{
			_errorCode = errorCode;
		}

		/// <summary>
		/// Values the of.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <returns></returns>
		public static ErrorConstant ValueOf(int errorCode)
		{
			switch (errorCode)
			{
			case 0:
				return NULL;
			case 7:
				return DIV_0;
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
				Console.Error.WriteLine("Warning - Unexpected error code (" + errorCode + ")");
				return new ErrorConstant(errorCode);
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(Text);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
