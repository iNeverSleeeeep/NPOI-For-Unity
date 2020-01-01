using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: NAMECMT Record (0x0894)
	/// Description: Defines a comment associated with a specified name.
	/// REFERENCE:
	///
	/// @author Andrew Shirley (aks at corefiling.co.uk)
	public class NameCommentRecord : StandardRecord
	{
		public const short sid = 2196;

		private short field_1_record_type;

		private short field_2_frt_cell_ref_flag;

		private long field_3_reserved;

		private string field_6_name_text;

		private string field_7_comment_text;

		protected override int DataSize => 18 + field_6_name_text.Length + field_7_comment_text.Length;

		/// return the non static version of the id for this record.
		public override short Sid => 2196;

		/// @return the name of the NameRecord to which this comment applies.
		public string NameText
		{
			get
			{
				return field_6_name_text;
			}
			set
			{
				field_6_name_text = value;
			}
		}

		/// @return the text of the comment.
		public string CommentText
		{
			get
			{
				return field_7_comment_text;
			}
			set
			{
				field_7_comment_text = value;
			}
		}

		public short RecordType => field_1_record_type;

		public NameCommentRecord(string name, string comment)
		{
			field_1_record_type = 0;
			field_2_frt_cell_ref_flag = 0;
			field_3_reserved = 0L;
			field_6_name_text = name;
			field_7_comment_text = comment;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			int length = field_6_name_text.Length;
			int length2 = field_7_comment_text.Length;
			out1.WriteShort(field_1_record_type);
			out1.WriteShort(field_2_frt_cell_ref_flag);
			out1.WriteLong(field_3_reserved);
			out1.WriteShort(length);
			out1.WriteShort(length2);
			out1.WriteByte(0);
			StringUtil.PutCompressedUnicode(field_6_name_text, out1);
			out1.WriteByte(0);
			StringUtil.PutCompressedUnicode(field_7_comment_text, out1);
		}

		/// @param ris the RecordInputstream to read the record from
		public NameCommentRecord(RecordInputStream ris)
		{
			field_1_record_type = ((ILittleEndianInput)ris).ReadShort();
			field_2_frt_cell_ref_flag = ((ILittleEndianInput)ris).ReadShort();
			field_3_reserved = ((ILittleEndianInput)ris).ReadLong();
			int nChars = ((ILittleEndianInput)ris).ReadShort();
			int nChars2 = ((ILittleEndianInput)ris).ReadShort();
			((ILittleEndianInput)ris).ReadByte();
			field_6_name_text = StringUtil.ReadCompressedUnicode(ris, nChars);
			((ILittleEndianInput)ris).ReadByte();
			field_7_comment_text = StringUtil.ReadCompressedUnicode(ris, nChars2);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NAMECMT]\n");
			stringBuilder.Append("    .record type            = ").Append(HexDump.ShortToHex(field_1_record_type)).Append("\n");
			stringBuilder.Append("    .frt cell ref flag      = ").Append(HexDump.ByteToHex(field_2_frt_cell_ref_flag)).Append("\n");
			stringBuilder.Append("    .reserved               = ").Append(field_3_reserved).Append("\n");
			stringBuilder.Append("    .name length            = ").Append(field_6_name_text.Length).Append("\n");
			stringBuilder.Append("    .comment length         = ").Append(field_7_comment_text.Length).Append("\n");
			stringBuilder.Append("    .name                   = ").Append(field_6_name_text).Append("\n");
			stringBuilder.Append("    .comment                = ").Append(field_7_comment_text).Append("\n");
			stringBuilder.Append("[/NAMECMT]\n");
			return stringBuilder.ToString();
		}
	}
}
