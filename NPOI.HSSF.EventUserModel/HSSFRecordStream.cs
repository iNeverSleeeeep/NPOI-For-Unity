using NPOI.HSSF.Record;
using NPOI.Util;
using System.Collections;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// A stream based way to Get at complete records, with
	/// as low a memory footprint as possible.
	/// This handles Reading from a RecordInputStream, turning
	/// the data into full records, Processing continue records
	/// etc.
	/// Most users should use HSSFEventFactory 
	/// HSSFListener and have new records pushed to
	/// them, but this does allow for a "pull" style of coding. 
	/// </summary>
	public class HSSFRecordStream
	{
		private RecordInputStream in1;

		/// Have we run out of records on the stream? 
		private bool hitEOS;

		/// Have we returned all the records there are? 
		private bool complete;

		/// Sometimes we end up with a bunch of
		///  records. When we do, these should
		///  be returned before the next normal
		///  record Processing occurs (i.e. before
		///  we Check for continue records and
		///  return rec)
		private ArrayList bonusRecords;

		/// The next record to return, which may need to have its
		///  continue records passed to it before we do
		private NPOI.HSSF.Record.Record rec;

		/// The most recent record that we gave to the user
		private NPOI.HSSF.Record.Record lastRec;

		/// The most recent DrawingRecord seen
		private DrawingRecord lastDrawingRecord = new DrawingRecord();

		public HSSFRecordStream(RecordInputStream inp)
		{
			in1 = inp;
		}

		/// <summary>
		/// Returns the next (complete) record from the
		/// stream, or null if there are no more.
		/// </summary>
		/// <returns></returns>
		public NPOI.HSSF.Record.Record NextRecord()
		{
			NPOI.HSSF.Record.Record record = null;
			while (record == null && !complete)
			{
				record = GetBonusRecord();
				if (record == null)
				{
					record = GetNextRecord();
				}
			}
			return record;
		}

		/// <summary>
		/// If there are any "bonus" records, that should
		/// be returned before Processing new ones,
		/// grabs the next and returns it.
		/// If not, returns null;
		/// </summary>
		/// <returns></returns>
		private NPOI.HSSF.Record.Record GetBonusRecord()
		{
			if (bonusRecords != null)
			{
				NPOI.HSSF.Record.Record result = (NPOI.HSSF.Record.Record)bonusRecords[0];
				bonusRecords.RemoveAt(0);
				if (bonusRecords.Count == 0)
				{
					bonusRecords = null;
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// Returns the next available record, or null if
		/// this pass didn't return a record that's
		/// suitable for returning (eg was a continue record).
		/// </summary>
		/// <returns></returns>
		private NPOI.HSSF.Record.Record GetNextRecord()
		{
			NPOI.HSSF.Record.Record result = null;
			if (in1.HasNextRecord)
			{
				in1.NextRecord();
				short sid = in1.Sid;
				if (sid == 0)
				{
					return null;
				}
				if (rec != null && sid != 60)
				{
					result = rec;
				}
				if (sid != 60)
				{
					NPOI.HSSF.Record.Record[] array = RecordFactory.CreateRecord(in1);
					if (array.Length > 1)
					{
						bonusRecords = new ArrayList(array.Length - 1);
						for (int i = 0; i < array.Length - 1; i++)
						{
							bonusRecords.Add(array[i]);
						}
					}
					rec = array[array.Length - 1];
				}
				else
				{
					NPOI.HSSF.Record.Record[] array2 = RecordFactory.CreateRecord(in1);
					ContinueRecord continueRecord = (ContinueRecord)array2[0];
					if (lastRec is ObjRecord || lastRec is TextObjectRecord)
					{
						lastDrawingRecord.ProcessContinueRecord(continueRecord.Data);
						rec = lastDrawingRecord;
					}
					else if (lastRec is DrawingGroupRecord)
					{
						((DrawingGroupRecord)lastRec).ProcessContinueRecord(continueRecord.Data);
						rec = lastRec;
					}
					else if (!(rec is UnknownRecord))
					{
						throw new RecordFormatException("Records should handle ContinueRecord internally. Should not see this exception");
					}
				}
				lastRec = rec;
				if (rec is DrawingRecord)
				{
					lastDrawingRecord = (DrawingRecord)rec;
				}
			}
			else
			{
				hitEOS = true;
			}
			if (hitEOS)
			{
				complete = true;
				if (rec != null)
				{
					result = rec;
					rec = null;
				}
			}
			return result;
		}
	}
}
