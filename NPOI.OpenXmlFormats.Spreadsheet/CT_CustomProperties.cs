using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomProperties
	{
		private List<CT_CustomProperty> customPrField;

		public List<CT_CustomProperty> customPr
		{
			get
			{
				return customPrField;
			}
			set
			{
				customPrField = value;
			}
		}

		public static CT_CustomProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomProperties cT_CustomProperties = new CT_CustomProperties();
			cT_CustomProperties.customPr = new List<CT_CustomProperty>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customPr")
				{
					cT_CustomProperties.customPr.Add(CT_CustomProperty.Parse(childNode, namespaceManager));
				}
			}
			return cT_CustomProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (customPr != null)
			{
				foreach (CT_CustomProperty item in customPr)
				{
					item.Write(sw, "customPr");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
