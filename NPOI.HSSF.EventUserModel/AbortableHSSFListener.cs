using NPOI.HSSF.Record;

namespace NPOI.HSSF.EventUserModel
{
	/// Interface for use with the HSSFRequest and HSSFEventFactory.  Users should Create
	/// a listener supporting this interface and register it with the HSSFRequest (associating
	/// it with Record SID's).
	///
	/// @see org.apache.poi.hssf.eventusermodel.HSSFEventFactory
	/// @see org.apache.poi.hssf.eventusermodel.HSSFRequest
	/// @see org.apache.poi.hssf.eventusermodel.HSSFUserException
	///
	/// @author Carey Sublette (careysub@earthling.net)
	public abstract class AbortableHSSFListener : IHSSFListener
	{
		/// This method, inherited from HSSFListener Is implemented as a stub.
		/// It Is never called by HSSFEventFActory or HSSFRequest.
		public virtual void ProcessRecord(NPOI.HSSF.Record.Record record)
		{
		}

		/// Process an HSSF Record. Called when a record occurs in an HSSF file. 
		/// Provides two options for halting the Processing of the HSSF file.
		///
		/// The return value provides a means of non-error termination with a 
		/// user-defined result code. A value of zero must be returned to 
		/// continue Processing, any other value will halt Processing by
		/// <c>HSSFEventFactory</c> with the code being passed back by 
		/// its abortable Process events methods.
		///
		/// Error termination can be done by throwing the HSSFUserException.
		///
		/// Note that HSSFEventFactory will not call the inherited Process 
		///
		/// @return result code of zero for continued Processing.
		///
		/// @throws HSSFUserException User code can throw this to abort 
		/// file Processing by HSSFEventFactory and return diagnostic information.
		public abstract short AbortableProcessRecord(NPOI.HSSF.Record.Record record);
	}
}
