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
	public class CT_AutoCaption
	{
		private string nameField;

		private string captionField;

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
		public string caption
		{
			get
			{
				return captionField;
			}
			set
			{
				captionField = value;
			}
		}

		public static CT_AutoCaption Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AutoCaption cT_AutoCaption = new CT_AutoCaption();
			cT_AutoCaption.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			cT_AutoCaption.caption = XmlHelper.ReadString(node.Attributes["w:caption"]);
			return cT_AutoCaption;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:name", name);
			XmlHelper.WriteAttribute(sw, "w:caption", caption);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
