using NPOI.DDF;
using NPOI.Util;
using System;
using System.Collections;

namespace NPOI.HSSF.Record
{
	public class DrawingGroupRecord : AbstractEscherHolderRecord
	{
		public const short sid = 235;

		private const int MAX_RECORD_SIZE = 8228;

		private const int MAX_DATA_SIZE = 8224;

		protected override string RecordName => "MSODRAWINGGROUP";

		public override short Sid => 235;

		/// Size of record (including 4 byte headers for all sections)
		public override int RecordSize => GrossSizeFromDataSize(RawDataSize);

		public int RawDataSize
		{
			get
			{
				IList escherRecords = base.EscherRecords;
				byte[] rawData = base.RawData;
				if (escherRecords.Count == 0 && rawData != null)
				{
					return rawData.Length;
				}
				int num = 0;
				IEnumerator enumerator = escherRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					num += escherRecord.RecordSize;
				}
				return num;
			}
		}

		public DrawingGroupRecord()
		{
		}

		public DrawingGroupRecord(RecordInputStream in1)
			: base(in1)
		{
		}

		public override int Serialize(int offset, byte[] data)
		{
			byte[] rawData = base.RawData;
			if (base.EscherRecords.Count == 0 && rawData != null)
			{
				return WriteData(offset, data, rawData);
			}
			byte[] array = new byte[RawDataSize];
			int num = 0;
			IEnumerator enumerator = base.EscherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				num += escherRecord.Serialize(num, array, new NullEscherSerializationListener());
			}
			return WriteData(offset, data, array);
		}

		/// Process the bytes into escher records.
		/// (Not done by default in case we break things,
		///  Unless you Set the "poi.deSerialize.escher" 
		///  system property)
		public void ProcessChildRecords()
		{
			ConvertRawBytesToEscherRecords();
		}

		public static int GrossSizeFromDataSize(int dataSize)
		{
			return dataSize + ((dataSize - 1) / 8224 + 1) * 4;
		}

		private int WriteData(int offset, byte[] data, byte[] rawData)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < rawData.Length)
			{
				int num3 = Math.Min(rawData.Length - num2, 8224);
				if (num2 / 8224 >= 2)
				{
					WriteContinueHeader(data, offset, num3);
				}
				else
				{
					WriteHeader(data, offset, num3);
				}
				num += 4;
				offset += 4;
				Array.Copy(rawData, num2, data, offset, num3);
				offset += num3;
				num2 += num3;
				num += num3;
			}
			return num;
		}

		private void WriteHeader(byte[] data, int offset, int sizeExcludingHeader)
		{
			LittleEndian.PutShort(data, offset, Sid);
			LittleEndian.PutShort(data, 2 + offset, (short)sizeExcludingHeader);
		}

		private void WriteContinueHeader(byte[] data, int offset, int sizeExcludingHeader)
		{
			LittleEndian.PutShort(data, offset, 60);
			LittleEndian.PutShort(data, 2 + offset, (short)sizeExcludingHeader);
		}
	}
}
