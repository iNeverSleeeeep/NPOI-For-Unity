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
	public class CT_TableColumns
	{
		private List<CT_TableColumn> tableColumnField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_TableColumn> tableColumn
		{
			get
			{
				return tableColumnField;
			}
			set
			{
				tableColumnField = value;
			}
		}

		[XmlAttribute]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public static CT_TableColumns Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableColumns cT_TableColumns = new CT_TableColumns();
			if (node.Attributes["count"] != null)
			{
				cT_TableColumns.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			}
			cT_TableColumns.tableColumn = new List<CT_TableColumn>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tableColumn")
				{
					cT_TableColumns.tableColumn.Add(CT_TableColumn.Parse(childNode, namespaceManager));
				}
			}
			return cT_TableColumns;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (tableColumn != null)
			{
				foreach (CT_TableColumn item in tableColumn)
				{
					item.Write(sw, "tableColumn");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
