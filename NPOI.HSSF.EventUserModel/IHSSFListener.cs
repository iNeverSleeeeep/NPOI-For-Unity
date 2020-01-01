using NPOI.HSSF.Record;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// Interface for use with the HSSFRequest and HSSFEventFactory.  Users should Create
	/// a listener supporting this interface and register it with the HSSFRequest (associating
	/// it with Record SID's).
	/// @author  acoliver@apache.org
	/// </summary>
	public interface IHSSFListener
	{
		/// <summary>
		/// Process an HSSF Record. Called when a record occurs in an HSSF file.
		/// </summary>
		/// <param name="record">The record.</param>
		void ProcessRecord(NPOI.HSSF.Record.Record record);
	}
}
