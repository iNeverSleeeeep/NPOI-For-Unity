using NPOI.OpenXml4Net.Util;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_TableStyles
	{
		private List<CT_TableStyle> tableStyleField;

		private uint countField;

		private bool countFieldSpecified;

		private string defaultTableStyleField;

		private string defaultPivotStyleField;

		[XmlElement]
		public List<CT_TableStyle> tableStyle
		{
			get
			{
				return tableStyleField;
			}
			set
			{
				tableStyleField = value;
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

		public string defaultTableStyle
		{
			get
			{
				return defaultTableStyleField;
			}
			set
			{
				defaultTableStyleField = value;
			}
		}

		public string defaultPivotStyle
		{
			get
			{
				return defaultPivotStyleField;
			}
			set
			{
				defaultPivotStyleField = value;
			}
		}

		public static CT_TableStyles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableStyles cT_TableStyles = new CT_TableStyles();
			cT_TableStyles.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_TableStyles.defaultTableStyle = XmlHelper.ReadString(node.Attributes["defaultTableStyle"]);
			cT_TableStyles.defaultPivotStyle = XmlHelper.ReadString(node.Attributes["defaultPivotStyle"]);
			cT_TableStyles.tableStyle = new List<CT_TableStyle>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tableStyle")
				{
					cT_TableStyles.tableStyle.Add(CT_TableStyle.Parse(childNode, namespaceManager));
				}
			}
			return cT_TableStyles;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", (double)count, true);
			XmlHelper.WriteAttribute(sw, "defaultTableStyle", defaultTableStyle);
			XmlHelper.WriteAttribute(sw, "defaultPivotStyle", defaultPivotStyle);
			sw.Write(">");
			if (tableStyle != null)
			{
				foreach (CT_TableStyle item in tableStyle)
				{
					item.Write(sw, "tableStyle");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
