using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The AxcExt record specifies additional extension properties of a date axis (section 2.2.3.6), 
	/// along with a CatSerRange record (section 2.4.39).
	/// </summary>
	public class AxcExtRecord : StandardRecord
	{
		public const short sid = 4194;

		private short field_1_catMin;

		private short field_2_catMax;

		private short field_3_catMajor;

		private short field_4_duMajor;

		private short field_5_catMinor;

		private short field_6_duMinor;

		private short field_7_duBase;

		private short field_8_catCrossDate;

		private short field_9_options;

		private BitField fAutoMin = BitFieldFactory.GetInstance(1);

		private BitField fAutoMax = BitFieldFactory.GetInstance(2);

		private BitField fAutoMajor = BitFieldFactory.GetInstance(4);

		private BitField fAutoMinor = BitFieldFactory.GetInstance(8);

		private BitField fDateAxis = BitFieldFactory.GetInstance(16);

		private BitField fAutoBase = BitFieldFactory.GetInstance(32);

		private BitField fAutoCross = BitFieldFactory.GetInstance(64);

		private BitField fAutoDate = BitFieldFactory.GetInstance(128);

		protected override int DataSize => 18;

		public override short Sid => 4194;

		public short MinimumDate
		{
			get
			{
				return field_1_catMin;
			}
			set
			{
				field_1_catMin = value;
			}
		}

		public short MaximumDate
		{
			get
			{
				return field_2_catMax;
			}
			set
			{
				field_2_catMax = value;
			}
		}

		/// <summary>
		/// specifies the interval at which the major tick marks are displayed on the axis (section 2.2.3.6), 
		/// in the unit defined by duMajor.
		/// </summary>
		public short MajorInterval
		{
			get
			{
				return field_3_catMajor;
			}
			set
			{
				field_3_catMajor = value;
			}
		}

		/// <summary>
		/// specifies the unit of time to use for catMajor when the axis (section 2.2.3.6) is a date axis (section 2.2.3.6).
		/// If fDateAxis is set to 0, MUST be ignored.
		/// </summary>
		public DateUnit MajorUnit
		{
			get
			{
				return (DateUnit)field_4_duMajor;
			}
			set
			{
				field_4_duMajor = (short)value;
			}
		}

		/// <summary>
		/// specifies the interval at which the minor tick marks are displayed on the axis (section 2.2.3.6), 
		/// in a unit defined by duMinor.
		/// </summary>
		public short MinorInterval
		{
			get
			{
				return field_5_catMinor;
			}
			set
			{
				field_5_catMinor = value;
			}
		}

		public DateUnit MinorUnit
		{
			get
			{
				return (DateUnit)field_6_duMinor;
			}
			set
			{
				field_6_duMinor = (short)value;
			}
		}

		/// <summary>
		/// specifies the smallest unit of time used by the axis (section 2.2.3.6).
		/// </summary>
		public DateUnit BaseUnit
		{
			get
			{
				return (DateUnit)field_7_duBase;
			}
			set
			{
				field_7_duBase = (short)value;
			}
		}

		/// <summary>
		/// specifies at which date, as a date in the date system specified by the Date1904 record (section 2.4.77), 
		/// in the units defined by duBase, the value axis (section 2.2.3.6) crosses this axis (section 2.2.3.6).
		/// </summary>
		public short CrossDate
		{
			get
			{
				return field_8_catCrossDate;
			}
			set
			{
				field_8_catCrossDate = value;
			}
		}

		public short Options
		{
			get
			{
				return field_9_options;
			}
			set
			{
				field_9_options = value;
			}
		}

		/// <summary>
		/// specifies whether MinimumDate is calculated automatically.
		/// </summary>
		public bool IsAutoMin
		{
			get
			{
				return fAutoMin.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoMin.SetShortBoolean(field_9_options, value);
			}
		}

		/// <summary>
		/// specifies whether MaximumDate is calculated automatically.
		/// </summary>
		public bool IsAutoMax
		{
			get
			{
				return fAutoMax.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoMax.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsAutoMajor
		{
			get
			{
				return fAutoMajor.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoMajor.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsAutoMinor
		{
			get
			{
				return fAutoMinor.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoMinor.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsDateAxis
		{
			get
			{
				return fDateAxis.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fDateAxis.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsAutoBase
		{
			get
			{
				return fAutoBase.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoBase.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsAutoCross
		{
			get
			{
				return fAutoCross.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoCross.SetShortBoolean(field_9_options, value);
			}
		}

		public bool IsAutoDate
		{
			get
			{
				return fAutoDate.IsSet(field_9_options);
			}
			set
			{
				field_9_options = fAutoDate.SetShortBoolean(field_9_options, value);
			}
		}

		public AxcExtRecord()
		{
		}

		public AxcExtRecord(RecordInputStream in1)
		{
			field_1_catMin = in1.ReadShort();
			field_2_catMax = in1.ReadShort();
			field_3_catMajor = in1.ReadShort();
			field_4_duMajor = in1.ReadShort();
			field_5_catMinor = in1.ReadShort();
			field_6_duMinor = in1.ReadShort();
			field_7_duBase = in1.ReadShort();
			field_8_catCrossDate = in1.ReadShort();
			field_9_options = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AXCEXT]\n");
			stringBuilder.Append("    .catMin      = ").Append("0x").Append(HexDump.ToHex(MinimumDate))
				.Append(" (")
				.Append(MinimumDate)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catMax      = ").Append("0x").Append(HexDump.ToHex(MaximumDate))
				.Append(" (")
				.Append(MaximumDate)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catMajor       = ").Append("0x").Append(HexDump.ToHex(MajorInterval))
				.Append(" (")
				.Append(MajorInterval)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .duMajor            = ").Append("0x").Append(HexDump.ToHex((short)MajorUnit))
				.Append(" (")
				.Append(MajorUnit)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catMinor       = ").Append("0x").Append(HexDump.ToHex(MinorInterval))
				.Append(" (")
				.Append(MinorInterval)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .duMinor            = ").Append("0x").Append(HexDump.ToHex((short)MinorUnit))
				.Append(" (")
				.Append(MinorUnit)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .duBase             = ").Append("0x").Append(HexDump.ToHex((short)BaseUnit))
				.Append(" (")
				.Append(BaseUnit)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catCrossDate        = ").Append("0x").Append(HexDump.ToHex(CrossDate))
				.Append(" (")
				.Append(CrossDate)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .fAutoMin           = ").Append(IsAutoMin).Append('\n');
			stringBuilder.Append("         .fAutoMax           = ").Append(IsAutoMax).Append('\n');
			stringBuilder.Append("         .fAutoMajor         = ").Append(IsAutoMajor).Append('\n');
			stringBuilder.Append("         .fAutoMinor         = ").Append(IsAutoMinor).Append('\n');
			stringBuilder.Append("         .fDateAxis          = ").Append(IsDateAxis).Append('\n');
			stringBuilder.Append("         .fAutoBase          = ").Append(IsAutoBase).Append('\n');
			stringBuilder.Append("         .fAutoCross         = ").Append(IsAutoCross).Append('\n');
			stringBuilder.Append("         .fAutoDate          = ").Append(IsAutoDate).Append('\n');
			stringBuilder.Append("[/AXCEXT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_catMin);
			out1.WriteShort(field_2_catMax);
			out1.WriteShort(field_3_catMajor);
			out1.WriteShort(field_4_duMajor);
			out1.WriteShort(field_5_catMinor);
			out1.WriteShort(field_6_duMinor);
			out1.WriteShort(field_7_duBase);
			out1.WriteShort(field_8_catCrossDate);
			out1.WriteShort(field_9_options);
		}

		public override object Clone()
		{
			AxcExtRecord axcExtRecord = new AxcExtRecord();
			axcExtRecord.field_1_catMin = field_1_catMin;
			axcExtRecord.field_2_catMax = field_2_catMax;
			axcExtRecord.field_3_catMajor = field_3_catMajor;
			axcExtRecord.field_4_duMajor = field_4_duMajor;
			axcExtRecord.field_5_catMinor = field_5_catMinor;
			axcExtRecord.field_6_duMinor = field_6_duMinor;
			axcExtRecord.field_7_duBase = field_7_duBase;
			axcExtRecord.field_8_catCrossDate = field_8_catCrossDate;
			axcExtRecord.field_9_options = field_9_options;
			return axcExtRecord;
		}
	}
}
