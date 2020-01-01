namespace NPOI.HSSF.Record.Aggregates
{
	/// A wrapper for {@link RecordVisitor} which accumulates the sizes of all
	/// records visited.
	public class PositionTrackingVisitor : RecordVisitor
	{
		private RecordVisitor _rv;

		private int _position;

		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public PositionTrackingVisitor(RecordVisitor rv, int initialPosition)
		{
			_rv = rv;
			_position = initialPosition;
		}

		public void VisitRecord(Record r)
		{
			_position += r.RecordSize;
			_rv.VisitRecord(r);
		}
	}
}
