using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// Specifies a rectangular area of cells A1:A4 for instance.
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class AreaNPtg : Area2DPtgBase
	{
		public const short sid = 45;

		protected override byte Sid => 45;

		public AreaNPtg(ILittleEndianInput in1)
			: base(in1)
		{
		}
	}
}
