using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class StylesDocument
	{
		private CT_Styles styles;

		public CT_Styles Styles
		{
			get
			{
				return styles;
			}
		}

		public StylesDocument()
		{
			styles = new CT_Styles();
		}

		public static StylesDocument Parse(XmlDocument doc, XmlNamespaceManager namespaceMgr)
		{
			CT_Styles cT_Styles = CT_Styles.Parse(doc.DocumentElement, namespaceMgr);
			return new StylesDocument(cT_Styles);
		}

		public StylesDocument(CT_Styles styles)
		{
			this.styles = styles;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				styles.Write(sw);
			}
		}
	}
}
