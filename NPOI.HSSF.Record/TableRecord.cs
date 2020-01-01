using NPOI.SS.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// DATATABLE (0x0236)<p />
	///
	/// TableRecord - The record specifies a data table.
	/// This record Is preceded by a single Formula record that
	///  defines the first cell in the data table, which should
	///  only contain a single Ptg, {@link TblPtg}.
	///
	/// See p536 of the June 08 binary docs
	public class TableRecord : SharedValueRecordBase
	{
		public const short sid = 566;

		private static BitField alwaysCalc = BitFieldFactory.GetInstance(1);

		private static BitField calcOnOpen = BitFieldFactory.GetInstance(2);

		private static BitField rowOrColInpCell = BitFieldFactory.GetInstance(4);

		private static BitField oneOrTwoVar = BitFieldFactory.GetInstance(8);

		private static BitField rowDeleted = BitFieldFactory.GetInstance(16);

		private static BitField colDeleted = BitFieldFactory.GetInstance(32);

		private int field_5_flags;

		private int field_6_res;

		private int field_7_rowInputRow;

		private int field_8_colInputRow;

		private int field_9_rowInputCol;

		private int field_10_colInputCol;

		public int Flags
		{
			get
			{
				return field_5_flags;
			}
			set
			{
				field_5_flags = value;
			}
		}

		public int RowInputRow
		{
			get
			{
				return field_7_rowInputRow;
			}
			set
			{
				field_7_rowInputRow = value;
			}
		}

		public int ColInputRow
		{
			get
			{
				return field_8_colInputRow;
			}
			set
			{
				field_8_colInputRow = value;
			}
		}

		public int RowInputCol
		{
			get
			{
				return field_9_rowInputCol;
			}
			set
			{
				field_9_rowInputCol = value;
			}
		}

		public int ColInputCol
		{
			get
			{
				return field_10_colInputCol;
			}
			set
			{
				field_10_colInputCol = value;
			}
		}

		public bool IsAlwaysCalc
		{
			get
			{
				return alwaysCalc.IsSet(field_5_flags);
			}
			set
			{
				field_5_flags = alwaysCalc.SetBoolean(field_5_flags, value);
			}
		}

		public bool IsRowOrColInpCell
		{
			get
			{
				return rowOrColInpCell.IsSet(field_5_flags);
			}
			set
			{
				field_5_flags = rowOrColInpCell.SetBoolean(field_5_flags, value);
			}
		}

		public bool IsOneNotTwoVar
		{
			get
			{
				return oneOrTwoVar.IsSet(field_5_flags);
			}
			set
			{
				field_5_flags = oneOrTwoVar.SetBoolean(field_5_flags, value);
			}
		}

		public bool IsColDeleted
		{
			get
			{
				return colDeleted.IsSet(field_5_flags);
			}
			set
			{
				field_5_flags = colDeleted.SetBoolean(field_5_flags, value);
			}
		}

		public bool IsRowDeleted
		{
			get
			{
				return rowDeleted.IsSet(field_5_flags);
			}
			set
			{
				field_5_flags = rowDeleted.SetBoolean(field_5_flags, value);
			}
		}

		public override short Sid => 566;

		protected override int ExtraDataSize => 10;

		public TableRecord(RecordInputStream in1)
			: base(in1)
		{
			field_5_flags = in1.ReadByte();
			field_6_res = in1.ReadByte();
			field_7_rowInputRow = in1.ReadShort();
			field_8_colInputRow = in1.ReadShort();
			field_9_rowInputCol = in1.ReadShort();
			field_10_colInputCol = in1.ReadShort();
		}

		public TableRecord(CellRangeAddress8Bit range)
			: base(range)
		{
			field_6_res = 0;
		}

		protected override void SerializeExtraData(ILittleEndianOutput out1)
		{
			out1.WriteByte(field_5_flags);
			out1.WriteByte(field_6_res);
			out1.WriteShort(field_7_rowInputRow);
			out1.WriteShort(field_8_colInputRow);
			out1.WriteShort(field_9_rowInputCol);
			out1.WriteShort(field_10_colInputCol);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TABLE]\n");
			stringBuilder.Append("    .range    = ").Append(base.Range.ToString()).Append("\n");
			stringBuilder.Append("    .flags    = ").Append(HexDump.ByteToHex(field_5_flags)).Append("\n");
			stringBuilder.Append("    .alwaysClc= ").Append(IsAlwaysCalc).Append("\n");
			stringBuilder.Append("    .reserved = ").Append(HexDump.IntToHex(field_6_res)).Append("\n");
			CellReference cellReference = cr(field_7_rowInputRow, field_8_colInputRow);
			CellReference cellReference2 = cr(field_9_rowInputCol, field_10_colInputCol);
			stringBuilder.Append("    .rowInput = ").Append(cellReference.FormatAsString()).Append("\n");
			stringBuilder.Append("    .colInput = ").Append(cellReference2.FormatAsString()).Append("\n");
			stringBuilder.Append("[/TABLE]\n");
			return stringBuilder.ToString();
		}

		private static CellReference cr(int rowIx, int colIxAndFlags)
		{
			int pCol = colIxAndFlags & 0xFF;
			bool pAbsRow = (colIxAndFlags & 0x8000) == 0;
			bool pAbsCol = (colIxAndFlags & 0x4000) == 0;
			return new CellReference(rowIx, pCol, pAbsRow, pAbsCol);
		}
	}
}
