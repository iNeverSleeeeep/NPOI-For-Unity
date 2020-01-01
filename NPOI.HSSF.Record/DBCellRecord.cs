using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        DBCell Record
	/// Description:  Used by Excel and other MS apps to quickly Find rows in the sheets.
	/// REFERENCE:  PG 299/440 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height
	/// @version 2.0-pre
	public class DBCellRecord : StandardRecord
	{
		public const int BLOCK_SIZE = 32;

		public const short sid = 215;

		private int field_1_row_offset;

		private short[] field_2_cell_offsets;

		/// Gets offset from the start of this DBCellRecord to the start of the first cell in
		/// the next DBCell block.
		///
		/// @return rowoffset to the start of the first cell in the next DBCell block
		public int RowOffset
		{
			get
			{
				return field_1_row_offset;
			}
			set
			{
				field_1_row_offset = value;
			}
		}

		/// Get the number of cell offsets in the celloffset array
		///
		/// @return number of cell offsets
		public int NumCellOffsets => field_2_cell_offsets.Length;

		protected override int DataSize => 4 + field_2_cell_offsets.Length * 2;

		public override short Sid => 215;

		public DBCellRecord()
		{
			field_2_cell_offsets = new short[0];
		}

		/// Constructs a DBCellRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public DBCellRecord(RecordInputStream in1)
		{
			field_1_row_offset = in1.ReadUShort();
			int remaining = in1.Remaining;
			field_2_cell_offsets = new short[remaining / 2];
			for (int i = 0; i < field_2_cell_offsets.Length; i++)
			{
				field_2_cell_offsets[i] = in1.ReadShort();
			}
		}

		/// offset from the start of this DBCellRecord to the start of the first cell in
		/// the next DBCell block.
		public DBCellRecord(int rowOffset, short[] cellOffsets)
		{
			field_1_row_offset = rowOffset;
			field_2_cell_offsets = cellOffsets;
		}

		public void AddCellOffset(short offset)
		{
			if (field_2_cell_offsets == null)
			{
				field_2_cell_offsets = new short[1];
			}
			else
			{
				short[] destinationArray = new short[field_2_cell_offsets.Length + 1];
				Array.Copy(field_2_cell_offsets, 0, destinationArray, 0, field_2_cell_offsets.Length);
				field_2_cell_offsets = destinationArray;
			}
			field_2_cell_offsets[field_2_cell_offsets.Length - 1] = offset;
		}

		/// return the cell offset in the array
		///
		/// @param index of the cell offset to retrieve
		/// @return celloffset from the celloffset array
		public short GetCellOffsetAt(int index)
		{
			return field_2_cell_offsets[index];
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DBCELL]\n");
			stringBuilder.Append("    .rowoffset       = ").Append(StringUtil.ToHexString(RowOffset)).Append("\n");
			for (int i = 0; i < field_2_cell_offsets.Length; i++)
			{
				stringBuilder.Append("    .cell_").Append(i).Append(" = ")
					.Append(HexDump.ShortToHex(field_2_cell_offsets[i]))
					.Append("\n");
			}
			stringBuilder.Append("[/DBCELL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_row_offset);
			for (int i = 0; i < field_2_cell_offsets.Length; i++)
			{
				out1.WriteShort(field_2_cell_offsets[i]);
			}
		}

		/// @returns the size of the Group of <c>DBCellRecord</c>s needed to encode
		/// the specified number of blocks and rows
		public static int CalculateSizeOfRecords(int nBlocks, int nRows)
		{
			return nBlocks * 8 + nRows * 2;
		}

		public override object Clone()
		{
			return this;
		}
	}
}
