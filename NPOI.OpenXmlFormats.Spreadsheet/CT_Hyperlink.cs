using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Hyperlink
	{
		private string refField = string.Empty;

		private string idField;

		private string locationField;

		private string tooltipField;

		private string displayField;

		[XmlAttribute("ref")]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		[XmlAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public string location
		{
			get
			{
				return locationField;
			}
			set
			{
				locationField = value;
			}
		}

		[XmlAttribute]
		public string tooltip
		{
			get
			{
				return tooltipField;
			}
			set
			{
				tooltipField = value;
			}
		}

		[XmlAttribute]
		public string display
		{
			get
			{
				return displayField;
			}
			set
			{
				displayField = value;
			}
		}

		public static CT_Hyperlink Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Hyperlink cT_Hyperlink = new CT_Hyperlink();
			cT_Hyperlink.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_Hyperlink.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			cT_Hyperlink.location = XmlHelper.ReadString(node.Attributes["location"]);
			cT_Hyperlink.tooltip = XmlHelper.ReadString(node.Attributes["tooltip"]);
			cT_Hyperlink.display = XmlHelper.ReadString(node.Attributes["display"]);
			return cT_Hyperlink;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			XmlHelper.WriteAttribute(sw, "location", location);
			XmlHelper.WriteAttribute(sw, "tooltip", tooltip);
			XmlHelper.WriteAttribute(sw, "display", display);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
