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
	public class CT_CellStyles
	{
		private List<CT_CellStyle> cellStyleField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_CellStyle> cellStyle
		{
			get
			{
				return cellStyleField;
			}
			set
			{
				cellStyleField = value;
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

		public static CT_CellStyles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellStyles cT_CellStyles = new CT_CellStyles();
			cT_CellStyles.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_CellStyles.cellStyle = new List<CT_CellStyle>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cellStyle")
				{
					cT_CellStyles.cellStyle.Add(CT_CellStyle.Parse(childNode, namespaceManager));
				}
			}
			return cT_CellStyles;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (cellStyle != null)
			{
				foreach (CT_CellStyle item in cellStyle)
				{
					item.Write(sw, "cellStyle");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
