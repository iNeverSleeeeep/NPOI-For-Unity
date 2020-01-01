using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// ToGether the the EscherOptRecord this record defines some of the basic
	/// properties of a shape.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherSpRecord : EscherRecord
	{
		public const short RECORD_ID = -4086;

		public const string RECORD_DESCRIPTION = "MsofbtSp";

		public const int FLAG_GROUP = 1;

		public const int FLAG_CHILD = 2;

		public const int FLAG_PATRIARCH = 4;

		public const int FLAG_DELETED = 8;

		public const int FLAG_OLESHAPE = 16;

		public const int FLAG_HAVEMASTER = 32;

		public const int FLAG_FLIPHORIZ = 64;

		public const int FLAG_FLIPVERT = 128;

		public const int FLAG_CONNECTOR = 256;

		public const int FLAG_HAVEANCHOR = 512;

		public const int FLAG_BACKGROUND = 1024;

		public const int FLAG_HASSHAPETYPE = 2048;

		private int field_1_shapeId;

		private int field_2_flags;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 16;

		/// <summary>
		/// @return  the 16 bit identifier for this record.
		/// </summary>
		/// <value></value>
		public override short RecordId => -4086;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Sp";

		/// <summary>
		/// Gets or sets A number that identifies this shape
		/// </summary>
		/// <value>The shape id.</value>
		public int ShapeId
		{
			get
			{
				return field_1_shapeId;
			}
			set
			{
				field_1_shapeId = value;
			}
		}

		/// <summary>
		/// The flags that apply to this shape.
		/// </summary>
		/// <value>The flags.</value>
		public int Flags
		{
			get
			{
				return field_2_flags;
			}
			set
			{
				field_2_flags = value;
			}
		}

		/// <summary>
		/// Get or set shape type. Must be one of MSOSPT values (see [MS-ODRAW] for details).
		/// </summary>
		public short ShapeType
		{
			get
			{
				return Instance;
			}
			set
			{
				Instance = value;
			}
		}

		/// <summary>
		/// The contract of this method is to deSerialize an escher record including
		/// it's children.
		/// </summary>
		/// <param name="data">The byte array containing the Serialized escher
		/// records.</param>
		/// <param name="offset">The offset into the byte array.</param>
		/// <param name="recordFactory">A factory for creating new escher records</param>
		/// <returns>The number of bytes written.</returns>  
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			ReadHeader(data, offset);
			int num = offset + 8;
			int num2 = 0;
			field_1_shapeId = LittleEndian.GetInt(data, num + num2);
			num2 += 4;
			field_2_flags = LittleEndian.GetInt(data, num + num2);
			num2 += 4;
			return RecordSize;
		}

		/// <summary>
		/// Serializes to an existing byte array without serialization listener.
		/// This is done by delegating to Serialize(int, byte[], EscherSerializationListener).
		/// </summary>
		/// <param name="offset">the offset within the data byte array.</param>
		/// <param name="data"> the data array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int value = 8;
			LittleEndian.PutInt(data, offset + 4, value);
			LittleEndian.PutInt(data, offset + 8, field_1_shapeId);
			LittleEndian.PutInt(data, offset + 12, field_2_flags);
			listener.AfterRecordSerialize(offset + RecordSize, RecordId, RecordSize, this);
			return 16;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4086)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  ShapeType: 0x" + HexDump.ToHex(ShapeType) + newLine + "  ShapeId: " + field_1_shapeId + newLine + "  Flags: " + DecodeFlags(field_2_flags) + " (0x" + HexDump.ToHex(field_2_flags) + ")" + newLine;
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<ShapeType>0x")
				.Append(HexDump.ToHex(ShapeType))
				.Append("</ShapeType>\n")
				.Append(tab)
				.Append("\t")
				.Append("<ShapeId>")
				.Append(field_1_shapeId)
				.Append("</ShapeId>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Flags>")
				.Append(DecodeFlags(field_2_flags) + " (0x" + HexDump.ToHex(field_2_flags) + ")")
				.Append("</Flags>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Converts the shape flags into a more descriptive name.
		/// </summary>
		/// <param name="flags">The flags.</param>
		/// <returns></returns>
		private string DecodeFlags(int flags)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(((flags & 1) != 0) ? "|GROUP" : "");
			stringBuilder.Append(((flags & 2) != 0) ? "|CHILD" : "");
			stringBuilder.Append(((flags & 4) != 0) ? "|PATRIARCH" : "");
			stringBuilder.Append(((flags & 8) != 0) ? "|DELETED" : "");
			stringBuilder.Append(((flags & 0x10) != 0) ? "|OLESHAPE" : "");
			stringBuilder.Append(((flags & 0x20) != 0) ? "|HAVEMASTER" : "");
			stringBuilder.Append(((flags & 0x40) != 0) ? "|FLIPHORIZ" : "");
			stringBuilder.Append(((flags & 0x80) != 0) ? "|FLIPVERT" : "");
			stringBuilder.Append(((flags & 0x100) != 0) ? "|CONNECTOR" : "");
			stringBuilder.Append(((flags & 0x200) != 0) ? "|HAVEANCHOR" : "");
			stringBuilder.Append(((flags & 0x400) != 0) ? "|BACKGROUND" : "");
			stringBuilder.Append(((flags & 0x800) != 0) ? "|HASSHAPETYPE" : "");
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(0, 1);
			}
			return stringBuilder.ToString();
		}
	}
}
