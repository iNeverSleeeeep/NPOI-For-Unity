using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// Sketch of XWPF header class
	public class XWPFHeader : XWPFHeaderFooter
	{
		/// <summary>
		/// Get the PartType of the body
		/// </summary>
		public override BodyType PartType
		{
			get
			{
				return BodyType.HEADER;
			}
		}

		public XWPFHeader()
		{
			headerFooter = new CT_Hdr();
			ReadHdrFtr();
		}

		public XWPFHeader(POIXMLDocumentPart parent, PackagePart part, PackageRelationship rel)
			: base(parent, part, rel)
		{
		}

		public XWPFHeader(XWPFDocument doc, CT_HdrFtr hdrFtr)
			: base(doc, hdrFtr)
		{
			foreach (object item3 in hdrFtr.Items)
			{
				if (item3 is CT_P)
				{
					XWPFParagraph item = new XWPFParagraph((CT_P)item3, this);
					paragraphs.Add(item);
				}
				if (item3 is CT_Tbl)
				{
					XWPFTable item2 = new XWPFTable((CT_Tbl)item3, this);
					tables.Add(item2);
				}
			}
		}

		/// <summary>
		/// Save and commit footer
		/// </summary>
		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				HdrDocument hdrDocument = new HdrDocument((CT_Hdr)headerFooter);
				hdrDocument.Save(stream);
			}
		}

		/// <summary>
		/// Read the document
		/// </summary>
		internal override void OnDocumentRead()
		{
			base.OnDocumentRead();
			HdrDocument hdrDocument = null;
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				hdrDocument = HdrDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				headerFooter = hdrDocument.Hdr;
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
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}
	}
}
