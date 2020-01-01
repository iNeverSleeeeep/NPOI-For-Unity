using NPOI.SS.Formula.PTG;
using System;
using System.Collections;

namespace NPOI.SS.Formula
{
	/// Common logic for rendering formulas.<br />
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public class FormulaRenderer
	{
		/// Static method To convert an array of {@link Ptg}s in RPN order
		/// To a human readable string format in infix mode.
		/// @param book  used for defined names and 3D references
		/// @param ptgs  must not be <c>null</c>
		/// @return a human readable String
		public static string ToFormulaString(IFormulaRenderingWorkbook book, Ptg[] ptgs)
		{
			if (ptgs == null || ptgs.Length == 0)
			{
				throw new ArgumentException("ptgs must not be null");
			}
			Stack stack = new Stack();
			foreach (Ptg ptg in ptgs)
			{
				if (!(ptg is MemAreaPtg) && !(ptg is MemFuncPtg) && !(ptg is MemErrPtg))
				{
					if (ptg is ParenthesisPtg)
					{
						string str = (string)stack.Pop();
						stack.Push("(" + str + ")");
					}
					else if (ptg is AttrPtg)
					{
						AttrPtg attrPtg = (AttrPtg)ptg;
						if (!attrPtg.IsOptimizedIf && !attrPtg.IsOptimizedChoose && !attrPtg.IsSkip && !attrPtg.IsSpace && !attrPtg.IsSemiVolatile)
						{
							if (!attrPtg.IsSum)
							{
								throw new Exception("Unexpected tAttr: " + attrPtg.ToString());
							}
							string[] operands = GetOperands(stack, attrPtg.NumberOfOperands);
							stack.Push(attrPtg.ToFormulaString(operands));
						}
					}
					else if (ptg is WorkbookDependentFormula)
					{
						WorkbookDependentFormula workbookDependentFormula = (WorkbookDependentFormula)ptg;
						stack.Push(workbookDependentFormula.ToFormulaString(book));
					}
					else if (!(ptg is OperationPtg))
					{
						stack.Push(ptg.ToFormulaString());
					}
					else
					{
						OperationPtg operationPtg = (OperationPtg)ptg;
						string[] operands2 = GetOperands(stack, operationPtg.NumberOfOperands);
						stack.Push(operationPtg.ToFormulaString(operands2));
					}
				}
			}
			if (stack.Count == 0)
			{
				throw new InvalidOperationException("Stack underflow");
			}
			string result = (string)stack.Pop();
			if (stack.Count != 0)
			{
				throw new InvalidOperationException("too much stuff left on the stack");
			}
			return result;
		}

		private static string[] GetOperands(Stack stack, int nOperands)
		{
			string[] array = new string[nOperands];
			for (int num = nOperands - 1; num >= 0; num--)
			{
				if (stack.Count == 0)
				{
					string message = "Too few arguments supplied to operation. Expected (" + nOperands + ") operands but got (" + (nOperands - num - 1) + ")";
					throw new InvalidOperationException(message);
				}
				array[num] = (string)stack.Pop();
			}
			return array;
		}
	}
}
