using System;

namespace NPOI.SS.Formula.PTG
{
	/// defines a Ptg that is an operation instead of an operand
	/// @author  andy
	[Serializable]
	public abstract class OperationPtg : Ptg
	{
		public const int TYPE_UNARY = 0;

		public const int TYPE_BINARY = 1;

		public const int TYPE_FUNCTION = 2;

		/// The number of operands expected by the operations
		public abstract int NumberOfOperands
		{
			get;
		}

		public override byte DefaultOperandClass => 32;

		/// returns a string representation of the operations
		/// the Length of the input array should equal the number returned by 
		/// @see #GetNumberOfOperands
		public abstract string ToFormulaString(string[] operands);
	}
}
