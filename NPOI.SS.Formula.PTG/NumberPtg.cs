using NPOI.SS.Util;
using NPOI.Util;
using System.Globalization;

namespace NPOI.SS.Formula.PTG
{
	/// Number
	/// Stores a floating point value in a formula
	/// value stored in a 8 byte field using IEEE notation
	/// @author  Avik Sengupta
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class NumberPtg : ScalarConstantPtg
	{
		public const int SIZE = 9;

		public const byte sid = 31;

		private double field_1_value;

		public double Value => field_1_value;

		public override int Size => 9;

		/// Create a NumberPtg from a byte array Read from disk 
		public NumberPtg(ILittleEndianInput in1)
		{
			field_1_value = in1.ReadDouble();
		}

		/// Create a NumberPtg from a string representation of  the number
		/// Number format is not checked, it is expected to be validated in the parser
		///  that calls this method. 
		/// @param value : String representation of a floating point number
		public NumberPtg(string value)
			: this(double.Parse(value, CultureInfo.InvariantCulture))
		{
		}

		public NumberPtg(double value)
		{
			field_1_value = value;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(31 + base.PtgClass);
			out1.WriteDouble(Value);
		}

		public override string ToFormulaString()
		{
			return NumberToTextConverter.ToText(Value);
		}
	}
}
