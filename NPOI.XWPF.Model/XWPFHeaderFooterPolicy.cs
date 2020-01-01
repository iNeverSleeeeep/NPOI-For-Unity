#define TRACE
using NPOI.OpenXmlFormats.Vml;
using NPOI.OpenXmlFormats.Vml.Office;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.XWPF.Model
{
	/// A .docx file can have no headers/footers, the same header/footer
	///  on each page, odd/even page footers, and optionally also 
	///  a different header/footer on the first page.
	/// This class handles sorting out what there is, and giving you
	///  the right headers and footers for the document.
	public class XWPFHeaderFooterPolicy
	{
		public static ST_HdrFtr DEFAULT = ST_HdrFtr.@default;

		public static ST_HdrFtr EVEN = ST_HdrFtr.even;

		public static ST_HdrFtr FIRST = ST_HdrFtr.first;

		private XWPFDocument doc;

		private XWPFHeader firstPageHeader;

		private XWPFFooter firstPageFooter;

		private XWPFHeader evenPageHeader;

		private XWPFFooter evenPageFooter;

		private XWPFHeader defaultHeader;

		private XWPFFooter defaultFooter;

		/// Figures out the policy for the given document,
		///  and Creates any header and footer objects
		///  as required.
		public XWPFHeaderFooterPolicy(XWPFDocument doc)
			: this(doc, doc.Document.body.sectPr)
		{
		}

		/// Figures out the policy for the given document,
		///  and Creates any header and footer objects
		///  as required.
		public XWPFHeaderFooterPolicy(XWPFDocument doc, CT_SectPr sectPr)
		{
			this.doc = doc;
			for (int i = 0; i < sectPr.SizeOfHeaderReferenceArray(); i++)
			{
				CT_HdrFtrRef headerReferenceArray = sectPr.GetHeaderReferenceArray(i);
				POIXMLDocumentPart relationById = doc.GetRelationById(headerReferenceArray.id);
				XWPFHeader hdr = null;
				if (relationById != null && relationById is XWPFHeader)
				{
					hdr = (XWPFHeader)relationById;
				}
				ST_HdrFtr type = headerReferenceArray.type;
				assignHeader(hdr, type);
			}
			for (int j = 0; j < sectPr.SizeOfFooterReferenceArray(); j++)
			{
				CT_HdrFtrRef footerReferenceArray = sectPr.GetFooterReferenceArray(j);
				POIXMLDocumentPart relationById2 = doc.GetRelationById(footerReferenceArray.id);
				XWPFFooter ftr = null;
				if (relationById2 != null && relationById2 is XWPFFooter)
				{
					ftr = (XWPFFooter)relationById2;
				}
				ST_HdrFtr type2 = footerReferenceArray.type;
				assignFooter(ftr, type2);
			}
		}

		private void assignFooter(XWPFFooter ftr, ST_HdrFtr type)
		{
			switch (type)
			{
			case ST_HdrFtr.first:
				firstPageFooter = ftr;
				break;
			case ST_HdrFtr.even:
				evenPageFooter = ftr;
				break;
			default:
				defaultFooter = ftr;
				break;
			}
		}

		private void assignHeader(XWPFHeader hdr, ST_HdrFtr type)
		{
			switch (type)
			{
			case ST_HdrFtr.first:
				firstPageHeader = hdr;
				break;
			case ST_HdrFtr.even:
				evenPageHeader = hdr;
				break;
			default:
				defaultHeader = hdr;
				break;
			}
		}

		public XWPFHeader CreateHeader(ST_HdrFtr type)
		{
			return CreateHeader(type, null);
		}

		public XWPFHeader CreateHeader(ST_HdrFtr type, XWPFParagraph[] pars)
		{
			XWPFRelation hEADER = XWPFRelation.HEADER;
			string pStyle = "Header";
			int relationIndex = GetRelationIndex(hEADER);
			HdrDocument hdrDocument = new HdrDocument();
			XWPFHeader xWPFHeader = (XWPFHeader)doc.CreateRelationship(hEADER, XWPFFactory.GetInstance(), relationIndex);
			CT_HdrFtr cT_HdrFtr = buildHdr(type, pStyle, xWPFHeader, pars);
			xWPFHeader.SetHeaderFooter(cT_HdrFtr);
			hdrDocument.SetHdr((CT_Hdr)cT_HdrFtr);
			assignHeader(xWPFHeader, type);
			using (Stream stream = xWPFHeader.GetPackagePart().GetOutputStream())
			{
				hdrDocument.Save(stream);
				return xWPFHeader;
			}
		}

		public XWPFFooter CreateFooter(ST_HdrFtr type)
		{
			return CreateFooter(type, null);
		}

		public XWPFFooter CreateFooter(ST_HdrFtr type, XWPFParagraph[] pars)
		{
			XWPFRelation fOOTER = XWPFRelation.FOOTER;
			string pStyle = "Footer";
			int relationIndex = GetRelationIndex(fOOTER);
			FtrDocument ftrDocument = new FtrDocument();
			XWPFFooter xWPFFooter = (XWPFFooter)doc.CreateRelationship(fOOTER, XWPFFactory.GetInstance(), relationIndex);
			CT_HdrFtr cT_HdrFtr = buildFtr(type, pStyle, xWPFFooter, pars);
			xWPFFooter.SetHeaderFooter(cT_HdrFtr);
			ftrDocument.SetFtr((CT_Ftr)cT_HdrFtr);
			assignFooter(xWPFFooter, type);
			using (Stream stream = xWPFFooter.GetPackagePart().GetOutputStream())
			{
				ftrDocument.Save(stream);
				return xWPFFooter;
			}
		}

		private int GetRelationIndex(XWPFRelation relation)
		{
			List<POIXMLDocumentPart> relations = doc.GetRelations();
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

		private CT_HdrFtr buildFtr(ST_HdrFtr type, string pStyle, XWPFHeaderFooter wrapper, XWPFParagraph[] pars)
		{
			CT_HdrFtr result = buildHdrFtr(pStyle, pars, wrapper);
			SetFooterReference(type, wrapper);
			return result;
		}

		private CT_HdrFtr buildHdr(ST_HdrFtr type, string pStyle, XWPFHeaderFooter wrapper, XWPFParagraph[] pars)
		{
			CT_HdrFtr result = buildHdrFtr(pStyle, pars, wrapper);
			SetHeaderReference(type, wrapper);
			return result;
		}

		private CT_HdrFtr buildHdrFtr(string pStyle, XWPFParagraph[] paragraphs)
		{
			CT_HdrFtr cT_HdrFtr = new CT_HdrFtr();
			if (paragraphs != null)
			{
				for (int i = 0; i < paragraphs.Length; i++)
				{
					cT_HdrFtr.AddNewP();
					cT_HdrFtr.SetPArray(i, paragraphs[i].GetCTP());
				}
			}
			else
			{
				CT_P cT_P = cT_HdrFtr.AddNewP();
				byte[] rsidR = doc.Document.body.GetPArray(0).rsidR;
				byte[] rsidRDefault = doc.Document.body.GetPArray(0).rsidRDefault;
				cT_P.rsidR = rsidR;
				cT_P.rsidRDefault = rsidRDefault;
				CT_PPr cT_PPr = cT_P.AddNewPPr();
				cT_PPr.AddNewPStyle().val = pStyle;
			}
			return cT_HdrFtr;
		}

		/// MB 24 May 2010. Created this overloaded buildHdrFtr() method because testing demonstrated
		/// that the XWPFFooter or XWPFHeader object returned by calls to the CreateHeader(int, XWPFParagraph[])
		/// and CreateFooter(int, XWPFParagraph[]) methods or the GetXXXXXHeader/Footer methods where
		/// headers or footers had been Added to a document since it had been Created/opened, returned
		/// an object that Contained no XWPFParagraph objects even if the header/footer itself did contain
		/// text. The reason was that this line of code; CTHdrFtr ftr = CTHdrFtr.Factory.NewInstance(); 
		/// Created a brand new instance of the CTHDRFtr class which was then populated with data when
		/// it should have recovered the CTHdrFtr object encapsulated within the XWPFHeaderFooter object
		/// that had previoulsy been instantiated in the CreateHeader(int, XWPFParagraph[]) or 
		/// CreateFooter(int, XWPFParagraph[]) methods.
		private CT_HdrFtr buildHdrFtr(string pStyle, XWPFParagraph[] paragraphs, XWPFHeaderFooter wrapper)
		{
			CT_HdrFtr cT_HdrFtr = wrapper._getHdrFtr();
			if (paragraphs != null)
			{
				for (int i = 0; i < paragraphs.Length; i++)
				{
					cT_HdrFtr.AddNewP();
					cT_HdrFtr.SetPArray(i, paragraphs[i].GetCTP());
				}
			}
			else
			{
				CT_P cT_P = cT_HdrFtr.AddNewP();
				byte[] rsidR = doc.Document.body.GetPArray(0).rsidR;
				byte[] rsidRDefault = doc.Document.body.GetPArray(0).rsidRDefault;
				cT_P.rsidP = rsidR;
				cT_P.rsidRDefault = rsidRDefault;
				CT_PPr cT_PPr = cT_P.AddNewPPr();
				cT_PPr.AddNewPStyle().val = pStyle;
			}
			return cT_HdrFtr;
		}

		private void SetFooterReference(ST_HdrFtr type, XWPFHeaderFooter wrapper)
		{
			CT_HdrFtrRef cT_HdrFtrRef = doc.Document.body.sectPr.AddNewFooterReference();
			cT_HdrFtrRef.type = type;
			cT_HdrFtrRef.id = wrapper.GetPackageRelationship().Id;
		}

		private void SetHeaderReference(ST_HdrFtr type, XWPFHeaderFooter wrapper)
		{
			CT_HdrFtrRef cT_HdrFtrRef = doc.Document.body.sectPr.AddNewHeaderReference();
			cT_HdrFtrRef.type = type;
			cT_HdrFtrRef.id = wrapper.GetPackageRelationship().Id;
		}

		private XmlSerializerNamespaces Commit(XWPFHeaderFooter wrapper)
		{
			return new XmlSerializerNamespaces(new XmlQualifiedName[8]
			{
				new XmlQualifiedName("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006"),
				new XmlQualifiedName("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"),
				new XmlQualifiedName("m", "http://schemas.openxmlformats.org/officeDocument/2006/math"),
				new XmlQualifiedName("v", "urn:schemas-microsoft-com:vml"),
				new XmlQualifiedName("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing"),
				new XmlQualifiedName("w10", "urn:schemas-microsoft-com:office:word"),
				new XmlQualifiedName("wne", "http://schemas.microsoft.com/office/word/2006/wordml"),
				new XmlQualifiedName("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main")
			});
		}

		public XWPFHeader GetFirstPageHeader()
		{
			return firstPageHeader;
		}

		public XWPFFooter GetFirstPageFooter()
		{
			return firstPageFooter;
		}

		/// Returns the odd page header. This is
		///  also the same as the default one...
		public XWPFHeader GetOddPageHeader()
		{
			return defaultHeader;
		}

		/// Returns the odd page footer. This is
		///  also the same as the default one...
		public XWPFFooter GetOddPageFooter()
		{
			return defaultFooter;
		}

		public XWPFHeader GetEvenPageHeader()
		{
			return evenPageHeader;
		}

		public XWPFFooter GetEvenPageFooter()
		{
			return evenPageFooter;
		}

		public XWPFHeader GetDefaultHeader()
		{
			return defaultHeader;
		}

		public XWPFFooter GetDefaultFooter()
		{
			return defaultFooter;
		}

		/// Get the header that applies to the given
		///  (1 based) page.
		/// @param pageNumber The one based page number
		public XWPFHeader GetHeader(int pageNumber)
		{
			if (pageNumber == 1 && firstPageHeader != null)
			{
				return firstPageHeader;
			}
			if (pageNumber % 2 == 0 && evenPageHeader != null)
			{
				return evenPageHeader;
			}
			return defaultHeader;
		}

		/// Get the footer that applies to the given
		///  (1 based) page.
		/// @param pageNumber The one based page number
		public XWPFFooter GetFooter(int pageNumber)
		{
			if (pageNumber == 1 && firstPageFooter != null)
			{
				return firstPageFooter;
			}
			if (pageNumber % 2 == 0 && evenPageFooter != null)
			{
				return evenPageFooter;
			}
			return defaultFooter;
		}

		public void CreateWatermark(string text)
		{
			XWPFParagraph[] array = new XWPFParagraph[1];
			try
			{
				array[0] = GetWatermarkParagraph(text, 1);
				CreateHeader(DEFAULT, array);
				array[0] = GetWatermarkParagraph(text, 2);
				CreateHeader(FIRST, array);
				array[0] = GetWatermarkParagraph(text, 3);
				CreateHeader(EVEN, array);
			}
			catch (IOException ex)
			{
				Trace.Write(ex.StackTrace);
			}
		}

		private XWPFParagraph GetWatermarkParagraph(string text, int idx)
		{
			CT_P cT_P = new CT_P();
			byte[] rsidR = doc.Document.body.GetPArray(0).rsidR;
			byte[] rsidRDefault = doc.Document.body.GetPArray(0).rsidRDefault;
			cT_P.rsidP = rsidR;
			cT_P.rsidRDefault = rsidRDefault;
			CT_PPr cT_PPr = cT_P.AddNewPPr();
			cT_PPr.AddNewPStyle().val = "Header";
			NPOI.OpenXmlFormats.Wordprocessing.CT_R cT_R = cT_P.AddNewR();
			CT_RPr cT_RPr = cT_R.AddNewRPr();
			cT_RPr.AddNewNoProof();
			CT_Picture cT_Picture = cT_R.AddNewPict();
			CT_Group cT_Group = new CT_Group();
			CT_Shapetype cT_Shapetype = cT_Group.AddNewShapetype();
			cT_Shapetype.id = "_x0000_t136";
			cT_Shapetype.coordsize = "1600,21600";
			cT_Shapetype.spt = 136f;
			cT_Shapetype.adj = "10800";
			cT_Shapetype.path2 = "m@7,0l@8,0m@5,21600l@6,21600e";
			CT_Formulas cT_Formulas = cT_Shapetype.AddNewFormulas();
			cT_Formulas.AddNewF().eqn = "sum #0 0 10800";
			cT_Formulas.AddNewF().eqn = "prod #0 2 1";
			cT_Formulas.AddNewF().eqn = "sum 21600 0 @1";
			cT_Formulas.AddNewF().eqn = "sum 0 0 @2";
			cT_Formulas.AddNewF().eqn = "sum 21600 0 @3";
			cT_Formulas.AddNewF().eqn = "if @0 @3 0";
			cT_Formulas.AddNewF().eqn = "if @0 21600 @1";
			cT_Formulas.AddNewF().eqn = "if @0 0 @2";
			cT_Formulas.AddNewF().eqn = "if @0 @4 21600";
			cT_Formulas.AddNewF().eqn = "mid @5 @6";
			cT_Formulas.AddNewF().eqn = "mid @8 @5";
			cT_Formulas.AddNewF().eqn = "mid @7 @8";
			cT_Formulas.AddNewF().eqn = "mid @6 @7";
			cT_Formulas.AddNewF().eqn = "sum @6 0 @5";
			CT_Path cT_Path = cT_Shapetype.AddNewPath();
			cT_Path.textpathok = NPOI.OpenXmlFormats.Vml.ST_TrueFalse.t;
			cT_Path.connecttype = ST_ConnectType.custom;
			cT_Path.connectlocs = "@9,0;@10,10800;@11,21600;@12,10800";
			cT_Path.connectangles = "270,180,90,0";
			CT_TextPath cT_TextPath = cT_Shapetype.AddNewTextpath();
			cT_TextPath.on = NPOI.OpenXmlFormats.Vml.ST_TrueFalse.t;
			cT_TextPath.fitshape = NPOI.OpenXmlFormats.Vml.ST_TrueFalse.t;
			CT_Handles cT_Handles = cT_Shapetype.AddNewHandles();
			CT_H cT_H = cT_Handles.AddNewH();
			cT_H.position = "#0,bottomRight";
			cT_H.xrange = "6629,14971";
			NPOI.OpenXmlFormats.Vml.Office.CT_Lock cT_Lock = cT_Shapetype.AddNewLock();
			cT_Lock.ext = ST_Ext.edit;
			CT_Shape cT_Shape = cT_Group.AddNewShape();
			cT_Shape.id = "PowerPlusWaterMarkObject" + idx;
			cT_Shape.spid = "_x0000_s102" + (4 + idx);
			cT_Shape.type = "#_x0000_t136";
			cT_Shape.style = "position:absolute;margin-left:0;margin-top:0;width:415pt;height:207.5pt;z-index:-251654144;mso-wrap-edited:f;mso-position-horizontal:center;mso-position-horizontal-relative:margin;mso-position-vertical:center;mso-position-vertical-relative:margin";
			cT_Shape.wrapcoords = "616 5068 390 16297 39 16921 -39 17155 7265 17545 7186 17467 -39 17467 18904 17467 10507 17467 8710 17545 18904 17077 18787 16843 18358 16297 18279 12554 19178 12476 20701 11774 20779 11228 21131 10059 21248 8811 21248 7563 20975 6316 20935 5380 19490 5146 14022 5068 2616 5068";
			cT_Shape.fillcolor = "black";
			cT_Shape.stroked = NPOI.OpenXmlFormats.Vml.ST_TrueFalse.@false;
			CT_TextPath cT_TextPath2 = cT_Shape.AddNewTextpath();
			cT_TextPath2.style = "font-family:&quot;Cambria&quot;;font-size:1pt";
			cT_TextPath2.@string = text;
			cT_Picture.Set(cT_Group);
			return new XWPFParagraph(cT_P, doc);
		}
	}
}
