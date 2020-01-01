using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/schemaLibrary/2006/main")]
	public class CT_Schema
	{
		private string uriField;

		private string manifestLocationField;

		private string schemaLocationField;

		[XmlAttribute]
		[DefaultValue("")]
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

		[XmlAttribute]
		public string manifestLocation
		{
			get
			{
				return manifestLocationField;
			}
			set
			{
				manifestLocationField = value;
			}
		}

		[XmlAttribute]
		public string schemaLocation
		{
			get
			{
				return schemaLocationField;
			}
			set
			{
				schemaLocationField = value;
			}
		}

		public static CT_Schema Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Schema cT_Schema = new CT_Schema();
			cT_Schema.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_Schema.manifestLocation = XmlHelper.ReadString(node.Attributes["w:manifestLocation"]);
			cT_Schema.schemaLocation = XmlHelper.ReadString(node.Attributes["w:schemaLocation"]);
			return cT_Schema;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:manifestLocation", manifestLocation);
			XmlHelper.WriteAttribute(sw, "w:schemaLocation", schemaLocation);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_Schema()
		{
			uriField = "";
		}
	}
}
