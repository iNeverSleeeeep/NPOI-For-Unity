using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	public class XWPFFootnote : IEnumerator<XWPFParagraph>, IDisposable, IEnumerator, IBody
	{
		private List<XWPFParagraph> paragraphs = new List<XWPFParagraph>();

		private List<XWPFTable> tables = new List<XWPFTable>();

		private List<XWPFPictureData> pictures = new List<XWPFPictureData>();

		private List<IBodyElement> bodyElements = new List<IBodyElement>();

		private CT_FtnEdn ctFtnEdn;

		private XWPFFootnotes footnotes;

		public IList<XWPFParagraph> Paragraphs
		{
			get
			{
				return paragraphs;
			}
		}

		public IList<XWPFTable> Tables
		{
			get
			{
				return tables;
			}
		}

		public IList<XWPFPictureData> Pictures
		{
			get
			{
				return pictures;
			}
		}

		public IList<IBodyElement> BodyElements
		{
			get
			{
				return bodyElements;
			}
		}

		public POIXMLDocumentPart Owner
		{
			get
			{
				return footnotes;
			}
		}

		/// Get the PartType of the body
		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public BodyType PartType
		{
			get
			{
				return BodyType.FOOTNOTE;
			}
		}

		public XWPFParagraph Current
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		object IEnumerator.Current
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public XWPFFootnote(CT_FtnEdn note, XWPFFootnotes xFootnotes)
		{
			footnotes = xFootnotes;
			ctFtnEdn = note;
			foreach (CT_P p in ctFtnEdn.GetPList())
			{
				paragraphs.Add(new XWPFParagraph(p, this));
			}
		}

		public XWPFFootnote(XWPFDocument document, CT_FtnEdn body)
		{
			if (body != null)
			{
				foreach (CT_P p in body.GetPList())
				{
					paragraphs.Add(new XWPFParagraph(p, document));
				}
			}
		}

		public IEnumerator<XWPFParagraph> GetEnumerator()
		{
			return paragraphs.GetEnumerator();
		}

		public CT_FtnEdn GetCTFtnEdn()
		{
			return ctFtnEdn;
		}

		public void SetCTFtnEdn(CT_FtnEdn footnote)
		{
			ctFtnEdn = footnote;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="pos">position in table array</param>
		/// <returns>The table at position pos</returns>
		public XWPFTable GetTableArray(int pos)
		{
			if (pos > 0 && pos < tables.Count)
			{
				return tables[pos];
			}
			return null;
		}

		/// <summary>
		/// inserts an existing XWPFTable to the arrays bodyElements and tables
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="table"></param>
		public void InsertTable(int pos, XWPFTable table)
		{
			bodyElements.Insert(pos, table);
			int i;
			for (i = 0; i < ctFtnEdn.GetTblList().Count; i++)
			{
				CT_Tbl tblArray = ctFtnEdn.GetTblArray(i);
				if (tblArray == table.GetCTTbl())
				{
					break;
				}
			}
			tables.Insert(i, table);
		}

		/// if there is a corresponding {@link XWPFTable} of the parameter ctTable in the tableList of this header
		/// the method will return this table
		/// if there is no corresponding {@link XWPFTable} the method will return null 
		/// @param ctTable
		/// @see NPOI.XWPF.UserModel.IBody#getTable(CTTbl ctTable)
		public XWPFTable GetTable(CT_Tbl ctTable)
		{
			foreach (XWPFTable table in tables)
			{
				if (table == null)
				{
					return null;
				}
				if (table.GetCTTbl().Equals(ctTable))
				{
					return table;
				}
			}
			return null;
		}

		/// if there is a corresponding {@link XWPFParagraph} of the parameter ctTable in the paragraphList of this header or footer
		/// the method will return this paragraph
		/// if there is no corresponding {@link XWPFParagraph} the method will return null 
		/// @param p is instance of CTP and is searching for an XWPFParagraph
		/// @return null if there is no XWPFParagraph with an corresponding CTPparagraph in the paragraphList of this header or footer
		/// 		   XWPFParagraph with the correspondig CTP p
		/// @see NPOI.XWPF.UserModel.IBody#getParagraph(CTP p)
		public XWPFParagraph GetParagraph(CT_P p)
		{
			foreach (XWPFParagraph paragraph in paragraphs)
			{
				if (paragraph.GetCTP().Equals(p))
				{
					return paragraph;
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the paragraph that holds the text of the header or footer.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public XWPFParagraph GetParagraphArray(int pos)
		{
			return paragraphs[pos];
		}

		/// <summary>
		/// Get the TableCell which belongs to the TableCell
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		public XWPFTableCell GetTableCell(CT_Tc cell)
		{
			object parent = cell.Parent;
			if (!(parent is CT_Row))
			{
				return null;
			}
			CT_Row cT_Row = (CT_Row)parent;
			if (!(cT_Row.Parent is CT_Tbl))
			{
				return null;
			}
			CT_Tbl ctTable = (CT_Tbl)cT_Row.Parent;
			XWPFTable table = GetTable(ctTable);
			if (table == null)
			{
				return null;
			}
			XWPFTableRow row = table.GetRow(cT_Row);
			if (cT_Row == null)
			{
				return null;
			}
			return row.GetTableCell(cell);
		}

		/// verifies that cursor is on the right position
		/// @param cursor
		private bool IsCursorInFtn(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// @param cursor
		/// @return the inserted table
		/// @see NPOI.XWPF.UserModel.IBody#insertNewTbl(XmlCursor cursor)
		public XWPFTable InsertNewTbl(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// add a new paragraph at position of the cursor
		/// @param cursor
		/// @return the inserted paragraph
		/// @see NPOI.XWPF.UserModel.IBody#insertNewParagraph(XmlCursor cursor)
		public XWPFParagraph InsertNewParagraph(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// add a new table to the end of the footnote
		/// @param table
		/// @return the Added XWPFTable
		public XWPFTable AddNewTbl(CT_Tbl table)
		{
			CT_Tbl cT_Tbl = ctFtnEdn.AddNewTbl();
			cT_Tbl.Set(table);
			XWPFTable xWPFTable = new XWPFTable(cT_Tbl, this);
			tables.Add(xWPFTable);
			return xWPFTable;
		}

		/// add a new paragraph to the end of the footnote
		/// @param paragraph
		/// @return the Added XWPFParagraph
		public XWPFParagraph AddNewParagraph(CT_P paragraph)
		{
			CT_P prgrph = ctFtnEdn.AddNewP(paragraph);
			XWPFParagraph xWPFParagraph = new XWPFParagraph(prgrph, this);
			paragraphs.Add(xWPFParagraph);
			return xWPFParagraph;
		}

		/// @see NPOI.XWPF.UserModel.IBody#getXWPFDocument()
		public XWPFDocument GetXWPFDocument()
		{
			return footnotes.GetXWPFDocument();
		}

		/// returns the Part, to which the body belongs, which you need for Adding relationship to other parts
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			return footnotes;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public bool MoveNext()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}
	}
}
