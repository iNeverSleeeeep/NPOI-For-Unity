using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Attr
	{
		private string uriField;

		private string nameField;

		private string valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public static CT_Attr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Attr cT_Attr = new CT_Attr();
			cT_Attr.uri = XmlHelper.ReadString(node.Attributes["w:uri"]);
			cT_Attr.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			cT_Attr.val = XmlHelper.ReadString(node.Attributes["w:val"]);
			return cT_Attr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:uri", uri);
			XmlHelper.WriteAttribute(sw, "w:name", name);
			XmlHelper.WriteAttribute(sw, "w:val", val);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
