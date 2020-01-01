using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        VCenter record
	/// Description:  tells whether to center the sheet between vertical margins
	/// REFERENCE:  PG 420 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class VCenterRecord : StandardRecord
	{
		public const short sid = 132;

		private short field_1_vcenter;

		/// Get whether to center vertically or not
		/// @return vcenter or not
		public bool VCenter
		{
			get
			{
				return field_1_vcenter == 1;
			}
			set
			{
				if (value)
				{
					field_1_vcenter = 1;
				}
				else
				{
					field_1_vcenter = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 132;

		public VCenterRecord()
		{
		}

		/// Constructs a VCENTER record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public VCenterRecord(RecordInputStream in1)
		{
			field_1_vcenter = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[VCENTER]\n");
			stringBuilder.Append("    .vcenter        = ").Append(VCenter).Append("\n");
			stringBuilder.Append("[/VCENTER]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_vcenter);
		}

		public override object Clone()
		{
			VCenterRecord vCenterRecord = new VCenterRecord();
			vCenterRecord.field_1_vcenter = field_1_vcenter;
			return vCenterRecord;
		}
	}
}
