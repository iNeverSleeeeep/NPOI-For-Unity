using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// specifies the properties of a category (3) axis, a date axis, or a series axis.
	/// </summary>
	public class CatSerRangeRecord : StandardRecord
	{
		public const short sid = 4128;

		private short field_1_catCross;

		private short field_2_catLabel;

		private short field_3_catMark;

		private short field_4_options;

		private BitField fBetween = BitFieldFactory.GetInstance(1);

		private BitField fMaxCross = BitFieldFactory.GetInstance(2);

		private BitField fReverse = BitFieldFactory.GetInstance(4);

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 8;

		public override short Sid => 4128;

		/// <summary>
		/// specifies where the value axis crosses this axis, based on the following table.
		/// If fMaxCross is set to 1, the value this field MUST be ignored.
		/// Category (3) axis   This field specifies the category (3) at which the value axis crosses. 
		///                     For example, if this field is 2, the value axis crosses this axis at the second category (3) 
		///                     on this axis. MUST be greater than or equal to 1 and less than or equal to 31999.
		/// Series axis         MUST be 0.
		/// Date axis           catCross MUST be equal to the value given by the following formula:
		///                     catCross = catCrossDate – catMin + 1
		///                     Where catCrossDate is the catCrossDate field of the AxcExt record 
		///                     and catMin is the catMin field of the AxcExt record.
		/// </summary>
		public short CrossPoint
		{
			get
			{
				return field_1_catCross;
			}
			set
			{
				field_1_catCross = value;
			}
		}

		/// <summary>
		/// specifies the interval between axis labels on this axis. MUST be greater than or equal to 1 and 
		/// less than or equal to 31999. MUST be ignored for a date axis.
		/// </summary>
		public short LabelInterval
		{
			get
			{
				return field_2_catLabel;
			}
			set
			{
				field_2_catLabel = value;
			}
		}

		/// <summary>
		/// specifies the interval at which major tick marks and minor tick marks are displayed on the axis. 
		/// Major tick marks and minor tick marks that would have been visible are hidden unless they are 
		/// located at a multiple of this field.
		/// </summary>
		public short MarkInterval
		{
			get
			{
				return field_3_catMark;
			}
			set
			{
				field_3_catMark = value;
			}
		}

		public short Options
		{
			get
			{
				return field_4_options;
			}
			set
			{
				field_4_options = value;
			}
		}

		/// <summary>
		/// specifies whether the value axis crosses this axis between major tick marks. MUST be a value from to following table:
		/// 0  The value axis crosses this axis on a major tick mark.
		/// 1  The value axis crosses this axis between major tick marks.
		/// </summary>
		public bool IsBetween
		{
			get
			{
				return fBetween.IsSet(field_4_options);
			}
			set
			{
				field_4_options = fBetween.SetShortBoolean(field_4_options, value);
			}
		}

		/// <summary>
		/// specifies whether the value axis crosses this axis at the last category (3), the last series, 
		/// or the maximum date. MUST be a value from the following table:
		/// 0  The value axis crosses this axis at the value specified by catCross.
		/// 1  The value axis crosses this axis at the last category (3), the last series, or the maximum date.
		/// </summary>
		public bool IsMaxCross
		{
			get
			{
				return fMaxCross.IsSet(field_4_options);
			}
			set
			{
				field_4_options = fMaxCross.SetShortBoolean(field_4_options, value);
			}
		}

		/// <summary>
		/// specifies whether the axis is displayed in reverse order. MUST be a value from the following table:
		/// 0  The axis is displayed in order.
		/// 1  The axis is display in reverse order.
		/// </summary>
		public bool IsReverse
		{
			get
			{
				return fReverse.IsSet(field_4_options);
			}
			set
			{
				field_4_options = fReverse.SetShortBoolean(field_4_options, value);
			}
		}

		public CatSerRangeRecord()
		{
		}

		/// Constructs a CategorySeriesAxis record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public CatSerRangeRecord(RecordInputStream in1)
		{
			field_1_catCross = in1.ReadShort();
			field_2_catLabel = in1.ReadShort();
			field_3_catMark = in1.ReadShort();
			field_4_options = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CATSERRANGE]\n");
			stringBuilder.Append("    .catCross        = ").Append("0x").Append(HexDump.ToHex(CrossPoint))
				.Append(" (")
				.Append(CrossPoint)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catLabel       = ").Append("0x").Append(HexDump.ToHex(LabelInterval))
				.Append(" (")
				.Append(LabelInterval)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .catMark    = ").Append("0x").Append(HexDump.ToHex(MarkInterval))
				.Append(" (")
				.Append(MarkInterval)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append("0x").Append(HexDump.ToHex(Options))
				.Append(" (")
				.Append(Options)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .fBetween        = ").Append(IsBetween).Append('\n');
			stringBuilder.Append("         .fMaxCross       = ").Append(IsMaxCross).Append('\n');
			stringBuilder.Append("         .fReverse        = ").Append(IsReverse).Append('\n');
			stringBuilder.Append("[/CATSERRANGE]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_catCross);
			out1.WriteShort(field_2_catLabel);
			out1.WriteShort(field_3_catMark);
			out1.WriteShort(field_4_options);
		}

		public override object Clone()
		{
			CatSerRangeRecord catSerRangeRecord = new CatSerRangeRecord();
			catSerRangeRecord.field_1_catCross = field_1_catCross;
			catSerRangeRecord.field_2_catLabel = field_2_catLabel;
			catSerRangeRecord.field_3_catMark = field_3_catMark;
			catSerRangeRecord.field_4_options = field_4_options;
			return catSerRangeRecord;
		}
	}
}
