using NPOI.HSSF.Record;
using NPOI.Util;
using System;

namespace NPOI.SS.Util
{
	public class CellRangeAddress8Bit : CellRangeAddressBase
	{
		public const int ENCODED_SIZE = 6;

		public CellRangeAddress8Bit(int firstRow, int lastRow, int firstCol, int lastCol)
			: base(firstRow, lastRow, firstCol, lastCol)
		{
		}

		public CellRangeAddress8Bit(RecordInputStream in1)
			: base(ReadUShortAndCheck(in1), in1.ReadUShort(), in1.ReadUByte(), in1.ReadUByte())
		{
		}

		private static int ReadUShortAndCheck(RecordInputStream in1)
		{
			if (in1.Remaining < 6)
			{
				throw new Exception("Ran out of data reading CellRangeAddress");
			}
			return in1.ReadUShort();
		}

		public int Serialize(int offset, byte[] data)
		{
			LittleEndian.PutUShort(data, offset, base.FirstRow);
			LittleEndian.PutUShort(data, offset + 2, base.LastRow);
			LittleEndian.PutByte(data, offset + 4, base.FirstColumn);
			LittleEndian.PutByte(data, offset + 5, base.LastColumn);
			return 6;
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(base.FirstRow);
			out1.WriteShort(base.LastRow);
			out1.WriteByte(base.FirstColumn);
			out1.WriteByte(base.LastColumn);
		}

		public CellRangeAddress8Bit Copy()
		{
			return new CellRangeAddress8Bit(base.FirstRow, base.LastRow, base.FirstColumn, base.LastColumn);
		}

		public static int GetEncodedSize(int numberOfItems)
		{
			return numberOfItems * 6;
		}
	}
}
