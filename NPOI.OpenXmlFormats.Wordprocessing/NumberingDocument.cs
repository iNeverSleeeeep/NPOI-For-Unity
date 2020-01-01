using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class NumberingDocument
	{
		private CT_Numbering numbering;

		public CT_Numbering Numbering
		{
			get
			{
				return numbering;
			}
		}

		public NumberingDocument()
		{
			numbering = new CT_Numbering();
		}

		public NumberingDocument(CT_Numbering numbering)
		{
			this.numbering = numbering;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				numbering.Write(sw);
			}
		}

		public static NumberingDocument Parse(XmlDocument doc, XmlNamespaceManager NameSpaceManager)
		{
			CT_Numbering cT_Numbering = CT_Numbering.Parse(doc.DocumentElement, NameSpaceManager);
			return new NumberingDocument(cT_Numbering);
		}
	}
}
