using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// Common baseclass of all value operators.
	/// Subclasses include all Unary and binary operators except for the reference operators (IntersectionPtg, RangePtg, UnionPtg) 
	///
	/// @author Josh Micich
	public abstract class ValueOperatorPtg : OperationPtg
	{
		/// All Operator <c>Ptg</c>s are base tokens (i.e. are not RVA classified)  
		public override bool IsBaseToken => true;

		public override byte DefaultOperandClass => 32;

		protected abstract byte Sid
		{
			get;
		}

		public override int Size => 1;

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(Sid + base.PtgClass);
		}

		public override string ToFormulaString()
		{
			throw new NotImplementedException("ToFormulaString(String[] operands) should be used for subclasses of OperationPtgs");
		}
	}
}
