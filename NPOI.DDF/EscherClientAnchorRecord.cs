using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// The escher client anchor specifies which rows and cells the shape is bound to as well as
	/// the offsets within those cells.  Each cell is 1024 units wide by 256 units long regardless
	/// of the actual size of the cell.  The EscherClientAnchorRecord only applies to the top-most
	/// shapes.  Shapes contained in groups are bound using the EscherChildAnchorRecords.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherClientAnchorRecord : EscherRecord
	{
		public const short RECORD_ID = -4080;

		public const string RECORD_DESCRIPTION = "MsofbtClientAnchor";

		/// bit[0] -  fMove (1 bit): A bit that specifies whether the shape will be kept intact when the cells are moved.
		/// bit[1] - fSize (1 bit): A bit that specifies whether the shape will be kept intact when the cells are resized. If fMove is 1, the value MUST be 1.
		/// bit[2-4] - reserved, MUST be 0 and MUST be ignored
		/// bit[5-15]- Undefined and MUST be ignored.
		///
		/// it can take values: 0, 2, 3
		private short field_1_flag;

		private short field_2_col1;

		private short field_3_dx1;

		private short field_4_row1;

		private short field_5_dy1;

		private short field_6_col2;

		private short field_7_dx2;

		private short field_8_row2;

		private short field_9_dy2;

		private byte[] remainingData;

		private bool shortRecord;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 8 + (shortRecord ? 8 : 18) + ((remainingData != null) ? remainingData.Length : 0);

		/// <summary>
		/// The record id for this record.
		/// </summary>
		/// <value></value>
		public override short RecordId => -4080;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "ClientAnchor";

		/// <summary>
		/// Gets or sets the flag.
		/// </summary>
		/// <value>0 = Move and size with Cells, 2 = Move but don't size with cells, 3 = Don't move or size with cells.</value>
		public short Flag
		{
			get
			{
				return field_1_flag;
			}
			set
			{
				field_1_flag = value;
			}
		}

		/// <summary>
		/// Gets or sets The column number for the top-left position.  0 based.
		/// </summary>
		/// <value>The col1.</value>
		public short Col1
		{
			get
			{
				return field_2_col1;
			}
			set
			{
				field_2_col1 = value;
			}
		}

		/// <summary>
		/// Gets or sets The x offset within the top-left cell.  Range is from 0 to 1023.
		/// </summary>
		/// <value>The DX1.</value>
		public short Dx1
		{
			get
			{
				return field_3_dx1;
			}
			set
			{
				field_3_dx1 = value;
			}
		}

		/// <summary>
		/// Gets or sets The row number for the top-left corner of the shape.
		/// </summary>
		/// <value>The row1.</value>
		public short Row1
		{
			get
			{
				return field_4_row1;
			}
			set
			{
				field_4_row1 = value;
			}
		}

		/// <summary>
		/// Gets or sets The y offset within the top-left corner of the current shape.
		/// </summary>
		/// <value>The dy1.</value>
		public short Dy1
		{
			get
			{
				return field_5_dy1;
			}
			set
			{
				shortRecord = false;
				field_5_dy1 = value;
			}
		}

		/// <summary>
		/// Gets or sets The column of the bottom right corner of this shape.
		/// </summary>
		/// <value>The col2.</value>
		public short Col2
		{
			get
			{
				return field_6_col2;
			}
			set
			{
				shortRecord = false;
				field_6_col2 = value;
			}
		}

		/// <summary>
		/// Gets or sets The x offset withing the cell for the bottom-right corner of this shape.
		/// </summary>
		/// <value>The DX2.</value>
		public short Dx2
		{
			get
			{
				return field_7_dx2;
			}
			set
			{
				shortRecord = false;
				field_7_dx2 = value;
			}
		}

		/// <summary>
		/// Gets or sets The row number for the bottom-right corner of the current shape.
		/// </summary>
		/// <value>The row2.</value>
		public short Row2
		{
			get
			{
				return field_8_row2;
			}
			set
			{
				shortRecord = false;
				field_8_row2 = value;
			}
		}

		/// <summary>
		/// Gets or sets The y offset withing the cell for the bottom-right corner of this shape.
		/// </summary>
		/// <value>The dy2.</value>
		public short Dy2
		{
			get
			{
				return field_9_dy2;
			}
			set
			{
				field_9_dy2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the remaining data.
		/// </summary>
		/// <value>The remaining data.</value>
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
			int num2 = offset + 8;
			int num3 = 0;
			if (num != 4)
			{
				field_1_flag = LittleEndian.GetShort(data, num2 + num3);
				num3 += 2;
				field_2_col1 = LittleEndian.GetShort(data, num2 + num3);
				num3 += 2;
				field_3_dx1 = LittleEndian.GetShort(data, num2 + num3);
				num3 += 2;
				field_4_row1 = LittleEndian.GetShort(data, num2 + num3);
				num3 += 2;
				if (num >= 18)
				{
					field_5_dy1 = LittleEndian.GetShort(data, num2 + num3);
					num3 += 2;
					field_6_col2 = LittleEndian.GetShort(data, num2 + num3);
					num3 += 2;
					field_7_dx2 = LittleEndian.GetShort(data, num2 + num3);
					num3 += 2;
					field_8_row2 = LittleEndian.GetShort(data, num2 + num3);
					num3 += 2;
					field_9_dy2 = LittleEndian.GetShort(data, num2 + num3);
					num3 += 2;
					shortRecord = false;
				}
				else
				{
					shortRecord = true;
				}
			}
			num -= num3;
			remainingData = new byte[num];
			Array.Copy(data, num2 + num3, remainingData, 0, num);
			return 8 + num3 + num;
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array.
		/// </summary>
		/// <param name="offset">The offset into data to start writing the record data to.</param>
		/// <param name="data">The byte array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			if (remainingData == null)
			{
				remainingData = new byte[0];
			}
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int value = remainingData.Length + (shortRecord ? 8 : 18);
			LittleEndian.PutInt(data, offset + 4, value);
			LittleEndian.PutShort(data, offset + 8, field_1_flag);
			LittleEndian.PutShort(data, offset + 10, field_2_col1);
			LittleEndian.PutShort(data, offset + 12, field_3_dx1);
			LittleEndian.PutShort(data, offset + 14, field_4_row1);
			if (!shortRecord)
			{
				LittleEndian.PutShort(data, offset + 16, field_5_dy1);
				LittleEndian.PutShort(data, offset + 18, field_6_col2);
				LittleEndian.PutShort(data, offset + 20, field_7_dx2);
				LittleEndian.PutShort(data, offset + 22, field_8_row2);
				LittleEndian.PutShort(data, offset + 24, field_9_dy2);
			}
			Array.Copy(remainingData, 0, data, offset + (shortRecord ? 16 : 26), remainingData.Length);
			int num = offset + 8 + (shortRecord ? 8 : 18) + remainingData.Length;
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
				return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4080)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  Flag: " + field_1_flag + newLine + "  Col1: " + field_2_col1 + newLine + "  DX1: " + field_3_dx1 + newLine + "  Row1: " + field_4_row1 + newLine + "  DY1: " + field_5_dy1 + newLine + "  Col2: " + field_6_col2 + newLine + "  DX2: " + field_7_dx2 + newLine + "  Row2: " + field_8_row2 + newLine + "  DY2: " + field_9_dy2 + newLine + "  Extra Data:" + newLine + text;
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
					text = "error\n";
				}
				if (text.Contains("No Data"))
				{
					text = "No Data";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
					.Append("\t")
					.Append("<Flag>")
					.Append(field_1_flag)
					.Append("</Flag>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Col1>")
					.Append(field_2_col1)
					.Append("</Col1>\n")
					.Append(tab)
					.Append("\t")
					.Append("<DX1>")
					.Append(field_3_dx1)
					.Append("</DX1>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Row1>")
					.Append(field_4_row1)
					.Append("</Row1>\n")
					.Append(tab)
					.Append("\t")
					.Append("<DY1>")
					.Append(field_5_dy1)
					.Append("</DY1>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Col2>")
					.Append(field_6_col2)
					.Append("</Col2>\n")
					.Append(tab)
					.Append("\t")
					.Append("<DX2>")
					.Append(field_7_dx2)
					.Append("</DX2>\n")
					.Append(tab)
					.Append("\t")
					.Append("<Row2>")
					.Append(field_8_row2)
					.Append("</Row2>\n")
					.Append(tab)
					.Append("\t")
					.Append("<DY2>")
					.Append(field_9_dy2)
					.Append("</DY2>\n")
					.Append(tab)
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
