using NPOI.OpenXmlFormats.Dml;
using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class ThemeDocument
	{
		private CT_OfficeStyleSheet stylesheet;

		public ThemeDocument()
		{
		}

		public ThemeDocument(CT_OfficeStyleSheet stylesheet)
		{
			this.stylesheet = stylesheet;
		}

		public CT_OfficeStyleSheet GetTheme()
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

		public static ThemeDocument Parse(XmlDocument xmldoc, XmlNamespaceManager namespaceManager)
		{
			CT_OfficeStyleSheet cT_OfficeStyleSheet = CT_OfficeStyleSheet.Parse(xmldoc.DocumentElement, namespaceManager);
			return new ThemeDocument(cT_OfficeStyleSheet);
		}
	}
}
