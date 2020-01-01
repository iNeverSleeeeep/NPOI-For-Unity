using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Hyperlinks
	{
		private List<CT_Hyperlink> hyperlinkField = new List<CT_Hyperlink>();

		[XmlElement("hyperlink", IsNullable = false)]
		public List<CT_Hyperlink> hyperlink
		{
			get
			{
				return hyperlinkField;
			}
			set
			{
				hyperlinkField = value;
			}
		}

		public static CT_Hyperlinks Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Hyperlinks cT_Hyperlinks = new CT_Hyperlinks();
			cT_Hyperlinks.hyperlink = new List<CT_Hyperlink>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "hyperlink")
				{
					cT_Hyperlinks.hyperlink.Add(CT_Hyperlink.Parse(childNode, namespaceManager));
				}
			}
			return cT_Hyperlinks;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (hyperlink != null)
			{
				foreach (CT_Hyperlink item in hyperlink)
				{
					item.Write(sw, "hyperlink");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public void SetHyperlinkArray(CT_Hyperlink[] array)
		{
			hyperlinkField = new List<CT_Hyperlink>(array);
		}
	}
}
