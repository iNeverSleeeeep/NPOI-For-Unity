using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Model
{
	/// <summary>
	/// Low level model implementation of a Sheet (one workbook Contains many sheets)
	/// This file Contains the low level binary records starting at the sheets BOF and
	/// ending with the sheets EOF.  Use HSSFSheet for a high level representation.
	///
	/// The structures of the highlevel API use references to this to perform most of their
	/// operations.  Its probably Unwise to use these low level structures directly Unless you
	/// really know what you're doing.  I recommend you Read the Microsoft Excel 97 Developer's
	/// Kit (Microsoft Press) and the documentation at http://sc.openoffice.org/excelfileformat.pdf
	/// before even attempting to use this.
	/// </summary>
	/// <remarks>
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author  Shawn Laubach (slaubach at apache dot org) Gridlines, Headers, Footers, PrintSetup, and Setting Default Column Styles
	/// @author Jason Height (jheight at chariot dot net dot au) Clone support. DBCell and Index Record writing support
	/// @author  Brian Sanders (kestrel at burdell dot org) Active Cell support
	/// @author  Jean-Pierre Paris (jean-pierre.paris at m4x dot org) (Just a little)
	/// </remarks>
	[Serializable]
	public class InternalSheet
	{
		private class RecordCloner : RecordVisitor
		{
			private IList _destList;

			public RecordCloner(IList destList)
			{
				_destList = destList;
			}

			public void VisitRecord(NPOI.HSSF.Record.Record r)
			{
				_destList.Add(r.Clone());
			}
		}

		private class RecordVisitor1 : RecordVisitor
		{
			private List<RecordBase> _records;

			public RecordVisitor1(List<RecordBase> recs)
			{
				_records = recs;
			}

			public void VisitRecord(NPOI.HSSF.Record.Record r)
			{
				_records.Add(r);
			}
		}

		private int preoffset;

		protected int dimsloc = -1;

		[NonSerialized]
		protected DimensionsRecord dims;

		[NonSerialized]
		protected DefaultColWidthRecord defaultcolwidth = new DefaultColWidthRecord();

		[NonSerialized]
		protected DefaultRowHeightRecord defaultrowheight = new DefaultRowHeightRecord();

		[NonSerialized]
		protected GridsetRecord gridset;

		[NonSerialized]
		protected PrintSetupRecord printSetup;

		[NonSerialized]
		protected HeaderRecord header;

		[NonSerialized]
		protected FooterRecord footer;

		[NonSerialized]
		protected PrintGridlinesRecord printGridlines;

		[NonSerialized]
		protected WindowTwoRecord windowTwo;

		[NonSerialized]
		protected MergeCellsRecord merged;

		/// java object always present, but if empty no BIFF records are written 
		[NonSerialized]
		private MergedCellsTable _mergedCellsTable;

		[NonSerialized]
		protected RowRecordsAggregate _rowsAggregate;

		[NonSerialized]
		private PageSettingsBlock _psBlock;

		protected IMargin[] margins;

		[NonSerialized]
		protected SelectionRecord selection;

		[NonSerialized]
		internal ColumnInfoRecordsAggregate _columnInfos;

		/// the DimensionsRecord is always present 
		[NonSerialized]
		private DimensionsRecord _dimensions;

		[NonSerialized]
		private DataValidityTable _dataValidityTable;

		private IEnumerator rowRecEnumerator;

		protected int eofLoc;

		[NonSerialized]
		private GutsRecord _gutsRecord;

		[NonSerialized]
		protected PageBreakRecord rowBreaks;

		[NonSerialized]
		protected PageBreakRecord colBreaks;

		[NonSerialized]
		protected ConditionalFormattingTable condFormatting;

		[NonSerialized]
		protected SheetExtRecord sheetext;

		protected List<RecordBase> records;

		/// Add an UncalcedRecord if not true indicating formulas have not been calculated 
		protected bool _isUncalced;

		[NonSerialized]
		private WorksheetProtectionBlock _protectionBlock = new WorksheetProtectionBlock();

		public WindowTwoRecord WindowTwo => windowTwo;

		/// <summary>
		/// Gets the column infos.
		/// </summary>
		/// <value>The column infos.</value>
		public ColumnInfoRecordsAggregate ColumnInfos => _columnInfos;

		internal MergedCellsTable MergedRecords => _mergedCellsTable;

		/// <summary>
		/// Gets the number of merged regions.
		/// </summary>
		/// <value>The number merged regions.</value>
		public int NumMergedRegions => MergedRecords.NumberOfMergedRegions;

		/// <summary>
		/// Gets the number of conditional formattings.
		/// </summary>
		/// <value>The number of conditional formattings.</value>
		public int NumConditionalFormattings => condFormatting.Count;

		/// <summary>
		/// Gets or Sets the preoffset when using DBCELL records (currently Unused) - this Is
		/// the position of this sheet within the whole file.
		/// </summary>
		/// <value>the offset of the sheet's BOF within the file.</value>
		public int PreOffset
		{
			get
			{
				return preoffset;
			}
			set
			{
				preoffset = value;
			}
		}

		/// <summary>
		/// Get the NEXT RowRecord (from LOC).  The first record that is a Row record
		/// (starting at LOC) will be returned.
		/// This method is "loc" sensitive.  Meaning you need to Set LOC to where you
		/// want it to start searching.  If you don't know do this: SetLoc(GetDimsLoc).
		/// When Adding several rows you can just start at the last one by leaving loc
		/// at what this Sets it to.  For this method, Set loc to dimsloc to start with.
		/// subsequent calls will return rows in (physical) sequence or NULL when you Get to the end.
		/// </summary>
		/// <value>RowRecord representing the next row record or NULL if there are no more</value>
		public RowRecord NextRow
		{
			get
			{
				if (rowRecEnumerator == null)
				{
					rowRecEnumerator = _rowsAggregate.GetEnumerator();
				}
				if (!rowRecEnumerator.MoveNext())
				{
					return null;
				}
				return (RowRecord)rowRecEnumerator.Current;
			}
		}

		/// <summary>
		/// Gets the page settings.
		/// </summary>
		/// <returns></returns>
		public PageSettingsBlock PageSettings
		{
			get
			{
				if (_psBlock == null)
				{
					_psBlock = new PageSettingsBlock();
					RecordOrderer.AddNewSheetRecord(records, _psBlock);
				}
				return _psBlock;
			}
		}

		/// Get the default column width for the sheet (if the columns do not define their own width)
		/// @return default column width
		public int DefaultColumnWidth
		{
			get
			{
				return defaultcolwidth.ColWidth;
			}
			set
			{
				defaultcolwidth.ColWidth = (short)value;
			}
		}

		/// Get the default row height for the sheet (if the rows do not define their own height)
		/// @return  default row height
		public short DefaultRowHeight
		{
			get
			{
				return defaultrowheight.RowHeight;
			}
			set
			{
				defaultrowheight.RowHeight = value;
				defaultrowheight.OptionFlags = 1;
			}
		}

		/// <summary>
		/// Gets or sets the top row.
		/// </summary>
		/// <value>The top row.</value>
		public short TopRow
		{
			get
			{
				if (windowTwo != null)
				{
					return windowTwo.TopRow;
				}
				return 0;
			}
			set
			{
				if (windowTwo != null)
				{
					windowTwo.TopRow = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the left col.
		/// </summary>
		/// <value>The left col.</value>
		public short LeftCol
		{
			get
			{
				if (windowTwo != null)
				{
					return windowTwo.LeftCol;
				}
				return 0;
			}
			set
			{
				if (windowTwo != null)
				{
					windowTwo.LeftCol = value;
				}
			}
		}

		/// <summary>
		/// Returns the active row
		/// </summary>
		/// <value>the active row index</value>
		/// @see org.apache.poi.hssf.record.SelectionRecord
		public int ActiveCellRow
		{
			get
			{
				if (selection == null)
				{
					return 0;
				}
				return selection.ActiveCellRow;
			}
		}

		/// <summary>
		/// Gets the active cell col.
		/// </summary>
		/// <value>the active column index</value>
		/// @see org.apache.poi.hssf.record.SelectionRecord
		public int ActiveCellCol
		{
			get
			{
				if (selection == null)
				{
					return 0;
				}
				return selection.ActiveCellCol;
			}
		}

		public List<RecordBase> Records => records;

		/// <summary>
		/// Gets the gridset record for this sheet.
		/// </summary>
		/// <value>The gridset record.</value>
		public GridsetRecord GridsetRecord => gridset;

		private GutsRecord GutsRecord
		{
			get
			{
				if (_gutsRecord == null)
				{
					GutsRecord gutsRecord = CreateGuts();
					RecordOrderer.AddNewSheetRecord(records, gutsRecord);
					_gutsRecord = gutsRecord;
				}
				return _gutsRecord;
			}
		}

		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>the HeaderRecord.</value>
		public HeaderRecord Header
		{
			get
			{
				return header;
			}
			set
			{
				header = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is auto tab color.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is auto tab color; otherwise, <c>false</c>.
		/// </value>
		public bool IsAutoTabColor
		{
			get
			{
				return sheetext.IsAutoColor;
			}
			set
			{
				sheetext.IsAutoColor = value;
			}
		}

		public short TabColorIndex
		{
			get
			{
				return sheetext.TabColorIndex;
			}
			set
			{
				if ((value <= 8 || value >= 63) && value != 127)
				{
					throw new ArgumentException("invalid color index");
				}
				sheetext.TabColorIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the footer.
		/// </summary>
		/// <value>FooterRecord for the sheet.</value>
		public FooterRecord Footer
		{
			get
			{
				return footer;
			}
			set
			{
				footer = value;
			}
		}

		/// Returns the PrintSetupRecord.
		/// @return PrintSetupRecord for the sheet.
		public PrintSetupRecord PrintSetup
		{
			get
			{
				return printSetup;
			}
			set
			{
				printSetup = value;
			}
		}

		/// @return <c>true</c> if gridlines are printed
		public bool IsGridsPrinted
		{
			get
			{
				if (gridset == null)
				{
					gridset = CreateGridset();
					int index = FindFirstRecordLocBySid(10);
					records.Insert(index, gridset);
				}
				return !gridset.Gridset;
			}
			set
			{
				gridset.Gridset = !value;
			}
		}

		/// Returns the PrintGridlinesRecord.
		/// @return PrintGridlinesRecord for the sheet.
		public PrintGridlinesRecord PrintGridlines
		{
			get
			{
				return printGridlines;
			}
			set
			{
				printGridlines = value;
			}
		}

		/// Returns the information regarding the currently configured pane (split or freeze).
		/// @return null if no pane configured, or the pane information.
		public PaneInformation PaneInformation
		{
			get
			{
				PaneRecord paneRecord = (PaneRecord)FindFirstRecordBySid(65);
				if (paneRecord == null)
				{
					return null;
				}
				return new PaneInformation(paneRecord.X, paneRecord.Y, paneRecord.TopRow, paneRecord.LeftColumn, (byte)paneRecord.ActivePane, windowTwo.FreezePanes);
			}
		}

		public SelectionRecord Selection
		{
			get
			{
				return selection;
			}
			set
			{
				selection = value;
			}
		}

		public WorksheetProtectionBlock ProtectionBlock => _protectionBlock;

		/// Returns if gridlines are Displayed.
		/// @return whether gridlines are Displayed
		public bool DisplayGridlines
		{
			get
			{
				return windowTwo.DisplayGridlines;
			}
			set
			{
				windowTwo.DisplayGridlines = value;
			}
		}

		/// Returns if formulas are Displayed.
		/// @return whether formulas are Displayed
		public bool DisplayFormulas
		{
			get
			{
				return windowTwo.DisplayFormulas;
			}
			set
			{
				windowTwo.DisplayFormulas = value;
			}
		}

		/// Returns if RowColHeadings are Displayed.
		/// @return whether RowColHeadings are Displayed
		public bool DisplayRowColHeadings
		{
			get
			{
				return windowTwo.DisplayRowColHeadings;
			}
			set
			{
				windowTwo.DisplayRowColHeadings = value;
			}
		}

		/// @return whether an Uncalced record must be Inserted or not at generation
		public bool IsUncalced
		{
			get
			{
				return _isUncalced;
			}
			set
			{
				_isUncalced = value;
			}
		}

		public RowRecordsAggregate RowsAggregate => _rowsAggregate;

		public ConditionalFormattingTable ConditionalFormattingTable
		{
			get
			{
				if (condFormatting == null)
				{
					condFormatting = new ConditionalFormattingTable();
					RecordOrderer.AddNewSheetRecord(records, condFormatting);
				}
				return condFormatting;
			}
		}

		/// <summary>
		/// Clones the low level records of this sheet and returns the new sheet instance.
		/// This method is implemented by Adding methods for deep cloning to all records that
		/// can be Added to a sheet. The Record object does not implement Cloneable.
		/// When Adding a new record, implement a public Clone method if and only if the record
		/// belongs to a sheet.
		/// </summary>
		/// <returns></returns>
		public InternalSheet CloneSheet()
		{
			List<RecordBase> list = new List<RecordBase>(records.Count);
			for (int i = 0; i < records.Count; i++)
			{
				RecordBase recordBase = records[i];
				if (recordBase is RecordAggregate)
				{
					((RecordAggregate)recordBase).VisitContainedRecords(new RecordCloner(list));
				}
				else
				{
					if (recordBase is EscherAggregate)
					{
						recordBase = new DrawingRecord();
					}
					NPOI.HSSF.Record.Record item = (NPOI.HSSF.Record.Record)((NPOI.HSSF.Record.Record)recordBase).Clone();
					list.Add(item);
				}
			}
			return CreateSheet(new RecordStream(list, 0));
		}

		/// <summary>
		/// get the NEXT value record (from LOC).  The first record that is a value record
		/// (starting at LOC) will be returned.
		/// This method is "loc" sensitive.  Meaning you need to set LOC to where you
		/// want it to start searching.  If you don't know do this: setLoc(getDimsLoc).
		/// When adding several rows you can just start at the last one by leaving loc
		/// at what this sets it to.  For this method, set loc to dimsloc to start with,
		/// subsequent calls will return values in (physical) sequence or NULL when you get to the end.
		/// </summary>
		/// <returns>the next value record or NULL if there are no more</returns>
		public CellValueRecordInterface[] GetValueRecords()
		{
			return _rowsAggregate.GetValueRecords();
		}

		/// <summary>
		/// Creates the sheet.
		/// </summary>
		/// <param name="rs">The stream.</param>
		/// <returns></returns>
		public static InternalSheet CreateSheet(RecordStream rs)
		{
			return new InternalSheet(rs);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Model.InternalSheet" /> class.
		/// </summary>
		/// <param name="rs">The stream.</param>
		private InternalSheet(RecordStream rs)
		{
			_mergedCellsTable = new MergedCellsTable();
			RowRecordsAggregate rowRecordsAggregate = null;
			records = new List<RecordBase>(128);
			int num = -1;
			if (rs.PeekNextSid() != 2057)
			{
				throw new Exception("BOF record expected");
			}
			BOFRecord bOFRecord = (BOFRecord)rs.GetNext();
			BOFRecordType type = bOFRecord.Type;
			records.Add(bOFRecord);
			while (rs.HasNext())
			{
				int num2 = rs.PeekNextSid();
				switch (num2)
				{
				case 432:
					break;
				case 125:
					goto IL_00c2;
				case 434:
					goto IL_00ec;
				default:
					goto IL_010e;
				}
				condFormatting = new ConditionalFormattingTable(rs);
				records.Add(condFormatting);
				continue;
				IL_00c2:
				_columnInfos = new ColumnInfoRecordsAggregate(rs);
				records.Add(_columnInfos);
				continue;
				IL_010e:
				if (RecordOrderer.IsRowBlockRecord(num2))
				{
					if (rowRecordsAggregate != null)
					{
						throw new InvalidOperationException("row/cell records found in the wrong place");
					}
					RowBlocksReader rowBlocksReader = new RowBlocksReader(rs);
					_mergedCellsTable.AddRecords(rowBlocksReader.LooseMergedCells);
					rowRecordsAggregate = new RowRecordsAggregate(rowBlocksReader.PlainRecordStream, rowBlocksReader.SharedFormulaManager);
					records.Add(rowRecordsAggregate);
				}
				else if (CustomViewSettingsRecordAggregate.IsBeginRecord(num2))
				{
					records.Add(new CustomViewSettingsRecordAggregate(rs));
				}
				else if (PageSettingsBlock.IsComponentRecord(num2))
				{
					if (_psBlock == null)
					{
						_psBlock = new PageSettingsBlock(rs);
						records.Add(_psBlock);
					}
					else
					{
						_psBlock.AddLateRecords(rs);
					}
					_psBlock.PositionRecords(records);
				}
				else if (WorksheetProtectionBlock.IsComponentRecord(num2))
				{
					_protectionBlock.AddRecords(rs);
				}
				else
				{
					switch (num2)
					{
					case 229:
						break;
					case 2057:
						goto IL_020c;
					default:
						goto IL_0226;
					}
					_mergedCellsTable.Read(rs);
				}
				continue;
				IL_00ec:
				_dataValidityTable = new DataValidityTable(rs);
				records.Add(_dataValidityTable);
				continue;
				IL_0226:
				NPOI.HSSF.Record.Record next = rs.GetNext();
				switch (num2)
				{
				case 94:
					_isUncalced = true;
					continue;
				case 2151:
				case 2152:
					records.Add(next);
					continue;
				case 10:
					break;
				case 512:
					if (_columnInfos == null)
					{
						_columnInfos = new ColumnInfoRecordsAggregate();
						records.Add(_columnInfos);
					}
					_dimensions = (DimensionsRecord)next;
					num = records.Count;
					goto default;
				case 85:
					defaultcolwidth = (DefaultColWidthRecord)next;
					goto default;
				case 549:
					defaultrowheight = (DefaultRowHeightRecord)next;
					goto default;
				case 43:
					printGridlines = (PrintGridlinesRecord)next;
					goto default;
				case 130:
					gridset = (GridsetRecord)next;
					goto default;
				case 29:
					selection = (SelectionRecord)next;
					goto default;
				case 574:
					windowTwo = (WindowTwoRecord)next;
					goto default;
				case 2146:
					sheetext = (SheetExtRecord)next;
					goto default;
				case 128:
					_gutsRecord = (GutsRecord)next;
					goto default;
				default:
					records.Add(next);
					continue;
				case 523:
					continue;
				}
				records.Add(next);
				break;
				IL_020c:
				ChartSubstreamRecordAggregate ra = new ChartSubstreamRecordAggregate(rs);
				SpillAggregate(ra, records);
			}
			if (windowTwo == null)
			{
				throw new InvalidOperationException("WINDOW2 was not found");
			}
			if (_dimensions == null)
			{
				if (rowRecordsAggregate == null)
				{
					rowRecordsAggregate = new RowRecordsAggregate();
				}
				num = FindFirstRecordLocBySid(574);
				_dimensions = rowRecordsAggregate.CreateDimensions();
				records.Insert(num, _dimensions);
			}
			if (rowRecordsAggregate == null)
			{
				rowRecordsAggregate = new RowRecordsAggregate();
				records.Insert(num + 1, rowRecordsAggregate);
			}
			_rowsAggregate = rowRecordsAggregate;
			RecordOrderer.AddNewSheetRecord(records, _mergedCellsTable);
			RecordOrderer.AddNewSheetRecord(records, _protectionBlock);
		}

		private static void SpillAggregate(RecordAggregate ra, List<RecordBase> recs)
		{
			ra.VisitContainedRecords(new RecordVisitor1(recs));
		}

		/// <summary>
		/// Creates a sheet with all the usual records minus values and the "index"
		/// record (not required).  Sets the location pointer to where the first value
		/// records should go.  Use this to Create a sheet from "scratch".
		/// </summary>
		/// <returns>Sheet object with all values Set to defaults</returns>
		public static InternalSheet CreateSheet()
		{
			return new InternalSheet();
		}

		private InternalSheet()
		{
			_mergedCellsTable = new MergedCellsTable();
			records = new List<RecordBase>(32);
			records.Add(CreateBOF());
			records.Add(CreateCalcMode());
			records.Add(CreateCalcCount());
			records.Add(CreateRefMode());
			records.Add(CreateIteration());
			records.Add(CreateDelta());
			records.Add(CreateSaveRecalc());
			records.Add(CreatePrintHeaders());
			printGridlines = CreatePrintGridlines();
			records.Add(printGridlines);
			gridset = CreateGridset();
			records.Add(gridset);
			_gutsRecord = CreateGuts();
			records.Add(_gutsRecord);
			defaultrowheight = CreateDefaultRowHeight();
			records.Add(defaultrowheight);
			records.Add(CreateWSBool());
			_psBlock = new PageSettingsBlock();
			records.Add(_psBlock);
			records.Add(_protectionBlock);
			defaultcolwidth = CreateDefaultColWidth();
			records.Add(defaultcolwidth);
			ColumnInfoRecordsAggregate columnInfoRecordsAggregate = new ColumnInfoRecordsAggregate();
			records.Add(columnInfoRecordsAggregate);
			_columnInfos = columnInfoRecordsAggregate;
			_dimensions = CreateDimensions();
			records.Add(_dimensions);
			_rowsAggregate = new RowRecordsAggregate();
			records.Add(_rowsAggregate);
			records.Add(windowTwo = CreateWindowTwo());
			selection = CreateSelection();
			records.Add(selection);
			records.Add(_mergedCellsTable);
			sheetext = new SheetExtRecord();
			records.Add(sheetext);
			records.Add(EOFRecord.instance);
		}

		/// <summary>
		/// Adds the merged region.
		/// </summary>
		/// <param name="rowFrom">the row index From </param>
		/// <param name="colFrom">The column index From.</param>
		/// <param name="rowTo">The row index To</param>
		/// <param name="colTo">The column To.</param>
		/// <returns></returns>
		public int AddMergedRegion(int rowFrom, int colFrom, int rowTo, int colTo)
		{
			if (rowTo < rowFrom)
			{
				throw new ArgumentException("The 'to' row (" + rowTo + ") must not be less than the 'from' row (" + rowFrom + ")");
			}
			if (colTo < colFrom)
			{
				throw new ArgumentException("The 'to' col (" + colTo + ") must not be less than the 'from' col (" + colFrom + ")");
			}
			MergedCellsTable mergedRecords = MergedRecords;
			mergedRecords.AddArea(rowFrom, colFrom, rowTo, colTo);
			return mergedRecords.NumberOfMergedRegions - 1;
		}

		/// <summary>
		/// Removes the merged region.
		/// </summary>
		/// <param name="index">The index.</param>
		public void RemoveMergedRegion(int index)
		{
			MergedCellsTable mergedRecords = MergedRecords;
			if (index < mergedRecords.NumberOfMergedRegions)
			{
				mergedRecords.Remove(index);
			}
		}

		/// <summary>
		/// Gets the merged region at.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public CellRangeAddress GetMergedRegionAt(int index)
		{
			MergedCellsTable mergedRecords = MergedRecords;
			if (index >= mergedRecords.NumberOfMergedRegions)
			{
				return null;
			}
			return mergedRecords.Get(index);
		}

		/// <summary>
		/// Per an earlier reported bug in working with Andy Khan's excel Read library.  This
		/// Sets the values in the sheet's DimensionsRecord object to be correct.  Excel doesn't
		/// really care, but we want to play nice with other libraries.
		/// </summary>
		/// <param name="firstrow">The first row.</param>
		/// <param name="firstcol">The first column.</param>
		/// <param name="lastrow">The last row.</param>
		/// <param name="lastcol">The last column.</param>
		public void SetDimensions(int firstrow, short firstcol, int lastrow, short lastcol)
		{
			dims.FirstCol = firstcol;
			dims.FirstRow = firstrow;
			dims.LastCol = lastcol;
			dims.LastRow = lastrow;
		}

		/// <summary>
		/// Create a row record.  (does not Add it to the records contained in this sheet)
		/// </summary>
		/// <param name="row">row number</param>
		/// <returns>RowRecord Created for the passed in row number</returns>
		public RowRecord CreateRow(int row)
		{
			return RowRecordsAggregate.CreateRow(row);
		}

		/// <summary>
		/// Create a LABELSST Record (does not Add it to the records contained in this sheet)
		/// </summary>
		/// <param name="row">the row the LabelSST Is a member of</param>
		/// <param name="col">the column the LabelSST defines</param>
		/// <param name="index">the index of the string within the SST (use workbook AddSSTString method)</param>
		/// <returns>LabelSSTRecord newly Created containing your SST Index, row,col.</returns>
		public LabelSSTRecord CreateLabelSST(int row, short col, int index)
		{
			LabelSSTRecord labelSSTRecord = new LabelSSTRecord();
			labelSSTRecord.Row = row;
			labelSSTRecord.Column = col;
			labelSSTRecord.SSTIndex = index;
			labelSSTRecord.XFIndex = 15;
			return labelSSTRecord;
		}

		/// <summary>
		/// Create a NUMBER Record (does not Add it to the records contained in this sheet)
		/// </summary>
		/// <param name="row">the row the NumberRecord is a member of</param>
		/// <param name="col">the column the NumberRecord defines</param>
		/// <param name="value">value for the number record</param>
		/// <returns>NumberRecord for that row, col containing that value as Added to the sheet</returns>
		public NumberRecord CreateNumber(int row, short col, double value)
		{
			NumberRecord numberRecord = new NumberRecord();
			numberRecord.Row = row;
			numberRecord.Column = col;
			numberRecord.Value = value;
			numberRecord.XFIndex = 15;
			return numberRecord;
		}

		/// <summary>
		/// Create a BLANK record (does not Add it to the records contained in this sheet)
		/// </summary>
		/// <param name="row">the row the BlankRecord is a member of</param>
		/// <param name="col">the column the BlankRecord is a member of</param>
		/// <returns></returns>
		public BlankRecord CreateBlank(int row, short col)
		{
			BlankRecord blankRecord = new BlankRecord();
			blankRecord.Row = row;
			blankRecord.Column = col;
			blankRecord.XFIndex = 15;
			return blankRecord;
		}

		/// <summary>
		/// Adds a value record to the sheet's contained binary records
		/// (i.e. LabelSSTRecord or NumberRecord).
		/// This method is "loc" sensitive.  Meaning you need to Set LOC to where you
		/// want it to start searching.  If you don't know do this: SetLoc(GetDimsLoc).
		/// When Adding several rows you can just start at the last one by leaving loc
		/// at what this Sets it to.
		/// </summary>
		/// <param name="row">the row to Add the cell value to</param>
		/// <param name="col">the cell value record itself.</param>
		public void AddValueRecord(int row, CellValueRecordInterface col)
		{
			DimensionsRecord dimensions = _dimensions;
			if (col.Column >= dimensions.LastCol)
			{
				dimensions.LastCol = (short)(col.Column + 1);
			}
			if (col.Column < dimensions.FirstCol)
			{
				dimensions.FirstCol = col.Column;
			}
			_rowsAggregate.InsertCell(col);
		}

		/// <summary>
		/// Remove a value record from the records array.
		/// This method is not loc sensitive, it Resets loc to = dimsloc so no worries.
		/// </summary>
		/// <param name="row">the row of the value record you wish to Remove</param>
		/// <param name="col">a record supporting the CellValueRecordInterface.</param>
		public void RemoveValueRecord(int row, CellValueRecordInterface col)
		{
			_rowsAggregate.RemoveCell(col);
		}

		/// <summary>
		/// Replace a value record from the records array.
		/// This method is not loc sensitive, it Resets loc to = dimsloc so no worries.
		/// </summary>
		/// <param name="newval">a record supporting the CellValueRecordInterface.  this will Replace
		/// the cell value with the same row and column.  If there Isn't one, one will
		/// be Added.</param>
		public void ReplaceValueRecord(CellValueRecordInterface newval)
		{
			_rowsAggregate.RemoveCell(newval);
			_rowsAggregate.InsertCell(newval);
		}

		/// <summary>
		/// Adds a row record to the sheet
		/// This method is "loc" sensitive.  Meaning you need to Set LOC to where you
		/// want it to start searching.  If you don't know do this: SetLoc(GetDimsLoc).
		/// When Adding several rows you can just start at the last one by leaving loc
		/// at what this Sets it to.
		/// </summary>
		/// <param name="row">the row record to be Added</param>
		public void AddRow(RowRecord row)
		{
			DimensionsRecord dimensions = _dimensions;
			if (row.RowNumber >= dimensions.LastRow)
			{
				dimensions.LastRow = row.RowNumber + 1;
			}
			if (row.RowNumber < dimensions.FirstRow)
			{
				dimensions.FirstRow = row.RowNumber;
			}
			RowRecord row2 = _rowsAggregate.GetRow(row.RowNumber);
			if (row2 != null)
			{
				_rowsAggregate.RemoveRow(row2);
			}
			_rowsAggregate.InsertRow(row);
		}

		/// <summary>
		/// Removes a row record
		/// This method is not loc sensitive, it Resets loc to = dimsloc so no worries.
		/// </summary>
		/// <param name="row">the row record to Remove</param>
		public void RemoveRow(RowRecord row)
		{
			_rowsAggregate.RemoveRow(row);
		}

		/// <summary>
		/// Get the NEXT (from LOC) RowRecord where rownumber matches the given rownum.
		/// The first record that is a Row record (starting at LOC) that has the
		/// same rownum as the given rownum will be returned.
		/// This method is "loc" sensitive.  Meaning you need to Set LOC to where you
		/// want it to start searching.  If you don't know do this: SetLoc(GetDimsLoc).
		/// When Adding several rows you can just start at the last one by leaving loc
		/// at what this Sets it to.  For this method, Set loc to dimsloc to start with.
		/// subsequent calls will return rows in (physical) sequence or NULL when you Get to the end.
		/// </summary>
		/// <param name="rownum">which row to return (careful with LOC)</param>
		/// <returns>RowRecord representing the next row record or NULL if there are no more</returns>
		public RowRecord GetRow(int rownum)
		{
			return _rowsAggregate.GetRow(rownum);
		}

		/// <summary>
		/// Creates the BOF record
		/// </summary>
		/// <returns>record containing a BOFRecord</returns>
		public static NPOI.HSSF.Record.Record CreateBOF()
		{
			BOFRecord bOFRecord = new BOFRecord();
			bOFRecord.Version = 1536;
			bOFRecord.Type = BOFRecordType.Worksheet;
			bOFRecord.Build = 3515;
			bOFRecord.BuildYear = 1996;
			bOFRecord.HistoryBitMask = 193;
			bOFRecord.RequiredVersion = 6;
			return bOFRecord;
		}

		/// <summary>
		/// Creates the Index record  - not currently used
		/// </summary>
		/// <returns>record containing a IndexRecord</returns>
		protected NPOI.HSSF.Record.Record CreateIndex()
		{
			IndexRecord indexRecord = new IndexRecord();
			indexRecord.FirstRow = 0;
			indexRecord.LastRowAdd1 = 0;
			return indexRecord;
		}

		/// <summary>
		/// Creates the CalcMode record and Sets it to 1 (automatic formula caculation)
		/// </summary>
		/// <returns>record containing a CalcModeRecord</returns>
		protected NPOI.HSSF.Record.Record CreateCalcMode()
		{
			CalcModeRecord calcModeRecord = new CalcModeRecord();
			calcModeRecord.SetCalcMode(1);
			return calcModeRecord;
		}

		/// <summary>
		/// Creates the CalcCount record and Sets it to 0x64 (default number of iterations)
		/// </summary>
		/// <returns>record containing a CalcCountRecord</returns>
		protected NPOI.HSSF.Record.Record CreateCalcCount()
		{
			CalcCountRecord calcCountRecord = new CalcCountRecord();
			calcCountRecord.Iterations = 100;
			return calcCountRecord;
		}

		/// <summary>
		/// Creates the RefMode record and Sets it to A1 Mode (default reference mode)
		/// </summary>
		/// <returns>record containing a RefModeRecord</returns>
		protected NPOI.HSSF.Record.Record CreateRefMode()
		{
			RefModeRecord refModeRecord = new RefModeRecord();
			refModeRecord.Mode = 1;
			return refModeRecord;
		}

		/// <summary>
		/// Creates the Iteration record and Sets it to false (don't iteratively calculate formulas)
		/// </summary>
		/// <returns>record containing a IterationRecord</returns>
		protected NPOI.HSSF.Record.Record CreateIteration()
		{
			return new IterationRecord(iterateOn: false);
		}

		/// <summary>
		/// Creates the Delta record and Sets it to 0.0010 (default accuracy)
		/// </summary>
		/// <returns>record containing a DeltaRecord</returns>
		protected NPOI.HSSF.Record.Record CreateDelta()
		{
			return new DeltaRecord(0.001);
		}

		/// <summary>
		/// Creates the SaveRecalc record and Sets it to true (recalculate before saving)
		/// </summary>
		/// <returns>record containing a SaveRecalcRecord</returns>
		protected NPOI.HSSF.Record.Record CreateSaveRecalc()
		{
			SaveRecalcRecord saveRecalcRecord = new SaveRecalcRecord();
			saveRecalcRecord.Recalc = true;
			return saveRecalcRecord;
		}

		/// <summary>
		/// Creates the PrintHeaders record and Sets it to false (we don't Create headers yet so why print them)
		/// </summary>
		/// <returns>record containing a PrintHeadersRecord</returns>
		protected NPOI.HSSF.Record.Record CreatePrintHeaders()
		{
			PrintHeadersRecord printHeadersRecord = new PrintHeadersRecord();
			printHeadersRecord.PrintHeaders = false;
			return printHeadersRecord;
		}

		/// <summary>
		/// Creates the PrintGridlines record and Sets it to false (that makes for ugly sheets).  As far as I can
		/// tell this does the same thing as the GridsetRecord
		/// </summary>
		/// <returns>record containing a PrintGridlinesRecord</returns>
		protected PrintGridlinesRecord CreatePrintGridlines()
		{
			PrintGridlinesRecord printGridlinesRecord = new PrintGridlinesRecord();
			printGridlinesRecord.PrintGridlines = false;
			return printGridlinesRecord;
		}

		/// <summary>
		/// Creates the GridSet record and Sets it to true (user has mucked with the gridlines)
		/// </summary>
		/// <returns>record containing a GridsetRecord</returns>
		protected GridsetRecord CreateGridset()
		{
			GridsetRecord gridsetRecord = new GridsetRecord();
			gridsetRecord.Gridset = true;
			return gridsetRecord;
		}

		/// <summary>
		/// Creates the Guts record and Sets leftrow/topcol guttter and rowlevelmax/collevelmax to 0
		/// </summary>
		/// <returns>record containing a GutsRecordRecord</returns>
		protected GutsRecord CreateGuts()
		{
			GutsRecord gutsRecord = new GutsRecord();
			gutsRecord.LeftRowGutter = 0;
			gutsRecord.TopColGutter = 0;
			gutsRecord.RowLevelMax = 0;
			gutsRecord.ColLevelMax = 0;
			return gutsRecord;
		}

		/// <summary>
		/// Creates the DefaultRowHeight Record and Sets its options to 0 and rowheight to 0xff
		/// </summary>
		/// <see cref="T:NPOI.HSSF.Record.DefaultRowHeightRecord" />
		/// <see cref="T:NPOI.HSSF.Record.Record" />
		/// <returns>record containing a DefaultRowHeightRecord</returns>
		protected DefaultRowHeightRecord CreateDefaultRowHeight()
		{
			DefaultRowHeightRecord defaultRowHeightRecord = new DefaultRowHeightRecord();
			defaultRowHeightRecord.OptionFlags = 0;
			defaultRowHeightRecord.RowHeight = 255;
			return defaultRowHeightRecord;
		}

		/// Creates the WSBoolRecord and Sets its values to defaults
		/// @see org.apache.poi.hssf.record.WSBoolRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a WSBoolRecord
		protected NPOI.HSSF.Record.Record CreateWSBool()
		{
			WSBoolRecord wSBoolRecord = new WSBoolRecord();
			wSBoolRecord.WSBool1 = 4;
			wSBoolRecord.WSBool2 = 1;
			return wSBoolRecord;
		}

		/// Creates the HCenter Record and Sets it to false (don't horizontally center)
		/// @see org.apache.poi.hssf.record.HCenterRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a HCenterRecord
		protected NPOI.HSSF.Record.Record CreateHCenter()
		{
			HCenterRecord hCenterRecord = new HCenterRecord();
			hCenterRecord.HCenter = false;
			return hCenterRecord;
		}

		/// Creates the VCenter Record and Sets it to false (don't horizontally center)
		/// @see org.apache.poi.hssf.record.VCenterRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a VCenterRecord
		protected NPOI.HSSF.Record.Record CreateVCenter()
		{
			VCenterRecord vCenterRecord = new VCenterRecord();
			vCenterRecord.VCenter = false;
			return vCenterRecord;
		}

		/// Creates the PrintSetup Record and Sets it to defaults and marks it invalid
		/// @see org.apache.poi.hssf.record.PrintSetupRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a PrintSetupRecord
		protected NPOI.HSSF.Record.Record CreatePrintSetup()
		{
			PrintSetupRecord printSetupRecord = new PrintSetupRecord();
			printSetupRecord.PaperSize = 1;
			printSetupRecord.Scale = 100;
			printSetupRecord.PageStart = 1;
			printSetupRecord.FitWidth = 1;
			printSetupRecord.FitHeight = 1;
			printSetupRecord.Options = 2;
			printSetupRecord.HResolution = 300;
			printSetupRecord.VResolution = 300;
			printSetupRecord.HeaderMargin = 0.5;
			printSetupRecord.FooterMargin = 0.5;
			printSetupRecord.Copies = 0;
			return printSetupRecord;
		}

		/// Creates the DefaultColWidth Record and Sets it to 8
		/// @see org.apache.poi.hssf.record.DefaultColWidthRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a DefaultColWidthRecord
		protected DefaultColWidthRecord CreateDefaultColWidth()
		{
			DefaultColWidthRecord defaultColWidthRecord = new DefaultColWidthRecord();
			defaultColWidthRecord.ColWidth = 8;
			return defaultColWidthRecord;
		}

		/// Get the width of a given column in Units of 1/256th of a Char width
		/// @param column index
		/// @see org.apache.poi.hssf.record.DefaultColWidthRecord
		/// @see org.apache.poi.hssf.record.ColumnInfoRecord
		/// @see #SetColumnWidth(short,short)
		/// @return column width in Units of 1/256th of a Char width
		public int GetColumnWidth(int columnIndex)
		{
			return _columnInfos.FindColumnInfo(columnIndex)?.ColumnWidth ?? (256 * defaultcolwidth.ColWidth);
		}

		/// Get the index to the ExtendedFormatRecord "associated" with
		/// the column at specified 0-based index. (In this case, an
		/// ExtendedFormatRecord index is actually associated with a
		/// ColumnInfoRecord which spans 1 or more columns)
		/// <br />
		/// Returns the index to the default ExtendedFormatRecord (0xF)
		/// if no ColumnInfoRecord exists that includes the column
		/// index specified.
		/// @param column
		/// @return index of ExtendedFormatRecord associated with
		/// ColumnInfoRecord that includes the column index or the
		/// index of the default ExtendedFormatRecord (0xF)
		public short GetXFIndexForColAt(short columnIndex)
		{
			ColumnInfoRecord columnInfoRecord = _columnInfos.FindColumnInfo(columnIndex);
			if (columnInfoRecord != null)
			{
				return (short)columnInfoRecord.XFIndex;
			}
			return 15;
		}

		/// Set the width for a given column in 1/256th of a Char width Units
		/// @param column - the column number
		/// @param width (in Units of 1/256th of a Char width)
		public void SetColumnWidth(int column, int width)
		{
			if (width > 65280)
			{
				throw new ArgumentException("The maximum column width for an individual cell is 255 characters.");
			}
			SetColumn(column, null, width, null, null, null);
		}

		/// Get the hidden property for a given column.
		/// @param column index
		/// @see org.apache.poi.hssf.record.DefaultColWidthRecord
		/// @see org.apache.poi.hssf.record.ColumnInfoRecord
		/// @see #SetColumnHidden(short,bool)
		/// @return whether the column is hidden or not.
		public bool IsColumnHidden(int columnIndex)
		{
			return _columnInfos.FindColumnInfo(columnIndex)?.IsHidden ?? false;
		}

		/// Get the hidden property for a given column.
		/// @param column - the column number
		/// @param hidden - whether the column is hidden or not
		public void SetColumnHidden(int column, bool hidden)
		{
			SetColumn(column, null, null, null, hidden, null);
		}

		public void SetDefaultColumnStyle(int column, int styleIndex)
		{
			SetColumn(column, (short)styleIndex, null, null, null, null);
		}

		public void SetColumn(int column, int width, int level, bool hidden, bool collapsed)
		{
			_columnInfos.SetColumn(column, 0, width, level, hidden, collapsed);
		}

		public void SetColumn(int column, short? xfStyle, int? width, int? level, bool? hidden, bool? collapsed)
		{
			_columnInfos.SetColumn(column, xfStyle, width, level, hidden, collapsed);
		}

		private GutsRecord GetGutsRecord()
		{
			if (_gutsRecord == null)
			{
				GutsRecord gutsRecord = CreateGuts();
				RecordOrderer.AddNewSheetRecord(records, gutsRecord);
				_gutsRecord = gutsRecord;
			}
			return _gutsRecord;
		}

		/// Creates an outline Group for the specified columns.
		/// @param fromColumn    Group from this column (inclusive)
		/// @param toColumn      Group to this column (inclusive)
		/// @param indent        if true the Group will be indented by one level,
		///                      if false indenting will be Removed by one level.
		public void GroupColumnRange(int fromColumn, int toColumn, bool indent)
		{
			_columnInfos.GroupColumnRange(fromColumn, toColumn, indent);
			int maxOutlineLevel = _columnInfos.MaxOutlineLevel;
			GutsRecord gutsRecord = GetGutsRecord();
			gutsRecord.ColLevelMax = (short)(maxOutlineLevel + 1);
			if (maxOutlineLevel == 0)
			{
				gutsRecord.TopColGutter = 0;
			}
			else
			{
				gutsRecord.TopColGutter = (short)(29 + 12 * (maxOutlineLevel - 1));
			}
		}

		/// Creates the Dimensions Record and Sets it to bogus values (you should Set this yourself
		/// or let the high level API do it for you)
		/// @see org.apache.poi.hssf.record.DimensionsRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a DimensionsRecord
		private static DimensionsRecord CreateDimensions()
		{
			DimensionsRecord dimensionsRecord = new DimensionsRecord();
			dimensionsRecord.FirstCol = 0;
			dimensionsRecord.LastRow = 1;
			dimensionsRecord.FirstRow = 0;
			dimensionsRecord.LastCol = 1;
			return dimensionsRecord;
		}

		/// Creates the WindowTwo Record and Sets it to:  
		/// options        = 0x6b6 
		/// toprow         = 0 
		/// leftcol        = 0 
		/// headercolor    = 0x40 
		/// pagebreakzoom  = 0x0 
		/// normalzoom     = 0x0 
		/// @see org.apache.poi.hssf.record.WindowTwoRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a WindowTwoRecord
		private static WindowTwoRecord CreateWindowTwo()
		{
			WindowTwoRecord windowTwoRecord = new WindowTwoRecord();
			windowTwoRecord.Options = 1718;
			windowTwoRecord.TopRow = 0;
			windowTwoRecord.LeftCol = 0;
			windowTwoRecord.HeaderColor = 64;
			windowTwoRecord.PageBreakZoom = 0;
			windowTwoRecord.NormalZoom = 0;
			return windowTwoRecord;
		}

		/// <summary>
		/// Creates the Selection record and Sets it to nothing selected
		/// </summary>
		/// <returns>record containing a SelectionRecord</returns>
		private static SelectionRecord CreateSelection()
		{
			return new SelectionRecord(0, 0);
		}

		/// <summary>
		/// Sets the active cell.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		public void SetActiveCell(int row, int column)
		{
			SetActiveCellRange(row, row, column, column);
		}

		/// <summary>
		/// Sets the active cell range.
		/// </summary>
		/// <param name="firstRow">The firstrow.</param>
		/// <param name="lastRow">The lastrow.</param>
		/// <param name="firstColumn">The firstcolumn.</param>
		/// <param name="lastColumn">The lastcolumn.</param>
		public void SetActiveCellRange(int firstRow, int lastRow, int firstColumn, int lastColumn)
		{
			List<CellRangeAddress8Bit> list = new List<CellRangeAddress8Bit>();
			list.Add(new CellRangeAddress8Bit(firstRow, lastRow, firstColumn, lastColumn));
			SetActiveCellRange(list, 0, firstRow, firstColumn);
		}

		/// <summary>
		/// Sets the active cell range.
		/// </summary>
		/// <param name="cellranges">The cellranges.</param>
		/// <param name="activeRange">The index of the active range.</param>
		/// <param name="activeRow">The active row in the active range</param>
		/// <param name="activeColumn">The active column in the active range</param>
		public void SetActiveCellRange(List<CellRangeAddress8Bit> cellranges, int activeRange, int activeRow, int activeColumn)
		{
			selection.ActiveCellCol = activeColumn;
			selection.ActiveCellRow = activeRow;
			selection.ActiveCellRef = activeRange;
			selection.CellReferences = cellranges.ToArray();
		}

		/// <summary>
		/// Creates the EOF record
		/// </summary>
		/// <returns>record containing a EOFRecord</returns>
		protected NPOI.HSSF.Record.Record CreateEOF()
		{
			return new EOFRecord();
		}

		/// <summary>
		/// Returns the first occurance of a record matching a particular sid.
		/// </summary>
		/// <param name="sid">The sid.</param>
		/// <returns></returns>
		public NPOI.HSSF.Record.Record FindFirstRecordBySid(short sid)
		{
			int num = FindFirstRecordLocBySid(sid);
			if (num < 0)
			{
				return null;
			}
			return (NPOI.HSSF.Record.Record)records[num];
		}

		/// <summary>
		/// Sets the SCL record or Creates it in the correct place if it does not
		/// already exist.
		/// </summary>
		/// <param name="sclRecord">The record to set.</param>
		public void SetSCLRecord(SCLRecord sclRecord)
		{
			int num = FindFirstRecordLocBySid(160);
			if (num == -1)
			{
				int num2 = FindFirstRecordLocBySid(574);
				records.Insert(num2 + 1, sclRecord);
			}
			else
			{
				records[num] = sclRecord;
			}
		}

		/// Finds the first occurance of a record matching a particular sid and
		/// returns it's position.
		/// @param sid   the sid to search for
		/// @return  the record position of the matching record or -1 if no match
		///          is made.
		public int FindFirstRecordLocBySid(short sid)
		{
			int count = records.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = records[i];
				if (obj is NPOI.HSSF.Record.Record)
				{
					NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)obj;
					if (record.Sid == sid)
					{
						return i;
					}
				}
			}
			return -1;
		}

		public WindowTwoRecord GetWindowTwo()
		{
			return windowTwo;
		}

		/// Sets whether the sheet is selected
		/// @param sel True to select the sheet, false otherwise.
		public void SetSelected(bool sel)
		{
			windowTwo.IsSelected = sel;
		}

		/// Creates a split (freezepane). Any existing freezepane or split pane Is overwritten.
		/// @param colSplit      Horizonatal position of split.
		/// @param rowSplit      Vertical position of split.
		/// @param topRow        Top row visible in bottom pane
		/// @param leftmostColumn   Left column visible in right pane.
		public void CreateFreezePane(int colSplit, int rowSplit, int topRow, int leftmostColumn)
		{
			int num = FindFirstRecordLocBySid(65);
			if (num != -1)
			{
				records.RemoveAt(num);
			}
			if (colSplit == 0 && rowSplit == 0)
			{
				windowTwo.FreezePanes = false;
				windowTwo.FreezePanesNoSplit = false;
				SelectionRecord selectionRecord = (SelectionRecord)FindFirstRecordBySid(29);
				selectionRecord.Pane = 3;
			}
			else
			{
				int num2 = FindFirstRecordLocBySid(574);
				PaneRecord paneRecord = new PaneRecord();
				paneRecord.X = (short)colSplit;
				paneRecord.Y = (short)rowSplit;
				paneRecord.TopRow = (short)topRow;
				paneRecord.LeftColumn = (short)leftmostColumn;
				if (rowSplit == 0)
				{
					paneRecord.TopRow = 0;
					paneRecord.ActivePane = 1;
				}
				else if (colSplit == 0)
				{
					paneRecord.LeftColumn = 0;
					paneRecord.ActivePane = 2;
				}
				else
				{
					paneRecord.ActivePane = 0;
				}
				records.Insert(num2 + 1, paneRecord);
				windowTwo.FreezePanes = true;
				windowTwo.FreezePanesNoSplit = true;
				SelectionRecord selectionRecord2 = (SelectionRecord)FindFirstRecordBySid(29);
				selectionRecord2.Pane = (byte)paneRecord.ActivePane;
			}
		}

		/// Creates a split pane. Any existing freezepane or split pane is overwritten.
		/// @param xSplitPos      Horizonatal position of split (in 1/20th of a point).
		/// @param ySplitPos      Vertical position of split (in 1/20th of a point).
		/// @param topRow        Top row visible in bottom pane
		/// @param leftmostColumn   Left column visible in right pane.
		/// @param activePane    Active pane.  One of: PANE_LOWER_RIGHT,
		///                      PANE_UPPER_RIGHT, PANE_LOWER_LEFT, PANE_UPPER_LEFT
		/// @see #PANE_LOWER_LEFT
		/// @see #PANE_LOWER_RIGHT
		/// @see #PANE_UPPER_LEFT
		/// @see #PANE_UPPER_RIGHT
		public void CreateSplitPane(int xSplitPos, int ySplitPos, int topRow, int leftmostColumn, PanePosition activePane)
		{
			int num = FindFirstRecordLocBySid(65);
			if (num != -1)
			{
				records.RemoveAt(num);
			}
			int num2 = FindFirstRecordLocBySid(574);
			PaneRecord paneRecord = new PaneRecord();
			paneRecord.X = (short)xSplitPos;
			paneRecord.Y = (short)ySplitPos;
			paneRecord.TopRow = (short)topRow;
			paneRecord.LeftColumn = (short)leftmostColumn;
			paneRecord.ActivePane = (short)activePane;
			records.Insert(num2 + 1, paneRecord);
			windowTwo.FreezePanes = false;
			windowTwo.FreezePanesNoSplit = false;
			SelectionRecord selectionRecord = (SelectionRecord)FindFirstRecordBySid(29);
			selectionRecord.Pane = 0;
		}

		/// creates a Password record with password set to 00.
		protected static PasswordRecord CreatePassword()
		{
			return new PasswordRecord(0);
		}

		/// creates a Protect record with protect set to false.
		protected ProtectRecord CreateProtect()
		{
			return new ProtectRecord(isProtected: false);
		}

		/// Creates an ObjectProtect record with protect Set to false.
		/// @see org.apache.poi.hssf.record.ObjectProtectRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return an ObjectProtectRecord
		protected ObjectProtectRecord CreateObjectProtect()
		{
			ObjectProtectRecord objectProtectRecord = new ObjectProtectRecord();
			objectProtectRecord.Protect = false;
			return objectProtectRecord;
		}

		/// Creates a ScenarioProtect record with protect Set to false.
		/// @see org.apache.poi.hssf.record.ScenarioProtectRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return a ScenarioProtectRecord
		protected ScenarioProtectRecord CreateScenarioProtect()
		{
			ScenarioProtectRecord scenarioProtectRecord = new ScenarioProtectRecord();
			scenarioProtectRecord.Protect = false;
			return scenarioProtectRecord;
		}

		/// <summary>
		/// Finds the DrawingRecord for our sheet, and  attaches it to the DrawingManager (which knows about
		///  the overall DrawingGroup for our workbook).
		/// If requested, will Create a new DrawRecord if none currently exist
		/// </summary>
		/// <param name="drawingManager">The DrawingManager2 for our workbook</param>
		/// <param name="CreateIfMissing">Should one be Created if missing?</param>
		/// <returns>location of EscherAggregate record. if no EscherAggregate record is found return -1</returns>
		public int AggregateDrawingRecords(DrawingManager2 drawingManager, bool CreateIfMissing)
		{
			int num = FindFirstRecordLocBySid(236);
			if (num == -1)
			{
				if (!CreateIfMissing)
				{
					return -1;
				}
				EscherAggregate item = new EscherAggregate(createDefaultTree: true);
				num = FindFirstRecordLocBySid(9876);
				if (num == -1)
				{
					num = FindFirstRecordLocBySid(574);
				}
				else
				{
					Records.RemoveAt(num);
				}
				Records.Insert(num, item);
				return num;
			}
			EscherAggregate.CreateAggregate(records, num);
			return num;
		}

		/// Perform any work necessary before the sheet is about to be Serialized.
		/// For instance the escher aggregates size needs to be calculated before
		/// serialization so that the dgg record (which occurs first) can be written.
		public void Preserialize()
		{
			IEnumerator enumerator = Records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				RecordBase recordBase = (RecordBase)enumerator.Current;
				if (recordBase is EscherAggregate)
				{
					int recordSize = recordBase.RecordSize;
				}
			}
		}

		/// Shifts all the page breaks in the range "count" number of rows/columns
		/// @param breaks The page record to be Shifted
		/// @param start Starting "main" value to Shift breaks
		/// @param stop Ending "main" value to Shift breaks
		/// @param count number of Units (rows/columns) to Shift by
		public void ShiftBreaks(PageBreakRecord breaks, short start, short stop, int count)
		{
			if (rowBreaks != null)
			{
				IEnumerator breaksEnumerator = breaks.GetBreaksEnumerator();
				IList list = new ArrayList();
				while (breaksEnumerator.MoveNext())
				{
					PageBreakRecord.Break @break = (PageBreakRecord.Break)breaksEnumerator.Current;
					int main = @break.main;
					bool flag = main >= start;
					bool flag2 = main <= stop;
					if (flag && flag2)
					{
						list.Add(@break);
					}
				}
				breaksEnumerator = list.GetEnumerator();
				while (breaksEnumerator.MoveNext())
				{
					PageBreakRecord.Break break2 = (PageBreakRecord.Break)breaksEnumerator.Current;
					breaks.RemoveBreak(break2.main);
					breaks.AddBreak(break2.main + count, break2.subFrom, break2.subTo);
				}
			}
		}

		/// Shifts the horizontal page breaks for the indicated count
		/// @param startingRow
		/// @param endingRow
		/// @param count
		public void ShiftRowBreaks(int startingRow, int endingRow, int count)
		{
			ShiftBreaks(rowBreaks, (short)startingRow, (short)endingRow, (short)count);
		}

		/// Shifts the vertical page breaks for the indicated count
		/// @param startingCol
		/// @param endingCol
		/// @param count
		public void ShiftColumnBreaks(short startingCol, short endingCol, short count)
		{
			ShiftBreaks(colBreaks, startingCol, endingCol, count);
		}

		public void SetColumnGroupCollapsed(int columnNumber, bool collapsed)
		{
			if (collapsed)
			{
				_columnInfos.CollapseColumn(columnNumber);
			}
			else
			{
				_columnInfos.ExpandColumn(columnNumber);
			}
		}

		/// Updates formulas in cells and conditional formats due to moving of cells
		/// @param externSheetIndex the externSheet index of this sheet
		public void UpdateFormulasAfterCellShift(FormulaShifter shifter, int externSheetIndex)
		{
			RowsAggregate.UpdateFormulasAfterRowShift(shifter, externSheetIndex);
			if (condFormatting != null)
			{
				ConditionalFormattingTable.UpdateFormulasAfterCellShift(shifter, externSheetIndex);
			}
		}

		public void VisitContainedRecords(RecordVisitor rv, int offset)
		{
			PositionTrackingVisitor positionTrackingVisitor = new PositionTrackingVisitor(rv, offset);
			bool flag = false;
			int num = offset;
			for (int i = 0; i < records.Count; i++)
			{
				RecordBase recordBase = records[i];
				if (recordBase is RecordAggregate)
				{
					RecordAggregate recordAggregate = (RecordAggregate)recordBase;
					recordAggregate.VisitContainedRecords(positionTrackingVisitor);
					num += recordAggregate.RecordSize;
				}
				else
				{
					if (recordBase is DefaultColWidthRecord)
					{
						((DefaultColWidthRecord)recordBase).offsetForFilePointer = num;
					}
					positionTrackingVisitor.VisitRecord((NPOI.HSSF.Record.Record)recordBase);
					num += recordBase.RecordSize;
				}
				if (recordBase is BOFRecord && !flag)
				{
					flag = true;
					if (_isUncalced)
					{
						UncalcedRecord uncalcedRecord = new UncalcedRecord();
						positionTrackingVisitor.VisitRecord(uncalcedRecord);
						num += uncalcedRecord.RecordSize;
					}
					if (_rowsAggregate != null)
					{
						int sizeOfInitialSheetRecords = GetSizeOfInitialSheetRecords(i);
						int position = positionTrackingVisitor.Position;
						IndexRecord indexRecord = _rowsAggregate.CreateIndexRecord(position, sizeOfInitialSheetRecords, 0);
						positionTrackingVisitor.VisitRecord(indexRecord);
						num += indexRecord.RecordSize;
					}
				}
			}
		}

		/// 'initial sheet records' are between INDEX and the 'Row Blocks'
		/// @param bofRecordIndex index of record after which INDEX record is to be placed
		/// @return count of bytes from end of INDEX record to first ROW record.
		private int GetSizeOfInitialSheetRecords(int bofRecordIndex)
		{
			int num = 0;
			for (int i = bofRecordIndex + 1; i < records.Count; i++)
			{
				RecordBase recordBase = records[i];
				if (recordBase is RowRecordsAggregate)
				{
					break;
				}
				num += recordBase.RecordSize;
			}
			if (_isUncalced)
			{
				num += UncalcedRecord.StaticRecordSize;
			}
			return num;
		}

		public void GroupRowRange(int fromRow, int toRow, bool indent)
		{
			for (int i = fromRow; i <= toRow; i++)
			{
				RowRecord rowRecord = GetRow(i);
				if (rowRecord == null)
				{
					rowRecord = CreateRow(i);
					AddRow(rowRecord);
				}
				int outlineLevel = rowRecord.OutlineLevel;
				outlineLevel = ((!indent) ? (outlineLevel - 1) : (outlineLevel + 1));
				outlineLevel = Math.Max(0, outlineLevel);
				outlineLevel = Math.Min(7, outlineLevel);
				rowRecord.OutlineLevel = (short)outlineLevel;
			}
			RecalcRowGutter();
		}

		private void RecalcRowGutter()
		{
			int num = 0;
			IEnumerator enumerator = _rowsAggregate.GetEnumerator();
			while (enumerator.MoveNext())
			{
				RowRecord rowRecord = (RowRecord)enumerator.Current;
				num = Math.Max(rowRecord.OutlineLevel, num);
			}
			GutsRecord gutsRecord = GetGutsRecord();
			if (gutsRecord == null)
			{
				gutsRecord = new GutsRecord();
				records.Add(gutsRecord);
			}
			gutsRecord.RowLevelMax = (short)(num + 1);
			gutsRecord.LeftRowGutter = (short)(29 + 12 * num);
		}

		public DataValidityTable GetOrCreateDataValidityTable()
		{
			if (_dataValidityTable == null)
			{
				_dataValidityTable = new DataValidityTable();
				RecordOrderer.AddNewSheetRecord(records, _dataValidityTable);
			}
			return _dataValidityTable;
		}

		/// Get the {@link NoteRecord}s (related to cell comments) for this sheet
		/// @return never <code>null</code>, typically empty array
		public NoteRecord[] GetNoteRecords()
		{
			List<NoteRecord> list = new List<NoteRecord>();
			for (int num = records.Count - 1; num >= 0; num--)
			{
				RecordBase recordBase = records[num];
				if (recordBase is NoteRecord)
				{
					list.Add((NoteRecord)recordBase);
				}
			}
			if (list.Count < 1)
			{
				return NoteRecord.EMPTY_ARRAY;
			}
			NoteRecord[] array = new NoteRecord[list.Count];
			return list.ToArray();
		}
	}
}
