using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Model
{
	/// Segregates the 'Row Blocks' section of a single sheet into plain row/cell records and 
	/// shared formula records.
	///
	/// @author Josh Micich
	public class RowBlocksReader
	{
		private ArrayList _plainRecords;

		private SharedValueManager _sfm;

		private MergeCellsRecord[] _mergedCellsRecords;

		/// Some unconventional apps place {@link MergeCellsRecord}s within the row block.  They 
		/// actually should be in the {@link MergedCellsTable} which is much later (see bug 45699).
		/// @return any loose  <c>MergeCellsRecord</c>s found
		public MergeCellsRecord[] LooseMergedCells => _mergedCellsRecords;

		public SharedValueManager SharedFormulaManager => _sfm;

		/// @return a {@link RecordStream} containing all the non-{@link SharedFormulaRecord} 
		/// non-{@link ArrayRecord} and non-{@link TableRecord} Records.
		public RecordStream PlainRecordStream => new RecordStream(_plainRecords, 0);

		/// Also collects any loose MergeCellRecords and puts them in the supplied
		/// mergedCellsTable
		public RowBlocksReader(RecordStream rs)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			ArrayList arrayList5 = new ArrayList();
			List<CellReference> list = new List<CellReference>();
			NPOI.HSSF.Record.Record record = null;
			while (!RecordOrderer.IsEndOfRowBlock(rs.PeekNextSid()))
			{
				if (!rs.HasNext())
				{
					throw new InvalidOperationException("Failed to find end of row/cell records");
				}
				NPOI.HSSF.Record.Record next = rs.GetNext();
				ArrayList arrayList6;
				switch (next.Sid)
				{
				case 229:
					arrayList6 = arrayList5;
					break;
				case 1212:
				{
					arrayList6 = arrayList2;
					if (!(record is FormulaRecord))
					{
						throw new Exception("Shared formula record should follow a FormulaRecord");
					}
					FormulaRecord formulaRecord = (FormulaRecord)record;
					list.Add(new CellReference(formulaRecord.Row, formulaRecord.Column));
					break;
				}
				case 545:
					arrayList6 = arrayList3;
					break;
				case 566:
					arrayList6 = arrayList4;
					break;
				default:
					arrayList6 = arrayList;
					break;
				}
				arrayList6.Add(next);
				record = next;
			}
			SharedFormulaRecord[] array = new SharedFormulaRecord[arrayList2.Count];
			List<ArrayRecord> list2 = new List<ArrayRecord>(arrayList3.Count);
			List<TableRecord> list3 = new List<TableRecord>(arrayList4.Count);
			array = (SharedFormulaRecord[])arrayList2.ToArray(typeof(SharedFormulaRecord));
			CellReference[] array2 = new CellReference[list.Count];
			array2 = list.ToArray();
			list2 = new List<ArrayRecord>((ArrayRecord[])arrayList3.ToArray(typeof(ArrayRecord)));
			list3 = new List<TableRecord>((TableRecord[])arrayList4.ToArray(typeof(TableRecord)));
			_plainRecords = arrayList;
			_sfm = SharedValueManager.Create(array, array2, list2, list3);
			_mergedCellsRecords = new MergeCellsRecord[arrayList5.Count];
			_mergedCellsRecords = (MergeCellsRecord[])arrayList5.ToArray(typeof(MergeCellsRecord));
		}
	}
}
