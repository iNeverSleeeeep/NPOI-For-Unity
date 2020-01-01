using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// Missing Function Arguments
	///
	/// Avik Sengupta &lt;avik at apache.org&gt;
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class MissingArgPtg : ScalarConstantPtg
	{
		private const int SIZE = 1;

		public const byte sid = 22;

		public static Ptg instance = new MissingArgPtg();

		public override int Size => 1;

		private MissingArgPtg()
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(22 + base.PtgClass);
		}

		public override string ToFormulaString()
		{
			return " ";
		}
	}
}
