namespace NPOI.POIFS.EventFileSystem
{
	/// Interface POIFSWriterListener
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @version %I%, %G%
	public interface POIFSWriterListener
	{
		/// Process a POIFSWriterEvent that this listener had registered
		/// for
		///
		/// @param event the POIFSWriterEvent
		void ProcessPOIFSWriterEvent(POIFSWriterEvent event1);
	}
}
