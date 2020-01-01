using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SdtDropDownList
	{
		private List<CT_SdtListItem> listItemField;

		private string lastValueField;

		[XmlElement("listItem", Order = 0)]
		public List<CT_SdtListItem> listItem
		{
			get
			{
				return listItemField;
			}
			set
			{
				listItemField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string lastValue
		{
			get
			{
				return lastValueField;
			}
			set
			{
				lastValueField = value;
			}
		}

		public static CT_SdtDropDownList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtDropDownList cT_SdtDropDownList = new CT_SdtDropDownList();
			cT_SdtDropDownList.lastValue = XmlHelper.ReadString(node.Attributes["w:lastValue"]);
			cT_SdtDropDownList.listItem = new List<CT_SdtListItem>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "listItem")
				{
					cT_SdtDropDownList.listItem.Add(CT_SdtListItem.Parse(childNode, namespaceManager));
				}
			}
			return cT_SdtDropDownList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:lastValue", lastValue);
			sw.Write(">");
			if (listItem != null)
			{
				foreach (CT_SdtListItem item in listItem)
				{
					item.Write(sw, "listItem");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
