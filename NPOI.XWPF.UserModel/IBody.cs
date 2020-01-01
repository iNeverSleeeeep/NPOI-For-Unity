using NPOI.OpenXmlFormats.Wordprocessing;
using System.Collections.Generic;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// <p>
	/// 8 Jan 2010
	/// </p>
	/// <p>
	/// // This Interface represents an object, which is able to have a collection of paragraphs and tables
	///             	this can be XWFPDocument, XWPFHeader, XWPFFooter, XWPFTableCell
	/// </p>
	/// @author Philipp Epp
	public interface IBody
	{
		/// Get the PartType of the body, for example
		/// DOCUMENT, HEADER, FOOTER,	FOOTNOTE, 
		/// @return the PartType of the body
		BodyType PartType
		{
			get;
		}

		/// Returns an Iterator with paragraphs and tables, 
		///  in the order that they occur in the text.
		IList<IBodyElement> BodyElements
		{
			get;
		}

		/// Returns the paragraph(s) that holds
		///  the text of the header or footer.
		IList<XWPFParagraph> Paragraphs
		{
			get;
		}

		/// Return the table(s) that holds the text
		///  of the IBodyPart, for complex cases
		///  where a paragraph isn't used.
		IList<XWPFTable> Tables
		{
			get;
		}

		/// returns the Part, to which the body belongs, which you need for Adding relationship to other parts
		/// Actually it is needed of the class XWPFTableCell. Because you have to know to which part the tableCell
		/// belongs.
		/// @return the Part, to which the body belongs
		POIXMLDocumentPart GetPart();

		/// if there is a corresponding {@link XWPFParagraph} of the parameter ctTable in the paragraphList of this header or footer
		/// the method will return this paragraph
		/// if there is no corresponding {@link XWPFParagraph} the method will return null 
		/// @param p is instance of CTP and is searching for an XWPFParagraph
		/// @return null if there is no XWPFParagraph with an corresponding CTPparagraph in the paragraphList of this header or footer
		/// 		   XWPFParagraph with the correspondig CTP p
		XWPFParagraph GetParagraph(CT_P p);

		/// if there is a corresponding {@link XWPFTable} of the parameter ctTable in the tableList of this header
		/// the method will return this table
		/// if there is no corresponding {@link XWPFTable} the method will return null 
		/// @param ctTable
		XWPFTable GetTable(CT_Tbl ctTable);

		/// Returns the paragraph that of position pos
		XWPFParagraph GetParagraphArray(int pos);

		/// Returns the table at position pos
		XWPFTable GetTableArray(int pos);

		/// inserts a new paragraph at position of the cursor
		///  @param cursor
		XWPFParagraph InsertNewParagraph(XmlDocument cursor);

		/// inserts a new Table at the cursor position.
		/// @param cursor
		XWPFTable InsertNewTbl(XmlDocument cursor);

		/// inserts a new Table at position pos
		/// @param pos
		/// @param table
		void InsertTable(int pos, XWPFTable table);

		/// returns the TableCell to which the Table belongs
		/// @param cell
		XWPFTableCell GetTableCell(CT_Tc cell);

		/// Return XWPFDocument
		XWPFDocument GetXWPFDocument();
	}
}
