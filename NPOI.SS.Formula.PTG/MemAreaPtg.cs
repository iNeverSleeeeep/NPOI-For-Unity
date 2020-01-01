using NPOI.Util;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class MemAreaPtg : OperandPtg
	{
		public const short sid = 38;

		private const int SIZE = 7;

		private int field_1_reserved;

		private int field_2_subex_len;

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

		public int LenRefSubexpression
		{
			get
			{
				return field_2_subex_len;
			}
			set
			{
				field_2_subex_len = value;
			}
		}

		public override int Size => 7;

		public override byte DefaultOperandClass => 32;

		/// Creates new MemAreaPtg 
		public MemAreaPtg(int subexLen)
		{
			field_1_reserved = 0;
			field_2_subex_len = subexLen;
		}

		public MemAreaPtg(ILittleEndianInput in1)
		{
			field_1_reserved = in1.ReadInt();
			field_2_subex_len = in1.ReadShort();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(38 + base.PtgClass);
			out1.WriteInt(field_1_reserved);
			out1.WriteShort(field_2_subex_len);
		}

		public override string ToFormulaString()
		{
			return "";
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [len=");
			stringBuilder.Append(field_2_subex_len);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
