using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Protection Revision 4 password Record
	/// Description:  Stores the (2 byte??!!) encrypted password for a shared
	///               workbook
	/// REFERENCE:  PG 374 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class PasswordRev4Record : StandardRecord
	{
		public const short sid = 444;

		private int field_1_password;

		protected override int DataSize => 2;

		public override short Sid => 444;

		public PasswordRev4Record(int pw)
		{
			field_1_password = pw;
		}

		/// Constructs a PasswordRev4 (PROT4REVPASS) record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PasswordRev4Record(RecordInputStream in1)
		{
			field_1_password = in1.ReadShort();
		}

		/// set the password
		///
		/// @param pw  representing the password
		public void SetPassword(short pw)
		{
			field_1_password = pw;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROT4REVPASSWORD]\n");
			stringBuilder.Append("    .password       = ").Append(StringUtil.ToHexString(field_1_password)).Append("\n");
			stringBuilder.Append("[/PROT4REVPASSWORD]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_password);
		}
	}
}
