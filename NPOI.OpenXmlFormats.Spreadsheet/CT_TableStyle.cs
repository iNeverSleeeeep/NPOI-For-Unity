using NPOI.OpenXml4Net.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_TableStyle
	{
		private List<CT_TableStyleElement> tableStyleElementField;

		private string nameField;

		private bool pivotField;

		private bool tableField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_TableStyleElement> tableStyleElement
		{
			get
			{
				return tableStyleElementField;
			}
			set
			{
				tableStyleElementField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(true)]
		public bool pivot
		{
			get
			{
				return pivotField;
			}
			set
			{
				pivotField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool table
		{
			get
			{
				return tableField;
			}
			set
			{
				tableField = value;
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

		public CT_TableStyle()
		{
			pivotField = true;
			tableField = true;
		}

		public static CT_TableStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableStyle cT_TableStyle = new CT_TableStyle();
			cT_TableStyle.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_TableStyle.pivot = XmlHelper.ReadBool(node.Attributes["pivot"]);
			cT_TableStyle.table = XmlHelper.ReadBool(node.Attributes["table"]);
			cT_TableStyle.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_TableStyle.tableStyleElement = new List<CT_TableStyleElement>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tableStyleElement")
				{
					cT_TableStyle.tableStyleElement.Add(CT_TableStyleElement.Parse(childNode, namespaceManager));
				}
			}
			return cT_TableStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "pivot", pivot);
			XmlHelper.WriteAttribute(sw, "table", table);
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (tableStyleElement != null)
			{
				foreach (CT_TableStyleElement item in tableStyleElement)
				{
					item.Write(sw, "tableStyleElement");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
