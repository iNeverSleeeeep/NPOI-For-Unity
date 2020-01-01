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
	public class CT_IconSet
	{
		private List<CT_Cfvo> cfvoField;

		private ST_IconSetType iconSetField;

		private bool showValueField;

		private bool percentField;

		private bool reverseField;

		[XmlElement]
		public List<CT_Cfvo> cfvo
		{
			get
			{
				return cfvoField;
			}
			set
			{
				cfvoField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_IconSetType.Item3TrafficLights1)]
		public ST_IconSetType iconSet
		{
			get
			{
				return iconSetField;
			}
			set
			{
				iconSetField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showValue
		{
			get
			{
				return showValueField;
			}
			set
			{
				showValueField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool percent
		{
			get
			{
				return percentField;
			}
			set
			{
				percentField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool reverse
		{
			get
			{
				return reverseField;
			}
			set
			{
				reverseField = value;
			}
		}

		public CT_IconSet()
		{
			iconSetField = ST_IconSetType.Item3TrafficLights1;
			showValueField = true;
			percentField = true;
			reverseField = false;
		}

		public static CT_IconSet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_IconSet cT_IconSet = new CT_IconSet();
			if (node.Attributes["iconSet"] != null)
			{
				cT_IconSet.iconSet = (ST_IconSetType)Enum.Parse(typeof(ST_IconSetType), "Item" + node.Attributes["iconSet"].Value);
			}
			cT_IconSet.showValue = XmlHelper.ReadBool(node.Attributes["showValue"]);
			cT_IconSet.percent = XmlHelper.ReadBool(node.Attributes["percent"]);
			cT_IconSet.reverse = XmlHelper.ReadBool(node.Attributes["reverse"]);
			cT_IconSet.cfvo = new List<CT_Cfvo>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cfvo")
				{
					cT_IconSet.cfvo.Add(CT_Cfvo.Parse(childNode, namespaceManager));
				}
			}
			return cT_IconSet;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (iconSet != ST_IconSetType.Item3TrafficLights1)
			{
				XmlHelper.WriteAttribute(sw, "iconSet", iconSet.ToString().Replace("Item", ""));
			}
			if (showValue)
			{
				XmlHelper.WriteAttribute(sw, "showValue", showValue);
			}
			if (percent)
			{
				XmlHelper.WriteAttribute(sw, "percent", percent);
			}
			if (reverse)
			{
				XmlHelper.WriteAttribute(sw, "reverse", reverse);
			}
			sw.Write(">");
			if (cfvo != null)
			{
				foreach (CT_Cfvo item in cfvo)
				{
					item.Write(sw, "cfvo");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
