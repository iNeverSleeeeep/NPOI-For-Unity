using NPOI.HSSF.Record;
using System.Collections;
using System.IO;

namespace NPOI.HSSF.EventModel
{
	/// Event-based record factory.  As opposed to RecordFactory
	/// this refactored version throws record events as it comes
	/// accross the records.  I throws the "lazily" one record behind
	/// to ensure that ContinueRecords are Processed first.
	///
	/// @author Andrew C. Oliver (acoliver@apache.org) - probably to blame for the bugs (so yank his chain on the list)
	/// @author Marc Johnson (mjohnson at apache dot org) - methods taken from RecordFactory
	/// @author Glen Stampoultzis (glens at apache.org) - methods taken from RecordFactory
	/// @author Csaba Nagy (ncsaba at yahoo dot com)
	public class EventRecordFactory
	{
		private IERFListener _listener;

		private ArrayList _sids;

		/// Create an EventRecordFactory
		/// @param abortable specifies whether the return from the listener 
		/// handler functions are obeyed.  False means they are ignored. True
		/// means the event loop exits on error.
		public EventRecordFactory(IERFListener listener, ArrayList sids)
		{
			_listener = listener;
			_sids = sids;
			if (_sids == null)
			{
				_sids = null;
			}
			else
			{
				if (_sids == null)
				{
					_sids = new ArrayList();
				}
				_sids.Sort();
			}
		}

		private bool IsSidIncluded(int sid)
		{
			if (_sids == null)
			{
				return true;
			}
			return _sids.BinarySearch((short)sid) >= 0;
		}

		/// sends the record event to all registered listeners.
		/// @param record the record to be thrown.
		/// @return <c>false</c> to abort.  This aborts
		/// out of the event loop should the listener return false
		private bool ProcessRecord(NPOI.HSSF.Record.Record record)
		{
			if (!IsSidIncluded(record.Sid))
			{
				return true;
			}
			return _listener.ProcessRecord(record);
		}

		/// Create an array of records from an input stream
		///
		/// @param in the InputStream from which the records will be
		///           obtained
		///
		/// @exception RecordFormatException on error Processing the
		///            InputStream
		public void ProcessRecords(Stream in1)
		{
			NPOI.HSSF.Record.Record record = null;
			RecordInputStream recordInputStream = new RecordInputStream(in1);
			while (recordInputStream.HasNextRecord)
			{
				recordInputStream.NextRecord();
				NPOI.HSSF.Record.Record[] array = RecordFactory.CreateRecord(recordInputStream);
				if (array.Length > 1)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (record != null && !ProcessRecord(record))
						{
							return;
						}
						record = array[i];
					}
				}
				else
				{
					NPOI.HSSF.Record.Record record2 = array[0];
					if (record2 != null)
					{
						if (record != null && !ProcessRecord(record))
						{
							return;
						}
						record = record2;
					}
				}
			}
			if (record != null)
			{
				ProcessRecord(record);
			}
		}
	}
}
