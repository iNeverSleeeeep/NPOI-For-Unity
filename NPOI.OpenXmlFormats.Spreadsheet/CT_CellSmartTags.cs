using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellSmartTags
	{
		private List<CT_CellSmartTag> cellSmartTagField;

		private string rField;

		public List<CT_CellSmartTag> cellSmartTag
		{
			get
			{
				return cellSmartTagField;
			}
			set
			{
				cellSmartTagField = value;
			}
		}

		public string r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		public CT_CellSmartTags()
		{
			cellSmartTagField = new List<CT_CellSmartTag>();
		}

		public static CT_CellSmartTags Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellSmartTags cT_CellSmartTags = new CT_CellSmartTags();
			cT_CellSmartTags.r = XmlHelper.ReadString(node.Attributes["r"]);
			cT_CellSmartTags.cellSmartTag = new List<CT_CellSmartTag>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cellSmartTag")
				{
					cT_CellSmartTags.cellSmartTag.Add(CT_CellSmartTag.Parse(childNode, namespaceManager));
				}
			}
			return cT_CellSmartTags;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r", r);
			sw.Write(">");
			if (cellSmartTag != null)
			{
				foreach (CT_CellSmartTag item in cellSmartTag)
				{
					item.Write(sw, "cellSmartTag");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
