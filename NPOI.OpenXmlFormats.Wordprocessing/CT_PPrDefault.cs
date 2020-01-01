using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_PPrDefault
	{
		private CT_PPr pPrField;

		[XmlElement(Order = 0)]
		public CT_PPr pPr
		{
			get
			{
				return pPrField;
			}
			set
			{
				pPrField = value;
			}
		}

		public static CT_PPrDefault Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PPrDefault cT_PPrDefault = new CT_PPrDefault();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pPr")
				{
					cT_PPrDefault.pPr = CT_PPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_PPrDefault;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (pPr != null)
			{
				pPr.Write(sw, "pPr");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
