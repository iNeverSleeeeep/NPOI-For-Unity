using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_Box
	{
		private CT_BoxPr boxPrField;

		private CT_OMathArg eField;

		[XmlElement(Order = 0)]
		public CT_BoxPr boxPr
		{
			get
			{
				return boxPrField;
			}
			set
			{
				boxPrField = value;
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

		public static CT_Box Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Box cT_Box = new CT_Box();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "boxPr")
				{
					cT_Box.boxPr = CT_BoxPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "e")
				{
					cT_Box.e = CT_OMathArg.Parse(childNode, namespaceManager);
				}
			}
			return cT_Box;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (boxPr != null)
			{
				boxPr.Write(sw, "boxPr");
			}
			if (e != null)
			{
				e.Write(sw, "e");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}

		public CT_Box()
		{
			eField = new CT_OMathArg();
			boxPrField = new CT_BoxPr();
		}
	}
}
