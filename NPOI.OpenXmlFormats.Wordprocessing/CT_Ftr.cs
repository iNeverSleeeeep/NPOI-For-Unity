using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[XmlRoot("ftr", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_Ftr : CT_HdrFtr
	{
		public static CT_Ftr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_Ftr ctObj = new CT_Ftr();
			return (CT_Ftr)CT_HdrFtr.Parse(ctObj, node, namespaceManager);
		}

		internal void Write(StreamWriter sw)
		{
			Write(sw, "ftr");
		}
	}
}
