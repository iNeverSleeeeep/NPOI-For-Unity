using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.IO;
using System.Xml;

namespace NPOI.XWPF.UserModel
{
	/// Sketch of XWPF footer class
	public class XWPFFooter : XWPFHeaderFooter
	{
		/// Get the PartType of the body
		/// @see NPOI.XWPF.UserModel.IBody#getPartType()
		public override BodyType PartType
		{
			get
			{
				return BodyType.FOOTER;
			}
		}

		public XWPFFooter()
		{
			headerFooter = new CT_Ftr();
			ReadHdrFtr();
		}

		public XWPFFooter(XWPFDocument doc, CT_HdrFtr hdrFtr)
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

		public XWPFFooter(POIXMLDocumentPart parent, PackagePart part, PackageRelationship rel)
			: base(parent, part, rel)
		{
		}

		/// save and Commit footer
		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			using (Stream stream = packagePart.GetOutputStream())
			{
				FtrDocument ftrDocument = new FtrDocument((CT_Ftr)headerFooter);
				ftrDocument.Save(stream);
			}
		}

		internal override void OnDocumentRead()
		{
			base.OnDocumentRead();
			FtrDocument ftrDocument = null;
			try
			{
				XmlDocument doc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				ftrDocument = FtrDocument.Parse(doc, POIXMLDocumentPart.NamespaceManager);
				headerFooter = ftrDocument.Ftr;
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
