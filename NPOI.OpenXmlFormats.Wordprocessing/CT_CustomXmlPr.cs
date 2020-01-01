using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_CustomXmlPr
	{
		private CT_String placeholderField;

		private List<CT_Attr> attrField;

		[XmlElement(Order = 0)]
		public CT_String placeholder
		{
			get
			{
				return placeholderField;
			}
			set
			{
				placeholderField = value;
			}
		}

		[XmlElement("attr", Order = 1)]
		public List<CT_Attr> attr
		{
			get
			{
				return attrField;
			}
			set
			{
				attrField = value;
			}
		}

		public static CT_CustomXmlPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomXmlPr cT_CustomXmlPr = new CT_CustomXmlPr();
			cT_CustomXmlPr.attr = new List<CT_Attr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "placeholder")
				{
					cT_CustomXmlPr.placeholder = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "attr")
				{
					cT_CustomXmlPr.attr.Add(CT_Attr.Parse(childNode, namespaceManager));
				}
			}
			return cT_CustomXmlPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (placeholder != null)
			{
				placeholder.Write(sw, "placeholder");
			}
			if (attr != null)
			{
				foreach (CT_Attr item in attr)
				{
					item.Write(sw, "attr");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
