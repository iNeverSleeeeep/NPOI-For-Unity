using NPOI.SS.Util;
using NPOI.Util;

namespace NPOI.HSSF.Record
{
	/// Common base class for {@link SharedFormulaRecord}, {@link ArrayRecord} and
	/// {@link TableRecord} which are have similarities.
	///
	/// @author Josh Micich
	public abstract class SharedValueRecordBase : StandardRecord
	{
		private CellRangeAddress8Bit _range;

		public CellRangeAddress8Bit Range => _range;

		public virtual int FirstRow => _range.FirstRow;

		public virtual int LastRow => _range.LastRow;

		public virtual int FirstColumn => (short)_range.FirstColumn;

		public virtual int LastColumn => (short)_range.LastColumn;

		protected override int DataSize => 6 + ExtraDataSize;

		protected abstract int ExtraDataSize
		{
			get;
		}

		protected SharedValueRecordBase(CellRangeAddress8Bit range)
		{
			_range = range;
		}

		protected SharedValueRecordBase()
			: this(new CellRangeAddress8Bit(0, 0, 0, 0))
		{
		}

		/// reads only the range (1 {@link CellRangeAddress8Bit}) from the stream
		public SharedValueRecordBase(RecordInputStream in1)
		{
			_range = new CellRangeAddress8Bit(in1);
		}

		protected abstract void SerializeExtraData(ILittleEndianOutput out1);

		public override void Serialize(ILittleEndianOutput out1)
		{
			_range.Serialize(out1);
			SerializeExtraData(out1);
		}

		/// @return <c>true</c> if (rowIx, colIx) is within the range ({@link #Range})
		/// of this shared value object.
		public bool IsInRange(int rowIx, int colIx)
		{
			CellRangeAddress8Bit range = _range;
			if (range.FirstRow <= rowIx && range.LastRow >= rowIx && range.FirstColumn <= colIx)
			{
				return range.LastColumn >= colIx;
			}
			return false;
		}

		/// @return <c>true</c> if (rowIx, colIx) describes the first cell in this shared value 
		/// object's range ({@link #Range})
		public bool IsFirstCell(int rowIx, int colIx)
		{
			CellRangeAddress8Bit range = Range;
			if (range.FirstRow == rowIx)
			{
				return range.FirstColumn == colIx;
			}
			return false;
		}
	}
}
