using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// A list of the most recently used colours for the drawings contained in
	/// this document.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherSplitMenuColorsRecord : EscherRecord
	{
		public const short RECORD_ID = -3810;

		public const string RECORD_DESCRIPTION = "MsofbtSplitMenuColors";

		private int field_1_color1;

		private int field_2_color2;

		private int field_3_color3;

		private int field_4_color4;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>number of bytes</value>
		public override int RecordSize => 24;

		/// <summary>
		/// Return the current record id.
		/// </summary>
		/// <value>the 16 bit identifer for this record.</value>
		public override short RecordId => -3810;

		/// <summary>
		/// Gets the short name for this record
		/// </summary>
		/// <value>The name of the record.</value>
		public override string RecordName => "SplitMenuColors";

		/// <summary>
		/// Gets or sets the color1.
		/// </summary>
		/// <value>The color1.</value>
		public int Color1
		{
			get
			{
				return field_1_color1;
			}
			set
			{
				field_1_color1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the color2.
		/// </summary>
		/// <value>The color2.</value>
		public int Color2
		{
			get
			{
				return field_2_color2;
			}
			set
			{
				field_2_color2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the color3.
		/// </summary>
		/// <value>The color3.</value>
		public int Color3
		{
			get
			{
				return field_3_color3;
			}
			set
			{
				field_3_color3 = value;
			}
		}

		/// <summary>
		/// Gets or sets the color4.
		/// </summary>
		/// <value>The color4.</value>
		public int Color4
		{
			get
			{
				return field_4_color4;
			}
			set
			{
				field_4_color4 = value;
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
			field_1_color1 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_2_color2 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_3_color3 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_4_color4 = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			num -= num3;
			if (num != 0)
			{
				throw new RecordFormatException("Expecting no remaining data but got " + num + " byte(s).");
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
			int num = offset + 2;
			LittleEndian.PutShort(data, num, RecordId);
			num += 2;
			int value = RecordSize - 8;
			LittleEndian.PutInt(data, num, value);
			num += 4;
			LittleEndian.PutInt(data, num, field_1_color1);
			num += 4;
			LittleEndian.PutInt(data, num, field_2_color2);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_color3);
			num += 4;
			LittleEndian.PutInt(data, num, field_4_color4);
			num += 4;
			listener.AfterRecordSerialize(num, RecordId, num - offset, this);
			return RecordSize;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		/// @return  a string representation of this record.
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-3810)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  Color1: 0x" + HexDump.ToHex(field_1_color1) + newLine + "  Color2: 0x" + HexDump.ToHex(field_2_color2) + newLine + "  Color3: 0x" + HexDump.ToHex(field_3_color3) + newLine + "  Color4: 0x" + HexDump.ToHex(field_4_color4) + newLine;
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<Color1>0x")
				.Append(HexDump.ToHex(field_1_color1))
				.Append("</Color1>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Color2>0x")
				.Append(HexDump.ToHex(field_2_color2))
				.Append("</Color2>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Color3>0x")
				.Append(HexDump.ToHex(field_3_color3))
				.Append("</Color3>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Color4>0x")
				.Append(HexDump.ToHex(field_4_color4))
				.Append("</Color4>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
