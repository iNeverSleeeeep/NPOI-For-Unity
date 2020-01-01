using NPOI.HSSF.UserModel;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// Title:        Deleted Reference 3D Ptg 
	/// Description:  Defined a cell in extern sheet. 
	/// REFERENCE:  
	/// @author Patrick Luby
	/// @version 1.0-pre
	public class DeletedRef3DPtg : OperandPtg, WorkbookDependentFormula
	{
		public const byte sid = 60;

		private int field_1_index_extern_sheet;

		private int unused1;

		public override byte DefaultOperandClass => 0;

		public override int Size => 7;

		/// Creates new DeletedRef3DPtg 
		public DeletedRef3DPtg(ILittleEndianInput in1)
		{
			field_1_index_extern_sheet = in1.ReadUShort();
			unused1 = in1.ReadInt();
		}

		public DeletedRef3DPtg(int externSheetIndex)
		{
			field_1_index_extern_sheet = externSheetIndex;
			unused1 = 0;
		}

		public string ToFormulaString(IFormulaRenderingWorkbook book)
		{
			return ExternSheetNameResolver.PrependSheetName(book, field_1_index_extern_sheet, HSSFErrorConstants.GetText(23));
		}

		public override string ToFormulaString()
		{
			throw new Exception("3D references need a workbook to determine formula text");
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(60 + base.PtgClass);
			out1.WriteShort(field_1_index_extern_sheet);
			out1.WriteInt(unused1);
		}
	}
}
