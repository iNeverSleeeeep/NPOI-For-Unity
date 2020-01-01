using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Object Protect Record
	/// Description: Protect embedded object with the lamest "security" ever invented.  
	/// This record tells  "I want to protect my objects" with lame security.  It 
	/// appears in conjunction with the PASSWORD and PROTECT records as well as its 
	/// scenario protect cousin.
	/// REFERENCE:  PG 368 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	public class ObjectProtectRecord : StandardRecord
	{
		public const short sid = 99;

		private short field_1_protect;

		/// Get whether the sheet Is protected or not
		/// @return whether to protect the sheet or not
		public bool Protect
		{
			get
			{
				return field_1_protect == 1;
			}
			set
			{
				if (value)
				{
					field_1_protect = 1;
				}
				else
				{
					field_1_protect = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 99;

		public ObjectProtectRecord()
		{
		}

		/// Constructs a Protect record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ObjectProtectRecord(RecordInputStream in1)
		{
			field_1_protect = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SCENARIOPROTECT]\n");
			stringBuilder.Append("    .protect         = ").Append(Protect).Append("\n");
			stringBuilder.Append("[/SCENARIOPROTECT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_protect);
		}

		public override object Clone()
		{
			ObjectProtectRecord objectProtectRecord = new ObjectProtectRecord();
			objectProtectRecord.field_1_protect = field_1_protect;
			return objectProtectRecord;
		}
	}
}
