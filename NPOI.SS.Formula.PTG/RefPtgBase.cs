using NPOI.SS.Util;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// ReferencePtgBase - handles references (such as A1, A2, IA4)
	/// @author  Andrew C. Oliver (acoliver@apache.org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public abstract class RefPtgBase : OperandPtg
	{
		/// The row index - zero based Unsigned 16 bit value 
		private int field_1_row;

		/// Field 2
		/// - lower 8 bits is the zero based Unsigned byte column index
		/// - bit 16 - IsRowRelative
		/// - bit 15 - IsColumnRelative
		private int field_2_col;

		private static BitField rowRelative = BitFieldFactory.GetInstance(32768);

		private static BitField colRelative = BitFieldFactory.GetInstance(16384);

		private static BitField column = BitFieldFactory.GetInstance(16383);

		/// Returns the row number as a short, which will be
		///  wrapped (negative) for values between 32769 and 65535
		public int Row
		{
			get
			{
				return field_1_row;
			}
			set
			{
				field_1_row = value;
			}
		}

		/// Returns the row number as an int, between 0 and 65535
		public int RowAsInt => field_1_row;

		public bool IsRowRelative
		{
			get
			{
				return rowRelative.IsSet(field_2_col);
			}
			set
			{
				field_2_col = rowRelative.SetBoolean(field_2_col, value);
			}
		}

		public bool IsColRelative
		{
			get
			{
				return colRelative.IsSet(field_2_col);
			}
			set
			{
				field_2_col = colRelative.SetBoolean(field_2_col, value);
			}
		}

		public int ColumnRawX
		{
			get
			{
				return field_2_col;
			}
			set
			{
				field_2_col = value;
			}
		}

		public int Column
		{
			get
			{
				return column.GetValue(field_2_col);
			}
			set
			{
				field_2_col = column.SetValue(field_2_col, value);
			}
		}

		public override byte DefaultOperandClass => 0;

		protected RefPtgBase()
		{
		}

		/// Takes in a String representation of a cell reference and Fills out the
		/// numeric fields.
		protected RefPtgBase(string cellref)
		{
			CellReference cellReference = new CellReference(cellref);
			Row = cellReference.Row;
			Column = cellReference.Col;
			IsColRelative = !cellReference.IsColAbsolute;
			IsRowRelative = !cellReference.IsRowAbsolute;
		}

		protected RefPtgBase(CellReference c)
		{
			Row = c.Row;
			Column = c.Col;
			IsColRelative = !c.IsColAbsolute;
			IsRowRelative = !c.IsRowAbsolute;
		}

		protected RefPtgBase(int row, int column, bool isRowRelative, bool isColumnRelative)
		{
			Row = row;
			Column = column;
			IsRowRelative = isRowRelative;
			IsColRelative = isColumnRelative;
		}

		protected RefPtgBase(ILittleEndianInput in1)
		{
			field_1_row = in1.ReadUShort();
			field_2_col = in1.ReadUShort();
		}

		protected void ReadCoordinates(ILittleEndianInput in1)
		{
			field_1_row = in1.ReadUShort();
			field_2_col = in1.ReadUShort();
		}

		protected void WriteCoordinates(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_row);
			out1.WriteShort(field_2_col);
		}

		protected void WriteCoordinates(byte[] array, int offset)
		{
			LittleEndian.PutUShort(array, offset, field_1_row);
			LittleEndian.PutUShort(array, offset + 2, field_2_col);
		}

		public string FormatReferenceAsString()
		{
			CellReference cellReference = new CellReference(Row, Column, !IsRowRelative, !IsColRelative);
			return cellReference.FormatAsString();
		}
	}
}
