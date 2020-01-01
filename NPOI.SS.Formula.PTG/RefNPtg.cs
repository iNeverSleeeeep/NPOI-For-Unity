using NPOI.Util;

namespace NPOI.SS.Formula.PTG
{
	/// RefNPtg
	/// @author Jason Height (jheight at apache dot com)
	public class RefNPtg : Ref2DPtgBase
	{
		public const byte sid = 44;

		protected override byte Sid => 44;

		/// Creates new ValueReferencePtg 
		public RefNPtg(ILittleEndianInput in1)
			: base(in1)
		{
		}
	}
}
