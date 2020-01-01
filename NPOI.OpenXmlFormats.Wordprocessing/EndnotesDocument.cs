using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class EndnotesDocument
	{
		private CT_Endnotes endnotes;

		public CT_Endnotes Endnotes
		{
			get
			{
				return endnotes;
			}
		}

		public EndnotesDocument()
		{
		}

		public static EndnotesDocument Parse(XmlDocument doc, XmlNamespaceManager namespaceMgr)
		{
			CT_Endnotes cT_Endnotes = CT_Endnotes.Parse(doc.DocumentElement, namespaceMgr);
			return new EndnotesDocument(cT_Endnotes);
		}

		public EndnotesDocument(CT_Endnotes endnotes)
		{
			this.endnotes = endnotes;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				endnotes.Write(sw);
			}
		}
	}
}
