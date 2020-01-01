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
	public class CT_Kinsoku
	{
		private string langField;

		private string valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string lang
		{
			get
			{
				return langField;
			}
			set
			{
				langField = value;
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

		public static CT_Kinsoku Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Kinsoku cT_Kinsoku = new CT_Kinsoku();
			cT_Kinsoku.lang = XmlHelper.ReadString(node.Attributes["w:lang"]);
			cT_Kinsoku.val = XmlHelper.ReadString(node.Attributes["w:val"]);
			return cT_Kinsoku;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:lang", lang);
			XmlHelper.WriteAttribute(sw, "w:val", val);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
