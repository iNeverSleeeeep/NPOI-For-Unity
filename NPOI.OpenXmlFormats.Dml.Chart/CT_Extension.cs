using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Extension
	{
		private string anyField;

		private string uriField;

		[XmlText]
		public string Any
		{
			get
			{
				return anyField;
			}
			set
			{
				anyField = value;
			}
		}

		[XmlAttribute(DataType = "token")]
		public string uri
		{
			get
			{
				return uriField;
			}
			set
			{
				uriField = value;
			}
		}

		public static CT_Extension Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Extension cT_Extension = new CT_Extension();
			cT_Extension.uri = XmlHelper.ReadString(node.Attributes["uri"]);
			cT_Extension.Any = node.InnerXml;
			return cT_Extension;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "uri", uri);
			sw.Write(">");
			if (Any != null)
			{
				sw.Write(Any);
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
