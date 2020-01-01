using NPOI.HSSF.Record;
using NPOI.SS.Util;
using NPOI.Util;
using System;

namespace NPOI.SS.Formula.PTG
{
	/// Specifies a rectangular area of cells A1:A4 for instance.
	/// @author  andy
	/// @author Jason Height (jheight at chariot dot net dot au)
	[Serializable]
	public abstract class AreaPtgBase : OperandPtg, AreaI
	{
		/// zero based, Unsigned 16 bit 
		private int field_1_first_row;

		/// zero based, Unsigned 16 bit 
		private int field_2_last_row;

		/// zero based, Unsigned 8 bit 
		private int field_3_first_column;

		/// zero based, Unsigned 8 bit 
		private int field_4_last_column;

		private static BitField rowRelative = BitFieldFactory.GetInstance(32768);

		private static BitField colRelative = BitFieldFactory.GetInstance(16384);

		private static BitField columnMask = BitFieldFactory.GetInstance(16383);

		/// @return the first row in the area
		public virtual int FirstRow
		{
			get
			{
				return field_1_first_row;
			}
			set
			{
				field_1_first_row = value;
			}
		}

		/// @return last row in the range (x2 in x1,y1-x2,y2)
		public virtual int LastRow
		{
			get
			{
				return field_2_last_row;
			}
			set
			{
				field_2_last_row = value;
			}
		}

		/// @return the first column number in the area.
		public virtual int FirstColumn
		{
			get
			{
				return columnMask.GetValue(field_3_first_column);
			}
			set
			{
				field_3_first_column = columnMask.SetValue(field_3_first_column, value);
			}
		}

		/// @return whether or not the first row is a relative reference or not.
		public virtual bool IsFirstRowRelative
		{
			get
			{
				return rowRelative.IsSet(field_3_first_column);
			}
			set
			{
				field_3_first_column = rowRelative.SetBoolean(field_3_first_column, value);
			}
		}

		/// @return Isrelative first column to relative or not
		public virtual bool IsFirstColRelative
		{
			get
			{
				return colRelative.IsSet(field_3_first_column);
			}
			set
			{
				field_3_first_column = colRelative.SetBoolean(field_3_first_column, value);
			}
		}

		/// @return lastcolumn in the area
		public virtual int LastColumn
		{
			get
			{
				return columnMask.GetValue(field_4_last_column);
			}
			set
			{
				field_4_last_column = columnMask.SetValue(field_4_last_column, value);
			}
		}

		/// @return last column and bitmask (the raw field)
		public virtual short LastColumnRaw => (short)field_4_last_column;

		/// @return last row relative or not
		public virtual bool IsLastRowRelative
		{
			get
			{
				return rowRelative.IsSet(field_4_last_column);
			}
			set
			{
				field_4_last_column = rowRelative.SetBoolean(field_4_last_column, value);
			}
		}

		/// @return lastcol relative or not
		public virtual bool IsLastColRelative
		{
			get
			{
				return colRelative.IsSet(field_4_last_column);
			}
			set
			{
				field_4_last_column = colRelative.SetBoolean(field_4_last_column, value);
			}
		}

		public override byte DefaultOperandClass => 0;

		/// TODO - (May-2008) fix subclasses of AreaPtg 'AreaN~' which are used in shared formulas.
		/// see similar comment in ReferencePtg
		protected Exception NotImplemented()
		{
			return new NotImplementedException("Coding Error: This method should never be called. This ptg should be Converted");
		}

		protected AreaPtgBase()
		{
		}

		protected AreaPtgBase(string arearef)
			: this(new AreaReference(arearef))
		{
		}

		protected AreaPtgBase(AreaReference ar)
		{
			CellReference firstCell = ar.FirstCell;
			CellReference lastCell = ar.LastCell;
			FirstRow = firstCell.Row;
			FirstColumn = ((firstCell.Col != -1) ? firstCell.Col : 0);
			LastRow = lastCell.Row;
			LastColumn = ((lastCell.Col == -1) ? 255 : lastCell.Col);
			IsFirstColRelative = !firstCell.IsColAbsolute;
			IsLastColRelative = !lastCell.IsColAbsolute;
			IsFirstRowRelative = !firstCell.IsRowAbsolute;
			IsLastRowRelative = !lastCell.IsRowAbsolute;
		}

		protected AreaPtgBase(int firstRow, int lastRow, int firstColumn, int lastColumn, bool firstRowRelative, bool lastRowRelative, bool firstColRelative, bool lastColRelative)
		{
			if (lastRow > firstRow)
			{
				FirstRow = firstRow;
				LastRow = lastRow;
				IsFirstRowRelative = firstRowRelative;
				IsLastRowRelative = lastRowRelative;
			}
			else
			{
				FirstRow = lastRow;
				LastRow = firstRow;
				IsFirstRowRelative = lastRowRelative;
				IsLastRowRelative = firstRowRelative;
			}
			if (lastColumn > firstColumn)
			{
				FirstColumn = firstColumn;
				LastColumn = lastColumn;
				IsFirstColRelative = firstColRelative;
				IsLastColRelative = lastColRelative;
			}
			else
			{
				FirstColumn = lastColumn;
				LastColumn = firstColumn;
				IsFirstColRelative = lastColRelative;
				IsLastColRelative = firstColRelative;
			}
		}

		protected void ReadCoordinates(ILittleEndianInput in1)
		{
			field_1_first_row = in1.ReadUShort();
			field_2_last_row = in1.ReadUShort();
			field_3_first_column = in1.ReadUShort();
			field_4_last_column = in1.ReadUShort();
		}

		protected void WriteCoordinates(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_first_row);
			out1.WriteShort(field_2_last_row);
			out1.WriteShort(field_3_first_column);
			out1.WriteShort(field_4_last_column);
		}

		protected void WriteCoordinates(byte[] array, int offset)
		{
			LittleEndian.PutUShort(array, offset, field_1_first_row);
			LittleEndian.PutUShort(array, offset + 2, field_2_last_row);
			LittleEndian.PutUShort(array, offset + 4, field_3_first_column);
			LittleEndian.PutUShort(array, offset + 6, field_4_last_column);
		}

		protected AreaPtgBase(RecordInputStream in1)
		{
			field_1_first_row = in1.ReadUShort();
			field_2_last_row = in1.ReadUShort();
			field_3_first_column = in1.ReadUShort();
			field_4_last_column = in1.ReadUShort();
		}

		/// Set the last column irrespective of the bitmasks
		public void SetLastColumnRaw(short column)
		{
			field_4_last_column = column;
		}

		public override string ToFormulaString()
		{
			return FormatReferenceAsString();
		}

		protected string FormatReferenceAsString()
		{
			CellReference cellReference = new CellReference(FirstRow, FirstColumn, !IsFirstRowRelative, !IsFirstColRelative);
			CellReference cellReference2 = new CellReference(LastRow, LastColumn, !IsLastRowRelative, !IsLastColRelative);
			if (AreaReference.IsWholeColumnReference(cellReference, cellReference2))
			{
				return new AreaReference(cellReference, cellReference2).FormatAsString();
			}
			return cellReference.FormatAsString() + ":" + cellReference2.FormatAsString();
		}
	}
}
