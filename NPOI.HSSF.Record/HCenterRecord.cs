using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        HCenter record
	/// Description:  whether to center between horizontal margins
	/// REFERENCE:  PG 320 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class HCenterRecord : StandardRecord
	{
		public const short sid = 131;

		private short field_1_hcenter;

		/// Get whether or not to horizonatally center this sheet.
		/// @return center - t/f
		public bool HCenter
		{
			get
			{
				return field_1_hcenter == 1;
			}
			set
			{
				if (value)
				{
					field_1_hcenter = 1;
				}
				else
				{
					field_1_hcenter = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 131;

		public HCenterRecord()
		{
		}

		/// Constructs an HCenter record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public HCenterRecord(RecordInputStream in1)
		{
			field_1_hcenter = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HCENTER]\n");
			stringBuilder.Append("    .hcenter        = ").Append(HCenter).Append("\n");
			stringBuilder.Append("[/HCENTER]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_hcenter);
		}

		public override object Clone()
		{
			HCenterRecord hCenterRecord = new HCenterRecord();
			hCenterRecord.field_1_hcenter = field_1_hcenter;
			return hCenterRecord;
		}
	}
}
