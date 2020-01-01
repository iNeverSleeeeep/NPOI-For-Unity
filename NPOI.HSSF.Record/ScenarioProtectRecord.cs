using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Scenario Protect Record
	/// Description:  I have no idea what a Scenario is or why on would want to 
	/// protect it with the lamest "security" ever invented.  However this record tells
	/// excel "I want to protect my scenarios" (0xAF) with lame security.  It appears 
	/// in conjunction with the PASSWORD and PROTECT records as well as its object 
	/// protect cousin.
	/// REFERENCE:  PG 383 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	public class ScenarioProtectRecord : StandardRecord
	{
		public const short sid = 221;

		private short field_1_protect;

		/// Get whether the sheet is protected or not
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

		public override short Sid => 221;

		protected override int DataSize => 2;

		public ScenarioProtectRecord()
		{
		}

		/// Constructs a Protect record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ScenarioProtectRecord(RecordInputStream in1)
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
			ScenarioProtectRecord scenarioProtectRecord = new ScenarioProtectRecord();
			scenarioProtectRecord.field_1_protect = field_1_protect;
			return scenarioProtectRecord;
		}
	}
}
