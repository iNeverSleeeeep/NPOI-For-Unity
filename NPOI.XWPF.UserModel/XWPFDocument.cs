using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.Util;
using NPOI.XWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// Experimental class to do low level Processing
	///  of docx files.
	///
	/// If you're using these low level classes, then you
	///  will almost certainly need to refer to the OOXML
	///  specifications from
	///  http://www.ecma-international.org/publications/standards/Ecma-376.htm
	///
	/// WARNING - APIs expected to change rapidly
	public class XWPFDocument : POIXMLDocument, Document, IBody
	{
		private CT_Document ctDocument;

		private XWPFSettings Settings;

		/// Keeps track on all id-values used in this document and included parts, like headers, footers, etc.
		private IdentifierManager drawingIdManager = new IdentifierManager(1L, 4294967295L);

		protected List<XWPFFooter> footers = new List<XWPFFooter>();

		protected List<XWPFHeader> headers = new List<XWPFHeader>();

		protected List<XWPFComment> comments = new List<XWPFComment>();

		protected List<XWPFHyperlink> hyperlinks = new List<XWPFHyperlink>();

		protected List<XWPFParagraph> paragraphs = new List<XWPFParagraph>();

		protected List<XWPFTable> tables = new List<XWPFTable>();

		protected List<IBodyElement> bodyElements = new List<IBodyElement>();

		protected List<XWPFPictureData> pictures = new List<XWPFPictureData>();

		protected Dictionary<long, List<XWPFPictureData>> packagePictures = new Dictionary<long, List<XWPFPictureData>>();

		protected Dictionary<int, XWPFFootnote> endnotes = new Dictionary<int, XWPFFootnote>();

		protected XWPFNumbering numbering;

		protected XWPFStyles styles;

		protected XWPFFootnotes footnotes;

		/// Handles the joy of different headers/footers for different pages 
		private XWPFHeaderFooterPolicy headerFooterPolicy;

		/// Returns the low level document base object
		public CT_Document Document
		{
			get
			{
				return ctDocument;
			}
		}

		internal IdentifierManager DrawingIdManager
		{
			get
			{
				return drawingIdManager;
			}
		}

		/// returns an Iterator with paragraphs and tables
		/// @see NPOI.XWPF.UserModel.IBody#getBodyElements()
		public IList<IBodyElement> BodyElements
		{
			get
			{
				return bodyElements.AsReadOnly();
			}
		}

		/// @see NPOI.XWPF.UserModel.IBody#getParagraphs()
		public IList<XWPFParagraph> Paragraphs
		{
			get
			{
				return paragraphs.AsReadOnly();
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

		/// @return  the list of footers
		public IList<XWPFFooter> FooterList
		{
			get
			{
				return footers.AsReadOnly();
			}
		}

		/// @return  the list of headers
		public IList<XWPFHeader> HeaderList
		{
			get
			{
				return headers.AsReadOnly();
			}
		}

		/// Returns all Pictures, which are referenced from the document itself.
		/// @return a {@link List} of {@link XWPFPictureData}. The returned {@link List} is unmodifiable. Use #a
		public IList<XWPFPictureData> AllPictures
		{
			get
			{
				return pictures.AsReadOnly();
			}
		}

		/// @return all Pictures in this package
		public IList<XWPFPictureData> AllPackagePictures
		{
			get
			{
				List<XWPFPictureData> list = new List<XWPFPictureData>();
				foreach (List<XWPFPictureData> value in packagePictures.Values)
				{
					list.AddRange(value);
				}
				return list.AsReadOnly();
			}
		}

		/// Get the PartType of the body, for example
		/// DOCUMENT, HEADER, FOOTER,	FOOTNOTE,
		///
		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public BodyType PartType
		{
			get
			{
				return BodyType.DOCUMENT;
			}
		}

		public XWPFDocument(OPCPackage pkg)
			: base(pkg)
		{
			Load(XWPFFactory.GetInstance());
		}

		public XWPFDocument(Stream is1)
			: base(PackageHelper.Open(is1))
		{
			Load(XWPFFactory.GetInstance());
		}

		public XWPFDocument()
			: base(NewPackage())
		{
			OnDocumentCreate();
		}

		internal override void OnDocumentRead()
		{
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				DocumentDocument documentDocument = DocumentDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				ctDocument = documentDocument.Document;
				InitFootnotes();
				foreach (object item3 in ctDocument.body.Items)
				{
					if (item3 is CT_P)
					{
						XWPFParagraph item = new XWPFParagraph((CT_P)item3, this);
						bodyElements.Add(item);
						paragraphs.Add(item);
					}
					else if (item3 is CT_Tbl)
					{
						XWPFTable item2 = new XWPFTable((CT_Tbl)item3, this);
						bodyElements.Add(item2);
						tables.Add(item2);
					}
				}
				if (documentDocument.Document.body.sectPr != null)
				{
					headerFooterPolicy = new XWPFHeaderFooterPolicy(this);
				}
				foreach (POIXMLDocumentPart relation in GetRelations())
				{
					string relationshipType = relation.GetPackageRelationship().RelationshipType;
					if (relationshipType.Equals(XWPFRelation.STYLES.Relation))
					{
						styles = (XWPFStyles)relation;
						styles.OnDocumentRead();
					}
					else if (relationshipType.Equals(XWPFRelation.NUMBERING.Relation))
					{
						numbering = (XWPFNumbering)relation;
						numbering.OnDocumentRead();
					}
					else if (relationshipType.Equals(XWPFRelation.FOOTER.Relation))
					{
						XWPFFooter xWPFFooter = (XWPFFooter)relation;
						footers.Add(xWPFFooter);
						xWPFFooter.OnDocumentRead();
					}
					else if (relationshipType.Equals(XWPFRelation.HEADER.Relation))
					{
						XWPFHeader xWPFHeader = (XWPFHeader)relation;
						headers.Add(xWPFHeader);
						xWPFHeader.OnDocumentRead();
					}
					else if (relationshipType.Equals(XWPFRelation.COMMENT.Relation))
					{
						XmlDocument doc2 = POIXMLDocumentPart.ConvertStreamToXml(relation.GetPackagePart().GetInputStream());
						CommentsDocument commentsDocument = CommentsDocument.Parse(doc2, POIXMLDocumentPart.NamespaceManager);
						foreach (CT_Comment item4 in commentsDocument.Comments.comment)
						{
							comments.Add(new XWPFComment(item4, this));
						}
					}
					else if (relationshipType.Equals(XWPFRelation.SETTINGS.Relation))
					{
						Settings = (XWPFSettings)relation;
						Settings.OnDocumentRead();
					}
					else if (relationshipType.Equals(XWPFRelation.IMAGES.Relation))
					{
						XWPFPictureData xWPFPictureData = (XWPFPictureData)relation;
						xWPFPictureData.OnDocumentRead();
						RegisterPackagePictureData(xWPFPictureData);
						pictures.Add(xWPFPictureData);
					}
					else if (relationshipType.Equals(XWPFRelation.GLOSSARY_DOCUMENT.Relation))
					{
						foreach (POIXMLDocumentPart relation2 in relation.GetRelations())
						{
							try
							{
								relation2.OnDocumentRead();
							}
							catch (Exception ex)
							{
								throw new POIXMLException(ex);
							}
						}
					}
				}
				InitHyperlinks();
			}
			catch (XmlException ex2)
			{
				throw new POIXMLException(ex2);
			}
		}

		private void InitHyperlinks()
		{
			try
			{
				IEnumerator<PackageRelationship> enumerator = GetPackagePart().GetRelationshipsByType(XWPFRelation.HYPERLINK.Relation).GetEnumerator();
				while (enumerator.MoveNext())
				{
					PackageRelationship current = enumerator.Current;
					hyperlinks.Add(new XWPFHyperlink(current.Id, current.TargetUri.ToString()));
				}
			}
			catch (InvalidDataException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		private void InitFootnotes()
		{
			foreach (POIXMLDocumentPart relation in GetRelations())
			{
				string relationshipType = relation.GetPackageRelationship().RelationshipType;
				if (relationshipType.Equals(XWPFRelation.FOOTNOTE.Relation))
				{
					footnotes = (XWPFFootnotes)relation;
					footnotes.OnDocumentRead();
				}
				else if (relationshipType.Equals(XWPFRelation.ENDNOTE.Relation))
				{
					XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(relation.GetPackagePart().GetInputStream());
					EndnotesDocument endnotesDocument = EndnotesDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
					foreach (CT_FtnEdn item in endnotesDocument.Endnotes.endnote)
					{
						endnotes.Add(int.Parse(item.id), new XWPFFootnote(this, item));
					}
				}
			}
		}

		/// Create a new WordProcessingML package and Setup the default minimal content
		protected static OPCPackage NewPackage()
		{
			try
			{
				OPCPackage oPCPackage = OPCPackage.Create(new MemoryStream());
				PackagePartName packagePartName = PackagingUriHelper.CreatePartName(XWPFRelation.DOCUMENT.DefaultFileName);
				oPCPackage.AddRelationship(packagePartName, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument");
				oPCPackage.CreatePart(packagePartName, XWPFRelation.DOCUMENT.ContentType);
				oPCPackage.GetPackageProperties().SetCreatorProperty(POIXMLDocument.DOCUMENT_CREATOR);
				return oPCPackage;
			}
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Create a new CT_Document with all values Set to default
		internal override void OnDocumentCreate()
		{
			ctDocument = new CT_Document();
			ctDocument.AddNewBody();
			Settings = (XWPFSettings)CreateRelationship(XWPFRelation.SETTINGS, XWPFFactory.GetInstance());
			CreateStyles();
			ExtendedProperties extendedProperties = GetProperties().ExtendedProperties;
			extendedProperties.GetUnderlyingProperties().Application = POIXMLDocument.DOCUMENT_CREATOR;
		}

		public IEnumerator<IBodyElement> GetBodyElementsIterator()
		{
			return bodyElements.GetEnumerator();
		}

		/// @see NPOI.XWPF.UserModel.IBody#getTableArray(int)
		public XWPFTable GetTableArray(int pos)
		{
			if (pos > 0 && pos < tables.Count)
			{
				return tables[pos];
			}
			return null;
		}

		public XWPFFooter GetFooterArray(int pos)
		{
			return footers[pos];
		}

		public XWPFHeader GetHeaderArray(int pos)
		{
			return headers[pos];
		}

		public string GetTblStyle(XWPFTable table)
		{
			return table.StyleID;
		}

		public XWPFHyperlink GetHyperlinkByID(string id)
		{
			IEnumerator<XWPFHyperlink> enumerator = hyperlinks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				XWPFHyperlink current = enumerator.Current;
				if (current.Id.Equals(id))
				{
					return current;
				}
			}
			return null;
		}

		public XWPFFootnote GetFootnoteByID(int id)
		{
			if (footnotes == null)
			{
				return null;
			}
			return footnotes.GetFootnoteById(id);
		}

		public XWPFFootnote GetEndnoteByID(int id)
		{
			if (endnotes == null || !endnotes.ContainsKey(id))
			{
				return null;
			}
			return endnotes[id];
		}

		public List<XWPFFootnote> GetFootnotes()
		{
			if (footnotes == null)
			{
				return new List<XWPFFootnote>();
			}
			return footnotes.GetFootnotesList();
		}

		public XWPFHyperlink[] GetHyperlinks()
		{
			return hyperlinks.ToArray();
		}

		public XWPFComment GetCommentByID(string id)
		{
			IEnumerator<XWPFComment> enumerator = comments.GetEnumerator();
			while (enumerator.MoveNext())
			{
				XWPFComment current = enumerator.Current;
				if (current.Id.Equals(id))
				{
					return current;
				}
			}
			return null;
		}

		public XWPFComment[] GetComments()
		{
			return comments.ToArray();
		}

		/// Get the document part that's defined as the
		///  given relationship of the core document.
		public PackagePart GetPartById(string id)
		{
			try
			{
				PackagePart corePart = base.CorePart;
				return corePart.GetRelatedPart(corePart.GetRelationship(id));
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("GetTargetPart exception", innerException);
			}
		}

		/// Returns the policy on headers and footers, which
		///  also provides a way to Get at them.
		public XWPFHeaderFooterPolicy GetHeaderFooterPolicy()
		{
			return headerFooterPolicy;
		}

		/// Returns the styles object used
		public CT_Styles GetCTStyle()
		{
			PackagePart[] relatedByType;
			try
			{
				relatedByType = GetRelatedByType(XWPFRelation.STYLES.Relation);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("get Style document part exception", innerException);
			}
			if (relatedByType.Length != 1)
			{
				throw new InvalidOperationException("Expecting one Styles document part, but found " + relatedByType.Length);
			}
			XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(relatedByType[0].GetInputStream());
			StylesDocument stylesDocument = StylesDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
			return stylesDocument.Styles;
		}

		/// Get the document's embedded files.
		public override List<PackagePart> GetAllEmbedds()
		{
			List<PackagePart> list = new List<PackagePart>();
			PackagePart packagePart = GetPackagePart();
			foreach (PackageRelationship item in GetPackagePart().GetRelationshipsByType(POIXMLDocument.OLE_OBJECT_REL_TYPE))
			{
				list.Add(packagePart.GetRelatedPart(item));
			}
			foreach (PackageRelationship item2 in GetPackagePart().GetRelationshipsByType(POIXMLDocument.PACK_OBJECT_REL_TYPE))
			{
				list.Add(packagePart.GetRelatedPart(item2));
			}
			return list;
		}

		/// Finds that for example the 2nd entry in the body list is the 1st paragraph
		private int GetBodyElementSpecificPos(int pos, List<IBodyElement> list)
		{
			if (list.Count == 0)
			{
				return -1;
			}
			if (pos >= 0 && pos < bodyElements.Count)
			{
				IBodyElement bodyElement = bodyElements[pos];
				if (bodyElement.ElementType != list[0].ElementType)
				{
					return -1;
				}
				int num = Math.Min(pos, list.Count - 1);
				for (int num2 = num; num2 >= 0; num2--)
				{
					if (list[num2] == bodyElement)
					{
						return num2;
					}
				}
			}
			return -1;
		}

		/// Look up the paragraph at the specified position in the body elemnts list
		/// and return this paragraphs position in the paragraphs list
		///
		/// @param pos
		///            The position of the relevant paragraph in the body elements
		///            list
		/// @return the position of the paragraph in the paragraphs list, if there is
		///         a paragraph at the position in the bodyelements list. Else it
		///         will return -1
		public int GetParagraphPos(int pos)
		{
			List<IBodyElement> list = new List<IBodyElement>();
			foreach (XWPFParagraph paragraph in paragraphs)
			{
				list.Add(paragraph);
			}
			return GetBodyElementSpecificPos(pos, list);
		}

		/// Get with the position of a table in the bodyelement array list 
		/// the position of this table in the table array list
		/// @param pos position of the table in the bodyelement array list
		/// @return if there is a table at the position in the bodyelement array list,
		/// 		   else it will return null. 
		public int GetTablePos(int pos)
		{
			List<IBodyElement> list = new List<IBodyElement>();
			foreach (XWPFTable table in tables)
			{
				list.Add(table);
			}
			return GetBodyElementSpecificPos(pos, list);
		}

		/// Add a new paragraph at position of the cursor. The cursor must be on the
		/// {@link org.apache.xmlbeans.XmlCursor.TokenType#START} tag of an subelement
		/// of the documents body. When this method is done, the cursor passed as
		/// parameter points to the {@link org.apache.xmlbeans.XmlCursor.TokenType#END}
		/// of the newly inserted paragraph.
		///
		/// @param cursor
		/// @return the {@link XWPFParagraph} object representing the newly inserted
		///         CTP object
		public XWPFParagraph InsertNewParagraph(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		public XWPFTable InsertNewTbl(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		/// verifies that cursor is on the right position
		/// @param cursor
		private bool IsCursorInBody(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		private int GetPosOfBodyElement(IBodyElement needle)
		{
			BodyElementType elementType = needle.ElementType;
			for (int i = 0; i < bodyElements.Count; i++)
			{
				IBodyElement bodyElement = bodyElements[i];
				if (bodyElement.ElementType == elementType && bodyElement.Equals(needle))
				{
					return i;
				}
			}
			return -1;
		}

		/// Get the position of the paragraph, within the list
		///  of all the body elements.
		/// @param p The paragraph to find
		/// @return The location, or -1 if the paragraph couldn't be found 
		public int GetPosOfParagraph(XWPFParagraph p)
		{
			return GetPosOfBodyElement(p);
		}

		/// Get the position of the table, within the list of
		///  all the body elements.
		/// @param t The table to find
		/// @return The location, or -1 if the table couldn't be found
		public int GetPosOfTable(XWPFTable t)
		{
			return GetPosOfBodyElement(t);
		}

		/// Commit and saves the document
		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				DocumentDocument documentDocument = new DocumentDocument(ctDocument);
				documentDocument.Save(stream);
			}
		}

		/// Gets the index of the relation we're trying to create
		/// @param relation
		/// @return i
		private int GetRelationIndex(XWPFRelation relation)
		{
			List<POIXMLDocumentPart> relations = GetRelations();
			int num = 1;
			IEnumerator<POIXMLDocumentPart> enumerator = relations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				POIXMLDocumentPart current = enumerator.Current;
				if (current.GetPackageRelationship().RelationshipType.Equals(relation.Relation))
				{
					num++;
				}
			}
			return num;
		}

		/// Appends a new paragraph to this document
		/// @return a new paragraph
		public XWPFParagraph CreateParagraph()
		{
			XWPFParagraph xWPFParagraph = new XWPFParagraph(ctDocument.body.AddNewP(), this);
			bodyElements.Add(xWPFParagraph);
			paragraphs.Add(xWPFParagraph);
			return xWPFParagraph;
		}

		/// Creates an empty numbering if one does not already exist and Sets the numbering member
		/// @return numbering
		public XWPFNumbering CreateNumbering()
		{
			if (numbering == null)
			{
				NumberingDocument numberingDocument = new NumberingDocument();
				XWPFRelation nUMBERING = XWPFRelation.NUMBERING;
				int relationIndex = GetRelationIndex(nUMBERING);
				XWPFNumbering xWPFNumbering = (XWPFNumbering)CreateRelationship(nUMBERING, XWPFFactory.GetInstance(), relationIndex);
				xWPFNumbering.SetNumbering(numberingDocument.Numbering);
				numbering = xWPFNumbering;
			}
			return numbering;
		}

		/// Creates an empty styles for the document if one does not already exist
		/// @return styles
		public XWPFStyles CreateStyles()
		{
			if (styles == null)
			{
				StylesDocument stylesDocument = new StylesDocument();
				XWPFRelation sTYLES = XWPFRelation.STYLES;
				int relationIndex = GetRelationIndex(sTYLES);
				XWPFStyles xWPFStyles = (XWPFStyles)CreateRelationship(sTYLES, XWPFFactory.GetInstance(), relationIndex);
				xWPFStyles.SetStyles(stylesDocument.Styles);
				styles = xWPFStyles;
			}
			return styles;
		}

		/// Creates an empty footnotes element for the document if one does not already exist
		/// @return footnotes
		public XWPFFootnotes CreateFootnotes()
		{
			if (footnotes == null)
			{
				FootnotesDocument footnotesDocument = new FootnotesDocument();
				XWPFRelation fOOTNOTE = XWPFRelation.FOOTNOTE;
				int relationIndex = GetRelationIndex(fOOTNOTE);
				XWPFFootnotes xWPFFootnotes = (XWPFFootnotes)CreateRelationship(fOOTNOTE, XWPFFactory.GetInstance(), relationIndex);
				xWPFFootnotes.SetFootnotes(footnotesDocument.Footnotes);
				footnotes = xWPFFootnotes;
			}
			return footnotes;
		}

		public XWPFFootnote AddFootnote(CT_FtnEdn note)
		{
			return footnotes.AddFootnote(note);
		}

		public XWPFFootnote AddEndnote(CT_FtnEdn note)
		{
			XWPFFootnote xWPFFootnote = new XWPFFootnote(this, note);
			endnotes.Add(int.Parse(note.id), xWPFFootnote);
			return xWPFFootnote;
		}

		/// remove a BodyElement from bodyElements array list 
		/// @param pos
		/// @return true if removing was successfully, else return false
		public bool RemoveBodyElement(int pos)
		{
			if (pos >= 0 && pos < bodyElements.Count)
			{
				BodyElementType elementType = bodyElements[pos].ElementType;
				if (elementType == BodyElementType.TABLE)
				{
					int tablePos = GetTablePos(pos);
					tables.RemoveAt(tablePos);
					ctDocument.body.RemoveTbl(tablePos);
				}
				if (elementType == BodyElementType.PARAGRAPH)
				{
					int paragraphPos = GetParagraphPos(pos);
					paragraphs.RemoveAt(paragraphPos);
					ctDocument.body.RemoveP(paragraphPos);
				}
				bodyElements.RemoveAt(pos);
				return true;
			}
			return false;
		}

		/// copies content of a paragraph to a existing paragraph in the list paragraphs at position pos
		/// @param paragraph
		/// @param pos
		public void SetParagraph(XWPFParagraph paragraph, int pos)
		{
			paragraphs[pos] = paragraph;
			ctDocument.body.SetPArray(pos, paragraph.GetCTP());
		}

		/// @return the LastParagraph of the document
		public XWPFParagraph GetLastParagraph()
		{
			int index = paragraphs.ToArray().Length - 1;
			return paragraphs[index];
		}

		/// Create an empty table with one row and one column as default.
		/// @return a new table
		public XWPFTable CreateTable()
		{
			XWPFTable xWPFTable = new XWPFTable(ctDocument.body.AddNewTbl(), this);
			bodyElements.Add(xWPFTable);
			tables.Add(xWPFTable);
			return xWPFTable;
		}

		/// Create an empty table with a number of rows and cols specified
		/// @param rows
		/// @param cols
		/// @return table
		public XWPFTable CreateTable(int rows, int cols)
		{
			XWPFTable xWPFTable = new XWPFTable(ctDocument.body.AddNewTbl(), this, rows, cols);
			bodyElements.Add(xWPFTable);
			tables.Add(xWPFTable);
			return xWPFTable;
		}

		public void CreateTOC()
		{
			CT_SdtBlock block = Document.body.AddNewSdt();
			TOC tOC = new TOC(block);
			foreach (XWPFParagraph paragraph in paragraphs)
			{
				string style = paragraph.Style;
				if (style != null && style.Substring(0, 7).Equals("Heading"))
				{
					try
					{
						int level = int.Parse(style.Substring("Heading".Length));
						tOC.AddRow(level, paragraph.Text, 1, "112723803");
					}
					catch (FormatException)
					{
					}
				}
			}
		}

		/// Replace content of table in array tables at position pos with a
		/// @param pos
		/// @param table
		public void SetTable(int pos, XWPFTable table)
		{
			tables[pos] = table;
			ctDocument.body.SetTblArray(pos, table.GetCTTbl());
		}

		/// Verifies that the documentProtection tag in Settings.xml file <br />
		/// specifies that the protection is enforced (w:enforcement="1") <br />
		/// and that the kind of protection is ReadOnly (w:edit="readOnly")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="readOnly" w:enforcement="1"/&gt;
		/// </pre>
		///
		/// @return true if documentProtection is enforced with option ReadOnly
		public bool IsEnforcedReadonlyProtection()
		{
			return Settings.IsEnforcedWith(ST_DocProtect.readOnly);
		}

		/// Verifies that the documentProtection tag in Settings.xml file <br />
		/// specifies that the protection is enforced (w:enforcement="1") <br />
		/// and that the kind of protection is forms (w:edit="forms")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="forms" w:enforcement="1"/&gt;
		/// </pre>
		///
		/// @return true if documentProtection is enforced with option forms
		public bool IsEnforcedFillingFormsProtection()
		{
			return Settings.IsEnforcedWith(ST_DocProtect.forms);
		}

		/// Verifies that the documentProtection tag in Settings.xml file <br />
		/// specifies that the protection is enforced (w:enforcement="1") <br />
		/// and that the kind of protection is comments (w:edit="comments")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="comments" w:enforcement="1"/&gt;
		/// </pre>
		///
		/// @return true if documentProtection is enforced with option comments
		public bool IsEnforcedCommentsProtection()
		{
			return Settings.IsEnforcedWith(ST_DocProtect.comments);
		}

		/// Verifies that the documentProtection tag in Settings.xml file <br />
		/// specifies that the protection is enforced (w:enforcement="1") <br />
		/// and that the kind of protection is trackedChanges (w:edit="trackedChanges")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="trackedChanges" w:enforcement="1"/&gt;
		/// </pre>
		///
		/// @return true if documentProtection is enforced with option trackedChanges
		public bool IsEnforcedTrackedChangesProtection()
		{
			return Settings.IsEnforcedWith(ST_DocProtect.trackedChanges);
		}

		public bool IsEnforcedUpdateFields()
		{
			return Settings.IsUpdateFields();
		}

		/// Enforces the ReadOnly protection.<br />
		/// In the documentProtection tag inside Settings.xml file, <br />
		/// it Sets the value of enforcement to "1" (w:enforcement="1") <br />
		/// and the value of edit to ReadOnly (w:edit="readOnly")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="readOnly" w:enforcement="1"/&gt;
		/// </pre>
		public void EnforceReadonlyProtection()
		{
			Settings.SetEnforcementEditValue(ST_DocProtect.readOnly);
		}

		/// Enforce the Filling Forms protection.<br />
		/// In the documentProtection tag inside Settings.xml file, <br />
		/// it Sets the value of enforcement to "1" (w:enforcement="1") <br />
		/// and the value of edit to forms (w:edit="forms")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="forms" w:enforcement="1"/&gt;
		/// </pre>
		public void EnforceFillingFormsProtection()
		{
			Settings.SetEnforcementEditValue(ST_DocProtect.forms);
		}

		/// Enforce the Comments protection.<br />
		/// In the documentProtection tag inside Settings.xml file,<br />
		/// it Sets the value of enforcement to "1" (w:enforcement="1") <br />
		/// and the value of edit to comments (w:edit="comments")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="comments" w:enforcement="1"/&gt;
		/// </pre>
		public void EnforceCommentsProtection()
		{
			Settings.SetEnforcementEditValue(ST_DocProtect.comments);
		}

		/// Enforce the Tracked Changes protection.<br />
		/// In the documentProtection tag inside Settings.xml file, <br />
		/// it Sets the value of enforcement to "1" (w:enforcement="1") <br />
		/// and the value of edit to trackedChanges (w:edit="trackedChanges")<br />
		/// <br />
		/// sample snippet from Settings.xml
		/// <pre>
		///     &lt;w:settings  ... &gt;
		///         &lt;w:documentProtection w:edit="trackedChanges" w:enforcement="1"/&gt;
		/// </pre>
		public void EnforceTrackedChangesProtection()
		{
			Settings.SetEnforcementEditValue(ST_DocProtect.trackedChanges);
		}

		/// Remove protection enforcement.<br />
		/// In the documentProtection tag inside Settings.xml file <br />
		/// it Sets the value of enforcement to "0" (w:enforcement="0") <br />
		public void RemoveProtectionEnforcement()
		{
			Settings.RemoveEnforcement();
		}

		/// Enforces fields update on document open (in Word).
		/// In the settings.xml file <br />
		/// sets the updateSettings value to true (w:updateSettings w:val="true")
		///
		///  NOTICES:
		///  <ul>
		///  	<li>Causing Word to ask on open: "This document contains fields that may refer to other files. Do you want to update the fields in this document?"
		///           (if "Update automatic links at open" is enabled)</li>
		///  	<li>Flag is removed after saving with changes in Word </li>
		///  </ul> 
		public void EnforceUpdateFields()
		{
			Settings.SetUpdateFields();
		}

		/// inserts an existing XWPFTable to the arrays bodyElements and tables
		/// @param pos
		/// @param table
		public void InsertTable(int pos, XWPFTable table)
		{
			bodyElements.Insert(pos, table);
			CT_Tbl[] tblArray = ctDocument.body.GetTblArray();
			int i;
			for (i = 0; i < tblArray.Length && tblArray[i] != table.GetCTTbl(); i++)
			{
			}
			tables.Insert(i, table);
		}

		public void RegisterPackagePictureData(XWPFPictureData picData)
		{
			List<XWPFPictureData> list = null;
			if (packagePictures.ContainsKey(picData.Checksum))
			{
				list = packagePictures[picData.Checksum];
			}
			if (list == null)
			{
				list = new List<XWPFPictureData>(1);
				packagePictures.Add(picData.Checksum, list);
			}
			if (!list.Contains(picData))
			{
				list.Add(picData);
			}
		}

		public XWPFPictureData FindPackagePictureData(byte[] pictureData, int format)
		{
			long key = IOUtils.CalculateChecksum(pictureData);
			XWPFPictureData xWPFPictureData = null;
			List<XWPFPictureData> list = null;
			if (packagePictures.ContainsKey(key))
			{
				list = packagePictures[key];
			}
			if (list != null)
			{
				IEnumerator<XWPFPictureData> enumerator = list.GetEnumerator();
				while (enumerator.MoveNext() && xWPFPictureData == null)
				{
					XWPFPictureData current = enumerator.Current;
					if (Arrays.Equals(pictureData, current.Data))
					{
						xWPFPictureData = current;
					}
				}
			}
			return xWPFPictureData;
		}

		public string AddPictureData(byte[] pictureData, int format)
		{
			XWPFPictureData xWPFPictureData = FindPackagePictureData(pictureData, format);
			POIXMLRelation pOIXMLRelation = XWPFPictureData.RELATIONS[format];
			if (xWPFPictureData == null)
			{
				int nextPicNameNumber = GetNextPicNameNumber(format);
				xWPFPictureData = (XWPFPictureData)CreateRelationship(pOIXMLRelation, XWPFFactory.GetInstance(), nextPicNameNumber);
				PackagePart packagePart = xWPFPictureData.GetPackagePart();
				Stream stream = null;
				try
				{
					stream = packagePart.GetOutputStream();
					stream.Write(pictureData, 0, pictureData.Length);
				}
				catch (IOException ex)
				{
					throw new POIXMLException(ex);
				}
				finally
				{
					try
					{
						stream.Close();
					}
					catch (IOException)
					{
					}
				}
				RegisterPackagePictureData(xWPFPictureData);
				pictures.Add(xWPFPictureData);
				return GetRelationId(xWPFPictureData);
			}
			if (!GetRelations().Contains(xWPFPictureData))
			{
				PackagePart packagePart2 = xWPFPictureData.GetPackagePart();
				TargetMode targetMode = TargetMode.Internal;
				PackagePartName partName = packagePart2.PartName;
				string relation = pOIXMLRelation.Relation;
				PackageRelationship packageRelationship = GetPackagePart().AddRelationship(partName, targetMode, relation);
				string id = packageRelationship.Id;
				AddRelation(id, xWPFPictureData);
				pictures.Add(xWPFPictureData);
				return id;
			}
			return GetRelationId(xWPFPictureData);
		}

		public string AddPictureData(Stream is1, int format)
		{
			try
			{
				byte[] pictureData = IOUtils.ToByteArray(is1);
				return AddPictureData(pictureData, format);
			}
			catch (IOException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Get the next free ImageNumber
		/// @param format
		/// @return the next free ImageNumber
		/// @throws InvalidFormatException 
		public int GetNextPicNameNumber(int format)
		{
			int num = AllPackagePictures.Count + 1;
			string fileName = XWPFPictureData.RELATIONS[format].GetFileName(num);
			PackagePartName partName = PackagingUriHelper.CreatePartName(fileName);
			while (base.Package.GetPart(partName) != null)
			{
				num++;
				fileName = XWPFPictureData.RELATIONS[format].GetFileName(num);
				partName = PackagingUriHelper.CreatePartName(fileName);
			}
			return num;
		}

		/// returns the PictureData by blipID
		/// @param blipID
		/// @return XWPFPictureData of a specificID
		public XWPFPictureData GetPictureDataByID(string blipID)
		{
			POIXMLDocumentPart relationById = GetRelationById(blipID);
			if (relationById is XWPFPictureData)
			{
				return (XWPFPictureData)relationById;
			}
			return null;
		}

		/// GetNumbering
		/// @return numbering
		public XWPFNumbering GetNumbering()
		{
			if (numbering == null)
			{
				numbering = new XWPFNumbering();
			}
			return numbering;
		}

		/// Get Styles
		/// @return styles for this document
		public XWPFStyles GetStyles()
		{
			return styles;
		}

		/// Get the paragraph with the CTP class p
		///
		/// @param p
		/// @return the paragraph with the CTP class p
		public XWPFParagraph GetParagraph(CT_P p)
		{
			for (int i = 0; i < Paragraphs.Count; i++)
			{
				if (Paragraphs[i].GetCTP() == p)
				{
					return Paragraphs[i];
				}
			}
			return null;
		}

		/// Get a table by its CTTbl-Object
		/// @param ctTbl
		/// @see NPOI.XWPF.UserModel.IBody#getTable(org.Openxmlformats.schemas.wordProcessingml.x2006.main.CTTbl)
		/// @return a table by its CTTbl-Object or null
		public XWPFTable GetTable(CT_Tbl ctTbl)
		{
			for (int i = 0; i < tables.Count; i++)
			{
				if (tables[i].GetCTTbl() == ctTbl)
				{
					return tables[i];
				}
			}
			return null;
		}

		public IEnumerator<XWPFTable> GetTablesEnumerator()
		{
			return tables.GetEnumerator();
		}

		public IEnumerator<XWPFParagraph> GetParagraphsEnumerator()
		{
			return paragraphs.GetEnumerator();
		}

		/// Returns the paragraph that of position pos
		/// @see NPOI.XWPF.UserModel.IBody#getParagraphArray(int)
		public XWPFParagraph GetParagraphArray(int pos)
		{
			if (pos >= 0 && pos < paragraphs.Count)
			{
				return paragraphs[pos];
			}
			return null;
		}

		/// returns the Part, to which the body belongs, which you need for Adding relationship to other parts
		/// Actually it is needed of the class XWPFTableCell. Because you have to know to which part the tableCell
		/// belongs.
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			return this;
		}

		/// Get the TableCell which belongs to the TableCell
		/// @param cell
		public XWPFTableCell GetTableCell(CT_Tc cell)
		{
			if (cell == null || !(cell.Parent is CT_Row))
			{
				return null;
			}
			object parent = ((CT_Row)cell.Parent).Parent;
			if (parent == null || !(parent is CT_Tbl))
			{
				return null;
			}
			XWPFTable table = GetTable((CT_Tbl)parent);
			if (table == null)
			{
				return null;
			}
			XWPFTableRow row = table.GetRow((CT_Row)cell.Parent);
			if (row == null)
			{
				return null;
			}
			return row.GetTableCell(cell);
		}

		public XWPFDocument GetXWPFDocument()
		{
			return this;
		}
	}
}
