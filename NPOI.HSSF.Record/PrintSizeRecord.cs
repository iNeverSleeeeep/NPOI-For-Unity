using NPOI.Util;

namespace NPOI.HSSF.Record
{
	public class PrintSizeRecord : StandardRecord
	{
		public const short sid = 51;

		private short printSize;

		public short PrintSize
		{
			get
			{
				return printSize;
			}
			set
			{
				printSize = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 51;

		public PrintSizeRecord()
		{
		}

		public PrintSizeRecord(RecordInputStream in1)
		{
			printSize = in1.ReadShort();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(printSize);
		}

		public override object Clone()
		{
			PrintSizeRecord printSizeRecord = new PrintSizeRecord();
			printSizeRecord.printSize = printSize;
			return printSizeRecord;
		}
	}
}
