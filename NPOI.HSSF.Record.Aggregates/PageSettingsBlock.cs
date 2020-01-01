using NPOI.HSSF.Model;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates
{
	/// Groups the page settings records for a worksheet.<p />
	///
	/// See OOO excelfileformat.pdf sec 4.4 'Page Settings Block'
	///
	/// @author Josh Micich
	public class PageSettingsBlock : RecordAggregate
	{
		private class CustomRecordVisitor1 : RecordVisitor
		{
			private CustomViewSettingsRecordAggregate _cv;

			private HeaderFooterRecord _hf;

			private List<HeaderFooterRecord> _sviewHeaderFooters;

			private Dictionary<string, HeaderFooterRecord> _hfGuidMap;

			public CustomRecordVisitor1(CustomViewSettingsRecordAggregate cv, HeaderFooterRecord hf, List<HeaderFooterRecord> sviewHeaderFooter, Dictionary<string, HeaderFooterRecord> hfGuidMap)
			{
				_cv = cv;
				_hf = hf;
				_sviewHeaderFooters = sviewHeaderFooter;
				_hfGuidMap = hfGuidMap;
			}

			public void VisitRecord(Record r)
			{
				if (r.Sid == 426)
				{
					string key = HexDump.ToHex(((UserSViewBegin)r).Guid);
					HeaderFooterRecord headerFooterRecord = _hfGuidMap[key];
					if (headerFooterRecord != null)
					{
						_cv.Append(_hf);
						_sviewHeaderFooters.Remove(_hf);
					}
				}
			}
		}

		private PageBreakRecord _rowBreaksRecord;

		private PageBreakRecord _columnBreaksRecord;

		private HeaderRecord header;

		private FooterRecord footer;

		private HCenterRecord _hCenter;

		private VCenterRecord _vCenter;

		private LeftMarginRecord _leftMargin;

		private RightMarginRecord _rightMargin;

		private TopMarginRecord _topMargin;

		private BottomMarginRecord _bottomMargin;

		private PrintSetupRecord printSetup;

		private Record _bitmap;

		private HeaderFooterRecord _headerFooter;

		private List<HeaderFooterRecord> _sviewHeaderFooters = new List<HeaderFooterRecord>();

		private List<PLSAggregate> _plsRecords;

		private Record _printSize;

		private PageBreakRecord RowBreaksRecord
		{
			get
			{
				if (_rowBreaksRecord == null)
				{
					_rowBreaksRecord = new HorizontalPageBreakRecord();
				}
				return _rowBreaksRecord;
			}
		}

		private PageBreakRecord ColumnBreaksRecord
		{
			get
			{
				if (_columnBreaksRecord == null)
				{
					_columnBreaksRecord = new VerticalPageBreakRecord();
				}
				return _columnBreaksRecord;
			}
		}

		/// Returns the HeaderRecord.
		/// @return HeaderRecord for the sheet.
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

		/// Returns the FooterRecord.
		/// @return FooterRecord for the sheet.
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

		/// @return all the horizontal page breaks, never <c>null</c>
		public int[] RowBreaks => RowBreaksRecord.GetBreaks();

		/// @return the number of row page breaks
		public int NumRowBreaks => RowBreaksRecord.NumBreaks;

		/// @return all the column page breaks, never <c>null</c>
		public int[] ColumnBreaks => ColumnBreaksRecord.GetBreaks();

		/// @return the number of column page breaks
		public int NumColumnBreaks => ColumnBreaksRecord.NumBreaks;

		public VCenterRecord VCenter => _vCenter;

		public HCenterRecord HCenter => _hCenter;

		public PageSettingsBlock(RecordStream rs)
		{
			_plsRecords = new List<PLSAggregate>();
			while (ReadARecord(rs))
			{
			}
		}

		/// Creates a PageSettingsBlock with default settings
		public PageSettingsBlock()
		{
			_plsRecords = new List<PLSAggregate>();
			_rowBreaksRecord = new HorizontalPageBreakRecord();
			_columnBreaksRecord = new VerticalPageBreakRecord();
			header = new HeaderRecord(string.Empty);
			footer = new FooterRecord(string.Empty);
			_hCenter = CreateHCenter();
			_vCenter = CreateVCenter();
			printSetup = CreatePrintSetup();
		}

		/// @return <c>true</c> if the specified Record sid is one belonging to the 
		/// 'Page Settings Block'.
		public static bool IsComponentRecord(int sid)
		{
			switch (sid)
			{
			case 20:
			case 21:
			case 26:
			case 27:
			case 38:
			case 39:
			case 40:
			case 41:
			case 51:
			case 77:
			case 131:
			case 132:
			case 161:
			case 233:
			case 2204:
				return true;
			default:
				return false;
			}
		}

		private bool ReadARecord(RecordStream rs)
		{
			switch (rs.PeekNextSid())
			{
			case 27:
				CheckNotPresent(_rowBreaksRecord);
				_rowBreaksRecord = (PageBreakRecord)rs.GetNext();
				break;
			case 26:
				CheckNotPresent(_columnBreaksRecord);
				_columnBreaksRecord = (PageBreakRecord)rs.GetNext();
				break;
			case 20:
				CheckNotPresent(header);
				header = (HeaderRecord)rs.GetNext();
				break;
			case 21:
				CheckNotPresent(footer);
				footer = (FooterRecord)rs.GetNext();
				break;
			case 131:
				CheckNotPresent(_hCenter);
				_hCenter = (HCenterRecord)rs.GetNext();
				break;
			case 132:
				CheckNotPresent(_vCenter);
				_vCenter = (VCenterRecord)rs.GetNext();
				break;
			case 38:
				CheckNotPresent(_leftMargin);
				_leftMargin = (LeftMarginRecord)rs.GetNext();
				break;
			case 39:
				CheckNotPresent(_rightMargin);
				_rightMargin = (RightMarginRecord)rs.GetNext();
				break;
			case 40:
				CheckNotPresent(_topMargin);
				_topMargin = (TopMarginRecord)rs.GetNext();
				break;
			case 41:
				CheckNotPresent(_bottomMargin);
				_bottomMargin = (BottomMarginRecord)rs.GetNext();
				break;
			case 77:
				_plsRecords.Add(new PLSAggregate(rs));
				break;
			case 161:
				CheckNotPresent(printSetup);
				printSetup = (PrintSetupRecord)rs.GetNext();
				break;
			case 233:
				CheckNotPresent(_bitmap);
				_bitmap = rs.GetNext();
				break;
			case 51:
				CheckNotPresent(_printSize);
				_printSize = rs.GetNext();
				break;
			case 2204:
			{
				HeaderFooterRecord headerFooterRecord = (HeaderFooterRecord)rs.GetNext();
				if (headerFooterRecord.IsCurrentSheet)
				{
					_headerFooter = headerFooterRecord;
				}
				else
				{
					_sviewHeaderFooters.Add(headerFooterRecord);
				}
				break;
			}
			default:
				return false;
			}
			return true;
		}

		private void CheckNotPresent(Record rec)
		{
			if (rec != null)
			{
				throw new RecordFormatException("Duplicate PageSettingsBlock record (sid=0x" + StringUtil.ToHexString(rec.Sid) + ")");
			}
		}

		public IEnumerator GetEnumerator()
		{
			return _plsRecords.GetEnumerator();
		}

		/// Sets a page break at the indicated column
		public void SetColumnBreak(int column, int fromRow, int toRow)
		{
			ColumnBreaksRecord.AddBreak(column, fromRow, toRow);
		}

		/// Removes a page break at the indicated column
		public void RemoveColumnBreak(int column)
		{
			ColumnBreaksRecord.RemoveBreak(column);
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			VisitIfPresent(_rowBreaksRecord, rv);
			VisitIfPresent(_columnBreaksRecord, rv);
			if (header == null)
			{
				rv.VisitRecord(new HeaderRecord(""));
			}
			else
			{
				rv.VisitRecord(header);
			}
			if (footer == null)
			{
				rv.VisitRecord(new FooterRecord(""));
			}
			else
			{
				rv.VisitRecord(footer);
			}
			VisitIfPresent(_hCenter, rv);
			VisitIfPresent(_vCenter, rv);
			VisitIfPresent(_leftMargin, rv);
			VisitIfPresent(_rightMargin, rv);
			VisitIfPresent(_topMargin, rv);
			VisitIfPresent(_bottomMargin, rv);
			foreach (PLSAggregate plsRecord in _plsRecords)
			{
				plsRecord.VisitContainedRecords(rv);
			}
			VisitIfPresent(printSetup, rv);
			VisitIfPresent(_printSize, rv);
			VisitIfPresent(_headerFooter, rv);
			VisitIfPresent(_bitmap, rv);
		}

		private static void VisitIfPresent(Record r, RecordVisitor rv)
		{
			if (r != null)
			{
				rv.VisitRecord(r);
			}
		}

		private static void VisitIfPresent(PageBreakRecord r, RecordVisitor rv)
		{
			if (r != null && !r.IsEmpty)
			{
				rv.VisitRecord(r);
			}
		}

		/// Creates the HCenter Record and sets it to false (don't horizontally center)
		private static HCenterRecord CreateHCenter()
		{
			HCenterRecord hCenterRecord = new HCenterRecord();
			hCenterRecord.HCenter = false;
			return hCenterRecord;
		}

		/// Creates the VCenter Record and sets it to false (don't horizontally center)
		private static VCenterRecord CreateVCenter()
		{
			VCenterRecord vCenterRecord = new VCenterRecord();
			vCenterRecord.VCenter = false;
			return vCenterRecord;
		}

		/// Creates the PrintSetup Record and sets it to defaults and marks it invalid
		/// @see org.apache.poi.hssf.record.PrintSetupRecord
		/// @see org.apache.poi.hssf.record.Record
		/// @return record containing a PrintSetupRecord
		private static PrintSetupRecord CreatePrintSetup()
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
			printSetupRecord.Copies = 1;
			return printSetupRecord;
		}

		private IMargin GetMarginRec(MarginType margin)
		{
			switch (margin)
			{
			case MarginType.LeftMargin:
				return _leftMargin;
			case MarginType.RightMargin:
				return _rightMargin;
			case MarginType.TopMargin:
				return _topMargin;
			case MarginType.BottomMargin:
				return _bottomMargin;
			default:
				throw new InvalidOperationException("Unknown margin constant:  " + (short)margin);
			}
		}

		/// Gets the size of the margin in inches.
		/// @param margin which margin to Get
		/// @return the size of the margin
		public double GetMargin(MarginType margin)
		{
			IMargin marginRec = GetMarginRec(margin);
			if (marginRec == null)
			{
				switch (margin)
				{
				case MarginType.LeftMargin:
					return 0.75;
				case MarginType.RightMargin:
					return 0.75;
				case MarginType.TopMargin:
					return 1.0;
				case MarginType.BottomMargin:
					return 1.0;
				default:
					throw new InvalidOperationException("Unknown margin constant:  " + margin);
				}
			}
			return marginRec.Margin;
		}

		/// Sets the size of the margin in inches.
		/// @param margin which margin to Get
		/// @param size the size of the margin
		public void SetMargin(MarginType margin, double size)
		{
			IMargin margin2 = GetMarginRec(margin);
			if (margin2 == null)
			{
				switch (margin)
				{
				case MarginType.LeftMargin:
					_leftMargin = new LeftMarginRecord();
					margin2 = _leftMargin;
					break;
				case MarginType.RightMargin:
					_rightMargin = new RightMarginRecord();
					margin2 = _rightMargin;
					break;
				case MarginType.TopMargin:
					_topMargin = new TopMarginRecord();
					margin2 = _topMargin;
					break;
				case MarginType.BottomMargin:
					_bottomMargin = new BottomMarginRecord();
					margin2 = _bottomMargin;
					break;
				default:
					throw new InvalidOperationException("Unknown margin constant:  " + margin);
				}
			}
			margin2.Margin = size;
		}

		/// Shifts all the page breaks in the range "count" number of rows/columns
		/// @param breaks The page record to be shifted
		/// @param start Starting "main" value to shift breaks
		/// @param stop Ending "main" value to shift breaks
		/// @param count number of units (rows/columns) to shift by
		private static void ShiftBreaks(PageBreakRecord breaks, int start, int stop, int count)
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
				breaks.AddBreak((short)(break2.main + count), break2.subFrom, break2.subTo);
			}
		}

		/// Sets a page break at the indicated row
		/// @param row
		public void SetRowBreak(int row, short fromCol, short toCol)
		{
			RowBreaksRecord.AddBreak((short)row, fromCol, toCol);
		}

		/// Removes a page break at the indicated row
		/// @param row
		public void RemoveRowBreak(int row)
		{
			if (RowBreaksRecord.GetBreaks().Length < 1)
			{
				throw new ArgumentException("Sheet does not define any row breaks");
			}
			RowBreaksRecord.RemoveBreak((short)row);
		}

		/// Queries if the specified row has a page break
		/// @param row
		/// @return true if the specified row has a page break
		public bool IsRowBroken(int row)
		{
			return RowBreaksRecord.GetBreak(row) != null;
		}

		/// Queries if the specified column has a page break
		///
		/// @return <c>true</c> if the specified column has a page break
		public bool IsColumnBroken(int column)
		{
			return ColumnBreaksRecord.GetBreak(column) != null;
		}

		/// Shifts the horizontal page breaks for the indicated count
		/// @param startingRow
		/// @param endingRow
		/// @param count
		public void ShiftRowBreaks(int startingRow, int endingRow, int count)
		{
			ShiftBreaks(RowBreaksRecord, startingRow, endingRow, count);
		}

		/// Shifts the vertical page breaks for the indicated count
		/// @param startingCol
		/// @param endingCol
		/// @param count
		public void ShiftColumnBreaks(short startingCol, short endingCol, short count)
		{
			ShiftBreaks(ColumnBreaksRecord, startingCol, endingCol, count);
		}

		/// <summary>
		///  HEADERFOOTER is new in 2007.  Some apps seem to have scattered this record long after
		/// the PageSettingsBlock where it belongs.
		/// </summary>
		/// <param name="rec"></param>
		public void AddLateHeaderFooter(HeaderFooterRecord rec)
		{
			if (_headerFooter != null)
			{
				throw new ArgumentNullException("This page settings block already has a header/footer record");
			}
			if (rec.Sid != 2204)
			{
				throw new RecordFormatException("Unexpected header-footer record sid: 0x" + StringUtil.ToHexString(rec.Sid));
			}
			_headerFooter = rec;
		}

		/// <summary>
		/// This method reads PageSettingsBlock records from the supplied RecordStream until the first non-PageSettingsBlock record is encountered.
		/// As each record is read, it is incorporated into this PageSettingsBlock.
		/// </summary>
		/// <param name="rs"></param> 
		public void AddLateRecords(RecordStream rs)
		{
			while (ReadARecord(rs))
			{
			}
		}

		public void PositionRecords(List<RecordBase> sheetRecords)
		{
			List<HeaderFooterRecord> list = new List<HeaderFooterRecord>(_sviewHeaderFooters);
			Dictionary<string, HeaderFooterRecord> dictionary = new Dictionary<string, HeaderFooterRecord>();
			foreach (HeaderFooterRecord item in list)
			{
				string key = HexDump.ToHex(item.Guid);
				if (dictionary.ContainsKey(key))
				{
					dictionary[key] = item;
				}
				else
				{
					dictionary.Add(HexDump.ToHex(item.Guid), item);
				}
			}
			foreach (HeaderFooterRecord item2 in list)
			{
				foreach (RecordBase sheetRecord in sheetRecords)
				{
					if (sheetRecord is CustomViewSettingsRecordAggregate)
					{
						CustomViewSettingsRecordAggregate customViewSettingsRecordAggregate = (CustomViewSettingsRecordAggregate)sheetRecord;
						customViewSettingsRecordAggregate.VisitContainedRecords(new CustomRecordVisitor1(customViewSettingsRecordAggregate, item2, _sviewHeaderFooters, dictionary));
					}
				}
			}
		}
	}
}
