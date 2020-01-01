namespace NPOI.POIFS.EventFileSystem
{
	/// Interface POIFSReaderListener
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @version %I%, %G%
	public interface POIFSReaderListener
	{
		/// Process a POIFSReaderEvent that this listener had Registered
		/// for
		///
		/// @param event the POIFSReaderEvent
		void ProcessPOIFSReaderEvent(POIFSReaderEvent evt);
	}
}
