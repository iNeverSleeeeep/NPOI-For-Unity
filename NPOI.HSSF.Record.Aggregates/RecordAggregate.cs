using System;

namespace NPOI.HSSF.Record.Aggregates
{
	/// <c>RecordAggregate</c>s are groups of of BIFF <c>Record</c>s that are typically stored 
	/// together and/or updated together.  Workbook / Sheet records are typically stored in a sequential
	/// list, which does not provide much structure to coordinate updates.
	///
	/// @author Josh Micich
	[Serializable]
	public abstract class RecordAggregate : RecordBase
	{
		private class SerializingRecordVisitor : RecordVisitor
		{
			private byte[] _data;

			private int _startOffset;

			private int _countBytesWritten;

			public SerializingRecordVisitor(byte[] data, int startOffset)
			{
				_data = data;
				_startOffset = startOffset;
				_countBytesWritten = 0;
			}

			public int CountBytesWritten()
			{
				return _countBytesWritten;
			}

			public void VisitRecord(Record r)
			{
				int offset = _startOffset + _countBytesWritten;
				_countBytesWritten += r.Serialize(offset, _data);
			}
		}

		private class RecordSizingVisitor : RecordVisitor
		{
			private int _totalSize;

			public int TotalSize => _totalSize;

			public RecordSizingVisitor()
			{
				_totalSize = 0;
			}

			public void VisitRecord(Record r)
			{
				_totalSize += r.RecordSize;
			}
		}

		public virtual short Sid
		{
			get
			{
				throw new NotImplementedException("Should not be called");
			}
		}

		public override int RecordSize
		{
			get
			{
				RecordSizingVisitor recordSizingVisitor = new RecordSizingVisitor();
				VisitContainedRecords(recordSizingVisitor);
				return recordSizingVisitor.TotalSize;
			}
		}

		/// Visit each of the atomic BIFF records contained in this {@link RecordAggregate} in the order
		/// that they should be written to file.  Implementors may or may not return the actual 
		/// {@link Record}s being used to manage POI's internal implementation.  Callers should not
		/// assume either way, and therefore only attempt to modify those {@link Record}s after cloning
		public abstract void VisitContainedRecords(RecordVisitor rv);

		public override int Serialize(int offset, byte[] data)
		{
			SerializingRecordVisitor serializingRecordVisitor = new SerializingRecordVisitor(data, offset);
			VisitContainedRecords(serializingRecordVisitor);
			return serializingRecordVisitor.CountBytesWritten();
		}

		public virtual Record CloneViaReserialise()
		{
			throw new NotImplementedException("Please implement it in subclass");
		}
	}
}
