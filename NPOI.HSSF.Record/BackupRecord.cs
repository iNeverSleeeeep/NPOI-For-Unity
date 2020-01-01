using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Backup Record 
	/// Description:  bool specifying whether
	///               the GUI should store a backup of the file.
	/// REFERENCE:  PG 287 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class BackupRecord : StandardRecord
	{
		public const short sid = 64;

		private short field_1_backup;

		/// Get the backup flag
		///
		/// @return short 0/1 (off/on)
		public short Backup
		{
			get
			{
				return field_1_backup;
			}
			set
			{
				field_1_backup = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 64;

		public BackupRecord()
		{
		}

		/// Constructs a BackupRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public BackupRecord(RecordInputStream in1)
		{
			field_1_backup = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BACKUP]\n");
			stringBuilder.Append("    .backup          = ").Append(StringUtil.ToHexString(Backup)).Append("\n");
			stringBuilder.Append("[/BACKUP]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Backup);
		}
	}
}
