using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class HdrDocument
	{
		private CT_Hdr hdr;

		public CT_Hdr Hdr
		{
			get
			{
				return hdr;
			}
		}

		public HdrDocument()
		{
			hdr = new CT_Hdr();
		}

		public static HdrDocument Parse(XmlDocument doc, XmlNamespaceManager namespaceMgr)
		{
			CT_Hdr cT_Hdr = CT_Hdr.Parse(doc.DocumentElement, namespaceMgr);
			return new HdrDocument(cT_Hdr);
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				hdr.Write(sw);
			}
		}

		public HdrDocument(CT_Hdr hdr)
		{
			this.hdr = hdr;
		}

		public void SetHdr(CT_Hdr hdr)
		{
			this.hdr = hdr;
		}
	}
}
