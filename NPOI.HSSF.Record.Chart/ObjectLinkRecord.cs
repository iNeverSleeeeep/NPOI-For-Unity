using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * Links text to an object on the chart or identifies it as the title.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class ObjectLinkRecord : StandardRecord
	{
		public const short sid = 4135;

		public const short ANCHOR_ID_CHART_TITLE = 1;

		public const short ANCHOR_ID_Y_AXIS = 2;

		public const short ANCHOR_ID_X_AXIS = 3;

		public const short ANCHOR_ID_SERIES_OR_POINT = 4;

		public const short ANCHOR_ID_Z_AXIS = 7;

		private short field_1_anchorId;

		private short field_2_link1;

		private short field_3_link2;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 6;

		public override short Sid => 4135;

		/// Get the anchor id field for the ObjectLink record.
		///
		/// @return  One of 
		///        ANCHOR_ID_CHART_TITLE
		///        ANCHOR_ID_Y_AXIS
		///        ANCHOR_ID_X_AXIS
		///        ANCHOR_ID_SERIES_OR_POINT
		///        ANCHOR_ID_Z_AXIS
		public short AnchorId
		{
			get
			{
				return field_1_anchorId;
			}
			set
			{
				field_1_anchorId = value;
			}
		}

		/// Get the link 1 field for the ObjectLink record.
		public short Link1
		{
			get
			{
				return field_2_link1;
			}
			set
			{
				field_2_link1 = value;
			}
		}

		/// Get the link 2 field for the ObjectLink record.
		public short Link2
		{
			get
			{
				return field_3_link2;
			}
			set
			{
				field_3_link2 = value;
			}
		}

		public ObjectLinkRecord()
		{
		}

		/// Constructs a ObjectLink record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public ObjectLinkRecord(RecordInputStream in1)
		{
			field_1_anchorId = in1.ReadShort();
			field_2_link1 = in1.ReadShort();
			field_3_link2 = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[OBJECTLINK]\n");
			stringBuilder.Append("    .AnchorId             = ").Append("0x").Append(HexDump.ToHex(AnchorId))
				.Append(" (")
				.Append(AnchorId)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .link1                = ").Append("0x").Append(HexDump.ToHex(Link1))
				.Append(" (")
				.Append(Link1)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .link2                = ").Append("0x").Append(HexDump.ToHex(Link2))
				.Append(" (")
				.Append(Link2)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/OBJECTLINK]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_anchorId);
			out1.WriteShort(field_2_link1);
			out1.WriteShort(field_3_link2);
		}

		public override object Clone()
		{
			ObjectLinkRecord objectLinkRecord = new ObjectLinkRecord();
			objectLinkRecord.field_1_anchorId = field_1_anchorId;
			objectLinkRecord.field_2_link1 = field_2_link1;
			objectLinkRecord.field_3_link2 = field_3_link2;
			return objectLinkRecord;
		}
	}
}
