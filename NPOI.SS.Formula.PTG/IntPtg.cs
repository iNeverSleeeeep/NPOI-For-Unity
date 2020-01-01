using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Integer (unsigned short integer)
	/// Stores an Unsigned short value (java int) in a formula
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class IntPtg : ScalarConstantPtg
	{
		private const int MIN_VALUE = 0;

		private const int MAX_VALUE = 65535;

		public const int SIZE = 3;

		public const byte sid = 30;

		private int field_1_value;

		public int Value => field_1_value;

		public override int Size => 3;

		/// Excel represents integers 0..65535 with the tInt token. 
		/// @return <c>true</c> if the specified value is within the range of values 
		/// <c>IntPtg</c> can represent. 
		public static bool IsInRange(int i)
		{
			if (i >= 0)
			{
				return i <= 65535;
			}
			return false;
		}

		public IntPtg(ILittleEndianInput in1)
			: this(in1.ReadUShort())
		{
		}

		public IntPtg(int value)
		{
			if (!IsInRange(value))
			{
				throw new ArgumentException("value is out of range: " + value);
			}
			field_1_value = value;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(30 + base.PtgClass);
			out1.WriteShort(Value);
		}

		public override string ToFormulaString()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(field_1_value);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
