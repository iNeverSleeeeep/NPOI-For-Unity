using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Password Record
	/// Description:  stores the encrypted password for a sheet or workbook (HSSF doesn't support encryption)
	/// REFERENCE:  PG 371 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class PasswordRecord : StandardRecord
	{
		public const short sid = 19;

		private int field_1_password;

		/// Get the password
		///
		/// @return short  representing the password
		public int Password
		{
			get
			{
				return field_1_password;
			}
			set
			{
				field_1_password = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 19;

		public PasswordRecord(int password)
		{
			field_1_password = password;
		}

		/// Constructs a Password record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public PasswordRecord(RecordInputStream in1)
		{
			field_1_password = in1.ReadShort();
		}

		public static short HashPassword(string password)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			int num = 0;
			if (bytes.Length > 0)
			{
				int num2 = bytes.Length;
				while (num2-- > 0)
				{
					num = (((num >> 14) & 1) | ((num << 1) & 0x7FFF));
					num ^= bytes[num2];
				}
				num = (((num >> 14) & 1) | ((num << 1) & 0x7FFF));
				num ^= bytes.Length;
				num ^= 0xCE4B;
			}
			return (short)num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PASSWORD]\n");
			stringBuilder.Append("    .password       = ").Append(StringUtil.ToHexString(Password)).Append("\n");
			stringBuilder.Append("[/PASSWORD]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_password);
		}

		/// Clone this record.
		public override object Clone()
		{
			return new PasswordRecord(field_1_password);
		}
	}
}
