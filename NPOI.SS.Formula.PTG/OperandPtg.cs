using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// @author Josh Micich
	[Serializable]
	public abstract class OperandPtg : Ptg
	{
		/// All Operand <c>Ptg</c>s are classifed ('relative', 'value', 'array')  
		public override bool IsBaseToken => false;

		public OperandPtg Copy()
		{
			try
			{
				return (OperandPtg)Clone();
			}
			catch (NotSupportedException e)
			{
				throw new RuntimeException(e);
			}
		}
	}
}
