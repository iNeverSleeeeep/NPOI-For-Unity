using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @author Daniel Noll (daniel at nuix dot com dot au)
	public class MemErrPtg : MemAreaPtg
	{
		public new const short sid = 39;

		private int field_1_reserved;

		private short field_2_subex_len;

		/// Creates new MemErrPtg 
		public MemErrPtg(ILittleEndianInput in1)
			: base(in1)
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(39 + base.PtgClass);
			out1.WriteInt(field_1_reserved);
			out1.WriteShort(field_2_subex_len);
		}

		public override string ToFormulaString()
		{
			return "ERR#";
		}
	}
}
