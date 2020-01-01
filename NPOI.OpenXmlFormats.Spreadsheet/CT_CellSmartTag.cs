using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellSmartTag
	{
		private List<CT_CellSmartTagPr> cellSmartTagPrField;

		private uint typeField;

		private bool deletedField;

		private bool xmlBasedField;

		public List<CT_CellSmartTagPr> cellSmartTagPr
		{
			get
			{
				return cellSmartTagPrField;
			}
			set
			{
				cellSmartTagPrField = value;
			}
		}

		public uint type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[DefaultValue(false)]
		public bool deleted
		{
			get
			{
				return deletedField;
			}
			set
			{
				deletedField = value;
			}
		}

		[DefaultValue(false)]
		public bool xmlBased
		{
			get
			{
				return xmlBasedField;
			}
			set
			{
				xmlBasedField = value;
			}
		}

		public CT_CellSmartTag()
		{
			cellSmartTagPrField = new List<CT_CellSmartTagPr>();
			deletedField = false;
			xmlBasedField = false;
		}

		public static CT_CellSmartTag Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellSmartTag cT_CellSmartTag = new CT_CellSmartTag();
			cT_CellSmartTag.type = XmlHelper.ReadUInt(node.Attributes["type"]);
			cT_CellSmartTag.deleted = XmlHelper.ReadBool(node.Attributes["deleted"]);
			cT_CellSmartTag.xmlBased = XmlHelper.ReadBool(node.Attributes["xmlBased"]);
			cT_CellSmartTag.cellSmartTagPr = new List<CT_CellSmartTagPr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cellSmartTagPr")
				{
					cT_CellSmartTag.cellSmartTagPr.Add(CT_CellSmartTagPr.Parse(childNode, namespaceManager));
				}
			}
			return cT_CellSmartTag;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type);
			XmlHelper.WriteAttribute(sw, "deleted", deleted);
			XmlHelper.WriteAttribute(sw, "xmlBased", xmlBased);
			sw.Write(">");
			if (cellSmartTagPr != null)
			{
				foreach (CT_CellSmartTagPr item in cellSmartTagPr)
				{
					item.Write(sw, "cellSmartTagPr");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
