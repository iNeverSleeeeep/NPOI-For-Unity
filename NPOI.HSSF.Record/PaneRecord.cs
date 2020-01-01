using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// * Describes the frozen and Unfozen panes.
	/// * NOTE: This source Is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class PaneRecord : StandardRecord
	{
		public const short sid = 65;

		public const short ACTIVE_PANE_LOWER_RIGHT = 0;

		public const short ACTIVE_PANE_UPPER_RIGHT = 1;

		public const short ACTIVE_PANE_LOWER_LEFT = 2;

		public const short ACTIVE_PANE_UPPER_LEFT = 3;

		private short field_1_x;

		private short field_2_y;

		private short field_3_topRow;

		private short field_4_leftColumn;

		private short field_5_activePane;

		protected override int DataSize => 10;

		public override short Sid => 65;

		/// Get the x field for the Pane record.
		public short X
		{
			get
			{
				return field_1_x;
			}
			set
			{
				field_1_x = value;
			}
		}

		/// Get the y field for the Pane record.
		public short Y
		{
			get
			{
				return field_2_y;
			}
			set
			{
				field_2_y = value;
			}
		}

		/// Get the top row field for the Pane record.
		public short TopRow
		{
			get
			{
				return field_3_topRow;
			}
			set
			{
				field_3_topRow = value;
			}
		}

		/// Get the left column field for the Pane record.
		public short LeftColumn
		{
			get
			{
				return field_4_leftColumn;
			}
			set
			{
				field_4_leftColumn = value;
			}
		}

		/// Get the active pane field for the Pane record.
		///
		/// @return  One of 
		///        ACTIVE_PANE_LOWER_RIGHT
		///        ACTIVE_PANE_UPPER_RIGHT
		///        ACTIVE_PANE_LOWER_LEFT
		///        ACTIVE_PANE_UPPER_LEFT
		public short ActivePane
		{
			get
			{
				return field_5_activePane;
			}
			set
			{
				field_5_activePane = value;
			}
		}

		public PaneRecord()
		{
		}

		/// Constructs a Pane record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public PaneRecord(RecordInputStream in1)
		{
			field_1_x = in1.ReadShort();
			field_2_y = in1.ReadShort();
			field_3_topRow = in1.ReadShort();
			field_4_leftColumn = in1.ReadShort();
			field_5_activePane = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PANE]\n");
			stringBuilder.Append("    .x                    = ").Append("0x").Append(HexDump.ToHex(X))
				.Append(" (")
				.Append(X)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .y                    = ").Append("0x").Append(HexDump.ToHex(Y))
				.Append(" (")
				.Append(Y)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .topRow               = ").Append("0x").Append(HexDump.ToHex(TopRow))
				.Append(" (")
				.Append(TopRow)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .leftColumn           = ").Append("0x").Append(HexDump.ToHex(LeftColumn))
				.Append(" (")
				.Append(LeftColumn)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .activePane           = ").Append("0x").Append(HexDump.ToHex(ActivePane))
				.Append(" (")
				.Append(ActivePane)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/PANE]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_x);
			out1.WriteShort(field_2_y);
			out1.WriteShort(field_3_topRow);
			out1.WriteShort(field_4_leftColumn);
			out1.WriteShort(field_5_activePane);
		}

		public override object Clone()
		{
			PaneRecord paneRecord = new PaneRecord();
			paneRecord.field_1_x = field_1_x;
			paneRecord.field_2_y = field_2_y;
			paneRecord.field_3_topRow = field_3_topRow;
			paneRecord.field_4_leftColumn = field_4_leftColumn;
			paneRecord.field_5_activePane = field_5_activePane;
			return paneRecord;
		}
	}
}
