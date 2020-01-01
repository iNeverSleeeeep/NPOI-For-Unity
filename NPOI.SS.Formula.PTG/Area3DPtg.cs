using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// Title:        Area 3D Ptg - 3D reference (Sheet + Area)
	/// Description:  Defined a area in Extern Sheet. 
	/// REFERENCE:  
	/// @author Libin Roman (Vista Portal LDT. Developer)
	/// @author avik
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 1.0-pre
	[Serializable]
	public class Area3DPtg : AreaPtgBase, WorkbookDependentFormula, IExternSheetReferenceToken
	{
		public const byte sid = 59;

		private const int SIZE = 11;

		private int field_1_index_extern_sheet;

		private BitField rowRelative = BitFieldFactory.GetInstance(32768);

		private BitField colRelative = BitFieldFactory.GetInstance(16384);

		public override int Size => 11;

		public int ExternSheetIndex
		{
			get
			{
				return field_1_index_extern_sheet;
			}
			set
			{
				field_1_index_extern_sheet = value;
			}
		}

		public override byte DefaultOperandClass => 0;

		public Area3DPtg(string arearef, int externIdx)
			: base(arearef)
		{
			ExternSheetIndex = externIdx;
		}

		public Area3DPtg(AreaReference arearef, int externIdx)
			: base(arearef)
		{
			ExternSheetIndex = externIdx;
		}

		public Area3DPtg(ILittleEndianInput in1)
		{
			field_1_index_extern_sheet = in1.ReadShort();
			ReadCoordinates(in1);
		}

		public Area3DPtg(int firstRow, int lastRow, int firstColumn, int lastColumn, bool firstRowRelative, bool lastRowRelative, bool firstColRelative, bool lastColRelative, int externalSheetIndex)
			: base(firstRow, lastRow, firstColumn, lastColumn, firstRowRelative, lastRowRelative, firstColRelative, lastColRelative)
		{
			ExternSheetIndex = externalSheetIndex;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append(" [");
			stringBuilder.Append("sheetIx=").Append(ExternSheetIndex);
			stringBuilder.Append(" ! ");
			stringBuilder.Append(FormatReferenceAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(59 + base.PtgClass);
			out1.WriteShort(field_1_index_extern_sheet);
			WriteCoordinates(out1);
		}

		public void SetArea(string ref1)
		{
			AreaReference areaReference = new AreaReference(ref1);
			CellReference firstCell = areaReference.FirstCell;
			CellReference lastCell = areaReference.LastCell;
			FirstRow = (short)firstCell.Row;
			FirstColumn = firstCell.Col;
			LastRow = (short)lastCell.Row;
			LastColumn = lastCell.Col;
			IsFirstColRelative = !firstCell.IsColAbsolute;
			IsLastColRelative = !lastCell.IsColAbsolute;
			IsFirstRowRelative = !firstCell.IsRowAbsolute;
			IsLastRowRelative = !lastCell.IsRowAbsolute;
		}

		public override string ToFormulaString()
		{
			throw new NotImplementedException("3D references need a workbook to determine formula text");
		}

		/// @return text representation of this area reference that can be used in text
		///  formulas. The sheet name will get properly delimited if required.
		public string ToFormulaString(IFormulaRenderingWorkbook book)
		{
			return ExternSheetNameResolver.PrependSheetName(book, field_1_index_extern_sheet, FormatReferenceAsString());
		}
	}
}
