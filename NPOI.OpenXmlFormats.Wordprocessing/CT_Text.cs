using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Text
	{
		private string spaceField;

		private string valueField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
		public string space
		{
			get
			{
				return spaceField;
			}
			set
			{
				spaceField = value;
			}
		}

		[XmlText]
		public string Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}

		public static CT_Text Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Text cT_Text = new CT_Text();
			cT_Text.space = XmlHelper.ReadString(node.Attributes["xml:space"]);
			cT_Text.Value = node.InnerText;
			return cT_Text;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			space = "preserve";
			XmlHelper.WriteAttribute(sw, "xml:space", space);
			sw.Write(">");
			sw.Write(XmlHelper.EncodeXml(valueField));
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
