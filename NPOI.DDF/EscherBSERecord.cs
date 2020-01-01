using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The BSE record is related closely to the EscherBlipRecord and stores
	/// extra information about the blip.  A blip record is actually stored inside
	/// the BSE record even though the BSE record isn't actually a container record.
	/// @author Glen Stampoultzis
	/// @see EscherBlipRecord
	/// </summary>
	public class EscherBSERecord : EscherRecord
	{
		public const short RECORD_ID = -4089;

		public const string RECORD_DESCRIPTION = "MsofbtBSE";

		public const byte BT_ERROR = 0;

		public const byte BT_UNKNOWN = 1;

		public const byte BT_EMF = 2;

		public const byte BT_WMF = 3;

		public const byte BT_PICT = 4;

		public const byte BT_JPEG = 5;

		public const byte BT_PNG = 6;

		public const byte BT_DIB = 7;

		private byte field_1_blipTypeWin32;

		private byte field_2_blipTypeMacOS;

		private byte[] field_3_uid;

		private short field_4_tag;

		private int field_5_size;

		private int field_6_ref;

		private int field_7_offset;

		private byte field_8_usage;

		private byte field_9_name;

		private byte field_10_unused2;

		private byte field_11_unused3;

		private EscherBlipRecord field_12_blipRecord;

		private byte[] _remainingData;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize
		{
			get
			{
				int num = 0;
				if (field_12_blipRecord != null)
				{
					num = field_12_blipRecord.RecordSize;
				}
				int num2 = 0;
				if (_remainingData != null)
				{
					num2 = _remainingData.Length;
				}
				return 44 + num + num2;
			}
		}

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "BSE";

		/// <summary>
		/// Gets or sets the expected blip type under windows (failure to match this blip type will result in
		/// Excel converting to this format).
		/// </summary>
		/// <value>The blip type win32.</value>
		public byte BlipTypeWin32
		{
			get
			{
				return field_1_blipTypeWin32;
			}
			set
			{
				field_1_blipTypeWin32 = value;
			}
		}

		/// <summary>
		/// Gets or sets the expected blip type under MacOS (failure to match this blip type will result in
		/// Excel converting to this format).
		/// </summary>
		/// <value>The blip type mac OS.</value>
		public byte BlipTypeMacOS
		{
			get
			{
				return field_2_blipTypeMacOS;
			}
			set
			{
				field_2_blipTypeMacOS = value;
			}
		}

		/// <summary>
		/// Gets or sets 16 byte MD4 checksum.
		/// </summary>
		/// <value>The UID.</value>
		public byte[] UID
		{
			get
			{
				return field_3_uid;
			}
			set
			{
				field_3_uid = value;
			}
		}

		/// <summary>
		/// Gets or sets the tag. (Unused)
		/// </summary>
		/// <value>The tag.</value>
		public short Tag
		{
			get
			{
				return field_4_tag;
			}
			set
			{
				field_4_tag = value;
			}
		}

		/// <summary>
		/// Gets or sets Blip size in stream..
		/// </summary>
		/// <value>The size.</value>
		public int Size
		{
			get
			{
				return field_5_size;
			}
			set
			{
				field_5_size = value;
			}
		}

		/// <summary>
		/// Gets or sets the reference count of this blip.
		/// </summary>
		/// <value>The ref.</value>
		public int Ref
		{
			get
			{
				return field_6_ref;
			}
			set
			{
				field_6_ref = value;
			}
		}

		/// <summary>
		/// Gets or sets the offset in the delay stream..
		/// </summary>
		/// <value>The offset.</value>
		public int Offset
		{
			get
			{
				return field_7_offset;
			}
			set
			{
				field_7_offset = value;
			}
		}

		/// <summary>
		/// Defines the way this blip is used.
		/// </summary>
		/// <value>The usage.</value>
		public byte Usage
		{
			get
			{
				return field_8_usage;
			}
			set
			{
				field_8_usage = value;
			}
		}

		/// <summary>
		/// Gets or sets the blip name.
		/// </summary>
		/// <value>The name.</value>
		public byte Name
		{
			get
			{
				return field_9_name;
			}
			set
			{
				field_9_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the unused2.
		/// </summary>
		/// <value>The unused2.</value>
		public byte Unused2
		{
			get
			{
				return field_10_unused2;
			}
			set
			{
				field_10_unused2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the unused3.
		/// </summary>
		/// <value>The unused3.</value>
		public byte Unused3
		{
			get
			{
				return field_11_unused3;
			}
			set
			{
				field_11_unused3 = value;
			}
		}

		/// <summary>
		/// Gets or sets the blip record.
		/// </summary>
		/// <value>The blip record.</value>
		public EscherBlipRecord BlipRecord
		{
			get
			{
				return field_12_blipRecord;
			}
			set
			{
				field_12_blipRecord = value;
			}
		}

		/// <summary>
		/// Gets or sets any remaining data in this record.
		/// </summary>
		/// <value>The remaining data.</value>
		public byte[] RemainingData
		{
			get
			{
				return _remainingData;
			}
			set
			{
				_remainingData = value;
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
			field_1_blipTypeWin32 = data[num2];
			field_2_blipTypeMacOS = data[num2 + 1];
			field_3_uid = new byte[16];
			Array.Copy(data, num2 + 2, field_3_uid, 0, 16);
			field_4_tag = LittleEndian.GetShort(data, num2 + 18);
			field_5_size = LittleEndian.GetInt(data, num2 + 20);
			field_6_ref = LittleEndian.GetInt(data, num2 + 24);
			field_7_offset = LittleEndian.GetInt(data, num2 + 28);
			field_8_usage = data[num2 + 32];
			field_9_name = data[num2 + 33];
			field_10_unused2 = data[num2 + 34];
			field_11_unused3 = data[num2 + 35];
			num -= 36;
			int num3 = 0;
			if (num > 0)
			{
				field_12_blipRecord = (EscherBlipRecord)recordFactory.CreateRecord(data, num2 + 36);
				num3 = field_12_blipRecord.FillFields(data, num2 + 36, recordFactory);
			}
			num2 += 36 + num3;
			num -= num3;
			_remainingData = new byte[num];
			Array.Copy(data, num2, _remainingData, 0, num);
			return num + 8 + 36 + ((field_12_blipRecord != null) ? field_12_blipRecord.RecordSize : 0);
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array.  
		/// </summary>
		/// <param name="offset">The offset into 
		/// data to start writing the record data to</param>
		/// <param name="data">The byte array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			if (_remainingData == null)
			{
				_remainingData = new byte[0];
			}
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			if (_remainingData == null)
			{
				_remainingData = new byte[0];
			}
			int num = (field_12_blipRecord != null) ? field_12_blipRecord.RecordSize : 0;
			int value = _remainingData.Length + 36 + num;
			LittleEndian.PutInt(data, offset + 4, value);
			data[offset + 8] = field_1_blipTypeWin32;
			data[offset + 9] = field_2_blipTypeMacOS;
			for (int i = 0; i < 16; i++)
			{
				data[offset + 10 + i] = field_3_uid[i];
			}
			LittleEndian.PutShort(data, offset + 26, field_4_tag);
			LittleEndian.PutInt(data, offset + 28, field_5_size);
			LittleEndian.PutInt(data, offset + 32, field_6_ref);
			LittleEndian.PutInt(data, offset + 36, field_7_offset);
			data[offset + 40] = field_8_usage;
			data[offset + 41] = field_9_name;
			data[offset + 42] = field_10_unused2;
			data[offset + 43] = field_11_unused3;
			int num2 = 0;
			if (field_12_blipRecord != null)
			{
				num2 = field_12_blipRecord.Serialize(offset + 44, data);
			}
			if (_remainingData == null)
			{
				_remainingData = new byte[0];
			}
			Array.Copy(_remainingData, 0, data, offset + 44 + num2, _remainingData.Length);
			int num3 = offset + 8 + 36 + _remainingData.Length + num2;
			listener.AfterRecordSerialize(num3, RecordId, num3 - offset, this);
			return num3 - offset;
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
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string text;
				try
				{
					HexDump.Dump(_remainingData, 0L, memoryStream, 0);
					text = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
				catch (Exception ex)
				{
					text = ex.ToString();
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4089)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + '\n' + "  Instance: 0x" + HexDump.ToHex(Instance) + '\n' + "  BlipTypeWin32: " + field_1_blipTypeWin32 + newLine + "  BlipTypeMacOS: " + field_2_blipTypeMacOS + newLine + "  SUID: " + HexDump.ToHex(field_3_uid) + newLine + "  Tag: " + field_4_tag + newLine + "  Size: " + field_5_size + newLine + "  Ref: " + field_6_ref + newLine + "  Offset: " + field_7_offset + newLine + "  Usage: " + field_8_usage + newLine + "  Name: " + field_9_name + newLine + "  Unused2: " + field_10_unused2 + newLine + "  Unused3: " + field_11_unused3 + newLine + "  blipRecord: " + ((field_12_blipRecord == null) ? "null" : field_12_blipRecord.ToString()) + newLine + "  Extra Data:" + newLine + text;
			}
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<BlipTypeWin32>")
				.Append(field_1_blipTypeWin32)
				.Append("</BlipTypeWin32>\n")
				.Append(tab)
				.Append("\t")
				.Append("<BlipTypeMacOS>")
				.Append(field_2_blipTypeMacOS)
				.Append("</BlipTypeMacOS>\n")
				.Append(tab)
				.Append("\t")
				.Append("<SUID>")
				.Append((field_3_uid == null) ? "" : HexDump.ToHex(field_3_uid))
				.Append("</SUID>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Tag>")
				.Append(field_4_tag)
				.Append("</Tag>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Size>")
				.Append(field_5_size)
				.Append("</Size>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Ref>")
				.Append(field_6_ref)
				.Append("</Ref>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Offset>")
				.Append(field_7_offset)
				.Append("</Offset>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Usage>")
				.Append(field_8_usage)
				.Append("</Usage>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Name>")
				.Append(field_9_name)
				.Append("</Name>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Unused2>")
				.Append(field_10_unused2)
				.Append("</Unused2>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Unused3>")
				.Append(field_11_unused3)
				.Append("</Unused3>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Retrieve the string representation given a blip id.
		/// </summary>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public string GetBlipType(byte b)
		{
			switch (b)
			{
			case 0:
				return " ERROR";
			case 1:
				return " UNKNOWN";
			case 2:
				return " EMF";
			case 3:
				return " WMF";
			case 4:
				return " PICT";
			case 5:
				return " JPEG";
			case 6:
				return " PNG";
			case 7:
				return " DIB";
			default:
				if (b < 32)
				{
					return " NotKnown";
				}
				return " Client";
			}
		}
	}
}
