using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_MCPr
	{
		private CT_Integer255 countField;

		private CT_XAlign mcJcField;

		[XmlElement(Order = 0)]
		public CT_Integer255 count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_XAlign mcJc
		{
			get
			{
				return mcJcField;
			}
			set
			{
				mcJcField = value;
			}
		}

		public static CT_MCPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MCPr cT_MCPr = new CT_MCPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "count")
				{
					cT_MCPr.count = CT_Integer255.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mcJc")
				{
					cT_MCPr.mcJc = CT_XAlign.Parse(childNode, namespaceManager);
				}
			}
			return cT_MCPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (count != null)
			{
				count.Write(sw, "count");
			}
			if (mcJc != null)
			{
				mcJc.Write(sw, "mcJc");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
