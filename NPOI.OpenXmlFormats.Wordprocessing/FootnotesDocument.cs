using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class FootnotesDocument
	{
		private CT_Footnotes footnotes;

		public CT_Footnotes Footnotes
		{
			get
			{
				return footnotes;
			}
		}

		public FootnotesDocument()
		{
			footnotes = new CT_Footnotes();
		}

		public static FootnotesDocument Parse(XmlDocument doc, XmlNamespaceManager namespaceMgr)
		{
			CT_Footnotes cT_Footnotes = CT_Footnotes.Parse(doc.DocumentElement, namespaceMgr);
			return new FootnotesDocument(cT_Footnotes);
		}

		public FootnotesDocument(CT_Footnotes footnotes)
		{
			this.footnotes = footnotes;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				footnotes.Write(sw);
			}
		}
	}
}
