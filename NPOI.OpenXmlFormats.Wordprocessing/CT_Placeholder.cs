using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Placeholder
	{
		private CT_String docPartField;

		[XmlElement(Order = 0)]
		public CT_String docPart
		{
			get
			{
				return docPartField;
			}
			set
			{
				docPartField = value;
			}
		}

		public static CT_Placeholder Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Placeholder cT_Placeholder = new CT_Placeholder();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "docPart")
				{
					cT_Placeholder.docPart = CT_String.Parse(childNode, namespaceManager);
				}
			}
			return cT_Placeholder;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (docPart != null)
			{
				docPart.Write(sw, "docPart");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
