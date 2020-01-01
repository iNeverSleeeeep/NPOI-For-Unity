using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_MC
	{
		private CT_MCPr mcPrField;

		[XmlElement(Order = 0)]
		public CT_MCPr mcPr
		{
			get
			{
				return mcPrField;
			}
			set
			{
				mcPrField = value;
			}
		}

		public static CT_MC Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MC cT_MC = new CT_MC();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "mcPr")
				{
					cT_MC.mcPr = CT_MCPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_MC;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (mcPr != null)
			{
				mcPr.Write(sw, "mcPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
