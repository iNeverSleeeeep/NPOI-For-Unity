using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// NOTE: Comment Associated with a Cell (1Ch)
	///
	/// @author Yegor Kozlov
	public class NoteRecord : StandardRecord
	{
		public const short sid = 28;

		/// Flag indicating that the comment Is hidden (default)
		public const short NOTE_HIDDEN = 0;

		/// Flag indicating that the comment Is visible
		public const short NOTE_VISIBLE = 2;

		private const byte DEFAULT_PADDING = 0;

		public static readonly NoteRecord[] EMPTY_ARRAY = new NoteRecord[0];

		private int field_1_row;

		private int field_2_col;

		private short field_3_flags;

		private int field_4_shapeid;

		private bool field_5_hasMultibyte;

		private string field_6_author;

		/// Saves padding byte value to reduce delta during round-trip serialization.<br />
		///
		/// The documentation is not clear about how padding should work.  In any case
		/// Excel(2007) does something different.
		private byte? field_7_padding;

		/// @return id of this record.
		public override short Sid => 28;

		/// Size of record
		protected override int DataSize => 11 + field_6_author.Length * ((!field_5_hasMultibyte) ? 1 : 2) + (((int?)field_7_padding).HasValue ? 1 : 0);

		/// Return the row that Contains the comment
		///
		/// @return the row that Contains the comment
		public int Row
		{
			get
			{
				return field_1_row;
			}
			set
			{
				field_1_row = value;
			}
		}

		/// Return the column that Contains the comment
		///
		/// @return the column that Contains the comment
		public int Column
		{
			get
			{
				return field_2_col;
			}
			set
			{
				field_2_col = value;
			}
		}

		/// Options flags.
		///
		/// @return the options flag
		/// @see #NOTE_VISIBLE
		/// @see #NOTE_HIDDEN
		public short Flags
		{
			get
			{
				return field_3_flags;
			}
			set
			{
				field_3_flags = value;
			}
		}

		/// Object id for OBJ record that Contains the comment
		public int ShapeId
		{
			get
			{
				return field_4_shapeid;
			}
			set
			{
				field_4_shapeid = value;
			}
		}

		/// Name of the original comment author
		///
		/// @return the name of the original author of the comment
		public string Author
		{
			get
			{
				return field_6_author;
			}
			set
			{
				field_6_author = value;
				field_5_hasMultibyte = StringUtil.HasMultibyte(value);
			}
		}

		/// For unit testing only!
		internal bool AuthorIsMultibyte => field_5_hasMultibyte;

		/// Construct a new <c>NoteRecord</c> and
		/// Fill its data with the default values
		public NoteRecord()
		{
			field_6_author = "";
			field_3_flags = 0;
			field_7_padding = 0;
		}

		/// Constructs a <c>NoteRecord</c> and Fills its fields
		/// from the supplied <c>RecordInputStream</c>.
		///
		/// @param in the stream to Read from
		public NoteRecord(RecordInputStream in1)
		{
			field_1_row = in1.ReadShort();
			field_2_col = in1.ReadUShort();
			field_3_flags = in1.ReadShort();
			field_4_shapeid = in1.ReadUShort();
			int nChars = in1.ReadShort();
			field_5_hasMultibyte = (in1.ReadByte() != 0);
			if (field_5_hasMultibyte)
			{
				field_6_author = StringUtil.ReadUnicodeLE(in1, nChars);
			}
			else
			{
				field_6_author = StringUtil.ReadCompressedUnicode(in1, nChars);
			}
			if (in1.Available() == 1)
			{
				field_7_padding = (byte)in1.ReadByte();
			}
		}

		/// Serialize the record data into the supplied array of bytes
		///
		/// @param offset offset in the <c>data</c>
		/// @param data the data to Serialize into
		///
		/// @return size of the record
		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_row);
			out1.WriteShort(field_2_col);
			out1.WriteShort(field_3_flags);
			out1.WriteShort(field_4_shapeid);
			out1.WriteShort(field_6_author.Length);
			out1.WriteByte(field_5_hasMultibyte ? 1 : 0);
			if (field_5_hasMultibyte)
			{
				StringUtil.PutUnicodeLE(field_6_author, out1);
			}
			else
			{
				StringUtil.PutCompressedUnicode(field_6_author, out1);
			}
			if (((int?)field_7_padding).HasValue)
			{
				out1.WriteByte(Convert.ToInt32(field_7_padding, CultureInfo.InvariantCulture));
			}
		}

		/// Convert this record to string.
		/// Used by BiffViewer and other utulities.
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NOTE]\n");
			stringBuilder.Append("    .recordid = 0x" + StringUtil.ToHexString(Sid) + ", size = " + RecordSize + "\n");
			stringBuilder.Append("    .row =     " + field_1_row + "\n");
			stringBuilder.Append("    .col =     " + field_2_col + "\n");
			stringBuilder.Append("    .flags =   " + field_3_flags + "\n");
			stringBuilder.Append("    .shapeid = " + field_4_shapeid + "\n");
			stringBuilder.Append("    .author =  " + field_6_author + "\n");
			stringBuilder.Append("[/NOTE]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			NoteRecord noteRecord = new NoteRecord();
			noteRecord.field_1_row = field_1_row;
			noteRecord.field_2_col = field_2_col;
			noteRecord.field_3_flags = field_3_flags;
			noteRecord.field_4_shapeid = field_4_shapeid;
			noteRecord.field_6_author = field_6_author;
			return noteRecord;
		}
	}
}
