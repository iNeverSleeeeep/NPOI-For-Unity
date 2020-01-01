using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The blip record is used to hold details about large binary objects that occur in escher such
	/// as JPEG, GIF, PICT and WMF files.  The contents of the stream is usually compressed.  Inflate
	/// can be used to decompress the data.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherBlipWMFRecord : EscherBlipRecord
	{
		public new const string RECORD_DESCRIPTION = "msofbtBlip";

		private const int HEADER_SIZE = 8;

		private byte[] field_1_secondaryUID;

		private int field_2_cacheOfSize;

		private int field_3_boundaryTop;

		private int field_4_boundaryLeft;

		private int field_5_boundaryWidth;

		private int field_6_boundaryHeight;

		private int field_7_width;

		private int field_8_height;

		private int field_9_cacheOfSavedSize;

		private byte field_10_compressionFlag;

		private byte field_11_filter;

		private byte[] field_12_data;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 58 + field_12_data.Length;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Blip";

		/// <summary>
		/// Gets or sets the secondary UID.
		/// </summary>
		/// <value>The secondary UID.</value>
		public byte[] SecondaryUID
		{
			get
			{
				return field_1_secondaryUID;
			}
			set
			{
				field_1_secondaryUID = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the cache of.
		/// </summary>
		/// <value>The size of the cache of.</value>
		public int CacheOfSize
		{
			get
			{
				return field_2_cacheOfSize;
			}
			set
			{
				field_2_cacheOfSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the top boundary of the metafile drawing commands
		/// </summary>
		/// <value>The boundary top.</value>
		public int BoundaryTop
		{
			get
			{
				return field_3_boundaryTop;
			}
			set
			{
				field_3_boundaryTop = value;
			}
		}

		/// <summary>
		/// Gets or sets the left boundary of the metafile drawing commands
		/// </summary>
		/// <value>The boundary left.</value>
		public int BoundaryLeft
		{
			get
			{
				return field_4_boundaryLeft;
			}
			set
			{
				field_4_boundaryLeft = value;
			}
		}

		/// <summary>
		/// Gets or sets the boundary width of the metafile drawing commands
		/// </summary>
		/// <value>The width of the boundary.</value>
		public int BoundaryWidth
		{
			get
			{
				return field_5_boundaryWidth;
			}
			set
			{
				field_5_boundaryWidth = value;
			}
		}

		/// <summary>
		/// Gets or sets the boundary height of the metafile drawing commands
		/// </summary>
		/// <value>The height of the boundary.</value>
		public int BoundaryHeight
		{
			get
			{
				return field_6_boundaryHeight;
			}
			set
			{
				field_6_boundaryHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the metafile in EMU's (English Metric Units).
		/// </summary>
		/// <value>The width.</value>
		public int Width
		{
			get
			{
				return field_7_width;
			}
			set
			{
				field_7_width = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the metafile in EMU's (English Metric Units).
		/// </summary>
		/// <value>The height.</value>
		public int Height
		{
			get
			{
				return field_8_height;
			}
			set
			{
				field_8_height = value;
			}
		}

		/// <summary>
		/// Gets or sets the cache of the saved size
		/// </summary>
		/// <value>the cache of the saved size.</value>
		public int CacheOfSavedSize
		{
			get
			{
				return field_9_cacheOfSavedSize;
			}
			set
			{
				field_9_cacheOfSavedSize = value;
			}
		}

		/// <summary>
		/// Is the contents of the blip compressed?
		/// </summary>
		/// <value>The compression flag.</value>
		public byte CompressionFlag
		{
			get
			{
				return field_10_compressionFlag;
			}
			set
			{
				field_10_compressionFlag = value;
			}
		}

		/// <summary>
		/// Gets or sets the filter.
		/// </summary>
		/// <value>The filter.</value>
		public byte Filter
		{
			get
			{
				return field_11_filter;
			}
			set
			{
				field_11_filter = value;
			}
		}

		/// <summary>
		/// Gets or sets The BLIP data
		/// </summary>
		/// <value>The data.</value>
		public byte[] Data
		{
			get
			{
				return field_12_data;
			}
			set
			{
				field_12_data = value;
			}
		}

		/// <summary>
		/// This method deserializes the record from a byte array.
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
			int num3 = 0;
			field_1_secondaryUID = new byte[16];
			Array.Copy(data, num2 + num3, field_1_secondaryUID, 0, 16);
			num3 += 16;
			field_2_cacheOfSize = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_3_boundaryTop = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_4_boundaryLeft = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_5_boundaryWidth = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_6_boundaryHeight = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_7_width = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_8_height = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_9_cacheOfSavedSize = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_10_compressionFlag = data[num2 + num3];
			num3++;
			field_11_filter = data[num2 + num3];
			num3++;
			int num4 = num - num3;
			field_12_data = new byte[num4];
			Array.Copy(data, num2 + num3, field_12_data, 0, num4);
			num3 += num4;
			return 8 + num3;
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array.
		/// @param offset   
		/// </summary>
		/// <param name="offset">The offset into data to start writing the record data to.</param>
		/// <param name="data">the data array to Serialize to</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>the number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int value = field_12_data.Length + 36;
			LittleEndian.PutInt(data, offset + 4, value);
			int num = offset + 8;
			Array.Copy(field_1_secondaryUID, 0, data, num, 16);
			num += 16;
			LittleEndian.PutInt(data, num, field_2_cacheOfSize);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_boundaryTop);
			num += 4;
			LittleEndian.PutInt(data, num, field_4_boundaryLeft);
			num += 4;
			LittleEndian.PutInt(data, num, field_5_boundaryWidth);
			num += 4;
			LittleEndian.PutInt(data, num, field_6_boundaryHeight);
			num += 4;
			LittleEndian.PutInt(data, num, field_7_width);
			num += 4;
			LittleEndian.PutInt(data, num, field_8_height);
			num += 4;
			LittleEndian.PutInt(data, num, field_9_cacheOfSavedSize);
			num += 4;
			data[num++] = field_10_compressionFlag;
			data[num++] = field_11_filter;
			Array.Copy(field_12_data, 0, data, num, field_12_data.Length);
			num += field_12_data.Length;
			listener.AfterRecordSerialize(num, RecordId, num - offset, this);
			return num - offset;
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
					HexDump.Dump(field_12_data, 0L, memoryStream, 0);
					empty = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
				catch (Exception ex)
				{
					empty = ex.ToString();
				}
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  Secondary UID: " + HexDump.ToHex(field_1_secondaryUID) + newLine + "  CacheOfSize: " + field_2_cacheOfSize + newLine + "  BoundaryTop: " + field_3_boundaryTop + newLine + "  BoundaryLeft: " + field_4_boundaryLeft + newLine + "  BoundaryWidth: " + field_5_boundaryWidth + newLine + "  BoundaryHeight: " + field_6_boundaryHeight + newLine + "  X: " + field_7_width + newLine + "  Y: " + field_8_height + newLine + "  CacheOfSavedSize: " + field_9_cacheOfSavedSize + newLine + "  CompressionFlag: " + field_10_compressionFlag + newLine + "  Filter: " + field_11_filter + newLine + "  Data:" + newLine + empty;
			}
		}

		public override string ToXml(string tab)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string value;
				try
				{
					HexDump.Dump(field_12_data, 0L, memoryStream, 0);
					value = HexDump.ToHex(memoryStream.ToArray());
				}
				catch (Exception ex)
				{
					value = ex.ToString();
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
					.Append("\t")
					.Append("<SecondaryUID>0x")
					.Append(HexDump.ToHex(field_1_secondaryUID))
					.Append("</SecondaryUID>\n")
					.Append(tab)
					.Append("\t")
					.Append("<CacheOfSize>")
					.Append(field_2_cacheOfSize)
					.Append("</CacheOfSize>\n")
					.Append(tab)
					.Append("\t")
					.Append("<BoundaryTop>")
					.Append(field_3_boundaryTop)
					.Append("</BoundaryTop>\n")
					.Append(tab)
					.Append("\t")
					.Append("<BoundaryLeft>")
					.Append(field_4_boundaryLeft)
					.Append("</BoundaryLeft>\n")
					.Append(tab)
					.Append("\t")
					.Append("<BoundaryWidth>")
					.Append(field_5_boundaryWidth)
					.Append("</BoundaryWidth>\n")
					.Append(tab)
					.Append("\t")
					.Append("<BoundaryHeight>")
					.Append(field_6_boundaryHeight)
					.Append("</BoundaryHeight>\n")
					.Append(tab)
					.Append("\t")
					.Append("<X>")
					.Append(field_7_width)
					.Append("</X>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Y>")
					.Append(field_8_height)
					.Append("</Y>\n")
					.Append(tab)
					.Append("\t")
					.Append("<CacheOfSavedSize>")
					.Append(field_9_cacheOfSavedSize)
					.Append("</CacheOfSavedSize>\n")
					.Append(tab)
					.Append("\t")
					.Append("<CompressionFlag>")
					.Append(field_10_compressionFlag)
					.Append("</CompressionFlag>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Filter>")
					.Append(field_11_filter)
					.Append("</Filter>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Data>")
					.Append(value)
					.Append("</Data>\n");
				stringBuilder.Append(tab).Append("</").Append(GetType().Name)
					.Append(">\n");
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Compress the contents of the provided array
		/// </summary>
		/// <param name="data">An uncompressed byte array</param>
		/// <returns></returns>
		public static byte[] Compress(byte[] data)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Deflater deflater = new Deflater(0, noZlibHeaderOrFooter: false);
				DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(memoryStream, deflater);
				try
				{
					deflaterOutputStream.Write(data, 0, data.Length);
					return memoryStream.ToArray();
				}
				catch (IOException ex)
				{
					throw new RecordFormatException(ex.ToString());
				}
				finally
				{
					memoryStream.Close();
					deflaterOutputStream?.Close();
				}
			}
		}

		/// <summary>
		/// Decompresses the specified data.
		/// </summary>
		/// <param name="data">The compressed byte array.</param>
		/// <param name="pos">The starting position into the byte array.</param>
		/// <param name="Length">The number of compressed bytes to decompress.</param>
		/// <returns>An uncompressed byte array</returns>
		public static byte[] Decompress(byte[] data, int pos, int Length)
		{
			byte[] array = new byte[Length];
			Array.Copy(data, pos + 50, array, 0, Length);
			using (MemoryStream baseInputStream = new MemoryStream(array))
			{
				Inflater inf = new Inflater(noHeader: false);
				using (InflaterInputStream inflaterInputStream = new InflaterInputStream(baseInputStream, inf))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						try
						{
							int num;
							while ((num = inflaterInputStream.ReadByte()) != -1)
							{
								memoryStream.WriteByte((byte)num);
							}
							return memoryStream.ToArray();
						}
						catch (IOException ex)
						{
							throw new RecordFormatException(ex.ToString());
						}
					}
				}
			}
		}
	}
}
