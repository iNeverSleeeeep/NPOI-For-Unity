using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SmartTagType
	{
		private string namespaceUriField;

		private string nameField;

		private string urlField;

		public string namespaceUri
		{
			get
			{
				return namespaceUriField;
			}
			set
			{
				namespaceUriField = value;
			}
		}

		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public string url
		{
			get
			{
				return urlField;
			}
			set
			{
				urlField = value;
			}
		}

		public static CT_SmartTagType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SmartTagType cT_SmartTagType = new CT_SmartTagType();
			cT_SmartTagType.namespaceUri = XmlHelper.ReadString(node.Attributes["namespaceUri"]);
			cT_SmartTagType.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_SmartTagType.url = XmlHelper.ReadString(node.Attributes["url"]);
			return cT_SmartTagType;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "namespaceUri", namespaceUri);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "url", url);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
