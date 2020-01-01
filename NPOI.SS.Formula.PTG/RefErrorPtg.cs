using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// RefError - handles deleted cell reference
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class RefErrorPtg : OperandPtg
	{
		private const int SIZE = 5;

		public const byte sid = 42;

		private int field_1_reserved;

		public int Reserved
		{
			get
			{
				return field_1_reserved;
			}
			set
			{
				field_1_reserved = value;
			}
		}

		public override int Size => 5;

		public override byte DefaultOperandClass => 0;

		public RefErrorPtg()
		{
			field_1_reserved = 0;
		}

		public RefErrorPtg(ILittleEndianInput in1)
		{
			field_1_reserved = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[RefError]\n");
			stringBuilder.Append("reserved = ").Append(Reserved).Append("\n");
			return stringBuilder.ToString();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(42 + base.PtgClass);
			out1.WriteInt(field_1_reserved);
		}

		public override string ToFormulaString()
		{
			return "#REF!";
		}
	}
}
