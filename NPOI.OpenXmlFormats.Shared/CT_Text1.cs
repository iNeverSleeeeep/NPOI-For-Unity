using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Text1
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

		public static CT_Text1 Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Text1 cT_Text = new CT_Text1();
			cT_Text.space = XmlHelper.ReadString(node.Attributes["m:space"]);
			cT_Text.Value = XmlHelper.ReadString(node.Attributes["m:Value"]);
			return cT_Text;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:space", space);
			XmlHelper.WriteAttribute(sw, "m:Value", Value);
			sw.Write(">");
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
