using NPOI.HSSF.Record;

namespace NPOI.HSSF.EventModel
{
	/// An ERFListener Is registered with the EventRecordFactory.
	/// An ERFListener listens for Records coming from the stream
	/// via the EventRecordFactory
	///
	/// @see EventRecordFactory
	/// @author Andrew C. Oliver acoliver@apache.org
	public interface IERFListener
	{
		/// Process a Record.  This method Is called by the 
		/// EventRecordFactory when a record Is returned.
		/// @return bool specifying whether the effort was a success.
		bool ProcessRecord(NPOI.HSSF.Record.Record rec);
	}
}
