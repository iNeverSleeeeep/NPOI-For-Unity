using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_PathShadeProperties
	{
		private CT_RelativeRect fillToRectField;

		private ST_PathShadeType pathField;

		private bool pathFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_RelativeRect fillToRect
		{
			get
			{
				return fillToRectField;
			}
			set
			{
				fillToRectField = value;
			}
		}

		[XmlAttribute]
		public ST_PathShadeType path
		{
			get
			{
				return pathField;
			}
			set
			{
				pathField = value;
			}
		}

		[XmlIgnore]
		public bool pathSpecified
		{
			get
			{
				return pathFieldSpecified;
			}
			set
			{
				pathFieldSpecified = value;
			}
		}

		public static CT_PathShadeProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PathShadeProperties cT_PathShadeProperties = new CT_PathShadeProperties();
			if (node.Attributes["path"] != null)
			{
				cT_PathShadeProperties.path = (ST_PathShadeType)Enum.Parse(typeof(ST_PathShadeType), node.Attributes["path"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fillToRect")
				{
					cT_PathShadeProperties.fillToRect = CT_RelativeRect.Parse(childNode, namespaceManager);
				}
			}
			return cT_PathShadeProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "path", path.ToString());
			sw.Write(">");
			if (fillToRect != null)
			{
				fillToRect.Write(sw, "fillToRect");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
