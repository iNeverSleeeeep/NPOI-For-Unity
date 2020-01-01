using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// Looks After the collection of Footnotes for a document
	///
	/// @author Mike McEuen (mceuen@hp.com)
	public class XWPFFootnotes : POIXMLDocumentPart
	{
		private List<XWPFFootnote> listFootnote = new List<XWPFFootnote>();

		private CT_Footnotes ctFootnotes;

		protected XWPFDocument document;

		/// Construct XWPFFootnotes from a package part
		///
		/// @param part the package part holding the data of the footnotes,
		/// @param rel  the package relationship of type "http://schemas.Openxmlformats.org/officeDocument/2006/relationships/footnotes"
		public XWPFFootnotes(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		/// Construct XWPFFootnotes from scratch for a new document.
		public XWPFFootnotes()
		{
		}

		/// Read document
		internal override void OnDocumentRead()
		{
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				FootnotesDocument footnotesDocument = FootnotesDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				ctFootnotes = footnotesDocument.Footnotes;
			}
			catch (XmlException)
			{
				throw new POIXMLException();
			}
			if (ctFootnotes.footnote != null)
			{
				foreach (CT_FtnEdn item in ctFootnotes.footnote)
				{
					listFootnote.Add(new XWPFFootnote(item, this));
				}
			}
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				FootnotesDocument footnotesDocument = new FootnotesDocument(ctFootnotes);
				footnotesDocument.Save(stream);
			}
		}

		public List<XWPFFootnote> GetFootnotesList()
		{
			return listFootnote;
		}

		public XWPFFootnote GetFootnoteById(int id)
		{
			foreach (XWPFFootnote item in listFootnote)
			{
				if (item.GetCTFtnEdn().id == id.ToString())
				{
					return item;
				}
			}
			return null;
		}

		/// Sets the ctFootnotes
		/// @param footnotes
		public void SetFootnotes(CT_Footnotes footnotes)
		{
			ctFootnotes = footnotes;
		}

		/// add an XWPFFootnote to the document
		/// @param footnote
		/// @throws IOException		 
		public void AddFootnote(XWPFFootnote footnote)
		{
			listFootnote.Add(footnote);
			ctFootnotes.AddNewFootnote().Set(footnote.GetCTFtnEdn());
		}

		/// add a footnote to the document
		/// @param note
		/// @throws IOException		 
		public XWPFFootnote AddFootnote(CT_FtnEdn note)
		{
			CT_FtnEdn cT_FtnEdn = ctFootnotes.AddNewFootnote();
			cT_FtnEdn.Set(note);
			XWPFFootnote xWPFFootnote = new XWPFFootnote(cT_FtnEdn, this);
			listFootnote.Add(xWPFFootnote);
			return xWPFFootnote;
		}

		public void SetXWPFDocument(XWPFDocument doc)
		{
			document = doc;
		}

		/// @see NPOI.XWPF.UserModel.IBody#getPart()
		public XWPFDocument GetXWPFDocument()
		{
			if (document != null)
			{
				return document;
			}
			return (XWPFDocument)GetParent();
		}
	}
}
