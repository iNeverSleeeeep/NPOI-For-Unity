using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_StretchInfoProperties
	{
		private CT_RelativeRect fillRectField;

		[XmlElement(Order = 0)]
		public CT_RelativeRect fillRect
		{
			get
			{
				return fillRectField;
			}
			set
			{
				fillRectField = value;
			}
		}

		public static CT_StretchInfoProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StretchInfoProperties cT_StretchInfoProperties = new CT_StretchInfoProperties();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fillRect")
				{
					cT_StretchInfoProperties.fillRect = CT_RelativeRect.Parse(childNode, namespaceManager);
				}
			}
			return cT_StretchInfoProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (fillRect != null)
			{
				fillRect.Write(sw, "fillRect");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_RelativeRect AddNewFillRect()
		{
			fillRectField = new CT_RelativeRect();
			return fillRectField;
		}
	}
}
