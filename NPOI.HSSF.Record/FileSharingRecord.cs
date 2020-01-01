using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        FILESHARING
	/// Description:  stores the encrypted Readonly for a workbook (Write protect) 
	/// This functionality Is accessed from the options dialog box available when performing 'Save As'.<p />
	/// REFERENCE:  PG 314 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)<p />
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	public class FileSharingRecord : StandardRecord
	{
		public const short sid = 91;

		private short field_1_Readonly;

		private short field_2_password;

		private byte field_3_username_unicode_options;

		private string field_3_username_value;

		/// Get the Readonly
		///
		/// @return short  representing if this Is Read only (1 = true)
		public short ReadOnly
		{
			get
			{
				return field_1_Readonly;
			}
			set
			{
				field_1_Readonly = value;
			}
		}

		/// @returns password hashed with hashPassword() (very lame)
		public short Password
		{
			get
			{
				return field_2_password;
			}
			set
			{
				field_2_password = value;
			}
		}

		/// @returns username of the user that Created the file
		public string Username
		{
			get
			{
				return field_3_username_value;
			}
			set
			{
				field_3_username_value = value;
			}
		}

		protected override int DataSize
		{
			get
			{
				int length = field_3_username_value.Length;
				if (length < 1)
				{
					return 6;
				}
				return 7 + length;
			}
		}

		public override short Sid => 91;

		public FileSharingRecord()
		{
		}

		/// Constructs a FileSharing record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public FileSharingRecord(RecordInputStream in1)
		{
			field_1_Readonly = in1.ReadShort();
			field_2_password = in1.ReadShort();
			int num = in1.ReadShort();
			if (num > 0)
			{
				field_3_username_unicode_options = (byte)in1.ReadByte();
				field_3_username_value = in1.ReadCompressedUnicode(num);
				if (field_3_username_value == null)
				{
					field_3_username_value = "";
				}
			}
			else
			{
				field_3_username_value = "";
			}
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
			stringBuilder.Append("[FILESHARING]\n");
			stringBuilder.Append("    .Readonly       = ").Append((ReadOnly == 1) ? "true" : "false").Append("\n");
			stringBuilder.Append("    .password       = ").Append(StringUtil.ToHexString(Password)).Append("\n");
			stringBuilder.Append("    .username       = ").Append(Username).Append("\n");
			stringBuilder.Append("[/FILESHARING]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(ReadOnly);
			out1.WriteShort(Password);
			out1.WriteShort(field_3_username_value.Length);
			if (field_3_username_value.Length > 0)
			{
				out1.WriteByte(field_3_username_unicode_options);
				StringUtil.PutCompressedUnicode(Username, out1);
			}
		}

		/// Clone this record.
		public override object Clone()
		{
			FileSharingRecord fileSharingRecord = new FileSharingRecord();
			fileSharingRecord.ReadOnly = field_1_Readonly;
			fileSharingRecord.Password = field_2_password;
			fileSharingRecord.Username = field_3_username_value;
			return fileSharingRecord;
		}
	}
}
