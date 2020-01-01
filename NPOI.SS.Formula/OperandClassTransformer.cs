using NPOI.SS.Formula.PTG;
using System;

namespace NPOI.SS.Formula
{
	/// This class performs 'operand class' transformation. Non-base Tokens are classified into three 
	/// operand classes:
	/// <ul>
	/// <li>reference</li> 
	/// <li>value</li> 
	/// <li>array</li> 
	/// </ul>
	/// <p />
	///
	/// The operand class chosen for each Token depends on the formula type and the Token's place
	/// in the formula. If POI Gets the operand class wrong, Excel <em>may</em> interpret the formula
	/// incorrectly.  This condition is typically manifested as a formula cell that displays as '#VALUE!',
	/// but resolves correctly when the user presses F2, enter.<p />
	///
	/// The logic implemented here was partially inspired by the description in
	/// "OpenOffice.org's Documentation of the Microsoft Excel File Format".  The model presented there
	/// seems To be inconsistent with observed Excel behaviour (These differences have not been fully
	/// investigated). The implementation in this class Has been heavily modified in order To satisfy
	/// concrete examples of how Excel performs the same logic (see TestRVA).<p />
	///
	/// Hopefully, as Additional important test cases are identified and Added To the test suite, 
	/// patterns might become more obvious in this code and allow for simplification.
	///
	/// @author Josh Micich
	internal class OperandClassTransformer
	{
		private FormulaType _formulaType;

		public OperandClassTransformer(FormulaType formulaType)
		{
			_formulaType = formulaType;
		}

		/// Traverses the supplied formula parse tree, calling <c>Ptg.SetClass()</c> for each non-base
		/// Token To Set its operand class.
		public void TransformFormula(ParseNode rootNode)
		{
			byte desiredOperandClass;
			switch (_formulaType)
			{
			case FormulaType.Cell:
				desiredOperandClass = 32;
				break;
			case FormulaType.Array:
				desiredOperandClass = 64;
				break;
			case FormulaType.NamedRange:
			case FormulaType.DataValidationList:
				desiredOperandClass = 0;
				break;
			default:
				throw new Exception("Incomplete code - formula type (" + _formulaType + ") not supported yet");
			}
			TransformNode(rootNode, desiredOperandClass, callerForceArrayFlag: false);
		}

		/// @param callerForceArrayFlag <c>true</c> if one of the current node's parents is a 
		/// function Ptg which Has been Changed from default 'V' To 'A' type (due To requirements on
		/// the function return value).
		private void TransformNode(ParseNode node, byte desiredOperandClass, bool callerForceArrayFlag)
		{
			Ptg ptg = node.GetToken();
			ParseNode[] children = node.GetChildren();
			if (IsSimpleValueFunction(ptg))
			{
				bool callerForceArrayFlag2 = desiredOperandClass == 64;
				for (int i = 0; i < children.Length; i++)
				{
					TransformNode(children[i], desiredOperandClass, callerForceArrayFlag2);
				}
				SetSimpleValueFuncClass((AbstractFunctionPtg)ptg, desiredOperandClass, callerForceArrayFlag);
			}
			else
			{
				if (IsSingleArgSum(ptg))
				{
					ptg = FuncVarPtg.SUM;
				}
				if (ptg is ValueOperatorPtg || ptg is ControlPtg || ptg is MemFuncPtg || ptg is MemAreaPtg || ptg is UnionPtg)
				{
					byte desiredOperandClass2 = (byte)((desiredOperandClass == 0) ? 32 : desiredOperandClass);
					for (int j = 0; j < children.Length; j++)
					{
						TransformNode(children[j], desiredOperandClass2, callerForceArrayFlag);
					}
				}
				else if (ptg is AbstractFunctionPtg)
				{
					TransformFunctionNode((AbstractFunctionPtg)ptg, children, desiredOperandClass, callerForceArrayFlag);
				}
				else if (children.Length > 0)
				{
					if (!(ptg is OperationPtg))
					{
						throw new InvalidOperationException("Node should not have any children");
					}
				}
				else if (!ptg.IsBaseToken)
				{
					ptg.PtgClass = TransformClass(ptg.PtgClass, desiredOperandClass, callerForceArrayFlag);
				}
			}
		}

		private static bool IsSingleArgSum(Ptg token)
		{
			if (token is AttrPtg)
			{
				AttrPtg attrPtg = (AttrPtg)token;
				return attrPtg.IsSum;
			}
			return false;
		}

		private static bool IsSimpleValueFunction(Ptg token)
		{
			if (token is AbstractFunctionPtg)
			{
				AbstractFunctionPtg abstractFunctionPtg = (AbstractFunctionPtg)token;
				if (abstractFunctionPtg.DefaultOperandClass != 32)
				{
					return false;
				}
				int numberOfOperands = abstractFunctionPtg.NumberOfOperands;
				for (int num = numberOfOperands - 1; num >= 0; num--)
				{
					if (abstractFunctionPtg.GetParameterClass(num) != 32)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		private byte TransformClass(byte currentOperandClass, byte desiredOperandClass, bool callerForceArrayFlag)
		{
			switch (desiredOperandClass)
			{
			case 32:
				if (!callerForceArrayFlag)
				{
					return 32;
				}
				return 64;
			case 64:
				return 64;
			case 0:
				if (!callerForceArrayFlag)
				{
					return currentOperandClass;
				}
				return 0;
			default:
				throw new InvalidOperationException("Unexpected operand class (" + desiredOperandClass + ")");
			}
		}

		private void TransformFunctionNode(AbstractFunctionPtg afp, ParseNode[] children, byte desiredOperandClass, bool callerForceArrayFlag)
		{
			byte defaultOperandClass = afp.DefaultOperandClass;
			bool callerForceArrayFlag2;
			if (callerForceArrayFlag)
			{
				switch (defaultOperandClass)
				{
				case 0:
					if (desiredOperandClass == 0)
					{
						afp.PtgClass = 0;
					}
					else
					{
						afp.PtgClass = 64;
					}
					callerForceArrayFlag2 = false;
					break;
				case 64:
					afp.PtgClass = 64;
					callerForceArrayFlag2 = false;
					break;
				case 32:
					afp.PtgClass = 64;
					callerForceArrayFlag2 = true;
					break;
				default:
					throw new InvalidOperationException("Unexpected operand class (" + defaultOperandClass + ")");
				}
			}
			else if (defaultOperandClass != desiredOperandClass)
			{
				switch (desiredOperandClass)
				{
				case 32:
					afp.PtgClass = 32;
					callerForceArrayFlag2 = false;
					break;
				case 64:
					switch (defaultOperandClass)
					{
					case 0:
						afp.PtgClass = 0;
						break;
					case 32:
						afp.PtgClass = 64;
						break;
					default:
						throw new InvalidOperationException("Unexpected operand class (" + defaultOperandClass + ")");
					}
					callerForceArrayFlag2 = (defaultOperandClass == 32);
					break;
				case 0:
					switch (defaultOperandClass)
					{
					case 64:
						afp.PtgClass = 64;
						break;
					case 32:
						afp.PtgClass = 32;
						break;
					default:
						throw new InvalidOperationException("Unexpected operand class (" + defaultOperandClass + ")");
					}
					callerForceArrayFlag2 = false;
					break;
				default:
					throw new InvalidOperationException("Unexpected operand class (" + desiredOperandClass + ")");
				}
			}
			else
			{
				callerForceArrayFlag2 = false;
				afp.PtgClass = defaultOperandClass;
			}
			for (int i = 0; i < children.Length; i++)
			{
				ParseNode node = children[i];
				byte parameterClass = afp.GetParameterClass(i);
				TransformNode(node, parameterClass, callerForceArrayFlag2);
			}
		}

		private void SetSimpleValueFuncClass(AbstractFunctionPtg afp, byte desiredOperandClass, bool callerForceArrayFlag)
		{
			if (callerForceArrayFlag || desiredOperandClass == 64)
			{
				afp.PtgClass = 64;
			}
			else
			{
				afp.PtgClass = 32;
			}
		}
	}
}
