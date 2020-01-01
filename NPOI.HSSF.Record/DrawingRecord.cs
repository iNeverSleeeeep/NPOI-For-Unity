using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	/// DrawingRecord (0x00EC)<p />
	public class DrawingRecord : StandardRecord
	{
		public const short sid = 236;

		private static byte[] EMPTY_BYTE_ARRAY = new byte[0];

		private byte[] recordData;

		private byte[] contd;

		protected override int DataSize => recordData.Length;

		public override short Sid => 236;

		public byte[] Data
		{
			get
			{
				return recordData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("data must not be null");
				}
				recordData = value;
			}
		}

		public DrawingRecord()
		{
			recordData = EMPTY_BYTE_ARRAY;
		}

		public DrawingRecord(RecordInputStream in1)
		{
			recordData = in1.ReadRemainder();
		}

		[Obsolete]
		public void ProcessContinueRecord(byte[] record)
		{
			contd = record;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.Write(recordData);
		}

		/// Cloning of drawing records must be executed through HSSFPatriarch, because all id's must be changed
		/// @return cloned drawing records
		public override object Clone()
		{
			DrawingRecord drawingRecord = new DrawingRecord();
			drawingRecord.recordData = (byte[])recordData.Clone();
			if (contd != null)
			{
				drawingRecord.contd = (byte[])contd.Clone();
			}
			return drawingRecord;
		}

		public override string ToString()
		{
			return "DrawingRecord[" + recordData.Length + "]";
		}
	}
}
