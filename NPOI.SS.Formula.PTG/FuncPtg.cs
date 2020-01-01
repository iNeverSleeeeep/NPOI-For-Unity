using NPOI.SS.Formula.Function;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// @author aviks
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @author Danny Mui (dmui at apache dot org) (Leftover handling)
	[Serializable]
	public class FuncPtg : AbstractFunctionPtg
	{
		public const byte sid = 33;

		public const int SIZE = 3;

		public override int Size => 3;

		public static FuncPtg Create(ILittleEndianInput in1)
		{
			return Create(in1.ReadUShort());
		}

		private FuncPtg(int funcIndex, FunctionMetadata fm)
			: base(funcIndex, fm.ReturnClassCode, fm.ParameterClassCodes, fm.MinParams)
		{
		}

		public static FuncPtg Create(int functionIndex)
		{
			FunctionMetadata functionByIndex = FunctionMetadataRegistry.GetFunctionByIndex(functionIndex);
			if (functionByIndex == null)
			{
				throw new Exception("Invalid built-in function index (" + functionIndex + ")");
			}
			return new FuncPtg(functionIndex, functionByIndex);
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(33 + base.PtgClass);
			out1.WriteShort(_functionIndex);
		}
	}
}
