using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        DATAVALIDATIONS Record
	/// Description:  used in data validation ;
	///               This record Is the list header of all data validation records (0x01BE) in the current sheet.
	/// @author Dragos Buleandra (dragos.buleandra@trade2b.ro)
	public class DVALRecord : StandardRecord
	{
		public const short sid = 434;

		/// Options of the DVAL 
		private short field_1_options;

		/// Horizontal position of the dialog 
		private int field_2_horiz_pos;

		/// Vertical position of the dialog 
		private int field_3_vert_pos;

		/// Object ID of the drop down arrow object for list boxes ;
		/// in our case this will be always FFFF , Until
		/// MSODrawingGroup and MSODrawing records are implemented 
		private int field_cbo_id;

		/// Number of following DV Records 
		private int field_5_dv_no;

		/// @return the field_1_options
		public short Options
		{
			get
			{
				return field_1_options;
			}
			set
			{
				field_1_options = value;
			}
		}

		/// @return the Horizontal position of the dialog
		public int HorizontalPos
		{
			get
			{
				return field_2_horiz_pos;
			}
			set
			{
				field_2_horiz_pos = value;
			}
		}

		/// @return the the Vertical position of the dialog
		public int VerticalPos
		{
			get
			{
				return field_3_vert_pos;
			}
			set
			{
				field_3_vert_pos = value;
			}
		}

		/// Get Object ID of the drop down arrow object for list boxes
		public int ObjectID
		{
			get
			{
				return field_cbo_id;
			}
			set
			{
				field_cbo_id = value;
			}
		}

		/// Get number of following DV records
		public int DVRecNo
		{
			get
			{
				return field_5_dv_no;
			}
			set
			{
				field_5_dv_no = value;
			}
		}

		protected override int DataSize => 18;

		public override short Sid => 434;

		public DVALRecord()
		{
			field_cbo_id = -1;
			field_5_dv_no = 0;
		}

		/// Constructs a DVAL record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public DVALRecord(RecordInputStream in1)
		{
			field_1_options = in1.ReadShort();
			field_2_horiz_pos = in1.ReadInt();
			field_3_vert_pos = in1.ReadInt();
			field_cbo_id = in1.ReadInt();
			field_5_dv_no = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DVAL]\n");
			stringBuilder.Append("    .options      = ").Append(Options).Append('\n');
			stringBuilder.Append("    .horizPos     = ").Append(HorizontalPos).Append('\n');
			stringBuilder.Append("    .vertPos      = ").Append(VerticalPos).Append('\n');
			stringBuilder.Append("    .comboObjectID   = ").Append(StringUtil.ToHexString(ObjectID)).Append("\n");
			stringBuilder.Append("    .DVRecordsNumber = ").Append(StringUtil.ToHexString(DVRecNo)).Append("\n");
			stringBuilder.Append("[/DVAL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Options);
			out1.WriteInt(HorizontalPos);
			out1.WriteInt(VerticalPos);
			out1.WriteInt(ObjectID);
			out1.WriteInt(DVRecNo);
		}

		public override object Clone()
		{
			DVALRecord dVALRecord = new DVALRecord();
			dVALRecord.field_1_options = field_1_options;
			dVALRecord.field_2_horiz_pos = field_2_horiz_pos;
			dVALRecord.field_3_vert_pos = field_3_vert_pos;
			dVALRecord.field_cbo_id = field_cbo_id;
			dVALRecord.field_5_dv_no = field_5_dv_no;
			return dVALRecord;
		}
	}
}
