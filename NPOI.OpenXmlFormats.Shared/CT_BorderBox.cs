using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_BorderBox
	{
		private CT_BorderBoxPr borderBoxPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_BorderBoxPr borderBoxPr
		{
			get
			{
				return borderBoxPrField;
			}
			set
			{
				borderBoxPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OMathArg e
		{
			get
			{
				return eField;
			}
			set
			{
				eField = value;
			}
		}

		public static CT_BorderBox Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BorderBox cT_BorderBox = new CT_BorderBox();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "borderBoxPr")
				{
					cT_BorderBox.borderBoxPr = CT_BorderBoxPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_BorderBox.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_BorderBox;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (borderBoxPr != null)
			{
				borderBoxPr.Write(sw, "borderBoxPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
