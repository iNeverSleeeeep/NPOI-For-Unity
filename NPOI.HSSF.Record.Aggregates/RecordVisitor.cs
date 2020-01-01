namespace NPOI.HSSF.Record.Aggregates
{
	public interface RecordVisitor
	{
		/// Implementors may call non-mutating methods on Record r.
		/// @param r must not be <c>null</c>
		void VisitRecord(Record r);
	}
}
