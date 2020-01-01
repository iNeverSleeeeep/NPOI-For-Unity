using NPOI.Util;
using System;

namespace NPOI.HSSF.Record.AutoFilter
{
	public class AutoFilterRecord : StandardRecord
	{
		public const short sid = 158;

		private short field_1_iEntry;

		private short field_2_grbit;

		private static readonly BitField wJoinFlag = BitFieldFactory.GetInstance(3);

		private static readonly BitField fSimple1Flag = BitFieldFactory.GetInstance(4);

		private static readonly BitField fSimple2Flag = BitFieldFactory.GetInstance(8);

		private static readonly BitField fTop10Flag = BitFieldFactory.GetInstance(16);

		private static readonly BitField fTopFlag = BitFieldFactory.GetInstance(32);

		private static readonly BitField fPercentFlag = BitFieldFactory.GetInstance(64);

		private static readonly BitField wTop10Flag = BitFieldFactory.GetInstance(65408);

		private DOPERRecord field_3_doper1;

		private DOPERRecord field_4_doper2;

		private string field_5_rgch1 = string.Empty;

		private string field_6_rgch2 = string.Empty;

		private bool rgch1_multibyte;

		private bool rgch2_multibyte;

		public override short Sid => 158;

		public short iEntry
		{
			get
			{
				return field_1_iEntry;
			}
			set
			{
				field_1_iEntry = value;
			}
		}

		public short wJoin
		{
			get
			{
				return wJoinFlag.GetShortValue(field_2_grbit);
			}
			set
			{
				field_2_grbit = wJoinFlag.SetShortValue(field_2_grbit, value);
			}
		}

		public bool IsFirstConditionSimple
		{
			get
			{
				return fSimple1Flag.IsSet(field_2_grbit);
			}
			set
			{
				field_2_grbit = fSimple1Flag.SetShortBoolean(field_2_grbit, value);
			}
		}

		public bool IsSecondConditionSimple
		{
			get
			{
				return fSimple2Flag.IsSet(field_2_grbit);
			}
			set
			{
				field_2_grbit = fSimple2Flag.SetShortBoolean(field_2_grbit, value);
			}
		}

		public DOPERRecord Doper1 => field_3_doper1;

		public DOPERRecord Doper2 => field_4_doper2;

		public string Doper1RGCH
		{
			get
			{
				return field_5_rgch1;
			}
			set
			{
				if (field_5_rgch1.Length > 252)
				{
					throw new ArgumentOutOfRangeException("The length of string must be less than or equal to 252");
				}
				field_5_rgch1 = value;
				field_3_doper1.DataType = DOPERType.String;
				field_3_doper1.LengthOfString = (byte)field_5_rgch1.Length;
				rgch1_multibyte = StringUtil.HasMultibyte(value);
			}
		}

		public string Doper2RGCH
		{
			get
			{
				return field_6_rgch2;
			}
			set
			{
				if (field_6_rgch2.Length > 252)
				{
					throw new ArgumentOutOfRangeException("The length of string must be less than or equal to 252");
				}
				field_6_rgch2 = value;
				field_4_doper2.DataType = DOPERType.String;
				field_4_doper2.LengthOfString = (byte)field_6_rgch2.Length;
				rgch2_multibyte = StringUtil.HasMultibyte(value);
			}
		}

		protected override int DataSize
		{
			get
			{
				int num = 24;
				if (field_3_doper1.LengthOfString > 0)
				{
					num += 1 + field_5_rgch1.Length;
				}
				if (field_4_doper2.LengthOfString > 0)
				{
					num += 1 + field_6_rgch2.Length;
				}
				return num;
			}
		}

		public AutoFilterRecord()
		{
		}

		public AutoFilterRecord(RecordInputStream in1)
		{
			field_1_iEntry = in1.ReadShort();
			field_2_grbit = in1.ReadShort();
			field_3_doper1 = new DOPERRecord(in1);
			field_4_doper2 = new DOPERRecord(in1);
			if (field_3_doper1.DataType == DOPERType.String && field_3_doper1.LengthOfString > 0)
			{
				rgch1_multibyte = (in1.ReadByte() != 0);
				if (rgch1_multibyte)
				{
					field_5_rgch1 = in1.ReadUnicodeLEString(field_3_doper1.LengthOfString);
				}
				else
				{
					field_5_rgch1 = in1.ReadCompressedUnicode(field_3_doper1.LengthOfString);
				}
			}
			if (field_4_doper2.DataType == DOPERType.String && field_4_doper2.LengthOfString > 0)
			{
				rgch2_multibyte = (in1.ReadByte() != 0);
				if (rgch2_multibyte)
				{
					field_6_rgch2 = in1.ReadUnicodeLEString(field_4_doper2.LengthOfString);
				}
				else
				{
					field_6_rgch2 = in1.ReadCompressedUnicode(field_4_doper2.LengthOfString);
				}
			}
		}

		public override int Serialize(int offset, byte[] data)
		{
			return base.Serialize(offset, data);
		}

		public override object Clone()
		{
			AutoFilterRecord autoFilterRecord = new AutoFilterRecord();
			autoFilterRecord.field_1_iEntry = field_1_iEntry;
			autoFilterRecord.field_2_grbit = field_2_grbit;
			autoFilterRecord.field_3_doper1 = field_3_doper1;
			autoFilterRecord.field_4_doper2 = field_4_doper2;
			autoFilterRecord.field_5_rgch1 = field_5_rgch1;
			autoFilterRecord.field_6_rgch2 = field_6_rgch2;
			return autoFilterRecord;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_iEntry);
			out1.WriteShort(field_2_grbit);
			field_3_doper1.Serialize(out1);
			field_4_doper2.Serialize(out1);
			if (field_3_doper1.LengthOfString > 0)
			{
				out1.WriteByte(rgch1_multibyte ? 1 : 0);
				if (rgch1_multibyte)
				{
					StringUtil.PutUnicodeLE(field_5_rgch1, out1);
				}
				else
				{
					StringUtil.PutCompressedUnicode(field_5_rgch1, out1);
				}
			}
			if (field_4_doper2.LengthOfString > 0)
			{
				out1.WriteByte(rgch2_multibyte ? 1 : 0);
				if (rgch2_multibyte)
				{
					StringUtil.PutUnicodeLE(field_6_rgch2, out1);
				}
				else
				{
					StringUtil.PutCompressedUnicode(field_6_rgch2, out1);
				}
			}
		}
	}
}
