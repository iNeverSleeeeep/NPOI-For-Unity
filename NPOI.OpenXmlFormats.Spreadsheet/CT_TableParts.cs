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
	public class CT_TableParts
	{
		private List<CT_TablePart> tablePartField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_TablePart> tablePart
		{
			get
			{
				return tablePartField;
			}
			set
			{
				tablePartField = value;
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

		public CT_TableParts()
		{
			tablePartField = new List<CT_TablePart>();
		}

		public CT_TablePart AddNewTablePart()
		{
			CT_TablePart cT_TablePart = new CT_TablePart();
			tablePart.Add(cT_TablePart);
			return cT_TablePart;
		}

		public static CT_TableParts Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableParts cT_TableParts = new CT_TableParts();
			cT_TableParts.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_TableParts.tablePart = new List<CT_TablePart>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tablePart")
				{
					cT_TableParts.tablePart.Add(CT_TablePart.Parse(childNode, namespaceManager));
				}
			}
			return cT_TableParts;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (tablePart != null)
			{
				foreach (CT_TablePart item in tablePart)
				{
					item.Write(sw, "tablePart");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
