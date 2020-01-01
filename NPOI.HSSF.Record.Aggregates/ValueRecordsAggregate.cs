using NPOI.HSSF.Model;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using System;
using System.Collections;

namespace NPOI.HSSF.Record.Aggregates
{
	/// Aggregate value records toGether.  Things are easier to handle that way.
	///
	/// @author  andy
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class ValueRecordsAggregate
	{
		private class MyEnumerator : IEnumerator
		{
			private short nextColumn = -1;

			private int nextRow;

			private int lastRow;

			private CellValueRecordInterface[][] records;

			public object Current => records[nextRow][nextColumn];

			public MyEnumerator(ref CellValueRecordInterface[][] _records)
			{
				records = _records;
				nextRow = 0;
				lastRow = _records.Length - 1;
			}

			public MyEnumerator(ref CellValueRecordInterface[][] _records, int firstRow, int lastRow)
			{
				records = _records;
				nextRow = firstRow;
				this.lastRow = lastRow;
			}

			public bool MoveNext()
			{
				FindNext();
				return nextRow <= lastRow;
			}

			public void Remove()
			{
				throw new InvalidOperationException("gibt's noch nicht");
			}

			private void FindNext()
			{
				nextColumn++;
				while (nextRow <= lastRow)
				{
					CellValueRecordInterface[] array = (nextRow < records.Length) ? records[nextRow] : null;
					if (array == null)
					{
						nextColumn = 0;
					}
					else
					{
						while (nextColumn < array.Length)
						{
							if (array[nextColumn] != null)
							{
								return;
							}
							nextColumn++;
						}
						nextColumn = 0;
					}
					nextRow++;
				}
			}

			public void Reset()
			{
				nextColumn = -1;
				nextRow = 0;
			}
		}

		private const int MAX_ROW_INDEX = 65535;

		private const int INDEX_NOT_SET = -1;

		public const short sid = -1001;

		private int firstcell = -1;

		private int lastcell = -1;

		private CellValueRecordInterface[][] records;

		public int PhysicalNumberOfCells
		{
			get
			{
				int num = 0;
				for (int i = 0; i < records.Length; i++)
				{
					CellValueRecordInterface[] array = records[i];
					if (array != null)
					{
						for (short num2 = 0; num2 < array.Length; num2 = (short)(num2 + 1))
						{
							if (array[num2] != null)
							{
								num++;
							}
						}
					}
				}
				return num;
			}
		}

		public int FirstCellNum => firstcell;

		public int LastCellNum => lastcell;

		/// Creates a new instance of ValueRecordsAggregate 
		public ValueRecordsAggregate()
			: this(-1, -1, new CellValueRecordInterface[30][])
		{
		}

		private ValueRecordsAggregate(int firstCellIx, int lastCellIx, CellValueRecordInterface[][] pRecords)
		{
			firstcell = firstCellIx;
			lastcell = lastCellIx;
			records = pRecords;
		}

		public void InsertCell(CellValueRecordInterface cell)
		{
			int column = cell.Column;
			int row = cell.Row;
			if (row >= records.Length)
			{
				CellValueRecordInterface[][] array = records;
				int num = array.Length * 2;
				if (num < row + 1)
				{
					num = row + 1;
				}
				records = new CellValueRecordInterface[num][];
				Array.Copy(array, 0, records, 0, array.Length);
			}
			object obj = records[row];
			if (obj == null)
			{
				int num2 = column + 1;
				if (num2 < 10)
				{
					num2 = 10;
				}
				obj = new CellValueRecordInterface[num2];
				records[row] = (CellValueRecordInterface[])obj;
			}
			CellValueRecordInterface[] array2 = (CellValueRecordInterface[])obj;
			if (column >= array2.Length)
			{
				CellValueRecordInterface[] array3 = array2;
				int num3 = array3.Length * 2;
				if (num3 < column + 1)
				{
					num3 = column + 1;
				}
				array2 = new CellValueRecordInterface[num3];
				Array.Copy(array3, 0, array2, 0, array3.Length);
				records[row] = array2;
			}
			array2[column] = cell;
			if (column < firstcell || firstcell == -1)
			{
				firstcell = column;
			}
			if (column > lastcell || lastcell == -1)
			{
				lastcell = column;
			}
		}

		public void RemoveCell(CellValueRecordInterface cell)
		{
			if (cell == null)
			{
				throw new ArgumentException("cell must not be null");
			}
			int row = cell.Row;
			if (row >= records.Length)
			{
				throw new Exception("cell row is out of range");
			}
			CellValueRecordInterface[] array = records[row];
			if (array == null)
			{
				throw new Exception("cell row is already empty");
			}
			int column = cell.Column;
			if (column >= array.Length)
			{
				throw new Exception("cell column is out of range");
			}
			array[column] = null;
		}

		public void RemoveAllCellsValuesForRow(int rowIndex)
		{
			if (rowIndex < 0 || rowIndex > 65535)
			{
				throw new ArgumentException("Specified rowIndex " + rowIndex + " is outside the allowable range (0.." + 65535 + ")");
			}
			if (rowIndex < records.Length)
			{
				records[rowIndex] = null;
			}
		}

		public CellValueRecordInterface[] GetValueRecords()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < records.Length; i++)
			{
				CellValueRecordInterface[] array = records[i];
				if (array != null)
				{
					foreach (CellValueRecordInterface cellValueRecordInterface in array)
					{
						if (cellValueRecordInterface != null)
						{
							arrayList.Add(cellValueRecordInterface);
						}
					}
				}
			}
			return (CellValueRecordInterface[])arrayList.ToArray(typeof(CellValueRecordInterface));
		}

		public void AddMultipleBlanks(MulBlankRecord mbr)
		{
			for (int i = 0; i < mbr.NumColumns; i++)
			{
				BlankRecord blankRecord = new BlankRecord();
				blankRecord.Column = i + mbr.FirstColumn;
				blankRecord.Row = mbr.Row;
				blankRecord.XFIndex = mbr.GetXFAt(i);
				InsertCell(blankRecord);
			}
		}

		private MulBlankRecord CreateMBR(CellValueRecordInterface[] cellValues, int startIx, int nBlank)
		{
			short[] array = new short[nBlank];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((BlankRecord)cellValues[startIx + i]).XFIndex;
			}
			int row = cellValues[startIx].Row;
			return new MulBlankRecord(row, startIx, array);
		}

		public void Construct(CellValueRecordInterface rec, RecordStream rs, SharedValueManager sfh)
		{
			if (rec is FormulaRecord)
			{
				FormulaRecord formulaRec = (FormulaRecord)rec;
				StringRecord stringRecord = null;
				Type left = rs.PeekNextClass();
				stringRecord = ((!(left == typeof(StringRecord))) ? null : ((StringRecord)rs.GetNext()));
				InsertCell(new FormulaRecordAggregate(formulaRec, stringRecord, sfh));
			}
			else
			{
				InsertCell(rec);
			}
		}

		/// Sometimes the shared formula flag "seems" to be erroneously Set, in which case there is no 
		/// call to <c>SharedFormulaRecord.ConvertSharedFormulaRecord</c> and hence the 
		/// <c>ParsedExpression</c> field of this <c>FormulaRecord</c> will not Get updated.<br />
		/// As it turns out, this is not a problem, because in these circumstances, the existing value
		/// for <c>ParsedExpression</c> is perfectly OK.<p />
		///
		/// This method may also be used for Setting breakpoints to help diagnose Issues regarding the
		/// abnormally-Set 'shared formula' flags. 
		/// (see TestValueRecordsAggregate.testSpuriousSharedFormulaFlag()).<p />
		///
		/// The method currently does nothing but do not delete it without Finding a nice home for this 
		/// comment.
		private static void HandleMissingSharedFormulaRecord(FormulaRecord formula)
		{
		}

		/// Tallies a count of the size of the cell records
		/// that are attached to the rows in the range specified.
		public int GetRowCellBlockSize(int startRow, int endRow)
		{
			MyEnumerator myEnumerator = new MyEnumerator(ref records, startRow, endRow);
			int num = 0;
			while (myEnumerator.MoveNext())
			{
				CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)myEnumerator.Current;
				int row = cellValueRecordInterface.Row;
				if (row > endRow)
				{
					break;
				}
				if (row >= startRow && row <= endRow)
				{
					num += ((RecordBase)cellValueRecordInterface).RecordSize;
				}
			}
			return num;
		}

		/// Returns true if the row has cells attached to it 
		public bool RowHasCells(int row)
		{
			if (row > records.Length - 1)
			{
				return false;
			}
			CellValueRecordInterface[] array = records[row];
			if (array == null)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return true;
				}
			}
			return false;
		}

		public void UpdateFormulasAfterRowShift(FormulaShifter shifter, int currentExternSheetIndex)
		{
			for (int i = 0; i < records.Length; i++)
			{
				CellValueRecordInterface[] array = records[i];
				if (array != null)
				{
					foreach (CellValueRecordInterface cellValueRecordInterface in array)
					{
						if (cellValueRecordInterface is FormulaRecordAggregate)
						{
							FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)cellValueRecordInterface;
							Ptg[] formulaTokens = formulaRecordAggregate.FormulaTokens;
							Ptg[] parsedExpression = ((FormulaRecordAggregate)cellValueRecordInterface).FormulaRecord.ParsedExpression;
							if (shifter.AdjustFormula(formulaTokens, currentExternSheetIndex))
							{
								formulaRecordAggregate.SetParsedExpression(formulaTokens);
							}
						}
					}
				}
			}
		}

		public void VisitCellsForRow(int rowIndex, RecordVisitor rv)
		{
			CellValueRecordInterface[] array = records[rowIndex];
			if (array == null)
			{
				throw new ArgumentException("Row [" + rowIndex + "] is empty");
			}
			for (int i = 0; i < array.Length; i++)
			{
				RecordBase recordBase = (RecordBase)array[i];
				if (recordBase != null)
				{
					int num = CountBlanks(array, i);
					if (num > 1)
					{
						rv.VisitRecord(CreateMBR(array, i, num));
						i += num - 1;
					}
					else if (recordBase is RecordAggregate)
					{
						RecordAggregate recordAggregate = (RecordAggregate)recordBase;
						recordAggregate.VisitContainedRecords(rv);
					}
					else
					{
						rv.VisitRecord((Record)recordBase);
					}
				}
			}
		}

		private static int CountBlanks(CellValueRecordInterface[] rowCellValues, int startIx)
		{
			int i;
			for (i = startIx; i < rowCellValues.Length; i++)
			{
				CellValueRecordInterface cellValueRecordInterface = rowCellValues[i];
				if (!(cellValueRecordInterface is BlankRecord))
				{
					break;
				}
			}
			return i - startIx;
		}

		/// Serializes the cells that are allocated to a certain row range
		public int SerializeCellRow(int row, int offset, byte[] data)
		{
			MyEnumerator myEnumerator = new MyEnumerator(ref records, row, row);
			int num = offset;
			while (myEnumerator.MoveNext())
			{
				CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)myEnumerator.Current;
				if (cellValueRecordInterface.Row != row)
				{
					break;
				}
				num += ((RecordBase)cellValueRecordInterface).Serialize(num, data);
			}
			return num - offset;
		}

		public IEnumerator GetEnumerator()
		{
			return new MyEnumerator(ref records);
		}
	}
}
