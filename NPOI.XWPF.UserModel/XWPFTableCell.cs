using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// XWPFTableCell class.
	///
	/// @author Gregg Morris (gregg dot morris at gmail dot com) - added XWPFVertAlign enum,
	///         setColor(),
	///         setVerticalAlignment()
	public class XWPFTableCell : IBody
	{
		public enum XWPFVertAlign
		{
			TOP,
			CENTER,
			BOTH,
			BOTTOM
		}

		private CT_Tc ctTc;

		protected List<XWPFParagraph> paragraphs;

		protected List<XWPFTable> tables;

		protected List<IBodyElement> bodyElements;

		protected IBody part;

		private XWPFTableRow tableRow;

		private static Dictionary<XWPFVertAlign, ST_VerticalJc> alignMap;

		private static Dictionary<ST_VerticalJc, XWPFVertAlign> stVertAlignTypeMap;

		/// returns an Iterator with paragraphs and tables
		/// @see NPOI.XWPF.UserModel.IBody#getBodyElements()
		public IList<IBodyElement> BodyElements
		{
			get
			{
				return bodyElements.AsReadOnly();
			}
		}

		/// returns a list of paragraphs
		public IList<XWPFParagraph> Paragraphs
		{
			get
			{
				return paragraphs;
			}
		}

		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public BodyType PartType
		{
			get
			{
				return BodyType.TABLECELL;
			}
		}

		/// @see NPOI.XWPF.UserModel.IBody#getTables()
		public IList<XWPFTable> Tables
		{
			get
			{
				return tables.AsReadOnly();
			}
		}

		static XWPFTableCell()
		{
			alignMap = new Dictionary<XWPFVertAlign, ST_VerticalJc>();
			alignMap.Add(XWPFVertAlign.TOP, ST_VerticalJc.top);
			alignMap.Add(XWPFVertAlign.CENTER, ST_VerticalJc.center);
			alignMap.Add(XWPFVertAlign.BOTH, ST_VerticalJc.both);
			alignMap.Add(XWPFVertAlign.BOTTOM, ST_VerticalJc.bottom);
			stVertAlignTypeMap = new Dictionary<ST_VerticalJc, XWPFVertAlign>();
			stVertAlignTypeMap.Add(ST_VerticalJc.top, XWPFVertAlign.TOP);
			stVertAlignTypeMap.Add(ST_VerticalJc.center, XWPFVertAlign.CENTER);
			stVertAlignTypeMap.Add(ST_VerticalJc.both, XWPFVertAlign.BOTH);
			stVertAlignTypeMap.Add(ST_VerticalJc.bottom, XWPFVertAlign.BOTTOM);
		}

		/// If a table cell does not include at least one block-level element, then this document shall be considered corrupt
		public XWPFTableCell(CT_Tc cell, XWPFTableRow tableRow, IBody part)
		{
			ctTc = cell;
			this.part = part;
			this.tableRow = tableRow;
			if (cell.GetPList().Count < 1)
			{
				cell.AddNewP();
			}
			bodyElements = new List<IBodyElement>();
			paragraphs = new List<XWPFParagraph>();
			tables = new List<XWPFTable>();
			foreach (object item3 in ctTc.Items)
			{
				if (item3 is CT_P)
				{
					XWPFParagraph item = new XWPFParagraph((CT_P)item3, this);
					paragraphs.Add(item);
					bodyElements.Add(item);
				}
				if (item3 is CT_Tbl)
				{
					XWPFTable item2 = new XWPFTable((CT_Tbl)item3, this);
					tables.Add(item2);
					bodyElements.Add(item2);
				}
			}
		}

		public CT_Tc GetCTTc()
		{
			return ctTc;
		}

		public void SetParagraph(XWPFParagraph p)
		{
			if (ctTc.SizeOfPArray() == 0)
			{
				ctTc.AddNewP();
			}
			ctTc.SetPArray(0, p.GetCTP());
		}

		/// Add a Paragraph to this Table Cell
		/// @return The paragraph which was Added
		public XWPFParagraph AddParagraph()
		{
			XWPFParagraph xWPFParagraph = new XWPFParagraph(ctTc.AddNewP(), this);
			AddParagraph(xWPFParagraph);
			return xWPFParagraph;
		}

		/// add a Paragraph to this TableCell
		/// @param p the paragaph which has to be Added
		public void AddParagraph(XWPFParagraph p)
		{
			paragraphs.Add(p);
		}

		/// Removes a paragraph of this tablecell
		/// @param pos
		public void RemoveParagraph(int pos)
		{
			paragraphs.RemoveAt(pos);
			ctTc.RemoveP(pos);
		}

		/// if there is a corresponding {@link XWPFParagraph} of the parameter ctTable in the paragraphList of this table
		/// the method will return this paragraph
		/// if there is no corresponding {@link XWPFParagraph} the method will return null 
		/// @param p is instance of CTP and is searching for an XWPFParagraph
		/// @return null if there is no XWPFParagraph with an corresponding CTPparagraph in the paragraphList of this table
		/// 		   XWPFParagraph with the correspondig CTP p
		public XWPFParagraph GetParagraph(CT_P p)
		{
			foreach (XWPFParagraph paragraph in paragraphs)
			{
				if (p.Equals(paragraph.GetCTP()))
				{
					return paragraph;
				}
			}
			return null;
		}

		public void SetBorderBottom(XWPFTable.XWPFBorderType type, int size, int space, string rgbColor)
		{
			CT_TcPr cT_TcPr = null;
			if (!GetCTTc().IsSetTcPr())
			{
				cT_TcPr = GetCTTc().AddNewTcPr();
			}
			CT_TcBorders cT_TcBorders = cT_TcPr.AddNewTcBorders();
			cT_TcBorders.bottom = new CT_Border();
			CT_Border bottom = cT_TcBorders.bottom;
			bottom.val = XWPFTable.xwpfBorderTypeMap[type];
			bottom.sz = (ulong)size;
			bottom.space = (ulong)space;
			bottom.color = rgbColor;
		}

		public void SetText(string text)
		{
			CT_P prgrph = (ctTc.SizeOfPArray() == 0) ? ctTc.AddNewP() : ctTc.GetPArray(0);
			XWPFParagraph xWPFParagraph = new XWPFParagraph(prgrph, this);
			xWPFParagraph.CreateRun().SetText(text);
		}

		public XWPFTableRow GetTableRow()
		{
			return tableRow;
		}

		/// Set cell color. This sets some associated values; for finer control
		/// you may want to access these elements individually.
		/// @param rgbStr - the desired cell color, in the hex form "RRGGBB".
		public void SetColor(string rgbStr)
		{
			CT_TcPr cT_TcPr = ctTc.IsSetTcPr() ? ctTc.tcPr : ctTc.AddNewTcPr();
			CT_Shd cT_Shd = cT_TcPr.IsSetShd() ? cT_TcPr.shd : cT_TcPr.AddNewShd();
			cT_Shd.color = "auto";
			cT_Shd.val = ST_Shd.clear;
			cT_Shd.fill = rgbStr;
		}

		/// Get cell color. Note that this method only returns the "fill" value.
		/// @return RGB string of cell color
		public string GetColor()
		{
			string result = null;
			CT_TcPr tcPr = ctTc.tcPr;
			if (tcPr != null)
			{
				CT_Shd shd = tcPr.shd;
				if (shd != null)
				{
					result = shd.fill;
				}
			}
			return result;
		}

		/// Set the vertical alignment of the cell.
		/// @param vAlign - the desired alignment enum value
		public void SetVerticalAlignment(XWPFVertAlign vAlign)
		{
			CT_TcPr cT_TcPr = ctTc.IsSetTcPr() ? ctTc.tcPr : ctTc.AddNewTcPr();
			CT_VerticalJc cT_VerticalJc = cT_TcPr.AddNewVAlign();
			cT_VerticalJc.val = alignMap[vAlign];
		}

		/// Get the vertical alignment of the cell.
		/// @return the cell alignment enum value
		public XWPFVertAlign GetVerticalAlignment()
		{
			XWPFVertAlign result = XWPFVertAlign.TOP;
			CT_TcPr tcPr = ctTc.tcPr;
			if (ctTc != null)
			{
				CT_VerticalJc vAlign = tcPr.vAlign;
				result = stVertAlignTypeMap[vAlign.val];
			}
			return result;
		}

		/// add a new paragraph at position of the cursor
		/// @param cursor
		/// @return the inserted paragraph
		public XWPFParagraph InsertNewParagraph(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		public XWPFTable InsertNewTbl(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// verifies that cursor is on the right position
		private bool IsCursorInTableCell(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// @see NPOI.XWPF.UserModel.IBody#getParagraphArray(int)
		public XWPFParagraph GetParagraphArray(int pos)
		{
			if (pos > 0 && pos < paragraphs.Count)
			{
				return paragraphs[pos];
			}
			return null;
		}

		/// Get the to which the TableCell belongs
		///
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			return tableRow.GetTable().GetPart();
		}

		/// Get a table by its CTTbl-Object
		/// @see NPOI.XWPF.UserModel.IBody#getTable(org.Openxmlformats.schemas.wordProcessingml.x2006.main.CTTbl)
		public XWPFTable GetTable(CT_Tbl ctTable)
		{
			for (int i = 0; i < tables.Count; i++)
			{
				if (Tables[i].GetCTTbl() == ctTable)
				{
					return Tables[i];
				}
			}
			return null;
		}

		/// @see NPOI.XWPF.UserModel.IBody#getTableArray(int)
		public XWPFTable GetTableArray(int pos)
		{
			if (pos >= 0 && pos < tables.Count)
			{
				return tables[pos];
			}
			return null;
		}

		/// inserts an existing XWPFTable to the arrays bodyElements and tables
		/// @see NPOI.XWPF.UserModel.IBody#insertTable(int, NPOI.XWPF.UserModel.XWPFTable)
		public void InsertTable(int pos, XWPFTable table)
		{
			bodyElements.Insert(pos, table);
			int i;
			for (i = 0; i < ctTc.GetTblList().Count; i++)
			{
				CT_Tbl tblArray = ctTc.GetTblArray(i);
				if (tblArray == table.GetCTTbl())
				{
					break;
				}
			}
			tables.Insert(i, table);
		}

		public string GetText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (XWPFParagraph paragraph in paragraphs)
			{
				stringBuilder.Append(paragraph.Text);
			}
			return stringBuilder.ToString();
		}

		/// Get the TableCell which belongs to the TableCell
		public XWPFTableCell GetTableCell(CT_Tc cell)
		{
			throw new NotImplementedException();
		}

		public XWPFDocument GetXWPFDocument()
		{
			return part.GetXWPFDocument();
		}
	}
}
