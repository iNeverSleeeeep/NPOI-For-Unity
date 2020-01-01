using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The EscherClientDataRecord is used to store client specific data about the position of a
	/// shape within a container.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherClientDataRecord : EscherRecord
	{
		public const short RECORD_ID = -4079;

		public const string RECORD_DESCRIPTION = "MsofbtClientData";

		private byte[] remainingData;

		/// Returns the number of bytes that are required to Serialize this record.
		///
		/// @return Number of bytes
		public override int RecordSize => 8 + ((remainingData != null) ? remainingData.Length : 0);

		/// Returns the identifier of this record.
		public override short RecordId => -4079;

		/// The short name for this record
		public override string RecordName => "ClientData";

		/// Any data recording this record.
		public byte[] RemainingData
		{
			get
			{
				return remainingData;
			}
			set
			{
				remainingData = value;
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
			int sourceIndex = offset + 8;
			remainingData = new byte[num];
			Array.Copy(data, sourceIndex, remainingData, 0, num);
			return 8 + num;
		}

		/// This method Serializes this escher record into a byte array.
		///
		/// @param offset   The offset into <c>data</c> to start writing the record data to.
		/// @param data     The byte array to Serialize to.
		/// @param listener A listener to retrieve start and end callbacks.  Use a <c>NullEscherSerailizationListener</c> to ignore these events.
		/// @return The number of bytes written.
		/// @see NullEscherSerializationListener
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			if (remainingData == null)
			{
				remainingData = new byte[0];
			}
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			LittleEndian.PutInt(data, offset + 4, remainingData.Length);
			Array.Copy(remainingData, 0, data, offset + 8, remainingData.Length);
			int num = offset + 8 + remainingData.Length;
			listener.AfterRecordSerialize(num, RecordId, num - offset, this);
			return num - offset;
		}

		/// Returns the string representation of this record.
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string text;
				try
				{
					HexDump.Dump(remainingData, 0L, memoryStream, 0);
					text = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
				catch (Exception)
				{
					text = "error\n";
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4079)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  Extra Data:" + newLine + text;
			}
		}

		public override string ToXml(string tab)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string text;
				try
				{
					HexDump.Dump(remainingData, 0L, memoryStream, 0);
					text = HexDump.ToHex(memoryStream.ToArray());
				}
				catch (Exception)
				{
					text = "error";
				}
				if (text.Contains("No Data"))
				{
					text = "No Data";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
					.Append("\t")
					.Append("<ExtraData>")
					.Append(text)
					.Append("</ExtraData>\n");
				stringBuilder.Append(tab).Append("</").Append(GetType().Name)
					.Append(">\n");
				return stringBuilder.ToString();
			}
		}
	}
}
