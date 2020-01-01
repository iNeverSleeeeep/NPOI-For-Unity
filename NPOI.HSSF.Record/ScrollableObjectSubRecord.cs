using NPOI.Util;
using System;
using System.Globalization;

namespace NPOI.HSSF.Record
{
	/// <summary>
	/// FtSbs structure
	/// </summary>
	public class ScrollableObjectSubRecord : SubRecord
	{
		public const short sid = 12;

		private short field_1_iVal;

		private short field_2_iMin;

		private short field_3_iMax;

		private short field_4_dInc;

		private short field_5_dPage;

		private short field_6_fHoriz;

		private short field_7_dxScroll;

		private short field_8_options;

		private BitField fDrawFlag = BitFieldFactory.GetInstance(1);

		private BitField fDrawSliderOnly = BitFieldFactory.GetInstance(2);

		private BitField fTrackElevator = BitFieldFactory.GetInstance(4);

		private BitField fNo3d = BitFieldFactory.GetInstance(8);

		public override int DataSize => 20;

		public override short Sid => 12;

		public short CurrentValue
		{
			get
			{
				return field_1_iVal;
			}
			set
			{
				if (field_1_iVal < field_2_iMin || field_1_iVal > field_3_iMax)
				{
					throw new ArgumentOutOfRangeException("invalid value");
				}
				field_1_iVal = value;
			}
		}

		public short MaxValue
		{
			get
			{
				return field_2_iMin;
			}
			set
			{
				field_2_iMin = value;
			}
		}

		public short MinValue
		{
			get
			{
				return field_3_iMax;
			}
			set
			{
				field_3_iMax = value;
			}
		}

		public short IncreaseAmountChanged
		{
			get
			{
				return field_4_dInc;
			}
			set
			{
				field_4_dInc = value;
			}
		}

		public short PageAmountChanged
		{
			get
			{
				return field_5_dPage;
			}
			set
			{
				field_5_dPage = value;
			}
		}

		public bool IsHorizontal
		{
			get
			{
				return field_6_fHoriz == 1;
			}
			set
			{
				field_6_fHoriz = (short)(value ? 1 : 0);
			}
		}

		public short ScrollbarWidthInPixel
		{
			get
			{
				return field_7_dxScroll;
			}
			set
			{
				field_7_dxScroll = value;
			}
		}

		public bool IsVisible
		{
			get
			{
				return fDrawFlag.IsSet(field_8_options);
			}
			set
			{
				field_8_options = fDrawFlag.SetShortBoolean(field_8_options, value);
			}
		}

		public bool IsOnlySilderPortionVisible
		{
			get
			{
				return fDrawSliderOnly.IsSet(field_8_options);
			}
			set
			{
				field_8_options = fDrawSliderOnly.SetShortBoolean(field_8_options, value);
			}
		}

		public bool IsTrackElevator
		{
			get
			{
				return fTrackElevator.IsSet(field_8_options);
			}
			set
			{
				field_8_options = fTrackElevator.SetShortBoolean(field_8_options, value);
			}
		}

		public bool IsNo3D
		{
			get
			{
				return fNo3d.IsSet(field_8_options);
			}
			set
			{
				field_8_options = fNo3d.SetShortBoolean(field_8_options, value);
			}
		}

		public ScrollableObjectSubRecord()
		{
		}

		public ScrollableObjectSubRecord(ILittleEndianInput in1, int size)
		{
			if (size != DataSize)
			{
				throw new RecordFormatException(string.Format(CultureInfo.CurrentCulture, "Expected size {0} but got ({1})", new object[2]
				{
					DataSize,
					size
				}));
			}
			in1.ReadInt();
			field_1_iVal = in1.ReadShort();
			field_2_iMin = in1.ReadShort();
			field_3_iMax = in1.ReadShort();
			field_4_dInc = in1.ReadShort();
			field_5_dPage = in1.ReadShort();
			field_6_fHoriz = in1.ReadShort();
			field_7_dxScroll = in1.ReadShort();
			field_8_options = in1.ReadShort();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(12);
			out1.WriteShort(DataSize);
			out1.WriteInt(0);
			out1.WriteShort(field_1_iVal);
			out1.WriteShort(field_2_iMin);
			out1.WriteShort(field_3_iMax);
			out1.WriteShort(field_4_dInc);
			out1.WriteShort(field_5_dPage);
			out1.WriteShort(field_6_fHoriz);
			out1.WriteShort(field_7_dxScroll);
			out1.WriteShort(field_8_options);
		}

		public override object Clone()
		{
			ScrollableObjectSubRecord scrollableObjectSubRecord = new ScrollableObjectSubRecord();
			scrollableObjectSubRecord.field_1_iVal = field_1_iVal;
			scrollableObjectSubRecord.field_2_iMin = field_2_iMin;
			scrollableObjectSubRecord.field_3_iMax = field_3_iMax;
			scrollableObjectSubRecord.field_4_dInc = field_4_dInc;
			scrollableObjectSubRecord.field_5_dPage = field_5_dPage;
			scrollableObjectSubRecord.field_6_fHoriz = field_6_fHoriz;
			scrollableObjectSubRecord.field_7_dxScroll = field_7_dxScroll;
			scrollableObjectSubRecord.field_8_options = field_8_options;
			return scrollableObjectSubRecord;
		}
	}
}
