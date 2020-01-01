using NPOI.HSSF.UserModel;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class ErrPtg : ScalarConstantPtg
	{
		public const byte sid = 28;

		private const int SIZE = 2;

		/// <b>#NULL!</b>  - Intersection of two cell ranges is empty 
		public static readonly ErrPtg NULL_INTERSECTION = new ErrPtg(0);

		/// <b>#DIV/0!</b> - Division by zero 
		public static readonly ErrPtg DIV_ZERO = new ErrPtg(7);

		/// <b>#VALUE!</b> - Wrong type of operand 
		public static readonly ErrPtg VALUE_INVALID = new ErrPtg(15);

		/// <b>#REF!</b> - Illegal or deleted cell reference 
		public static readonly ErrPtg REF_INVALID = new ErrPtg(23);

		/// <b>#NAME?</b> - Wrong function or range name 
		public static readonly ErrPtg NAME_INVALID = new ErrPtg(29);

		/// <b>#NUM!</b> - Value range overflow 
		public static readonly ErrPtg NUM_ERROR = new ErrPtg(36);

		/// <b>#N/A</b> - Argument or function not available 
		public static readonly ErrPtg N_A = new ErrPtg(42);

		private int field_1_error_code;

		public override int Size => 2;

		public int ErrorCode => field_1_error_code;

		public static ErrPtg ValueOf(int code)
		{
			switch (code)
			{
			case 7:
				return DIV_ZERO;
			case 42:
				return N_A;
			case 29:
				return NAME_INVALID;
			case 0:
				return NULL_INTERSECTION;
			case 36:
				return NUM_ERROR;
			case 23:
				return REF_INVALID;
			case 15:
				return VALUE_INVALID;
			default:
				throw new InvalidOperationException("Unexpected error code (" + code + ")");
			}
		}

		/// Creates new ErrPtg 
		public ErrPtg(int errorCode)
		{
			if (!HSSFErrorConstants.IsValidCode(errorCode))
			{
				throw new ArgumentException("Invalid error code (" + errorCode + ")");
			}
			field_1_error_code = errorCode;
		}

		public ErrPtg(ILittleEndianInput in1)
			: this(in1.ReadByte())
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(28 + base.PtgClass);
			out1.WriteByte((byte)field_1_error_code);
		}

		public override string ToFormulaString()
		{
			return HSSFErrorConstants.GetText(field_1_error_code);
		}
	}
}
