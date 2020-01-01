using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// Parent of XWPF headers and footers
	public abstract class XWPFHeaderFooter : POIXMLDocumentPart, IBody
	{
		protected List<XWPFParagraph> paragraphs = new List<XWPFParagraph>(1);

		protected List<XWPFTable> tables = new List<XWPFTable>(1);

		protected List<XWPFPictureData> pictures = new List<XWPFPictureData>();

		protected List<IBodyElement> bodyElements = new List<IBodyElement>(1);

		protected CT_HdrFtr headerFooter;

		protected XWPFDocument document;

		public IList<IBodyElement> BodyElements
		{
			get
			{
				return bodyElements.AsReadOnly();
			}
		}

		/// Returns the paragraph(s) that holds
		///  the text of the header or footer.
		/// Normally there is only the one paragraph, but
		///  there could be more in certain cases, or 
		///  a table.
		public IList<XWPFParagraph> Paragraphs
		{
			get
			{
				return paragraphs.AsReadOnly();
			}
		}

		/// Return the table(s) that holds the text
		///  of the header or footer, for complex cases
		///  where a paragraph isn't used.
		/// Normally there's just one paragraph, but some
		///  complex headers/footers have a table or two
		///  in Addition. 
		public IList<XWPFTable> Tables
		{
			get
			{
				return tables.AsReadOnly();
			}
		}

		/// Returns the textual content of the header/footer,
		///  by flattening out the text of its paragraph(s)
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < paragraphs.Count; i++)
				{
					if (!paragraphs[i].IsEmpty)
					{
						string text = paragraphs[i].Text;
						if (text != null && text.Length > 0)
						{
							stringBuilder.Append(text);
							stringBuilder.Append('\n');
						}
					}
				}
				IList<XWPFTable> list = Tables;
				for (int j = 0; j < list.Count; j++)
				{
					string text2 = list[j].Text;
					if (text2 != null && text2.Length > 0)
					{
						stringBuilder.Append(text2);
						stringBuilder.Append('\n');
					}
				}
				return stringBuilder.ToString();
			}
		}

		public IList<XWPFPictureData> AllPictures
		{
			get
			{
				return pictures.AsReadOnly();
			}
		}

		/// Get all Pictures in this package
		/// @return all Pictures in this package
		public IList<XWPFPictureData> AllPackagePictures
		{
			get
			{
				return document.AllPackagePictures;
			}
		}

		/// verifies that cursor is on the right position
		/// @param cursor
		public POIXMLDocumentPart Owner
		{
			get
			{
				return this;
			}
		}

		public virtual BodyType PartType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public XWPFHeaderFooter(XWPFDocument doc, CT_HdrFtr hdrFtr)
		{
			if (doc == null)
			{
				throw new NullReferenceException();
			}
			document = doc;
			headerFooter = hdrFtr;
			ReadHdrFtr();
		}

		protected XWPFHeaderFooter()
		{
		}

		public XWPFHeaderFooter(POIXMLDocumentPart parent, PackagePart part, PackageRelationship rel)
			: base(parent, part, rel)
		{
			document = (XWPFDocument)GetParent();
			if (document == null)
			{
				throw new NullReferenceException();
			}
		}

		internal override void OnDocumentRead()
		{
			foreach (POIXMLDocumentPart relation in GetRelations())
			{
				if (relation is XWPFPictureData)
				{
					XWPFPictureData xWPFPictureData = (XWPFPictureData)relation;
					pictures.Add(xWPFPictureData);
					document.RegisterPackagePictureData(xWPFPictureData);
				}
			}
		}

		public CT_HdrFtr _getHdrFtr()
		{
			return headerFooter;
		}

		/// Set a new headerFooter
		public void SetHeaderFooter(CT_HdrFtr headerFooter)
		{
			this.headerFooter = headerFooter;
			ReadHdrFtr();
		}

		/// if there is a corresponding {@link XWPFTable} of the parameter ctTable in the tableList of this header
		/// the method will return this table
		/// if there is no corresponding {@link XWPFTable} the method will return null 
		/// @param ctTable
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

		/// Returns the paragraph that holds
		///  the text of the header or footer.
		public XWPFParagraph GetParagraphArray(int pos)
		{
			return paragraphs[pos];
		}

		/// Get a List of all Paragraphs
		/// @return a list of {@link XWPFParagraph} 
		public List<XWPFParagraph> GetListParagraph()
		{
			return paragraphs;
		}

		/// Adds a picture to the document.
		///
		/// @param pictureData       The picture data
		/// @param format            The format of the picture.
		///
		/// @return the index to this picture (0 based), the Added picture can be obtained from {@link #getAllPictures()} .
		/// @throws InvalidFormatException 
		public string AddPictureData(byte[] pictureData, int format)
		{
			XWPFPictureData xWPFPictureData = document.FindPackagePictureData(pictureData, format);
			POIXMLRelation pOIXMLRelation = XWPFPictureData.RELATIONS[format];
			if (xWPFPictureData == null)
			{
				int nextPicNameNumber = document.GetNextPicNameNumber(format);
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
				document.RegisterPackagePictureData(xWPFPictureData);
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

		/// Adds a picture to the document.
		///
		/// @param is                The stream to read image from
		/// @param format            The format of the picture.
		///
		/// @return the index to this picture (0 based), the Added picture can be obtained from {@link #getAllPictures()} .
		/// @throws InvalidFormatException 
		/// @ 
		public string AddPictureData(Stream is1, int format)
		{
			byte[] pictureData = IOUtils.ToByteArray(is1);
			return AddPictureData(pictureData, format);
		}

		/// returns the PictureData by blipID
		/// @param blipID
		/// @return XWPFPictureData of a specificID
		/// @throws Exception 
		public XWPFPictureData GetPictureDataByID(string blipID)
		{
			POIXMLDocumentPart relationById = GetRelationById(blipID);
			if (relationById != null && relationById is XWPFPictureData)
			{
				return (XWPFPictureData)relationById;
			}
			return null;
		}

		/// Returns the table at position pos
		/// @see NPOI.XWPF.UserModel.IBody#getTableArray(int)
		public XWPFTable GetTableArray(int pos)
		{
			if (pos > 0 && pos < tables.Count)
			{
				return tables[pos];
			}
			return null;
		}

		/// inserts an existing XWPFTable to the arrays bodyElements and tables
		/// @param pos
		/// @param table
		public void InsertTable(int pos, XWPFTable table)
		{
			bodyElements.Insert(pos, table);
			int i;
			for (i = 0; i < headerFooter.GetTblList().Count; i++)
			{
				CT_Tbl tblArray = headerFooter.GetTblArray(i);
				if (tblArray == table.GetCTTbl())
				{
					break;
				}
			}
			tables.Insert(i, table);
		}

		public void ReadHdrFtr()
		{
			bodyElements = new List<IBodyElement>();
			paragraphs = new List<XWPFParagraph>();
			tables = new List<XWPFTable>();
			foreach (object item3 in headerFooter.Items)
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

		/// Get the TableCell which belongs to the TableCell
		/// @param cell
		public XWPFTableCell GetTableCell(CT_Tc cell)
		{
			throw new NotImplementedException();
		}

		public void SetXWPFDocument(XWPFDocument doc)
		{
			document = doc;
		}

		public XWPFDocument GetXWPFDocument()
		{
			if (document != null)
			{
				return document;
			}
			return (XWPFDocument)GetParent();
		}

		/// returns the Part, to which the body belongs, which you need for Adding relationship to other parts
		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public POIXMLDocumentPart GetPart()
		{
			return this;
		}

		public XWPFParagraph InsertNewParagraph(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}

		public XWPFTable InsertNewTbl(XmlDocument cursor)
		{
			throw new NotImplementedException();
		}
	}
}
