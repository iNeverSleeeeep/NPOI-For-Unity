using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// When working with the EventUserModel, if you want to
	/// Process formulas, you need an instance of
	/// Workbook to pass to a HSSFWorkbook,
	/// to finally give to HSSFFormulaParser,
	/// and this will build you stub ones.
	/// Since you're working with the EventUserModel, you
	/// wouldn't want to Get a full Workbook and
	///  HSSFWorkbook, as they would eat too much memory.
	/// Instead, you should collect a few key records as they
	/// go past, then call this once you have them to build a
	/// stub Workbook, and from that a stub
	/// HSSFWorkbook, to use with the HSSFFormulaParser.
	/// The records you should collect are:
	/// ExternSheetRecord
	/// BoundSheetRecord
	/// You should probably also collect SSTRecord,
	/// but it's not required to pass this in.
	/// To help, this class includes a HSSFListener wrapper
	/// that will do the collecting for you.
	/// </summary>
	public class EventWorkbookBuilder
	{
		/// <summary>
		/// A wrapping HSSFListener which will collect
		/// BoundSheetRecords and {@link ExternSheetRecord}s as
		/// they go past, so you can Create a Stub {@link Workbook} from
		/// them once required.
		/// </summary>
		public class SheetRecordCollectingListener : IHSSFListener
		{
			private IHSSFListener childListener;

			private ArrayList boundSheetRecords = new ArrayList();

			private ArrayList externSheetRecords = new ArrayList();

			private SSTRecord sstRecord;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:NPOI.HSSF.EventUserModel.EventWorkbookBuilder.SheetRecordCollectingListener" /> class.
			/// </summary>
			/// <param name="childListener">The child listener.</param>
			public SheetRecordCollectingListener(IHSSFListener childListener)
			{
				this.childListener = childListener;
			}

			/// <summary>
			/// Gets the bound sheet records.
			/// </summary>
			/// <returns></returns>
			public BoundSheetRecord[] GetBoundSheetRecords()
			{
				return (BoundSheetRecord[])boundSheetRecords.ToArray(typeof(BoundSheetRecord));
			}

			/// <summary>
			/// Gets the extern sheet records.
			/// </summary>
			/// <returns></returns>
			public ExternSheetRecord[] GetExternSheetRecords()
			{
				return (ExternSheetRecord[])externSheetRecords.ToArray(typeof(ExternSheetRecord));
			}

			/// <summary>
			/// Gets the SST record.
			/// </summary>
			/// <returns></returns>
			public SSTRecord GetSSTRecord()
			{
				return sstRecord;
			}

			/// <summary>
			/// Gets the stub HSSF workbook.
			/// </summary>
			/// <returns></returns>
			public HSSFWorkbook GetStubHSSFWorkbook()
			{
				return CreateStubHSSFWorkbook(GetStubWorkbook());
			}

			/// <summary>
			/// Gets the stub workbook.
			/// </summary>
			/// <returns></returns>
			public InternalWorkbook GetStubWorkbook()
			{
				return CreateStubWorkbook(GetExternSheetRecords(), GetBoundSheetRecords(), GetSSTRecord());
			}

			/// <summary>
			/// Process this record ourselves, and then
			/// pass it on to our child listener
			/// </summary>
			/// <param name="record">The record.</param>
			public void ProcessRecord(NPOI.HSSF.Record.Record record)
			{
				ProcessRecordInternally(record);
				childListener.ProcessRecord(record);
			}

			/// <summary>
			/// Process the record ourselves, but do not
			/// pass it on to the child Listener.
			/// </summary>
			/// <param name="record">The record.</param>
			public void ProcessRecordInternally(NPOI.HSSF.Record.Record record)
			{
				if (record is BoundSheetRecord)
				{
					boundSheetRecords.Add(record);
				}
				else if (record is ExternSheetRecord)
				{
					externSheetRecords.Add(record);
				}
				else if (record is SSTRecord)
				{
					sstRecord = (SSTRecord)record;
				}
			}
		}

		/// Let us at the {@link Workbook} constructor on
		///  {@link HSSFWorkbook}
		private class StubHSSFWorkbook : HSSFWorkbook
		{
			public StubHSSFWorkbook(InternalWorkbook wb)
				: base(wb)
			{
			}
		}

		/// <summary>
		/// Wraps up your stub Workbook as a stub HSSFWorkbook, ready for passing to HSSFFormulaParser
		/// </summary>
		/// <param name="workbook">The stub workbook.</param>
		/// <returns></returns>
		public static HSSFWorkbook CreateStubHSSFWorkbook(InternalWorkbook workbook)
		{
			return new StubHSSFWorkbook(workbook);
		}

		/// <summary>
		/// Creates a stub Workbook from the supplied records,
		/// suitable for use with the {@link HSSFFormulaParser}
		/// </summary>
		/// <param name="externs">The ExternSheetRecords in your file</param>
		/// <param name="bounds">The BoundSheetRecords in your file</param>
		/// <param name="sst">TThe SSTRecord in your file.</param>
		/// <returns>A stub Workbook suitable for use with HSSFFormulaParser</returns>
		public static InternalWorkbook CreateStubWorkbook(ExternSheetRecord[] externs, BoundSheetRecord[] bounds, SSTRecord sst)
		{
			List<NPOI.HSSF.Record.Record> list = new List<NPOI.HSSF.Record.Record>();
			if (bounds != null)
			{
				for (int i = 0; i < bounds.Length; i++)
				{
					list.Add(bounds[i]);
				}
			}
			if (sst != null)
			{
				list.Add(sst);
			}
			if (externs != null)
			{
				list.Add(SupBookRecord.CreateInternalReferences((short)externs.Length));
				for (int j = 0; j < externs.Length; j++)
				{
					list.Add(externs[j]);
				}
			}
			list.Add(EOFRecord.instance);
			return InternalWorkbook.CreateWorkbook(list);
		}

		/// <summary>
		/// Creates a stub workbook from the supplied records,
		/// suitable for use with the HSSFFormulaParser
		/// </summary>
		/// <param name="externs">The ExternSheetRecords in your file</param>
		/// <param name="bounds">A stub Workbook suitable for use with HSSFFormulaParser</param>
		/// <returns>A stub Workbook suitable for use with {@link HSSFFormulaParser}</returns>
		public static InternalWorkbook CreateStubWorkbook(ExternSheetRecord[] externs, BoundSheetRecord[] bounds)
		{
			return CreateStubWorkbook(externs, bounds, null);
		}
	}
}
