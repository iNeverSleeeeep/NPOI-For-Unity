using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * The font basis record stores various font metrics.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Glen Stampoultzis (glens at apache.org)
	public class FontBasisRecord : StandardRecord
	{
		public const short sid = 4192;

		private short field_1_xBasis;

		private short field_2_yBasis;

		private short field_3_heightBasis;

		private short field_4_scale;

		private short field_5_indexToFontTable;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 10;

		public override short Sid => 4192;

		/// Get the x Basis field for the FontBasis record.
		public short XBasis
		{
			get
			{
				return field_1_xBasis;
			}
			set
			{
				field_1_xBasis = value;
			}
		}

		/// Get the y Basis field for the FontBasis record.
		public short YBasis
		{
			get
			{
				return field_2_yBasis;
			}
			set
			{
				field_2_yBasis = value;
			}
		}

		/// Get the height basis field for the FontBasis record.
		public short HeightBasis
		{
			get
			{
				return field_3_heightBasis;
			}
			set
			{
				field_3_heightBasis = value;
			}
		}

		/// Get the scale field for the FontBasis record.
		public short Scale
		{
			get
			{
				return field_4_scale;
			}
			set
			{
				field_4_scale = value;
			}
		}

		/// Get the index to font table field for the FontBasis record.
		public short IndexToFontTable
		{
			get
			{
				return field_5_indexToFontTable;
			}
			set
			{
				field_5_indexToFontTable = value;
			}
		}

		public FontBasisRecord()
		{
		}

		/// Constructs a FontBasis record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public FontBasisRecord(RecordInputStream in1)
		{
			field_1_xBasis = in1.ReadShort();
			field_2_yBasis = in1.ReadShort();
			field_3_heightBasis = in1.ReadShort();
			field_4_scale = in1.ReadShort();
			field_5_indexToFontTable = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FBI]\n");
			stringBuilder.Append("    .xBasis               = ").Append("0x").Append(HexDump.ToHex(XBasis))
				.Append(" (")
				.Append(XBasis)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .yBasis               = ").Append("0x").Append(HexDump.ToHex(YBasis))
				.Append(" (")
				.Append(YBasis)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .heightBasis          = ").Append("0x").Append(HexDump.ToHex(HeightBasis))
				.Append(" (")
				.Append(HeightBasis)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .scale                = ").Append("0x").Append(HexDump.ToHex(Scale))
				.Append(" (")
				.Append(Scale)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .indexToFontTable     = ").Append("0x").Append(HexDump.ToHex(IndexToFontTable))
				.Append(" (")
				.Append(IndexToFontTable)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/FBI]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_xBasis);
			out1.WriteShort(field_2_yBasis);
			out1.WriteShort(field_3_heightBasis);
			out1.WriteShort(field_4_scale);
			out1.WriteShort(field_5_indexToFontTable);
		}

		public override object Clone()
		{
			FontBasisRecord fontBasisRecord = new FontBasisRecord();
			fontBasisRecord.field_1_xBasis = field_1_xBasis;
			fontBasisRecord.field_2_yBasis = field_2_yBasis;
			fontBasisRecord.field_3_heightBasis = field_3_heightBasis;
			fontBasisRecord.field_4_scale = field_4_scale;
			fontBasisRecord.field_5_indexToFontTable = field_5_indexToFontTable;
			return fontBasisRecord;
		}
	}
}
