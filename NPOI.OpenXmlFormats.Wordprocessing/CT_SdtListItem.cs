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
	public class CT_SdtListItem
	{
		private string displayTextField;

		private string valueField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string displayText
		{
			get
			{
				return displayTextField;
			}
			set
			{
				displayTextField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string value
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

		public static CT_SdtListItem Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtListItem cT_SdtListItem = new CT_SdtListItem();
			cT_SdtListItem.displayText = XmlHelper.ReadString(node.Attributes["w:displayText"]);
			cT_SdtListItem.value = XmlHelper.ReadString(node.Attributes["w:value"]);
			return cT_SdtListItem;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:displayText", displayText);
			XmlHelper.WriteAttribute(sw, "w:value", value);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
