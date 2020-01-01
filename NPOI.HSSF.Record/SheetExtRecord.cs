using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// <summary>
	///
	/// </summary>
	public class SheetExtRecord : StandardRecord
	{
		public const short sid = 2146;

		private short rt;

		private short grbitFrt;

		private int cb;

		private BitField icvPlain = BitFieldFactory.GetInstance(127);

		private short optionflag = 255;

		private short optionflag2;

		private BitField icvPlain12 = BitFieldFactory.GetInstance(127);

		private BitField fCondFmtCalc = BitFieldFactory.GetInstance(128);

		private BitField fNotPublished = BitFieldFactory.GetInstance(256);

		private int xclrType;

		private int xclrValue;

		private long numTint;

		public short TabColorIndex
		{
			get
			{
				return icvPlain.GetShortValue(optionflag);
			}
			set
			{
				optionflag = icvPlain.SetShortValue(optionflag, value);
			}
		}

		public bool IsAutoColor
		{
			get
			{
				return TabColorIndex == 127;
			}
			set
			{
				if (value)
				{
					TabColorIndex = 127;
				}
				else
				{
					TabColorIndex = 8;
				}
			}
		}

		public bool EvaluateConditionalFormatting
		{
			get
			{
				return fCondFmtCalc.IsSet(optionflag2);
			}
			set
			{
				optionflag2 = (short)fCondFmtCalc.SetBoolean(optionflag2, value);
			}
		}

		public bool IsSheetPublished
		{
			get
			{
				return !fNotPublished.IsSet(optionflag2);
			}
			set
			{
				optionflag2 = (short)fNotPublished.SetBoolean(optionflag2, !value);
			}
		}

		protected override int DataSize => 20 + ((cb == 40) ? 20 : 0);

		public override short Sid => 2146;

		public SheetExtRecord()
		{
			rt = 2146;
			IsAutoColor = true;
		}

		public SheetExtRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			if (rt != 2146)
			{
				throw new ArgumentException("frtHeader.rt must be equals 0x0862 in SheetExt record");
			}
			grbitFrt = in1.ReadShort();
			in1.ReadInt();
			in1.ReadInt();
			cb = in1.ReadInt();
			optionflag = in1.ReadShort();
			in1.ReadShort();
			if (cb == 40)
			{
				optionflag2 = in1.ReadShort();
				xclrType = in1.ReadInt();
				xclrValue = in1.ReadInt();
				numTint = in1.ReadLong();
				in1.ReadShort();
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteInt(0);
			out1.WriteInt(0);
			cb = DataSize;
			out1.WriteInt(cb);
			out1.WriteShort(optionflag);
			out1.WriteShort(0);
			if (cb == 40)
			{
				out1.WriteShort(optionflag2);
				out1.WriteInt(xclrType);
				out1.WriteInt(xclrValue);
				out1.WriteLong(numTint);
				out1.WriteShort(0);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SHEETEXT]");
			stringBuilder.Append("[/SHEETEXT]");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			SheetExtRecord sheetExtRecord = new SheetExtRecord();
			sheetExtRecord.rt = rt;
			sheetExtRecord.grbitFrt = grbitFrt;
			sheetExtRecord.cb = DataSize;
			sheetExtRecord.optionflag = optionflag;
			if (cb == 40)
			{
				sheetExtRecord.optionflag2 = optionflag2;
				sheetExtRecord.xclrType = xclrType;
				sheetExtRecord.xclrValue = xclrValue;
				sheetExtRecord.numTint = numTint;
			}
			return sheetExtRecord;
		}
	}
}
