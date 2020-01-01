using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;

namespace NPOI.XWPF.UserModel
{
	/// @author gisellabronzetti
	/// @author gregg morris - added removeCell(), setCantSplitRow(), setRepeatHeader()
	public class XWPFTableRow
	{
		private CT_Row ctRow;

		private XWPFTable table;

		private List<XWPFTableCell> tableCells;

		/// This element specifies the height of the current table row within the
		/// current table. This height shall be used to determine the resulting
		/// height of the table row, which may be absolute or relative (depending on
		/// its attribute values). If omitted, then the table row shall automatically
		/// resize its height to the height required by its contents (the equivalent
		/// of an hRule value of auto).
		///
		/// @return height
		public int Height
		{
			get
			{
				CT_TrPr trPr = GetTrPr();
				if (trPr.SizeOfTrHeightArray() != 0)
				{
					return (int)trPr.GetTrHeightArray(0).val;
				}
				return 0;
			}
			set
			{
				CT_TrPr trPr = GetTrPr();
				CT_Height cT_Height = (trPr.SizeOfTrHeightArray() == 0) ? trPr.AddNewTrHeight() : trPr.GetTrHeightArray(0);
				cT_Height.val = (ulong)value;
			}
		}

		/// Return true if the "can't split row" value is true. The logic for this
		/// attribute is a little unusual: a TRUE value means DON'T allow rows to
		/// split, FALSE means allow rows to split.
		/// @return true if rows can't be split, false otherwise.
		public bool IsCantSplitRow
		{
			get
			{
				bool result = false;
				CT_TrPr trPr = GetTrPr();
				if (trPr.SizeOfCantSplitArray() > 0)
				{
					CT_OnOff cT_OnOff = trPr.GetCantSplitList()[0];
					result = cT_OnOff.val;
				}
				return result;
			}
			set
			{
				CT_TrPr trPr = GetTrPr();
				CT_OnOff cT_OnOff = trPr.AddNewCantSplit();
				cT_OnOff.val = value;
			}
		}

		/// Return true if a table's header row should be repeated at the top of a
		/// table split across pages.
		/// @return true if table's header row should be repeated at the top of each
		///         page of table, false otherwise.
		public bool IsRepeatHeader
		{
			get
			{
				bool result = false;
				CT_TrPr trPr = GetTrPr();
				if (trPr.SizeOfTblHeaderArray() > 0)
				{
					CT_OnOff cT_OnOff = trPr.GetTblHeaderList()[0];
					result = cT_OnOff.val;
				}
				return result;
			}
			set
			{
				CT_TrPr trPr = GetTrPr();
				CT_OnOff cT_OnOff = trPr.AddNewTblHeader();
				cT_OnOff.val = value;
			}
		}

		public XWPFTableRow(CT_Row row, XWPFTable table)
		{
			this.table = table;
			ctRow = row;
			GetTableCells();
		}

		public CT_Row GetCTRow()
		{
			return ctRow;
		}

		/// create a new XWPFTableCell and add it to the tableCell-list of this tableRow
		/// @return the newly Created XWPFTableCell
		public XWPFTableCell CreateCell()
		{
			XWPFTableCell xWPFTableCell = new XWPFTableCell(ctRow.AddNewTc(), this, table.Body);
			tableCells.Add(xWPFTableCell);
			return xWPFTableCell;
		}

		public void MergeCells(int startIndex, int endIndex)
		{
			if (startIndex >= endIndex)
			{
				throw new ArgumentOutOfRangeException("Start index must be smaller than end index");
			}
			if (startIndex < 0 || endIndex >= tableCells.Count)
			{
				throw new ArgumentOutOfRangeException("Invalid start index and end index");
			}
			XWPFTableCell cell = GetCell(startIndex);
			for (int num = endIndex; num > startIndex; num--)
			{
				RemoveCell(num);
			}
			if (!cell.GetCTTc().IsSetTcPr())
			{
				cell.GetCTTc().AddNewTcPr();
			}
			CT_TcPr tcPr = cell.GetCTTc().tcPr;
			if (tcPr.gridSpan == null)
			{
				tcPr.AddNewGridspan();
			}
			CT_DecimalNumber gridSpan = tcPr.gridSpan;
			gridSpan.val = (endIndex - startIndex + 1).ToString();
		}

		public XWPFTableCell GetCell(int pos)
		{
			if (pos >= 0 && pos < ctRow.SizeOfTcArray())
			{
				return GetTableCells()[pos];
			}
			return null;
		}

		public void RemoveCell(int pos)
		{
			if (pos >= 0 && pos < ctRow.SizeOfTcArray())
			{
				tableCells.RemoveAt(pos);
				ctRow.RemoveTc(pos);
			}
		}

		/// Adds a new TableCell at the end of this tableRow
		public XWPFTableCell AddNewTableCell()
		{
			CT_Tc cell = ctRow.AddNewTc();
			XWPFTableCell xWPFTableCell = new XWPFTableCell(cell, this, table.Body);
			tableCells.Add(xWPFTableCell);
			return xWPFTableCell;
		}

		private CT_TrPr GetTrPr()
		{
			if (!ctRow.IsSetTrPr())
			{
				return ctRow.AddNewTrPr();
			}
			return ctRow.trPr;
		}

		public XWPFTable GetTable()
		{
			return table;
		}

		/// create and return a list of all XWPFTableCell
		/// who belongs to this row
		/// @return a list of {@link XWPFTableCell} 
		public List<XWPFTableCell> GetTableCells()
		{
			if (tableCells == null)
			{
				List<XWPFTableCell> list = new List<XWPFTableCell>();
				foreach (CT_Tc tc in ctRow.GetTcList())
				{
					list.Add(new XWPFTableCell(tc, this, table.Body));
				}
				tableCells = list;
			}
			return tableCells;
		}

		/// returns the XWPFTableCell which belongs to the CTTC cell
		/// if there is no XWPFTableCell which belongs to the parameter CTTc cell null will be returned
		public XWPFTableCell GetTableCell(CT_Tc cell)
		{
			for (int i = 0; i < tableCells.Count; i++)
			{
				if (tableCells[i].GetCTTc() == cell)
				{
					return tableCells[i];
				}
			}
			return null;
		}
	}
}
