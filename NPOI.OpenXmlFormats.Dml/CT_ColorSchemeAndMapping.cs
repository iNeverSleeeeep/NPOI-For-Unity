using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ColorSchemeAndMapping
	{
		private CT_ColorScheme clrSchemeField;

		private CT_ColorMapping clrMapField;

		[XmlElement(Order = 0)]
		public CT_ColorScheme clrScheme
		{
			get
			{
				return clrSchemeField;
			}
			set
			{
				clrSchemeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ColorMapping clrMap
		{
			get
			{
				return clrMapField;
			}
			set
			{
				clrMapField = value;
			}
		}

		public static CT_ColorSchemeAndMapping Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ColorSchemeAndMapping cT_ColorSchemeAndMapping = new CT_ColorSchemeAndMapping();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "clrScheme")
				{
					cT_ColorSchemeAndMapping.clrScheme = CT_ColorScheme.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clrMap")
				{
					cT_ColorSchemeAndMapping.clrMap = CT_ColorMapping.Parse(childNode, namespaceManager);
				}
			}
			return cT_ColorSchemeAndMapping;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (clrScheme != null)
			{
				clrScheme.Write(sw, "clrScheme");
			}
			if (clrMap != null)
			{
				clrMap.Write(sw, "clrMap");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
