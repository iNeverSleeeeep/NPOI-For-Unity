using NPOI.HSSF.Model;
using NPOI.SS.Formula;
using System;
using System.Collections;

namespace NPOI.HSSF.Record.Aggregates
{
	/// Holds all the conditional formatting for a workbook sheet.<p />
	///
	/// See OOO exelfileformat.pdf sec 4.12 'Conditional Formatting Table'
	///
	/// @author Josh Micich
	public class ConditionalFormattingTable : RecordAggregate
	{
		private IList _cfHeaders;

		public int Count => _cfHeaders.Count;

		/// Creates an empty ConditionalFormattingTable
		public ConditionalFormattingTable()
		{
			_cfHeaders = new ArrayList();
		}

		public ConditionalFormattingTable(RecordStream rs)
		{
			IList list = new ArrayList();
			while (rs.PeekNextClass() == typeof(CFHeaderRecord))
			{
				list.Add(CFRecordsAggregate.CreateCFAggregate(rs));
			}
			_cfHeaders = list;
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			for (int i = 0; i < _cfHeaders.Count; i++)
			{
				CFRecordsAggregate cFRecordsAggregate = (CFRecordsAggregate)_cfHeaders[i];
				cFRecordsAggregate.VisitContainedRecords(rv);
			}
		}

		/// @return index of the newly added CF header aggregate
		public int Add(CFRecordsAggregate cfAggregate)
		{
			_cfHeaders.Add(cfAggregate);
			return _cfHeaders.Count - 1;
		}

		public CFRecordsAggregate Get(int index)
		{
			CheckIndex(index);
			return (CFRecordsAggregate)_cfHeaders[index];
		}

		public void Remove(int index)
		{
			CheckIndex(index);
			_cfHeaders.RemoveAt(index);
		}

		private void CheckIndex(int index)
		{
			if (index < 0 || index >= _cfHeaders.Count)
			{
				throw new ArgumentException("Specified CF index " + index + " is outside the allowable range (0.." + (_cfHeaders.Count - 1) + ")");
			}
		}

		public void UpdateFormulasAfterCellShift(FormulaShifter shifter, int externSheetIndex)
		{
			for (int i = 0; i < _cfHeaders.Count; i++)
			{
				CFRecordsAggregate cFRecordsAggregate = (CFRecordsAggregate)_cfHeaders[i];
				if (!cFRecordsAggregate.UpdateFormulasAfterCellShift(shifter, externSheetIndex))
				{
					_cfHeaders.RemoveAt(i);
					i--;
				}
			}
		}
	}
}
