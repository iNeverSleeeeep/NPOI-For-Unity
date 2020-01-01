using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NPOI.Util;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// @author Daniel Noll
	/// </summary>
	public class EscherMetafileBlip : EscherBlipRecord
	{
		public const short RECORD_ID_EMF = -4070;

		public const short RECORD_ID_WMF = -4069;

		public const short RECORD_ID_PICT = -4068;

		/// BLIP signatures as defined in the escher spec
		public const short SIGNATURE_EMF = 15680;

		public const short SIGNATURE_WMF = 8544;

		public const short SIGNATURE_PICT = 21536;

		private const int HEADER_SIZE = 8;

		private static POILogger log = POILogFactory.GetLogger(typeof(EscherMetafileBlip));

		private byte[] field_1_UID;

		/// The primary UID is only saved to disk if (blip_instance ^ blip_signature == 1)
		private byte[] field_2_UID;

		private int field_2_cb;

		private int field_3_rcBounds_x1;

		private int field_3_rcBounds_y1;

		private int field_3_rcBounds_x2;

		private int field_3_rcBounds_y2;

		private int field_4_ptSize_w;

		private int field_4_ptSize_h;

		private int field_5_cbSave;

		private byte field_6_fCompression;

		private byte field_7_fFilter;

		private byte[] raw_pictureData;

		private byte[] remainingData;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize
		{
			get
			{
				int num = 58 + raw_pictureData.Length;
				if (remainingData != null)
				{
					num += remainingData.Length;
				}
				if ((Options ^ Signature) == 16)
				{
					num += field_2_UID.Length;
				}
				return num;
			}
		}

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
		/// Gets or sets the primary UID.
		/// </summary>
		/// <value>The primary UID.</value>
		public byte[] PrimaryUID
		{
			get
			{
				return field_2_UID;
			}
			set
			{
				field_2_UID = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the uncompressed.
		/// </summary>
		/// <value>The size of the uncompressed.</value>
		public int UncompressedSize
		{
			get
			{
				return field_2_cb;
			}
			set
			{
				field_2_cb = value;
			}
		}

		/// <summary>
		/// Gets or sets the bounds.
		/// </summary>
		/// <value>The bounds.</value>
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(field_3_rcBounds_x1, field_3_rcBounds_y1, field_3_rcBounds_x2 - field_3_rcBounds_x1, field_3_rcBounds_y2 - field_3_rcBounds_y1);
			}
			set
			{
				field_3_rcBounds_x1 = value.X;
				field_3_rcBounds_y1 = value.Y;
				field_3_rcBounds_x2 = value.X + value.Width;
				field_3_rcBounds_y2 = value.Y + value.Height;
			}
		}

		/// <summary>
		/// Gets or sets the size EMU.
		/// </summary>
		/// <value>The size EMU.</value>
		public Size SizeEMU
		{
			get
			{
				return new Size(field_4_ptSize_w, field_4_ptSize_h);
			}
			set
			{
				field_4_ptSize_w = value.Width;
				field_4_ptSize_h = value.Height;
			}
		}

		/// <summary>
		/// Gets or sets the size of the compressed.
		/// </summary>
		/// <value>The size of the compressed.</value>
		public int CompressedSize
		{
			get
			{
				return field_5_cbSave;
			}
			set
			{
				field_5_cbSave = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is compressed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is compressed; otherwise, <c>false</c>.
		/// </value>
		public bool IsCompressed
		{
			get
			{
				return field_6_fCompression == 0;
			}
			set
			{
				field_6_fCompression = (byte)((!value) ? 254 : 0);
			}
		}

		public byte[] RemainingData => remainingData;

		/// <summary>
		/// Return the blip signature
		/// </summary>
		/// <value>the blip signature</value>
		public short Signature
		{
			get
			{
				short result = 0;
				switch (RecordId)
				{
				case -4070:
					result = 15680;
					break;
				case -4069:
					result = 8544;
					break;
				case -4068:
					result = 21536;
					break;
				default:
					log.Log(5, "Unknown metafile: " + RecordId);
					break;
				}
				return result;
			}
		}

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into</param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>
		/// The number of bytes Read from the byte array.
		/// </returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = offset + 8;
			field_1_UID = new byte[16];
			Array.Copy(data, num2, field_1_UID, 0, 16);
			num2 += 16;
			if ((Options ^ Signature) == 16)
			{
				field_2_UID = new byte[16];
				Array.Copy(data, num2, field_2_UID, 0, 16);
				num2 += 16;
			}
			field_2_cb = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_3_rcBounds_x1 = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_3_rcBounds_y1 = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_3_rcBounds_x2 = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_3_rcBounds_y2 = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_4_ptSize_w = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_4_ptSize_h = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_5_cbSave = LittleEndian.GetInt(data, num2);
			num2 += 4;
			field_6_fCompression = data[num2];
			num2++;
			field_7_fFilter = data[num2];
			num2++;
			raw_pictureData = new byte[field_5_cbSave];
			Array.Copy(data, num2, raw_pictureData, 0, field_5_cbSave);
			num2 += field_5_cbSave;
			if (field_6_fCompression == 0)
			{
				field_pictureData = InflatePictureData(raw_pictureData);
			}
			else
			{
				field_pictureData = raw_pictureData;
			}
			int num3 = num - num2 + offset + 8;
			if (num3 > 0)
			{
				remainingData = new byte[num3];
				Array.Copy(data, num2, remainingData, 0, num3);
			}
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
			int num = offset + 2;
			LittleEndian.PutShort(data, num, RecordId);
			num += 2;
			LittleEndian.PutInt(data, num, RecordSize - 8);
			num += 4;
			Array.Copy(field_1_UID, 0, data, num, field_1_UID.Length);
			num += field_1_UID.Length;
			if ((Options ^ Signature) == 16)
			{
				Array.Copy(field_2_UID, 0, data, num, field_2_UID.Length);
				num += field_2_UID.Length;
			}
			LittleEndian.PutInt(data, num, field_2_cb);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_rcBounds_x1);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_rcBounds_y1);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_rcBounds_x2);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_rcBounds_y2);
			num += 4;
			LittleEndian.PutInt(data, num, field_4_ptSize_w);
			num += 4;
			LittleEndian.PutInt(data, num, field_4_ptSize_h);
			num += 4;
			LittleEndian.PutInt(data, num, field_5_cbSave);
			num += 4;
			data[num] = field_6_fCompression;
			num++;
			data[num] = field_7_fFilter;
			num++;
			Array.Copy(raw_pictureData, 0, data, num, raw_pictureData.Length);
			num += raw_pictureData.Length;
			if (remainingData != null)
			{
				Array.Copy(remainingData, 0, data, num, remainingData.Length);
				num += remainingData.Length;
			}
			listener.AfterRecordSerialize(offset + RecordSize, RecordId, RecordSize, this);
			return RecordSize;
		}

		/// <summary>
		/// Decompresses the provided data, returning the inflated result.
		/// </summary>
		/// <param name="data">the deflated picture data.</param>
		/// <returns>the inflated picture data.</returns>
		private static byte[] InflatePictureData(byte[] data)
		{
			using (MemoryStream baseInputStream = new MemoryStream(data))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					InflaterInputStream inflaterInputStream = null;
					try
					{
						Inflater inf = new Inflater(noHeader: false);
						inflaterInputStream = new InflaterInputStream(baseInputStream, inf);
						byte[] array = new byte[4096];
						int count;
						while ((count = inflaterInputStream.Read(array, 0, array.Length)) > 0)
						{
							memoryStream.Write(array, 0, count);
						}
						return memoryStream.ToArray();
					}
					catch (IOException exception)
					{
						log.Log(5, "Possibly corrupt compression or non-compressed data", exception);
						return data;
					}
				}
			}
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
					HexDump.Dump(field_pictureData, 0L, memoryStream, 0);
					text = memoryStream.ToString();
				}
				catch (Exception ex)
				{
					text = ex.ToString();
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + '\n' + "  Instance: 0x" + HexDump.ToHex(Instance) + '\n' + "  UID: 0x" + HexDump.ToHex(field_1_UID) + newLine + ((field_2_UID == null) ? "" : ("  UID2: 0x" + HexDump.ToHex(field_2_UID) + newLine)) + "  Uncompressed Size: " + HexDump.ToHex(field_2_cb) + newLine + "  Bounds: " + Bounds + newLine + "  Size in EMU: " + SizeEMU + newLine + "  Compressed Size: " + HexDump.ToHex(field_5_cbSave) + newLine + "  Compression: " + HexDump.ToHex(field_6_fCompression) + newLine + "  Filter: " + HexDump.ToHex(field_7_fFilter) + newLine + "  Extra Data:" + newLine + text + ((remainingData == null) ? null : ("\n Remaining Data: " + HexDump.ToHex(remainingData, 32)));
			}
		}

		public override string ToXml(string tab)
		{
			string value = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<UID>0x")
				.Append(HexDump.ToHex(field_1_UID) + '\n' + ((field_2_UID == null) ? "" : ("  UID2: 0x" + HexDump.ToHex(field_2_UID) + '\n')))
				.Append("</UID>\n")
				.Append(tab)
				.Append("\t")
				.Append("<UncompressedSize>0x")
				.Append(HexDump.ToHex(field_2_cb))
				.Append("</UncompressedSize>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Bounds>")
				.Append(Bounds)
				.Append("</Bounds>\n")
				.Append(tab)
				.Append("\t")
				.Append("<SizeInEMU>")
				.Append(SizeEMU)
				.Append("</SizeInEMU>\n")
				.Append(tab)
				.Append("\t")
				.Append("<CompressedSize>0x")
				.Append(HexDump.ToHex(field_5_cbSave))
				.Append("</CompressedSize>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Compression>0x")
				.Append(HexDump.ToHex(field_6_fCompression))
				.Append("</Compression>\n")
				.Append(tab)
				.Append("\t")
				.Append("<Filter>0x")
				.Append(HexDump.ToHex(field_7_fFilter))
				.Append("</Filter>\n")
				.Append(tab)
				.Append("\t")
				.Append("<ExtraData>")
				.Append(value)
				.Append("</ExtraData>\n")
				.Append(tab)
				.Append("\t")
				.Append("<RemainingData>0x")
				.Append(HexDump.ToHex(remainingData, 32))
				.Append("</RemainingData>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
