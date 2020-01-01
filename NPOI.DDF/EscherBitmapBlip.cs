using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// @author Glen Stampoultzis
	/// @version $Id: EscherBitmapBlip.java 569827 2007-08-26 15:26:29Z yegor $
	/// </summary>
	public class EscherBitmapBlip : EscherBlipRecord
	{
		public const short RECORD_ID_JPEG = -4067;

		public const short RECORD_ID_PNG = -4066;

		public const short RECORD_ID_DIB = -4065;

		private const int HEADER_SIZE = 8;

		private byte[] field_1_UID;

		private byte field_2_marker = byte.MaxValue;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value> Number of bytes</value>
		public override int RecordSize => 25 + field_pictureData.Length;

		/// <summary>
		/// Gets or sets the UID.
		/// </summary>
		/// <value>The UID.</value>
		public byte[] UID
		{
			get
			{
				return field_1_UID;
			}
			set
			{
				field_1_UID = value;
			}
		}

		/// <summary>
		/// Gets or sets the marker.
		/// </summary>
		/// <value>The marker.</value>
		public byte Marker
		{
			get
			{
				return field_2_marker;
			}
			set
			{
				field_2_marker = value;
			}
		}

		/// <summary>
		/// This method deSerializes the record from a byte array.    
		/// </summary>
		/// <param name="data"> The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into </param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = offset + 8;
			field_1_UID = new byte[16];
			Array.Copy(data, num2, field_1_UID, 0, 16);
			num2 += 16;
			field_2_marker = data[num2];
			num2++;
			field_pictureData = new byte[num - 17];
			Array.Copy(data, num2, field_pictureData, 0, field_pictureData.Length);
			return num + 8;
		}

		/// <summary>
		/// Serializes the record to an existing byte array.
		/// </summary>
		/// <param name="offset">the offset within the byte array</param>
		/// <param name="data">the data array to Serialize to</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>the number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			LittleEndian.PutInt(data, offset + 4, RecordSize - 8);
			int num = offset + 8;
			Array.Copy(field_1_UID, 0, data, num, 16);
			data[num + 16] = field_2_marker;
			Array.Copy(field_pictureData, 0, data, num + 17, field_pictureData.Length);
			listener.AfterRecordSerialize(offset + RecordSize, RecordId, RecordSize, this);
			return 25 + field_pictureData.Length;
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string text;
				try
				{
					HexDump.Dump(field_pictureData, 0L, memoryStream, 0);
					text = HexDump.ToHex(memoryStream.ToArray());
				}
				catch (Exception ex)
				{
					text = ex.ToString();
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  UID: 0x" + HexDump.ToHex(field_1_UID) + newLine + "  Marker: 0x" + HexDump.ToHex(field_2_marker) + newLine + "  Extra Data:" + newLine + text;
			}
		}

		public override string ToXml(string tab)
		{
			string value;
			try
			{
				value = HexDump.ToHex(field_pictureData);
			}
			catch (Exception ex)
			{
				value = ex.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<UID>0x")
				.Append(HexDump.ToHex(field_1_UID))
				.Append("</UID>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Marker>0x")
				.Append(HexDump.ToHex(field_2_marker))
				.Append("</Marker>\n")
				.Append(tab)
				.Append("\t")
				.Append("<ExtraData>")
				.Append(value)
				.Append("</ExtraData>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
