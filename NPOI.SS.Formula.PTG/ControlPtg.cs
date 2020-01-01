using System;

namespace NPOI.SS.Formula.PTG
{
	/// Common baseclass for 
	/// tExp
	/// tTbl
	/// tParen
	/// tNlr
	/// tAttr
	/// tSheet
	/// tEndSheet
	public abstract class ControlPtg : Ptg
	{
		public override bool IsBaseToken => true;

		public override byte DefaultOperandClass
		{
			get
			{
				throw new NotImplementedException("Control tokens are not classified");
			}
		}
	}
}
