using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class StyleSheetDocument
	{
		private CT_Stylesheet stylesheet;

		public StyleSheetDocument()
		{
			stylesheet = new CT_Stylesheet();
		}

		public StyleSheetDocument(CT_Stylesheet stylesheet)
		{
			this.stylesheet = stylesheet;
		}

		public static StyleSheetDocument Parse(XmlDocument xmldoc, XmlNamespaceManager namespaceManager)
		{
			CT_Stylesheet cT_Stylesheet = CT_Stylesheet.Parse(xmldoc.DocumentElement, namespaceManager);
			return new StyleSheetDocument(cT_Stylesheet);
		}

		public void AddNewStyleSheet()
		{
			stylesheet = new CT_Stylesheet();
		}

		public CT_Stylesheet GetStyleSheet()
		{
			return stylesheet;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				stylesheet.Write(sw);
			}
		}
	}
}
