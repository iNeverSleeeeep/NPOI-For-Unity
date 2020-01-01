using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class FtrDocument
	{
		private CT_Ftr ftr;

		public CT_Ftr Ftr
		{
			get
			{
				return ftr;
			}
		}

		public FtrDocument()
		{
			ftr = new CT_Ftr();
		}

		public static FtrDocument Parse(XmlDocument doc, XmlNamespaceManager namespaceMgr)
		{
			CT_Ftr cT_Ftr = CT_Ftr.Parse(doc.DocumentElement, namespaceMgr);
			return new FtrDocument(cT_Ftr);
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				ftr.Write(sw);
			}
		}

		public FtrDocument(CT_Ftr ftr)
		{
			this.ftr = ftr;
		}

		public void SetFtr(CT_Ftr ftr)
		{
			this.ftr = ftr;
		}
	}
}
