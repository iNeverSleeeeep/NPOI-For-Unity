using NPOI.SS.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Selection Record
	/// Description:  shows the user's selection on the sheet
	///               for Write Set num refs to 0
	///
	/// TODO :  Fully implement reference subrecords.
	/// REFERENCE:  PG 291 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @author Glen Stampoultzis (glens at apache.org)
	public class SelectionRecord : StandardRecord
	{
		public const short sid = 29;

		private byte field_1_pane;

		private int field_2_row_active_cell;

		private int field_3_col_active_cell;

		private int field_4_ref_active_cell;

		private CellRangeAddress8Bit[] field_6_refs;

		/// <summary>
		/// Gets or sets the pane this is for.
		/// </summary>
		/// <value>The pane.</value>
		public byte Pane
		{
			get
			{
				return field_1_pane;
			}
			set
			{
				field_1_pane = value;
			}
		}

		/// <summary>
		/// Gets or sets the active cell row.
		/// </summary>
		/// <value>row number of active cell</value>
		public int ActiveCellRow
		{
			get
			{
				return field_2_row_active_cell;
			}
			set
			{
				field_2_row_active_cell = value;
			}
		}

		/// <summary>
		/// Gets or sets the active cell's col
		/// </summary>
		/// <value>number of active cell</value>
		public int ActiveCellCol
		{
			get
			{
				return field_3_col_active_cell;
			}
			set
			{
				field_3_col_active_cell = value;
			}
		}

		/// <summary>
		/// Gets or sets the active cell's reference number
		/// </summary>
		/// <value>ref number of active cell</value>
		public int ActiveCellRef
		{
			get
			{
				return field_4_ref_active_cell;
			}
			set
			{
				field_4_ref_active_cell = value;
			}
		}

		public CellRangeAddress8Bit[] CellReferences
		{
			get
			{
				return field_6_refs;
			}
			set
			{
				field_6_refs = value;
			}
		}

		protected override int DataSize => 9 + CellRangeAddress8Bit.GetEncodedSize(field_6_refs.Length);

		public override short Sid => 29;

		public SelectionRecord(int activeCellRow, int activeCellCol)
		{
			field_1_pane = 3;
			field_2_row_active_cell = activeCellRow;
			field_3_col_active_cell = activeCellCol;
			field_4_ref_active_cell = 0;
			field_6_refs = new CellRangeAddress8Bit[1]
			{
				new CellRangeAddress8Bit(activeCellRow, activeCellRow, activeCellCol, activeCellCol)
			};
		}

		/// <summary>
		/// Constructs a Selection record and Sets its fields appropriately.
		/// </summary>
		/// <param name="in1">the RecordInputstream to Read the record from</param>
		public SelectionRecord(RecordInputStream in1)
		{
			field_1_pane = (byte)in1.ReadByte();
			field_2_row_active_cell = in1.ReadUShort();
			field_3_col_active_cell = in1.ReadShort();
			field_4_ref_active_cell = in1.ReadShort();
			int num = in1.ReadUShort();
			field_6_refs = new CellRangeAddress8Bit[num];
			for (int i = 0; i < num; i++)
			{
				field_6_refs[i] = new CellRangeAddress8Bit(in1);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SELECTION]\n");
			stringBuilder.Append("    .pane            = ").Append(StringUtil.ToHexString(Pane)).Append("\n");
			stringBuilder.Append("    .activecellrow   = ").Append(StringUtil.ToHexString(ActiveCellRow)).Append("\n");
			stringBuilder.Append("    .activecellcol   = ").Append(StringUtil.ToHexString(ActiveCellCol)).Append("\n");
			stringBuilder.Append("    .activecellref   = ").Append(StringUtil.ToHexString(ActiveCellRef)).Append("\n");
			stringBuilder.Append("    .numrefs         = ").Append(StringUtil.ToHexString(field_6_refs.Length)).Append("\n");
			stringBuilder.Append("[/SELECTION]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(Pane);
			out1.WriteShort(ActiveCellRow);
			out1.WriteShort(ActiveCellCol);
			out1.WriteShort(ActiveCellRef);
			int v = field_6_refs.Length;
			out1.WriteShort(v);
			for (int i = 0; i < field_6_refs.Length; i++)
			{
				field_6_refs[i].Serialize(out1);
			}
		}

		public override object Clone()
		{
			SelectionRecord selectionRecord = new SelectionRecord(field_2_row_active_cell, field_3_col_active_cell);
			selectionRecord.field_1_pane = field_1_pane;
			selectionRecord.field_4_ref_active_cell = field_4_ref_active_cell;
			selectionRecord.field_6_refs = field_6_refs;
			return selectionRecord;
		}
	}
}
