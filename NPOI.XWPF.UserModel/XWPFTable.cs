using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.XWPF.UserModel
{
	/// Sketch of XWPFTable class. Only table's text is being hold.
	/// <p />
	/// Specifies the contents of a table present in the document. A table is a Set
	/// of paragraphs (and other block-level content) arranged in rows and columns.
	///
	/// @author Yury Batrakov (batrakov at gmail.com)
	/// @author Gregg Morris (gregg dot morris at gmail dot com) - added 
	///         setStyleID()
	///         getRowBandSize(), setRowBandSize()
	///         getColBandSize(), setColBandSize()
	///         getInsideHBorderType(), getInsideHBorderSize(), getInsideHBorderSpace(), getInsideHBorderColor()
	///         getInsideVBorderType(), getInsideVBorderSize(), getInsideVBorderSpace(), getInsideVBorderColor()
	///         setInsideHBorder(), setInsideVBorder()
	///         getCellMarginTop(), getCellMarginLeft(), getCellMarginBottom(), getCellMarginRight()
	///         setCellMargins()
	public class XWPFTable : IBodyElement
	{
		public enum XWPFBorderType
		{
			NIL,
			NONE,
			SINGLE,
			THICK,
			DOUBLE,
			DOTTED,
			DASHED,
			DOT_DASH
		}

		protected StringBuilder text = new StringBuilder();

		private CT_Tbl ctTbl;

		protected List<XWPFTableRow> tableRows;

		protected List<string> styleIDs;

		internal static Dictionary<XWPFBorderType, ST_Border> xwpfBorderTypeMap;

		internal static Dictionary<ST_Border, XWPFBorderType> stBorderTypeMap;

		protected IBody part;

		/// @return text
		public string Text
		{
			get
			{
				return text.ToString();
			}
		}

		/// @return width value
		public int Width
		{
			get
			{
				CT_TblPr trPr = GetTrPr();
				if (!trPr.IsSetTblW())
				{
					return -1;
				}
				return int.Parse(trPr.tblW.w);
			}
			set
			{
				CT_TblPr trPr = GetTrPr();
				CT_TblWidth cT_TblWidth = trPr.IsSetTblW() ? trPr.tblW : trPr.AddNewTblW();
				cT_TblWidth.w = value.ToString();
				cT_TblWidth.type = ST_TblWidth.pct;
			}
		}

		/// @return number of rows in table
		public int NumberOfRows
		{
			get
			{
				return ctTbl.SizeOfTrArray();
			}
		}

		/// Get the StyleID of the table
		/// @return	style-ID of the table
		public string StyleID
		{
			get
			{
				string result = null;
				CT_TblPr tblPr = ctTbl.tblPr;
				if (tblPr != null)
				{
					CT_String tblStyle = tblPr.tblStyle;
					if (tblStyle != null)
					{
						result = tblStyle.val;
					}
				}
				return result;
			}
			set
			{
				CT_TblPr trPr = GetTrPr();
				CT_String cT_String = trPr.tblStyle;
				if (cT_String == null)
				{
					cT_String = trPr.AddNewTblStyle();
				}
				cT_String.val = value;
			}
		}

		public XWPFBorderType InsideHBorderType
		{
			get
			{
				XWPFBorderType result = XWPFBorderType.NONE;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideH())
					{
						CT_Border insideH = tblBorders.insideH;
						result = stBorderTypeMap[insideH.val];
					}
				}
				return result;
			}
		}

		public int InsideHBorderSize
		{
			get
			{
				int result = -1;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideH())
					{
						CT_Border insideH = tblBorders.insideH;
						result = (int)insideH.sz;
					}
				}
				return result;
			}
		}

		public int InsideHBorderSpace
		{
			get
			{
				int result = -1;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideH())
					{
						CT_Border insideH = tblBorders.insideH;
						result = (int)insideH.space;
					}
				}
				return result;
			}
		}

		public string InsideHBorderColor
		{
			get
			{
				string result = null;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideH())
					{
						CT_Border insideH = tblBorders.insideH;
						result = insideH.color;
					}
				}
				return result;
			}
		}

		public XWPFBorderType InsideVBorderType
		{
			get
			{
				XWPFBorderType result = XWPFBorderType.NONE;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideV())
					{
						CT_Border insideV = tblBorders.insideV;
						result = stBorderTypeMap[insideV.val];
					}
				}
				return result;
			}
		}

		public int InsideVBorderSize
		{
			get
			{
				int result = -1;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideV())
					{
						CT_Border insideV = tblBorders.insideV;
						result = (int)insideV.sz;
					}
				}
				return result;
			}
		}

		public int InsideVBorderSpace
		{
			get
			{
				int result = -1;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideV())
					{
						CT_Border insideV = tblBorders.insideV;
						result = (int)insideV.space;
					}
				}
				return result;
			}
		}

		public string InsideVBorderColor
		{
			get
			{
				string result = null;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblBorders())
				{
					CT_TblBorders tblBorders = trPr.tblBorders;
					if (tblBorders.IsSetInsideV())
					{
						CT_Border insideV = tblBorders.insideV;
						result = insideV.color;
					}
				}
				return result;
			}
		}

		public int RowBandSize
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblStyleRowBandSize())
				{
					CT_DecimalNumber tblStyleRowBandSize = trPr.tblStyleRowBandSize;
					int.TryParse(tblStyleRowBandSize.val, out result);
				}
				return result;
			}
			set
			{
				CT_TblPr trPr = GetTrPr();
				CT_DecimalNumber cT_DecimalNumber = trPr.IsSetTblStyleRowBandSize() ? trPr.tblStyleRowBandSize : trPr.AddNewTblStyleRowBandSize();
				cT_DecimalNumber.val = value.ToString();
			}
		}

		public int ColBandSize
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				if (trPr.IsSetTblStyleColBandSize())
				{
					CT_DecimalNumber tblStyleColBandSize = trPr.tblStyleColBandSize;
					int.TryParse(tblStyleColBandSize.val, out result);
				}
				return result;
			}
			set
			{
				CT_TblPr trPr = GetTrPr();
				CT_DecimalNumber cT_DecimalNumber = trPr.IsSetTblStyleColBandSize() ? trPr.tblStyleColBandSize : trPr.AddNewTblStyleColBandSize();
				cT_DecimalNumber.val = value.ToString();
			}
		}

		public int CellMarginTop
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				CT_TblCellMar tblCellMar = trPr.tblCellMar;
				if (tblCellMar != null)
				{
					CT_TblWidth top = tblCellMar.top;
					if (top != null)
					{
						int.TryParse(top.w, out result);
					}
				}
				return result;
			}
		}

		public int CellMarginLeft
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				CT_TblCellMar tblCellMar = trPr.tblCellMar;
				if (tblCellMar != null)
				{
					CT_TblWidth left = tblCellMar.left;
					if (left != null)
					{
						int.TryParse(left.w, out result);
					}
				}
				return result;
			}
		}

		public int CellMarginBottom
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				CT_TblCellMar tblCellMar = trPr.tblCellMar;
				if (tblCellMar != null)
				{
					CT_TblWidth bottom = tblCellMar.bottom;
					if (bottom != null)
					{
						int.TryParse(bottom.w, out result);
					}
				}
				return result;
			}
		}

		public int CellMarginRight
		{
			get
			{
				int result = 0;
				CT_TblPr trPr = GetTrPr();
				CT_TblCellMar tblCellMar = trPr.tblCellMar;
				if (tblCellMar != null)
				{
					CT_TblWidth right = tblCellMar.right;
					if (right != null)
					{
						int.TryParse(right.w, out result);
					}
				}
				return result;
			}
		}

		public List<XWPFTableRow> Rows
		{
			get
			{
				return tableRows;
			}
		}

		/// returns the type of the BodyElement Table
		/// @see NPOI.XWPF.UserModel.IBodyElement#getElementType()
		public BodyElementType ElementType
		{
			get
			{
				return BodyElementType.TABLE;
			}
		}

		public IBody Body
		{
			get
			{
				return part;
			}
		}

		/// returns the partType of the bodyPart which owns the bodyElement
		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public BodyType PartType
		{
			get
			{
				return part.PartType;
			}
		}

		static XWPFTable()
		{
			xwpfBorderTypeMap = new Dictionary<XWPFBorderType, ST_Border>();
			xwpfBorderTypeMap.Add(XWPFBorderType.NIL, ST_Border.nil);
			xwpfBorderTypeMap.Add(XWPFBorderType.NONE, ST_Border.none);
			xwpfBorderTypeMap.Add(XWPFBorderType.SINGLE, ST_Border.single);
			xwpfBorderTypeMap.Add(XWPFBorderType.THICK, ST_Border.thick);
			xwpfBorderTypeMap.Add(XWPFBorderType.DOUBLE, ST_Border.@double);
			xwpfBorderTypeMap.Add(XWPFBorderType.DOTTED, ST_Border.dotted);
			xwpfBorderTypeMap.Add(XWPFBorderType.DASHED, ST_Border.dashed);
			xwpfBorderTypeMap.Add(XWPFBorderType.DOT_DASH, ST_Border.dotDash);
			stBorderTypeMap = new Dictionary<ST_Border, XWPFBorderType>();
			stBorderTypeMap.Add(ST_Border.nil, XWPFBorderType.NIL);
			stBorderTypeMap.Add(ST_Border.none, XWPFBorderType.NONE);
			stBorderTypeMap.Add(ST_Border.single, XWPFBorderType.SINGLE);
			stBorderTypeMap.Add(ST_Border.thick, XWPFBorderType.THICK);
			stBorderTypeMap.Add(ST_Border.@double, XWPFBorderType.DOUBLE);
			stBorderTypeMap.Add(ST_Border.dotted, XWPFBorderType.DOTTED);
			stBorderTypeMap.Add(ST_Border.dashed, XWPFBorderType.DASHED);
			stBorderTypeMap.Add(ST_Border.dotDash, XWPFBorderType.DOT_DASH);
		}

		public XWPFTable(CT_Tbl table, IBody part, int row, int col)
			: this(table, part)
		{
			CT_TblGrid cT_TblGrid = table.AddNewTblGrid();
			for (int i = 0; i < col; i++)
			{
				CT_TblGridCol cT_TblGridCol = cT_TblGrid.AddNewGridCol();
				cT_TblGridCol.w = 300uL;
			}
			for (int j = 0; j < row; j++)
			{
				XWPFTableRow xWPFTableRow = (GetRow(j) == null) ? CreateRow() : GetRow(j);
				for (int k = 0; k < col; k++)
				{
					if (xWPFTableRow.GetCell(k) == null)
					{
						xWPFTableRow.CreateCell();
					}
				}
			}
		}

		public void SetColumnWidth(int columnIndex, ulong width)
		{
			if (ctTbl.tblGrid != null)
			{
				if (columnIndex > ctTbl.tblGrid.gridCol.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Column index {0} doesn't exist.", columnIndex));
				}
				ctTbl.tblGrid.gridCol[columnIndex].w = width;
			}
		}

		public XWPFTable(CT_Tbl table, IBody part)
		{
			this.part = part;
			ctTbl = table;
			tableRows = new List<XWPFTableRow>();
			if (table.SizeOfTrArray() == 0)
			{
				CreateEmptyTable(table);
			}
			foreach (CT_Row tr in table.GetTrList())
			{
				StringBuilder stringBuilder = new StringBuilder();
				tr.Table = table;
				XWPFTableRow item = new XWPFTableRow(tr, this);
				tableRows.Add(item);
				foreach (CT_Tc tc in tr.GetTcList())
				{
					foreach (CT_P p in tc.GetPList())
					{
						XWPFParagraph xWPFParagraph = new XWPFParagraph(p, part);
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append('\t');
						}
						stringBuilder.Append(xWPFParagraph.Text);
					}
				}
				if (stringBuilder.Length > 0)
				{
					text.Append(stringBuilder);
					text.Append('\n');
				}
			}
		}

		private void CreateEmptyTable(CT_Tbl table)
		{
			table.AddNewTr().AddNewTc().AddNewP();
			CT_TblPr cT_TblPr = table.AddNewTblPr();
			if (!cT_TblPr.IsSetTblW())
			{
				cT_TblPr.AddNewTblW().w = "0";
			}
			cT_TblPr.tblW.type = ST_TblWidth.auto;
			cT_TblPr.AddNewTblLayout().type = ST_TblLayoutType.autofit;
			CT_TblBorders cT_TblBorders = cT_TblPr.AddNewTblBorders();
			cT_TblBorders.AddNewBottom().val = ST_Border.single;
			cT_TblBorders.AddNewInsideH().val = ST_Border.single;
			cT_TblBorders.AddNewInsideV().val = ST_Border.single;
			cT_TblBorders.AddNewLeft().val = ST_Border.single;
			cT_TblBorders.AddNewRight().val = ST_Border.single;
			cT_TblBorders.AddNewTop().val = ST_Border.single;
			CT_TblGrid cT_TblGrid = table.AddNewTblGrid();
			cT_TblGrid.AddNewGridCol().w = 2000uL;
		}

		/// @return ctTbl object
		internal CT_Tbl GetCTTbl()
		{
			return ctTbl;
		}

		public void AddNewRowBetween(int start, int end)
		{
			throw new NotImplementedException();
		}

		/// add a new column for each row in this table
		public void AddNewCol()
		{
			if (ctTbl.SizeOfTrArray() == 0)
			{
				CreateRow();
			}
			for (int i = 0; i < ctTbl.SizeOfTrArray(); i++)
			{
				XWPFTableRow xWPFTableRow = new XWPFTableRow(ctTbl.GetTrArray(i), this);
				xWPFTableRow.CreateCell();
			}
		}

		/// create a new XWPFTableRow object with as many cells as the number of columns defined in that moment
		///
		/// @return tableRow
		public XWPFTableRow CreateRow()
		{
			int sizeCol = (ctTbl.SizeOfTrArray() > 0) ? ctTbl.GetTrArray(0).SizeOfTcArray() : 0;
			XWPFTableRow xWPFTableRow = new XWPFTableRow(ctTbl.AddNewTr(), this);
			AddColumn(xWPFTableRow, sizeCol);
			tableRows.Add(xWPFTableRow);
			return xWPFTableRow;
		}

		/// @param pos - index of the row
		/// @return the row at the position specified or null if no rows is defined or if the position is greather than the max size of rows array
		public XWPFTableRow GetRow(int pos)
		{
			if (pos >= 0 && pos < ctTbl.SizeOfTrArray())
			{
				return Rows[pos];
			}
			return null;
		}

		private CT_TblPr GetTrPr()
		{
			if (ctTbl.tblPr == null)
			{
				return ctTbl.AddNewTblPr();
			}
			return ctTbl.tblPr;
		}

		private void AddColumn(XWPFTableRow tabRow, int sizeCol)
		{
			if (sizeCol > 0)
			{
				for (int i = 0; i < sizeCol; i++)
				{
					tabRow.CreateCell();
				}
			}
		}

		public void SetTopBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = (cT_TblBorders.top != null) ? cT_TblBorders.top : cT_TblBorders.AddNewTop();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetBottomBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = (cT_TblBorders.bottom != null) ? cT_TblBorders.bottom : cT_TblBorders.AddNewBottom();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetLeftBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = (cT_TblBorders.left != null) ? cT_TblBorders.left : cT_TblBorders.AddNewLeft();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetRightBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = (cT_TblBorders.right != null) ? cT_TblBorders.right : cT_TblBorders.AddNewRight();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetInsideHBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = cT_TblBorders.IsSetInsideH() ? cT_TblBorders.insideH : cT_TblBorders.AddNewInsideH();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetInsideVBorder(XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblBorders cT_TblBorders = trPr.IsSetTblBorders() ? trPr.tblBorders : trPr.AddNewTblBorders();
			CT_Border cT_Border = cT_TblBorders.IsSetInsideV() ? cT_TblBorders.insideV : cT_TblBorders.AddNewInsideV();
			cT_Border.val = xwpfBorderTypeMap[type];
			cT_Border.sz = (ulong)size;
			cT_Border.space = (ulong)space;
			cT_Border.color = rgbColor;
		}

		public void SetCellMargins(int top, int left, int bottom, int right)
		{
			CT_TblPr trPr = GetTrPr();
			CT_TblCellMar cT_TblCellMar = trPr.IsSetTblCellMar() ? trPr.tblCellMar : trPr.AddNewTblCellMar();
			CT_TblWidth cT_TblWidth = cT_TblCellMar.IsSetLeft() ? cT_TblCellMar.left : cT_TblCellMar.AddNewLeft();
			cT_TblWidth.type = ST_TblWidth.dxa;
			cT_TblWidth.w = left.ToString();
			cT_TblWidth = (cT_TblCellMar.IsSetTop() ? cT_TblCellMar.top : cT_TblCellMar.AddNewTop());
			cT_TblWidth.type = ST_TblWidth.dxa;
			cT_TblWidth.w = top.ToString();
			cT_TblWidth = (cT_TblCellMar.IsSetBottom() ? cT_TblCellMar.bottom : cT_TblCellMar.AddNewBottom());
			cT_TblWidth.type = ST_TblWidth.dxa;
			cT_TblWidth.w = bottom.ToString();
			cT_TblWidth = (cT_TblCellMar.IsSetRight() ? cT_TblCellMar.right : cT_TblCellMar.AddNewRight());
			cT_TblWidth.type = ST_TblWidth.dxa;
			cT_TblWidth.w = right.ToString();
		}

		/// add a new Row to the table
		///
		/// @param row	the row which should be Added
		public void AddRow(XWPFTableRow row)
		{
			ctTbl.AddNewTr();
			ctTbl.SetTrArray(NumberOfRows - 1, row.GetCTRow());
			tableRows.Add(row);
		}

		/// add a new Row to the table
		/// at position pos
		/// @param row	the row which should be Added
		public bool AddRow(XWPFTableRow row, int pos)
		{
			if (pos >= 0 && pos <= tableRows.Count)
			{
				ctTbl.InsertNewTr(pos);
				ctTbl.SetTrArray(pos, row.GetCTRow());
				tableRows.Insert(pos, row);
				return true;
			}
			return false;
		}

		/// inserts a new tablerow 
		/// @param pos
		/// @return  the inserted row
		public XWPFTableRow InsertNewTableRow(int pos)
		{
			if (pos >= 0 && pos <= tableRows.Count)
			{
				CT_Row row = ctTbl.InsertNewTr(pos);
				XWPFTableRow xWPFTableRow = new XWPFTableRow(row, this);
				tableRows.Insert(pos, xWPFTableRow);
				return xWPFTableRow;
			}
			return null;
		}

		/// Remove a row at position pos from the table
		/// @param pos	position the Row in the Table
		public bool RemoveRow(int pos)
		{
			if (pos >= 0 && pos < tableRows.Count)
			{
				if (ctTbl.SizeOfTrArray() > 0)
				{
					ctTbl.RemoveTr(pos);
				}
				tableRows.RemoveAt(pos);
				return true;
			}
			return false;
		}

		/// returns the part of the bodyElement
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			if (part != null)
			{
				return part.GetPart();
			}
			return null;
		}

		/// returns the XWPFRow which belongs to the CTRow row
		/// if this row is not existing in the table null will be returned
		public XWPFTableRow GetRow(CT_Row row)
		{
			for (int i = 0; i < Rows.Count; i++)
			{
				if (Rows[i].GetCTRow() == row)
				{
					return GetRow(i);
				}
			}
			return null;
		}
	}
}
