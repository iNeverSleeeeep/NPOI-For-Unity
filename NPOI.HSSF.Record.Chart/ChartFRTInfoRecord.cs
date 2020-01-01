using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The ChartFrtInfo record specifies the versions of the application that originally created and last saved the file.
	/// </summary>
	public class ChartFRTInfoRecord : StandardRecord
	{
		private class CFRTID : ICloneable
		{
			public const int ENCODED_SIZE = 4;

			private int rtFirst;

			private int rtLast;

			public CFRTID(int first, int last)
			{
				rtFirst = first;
				rtLast = last;
			}

			public CFRTID(RecordInputStream in1)
			{
				rtFirst = in1.ReadShort();
				rtLast = in1.ReadShort();
			}

			public void Serialize(ILittleEndianOutput out1)
			{
				out1.WriteShort(rtFirst);
				out1.WriteShort(rtLast);
			}

			public object Clone()
			{
				return new CFRTID(rtFirst, rtLast);
			}
		}

		public const short sid = 2128;

		private short rt;

		private short grbitFrt;

		private byte verOriginator;

		private byte verWriter;

		private CFRTID[] rgCFRTID;

		protected override int DataSize => 8 + rgCFRTID.Length * 4;

		public override short Sid => 2128;

		public ChartFRTInfoRecord()
		{
			rt = 2128;
			grbitFrt = 0;
			verOriginator = 10;
			verWriter = 10;
			rgCFRTID = new CFRTID[3];
			rgCFRTID[0] = new CFRTID(2128, 2138);
			rgCFRTID[1] = new CFRTID(2145, 2145);
			rgCFRTID[2] = new CFRTID(2154, 2155);
		}

		public ChartFRTInfoRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			verOriginator = (byte)in1.ReadByte();
			verWriter = (byte)in1.ReadByte();
			int num = in1.ReadShort();
			rgCFRTID = new CFRTID[num];
			for (int i = 0; i < num; i++)
			{
				rgCFRTID[i] = new CFRTID(in1);
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteByte(verOriginator);
			out1.WriteByte(verWriter);
			int num = rgCFRTID.Length;
			out1.WriteShort(num);
			for (int i = 0; i < num; i++)
			{
				rgCFRTID[i].Serialize(out1);
			}
		}

		public override object Clone()
		{
			ChartFRTInfoRecord chartFRTInfoRecord = new ChartFRTInfoRecord();
			chartFRTInfoRecord.grbitFrt = grbitFrt;
			chartFRTInfoRecord.rgCFRTID = new CFRTID[rgCFRTID.Length];
			chartFRTInfoRecord.rt = rt;
			chartFRTInfoRecord.verOriginator = verOriginator;
			chartFRTInfoRecord.verWriter = verWriter;
			for (int i = 0; i < rgCFRTID.Length; i++)
			{
				chartFRTInfoRecord.rgCFRTID[i] = (CFRTID)rgCFRTID[i].Clone();
			}
			return chartFRTInfoRecord;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CHARTFRTINFO]\n");
			stringBuilder.Append("    .rt           =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt     =").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .verOriginator=").Append(HexDump.ByteToHex(verOriginator)).Append('\n');
			stringBuilder.Append("    .verWriter    =").Append(HexDump.ByteToHex(verOriginator)).Append('\n');
			stringBuilder.Append("    .nCFRTIDs     =").Append(HexDump.ShortToHex(rgCFRTID.Length)).Append('\n');
			stringBuilder.Append("[/CHARTFRTINFO]\n");
			return stringBuilder.ToString();
		}
	}
}
