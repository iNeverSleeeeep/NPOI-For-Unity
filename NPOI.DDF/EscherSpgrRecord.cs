using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The spgr record defines information about a shape group.  Groups in escher
	/// are simply another form of shape that you can't physically see.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherSpgrRecord : EscherRecord
	{
		public const short RECORD_ID = -4087;

		public const string RECORD_DESCRIPTION = "MsofbtSpgr";

		private int field_1_rectX1;

		private int field_2_rectY1;

		private int field_3_rectX2;

		private int field_4_rectY2;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 24;

		/// <summary>
		/// Return the current record id.
		/// </summary>
		/// <value>The 16 bit identifier of this shape group record.</value>
		public override short RecordId => -4087;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Spgr";

		/// <summary>
		/// Gets or sets the starting top-left coordinate of child records.
		/// </summary>
		/// <value>The rect x1.</value>
		public int RectX1
		{
			get
			{
				return field_1_rectX1;
			}
			set
			{
				field_1_rectX1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the starting bottom-right coordinate of child records.
		/// </summary>
		/// <value>The rect x2.</value>
		public int RectX2
		{
			get
			{
				return field_3_rectX2;
			}
			set
			{
				field_3_rectX2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the starting top-left coordinate of child records.
		/// </summary>
		/// <value>The rect y1.</value>
		public int RectY1
		{
			get
			{
				return field_2_rectY1;
			}
			set
			{
				field_2_rectY1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the starting bottom-right coordinate of child records.
		/// </summary>
		/// <value>The rect y2.</value>
		public int RectY2
		{
			get
			{
				return field_4_rectY2;
			}
			set
			{
				field_4_rectY2 = value;
			}
		}

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into data</param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = offset + 8;
			int num3 = 0;
			field_1_rectX1 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_2_rectY1 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_3_rectX2 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_4_rectY2 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			num -= num3;
			if (num != 0)
			{
				throw new RecordFormatException("Expected no remaining bytes but got " + num);
			}
			return 8 + num3 + num;
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array
		/// </summary>
		/// <param name="offset">The offset into data
		///  to start writing the record data to.</param>
		/// <param name="data">The byte array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int value = 16;
			LittleEndian.PutInt(data, offset + 4, value);
			LittleEndian.PutInt(data, offset + 8, field_1_rectX1);
			LittleEndian.PutInt(data, offset + 12, field_2_rectY1);
			LittleEndian.PutInt(data, offset + 16, field_3_rectX2);
			LittleEndian.PutInt(data, offset + 20, field_4_rectY2);
			listener.AfterRecordSerialize(offset + RecordSize, RecordId, offset + RecordSize, this);
			return 24;
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
			return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4087)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  RectX: " + field_1_rectX1 + newLine + "  RectY: " + field_2_rectY1 + newLine + "  RectWidth: " + field_3_rectX2 + newLine + "  RectHeight: " + field_4_rectY2 + newLine;
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<RectX>")
				.Append(field_1_rectX1)
				.Append("</RectX>\n")
				.Append(tab)
				.Append("\t")
				.Append("<RectY>")
				.Append(field_2_rectY1)
				.Append("</RectY>\n")
				.Append(tab)
				.Append("\t")
				.Append("<RectWidth>")
				.Append(field_3_rectX2)
				.Append("</RectWidth>\n")
				.Append(tab)
				.Append("\t")
				.Append("<RectHeight>")
				.Append(field_4_rectY2)
				.Append("</RectHeight>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
