using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// specifies the color, size, and shape of the associated data markers that appear on line, radar, 
	/// and scatter chart groups. The associated data markers are specified by the preceding DataFormat record.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class MarkerFormatRecord : StandardRecord
	{
		public const short sid = 4105;

		private int field_1_rgbFore;

		private int field_2_rgbBack;

		private short field_3_imk;

		private short field_4_flag;

		private short field_5_icvFore;

		private short field_6_icvBack;

		private int field_7_miSize;

		private BitField fAuto = BitFieldFactory.GetInstance(1);

		private BitField fNotShowInt = BitFieldFactory.GetInstance(16);

		private BitField fNotShowBrd = BitFieldFactory.GetInstance(32);

		protected override int DataSize => 20;

		public override short Sid => 4105;

		/// <summary>
		/// the border color of the data marker.
		/// </summary>
		public int RGBFore
		{
			get
			{
				return field_1_rgbFore;
			}
			set
			{
				field_1_rgbFore = value;
			}
		}

		/// <summary>
		/// the interior color of the data marker.
		/// </summary>
		public int RGBBack
		{
			get
			{
				return field_2_rgbBack;
			}
			set
			{
				field_2_rgbBack = value;
			}
		}

		/// <summary>
		/// the type of data marker.
		/// </summary>
		public short DataMarkerType
		{
			get
			{
				return field_3_imk;
			}
			set
			{
				field_3_imk = value;
			}
		}

		/// <summary>
		/// whether the data marker is automatically generated.
		/// false The data marker is not automatically generated.
		/// true  The data marker type, size, and color are automatically generated and the values are set accordingly in this record.
		/// </summary>
		public bool Auto
		{
			get
			{
				return fAuto.IsSet(field_4_flag);
			}
			set
			{
				field_4_flag = fAuto.SetShortBoolean(field_4_flag, value);
			}
		}

		/// <summary>
		/// whether to show the data marker interior.
		/// false  The data marker interior is shown.
		/// true   The data marker interior is not shown.
		/// </summary>
		public bool NotShowInt
		{
			get
			{
				return fNotShowInt.IsSet(field_4_flag);
			}
			set
			{
				field_4_flag = fNotShowInt.SetShortBoolean(field_4_flag, value);
			}
		}

		/// <summary>
		/// whether to show the data marker border.
		/// false The data marker border is shown.
		/// true  The data marker border is not shown.
		/// </summary>
		public bool NotShowBorder
		{
			get
			{
				return fNotShowBrd.IsSet(field_4_flag);
			}
			set
			{
				field_4_flag = fNotShowBrd.SetShortBoolean(field_4_flag, value);
			}
		}

		/// <summary>
		/// the border color of the data marker.
		/// </summary>
		public short IcvFore
		{
			get
			{
				return field_5_icvFore;
			}
			set
			{
				field_5_icvFore = value;
			}
		}

		/// <summary>
		/// the interior color of the data marker.
		/// </summary>
		public short IcvBack
		{
			get
			{
				return field_6_icvBack;
			}
			set
			{
				field_6_icvBack = value;
			}
		}

		/// <summary>
		/// specifies the size in twips of the data marker.
		/// </summary>
		public int Size
		{
			get
			{
				return field_7_miSize;
			}
			set
			{
				field_7_miSize = value;
			}
		}

		public MarkerFormatRecord()
		{
		}

		public MarkerFormatRecord(RecordInputStream ris)
		{
			field_1_rgbFore = ris.ReadInt();
			field_2_rgbBack = ris.ReadInt();
			field_3_imk = ris.ReadShort();
			field_4_flag = ris.ReadShort();
			field_5_icvFore = ris.ReadShort();
			field_6_icvBack = ris.ReadShort();
			field_7_miSize = ris.ReadInt();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_rgbFore);
			out1.WriteInt(field_2_rgbBack);
			out1.WriteShort(field_3_imk);
			out1.WriteShort(field_4_flag);
			out1.WriteShort(field_5_icvFore);
			out1.WriteShort(field_6_icvBack);
			out1.WriteInt(field_7_miSize);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MARKERFORMAT]").AppendLine().Append("   .rgbFore          =")
				.Append(HexDump.ToHex(field_1_rgbFore))
				.Append("(")
				.Append(field_1_rgbFore)
				.AppendLine(")")
				.Append("   .rgbBack          =")
				.Append(HexDump.ToHex(field_2_rgbBack))
				.Append("(")
				.Append(field_2_rgbBack)
				.AppendLine(")")
				.Append("   .imk              =")
				.Append(HexDump.ToHex(field_3_imk))
				.Append("(")
				.Append(field_3_imk)
				.AppendLine(")")
				.Append("   .flag             =")
				.Append(HexDump.ToHex(field_4_flag))
				.Append("(")
				.Append(field_4_flag)
				.AppendLine(")")
				.Append("       .fAuto        =")
				.Append(Auto)
				.AppendLine()
				.Append("       .fNotShowInt  =")
				.Append(NotShowInt)
				.AppendLine()
				.Append("       .fNotShowBrd  =")
				.Append(NotShowBorder)
				.AppendLine()
				.Append("   .icvFore          =")
				.Append(HexDump.ToHex(field_5_icvFore))
				.Append("(")
				.Append(field_5_icvFore)
				.AppendLine(")")
				.Append("   .icvBack          =")
				.Append(HexDump.ToHex(field_6_icvBack))
				.Append("(")
				.Append(field_6_icvBack)
				.AppendLine(")")
				.Append("   .miSize           =")
				.Append(HexDump.ToHex(field_7_miSize))
				.Append("(")
				.Append(field_7_miSize)
				.AppendLine(")")
				.AppendLine("[/MARKERFORMAT]");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			MarkerFormatRecord markerFormatRecord = new MarkerFormatRecord();
			markerFormatRecord.Auto = Auto;
			markerFormatRecord.DataMarkerType = DataMarkerType;
			markerFormatRecord.IcvBack = IcvBack;
			markerFormatRecord.IcvFore = IcvFore;
			markerFormatRecord.NotShowBorder = NotShowBorder;
			markerFormatRecord.NotShowInt = NotShowInt;
			markerFormatRecord.RGBBack = RGBBack;
			markerFormatRecord.RGBFore = RGBFore;
			markerFormatRecord.Size = Size;
			return markerFormatRecord;
		}
	}
}
