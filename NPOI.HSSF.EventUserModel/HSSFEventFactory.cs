using NPOI.HSSF.Record;
using NPOI.POIFS.FileSystem;
using System.IO;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// Low level event based HSSF Reader.  Pass either a DocumentInputStream to
	/// Process events along with a request object or pass a POIFS POIFSFileSystem to
	/// ProcessWorkbookEvents along with a request.
	/// This will cause your file to be Processed a record at a time.  Each record with
	/// a static id matching one that you have registed in your HSSFRequest will be passed
	/// to your associated HSSFListener.
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Carey Sublette  (careysub@earthling.net)
	/// </summary>
	public class HSSFEventFactory
	{
		/// <summary>
		/// Processes a file into essentially record events.
		/// </summary>
		/// <param name="req">an Instance of HSSFRequest which has your registered listeners</param>
		/// <param name="fs">a POIFS filesystem containing your workbook</param>
		public void ProcessWorkbookEvents(HSSFRequest req, POIFSFileSystem fs)
		{
			Stream @in = fs.CreateDocumentInputStream("Workbook");
			ProcessEvents(req, @in);
		}

		/// <summary>
		/// Processes a file into essentially record events.
		/// </summary>
		/// <param name="req">an Instance of HSSFRequest which has your registered listeners</param>
		/// <param name="fs">a POIFS filesystem containing your workbook</param>
		/// <returns>numeric user-specified result code.</returns>
		public short AbortableProcessWorkbookEvents(HSSFRequest req, POIFSFileSystem fs)
		{
			Stream @in = fs.CreateDocumentInputStream("Workbook");
			return AbortableProcessEvents(req, @in);
		}

		/// <summary>
		/// Processes a DocumentInputStream into essentially Record events.
		/// If an 
		/// <c>AbortableHSSFListener</c>
		///  causes a halt to Processing during this call
		/// the method will return just as with 
		/// <c>abortableProcessEvents</c>
		/// , but no
		/// user code or 
		/// <c>HSSFUserException</c>
		///  will be passed back.
		/// </summary>
		/// <param name="req">an Instance of HSSFRequest which has your registered listeners</param>
		/// <param name="in1">a DocumentInputStream obtained from POIFS's POIFSFileSystem object</param>
		public void ProcessEvents(HSSFRequest req, Stream in1)
		{
			try
			{
				GenericProcessEvents(req, new RecordInputStream(in1));
			}
			catch (HSSFUserException)
			{
			}
		}

		/// <summary>
		/// Processes a DocumentInputStream into essentially Record events.
		/// </summary>
		/// <param name="req">an Instance of HSSFRequest which has your registered listeners</param>
		/// <param name="in1">a DocumentInputStream obtained from POIFS's POIFSFileSystem object</param>
		/// <returns>numeric user-specified result code.</returns>
		public short AbortableProcessEvents(HSSFRequest req, Stream in1)
		{
			return GenericProcessEvents(req, new RecordInputStream(in1));
		}

		/// <summary>
		/// Processes a DocumentInputStream into essentially Record events.
		/// </summary>
		/// <param name="req">an Instance of HSSFRequest which has your registered listeners</param>
		/// <param name="in1">a DocumentInputStream obtained from POIFS's POIFSFileSystem object</param>
		/// <returns>numeric user-specified result code.</returns>
		protected short GenericProcessEvents(HSSFRequest req, RecordInputStream in1)
		{
			bool flag = true;
			short num = 0;
			NPOI.HSSF.Record.Record record = null;
			HSSFRecordStream hSSFRecordStream = new HSSFRecordStream(in1);
			while (flag)
			{
				record = hSSFRecordStream.NextRecord();
				if (record != null)
				{
					num = req.ProcessRecord(record);
					if (num != 0)
					{
						break;
					}
				}
				else
				{
					flag = false;
				}
			}
			return num;
		}
	}
}
