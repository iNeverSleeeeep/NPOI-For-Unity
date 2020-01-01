using NPOI.SS.Formula.Function;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// This class provides the base functionality for Excel sheet functions
	/// There are two kinds of function Ptgs - tFunc and tFuncVar
	/// Therefore, this class will have ONLY two subclasses
	/// @author  Avik Sengupta
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	[Serializable]
	public abstract class AbstractFunctionPtg : OperationPtg
	{
		/// The name of the IF function (i.e. "IF").  Extracted as a constant for clarity.
		public const string FUNCTION_NAME_IF = "IF";

		/// All external functions have function index 255 
		private const short FUNCTION_INDEX_EXTERNAL = 255;

		protected byte returnClass;

		protected byte[] paramClass;

		protected byte _numberOfArgs;

		protected short _functionIndex;

		public override bool IsBaseToken => false;

		public override int NumberOfOperands => _numberOfArgs;

		public string Name => LookupName(_functionIndex);

		/// external functions Get some special Processing
		/// @return <c>true</c> if this is an external function
		public bool IsExternalFunction => _functionIndex == 255;

		public override byte DefaultOperandClass => returnClass;

		protected AbstractFunctionPtg(int functionIndex, int pReturnClass, byte[] paramTypes, int nParams)
		{
			_numberOfArgs = (byte)nParams;
			_functionIndex = (short)functionIndex;
			returnClass = (byte)pReturnClass;
			paramClass = paramTypes;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(LookupName(_functionIndex));
			stringBuilder.Append(" nArgs=").Append(_numberOfArgs);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public short GetFunctionIndex()
		{
			return _functionIndex;
		}

		public override string ToFormulaString()
		{
			return Name;
		}

		public override string ToFormulaString(string[] operands)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (IsExternalFunction)
			{
				stringBuilder.Append(operands[0]);
				AppendArgs(stringBuilder, 1, operands);
			}
			else
			{
				stringBuilder.Append(Name);
				AppendArgs(stringBuilder, 0, operands);
			}
			return stringBuilder.ToString();
		}

		private static void AppendArgs(StringBuilder buf, int firstArgIx, string[] operands)
		{
			buf.Append('(');
			for (int i = firstArgIx; i < operands.Length; i++)
			{
				if (i > firstArgIx)
				{
					buf.Append(',');
				}
				buf.Append(operands[i]);
			}
			buf.Append(")");
		}

		/// Used to detect whether a function name found in a formula is one of the standard excel functions
		///
		/// The name matching is case insensitive.
		/// @return <c>true</c> if the name specifies a standard worksheet function,
		///  <c>false</c> if the name should be assumed to be an external function.
		public static bool IsBuiltInFunctionName(string name)
		{
			short num = FunctionMetadataRegistry.LookupIndexByName(name.ToUpper());
			return num >= 0;
		}

		protected string LookupName(short index)
		{
			if (index == 255)
			{
				return "#external#";
			}
			FunctionMetadata functionByIndex = FunctionMetadataRegistry.GetFunctionByIndex(index);
			if (functionByIndex == null)
			{
				throw new Exception("bad function index (" + index + ")");
			}
			return functionByIndex.Name;
		}

		/// Resolves internal function names into function indexes.
		///
		/// The name matching is case insensitive.
		/// @return the standard worksheet function index if found, otherwise <c>FUNCTION_INDEX_EXTERNAL</c>
		protected static short LookupIndex(string name)
		{
			short num = FunctionMetadataRegistry.LookupIndexByName(name.ToUpper());
			if (num < 0)
			{
				return 255;
			}
			return num;
		}

		public byte GetParameterClass(int index)
		{
			if (index >= paramClass.Length)
			{
				return paramClass[paramClass.Length - 1];
			}
			return paramClass[index];
		}
	}
}
