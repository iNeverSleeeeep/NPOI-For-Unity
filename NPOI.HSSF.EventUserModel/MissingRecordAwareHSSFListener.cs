using NPOI.HSSF.EventUserModel.DummyRecord;
using NPOI.HSSF.Record;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// A HSSFListener which tracks rows and columns, and will
	/// trigger your HSSFListener for all rows and cells,
	/// even the ones that aren't actually stored in the file.
	/// This allows your code to have a more "Excel" like
	/// view of the data in the file, and not have to worry
	/// (as much) about if a particular row/cell Is in the
	/// file, or was skipped from being written as it was
	/// blank.
	/// </summary>
	public class MissingRecordAwareHSSFListener : IHSSFListener
	{
		private IHSSFListener childListener;

		private int lastRowRow;

		private int lastCellRow;

		private int lastCellColumn;

		/// <summary>
		/// Constructs a new MissingRecordAwareHSSFListener, which
		/// will fire ProcessRecord on the supplied child
		/// HSSFListener for all Records, and missing records.
		/// </summary>
		/// <param name="listener">The HSSFListener to pass records on to</param>
		public MissingRecordAwareHSSFListener(IHSSFListener listener)
		{
			ResetCounts();
			childListener = listener;
		}

		/// <summary>
		/// Process an HSSF Record. Called when a record occurs in an HSSF file.
		/// </summary>
		/// <param name="record"></param>
		public void ProcessRecord(NPOI.HSSF.Record.Record record)
		{
			CellValueRecordInterface[] array = null;
			int num;
			int num2;
			if (record is CellValueRecordInterface)
			{
				CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)record;
				num = cellValueRecordInterface.Row;
				num2 = cellValueRecordInterface.Column;
			}
			else
			{
				if (record is StringRecord)
				{
					childListener.ProcessRecord(record);
					return;
				}
				num = -1;
				num2 = -1;
				switch (record.Sid)
				{
				case 2057:
				{
					BOFRecord bOFRecord = (BOFRecord)record;
					if (bOFRecord.Type == BOFRecordType.Workbook || bOFRecord.Type == BOFRecordType.Worksheet)
					{
						ResetCounts();
					}
					break;
				}
				case 520:
				{
					RowRecord rowRecord = (RowRecord)record;
					if (lastRowRow + 1 < rowRecord.RowNumber)
					{
						for (int i = lastRowRow + 1; i < rowRecord.RowNumber; i++)
						{
							MissingRowDummyRecord record2 = new MissingRowDummyRecord(i);
							childListener.ProcessRecord(record2);
						}
					}
					lastRowRow = rowRecord.RowNumber;
					break;
				}
				case 1212:
					childListener.ProcessRecord(record);
					return;
				case 190:
				{
					MulBlankRecord mbk = (MulBlankRecord)record;
					array = RecordFactory.ConvertBlankRecords(mbk);
					break;
				}
				case 189:
				{
					MulRKRecord mrk = (MulRKRecord)record;
					array = RecordFactory.ConvertRKRecords(mrk);
					break;
				}
				case 28:
				{
					NoteRecord noteRecord = (NoteRecord)record;
					num = noteRecord.Row;
					num2 = noteRecord.Column;
					break;
				}
				}
			}
			if (array != null && array.Length > 0)
			{
				num = array[0].Row;
				num2 = array[0].Column;
			}
			if (num != lastCellRow && lastCellRow > -1)
			{
				for (int j = lastCellRow; j < num; j++)
				{
					int lastColumnNumber = -1;
					if (j == lastCellRow)
					{
						lastColumnNumber = lastCellColumn;
					}
					childListener.ProcessRecord(new LastCellOfRowDummyRecord(j, lastColumnNumber));
				}
			}
			if (lastCellRow != -1 && lastCellColumn != -1 && num == -1)
			{
				childListener.ProcessRecord(new LastCellOfRowDummyRecord(lastCellRow, lastCellColumn));
				lastCellRow = -1;
				lastCellColumn = -1;
			}
			if (num != lastCellRow)
			{
				lastCellColumn = -1;
			}
			if (lastCellColumn != num2 - 1)
			{
				for (int k = lastCellColumn + 1; k < num2; k++)
				{
					childListener.ProcessRecord(new MissingCellDummyRecord(num, k));
				}
			}
			if (array != null && array.Length > 0)
			{
				num2 = array[array.Length - 1].Column;
			}
			if (num2 != -1)
			{
				lastCellColumn = num2;
				lastCellRow = num;
			}
			if (array != null && array.Length > 0)
			{
				CellValueRecordInterface[] array2 = array;
				foreach (CellValueRecordInterface cellValueRecordInterface2 in array2)
				{
					childListener.ProcessRecord((NPOI.HSSF.Record.Record)cellValueRecordInterface2);
				}
			}
			else
			{
				childListener.ProcessRecord(record);
			}
		}

		private void ResetCounts()
		{
			lastRowRow = -1;
			lastCellRow = -1;
			lastCellColumn = -1;
		}
	}
}
