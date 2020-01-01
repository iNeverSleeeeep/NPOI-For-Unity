using NPOI.SS.Formula.PTG;
using NPOI.Util;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// A sub-record within the OBJ record which stores a reference to an object
	/// stored in a Separate entry within the OLE2 compound file.
	///
	/// @author Daniel Noll
	public class EmbeddedObjectRefSubRecord : SubRecord
	{
		public const short sid = 9;

		private static POILogger logger = POILogFactory.GetLogger(typeof(EmbeddedObjectRefSubRecord));

		private static byte[] EMPTY_BYTE_ARRAY = new byte[0];

		private int field_1_unknown_int;

		/// either an area or a cell ref 
		private Ptg field_2_refPtg;

		private byte[] field_2_unknownFormulaData;

		public bool field_3_unicode_flag;

		private string field_4_ole_classname;

		/// Formulas often have a single non-zero trailing byte.
		/// This is in a similar position to he pre-streamId padding
		/// It is unknown if the value is important (it seems to mirror a value a few bytes earlier) 
		private byte? field_4_unknownByte;

		private int? field_5_stream_id;

		private byte[] field_6_unknown;

		public override short Sid => 9;

		public override int DataSize
		{
			get
			{
				int formulaSize = (field_2_refPtg == null) ? field_2_unknownFormulaData.Length : field_2_refPtg.Size;
				int streamIDOffset = GetStreamIDOffset(formulaSize);
				return GetDataSize(streamIDOffset);
			}
		}

		/// Gets the stream ID containing the actual data.  The data itself
		/// can be found under a top-level directory entry in the OLE2 filesystem
		/// under the name "MBD<var>xxxxxxxx</var>" where <var>xxxxxxxx</var> is
		/// this ID converted into hex (in big endian order, funnily enough.)
		///
		/// @return the data stream ID. Possibly <c>null</c>
		public int? StreamId => field_5_stream_id;

		public string OLEClassName => field_4_ole_classname;

		public byte[] ObjectData => field_6_unknown;

		public EmbeddedObjectRefSubRecord()
		{
			field_2_unknownFormulaData = new byte[5]
			{
				2,
				108,
				106,
				22,
				1
			};
			field_6_unknown = EMPTY_BYTE_ARRAY;
			field_4_ole_classname = null;
			field_4_unknownByte = null;
		}

		/// Constructs an EmbeddedObjectRef record and Sets its fields appropriately.
		///
		/// @param in the record input stream.
		public EmbeddedObjectRefSubRecord(ILittleEndianInput in1, int size)
		{
			int num = in1.ReadShort();
			int num2 = size - 2;
			int num3 = num2 - num;
			int num4 = in1.ReadUShort();
			num2 -= 2;
			field_1_unknown_int = in1.ReadInt();
			num2 -= 4;
			byte[] formulaRawBytes = ReadRawData(in1, num4);
			num2 -= num4;
			field_2_refPtg = ReadRefPtg(formulaRawBytes);
			if (field_2_refPtg == null)
			{
				field_2_unknownFormulaData = formulaRawBytes;
			}
			else
			{
				field_2_unknownFormulaData = null;
			}
			int num6;
			if (num2 >= num3 + 3)
			{
				int num5 = in1.ReadByte();
				num6 = 1;
				if (num5 != 3)
				{
					throw new RecordFormatException("Expected byte 0x03 here");
				}
				int num7 = in1.ReadUShort();
				num6 += 2;
				if (num7 > 0)
				{
					field_3_unicode_flag = ((in1.ReadByte() & 1) != 0);
					num6++;
					if (field_3_unicode_flag)
					{
						field_4_ole_classname = StringUtil.ReadUnicodeLE(in1, num7);
						num6 += num7 * 2;
					}
					else
					{
						field_4_ole_classname = StringUtil.ReadCompressedUnicode(in1, num7);
						num6 += num7;
					}
				}
				else
				{
					field_4_ole_classname = "";
				}
			}
			else
			{
				field_4_ole_classname = null;
				num6 = 0;
			}
			num2 -= num6;
			if ((num6 + num4) % 2 != 0)
			{
				int num8 = in1.ReadByte();
				num2--;
				if (field_2_refPtg != null && field_4_ole_classname == null)
				{
					field_4_unknownByte = (byte)num8;
				}
			}
			int num9 = num2 - num3;
			if (num9 > 0)
			{
				logger.Log(7, "Discarding " + num9 + " unexpected padding bytes ");
				ReadRawData(in1, num9);
				num2 -= num9;
			}
			if (num3 >= 4)
			{
				field_5_stream_id = in1.ReadInt();
				num2 -= 4;
			}
			else
			{
				field_5_stream_id = null;
			}
			field_6_unknown = ReadRawData(in1, num2);
		}

		private static Ptg ReadRefPtg(byte[] formulaRawBytes)
		{
			using (MemoryStream @is = new MemoryStream(formulaRawBytes))
			{
				ILittleEndianInput littleEndianInput = new LittleEndianInputStream(@is);
				switch ((byte)littleEndianInput.ReadByte())
				{
				case 37:
					return new AreaPtg(littleEndianInput);
				case 59:
					return new Area3DPtg(littleEndianInput);
				case 36:
					return new RefPtg(littleEndianInput);
				case 58:
					return new Ref3DPtg(littleEndianInput);
				default:
					return null;
				}
			}
		}

		private static byte[] ReadRawData(ILittleEndianInput in1, int size)
		{
			if (size < 0)
			{
				throw new ArgumentException("Negative size (" + size + ")");
			}
			if (size == 0)
			{
				return EMPTY_BYTE_ARRAY;
			}
			byte[] array = new byte[size];
			in1.ReadFully(array);
			return array;
		}

		private int GetStreamIDOffset(int formulaSize)
		{
			int num = 6;
			num += formulaSize;
			if (field_4_ole_classname == null)
			{
				int num2 = 0;
			}
			else
			{
				num += 3;
				int num2 = field_4_ole_classname.Length;
				if (num2 > 0)
				{
					num++;
					num = ((!field_3_unicode_flag) ? (num + num2) : (num + num2 * 2));
				}
			}
			if (num % 2 != 0)
			{
				num++;
			}
			return num;
		}

		private int GetDataSize(int idOffset)
		{
			int num = 2 + idOffset;
			if (field_5_stream_id.HasValue)
			{
				num += 4;
			}
			return num + field_6_unknown.Length;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			int num = (field_2_refPtg == null) ? field_2_unknownFormulaData.Length : field_2_refPtg.Size;
			int streamIDOffset = GetStreamIDOffset(num);
			int dataSize = GetDataSize(streamIDOffset);
			out1.WriteShort(9);
			out1.WriteShort(dataSize);
			out1.WriteShort(streamIDOffset);
			out1.WriteShort(num);
			out1.WriteInt(field_1_unknown_int);
			int num2 = 12;
			if (field_2_refPtg == null)
			{
				out1.Write(field_2_unknownFormulaData);
			}
			else
			{
				field_2_refPtg.Write(out1);
			}
			num2 += num;
			if (field_4_ole_classname == null)
			{
				int num3 = 0;
			}
			else
			{
				out1.WriteByte(3);
				num2++;
				int num3 = field_4_ole_classname.Length;
				out1.WriteShort(num3);
				num2 += 2;
				if (num3 > 0)
				{
					out1.WriteByte(field_3_unicode_flag ? 1 : 0);
					num2++;
					if (field_3_unicode_flag)
					{
						StringUtil.PutUnicodeLE(field_4_ole_classname, out1);
						num2 += num3 * 2;
					}
					else
					{
						StringUtil.PutCompressedUnicode(field_4_ole_classname, out1);
						num2 += num3;
					}
				}
			}
			switch (streamIDOffset - (num2 - 6))
			{
			case 1:
				out1.WriteByte(((int?)field_4_unknownByte).HasValue ? Convert.ToByte(field_4_unknownByte, CultureInfo.InvariantCulture) : 0);
				num2++;
				break;
			default:
				throw new InvalidOperationException("Bad padding calculation (" + streamIDOffset + ", " + num2 + ")");
			case 0:
				break;
			}
			if (field_5_stream_id.HasValue)
			{
				out1.WriteInt(Convert.ToInt32(field_5_stream_id, CultureInfo.InvariantCulture));
				num2 += 4;
			}
			out1.Write(field_6_unknown);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ftPictFmla]\n");
			stringBuilder.Append("    .f2unknown     = ").Append(HexDump.IntToHex(field_1_unknown_int)).Append("\n");
			if (field_2_refPtg == null)
			{
				stringBuilder.Append("    .f3unknown     = ").Append(HexDump.ToHex(field_2_unknownFormulaData)).Append("\n");
			}
			else
			{
				stringBuilder.Append("    .formula       = ").Append(field_2_refPtg.ToString()).Append("\n");
			}
			if (field_4_ole_classname != null)
			{
				stringBuilder.Append("    .unicodeFlag   = ").Append(field_3_unicode_flag).Append("\n");
				stringBuilder.Append("    .oleClassname  = ").Append(field_4_ole_classname).Append("\n");
			}
			if (((int?)field_4_unknownByte).HasValue)
			{
				stringBuilder.Append("    .f4unknown   = ").Append(HexDump.ByteToHex(Convert.ToByte(field_4_unknownByte, CultureInfo.InvariantCulture))).Append("\n");
			}
			if (field_5_stream_id.HasValue)
			{
				stringBuilder.Append("    .streamId      = ").Append(HexDump.IntToHex(Convert.ToInt32(field_5_stream_id, CultureInfo.InvariantCulture))).Append("\n");
			}
			if (field_6_unknown.Length > 0)
			{
				stringBuilder.Append("    .f7unknown     = ").Append(HexDump.ToHex(field_6_unknown)).Append("\n");
			}
			stringBuilder.Append("[/ftPictFmla]");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return this;
		}
	}
}
