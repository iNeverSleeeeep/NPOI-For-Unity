using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Codepage Record
	/// Description:  the default CharSet. for the workbook
	/// REFERENCE:  PG 293 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class CodepageRecord : StandardRecord
	{
		public const short sid = 66;

		/// the likely correct value for CODEPAGE (at least for US versions).  We could use
		/// some help with international versions (which we do not have access to documentation
		/// for)
		public const short CODEPAGE = 1200;

		private short field_1_codepage;

		/// Get the codepage for this workbook
		///
		/// @see #CODEPAGE
		/// @return codepage - the codepage to Set
		public short Codepage
		{
			get
			{
				return field_1_codepage;
			}
			set
			{
				field_1_codepage = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 66;

		public CodepageRecord()
		{
		}

		/// Constructs a CodepageRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public CodepageRecord(RecordInputStream in1)
		{
			field_1_codepage = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CODEPAGE]\n");
			stringBuilder.Append("    .codepage        = ").Append(StringUtil.ToHexString(Codepage)).Append("\n");
			stringBuilder.Append("[/CODEPAGE]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Codepage);
		}
	}
}
