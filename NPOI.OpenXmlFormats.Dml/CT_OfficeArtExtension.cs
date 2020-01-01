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
	public class CT_OfficeArtExtension
	{
		private string anyField;

		private string uriField;

		private bool uriSpecifiedField;

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

		public static CT_OfficeArtExtension Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OfficeArtExtension cT_OfficeArtExtension = new CT_OfficeArtExtension();
			cT_OfficeArtExtension.uri = XmlHelper.ReadString(node.Attributes["uri"]);
			cT_OfficeArtExtension.Any = node.InnerXml.Replace(" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\"", "");
			return cT_OfficeArtExtension;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "uri", uri);
			sw.Write(">");
			if (!string.IsNullOrEmpty(anyField))
			{
				sw.Write(anyField);
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
