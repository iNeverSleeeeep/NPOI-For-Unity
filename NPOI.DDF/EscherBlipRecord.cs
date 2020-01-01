using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// @author Glen Stampoultzis
	/// @version $Id: EscherBlipRecord.java 569827 2007-08-26 15:26:29Z yegor $
	/// </summary>
	public class EscherBlipRecord : EscherRecord
	{
		public const short RECORD_ID_START = -4072;

		public const short RECORD_ID_END = -3817;

		public const string RECORD_DESCRIPTION = "msofbtBlip";

		private const int HEADER_SIZE = 8;

		protected byte[] field_pictureData;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => field_pictureData.Length + 8;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Blip";

		/// <summary>
		/// Gets or sets the picture data.
		/// </summary>
		/// <value>The picture data.</value>
		public byte[] PictureData
		{
			get
			{
				return field_pictureData;
			}
			set
			{
				field_pictureData = value;
			}
		}

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into </param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int sourceIndex = offset + 8;
			field_pictureData = new byte[num];
			Array.Copy(data, sourceIndex, field_pictureData, 0, num);
			return num + 8;
		}

		/// <summary>
		/// Serializes the record to an existing byte array.
		/// </summary>
		/// <param name="offset"> the offset within the byte array</param>
		/// <param name="data">the data array to Serialize to</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>the number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			Array.Copy(field_pictureData, 0, data, offset + 4, field_pictureData.Length);
			listener.AfterRecordSerialize(offset + 4 + field_pictureData.Length, RecordId, field_pictureData.Length + 4, this);
			return field_pictureData.Length + 4;
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
			string empty = string.Empty;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					HexDump.Dump(field_pictureData, 0L, memoryStream, 0);
					empty = HexDump.ToHex(memoryStream.ToArray());
				}
				catch (Exception ex)
				{
					empty = ex.ToString();
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  Options: 0x" + HexDump.ToHex(Options) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  Extra Data:" + newLine + empty;
			}
		}

		public override string ToXml(string tab)
		{
			string value = HexDump.ToHex(field_pictureData, 32);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
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
