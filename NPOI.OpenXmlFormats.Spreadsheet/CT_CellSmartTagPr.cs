using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellSmartTagPr
	{
		private string keyField;

		private string valField;

		public string key
		{
			get
			{
				return keyField;
			}
			set
			{
				keyField = value;
			}
		}

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

		public static CT_CellSmartTagPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellSmartTagPr cT_CellSmartTagPr = new CT_CellSmartTagPr();
			cT_CellSmartTagPr.key = XmlHelper.ReadString(node.Attributes["key"]);
			cT_CellSmartTagPr.val = XmlHelper.ReadString(node.Attributes["val"]);
			return cT_CellSmartTagPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "key", key);
			XmlHelper.WriteAttribute(sw, "val", val);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
