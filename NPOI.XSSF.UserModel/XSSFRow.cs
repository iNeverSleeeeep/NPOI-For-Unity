using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of a row of a spreadsheet.
	public class XSSFRow : IRow, IComparable<XSSFRow>
	{
		private static POILogger _logger = POILogFactory.GetLogger(typeof(XSSFRow));

		/// the xml bean Containing all cell defInitions for this row
		private CT_Row _row;

		/// Cells of this row keyed by their column indexes.
		/// The TreeMap ensures that the cells are ordered by columnIndex in the ascending order.
		private SortedDictionary<int, ICell> _cells;

		/// the parent sheet
		private XSSFSheet _sheet;

		/// Returns the XSSFSheet this row belongs to
		///
		/// @return the XSSFSheet that owns this row
		public ISheet Sheet
		{
			get
			{
				return _sheet;
			}
		}

		/// Get the number of the first cell Contained in this row.
		///
		/// @return short representing the first logical cell in the row,
		///  or -1 if the row does not contain any cells.
		public short FirstCellNum
		{
			get
			{
				return (short)((_cells.Count == 0) ? (-1) : GetFirstKey(_cells.Keys));
			}
		}

		/// Gets the index of the last cell Contained in this row <b>PLUS ONE</b>. The result also
		/// happens to be the 1-based column number of the last cell.  This value can be used as a
		/// standard upper bound when iterating over cells:
		/// <pre>
		/// short minColIx = row.GetFirstCellNum();
		/// short maxColIx = row.GetLastCellNum();
		/// for(short colIx=minColIx; colIx&lt;maxColIx; colIx++) {
		///   XSSFCell cell = row.GetCell(colIx);
		///   if(cell == null) {
		///     continue;
		///   }
		///   //... do something with cell
		/// }
		/// </pre>
		///
		/// @return short representing the last logical cell in the row <b>PLUS ONE</b>,
		///   or -1 if the row does not contain any cells.
		public short LastCellNum
		{
			get
			{
				return (short)((_cells.Count == 0) ? (-1) : (GetLastKey(_cells.Keys) + 1));
			}
		}

		/// Get the row's height measured in twips (1/20th of a point). If the height is not Set, the default worksheet value is returned,
		/// See {@link NPOI.XSSF.usermodel.XSSFSheet#GetDefaultRowHeightInPoints()}
		///
		/// @return row height measured in twips (1/20th of a point)
		public short Height
		{
			get
			{
				return (short)(HeightInPoints * 20f);
			}
			set
			{
				if (value < 0)
				{
					if (_row.IsSetHt())
					{
						_row.unSetHt();
					}
					if (_row.IsSetCustomHeight())
					{
						_row.unSetCustomHeight();
					}
				}
				else
				{
					_row.ht = (double)value / 20.0;
					_row.customHeight = true;
				}
			}
		}

		/// Returns row height measured in point size. If the height is not Set, the default worksheet value is returned,
		/// See {@link NPOI.XSSF.usermodel.XSSFSheet#GetDefaultRowHeightInPoints()}
		///
		/// @return row height measured in point size
		/// @see NPOI.XSSF.usermodel.XSSFSheet#GetDefaultRowHeightInPoints()
		public float HeightInPoints
		{
			get
			{
				if (_row.IsSetHt())
				{
					return (float)_row.ht;
				}
				return _sheet.DefaultRowHeightInPoints;
			}
			set
			{
				Height = (short)((value == -1f) ? (-1f) : (value * 20f));
			}
		}

		/// Gets the number of defined cells (NOT number of cells in the actual row!).
		/// That is to say if only columns 0,4,5 have values then there would be 3.
		///
		/// @return int representing the number of defined cells in the row.
		public int PhysicalNumberOfCells
		{
			get
			{
				return _cells.Count;
			}
		}

		/// Get row number this row represents
		///
		/// @return the row number (0 based)
		public int RowNum
		{
			get
			{
				return (int)(_row.r - 1);
			}
			set
			{
				int lastRowIndex = SpreadsheetVersion.EXCEL2007.LastRowIndex;
				if (value < 0 || value > lastRowIndex)
				{
					throw new ArgumentException("Invalid row number (" + value + ") outside allowable range (0.." + lastRowIndex + ")");
				}
				_row.r = (uint)(value + 1);
			}
		}

		/// Get whether or not to display this row with 0 height
		///
		/// @return - height is zero or not.
		public bool ZeroHeight
		{
			get
			{
				return _row.hidden;
			}
			set
			{
				_row.hidden = value;
			}
		}

		/// Is this row formatted? Most aren't, but some rows
		///  do have whole-row styles. For those that do, you
		///  can get the formatting from {@link #GetRowStyle()}
		public bool IsFormatted
		{
			get
			{
				return _row.IsSetS();
			}
		}

		/// Returns the whole-row cell style. Most rows won't
		///  have one of these, so will return null. Call
		///  {@link #isFormatted()} to check first.
		public ICellStyle RowStyle
		{
			get
			{
				if (!IsFormatted)
				{
					return null;
				}
				StylesTable stylesSource = ((XSSFWorkbook)Sheet.Workbook).GetStylesSource();
				if (stylesSource.NumCellStyles > 0)
				{
					return stylesSource.GetStyleAt((int)_row.s);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (_row.IsSetS())
					{
						_row.UnsetS();
						_row.UnsetCustomFormat();
					}
				}
				else
				{
					StylesTable stylesSource = ((XSSFWorkbook)Sheet.Workbook).GetStylesSource();
					XSSFCellStyle xSSFCellStyle = (XSSFCellStyle)value;
					xSSFCellStyle.VerifyBelongsToStylesSource(stylesSource);
					long num = stylesSource.PutStyle(xSSFCellStyle);
					_row.s = (uint)num;
					_row.customFormat = true;
				}
			}
		}

		public List<ICell> Cells
		{
			get
			{
				List<ICell> list = new List<ICell>();
				foreach (ICell value in _cells.Values)
				{
					list.Add(value);
				}
				return list;
			}
		}

		/// Construct a XSSFRow.
		///
		/// @param row the xml bean Containing all cell defInitions for this row.
		/// @param sheet the parent sheet.
		public XSSFRow(CT_Row row, XSSFSheet sheet)
		{
			_row = row;
			_sheet = sheet;
			_cells = new SortedDictionary<int, ICell>();
			if (0 < row.SizeOfCArray())
			{
				foreach (CT_Cell item in row.c)
				{
					XSSFCell xSSFCell = new XSSFCell(this, item);
					_cells.Add(xSSFCell.ColumnIndex, xSSFCell);
					sheet.OnReadCell(xSSFCell);
				}
			}
		}

		public SortedDictionary<int, ICell>.ValueCollection.Enumerator CellIterator()
		{
			return _cells.Values.GetEnumerator();
		}

		/// Alias for {@link #cellIterator()} to allow  foreach loops:
		/// <blockquote><pre>
		/// for(Cell cell : row){
		///     ...
		/// }
		/// </pre></blockquote>
		///
		/// @return an iterator over cells in this row.
		public IEnumerator GetEnumerator()
		{
			return CellIterator();
		}

		/// Compares two <code>XSSFRow</code> objects.  Two rows are equal if they belong to the same worksheet and
		/// their row indexes are Equal.
		///
		/// @param   row   the <code>XSSFRow</code> to be Compared.
		/// @return	the value <code>0</code> if the row number of this <code>XSSFRow</code> is
		/// 		equal to the row number of the argument <code>XSSFRow</code>; a value less than
		/// 		<code>0</code> if the row number of this this <code>XSSFRow</code> is numerically less
		/// 		than the row number of the argument <code>XSSFRow</code>; and a value greater
		/// 		than <code>0</code> if the row number of this this <code>XSSFRow</code> is numerically
		/// 		 greater than the row number of the argument <code>XSSFRow</code>.
		/// @throws ArgumentException if the argument row belongs to a different worksheet
		public int CompareTo(XSSFRow row)
		{
			int rowNum = RowNum;
			if (row.Sheet != Sheet)
			{
				throw new ArgumentException("The Compared rows must belong to the same XSSFSheet");
			}
			int rowNum2 = row.RowNum;
			if (rowNum >= rowNum2)
			{
				if (rowNum != rowNum2)
				{
					return 1;
				}
				return 0;
			}
			return -1;
		}

		public ICell CreateCell(int columnIndex)
		{
			return CreateCell(columnIndex, CellType.Blank);
		}

		public ICell CreateCell(int columnIndex, CellType type)
		{
			XSSFCell xSSFCell = _cells.ContainsKey(columnIndex) ? ((XSSFCell)_cells[columnIndex]) : null;
			CT_Cell cT_Cell;
			if (xSSFCell != null)
			{
				cT_Cell = xSSFCell.GetCTCell();
				cT_Cell.Set(new CT_Cell());
			}
			else
			{
				cT_Cell = _row.AddNewC();
			}
			XSSFCell xSSFCell2 = new XSSFCell(this, cT_Cell);
			xSSFCell2.SetCellNum(columnIndex);
			if (type != CellType.Blank)
			{
				xSSFCell2.SetCellType(type);
			}
			_cells[columnIndex] = xSSFCell2;
			return xSSFCell2;
		}

		/// Returns the cell at the given (0 based) index,
		///  with the {@link NPOI.SS.usermodel.Row.MissingCellPolicy} from the parent Workbook.
		///
		/// @return the cell at the given (0 based) index
		public ICell GetCell(int cellnum)
		{
			return GetCell(cellnum, _sheet.Workbook.MissingCellPolicy);
		}

		/// <summary>
		/// Get the hssfcell representing a given column (logical cell)
		/// 0-based. If you ask for a cell that is not defined, then
		/// you Get a null.
		/// This is the basic call, with no policies applied
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <returns>Cell representing that column or null if Undefined.</returns>
		private ICell RetrieveCell(int cellnum)
		{
			if (!_cells.ContainsKey(cellnum))
			{
				return null;
			}
			return _cells[cellnum];
		}

		public ICell GetCell(int cellnum, MissingCellPolicy policy)
		{
			if (cellnum < 0)
			{
				throw new ArgumentException("Cell index must be >= 0");
			}
			XSSFCell xSSFCell = (XSSFCell)RetrieveCell(cellnum);
			if (policy == MissingCellPolicy.RETURN_NULL_AND_BLANK)
			{
				return xSSFCell;
			}
			if (policy == MissingCellPolicy.RETURN_BLANK_AS_NULL)
			{
				if (xSSFCell == null)
				{
					return xSSFCell;
				}
				if (xSSFCell.CellType == CellType.Blank)
				{
					return null;
				}
				return xSSFCell;
			}
			if (policy == MissingCellPolicy.CREATE_NULL_AS_BLANK)
			{
				if (xSSFCell == null)
				{
					return CreateCell(cellnum, CellType.Blank);
				}
				return xSSFCell;
			}
			throw new ArgumentException("Illegal policy " + policy + " (" + policy.id + ")");
		}

		private int GetFirstKey(SortedDictionary<int, ICell>.KeyCollection keys)
		{
			int num = 0;
			foreach (int key in keys)
			{
				if (num == 0)
				{
					return key;
				}
			}
			throw new ArgumentOutOfRangeException();
		}

		private int GetLastKey(SortedDictionary<int, ICell>.KeyCollection keys)
		{
			int num = 0;
			foreach (int key in keys)
			{
				if (num == keys.Count - 1)
				{
					return key;
				}
				num++;
			}
			throw new ArgumentOutOfRangeException();
		}

		/// Applies a whole-row cell styling to the row.
		/// If the value is null then the style information is Removed,
		///  causing the cell to used the default workbook style.
		public void SetRowStyle(ICellStyle style)
		{
		}

		/// Remove the Cell from this row.
		///
		/// @param cell the cell to remove
		public void RemoveCell(ICell cell)
		{
			if (cell.Row != this)
			{
				throw new ArgumentException("Specified cell does not belong to this row");
			}
			XSSFCell xSSFCell = (XSSFCell)cell;
			if (xSSFCell.IsPartOfArrayFormulaGroup)
			{
				xSSFCell.NotifyArrayFormulaChanging();
			}
			if (cell.CellType == CellType.Formula)
			{
				((XSSFWorkbook)_sheet.Workbook).OnDeleteFormula(xSSFCell);
			}
			_cells.Remove(cell.ColumnIndex);
		}

		/// Returns the underlying CT_Row xml bean Containing all cell defInitions in this row
		///
		/// @return the underlying CT_Row xml bean
		public CT_Row GetCTRow()
		{
			return _row;
		}

		/// Fired when the document is written to an output stream.
		///
		/// @see NPOI.XSSF.usermodel.XSSFSheet#Write(java.io.OutputStream) ()
		internal void OnDocumentWrite()
		{
			bool flag = true;
			if (_row.SizeOfCArray() != _cells.Count)
			{
				flag = false;
			}
			else
			{
				int num = 0;
				foreach (XSSFCell value in _cells.Values)
				{
					CT_Cell cTCell = value.GetCTCell();
					CT_Cell cArray = _row.GetCArray(num++);
					string r = cTCell.r;
					string r2 = cArray.r;
					if (!((r == null) ? (r2 == null) : r.Equals(r2)))
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				CT_Cell[] array = new CT_Cell[_cells.Count];
				int num3 = 0;
				foreach (XSSFCell value2 in _cells.Values)
				{
					array[num3++] = value2.GetCTCell();
				}
				_row.SetCArray(array);
			}
		}

		/// @return formatted xml representation of this row
		public override string ToString()
		{
			return _row.ToString();
		}

		/// update cell references when Shifting rows
		///
		/// @param n the number of rows to move
		internal void Shift(int n)
		{
			int num = RowNum + n;
			CalculationChain calculationChain = ((XSSFWorkbook)_sheet.Workbook).GetCalculationChain();
			int sheetId = (int)_sheet.sheet.sheetId;
			string msg = "Row[rownum=" + RowNum + "] contains cell(s) included in a multi-cell array formula. You cannot change part of an array.";
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ICell cell = (ICell)enumerator.Current;
					XSSFCell xSSFCell = (XSSFCell)cell;
					if (xSSFCell.IsPartOfArrayFormulaGroup)
					{
						xSSFCell.NotifyArrayFormulaChanging(msg);
					}
					if (calculationChain != null)
					{
						calculationChain.RemoveItem(sheetId, xSSFCell.GetReference());
					}
					CT_Cell cTCell = xSSFCell.GetCTCell();
					string text2 = cTCell.r = new CellReference(num, xSSFCell.ColumnIndex).FormatAsString();
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			RowNum = num;
		}

		public void MoveCell(ICell cell, int newColumn)
		{
			throw new NotImplementedException();
		}

		public IRow CopyRowTo(int targetIndex)
		{
			return Sheet.CopyRow(RowNum, targetIndex);
		}

		public ICell CopyCell(int sourceIndex, int targetIndex)
		{
			return CellUtil.CopyCell(this, sourceIndex, targetIndex);
		}
	}
}
