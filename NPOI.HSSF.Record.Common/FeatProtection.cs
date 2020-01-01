using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Common
{
	/// Title: FeatProtection (Protection Shared Feature) common record part
	///
	/// This record part specifies Protection data for a sheet, stored
	///  as part of a Shared Feature. It can be found in records such
	///  as {@link FeatRecord}
	public class FeatProtection : SharedFeature
	{
		public const long NO_SELF_RELATIVE_SECURITY_FEATURE = 0L;

		public const long HAS_SELF_RELATIVE_SECURITY_FEATURE = 1L;

		private int fSD;

		/// 0 means no password. Otherwise indicates the
		///  password verifier algorithm (same kind as 
		///   {@link PasswordRecord} and
		///   {@link PasswordRev4Record})
		private int passwordVerifier;

		private string title;

		private byte[] securityDescriptor;

		public int DataSize => 8 + StringUtil.GetEncodedSize(title) + securityDescriptor.Length;

		public FeatProtection()
		{
			securityDescriptor = new byte[0];
		}

		public FeatProtection(RecordInputStream in1)
		{
			fSD = in1.ReadInt();
			passwordVerifier = in1.ReadInt();
			title = StringUtil.ReadUnicodeString(in1);
			securityDescriptor = in1.ReadRemainder();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" [FEATURE PROTECTION]\n");
			stringBuilder.Append("   Self Relative = " + fSD);
			stringBuilder.Append("   Password Verifier = " + passwordVerifier);
			stringBuilder.Append("   Title = " + title);
			stringBuilder.Append("   Security Descriptor Size = " + securityDescriptor.Length);
			stringBuilder.Append(" [/FEATURE PROTECTION]\n");
			return stringBuilder.ToString();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(fSD);
			out1.WriteInt(passwordVerifier);
			StringUtil.WriteUnicodeString(out1, title);
			out1.Write(securityDescriptor);
		}

		public int GetPasswordVerifier()
		{
			return passwordVerifier;
		}

		public void SetPasswordVerifier(int passwordVerifier)
		{
			this.passwordVerifier = passwordVerifier;
		}

		public string GetTitle()
		{
			return title;
		}

		public void SetTitle(string title)
		{
			this.title = title;
		}

		public int GetFSD()
		{
			return fSD;
		}
	}
}
