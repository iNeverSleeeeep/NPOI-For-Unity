using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// @author  aviks
	public class NameXPtg : OperandPtg, WorkbookDependentFormula
	{
		public const short sid = 57;

		private const int SIZE = 7;

		/// index to REF entry in externsheet record 
		private int _sheetRefIndex;

		/// index to defined name or externname table(1 based) 
		private int _nameNumber;

		/// reserved must be 0 
		private int _reserved;

		public override int Size => 7;

		public override byte DefaultOperandClass => 32;

		public int SheetRefIndex => _sheetRefIndex;

		public int NameIndex => _nameNumber - 1;

		private NameXPtg(int sheetRefIndex, int nameNumber, int reserved)
		{
			_sheetRefIndex = sheetRefIndex;
			_nameNumber = nameNumber;
			_reserved = reserved;
		}

		/// @param sheetRefIndex index to REF entry in externsheet record
		/// @param nameIndex index to defined name or externname table
		public NameXPtg(int sheetRefIndex, int nameIndex)
			: this(sheetRefIndex, nameIndex + 1, 0)
		{
		}

		public NameXPtg(ILittleEndianInput in1)
			: this(in1.ReadUShort(), in1.ReadUShort(), in1.ReadUShort())
		{
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(57 + base.PtgClass);
			out1.WriteShort(_sheetRefIndex);
			out1.WriteShort(_nameNumber);
			out1.WriteShort(_reserved);
		}

		public string ToFormulaString(IFormulaRenderingWorkbook book)
		{
			return book.ResolveNameXText(this);
		}

		public override string ToFormulaString()
		{
			throw new NotImplementedException("3D references need a workbook to determine formula text");
		}
	}
}
