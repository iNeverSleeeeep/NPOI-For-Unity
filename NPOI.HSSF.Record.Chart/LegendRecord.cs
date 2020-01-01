using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * Defines a legend for a chart.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class LegendRecord : StandardRecord
	{
		public const short sid = 4117;

		public const byte TYPE_BOTTOM = 0;

		public const byte TYPE_CORNER = 1;

		public const byte TYPE_TOP = 2;

		public const byte TYPE_RIGHT = 3;

		public const byte TYPE_LEFT = 4;

		public const byte TYPE_UNDOCKED = 7;

		public const byte SPACING_CLOSE = 0;

		public const byte SPACING_MEDIUM = 1;

		public const byte SPACING_OPEN = 2;

		private int field_1_xAxisUpperLeft;

		private int field_2_yAxisUpperLeft;

		private int field_3_xSize;

		private int field_4_ySize;

		private byte field_5_type;

		private byte field_6_spacing;

		private short field_7_options;

		private BitField autoPosition = BitFieldFactory.GetInstance(1);

		private BitField autoSeries = BitFieldFactory.GetInstance(2);

		private BitField autoXPositioning = BitFieldFactory.GetInstance(4);

		private BitField autoYPositioning = BitFieldFactory.GetInstance(8);

		private BitField vertical = BitFieldFactory.GetInstance(16);

		private BitField dataTable = BitFieldFactory.GetInstance(32);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 20;

		public override short Sid => 4117;

		/// Get the x axis upper left field for the Legend record.
		public int XAxisUpperLeft
		{
			get
			{
				return field_1_xAxisUpperLeft;
			}
			set
			{
				field_1_xAxisUpperLeft = value;
			}
		}

		/// Get the y axis upper left field for the Legend record.
		public int YAxisUpperLeft
		{
			get
			{
				return field_2_yAxisUpperLeft;
			}
			set
			{
				field_2_yAxisUpperLeft = value;
			}
		}

		/// Get the x size field for the Legend record.
		public int XSize
		{
			get
			{
				return field_3_xSize;
			}
			set
			{
				field_3_xSize = value;
			}
		}

		/// Get the y size field for the Legend record.
		public int YSize
		{
			get
			{
				return field_4_ySize;
			}
			set
			{
				field_4_ySize = value;
			}
		}

		/// Get the type field for the Legend record.
		///
		/// @return  One of 
		///        TYPE_BOTTOM
		///        TYPE_CORNER
		///        TYPE_TOP
		///        TYPE_RIGHT
		///        TYPE_LEFT
		///        TYPE_UNDOCKED
		public byte Type
		{
			get
			{
				return field_5_type;
			}
			set
			{
				field_5_type = value;
			}
		}

		/// Get the spacing field for the Legend record.
		///
		/// @return  One of 
		///        SPACING_CLOSE
		///        SPACING_MEDIUM
		///        SPACING_OPEN
		public byte Spacing
		{
			get
			{
				return field_6_spacing;
			}
			set
			{
				field_6_spacing = value;
			}
		}

		/// Get the options field for the Legend record.
		public short Options
		{
			get
			{
				return field_7_options;
			}
			set
			{
				field_7_options = value;
			}
		}

		/// automatic positioning (1=docked)
		/// @return  the auto position field value.
		public bool IsAutoPosition
		{
			get
			{
				return autoPosition.IsSet(field_7_options);
			}
			set
			{
				field_7_options = autoPosition.SetShortBoolean(field_7_options, value);
			}
		}

		/// excel 5 only (true)
		/// @return  the auto series field value.
		public bool IsAutoSeries
		{
			get
			{
				return autoSeries.IsSet(field_7_options);
			}
			set
			{
				field_7_options = autoSeries.SetShortBoolean(field_7_options, value);
			}
		}

		/// position of legend on the x axis is automatic
		/// @return  the auto x positioning field value.
		public bool IsAutoXPositioning
		{
			get
			{
				return autoXPositioning.IsSet(field_7_options);
			}
			set
			{
				field_7_options = autoXPositioning.SetShortBoolean(field_7_options, value);
			}
		}

		/// position of legend on the y axis is automatic
		/// @return  the auto y positioning field value.
		public bool IsAutoYPositioning
		{
			get
			{
				return autoYPositioning.IsSet(field_7_options);
			}
			set
			{
				field_7_options = autoYPositioning.SetShortBoolean(field_7_options, value);
			}
		}

		/// vertical or horizontal legend (1 or 0 respectively).  Always 0 if not automatic.
		/// @return  the vertical field value.
		public bool IsVertical
		{
			get
			{
				return vertical.IsSet(field_7_options);
			}
			set
			{
				field_7_options = vertical.SetShortBoolean(field_7_options, value);
			}
		}

		/// 1 if chart Contains data table
		/// @return  the data table field value.
		public bool IsDataTable
		{
			get
			{
				return dataTable.IsSet(field_7_options);
			}
			set
			{
				field_7_options = dataTable.SetShortBoolean(field_7_options, value);
			}
		}

		public LegendRecord()
		{
		}

		/// Constructs a Legend record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public LegendRecord(RecordInputStream in1)
		{
			field_1_xAxisUpperLeft = in1.ReadInt();
			field_2_yAxisUpperLeft = in1.ReadInt();
			field_3_xSize = in1.ReadInt();
			field_4_ySize = in1.ReadInt();
			field_5_type = (byte)in1.ReadByte();
			field_6_spacing = (byte)in1.ReadByte();
			field_7_options = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[LEGEND]\n");
			stringBuilder.Append("    .xAxisUpperLeft       = ").Append("0x").Append(HexDump.ToHex(XAxisUpperLeft))
				.Append(" (")
				.Append(XAxisUpperLeft)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .yAxisUpperLeft       = ").Append("0x").Append(HexDump.ToHex(YAxisUpperLeft))
				.Append(" (")
				.Append(YAxisUpperLeft)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .xSize                = ").Append("0x").Append(HexDump.ToHex(XSize))
				.Append(" (")
				.Append(XSize)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .ySize                = ").Append("0x").Append(HexDump.ToHex(YSize))
				.Append(" (")
				.Append(YSize)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .type                 = ").Append("0x").Append(HexDump.ToHex(Type))
				.Append(" (")
				.Append(Type)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .spacing              = ").Append("0x").Append(HexDump.ToHex(Spacing))
				.Append(" (")
				.Append(Spacing)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .autoPosition             = ").Append(IsAutoPosition).Append('\n');
			stringBuilder.Append("         .autoSeries               = ").Append(IsAutoSeries).Append('\n');
			stringBuilder.Append("         .autoXPositioning         = ").Append(IsAutoXPositioning).Append('\n');
			stringBuilder.Append("         .autoYPositioning         = ").Append(IsAutoYPositioning).Append('\n');
			stringBuilder.Append("         .vertical                 = ").Append(IsVertical).Append('\n');
			stringBuilder.Append("         .dataTable                = ").Append(IsDataTable).Append('\n');
			stringBuilder.Append("[/LEGEND]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_xAxisUpperLeft);
			out1.WriteInt(field_2_yAxisUpperLeft);
			out1.WriteInt(field_3_xSize);
			out1.WriteInt(field_4_ySize);
			out1.WriteByte(field_5_type);
			out1.WriteByte(field_6_spacing);
			out1.WriteShort(field_7_options);
		}

		public override object Clone()
		{
			LegendRecord legendRecord = new LegendRecord();
			legendRecord.field_1_xAxisUpperLeft = field_1_xAxisUpperLeft;
			legendRecord.field_2_yAxisUpperLeft = field_2_yAxisUpperLeft;
			legendRecord.field_3_xSize = field_3_xSize;
			legendRecord.field_4_ySize = field_4_ySize;
			legendRecord.field_5_type = field_5_type;
			legendRecord.field_6_spacing = field_6_spacing;
			legendRecord.field_7_options = field_7_options;
			return legendRecord;
		}
	}
}
